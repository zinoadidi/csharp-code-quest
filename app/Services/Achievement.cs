using app.Content;

namespace app.Services;

public sealed record Achievement(string Id, string Title, string Description, string Icon, Func<GameState, IReadOnlyList<Level>, bool> IsUnlocked);

/// <summary>
/// The fixed catalog of achievements (see content/GameSystems.md for the
/// design rationale). Evaluated after every state-mutating action in
/// <see cref="GameStateService"/>; whichever ones newly satisfy their predicate
/// get unlocked and surfaced as a celebratory popup.
/// </summary>
public static class Achievements
{
    public static readonly IReadOnlyList<Achievement> All =
    [
        new("first-blood", "First Blood", "Complete your very first task.", "🩸",
            (state, _) => state.Tasks.Values.Any(t => t.Completed)),

        new("streak-3", "Warming Up", "Reach a 3-task streak.", "🔥",
            (state, _) => state.MaxStreak >= 3),

        new("streak-5", "On a Roll", "Reach a 5-task streak.", "🔥",
            (state, _) => state.MaxStreak >= 5),

        new("streak-10", "Unstoppable", "Reach a 10-task streak.", "🔥",
            (state, _) => state.MaxStreak >= 10),

        new("sharp-mind", "Sharp Mind", "Complete 5 tasks without ever revealing a hint.", "🧠",
            (state, _) => state.NoHintCompletions >= 5),

        new("comeback-kid", "Comeback Kid", "Pass a task right after copying in the solution hint. No shame — you finished it.", "💪",
            (state, _) => state.ComebackCompletions >= 1),

        new("showtime", "Showtime!", "Play back a visual task you built.", "🎬",
            (state, _) => state.PlayedVisualTasks.Count >= 1),

        new("level-1-up", "Level Up!", "Complete every task in Level 1.", "⭐",
            (state, levels) => IsLevelComplete(state, levels, 1)),

        new("halfway-there", "Halfway There", "Complete every task in Level 6.", "🏔️",
            (state, levels) => IsLevelComplete(state, levels, 6)),

        new("csharp-master", "C# Master", "Complete every task in Level 12 — the whole quest.", "👑",
            (state, levels) => IsLevelComplete(state, levels, 12)),

        new("streak-15", "Legendary", "Reach a 15-task streak.", "⚡",
            (state, _) => state.MaxStreak >= 15),

        new("quarter-quest", "Quarter Quest", "Complete every task in Level 3.", "🧗",
            (state, levels) => IsLevelComplete(state, levels, 3)),

        new("double-digits", "Double Digits", "Complete every task in Level 10.", "🔟",
            (state, levels) => IsLevelComplete(state, levels, 10)),

        new("century", "Century Club", "Earn 100 points.", "💯",
            (state, _) => state.Points >= 100),

        new("half-k", "High Roller", "Earn 500 points.", "💰",
            (state, _) => state.Points >= 500),

        new("task-25", "Getting Serious", "Complete 25 tasks total.", "📈",
            (state, _) => state.Tasks.Values.Count(t => t.Completed) >= 25),

        new("task-50", "Grinder", "Complete 50 tasks total.", "🏗️",
            (state, _) => state.Tasks.Values.Count(t => t.Completed) >= 50),

        new("quiz-whiz", "Quiz Whiz", "Pass 5 quiz stages.", "🎓",
            (state, _) => state.PassedQuizzes.Count >= 5),

        new("quiz-master", "Quiz Master", "Pass every quiz stage in the game.", "🏆",
            (state, levels) => levels.Sum(l => l.QuizStagesOrEmpty.Count) > 0
                && state.PassedQuizzes.Count >= levels.Sum(l => l.QuizStagesOrEmpty.Count)),

        new("directors-cut", "Director's Cut", "Replay 5 visual tasks.", "🎥",
            (state, _) => state.PlayedVisualTasks.Count >= 5),

        new("hint-free-legend", "Hint-Free Legend", "Complete 20 tasks without ever revealing a hint.", "🦉",
            (state, _) => state.NoHintCompletions >= 20),

        new("momentum", "Momentum", "Complete every task in Level 5.", "🚀",
            (state, levels) => IsLevelComplete(state, levels, 5)),

        new("almost-there", "Almost There", "Complete every task in Level 8.", "🌠",
            (state, levels) => IsLevelComplete(state, levels, 8)),

        new("streak-20", "Iron Will", "Reach a 20-task streak.", "🛡️",
            (state, _) => state.MaxStreak >= 20),

        new("thousandaire", "Thousandaire", "Earn 1000 points.", "🤑",
            (state, _) => state.Points >= 1000),

        new("day-2", "Back Again", "Play on 2 different days.", "📅",
            (state, _) => state.DaysPlayed.Count >= 2),

        new("day-5", "Regular", "Play on 5 different days.", "🗓️",
            (state, _) => state.DaysPlayed.Count >= 5),

        new("day-7", "One Week In", "Play on 7 different days.", "🧭",
            (state, _) => state.DaysPlayed.Count >= 7),

        new("daystreak-3", "Three in a Row", "Play on 3 consecutive days.", "🔗",
            (state, _) => state.MaxDayStreak >= 3),

        new("daystreak-7", "Full Week Streak", "Play on 7 consecutive days.", "🌆",
            (state, _) => state.MaxDayStreak >= 7),
    ];

    private static bool IsLevelComplete(GameState state, IReadOnlyList<Level> levels, int levelId)
    {
        var level = levels.FirstOrDefault(l => l.Id == levelId);
        if (level is null || level.Tasks.Count == 0) return false;
        return level.Tasks.All(t => state.Tasks.TryGetValue(GameState.TaskKey(levelId, t.Id), out var p) && p.Completed);
    }
}
