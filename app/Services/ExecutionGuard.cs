using System.Diagnostics;

namespace app.Services;

/// <summary>
/// Throws once a script's wall-clock budget is exceeded. Blazor WASM is
/// single-threaded, so nothing external (a Task.Delay race, a cancellation
/// token) can preempt a student's synchronous `while (true) {}` from outside —
/// the only way to actually stop it is to make the STUDENT'S OWN compiled code
/// check a deadline periodically. <see cref="LoopGuardRewriter"/> injects a
/// call to <see cref="CheckDeadline"/> at the top of every loop body in the
/// parsed script for exactly this reason, before it's ever compiled.
///
/// Known remaining gap (see REQUIREMENTS.md): this only guards loops, not deep/
/// infinite recursion without a loop — recursion that never returns still hangs
/// the tab. Loops are the far more likely accident for the syntax this game
/// teaches (Level 4 onward), so they're covered first.
/// </summary>
public static class ExecutionGuard
{
    [ThreadStatic] private static Stopwatch? _stopwatch;
    [ThreadStatic] private static TimeSpan _budget;

    internal static void Start(TimeSpan budget)
    {
        _stopwatch = Stopwatch.StartNew();
        _budget = budget;
    }

    internal static void Stop() => _stopwatch = null;

    public static void CheckDeadline()
    {
        if (_stopwatch is { } sw && sw.Elapsed > _budget)
        {
            throw new TimeoutException("Your code ran for too long (likely an infinite loop) and was stopped.");
        }
    }
}
