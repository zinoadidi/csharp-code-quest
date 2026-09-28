namespace app.Services;

public sealed class TaskProgress
{
    public bool Completed { get; set; }
    public int HintsRevealed { get; set; }
    public bool UsedSolutionHint { get; set; }

    // Ever skipped via the Skip button (see GameStateService.SkipTaskAsync).
    // Stays true even if the task is solved later — it is the permanent record
    // of skip spending, so per-level/total skip caps can't be farmed by
    // skip → solve → skip again. Never implies completion on its own.

    // Failed Run Code attempts on THIS task specifically — separate from
    // GameState.ConsecutiveFailures (which only tracks streak-breaking across
    // the whole session). Used to gate the final, solution-revealing hint tier
    // behind actually having tried, per player feedback that showing the exact
    // answer on demand (with no attempt required) defeated the point of hints.
    public int FailedAttempts { get; set; }

    public bool Skipped { get; set; }
}

/// <summary>
/// Everything persisted to localStorage for one player. Plain data only (no
/// behavior) so it round-trips through System.Text.Json cleanly — see
/// <see cref="GameStateService"/> for the logic that mutates this.
/// </summary>
public sealed class GameState
{
    // Asked once at first launch (see Home.razor's username prompt) so
    // Microsoft Clarity session recordings/tags can be identified by name in
    // the Clarity dashboard — see GameStateService.SetUsernameAsync. Since
    // the server integration this doubles as the account's display name and
    // round-trips to the player's server profile/progress docs.
    public string? Username { get; set; }

    // Server account binding (see QuestAccountService). Null for saves that
    // predate the server integration — those players link up via the
    // migration prompt (Home.razor) without losing local progress.
    public string? UserId { get; set; }

    // Student's school email, collected at registration and used as the
    // second login/restore identifier alongside the username. Null for
    // demo-class accounts and pre-email saves until mapped on restore.
    public string? Email { get; set; }

    // Demo-class sessions are server profiles flagged in the database (see
    // QuestServerConfig) — this just exempts them from the school-email rule.
    public bool IsDemo { get; set; }

    // Client-side last-write stamp, refreshed on every SaveAsync — the
    // server progress doc carries its own copy for last-write-wins sync.
    public DateTime UpdatedAt { get; set; }

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

    // Skip wallet (see GameStateService for the economy rules: 2 skips max per
    // level, wallet capped at 7, +1 earned per 3-day streak). Defaults to a
    // full wallet: System.Text.Json leaves missing properties untouched, so
    // saves from before this feature existed deserialize straight to 7 with
    // no migration step, while a wallet deliberately spent to 0 round-trips
    // as an explicit 0.
    public int SkipsAvailable { get; set; } = 7;

    // Cumulative skips earned back via day-streak rewards (spent skips are
    // derived from Tasks[*].Skipped, so only earnings need a counter).
    public int SkipsEarnedFromStreaks { get; set; }

    // The CurrentDayStreak value at which the last streak-skip was awarded —
    // guards the 3-day milestone so it fires exactly once per milestone even
    // if several tasks complete on the same day.
    public int LastSkipAwardDayStreak { get; set; }

    // Set once, the first time Pages/Certificate.razor is viewed after
    // CompletedAt is set — lets that page play a one-time entrance
    // celebration on the actual moment of first seeing the finished
    // certificate, without repeating it on every later visit/reload.
    public bool CertificateCelebrated { get; set; }

    public static string TaskKey(int levelId, int taskId) => $"{levelId}-{taskId}";
    public static string QuizKey(int levelId, int afterTaskId) => $"{levelId}-quiz-{afterTaskId}";
}
