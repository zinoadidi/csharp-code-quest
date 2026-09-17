namespace app.Content;

/// <summary>
/// Level 5 — Arrays &amp; Collections I.
///
/// Arrays, List&lt;T&gt;, indexing, and iterating. Closes with this level's visual
/// task: lining up a row of shapes using values pulled straight out of an array,
/// the first time the visual mode is driven by a collection instead of one-off
/// calls.
/// </summary>
public static class Level05_ArraysAndCollections
{
    public static readonly Level Instance = new(
        Id: 5,
        Name: "Arrays & Collections I",
        Description: "Store more than one value at a time: arrays, List<T>, and looping over both.",
        Tasks: new List<GameTask>
        {
            GameTask.FlashcardDeck(
                new GlossaryTerm("Array", "A fixed-size list of values, all the same type, stored under one variable name and reached by position."),
                new GlossaryTerm("Index", "A number that says WHERE in an array or list a value lives — like a house number on a street."),
                new GlossaryTerm("Element", "One single value stored inside an array or list — a list of 5 names has 5 elements."),
                new GlossaryTerm("List", "Like an array, but it can grow or shrink while your program runs — C#'s List<T> is the go-to when you don't know the size in advance."),
                new GlossaryTerm("Collection", "The general term for any type that holds multiple values together — arrays, lists, dictionaries, and sets are all collections."),
                new GlossaryTerm("Length", "How many elements a collection currently holds — arrays use .Length, lists use .Count."),
                new GlossaryTerm("Foreach Loop", "A loop built specifically for walking through every element of a collection, one at a time, without needing to manage an index yourself."),
                new GlossaryTerm("Zero-Based Indexing", "The rule that the FIRST element in an array or list is at position 0, not 1 — so a 5-element array's valid indexes are 0 through 4."),
                new GlossaryTerm("Out of Bounds", "Trying to access an index that doesn't exist in a collection (like index 5 in a 5-element array) — this crashes your program instead of just returning nothing."),
                new GlossaryTerm("Dynamic Array", "Another name for a resizable collection like List<T> — as opposed to a plain array, whose size is locked in the moment it's created.")),
            new(
                Id: 1,
                Title: "Roll Call",
                Description: "Create an int array called scores with the values 10, 20, 30, 40, 50, then print each one with a foreach loop.",
                Difficulty: Difficulty.Easy,
                Points: 15,
                StarterCode: "// Create int[] scores = { 10, 20, 30, 40, 50 };\n// Use foreach to print each score\n",
                Example: "int[] scores = { 10, 20, 30, 40, 50 };\nforeach (int score in scores)\n{\n    Console.WriteLine(score);\n}",
                Hints: new List<HintTier>
                {
                    new("Declare the array with curly braces: int[] scores = { 10, 20, 30, 40, 50 };"),
                    new("int[] scores = { 10, 20, 30, 40, 50 };\nforeach (int score in scores)\n{\n    Console.WriteLine(score);\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "10", "20", "30", "40", "50")
            ),
            new(
                Id: 2,
                Title: "Total It Up",
                Description: "Given int[] nums = { 4, 8, 15, 16, 23 }, use a foreach loop to add every value into a total, then print the total.",
                Difficulty: Difficulty.Easy,
                Points: 15,
                StarterCode: "int[] nums = { 4, 8, 15, 16, 23 };\nint total = 0;\n// TODO: foreach over nums, adding each into total\n// Print total\n",
                Example: "int[] nums = { 4, 8, 15, 16, 23 };\nint total = 0;\nforeach (int n in nums)\n{\n    total += n;\n}\nConsole.WriteLine(total);\n// Output: 66",
                Hints: new List<HintTier>
                {
                    new("foreach (int n in nums) { total += n; } visits every element without needing an index."),
                    new("int[] nums = { 4, 8, 15, 16, 23 };\nint total = 0;\nforeach (int n in nums)\n{\n    total += n;\n}\nConsole.WriteLine(total);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "66"
            ),
            new(
                Id: 4,
                Title: "Find the Champion",
                Description: "Given int[] nums = { 3, 9, 2, 7, 5 }, find and print the largest value using a for loop (no built-in Max method).",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "int[] nums = { 3, 9, 2, 7, 5 };\nint max = nums[0];\n// TODO: loop from index 1 to the end, updating max whenever you find a bigger value\n// Print max\n",
                Example: "int[] nums = { 3, 9, 2, 7, 5 };\nint max = nums[0];\nfor (int i = 1; i < nums.Length; i++)\n{\n    if (nums[i] > max) max = nums[i];\n}\nConsole.WriteLine(max);\n// Output: 9",
                Hints: new List<HintTier>
                {
                    new("Start max as nums[0], then compare every later element against it, replacing max when you find something bigger."),
                    new("int[] nums = { 3, 9, 2, 7, 5 };\nint max = nums[0];\nfor (int i = 1; i < nums.Length; i++)\n{\n    if (nums[i] > max) max = nums[i];\n}\nConsole.WriteLine(max);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "9"
            ),
            new(
                Id: 5,
                Title: "Building a List",
                Description: "Create a List<string> called names, Add \"Ada\", \"Grace\", and \"Alan\" to it, print names.Count, then print each name.",
                Difficulty: Difficulty.Easy,
                Points: 15,
                StarterCode: "// Create List<string> names = new List<string>();\n// Add \"Ada\", \"Grace\", \"Alan\"\n// Print names.Count, then each name\n",
                Example: "List<string> names = new List<string>();\nnames.Add(\"Ada\");\nnames.Add(\"Grace\");\nnames.Add(\"Alan\");\nConsole.WriteLine(names.Count);\nforeach (string name in names)\n{\n    Console.WriteLine(name);\n}",
                Hints: new List<HintTier>
                {
                    new("A List<T> grows with .Add(...) — unlike an array, you don't need to know the size up front."),
                    new("List<string> names = new List<string>();\nnames.Add(\"Ada\");\nnames.Add(\"Grace\");\nnames.Add(\"Alan\");\nConsole.WriteLine(names.Count);\nforeach (string name in names)\n{\n    Console.WriteLine(name);\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "3", "Ada", "Grace", "Alan")
            ),
            new(
                Id: 6,
                Title: "Trim the List",
                Description: "Given a List<string> fruits containing \"apple\", \"banana\", \"cherry\", remove \"banana\" and print what remains.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "List<string> fruits = new List<string> { \"apple\", \"banana\", \"cherry\" };\n// TODO: remove \"banana\", then print the remaining fruits\n",
                Example: "List<string> fruits = new List<string> { \"apple\", \"banana\", \"cherry\" };\nfruits.Remove(\"banana\");\nforeach (string f in fruits)\n{\n    Console.WriteLine(f);\n}",
                Hints: new List<HintTier>
                {
                    new("fruits.Remove(\"banana\") deletes the first matching value from the list."),
                    new("List<string> fruits = new List<string> { \"apple\", \"banana\", \"cherry\" };\nfruits.Remove(\"banana\");\nforeach (string f in fruits)\n{\n    Console.WriteLine(f);\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "apple", "cherry")
            ),
            new(
                Id: 7,
                Title: "Numbered Roster",
                Description: "Given string[] players = { \"Rex\", \"Milo\", \"Zoe\" }, use a for loop to print each one as \"1: Rex\", \"2: Milo\", \"3: Zoe\".",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "string[] players = { \"Rex\", \"Milo\", \"Zoe\" };\n// TODO: for loop using the index to print \"<number>: <name>\", starting at 1\n",
                Example: "string[] players = { \"Rex\", \"Milo\", \"Zoe\" };\nfor (int i = 0; i < players.Length; i++)\n{\n    Console.WriteLine((i + 1) + \": \" + players[i]);\n}",
                Hints: new List<HintTier>
                {
                    new("A for loop gives you the index i, which you can use both to read players[i] and to compute the display number i + 1."),
                    new("string[] players = { \"Rex\", \"Milo\", \"Zoe\" };\nfor (int i = 0; i < players.Length; i++)\n{\n    Console.WriteLine((i + 1) + \": \" + players[i]);\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "1: Rex", "2: Milo", "3: Zoe")
            ),
            new(
                Id: 8,
                Title: "Is It In There?",
                Description: "Given a List<int> ids containing 101, 102, 103, print whether the list Contains(102), then print the IndexOf(103).",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "List<int> ids = new List<int> { 101, 102, 103 };\n// TODO: print ids.Contains(102)\n// TODO: print ids.IndexOf(103)\n",
                Example: "List<int> ids = new List<int> { 101, 102, 103 };\nConsole.WriteLine(ids.Contains(102));\nConsole.WriteLine(ids.IndexOf(103));\n// Output:\n// True\n// 2",
                Hints: new List<HintTier>
                {
                    new("Contains returns a bool, and IndexOf returns the position (0-based) or -1 if not found — both can go straight into Console.WriteLine."),
                    new("List<int> ids = new List<int> { 101, 102, 103 };\nConsole.WriteLine(ids.Contains(102));\nConsole.WriteLine(ids.IndexOf(103));", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "True", "2")
            ),
            new(
                Id: 9,
                Title: "Reverse the Queue",
                Description: "Given int[] queue = { 1, 2, 3, 4 }, reverse it in place with Array.Reverse, then print every value.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "int[] queue = { 1, 2, 3, 4 };\n// TODO: reverse queue with Array.Reverse, then print each value\n",
                Example: "int[] queue = { 1, 2, 3, 4 };\nArray.Reverse(queue);\nforeach (int q in queue)\n{\n    Console.WriteLine(q);\n}",
                Hints: new List<HintTier>
                {
                    new("Array.Reverse(queue) is a static method that flips the array's contents in place — it doesn't return a new array."),
                    new("int[] queue = { 1, 2, 3, 4 };\nArray.Reverse(queue);\nforeach (int q in queue)\n{\n    Console.WriteLine(q);\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "4", "3", "2", "1")
            ),
            new(
                Id: 10,
                Title: "Sort the Scores",
                Description: "Given a List<int> scores containing 42, 17, 88, 5, sort it ascending with .Sort(), then print every value.",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode: "List<int> scores = new List<int> { 42, 17, 88, 5 };\n// TODO: sort scores ascending, then print each value\n",
                Example: "List<int> scores = new List<int> { 42, 17, 88, 5 };\nscores.Sort();\nforeach (int s in scores)\n{\n    Console.WriteLine(s);\n}",
                Hints: new List<HintTier>
                {
                    new("List<T> has a built-in .Sort() method that sorts ascending in place — no need to write your own sorting logic."),
                    new("List<int> scores = new List<int> { 42, 17, 88, 5 };\nscores.Sort();\nforeach (int s in scores)\n{\n    Console.WriteLine(s);\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "5", "17", "42", "88")
            ),
            new(
                Id: 11,
                Title: "🎮 Visual: Line Up the Squad",
                Description:
                    "Time to move things on screen using an array! Given int[] positions = { 40, 120, 200, 280 }, loop through " +
                    "the array and for each position, call Canvas.AddShape with a unique id (\"p\" + index), the type \"circle\", " +
                    "that x value, and a fixed y of 100. Finish by printing \"Lined up!\". Once it passes, hit Play Again to watch " +
                    "your whole squad snap into formation.",
                Difficulty: Difficulty.Hard,
                Points: 30,
                StarterCode:
                    "int[] positions = { 40, 120, 200, 280 };\n" +
                    "// TODO: loop over positions with an index i\n" +
                    "//   - Canvas.AddShape(\"p\" + i, \"circle\", positions[i], 100)\n" +
                    "// Then print \"Lined up!\"\n",
                Example:
                    "int[] positions = { 40, 120, 200, 280 };\n" +
                    "for (int i = 0; i < positions.Length; i++)\n" +
                    "{\n" +
                    "    Canvas.AddShape(\"p\" + i, \"circle\", positions[i], 100);\n" +
                    "}\n" +
                    "Console.WriteLine(\"Lined up!\");",
                Hints: new List<HintTier>
                {
                    new("Use a for loop (not foreach) so you have the index i handy to build a unique id like \"p\" + i."),
                    new("Canvas.AddShape takes (id, type, x, y) at minimum — call it once per array element inside your loop."),
                    new(
                        "int[] positions = { 40, 120, 200, 280 };\n" +
                        "for (int i = 0; i < positions.Length; i++)\n" +
                        "{\n" +
                        "    Canvas.AddShape(\"p\" + i, \"circle\", positions[i], 100);\n" +
                        "}\n" +
                        "Console.WriteLine(\"Lined up!\");",
                        IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Lined up!",
                CheckSource: source => source.Contains("Canvas.AddShape") && (source.Contains("for") || source.Contains("foreach"))
            ),
            new(
                Id: 3,
                Title: "🎮 Visual: Color the Team",
                Description:
                    "Given string[] colors = { \"#ef4444\", \"#22c55e\", \"#3b82f6\" }, loop through with a for loop: for each index i, " +
                    "call Canvas.AddShape(\"p\" + i, \"circle\", i * 80, 100, 40, colors[i]) to draw a differently-colored teammate. " +
                    "Print \"Team ready!\" after the loop.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// string[] colors = { \"#ef4444\", \"#22c55e\", \"#3b82f6\" }\n// for loop over colors, Canvas.AddShape(\"p\" + i, \"circle\", i * 80, 100, 40, colors[i])\n// Print \"Team ready!\"\n",
                Example: "string[] colors = { \"#ef4444\", \"#22c55e\", \"#3b82f6\" };\nfor (int i = 0; i < colors.Length; i++)\n{\n    Canvas.AddShape(\"p\" + i, \"circle\", i * 80, 100, 40, colors[i]);\n}\nConsole.WriteLine(\"Team ready!\");",
                Hints: new List<HintTier>
                {
                    new("Loop by index (0 to colors.Length - 1) so you can use the same i for both the shape's x position and its color from the array."),
                    new("string[] colors = { \"#ef4444\", \"#22c55e\", \"#3b82f6\" };\nfor (int i = 0; i < colors.Length; i++)\n{\n    Canvas.AddShape(\"p\" + i, \"circle\", i * 80, 100, 40, colors[i]);\n}\nConsole.WriteLine(\"Team ready!\");", IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Team ready!",
                CheckSource: source => source.Contains("Canvas.AddShape") && source.Contains("for")
            )
        },
        QuizStages: new List<QuizStage>
        {
            new(
                Title: "Arrays Check-In",
                AfterTaskId: 6,
                QuestionsPerAttempt: 5,
                Cards: new List<QuizCard>
                {
                    new("What does `names.Length` give you for an array?", new List<string> { "The last index", "The number of elements", "The first element", "Whether it's empty" }, CorrectIndex: 1),
                    new("What's the index of the first element in an array?", new List<string> { "1", "0", "-1", "It depends" }, CorrectIndex: 1),
                    new("Which type can grow or shrink after creation, unlike a plain array?", new List<string> { "int[]", "List<T>", "string[]", "double[]" }, CorrectIndex: 1),
                    new("What method adds an item to the end of a List<T>?", new List<string> { "Add", "Push", "Append", "Insert" }, CorrectIndex: 0),
                    new("Which method removes an item from a List<T> by value?", new List<string> { "Delete", "Remove", "Pop", "Cut" }, CorrectIndex: 1),
                    new("What does `list.Contains(x)` return?", new List<string> { "The index of x", "A copy of the list", "true or false", "The value x" }, CorrectIndex: 2),
                    new("What does looping with `foreach (var n in numbers)` give you access to each time?", new List<string> { "The index only", "Each element's value", "The whole array", "Nothing useful" }, CorrectIndex: 1)
                }
            ),
            new(
                Title: "Arrays Mastery",
                AfterTaskId: 11,
                Format: QuizFormat.SwipeTrueFalse,
                QuestionsPerAttempt: 10,
                Cards: new List<QuizCard>
                {
                    new("Arrays in C# are zero-indexed.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`List<T>` has a fixed size that can never change.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("You can use a `for` loop to walk through every element of an array by index.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`Array.Sort()` rearranges an array's elements in place.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`Array.Reverse()` flips the order of the elements.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`list.Contains(x)` tells you whether x exists in the list.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Accessing an index beyond an array's length is always safe and returns 0.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("`foreach` lets you change the index you're currently on manually.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("You can sum all the numbers in an array using a loop and a running total.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`List<T>.Add()` puts a new item at the end of the list.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("An array's `Length` and a List's `Count` mean the same thing conceptually.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("You need a separate variable for every item instead of using an array or list.", new List<string> { "True", "False" }, CorrectIndex: 1)
                }
            )
        }
    );
}
