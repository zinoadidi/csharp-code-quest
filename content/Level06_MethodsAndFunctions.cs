namespace app.Content;

/// <summary>
/// Level 6 — Methods &amp; Functions.
///
/// Defining methods, parameters, return values, and overloading. Closes with a
/// visual task where a method is the thing driving the animation — writing
/// Canvas calls inside a loop inside a function, not just at the top level.
/// </summary>
public static class Level06_MethodsAndFunctions
{
    public static readonly Level Instance = new(
        Id: 6,
        Name: "Methods & Functions",
        Description: "Package up logic you can call by name: parameters, return values, and overloads.",
        Tasks: new List<GameTask>
        {
            GameTask.FlashcardDeck(
                new GlossaryTerm("Method", "A named, reusable block of steps you can run whenever you need it — also called a function — instead of writing the same code over and over."),
                new GlossaryTerm("Parameter", "A named placeholder a method declares for a value it expects to receive — like a blank on a form waiting to be filled in."),
                new GlossaryTerm("Argument", "The actual value you hand a method when you call it — it slots into that method's parameter."),
                new GlossaryTerm("Return Value", "The result a method hands back to whoever called it, using the return keyword — the method's answer to the question you asked it."),
                new GlossaryTerm("Void", "A method's return type when it doesn't hand anything back — it just does something (like printing) rather than computing an answer."),
                new GlossaryTerm("Call (Invoke)", "Actually running a method by writing its name followed by parentheses — defining a method doesn't run it; calling it does."),
                new GlossaryTerm("Overload", "Having multiple methods with the SAME name but different parameters — the computer picks the right one based on what you pass in."),
                new GlossaryTerm("Scope", "Where in your code a variable is visible and usable — a variable created inside a method generally can't be seen outside of it."),
                new GlossaryTerm("Local Variable", "A variable declared inside a method — it only exists while that method is running, then disappears."),
                new GlossaryTerm("Recursion", "A method that calls itself to solve a smaller version of the same problem, until it reaches a simple case it can answer directly.")),
            new(
                Id: 1,
                Title: "Say Hello",
                Description: "Define a method Greet(string name) that prints \"Hello, <name>!\", then call it with \"Ada\".",
                Difficulty: Difficulty.Easy,
                Points: 15,
                StarterCode: "// Define: void Greet(string name) { ... }\n// It should print \"Hello, \" + name + \"!\"\n// Call Greet(\"Ada\")\n",
                Example: "void Greet(string name)\n{\n    Console.WriteLine(\"Hello, \" + name + \"!\");\n}\nGreet(\"Ada\");",
                Hints: new List<HintTier>
                {
                    new("A method needs a return type (void, since it doesn't return anything), a name, and parameters in parentheses."),
                    new("void Greet(string name)\n{\n    Console.WriteLine(\"Hello, \" + name + \"!\");\n}\nGreet(\"Ada\");", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Hello, Ada!"
            ),
            new(
                Id: 2,
                Title: "Return Something",
                Description: "Define int Square(int n) that returns n * n, then print Square(6).",
                Difficulty: Difficulty.Easy,
                Points: 15,
                StarterCode: "// Define: int Square(int n) { return n * n; }\n// Print Square(6)\n",
                Example: "int Square(int n)\n{\n    return n * n;\n}\nConsole.WriteLine(Square(6));\n// Output: 36",
                Hints: new List<HintTier>
                {
                    new("A method that returns a value uses that value's type (int here) instead of void, and needs a return statement."),
                    new("int Square(int n)\n{\n    return n * n;\n}\nConsole.WriteLine(Square(6));", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "36"
            ),
            new(
                Id: 4,
                Title: "Two Parameters",
                Description: "Define int Add(int a, int b) that returns their sum, then print Add(4, 9).",
                Difficulty: Difficulty.Easy,
                Points: 15,
                StarterCode: "// Define: int Add(int a, int b) { return a + b; }\n// Print Add(4, 9)\n",
                Example: "int Add(int a, int b)\n{\n    return a + b;\n}\nConsole.WriteLine(Add(4, 9));\n// Output: 13",
                Hints: new List<HintTier>
                {
                    new("Separate multiple parameters with commas: (int a, int b)."),
                    new("int Add(int a, int b)\n{\n    return a + b;\n}\nConsole.WriteLine(Add(4, 9));", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "13"
            ),
            new(
                Id: 5,
                Title: "Reusable Check",
                Description: "Define bool IsEven(int n) that returns true if n is divisible by 2, then print IsEven(4) and IsEven(7).",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// Define: bool IsEven(int n) { return n % 2 == 0; }\n// Print IsEven(4), then IsEven(7)\n",
                Example: "bool IsEven(int n)\n{\n    return n % 2 == 0;\n}\nConsole.WriteLine(IsEven(4));\nConsole.WriteLine(IsEven(7));",
                Hints: new List<HintTier>
                {
                    new("A comparison like n % 2 == 0 already IS a bool value — you can return it directly."),
                    new("bool IsEven(int n)\n{\n    return n % 2 == 0;\n}\nConsole.WriteLine(IsEven(4));\nConsole.WriteLine(IsEven(7));", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "True", "False")
            ),
            new(
                Id: 6,
                Title: "Same Name, Different Job",
                Description: "Define two methods both named Combine: one Combine(int a, int b) that adds them, one Combine(string a, string b) that concatenates them. Print Combine(3, 4) then Combine(\"Hello, \", \"World!\").",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// Define int Combine(int a, int b) => a + b;\n// Define string Combine(string a, string b) => a + b;\n// Print Combine(3, 4)\n// Print Combine(\"Hello, \", \"World!\")\n",
                Example: "int Combine(int a, int b) => a + b;\nstring Combine(string a, string b) => a + b;\nConsole.WriteLine(Combine(3, 4));\nConsole.WriteLine(Combine(\"Hello, \", \"World!\"));",
                Hints: new List<HintTier>
                {
                    new("This is called overloading — two methods can share a name as long as their parameter types are different. C# picks the right one based on what you pass in."),
                    new("int Combine(int a, int b) => a + b;\nstring Combine(string a, string b) => a + b;\nConsole.WriteLine(Combine(3, 4));\nConsole.WriteLine(Combine(\"Hello, \", \"World!\"));", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "7", "Hello, World!")
            ),
            new(
                Id: 7,
                Title: "Loop Inside a Method",
                Description: "Define void PrintCountdown(int from) that prints every number from 'from' down to 1, then call PrintCountdown(4).",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// Define: void PrintCountdown(int from) { ... }\n// Use a for loop inside it, from `from` down to 1\n// Call PrintCountdown(4)\n",
                Example: "void PrintCountdown(int from)\n{\n    for (int i = from; i >= 1; i--)\n    {\n        Console.WriteLine(i);\n    }\n}\nPrintCountdown(4);",
                Hints: new List<HintTier>
                {
                    new("A method's body can contain anything a normal script can — including loops. Start the loop at `from` and count down."),
                    new("void PrintCountdown(int from)\n{\n    for (int i = from; i >= 1; i--)\n    {\n        Console.WriteLine(i);\n    }\n}\nPrintCountdown(4);", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "4", "3", "2", "1")
            ),
            new(
                Id: 8,
                Title: "Array In, Number Out",
                Description: "Define int Sum(int[] arr) that adds up every element and returns the total. Call it with int[] nums = { 5, 10, 15 } and print the result.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "int[] nums = { 5, 10, 15 };\n// Define int Sum(int[] arr) { ... } using a foreach loop\n// Print Sum(nums)\n",
                Example: "int Sum(int[] arr)\n{\n    int total = 0;\n    foreach (int n in arr)\n    {\n        total += n;\n    }\n    return total;\n}\nint[] nums = { 5, 10, 15 };\nConsole.WriteLine(Sum(nums));\n// Output: 30",
                Hints: new List<HintTier>
                {
                    new("An array can be passed as a parameter just like any other type — arr behaves exactly like the array you passed in."),
                    new("int Sum(int[] arr)\n{\n    int total = 0;\n    foreach (int n in arr)\n    {\n        total += n;\n    }\n    return total;\n}\nint[] nums = { 5, 10, 15 };\nConsole.WriteLine(Sum(nums));", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "30"
            ),
            new(
                Id: 9,
                Title: "Build a Repeated String",
                Description: "Define string Repeat(string s, int times) that returns s repeated that many times, then print Repeat(\"ha\", 3).",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// Define string Repeat(string s, int times) { ... } using a for loop and string concatenation\n// Print Repeat(\"ha\", 3)\n",
                Example: "string Repeat(string s, int times)\n{\n    string result = \"\";\n    for (int i = 0; i < times; i++)\n    {\n        result += s;\n    }\n    return result;\n}\nConsole.WriteLine(Repeat(\"ha\", 3));\n// Output: hahaha",
                Hints: new List<HintTier>
                {
                    new("Start with an empty string and use += inside a loop that runs `times` times."),
                    new("string Repeat(string s, int times)\n{\n    string result = \"\";\n    for (int i = 0; i < times; i++)\n    {\n        result += s;\n    }\n    return result;\n}\nConsole.WriteLine(Repeat(\"ha\", 3));", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "hahaha"
            ),
            new(
                Id: 10,
                Title: "A Method That Calls Itself",
                Description: "Define int Factorial(int n) that returns n! (n times every positive integer below it) by having it call itself with a smaller n. Print Factorial(5).",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode: "// Define int Factorial(int n):\n//   - if n <= 1, return 1\n//   - otherwise, return n * Factorial(n - 1)\n// Print Factorial(5)\n",
                Example: "int Factorial(int n)\n{\n    if (n <= 1) return 1;\n    return n * Factorial(n - 1);\n}\nConsole.WriteLine(Factorial(5));\n// Output: 120",
                Hints: new List<HintTier>
                {
                    new("This is called recursion — a method calling itself. It always needs a base case (n <= 1) that stops the calls, or it never ends."),
                    new("Factorial(5) is 5 * Factorial(4), which is 5 * 4 * Factorial(3), and so on down to Factorial(1) = 1."),
                    new("int Factorial(int n)\n{\n    if (n <= 1) return 1;\n    return n * Factorial(n - 1);\n}\nConsole.WriteLine(Factorial(5));", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "120"
            ),
            new(
                Id: 11,
                Title: "🎮 Visual: Gravity Drop",
                Description:
                    "Define void Drop(string id) that first calls Canvas.AddShape(id, \"circle\", 150, 20), then loops 5 times " +
                    "calling Canvas.MoveBy(id, 0, 30) each time (that's the shape falling, one step per loop). After defining " +
                    "it, call Drop(\"ball\") and print \"Splash!\". Play it back afterward to watch it fall.",
                Difficulty: Difficulty.Hard,
                Points: 30,
                StarterCode:
                    "// Define void Drop(string id):\n" +
                    "//   - Canvas.AddShape(id, \"circle\", 150, 20)\n" +
                    "//   - loop 5 times, each time calling Canvas.MoveBy(id, 0, 30)\n" +
                    "// Call Drop(\"ball\")\n" +
                    "// Print \"Splash!\"\n",
                Example:
                    "void Drop(string id)\n" +
                    "{\n" +
                    "    Canvas.AddShape(id, \"circle\", 150, 20);\n" +
                    "    for (int i = 0; i < 5; i++)\n" +
                    "    {\n" +
                    "        Canvas.MoveBy(id, 0, 30);\n" +
                    "    }\n" +
                    "}\n" +
                    "Drop(\"ball\");\n" +
                    "Console.WriteLine(\"Splash!\");",
                Hints: new List<HintTier>
                {
                    new("A method's body can hold the shape's setup AND a loop that animates it — the caller just needs one line: Drop(\"ball\")."),
                    new("Add the shape once, before the loop. Inside the loop, only move it — don't add it again."),
                    new(
                        "void Drop(string id)\n" +
                        "{\n" +
                        "    Canvas.AddShape(id, \"circle\", 150, 20);\n" +
                        "    for (int i = 0; i < 5; i++)\n" +
                        "    {\n" +
                        "        Canvas.MoveBy(id, 0, 30);\n" +
                        "    }\n" +
                        "}\n" +
                        "Drop(\"ball\");\n" +
                        "Console.WriteLine(\"Splash!\");",
                        IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Splash!",
                CheckSource: source => source.Contains("Canvas.AddShape") && source.Contains("Canvas.MoveBy")
            ),
            new(
                Id: 3,
                Title: "🎮 Visual: Spawn Method",
                Description:
                    "Define void Spawn(string id, double x, double y) that calls Canvas.AddShape(id, \"star\", x, y). " +
                    "Call it three times with \"s1\" at (40, 60), \"s2\" at (120, 60), and \"s3\" at (200, 60). " +
                    "Print \"Spawned 3 stars.\"",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// Define void Spawn(string id, double x, double y):\n//   Canvas.AddShape(id, \"star\", x, y)\n// Call Spawn 3 times, then print \"Spawned 3 stars.\"\n",
                Example: "void Spawn(string id, double x, double y)\n{\n    Canvas.AddShape(id, \"star\", x, y);\n}\nSpawn(\"s1\", 40, 60);\nSpawn(\"s2\", 120, 60);\nSpawn(\"s3\", 200, 60);\nConsole.WriteLine(\"Spawned 3 stars.\");",
                Hints: new List<HintTier>
                {
                    new("Write Spawn once, then call it three times with different arguments instead of repeating the Canvas.AddShape line."),
                    new("void Spawn(string id, double x, double y)\n{\n    Canvas.AddShape(id, \"star\", x, y);\n}\nSpawn(\"s1\", 40, 60);\nSpawn(\"s2\", 120, 60);\nSpawn(\"s3\", 200, 60);\nConsole.WriteLine(\"Spawned 3 stars.\");", IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Spawned 3 stars.",
                CheckSource: source => source.Contains("Canvas.AddShape") && source.Contains("void Spawn")
            )
        },
        QuizStages: new List<QuizStage>
        {
            new(
                Title: "Methods Check-In",
                AfterTaskId: 6,
                QuestionsPerAttempt: 5,
                Cards: new List<QuizCard>
                {
                    new("What keyword do you use for a method that doesn't return a value?", new List<string> { "null", "void", "empty", "none" }, CorrectIndex: 1),
                    new("How do you send a value back out of a method?", new List<string> { "print", "return", "output", "yield" }, CorrectIndex: 1),
                    new("What are the values passed into a method called?", new List<string> { "Returns", "Parameters", "Variables", "Fields" }, CorrectIndex: 1),
                    new("Can two methods share the same name if they take different parameters?", new List<string> { "No, never", "Yes, that's called overloading", "Only if one is void", "Only in the same class name" }, CorrectIndex: 1),
                    new("What is it called when a method calls itself?", new List<string> { "Looping", "Overloading", "Recursion", "Nesting" }, CorrectIndex: 2),
                    new("If a method is declared `int Sum(int a, int b)`, what must it do?", new List<string> { "Print a and b", "Return an int", "Return nothing", "Take no parameters" }, CorrectIndex: 1),
                    new("Why break code into methods instead of one long block?", new List<string> { "It runs faster", "It's reusable and easier to read", "It uses less memory", "C# requires it" }, CorrectIndex: 1)
                }
            ),
            new(
                Title: "Methods Mastery",
                AfterTaskId: 11,
                Format: QuizFormat.SwipeTrueFalse,
                QuestionsPerAttempt: 10,
                Cards: new List<QuizCard>
                {
                    new("A `void` method can still use `return;` on its own to exit early.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A method that returns `int` must return an int on every code path.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Method overloading means having two methods with the same name but different parameters.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A method can take an array as a parameter.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Recursion means a method that never finishes running.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("Every recursive method needs a base case to stop calling itself.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("You can call one method from inside another method.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A method's parameters are only visible inside that method's body.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Methods can contain loops and if statements just like Main can.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A `void` method can use `return` followed by a value.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("You must always pass the exact number of parameters a method declares (unless it has defaults or overloads).", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Copy-pasting the same block of code five times is better than writing one reusable method.", new List<string> { "True", "False" }, CorrectIndex: 1)
                }
            )
        }
    );
}
