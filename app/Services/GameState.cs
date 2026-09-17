namespace app.Services;

public sealed class TaskProgress
{
    public bool Completed { get; set; }
    public int HintsRevealed { get; set; }
    public bool UsedSolutionHint { get; set; }

    // Failed Run Code attempts on THIS task specifically — separate from
    // GameState.ConsecutiveFailures (which only tracks streak-breaking across
    // the whole session). Used to gate the final, solution-revealing hint tier
    // behind actually having tried, per player feedback that showing the exact
    // answer on demand (with no attempt required) defeated the point of hints.
    public int FailedAttempts { get; set; }
}

/// <summary>
/// Everything persisted to localStorage for one player. Plain data only (no
/// behavior) so it round-trips through System.Text.Json cleanly — see
/// <see cref="GameStateService"/> for the logic that mutates this.
/// </summary>
public sealed class GameState
{
    // Asked once at first launch (see Home.razor's username prompt) purely so
    // Microsoft Clarity session recordings/tags can be identified by name in
    // the Clarity dashboard — see GameStateService.SetUsernameAsync. This is
    // NOT an account system; there's no backend, so nothing round-trips this
    // value anywhere except this browser's own localStorage and Clarity's own
    // (write-only, from this app's side) session tagging.
    public string? Username { get; set; }

    // Whether task/level/achievement sound effects play (see wwwroot/js/sfx.js
    // and GameStateService.PlaySoundAsync). Persisted so muting sticks across
    // reloads, defaults on since it's a nice-to-have most players want.
    public bool SoundEnabled { get; set; } = true;

    // "light", "dark", or "system" (follow the OS preference) — see
    // wwwroot/js/theme.js and GameStateService.SetThemeAsync. Stored as a
    // plain string (not an enum) so it round-trips through this same JSON
    // blob without a converter, and so wwwroot/index.html's pre-boot inline
    // script (plain JS, no access to a C# enum) can read it directly.
    public string Theme { get; set; } = "system";

    public int Points { get; set; }
    public int Streak { get; set; }
    public int ConsecutiveFailures { get; set; }
    public int MaxStreak { get; set; }
    public int NoHintCompletions { get; set; }
    public int ComebackCompletions { get; set; }

    // Visual game mode (see REQUIREMENTS.md): completing a visual task earns
    // its normal task points like any other task; choosing to hit "Play Again"
    // afterward is a second, separate reward moment — a small one-time bonus
    // per task, tracked here so it can't be farmed by replaying repeatedly.
    public HashSet<string> PlayedVisualTasks { get; set; } = new();

    public Dictionary<string, TaskProgress> Tasks { get; set; } = new();
    public HashSet<string> UnlockedAchievements { get; set; } = new();

    // Flash card quiz stages the player has already cleared (see
    // content/Models.cs's QuizStage) — keyed by QuizKey so a passed quiz
    // doesn't have to be retaken just to keep moving through the level.
    public HashSet<string> PassedQuizzes { get; set; } = new();

    // Set once, the first time every task in every level is complete (see
    // GameStateService.CheckCompletionAsync) — powers Pages/Certificate.razor.
    // Null means "not completed yet"; never cleared once set, even if a
    // future level is added and this no longer covers "everything" (the
    // certificate is a record of a real accomplishment at the time, not a
    // live "100%" gauge).
    public DateTime? CompletedAt { get; set; }

    // Set once, on this player's very first LoadAsync — paired with
    // CompletedAt so the certificate can show "completed in N days." Best
    // available proxy for "when they started" for a save that predates this
    // field (see GameStateService.LoadAsync).
    public DateTime? StartedAt { get; set; }

    // Distinct calendar dates (UTC, "yyyy-MM-dd") on which the player
    // completed at least one task — gamifies coming back on different days,
    // separately from Streak (which is about consecutive PASSES, win or lose
    // across days, and resets on repeated failures within a session).
    public HashSet<string> DaysPlayed { get; set; } = new();

    public int CurrentDayStreak { get; set; }
    public int MaxDayStreak { get; set; }

    public static string TaskKey(int levelId, int taskId) => $"{levelId}-{taskId}";
    public static string QuizKey(int levelId, int afterTaskId) => $"{levelId}-quiz-{afterTaskId}";
}
