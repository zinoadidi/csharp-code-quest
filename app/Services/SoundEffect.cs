namespace app.Services;

/// <summary>
/// The full catalog of sound-effect names understood by wwwroot/js/sfx.js.
/// Every call site should reference one of these constants — via
/// <see cref="GameStateService.PlaySoundAsync"/>, the single gateway that
/// actually decides whether to play (respecting <see cref="GameState.SoundEnabled"/>)
/// and talks to JS interop — instead of a raw string literal. That keeps the
/// whole catalog in one place: adding, renaming, or retiring a sound effect
/// is a one-file change instead of hunting across every page/component that
/// might call it, and a typo becomes a compile error instead of a silently
/// dropped sound.
/// </summary>
public static class SoundEffect
{
    public const string TaskComplete = "taskComplete";
    public const string TaskFailed = "taskFailed";
    public const string LevelOpen = "levelOpen";
    public const string Navigate = "navigate";
    public const string LevelComplete = "levelComplete";
    public const string QuestComplete = "questComplete";
    public const string Achievement = "achievement";
    public const string QuizCorrect = "quizCorrect";
    public const string QuizIncorrect = "quizIncorrect";
    public const string QuizPass = "quizPass";
    public const string QuizFail = "quizFail";
    public const string FlashcardStreak = "flashcardStreak";
}
