using System.Globalization;
using System.Reflection;
using app.GameApi;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace app.Services;

public sealed record RunResult(bool Success, string Output, string? ErrorMessage, IReadOnlyList<CanvasCommand> CanvasCommands)
{
    public static RunResult Ok(string output, IReadOnlyList<CanvasCommand> canvasCommands) => new(true, output, null, canvasCommands);
    public static RunResult Fail(string message) => new(false, "", message, []);
}

/// <summary>
/// Compiles and runs student-typed C# entirely client-side via Roslyn, inside
/// the Blazor WebAssembly sandbox. Extracted from the original execution-engine
/// proof of concept (see ../README.md's "How Roslyn scripting actually works
/// here" for the full story of why this can't just be
/// CSharpScript.EvaluateAsync) so the real game UI can reuse it as a plain
/// service instead of duplicating the logic per-page.
/// </summary>
public sealed class CSharpRunner(HttpClient http)
{
    // A student's script gets this long, wall-clock, before ExecutionGuard
    // trips — see LoopGuardRewriter/ExecutionGuard for why this is the only way
    // to actually stop a runaway loop in a single-threaded WASM host (nothing
    // external can preempt synchronous CPU-bound code here). Generous enough
    // that no legitimate task solution should ever come close to it.
    private static readonly TimeSpan ExecutionBudget = TimeSpan.FromSeconds(3);

    // Names (without extension) of assemblies sandboxed scripts may reference —
    // both BCL assemblies and "app" itself (for Services/GameApi types like
    // Canvas and ExecutionGuard). Extend this (and the matching
    // RoslynRefAssemblyName item group / CopyOwnAssemblyForRoslyn target in
    // app.csproj) together when task content needs more of the BCL surface —
    // see REQUIREMENTS.md's "Known gaps" section.
    private static readonly string[] RequiredAssemblies =
    [
        "System.Private.CoreLib",
        "System.Runtime",
        "System.Console",
        "System.Linq",
        "System.Collections",
        "System.Collections.Concurrent",
        "System.Collections.Immutable",
        "System.ObjectModel",
        "netstandard",
        "app",
    ];

    private List<MetadataReference>? _cachedReferences;

    private async Task<List<MetadataReference>> GetReferencesAsync()
    {
        if (_cachedReferences is not null)
        {
            return _cachedReferences;
        }

        // Plain, unhashed copies placed into wwwroot/roslyn-refs at build time
        // by the CopyRoslynReferenceAssemblies/CopyOwnAssemblyForRoslyn MSBuild
        // targets (see app.csproj), fetched here at runtime because in-browser
        // WASM assemblies have no filesystem Assembly.Location to build a
        // MetadataReference from via reflection the normal way.
        var references = new List<MetadataReference>();
        foreach (var required in RequiredAssemblies)
        {
            var bytes = await http.GetByteArrayAsync($"roslyn-refs/{required}.dll");
            references.Add(MetadataReference.CreateFromImage(bytes));
        }

        _cachedReferences = references;
        return references;
    }

    public async Task<RunResult> RunAsync(string code)
    {
        // Task checks compare printed output against fixed, invariant-style
        // strings (e.g. exactly "3.5"). Console.WriteLine formats numbers using
        // the CURRENT THREAD CULTURE, not a fixed format — a student's browser
        // can report any locale, so force invariant culture on every single run
        // (not just once at startup) to keep grading deterministic regardless
        // of the visitor's locale.
        var originalCulture = CultureInfo.CurrentCulture;
        var originalUiCulture = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

        var stdout = new StringWriter();
        var originalOut = Console.Out;
        Console.SetOut(stdout);
        Canvas.BeginRecording();
        try
        {
            var references = await GetReferencesAsync();

            // Deliberately NOT Microsoft.CodeAnalysis.CSharp.Scripting's
            // CSharpScript.EvaluateAsync/RunAsync — see ../README.md for why
            // that throws inside WASM. Raw CSharpCompilation script-compilation
            // sidesteps it entirely: we control 100% of the references used.
            var parseOptions = CSharpParseOptions.Default.WithKind(SourceCodeKind.Script);
            var syntaxTree = CSharpSyntaxTree.ParseText(code, parseOptions);

            // Inject a deadline check at the top of every loop body before
            // compiling — see LoopGuardRewriter/ExecutionGuard. The student
            // never sees this; it's invisible instrumentation on the parsed
            // tree, not something they typed.
            var guardedRoot = new LoopGuardRewriter().Visit(syntaxTree.GetRoot());
            syntaxTree = syntaxTree.WithRootAndOptions(guardedRoot, parseOptions);

            var compilationOptions = new CSharpCompilationOptions(
                OutputKind.DynamicallyLinkedLibrary,
                usings: ["System", "System.Linq", "System.Collections.Generic", "app.GameApi"],
                optimizationLevel: OptimizationLevel.Debug,
                scriptClassName: "StudentScript");

            var compilation = CSharpCompilation.CreateScriptCompilation(
                assemblyName: "StudentScript_" + Guid.NewGuid().ToString("N"),
                syntaxTree: syntaxTree,
                references: references,
                options: compilationOptions,
                returnType: typeof(object));

            using var peStream = new MemoryStream();
            var emitResult = compilation.Emit(peStream);

            if (!emitResult.Success)
            {
                // Normally there's at least one Severity.Error diagnostic explaining
                // why. But an empty or comment-only script (e.g. a task's untouched
                // starter code, before a student writes anything) can fail Emit with
                // NO error-severity diagnostics at all — Roslyn's script Emit path
                // has a quirk where a submission with no executable statements
                // doesn't get a proper diagnostic. Rather than show a blank,
                // confusing "Compilation error:" box in that case, fall back to
                // every diagnostic regardless of severity, and if there's truly
                // nothing to report, say so plainly instead of showing an empty box.
                var errors = emitResult.Diagnostics
                    .Where(d => d.Severity == DiagnosticSeverity.Error)
                    .Select(d => d.ToString())
                    .ToList();
                if (errors.Count == 0)
                {
                    errors = emitResult.Diagnostics.Select(d => d.ToString()).ToList();
                }
                if (errors.Count == 0)
                {
                    return RunResult.Fail("Your code doesn't compile to anything runnable yet — try writing a complete statement (e.g. a line ending in a semicolon).");
                }
                return RunResult.Fail("Compilation error:\n" + string.Join("\n", errors));
            }

            peStream.Seek(0, SeekOrigin.Begin);
            var assembly = Assembly.Load(peStream.ToArray());

            var scriptClassName = compilation.ScriptClass!.MetadataName;
            var scriptType = assembly.GetType(scriptClassName)
                ?? throw new InvalidOperationException($"Could not locate generated script type '{scriptClassName}'.");
            var factory = scriptType.GetMethod("<Factory>", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                ?? throw new InvalidOperationException("Could not locate generated script entry point '<Factory>'.");

            // The generated <Factory>(object[] submissionStates) method expects
            // a slot array: index 0 is reserved for host "globals" (null here,
            // no globals object passed), index 1 receives this submission's own
            // instance once constructed.
            var submissionStates = new object?[2];

            ExecutionGuard.Start(ExecutionBudget);
            var task = (Task<object>)factory.Invoke(null, [submissionStates])!;
            var result = await task;

            var output = stdout.ToString();
            if (result is not null)
            {
                output += (output.Length > 0 ? "\n" : "") + $"=> {result}";
            }
            return RunResult.Ok(output, Canvas.EndRecording());
        }
        catch (TargetInvocationException tie) when (tie.InnerException is TimeoutException timeout)
        {
            return RunResult.Fail(timeout.Message);
        }
        catch (TimeoutException timeout)
        {
            return RunResult.Fail(timeout.Message);
        }
        catch (Exception ex)
        {
            return RunResult.Fail("Runtime error:\n" + ex.Message);
        }
        finally
        {
            Canvas.EndRecording();
            ExecutionGuard.Stop();
            Console.SetOut(originalOut);
            CultureInfo.CurrentCulture = originalCulture;
            CultureInfo.CurrentUICulture = originalUiCulture;
        }
    }
}
