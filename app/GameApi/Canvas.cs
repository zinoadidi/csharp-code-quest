namespace app.GameApi;

/// <summary>
/// One recorded change to the visual scene. Student code never sees this type —
/// it only calls the static <see cref="Canvas"/> methods below, which append to
/// an internal log. The host (see CanvasView.razor) replays the log as an
/// animation after the run completes, rather than rendering live while the
/// student's code executes — this keeps the execution model exactly the same
/// "run to completion, then look at the result" shape as every text-output
/// task, just with a visual result instead of console text.
/// </summary>
public sealed record CanvasCommand(string Kind, string ShapeId, string? ShapeType, double? X, double? Y, double? Size, string? Color);

/// <summary>
/// The simple "move shapes around" API exposed to student scripts for visual
/// tasks (see REQUIREMENTS.md's "Visual game mode" section). Kept deliberately
/// tiny — a handful of static methods a beginner can learn in one task — rather
/// than a general drawing/game engine.
///
/// Technical note: for a student script to call these methods at all, the
/// Roslyn compilation needs a MetadataReference to THIS assembly (app.dll) —
/// see the CopyOwnAssemblyForRoslyn target in app.csproj and RequiredAssemblies
/// in Services/CSharpRunner.cs for why that's exactly as fiddly as referencing
/// the BCL was, and how it's solved the same way (a plain unhashed copy fetched
/// as raw bytes at runtime, since in-browser assemblies have no Assembly.Location).
/// </summary>
public static class Canvas
{
    [ThreadStatic]
    private static List<CanvasCommand>? _log;

    /// <summary>Called by CSharpRunner immediately before each script run.</summary>
    internal static void BeginRecording() => _log = [];

    /// <summary>Called by CSharpRunner immediately after each script run.</summary>
    internal static IReadOnlyList<CanvasCommand> EndRecording()
    {
        var log = _log ?? [];
        _log = null;
        return log;
    }

    public static void AddShape(string id, string type, double x, double y, double size = 40, string color = "#7dd3fc")
        => _log?.Add(new CanvasCommand("add", id, type, x, y, size, color));

    public static void MoveTo(string id, double x, double y)
        => _log?.Add(new CanvasCommand("move", id, null, x, y, null, null));

    public static void MoveBy(string id, double dx, double dy)
        => _log?.Add(new CanvasCommand("moveBy", id, null, dx, dy, null, null));

    public static void SetColor(string id, string color)
        => _log?.Add(new CanvasCommand("color", id, null, null, null, null, color));

    public static void Remove(string id)
        => _log?.Add(new CanvasCommand("remove", id, null, null, null, null, null));
}
