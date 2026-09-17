namespace app.Content;

/// <summary>
/// The full, ordered set of levels the app renders. Add a new entry here the
/// moment a new LevelNN_*.cs file's Instance is ready — nothing else in the app
/// enumerates levels any other way.
/// </summary>
public static class AllLevels
{
    public static readonly IReadOnlyList<Level> Levels = new List<Level>
    {
        Level01_Fundamentals.Instance,
        Level02_OperatorsAndExpressions.Instance,
        Level03_ControlFlow.Instance,
        Level04_Loops.Instance,
        Level05_ArraysAndCollections.Instance,
        Level06_MethodsAndFunctions.Instance,
        Level07_StringsAndTextProcessing.Instance,
        Level08_ClassesAndObjectsI.Instance,
        Level09_ClassesAndObjectsII.Instance,
        Level10_CollectionsAndLinq.Instance,
        Level11_ErrorHandling.Instance,
        Level12_Capstone.Instance,
    };
}
