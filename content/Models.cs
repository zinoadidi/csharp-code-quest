namespace app.Content;

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

/// <summary>
/// A task is either a plain "write a snippet, check the output" exercise, a
/// mini-game the student completes by writing the missing code and can then
/// actually play once it passes, or a Flashcard — a no-code glossary deck
/// (see GameTask.Flashcards) shown as Task 0 of a level, flipped through one
/// card at a time and dismissed rather than run/checked.
/// </summary>
public enum TaskKind
{
    Snippet,
    MiniGame,
    Flashcard
}

/// <summary>
/// One glossary flashcard within a Flashcard-kind GameTask's Flashcards deck —
/// front shows <see cref="Term"/>, back (revealed on tap) shows
/// <see cref="Definition"/>.
/// </summary>
public sealed record GlossaryTerm(string Term, string Definition);

/// <summary>
/// One rung of a task's progressively-revealed hint ladder. The last tier for
/// every task should have <see cref="IsSolution"/> set to true and contain the
/// actual working code, copy/paste-ready.
/// </summary>
public sealed record HintTier(string Text, bool IsSolution = false);

public sealed record GameTask(
    int Id,
    string Title,
    string Description,
    Difficulty Difficulty,
    int Points,
    string StarterCode,
    IReadOnlyList<HintTier> Hints,
    Func<string, bool> CheckOutput,
    string? Example = null,
    TaskKind Kind = TaskKind.Snippet,
    Func<string, bool>? CheckSource = null,
    IReadOnlyList<GlossaryTerm>? Flashcards = null
)
{
    public bool Passes(string output, string source) =>
        CheckOutput(output) && (CheckSource is null || CheckSource(source));

    /// <summary>
    /// A no-code glossary deck of <paramref name="terms"/>, flipped through
    /// one card at a time — see TaskKind's doc. Always Id 0 so it sorts
    /// before a level's other tasks (which start at 1) without renumbering
    /// them or colliding with existing save data. Awards Points once, on
    /// completing the whole deck, exactly like any other single task.
    /// </summary>
    public static GameTask FlashcardDeck(params GlossaryTerm[] terms) => new(
        Id: 0,
        Title: "Quick Concepts",
        Description: "",
        Difficulty: Difficulty.Easy,
        Points: 20,
        StarterCode: "",
        Hints: [],
        CheckOutput: _ => true,
        Kind: TaskKind.Flashcard,
        Flashcards: terms);
}

/// <summary>
/// One multiple-choice flash card: a prompt and exactly 4 options, one of
/// which is correct (by index into <see cref="Options"/>). Also doubles as a
/// true/false swipe card when the owning QuizStage's Format is
/// SwipeTrueFalse — Options should then be exactly ["True", "False"] and
/// CorrectIndex 0 or 1, since QuizView renders those as a swipe gesture
/// instead of a 4-button grid.
/// </summary>
public sealed record QuizCard(string Prompt, IReadOnlyList<string> Options, int CorrectIndex);

/// <summary>
/// How a QuizStage's cards are presented — see QuizCard's doc for how the
/// same record shape covers both.
/// </summary>
public enum QuizFormat
{
    MultipleChoice,
    SwipeTrueFalse
}

/// <summary>
/// A retry-until-you-pass flash card quiz inserted between tasks in a level
/// (see Level.QuizStages) — reinforces keywords/concepts from the tasks
/// completed so far via quick recall rather than writing code. Blocks
/// progressing past <see cref="AfterTaskId"/> until passed.
///
/// <see cref="Cards"/> is a POOL, not necessarily what one attempt asks —
/// each attempt samples <see cref="QuestionsPerAttempt"/> cards at random
/// (see QuizView.razor) so a bigger pool means players don't see the exact
/// same questions, in the exact same order, on every retry or replay.
/// </summary>
public sealed record QuizStage(
    string Title,
    int AfterTaskId,
    IReadOnlyList<QuizCard> Cards,
    QuizFormat Format = QuizFormat.MultipleChoice,
    int QuestionsPerAttempt = 5)
{
    // A student needs to get at least this fraction of cards right in one pass
    // to clear the stage; falling short just re-shuffles and retries — no
    // penalty beyond time, since the point is memory reinforcement, not a gate
    // that could soft-lock progress.
    public const double PassThreshold = 0.8;
}

public sealed record Level(
    int Id,
    string Name,
    string Description,
    IReadOnlyList<GameTask> Tasks,
    IReadOnlyList<QuizStage>? QuizStages = null
)
{
    public IReadOnlyList<QuizStage> QuizStagesOrEmpty => QuizStages ?? [];
}

/// <summary>
/// Shared helpers for writing <see cref="GameTask.CheckOutput"/> predicates.
/// Multi-line tasks should compare line-by-line via <see cref="LinesEqual"/>
/// rather than raw string equality, since raw output can differ by trailing
/// whitespace or \r\n vs \n across environments even when the printed lines
/// are exactly right.
/// </summary>
public static class OutputChecks
{
    public static string[] Lines(string output) =>
        output
            .Replace("\r\n", "\n")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    public static bool LinesEqual(string output, params string[] expectedLines) =>
        Lines(output).SequenceEqual(expectedLines);
}
