namespace app.Content;

/// <summary>
/// Level 11 — Error Handling &amp; Robustness.
///
/// try/catch/finally, throwing exceptions, nullable types and the null-coalescing
/// operator. Closes with a fleet of robots that stop moving once their fuel runs
/// out, each move wrapped in try/catch so one failing robot doesn't stop the rest.
/// </summary>
public static class Level11_ErrorHandling
{
    public static readonly Level Instance = new(
        Id: 11,
        Name: "Error Handling & Robustness",
        Description: "Let your programs fail gracefully instead of crashing, and represent \"nothing here\" without lying about it.",
        Tasks: new List<GameTask>
        {
            GameTask.FlashcardDeck(
                new GlossaryTerm("Exception", "An object representing something that went wrong while a program was running — like trying to divide by zero, or open a file that doesn't exist."),
                new GlossaryTerm("Try/Catch", "A block of code you \"try\" to run, paired with a \"catch\" block that handles it gracefully if an exception happens instead of crashing the whole program."),
                new GlossaryTerm("Throw", "The keyword that deliberately raises an exception — either one the runtime detected on its own, or one you create yourself for a situation your own code considers invalid."),
                new GlossaryTerm("Finally", "A block that runs no matter what — whether the try block succeeded, failed, or threw an exception — perfect for cleanup that must always happen."),
                new GlossaryTerm("Stack Trace", "The list of methods that were running, in order, at the exact moment an exception was thrown — the first thing you read when debugging a crash."),
                new GlossaryTerm("Null Reference", "An error from trying to use a variable that doesn't actually point to any object yet — one of the most common bugs in any object-oriented language."),
                new GlossaryTerm("Validation", "Checking that data is actually usable BEFORE you try to use it — like confirming a number isn't negative before dividing by it."),
                new GlossaryTerm("Custom Exception", "Your own exception type, built by inheriting from Exception, for errors specific to your own program's rules rather than the built-in BCL ones."),
                new GlossaryTerm("Graceful Failure", "When something goes wrong but the program handles it cleanly — a friendly error message — instead of crashing outright."),
                new GlossaryTerm("Edge Case", "An unusual or extreme input that's easy to forget while writing the \"normal\" path — like an empty list, a zero, or the very first/last item.")),
            new(
                Id: 1,
                Title: "Catch the Crash",
                Description: "Create an int[3] array and try to print index 5. Wrap it in a try/catch for IndexOutOfRangeException, printing \"Caught an out-of-range error!\" instead of crashing.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "try\n{\n    int[] arr = new int[3];\n    Console.WriteLine(arr[5]);\n}\ncatch (IndexOutOfRangeException)\n{\n    // print \"Caught an out-of-range error!\"\n}\n",
                Example: "try\n{\n    int[] arr = new int[3];\n    Console.WriteLine(arr[5]);\n}\ncatch (IndexOutOfRangeException)\n{\n    Console.WriteLine(\"Caught an out-of-range error!\");\n}",
                Hints: new List<HintTier>
                {
                    new("Code that might throw goes in the `try` block; the matching `catch` block runs instead of letting the program crash."),
                    new("try\n{\n    int[] arr = new int[3];\n    Console.WriteLine(arr[5]);\n}\ncatch (IndexOutOfRangeException)\n{\n    Console.WriteLine(\"Caught an out-of-range error!\");\n}", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Caught an out-of-range error!"
            ),
            new(
                Id: 2,
                Title: "Read the Message",
                Description: "Try dividing 10 by 0 and printing the result. Catch DivideByZeroException as ex, printing \"Error: <ex.Message>\".",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "try\n{\n    int x = 10;\n    int y = 0;\n    Console.WriteLine(x / y);\n}\ncatch (DivideByZeroException ex)\n{\n    // print $\"Error: {ex.Message}\"\n}\n",
                Example: "try\n{\n    int x = 10;\n    int y = 0;\n    Console.WriteLine(x / y);\n}\ncatch (DivideByZeroException ex)\n{\n    Console.WriteLine($\"Error: {ex.Message}\");\n}\n// Output: Error: Attempted to divide by zero.",
                Hints: new List<HintTier>
                {
                    new("`catch (ExceptionType ex)` captures the exception object itself — `ex.Message` holds a human-readable description of what went wrong."),
                    new("try\n{\n    int x = 10;\n    int y = 0;\n    Console.WriteLine(x / y);\n}\ncatch (DivideByZeroException ex)\n{\n    Console.WriteLine($\"Error: {ex.Message}\");\n}", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Error: Attempted to divide by zero."
            ),
            new(
                Id: 4,
                Title: "Always Runs",
                Description: "Print \"Trying...\", then throw new Exception(\"Something broke\"). Catch it printing \"Caught: <message>\", and use a finally block to print \"Cleanup done.\" no matter what.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "try\n{\n    Console.WriteLine(\"Trying...\");\n    throw new Exception(\"Something broke\");\n}\ncatch (Exception ex)\n{\n    // print $\"Caught: {ex.Message}\"\n}\nfinally\n{\n    // print \"Cleanup done.\"\n}\n",
                Example: "try\n{\n    Console.WriteLine(\"Trying...\");\n    throw new Exception(\"Something broke\");\n}\ncatch (Exception ex)\n{\n    Console.WriteLine($\"Caught: {ex.Message}\");\n}\nfinally\n{\n    Console.WriteLine(\"Cleanup done.\");\n}",
                Hints: new List<HintTier>
                {
                    new("`throw new Exception(\"message\")` raises an error yourself. A `finally` block always runs after try/catch, whether an exception happened or not — great for cleanup."),
                    new("try\n{\n    Console.WriteLine(\"Trying...\");\n    throw new Exception(\"Something broke\");\n}\ncatch (Exception ex)\n{\n    Console.WriteLine($\"Caught: {ex.Message}\");\n}\nfinally\n{\n    Console.WriteLine(\"Cleanup done.\");\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "Trying...", "Caught: Something broke", "Cleanup done.")
            ),
            new(
                Id: 5,
                Title: "Fall Back Safely",
                Description: "Write ParseOrDefault(string text) that returns int.Parse(text), but returns -1 if a FormatException is thrown. Call it with \"42\" and \"oops\".",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "int ParseOrDefault(string text)\n{\n    try\n    {\n        return int.Parse(text);\n    }\n    catch (FormatException)\n    {\n        // return -1\n    }\n}\nConsole.WriteLine(ParseOrDefault(\"42\"));\nConsole.WriteLine(ParseOrDefault(\"oops\"));\n",
                Example: "int ParseOrDefault(string text)\n{\n    try\n    {\n        return int.Parse(text);\n    }\n    catch (FormatException)\n    {\n        return -1;\n    }\n}\nConsole.WriteLine(ParseOrDefault(\"42\"));\nConsole.WriteLine(ParseOrDefault(\"oops\"));",
                Hints: new List<HintTier>
                {
                    new("`int.Parse` throws a FormatException when the text isn't a valid number — catching it lets you return a safe fallback value instead of crashing."),
                    new("int ParseOrDefault(string text)\n{\n    try\n    {\n        return int.Parse(text);\n    }\n    catch (FormatException)\n    {\n        return -1;\n    }\n}\nConsole.WriteLine(ParseOrDefault(\"42\"));\nConsole.WriteLine(ParseOrDefault(\"oops\"));", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "42", "-1")
            ),
            new(
                Id: 6,
                Title: "Throw Your Own",
                Description: "Write Withdraw(int balance, int amount) that throws new InvalidOperationException(\"Insufficient funds\") if amount > balance, otherwise prints \"Withdrew <amount>\". Call Withdraw(100,50) then Withdraw(100,200) inside a try/catch that prints \"Blocked: <message>\" on failure.",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode: "void Withdraw(int balance, int amount)\n{\n    if (amount > balance)\n    {\n        // throw new InvalidOperationException(\"Insufficient funds\")\n    }\n    Console.WriteLine($\"Withdrew {amount}\");\n}\ntry\n{\n    Withdraw(100, 50);\n    Withdraw(100, 200);\n}\ncatch (InvalidOperationException ex)\n{\n    // print $\"Blocked: {ex.Message}\"\n}\n",
                Example: "void Withdraw(int balance, int amount)\n{\n    if (amount > balance)\n    {\n        throw new InvalidOperationException(\"Insufficient funds\");\n    }\n    Console.WriteLine($\"Withdrew {amount}\");\n}\ntry\n{\n    Withdraw(100, 50);\n    Withdraw(100, 200);\n}\ncatch (InvalidOperationException ex)\n{\n    Console.WriteLine($\"Blocked: {ex.Message}\");\n}",
                Hints: new List<HintTier>
                {
                    new("Throwing inside Withdraw immediately stops that call and jumps to the nearest matching catch — the second Withdraw call never gets to its Console.WriteLine."),
                    new("void Withdraw(int balance, int amount)\n{\n    if (amount > balance)\n    {\n        throw new InvalidOperationException(\"Insufficient funds\");\n    }\n    Console.WriteLine($\"Withdrew {amount}\");\n}\ntry\n{\n    Withdraw(100, 50);\n    Withdraw(100, 200);\n}\ncatch (InvalidOperationException ex)\n{\n    Console.WriteLine($\"Blocked: {ex.Message}\");\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "Withdrew 50", "Blocked: Insufficient funds")
            ),
            new(
                Id: 7,
                Title: "Maybe a Value",
                Description: "Declare int? maybeAge = null. Print \"No age set\" if it has no value, otherwise print it. Then set maybeAge = 25 and print its Value.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "int? maybeAge = null;\n// If maybeAge.HasValue print maybeAge.Value, else print \"No age set\"\n// Then set maybeAge = 25 and print maybeAge.Value\n",
                Example: "int? maybeAge = null;\nif (maybeAge.HasValue)\n{\n    Console.WriteLine(maybeAge.Value);\n}\nelse\n{\n    Console.WriteLine(\"No age set\");\n}\nmaybeAge = 25;\nConsole.WriteLine(maybeAge.Value);",
                Hints: new List<HintTier>
                {
                    new("`int?` is a nullable int — it can hold either a number or null. `.HasValue` tells you which, and `.Value` reads the number out."),
                    new("int? maybeAge = null;\nif (maybeAge.HasValue)\n{\n    Console.WriteLine(maybeAge.Value);\n}\nelse\n{\n    Console.WriteLine(\"No age set\");\n}\nmaybeAge = 25;\nConsole.WriteLine(maybeAge.Value);", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "No age set", "25")
            ),
            new(
                Id: 8,
                Title: "Fall Back With ??",
                Description: "Given int? a = null, print a ?? -1. Given int? c = 7, print c ?? -1.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "int? a = null;\n// Print a ?? -1\nint? c = 7;\n// Print c ?? -1\n",
                Example: "int? a = null;\nint b = a ?? -1;\nConsole.WriteLine(b);\nint? c = 7;\nint d = c ?? -1;\nConsole.WriteLine(d);",
                Hints: new List<HintTier>
                {
                    new("`??` is the null-coalescing operator: `x ?? fallback` gives you x's value if it's not null, otherwise the fallback — shorter than an if/else."),
                    new("int? a = null;\nint b = a ?? -1;\nConsole.WriteLine(b);\nint? c = 7;\nint d = c ?? -1;\nConsole.WriteLine(d);", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "-1", "7")
            ),
            new(
                Id: 9,
                Title: "Validate the Input",
                Description: "Write ValidateAge(int age) returning \"Invalid age\" if age < 0 or age > 130, otherwise \"Valid age\". Call it with 25, -5, and 200.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "string ValidateAge(int age)\n{\n    if (age < 0 || age > 130)\n    {\n        // return \"Invalid age\"\n    }\n    return \"Valid age\";\n}\nConsole.WriteLine(ValidateAge(25));\nConsole.WriteLine(ValidateAge(-5));\nConsole.WriteLine(ValidateAge(200));\n",
                Example: "string ValidateAge(int age)\n{\n    if (age < 0 || age > 130)\n    {\n        return \"Invalid age\";\n    }\n    return \"Valid age\";\n}\nConsole.WriteLine(ValidateAge(25));\nConsole.WriteLine(ValidateAge(-5));\nConsole.WriteLine(ValidateAge(200));",
                Hints: new List<HintTier>
                {
                    new("Not every invalid state needs an exception — sometimes a plain validation function returning a result string is simpler and clearer."),
                    new("string ValidateAge(int age)\n{\n    if (age < 0 || age > 130)\n    {\n        return \"Invalid age\";\n    }\n    return \"Valid age\";\n}\nConsole.WriteLine(ValidateAge(25));\nConsole.WriteLine(ValidateAge(-5));\nConsole.WriteLine(ValidateAge(200));", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "Valid age", "Invalid age", "Invalid age")
            ),
            new(
                Id: 10,
                Title: "Wrap and Rethrow",
                Description: "Inside a try, access index 10 of a 3-element array (throws IndexOutOfRangeException). Catch that and throw new ApplicationException(\"Wrapped index error\") instead. Catch the ApplicationException at an outer level and print its message.",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode: "try\n{\n    try\n    {\n        int[] nums = { 1, 2, 3 };\n        Console.WriteLine(nums[10]);\n    }\n    catch (IndexOutOfRangeException)\n    {\n        // throw new ApplicationException(\"Wrapped index error\")\n    }\n}\ncatch (ApplicationException ex)\n{\n    // print ex.Message\n}\n",
                Example: "try\n{\n    try\n    {\n        int[] nums = { 1, 2, 3 };\n        Console.WriteLine(nums[10]);\n    }\n    catch (IndexOutOfRangeException)\n    {\n        throw new ApplicationException(\"Wrapped index error\");\n    }\n}\ncatch (ApplicationException ex)\n{\n    Console.WriteLine(ex.Message);\n}",
                Hints: new List<HintTier>
                {
                    new("Catch blocks can throw a different, more meaningful exception instead of just swallowing the original — the outer try/catch then handles that new one."),
                    new("try\n{\n    try\n    {\n        int[] nums = { 1, 2, 3 };\n        Console.WriteLine(nums[10]);\n    }\n    catch (IndexOutOfRangeException)\n    {\n        throw new ApplicationException(\"Wrapped index error\");\n    }\n}\ncatch (ApplicationException ex)\n{\n    Console.WriteLine(ex.Message);\n}", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Wrapped index error"
            ),
            new(
                Id: 11,
                Title: "🎮 Visual: Robot Fuel Run",
                Description:
                    "Define class Robot11 with Id, Fuel, a constructor that calls Canvas.AddShape, and a Move() method that " +
                    "throws InvalidOperationException(\"<Id> is out of fuel!\") if Fuel <= 0, otherwise subtracts 10 from Fuel " +
                    "and calls Canvas.MoveBy(Id, 40, 0). Put a Robot11(\"r1\", 25) and Robot11(\"r2\", 5) in a list. For each " +
                    "robot, try calling Move() three times inside one try/catch — on success count it as succeeded, on " +
                    "InvalidOperationException count it as failed. Print \"Succeeded: <n>, Failed: <n>\".",
                Difficulty: Difficulty.Hard,
                Points: 35,
                StarterCode:
                    "class Robot11\n" +
                    "{\n" +
                    "    public string Id;\n" +
                    "    public int Fuel;\n" +
                    "    public Robot11(string id, int fuel)\n" +
                    "    {\n" +
                    "        Id = id; Fuel = fuel;\n" +
                    "        Canvas.AddShape(Id, \"circle\", 20, 100);\n" +
                    "    }\n" +
                    "    public void Move()\n" +
                    "    {\n" +
                    "        if (Fuel <= 0)\n" +
                    "        {\n" +
                    "            throw new InvalidOperationException($\"{Id} is out of fuel!\");\n" +
                    "        }\n" +
                    "        Fuel -= 10;\n" +
                    "        Canvas.MoveBy(Id, 40, 0);\n" +
                    "    }\n" +
                    "}\n" +
                    "List<Robot11> robots = new List<Robot11> { new Robot11(\"r1\", 25), new Robot11(\"r2\", 5) };\n" +
                    "int successCount = 0;\n" +
                    "int failCount = 0;\n" +
                    "// For each robot, try calling Move() 3 times in a loop inside one try/catch\n" +
                    "// On success, successCount++; on InvalidOperationException, failCount++\n" +
                    "// Print $\"Succeeded: {successCount}, Failed: {failCount}\"\n",
                Example:
                    "class Robot11\n" +
                    "{\n" +
                    "    public string Id;\n" +
                    "    public int Fuel;\n" +
                    "    public Robot11(string id, int fuel)\n" +
                    "    {\n" +
                    "        Id = id; Fuel = fuel;\n" +
                    "        Canvas.AddShape(Id, \"circle\", 20, 100);\n" +
                    "    }\n" +
                    "    public void Move()\n" +
                    "    {\n" +
                    "        if (Fuel <= 0)\n" +
                    "        {\n" +
                    "            throw new InvalidOperationException($\"{Id} is out of fuel!\");\n" +
                    "        }\n" +
                    "        Fuel -= 10;\n" +
                    "        Canvas.MoveBy(Id, 40, 0);\n" +
                    "    }\n" +
                    "}\n" +
                    "List<Robot11> robots = new List<Robot11> { new Robot11(\"r1\", 25), new Robot11(\"r2\", 5) };\n" +
                    "int successCount = 0;\n" +
                    "int failCount = 0;\n" +
                    "foreach (Robot11 r in robots)\n" +
                    "{\n" +
                    "    try\n" +
                    "    {\n" +
                    "        for (int i = 0; i < 3; i++)\n" +
                    "        {\n" +
                    "            r.Move();\n" +
                    "        }\n" +
                    "        successCount++;\n" +
                    "    }\n" +
                    "    catch (InvalidOperationException)\n" +
                    "    {\n" +
                    "        failCount++;\n" +
                    "    }\n" +
                    "}\n" +
                    "Console.WriteLine($\"Succeeded: {successCount}, Failed: {failCount}\");",
                Hints: new List<HintTier>
                {
                    new("r1 starts with 25 fuel and only needs 30 to move 3 times, but the check happens BEFORE subtracting — trace through whether it ever hits Fuel <= 0 during those 3 calls."),
                    new("r2 starts with only 5 fuel — after one Move() its Fuel goes negative, so the second Move() call in its loop will throw."),
                    new(
                        "class Robot11\n" +
                        "{\n" +
                        "    public string Id;\n" +
                        "    public int Fuel;\n" +
                        "    public Robot11(string id, int fuel)\n" +
                        "    {\n" +
                        "        Id = id; Fuel = fuel;\n" +
                        "        Canvas.AddShape(Id, \"circle\", 20, 100);\n" +
                        "    }\n" +
                        "    public void Move()\n" +
                        "    {\n" +
                        "        if (Fuel <= 0)\n" +
                        "        {\n" +
                        "            throw new InvalidOperationException($\"{Id} is out of fuel!\");\n" +
                        "        }\n" +
                        "        Fuel -= 10;\n" +
                        "        Canvas.MoveBy(Id, 40, 0);\n" +
                        "    }\n" +
                        "}\n" +
                        "List<Robot11> robots = new List<Robot11> { new Robot11(\"r1\", 25), new Robot11(\"r2\", 5) };\n" +
                        "int successCount = 0;\n" +
                        "int failCount = 0;\n" +
                        "foreach (Robot11 r in robots)\n" +
                        "{\n" +
                        "    try\n" +
                        "    {\n" +
                        "        for (int i = 0; i < 3; i++)\n" +
                        "        {\n" +
                        "            r.Move();\n" +
                        "        }\n" +
                        "        successCount++;\n" +
                        "    }\n" +
                        "    catch (InvalidOperationException)\n" +
                        "    {\n" +
                        "        failCount++;\n" +
                        "    }\n" +
                        "}\n" +
                        "Console.WriteLine($\"Succeeded: {successCount}, Failed: {failCount}\");",
                        IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Succeeded: 1, Failed: 1",
                CheckSource: source => source.Contains("Canvas.MoveBy") && source.Contains("catch (InvalidOperationException") && source.Contains("class Robot11")
            ),
            new(
                Id: 3,
                Title: "🎮 Visual: Safe Landing",
                Description:
                    "Call Canvas.AddShape(\"pod\", \"circle\", 100, 20) then try to divide 100 by 0 int-wise inside a try/catch. " +
                    "In the catch (DivideByZeroException), call Canvas.SetColor(\"pod\", \"#ef4444\") and print \"Crash avoided!\". " +
                    "Otherwise (if it somehow doesn't throw) call Canvas.SetColor(\"pod\", \"#22c55e\") and print \"Landed safely.\"",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// Canvas.AddShape(\"pod\", \"circle\", 100, 20)\n// try { divide by 0 } catch (DivideByZeroException) { red + \"Crash avoided!\" }\n",
                Example: "Canvas.AddShape(\"pod\", \"circle\", 100, 20);\ntry\n{\n    int a = 100;\n    int b = 0;\n    Console.WriteLine(a / b);\n    Canvas.SetColor(\"pod\", \"#22c55e\");\n    Console.WriteLine(\"Landed safely.\");\n}\ncatch (DivideByZeroException)\n{\n    Canvas.SetColor(\"pod\", \"#ef4444\");\n    Console.WriteLine(\"Crash avoided!\");\n}",
                Hints: new List<HintTier>
                {
                    new("Dividing an int by 0 throws a DivideByZeroException — put the risky division inside the try, and the recovery inside the catch."),
                    new("Canvas.AddShape(\"pod\", \"circle\", 100, 20);\ntry\n{\n    int a = 100;\n    int b = 0;\n    Console.WriteLine(a / b);\n    Canvas.SetColor(\"pod\", \"#22c55e\");\n    Console.WriteLine(\"Landed safely.\");\n}\ncatch (DivideByZeroException)\n{\n    Canvas.SetColor(\"pod\", \"#ef4444\");\n    Console.WriteLine(\"Crash avoided!\");\n}", IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Crash avoided!",
                CheckSource: source => source.Contains("Canvas.AddShape") && source.Contains("catch (DivideByZeroException")
            )
        },
        QuizStages: new List<QuizStage>
        {
            new(
                Title: "Error Handling Check-In",
                AfterTaskId: 6,
                QuestionsPerAttempt: 5,
                Cards: new List<QuizCard>
                {
                    new("What block catches an exception thrown inside a try block?", new List<string> { "catch", "except", "rescue", "handle" }, CorrectIndex: 0),
                    new("What does a `finally` block guarantee?", new List<string> { "It only runs if an exception happens", "It always runs, exception or not", "It never runs", "It replaces the catch block" }, CorrectIndex: 1),
                    new("What does `ex.Message` give you inside a catch block?", new List<string> { "The line number", "A human-readable description of the error", "The stack trace only", "Nothing useful" }, CorrectIndex: 1),
                    new("What keyword lets you manually raise an exception?", new List<string> { "raise", "throw", "error", "fail" }, CorrectIndex: 1),
                    new("What is `int?` (a nullable int) used for?", new List<string> { "A faster int", "An int that can also be null (no value)", "An int that's always positive", "A read-only int" }, CorrectIndex: 1),
                    new("What does the `??` operator do, as in `value ?? fallback`?", new List<string> { "Compares two values", "Returns fallback if value is null", "Throws if value is null", "Converts value to a string" }, CorrectIndex: 1),
                    new("Why validate input before using it, rather than letting bad input crash the program?", new List<string> { "It's required by the compiler", "It lets you fail predictably with a clear message instead of crashing", "It makes code run faster", "It's optional and has no real benefit" }, CorrectIndex: 1)
                }
            ),
            new(
                Title: "Error Handling Mastery",
                AfterTaskId: 11,
                Format: QuizFormat.SwipeTrueFalse,
                QuestionsPerAttempt: 10,
                Cards: new List<QuizCard>
                {
                    new("A `try`/`catch` block lets a program recover from an error instead of crashing.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A `finally` block only runs when an exception is thrown.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("You can catch a specific exception type, like `catch (InvalidOperationException ex)`.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`throw new Exception(\"message\")` manually raises an error.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A nullable type like `int?` can hold either a number or null.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("The `??` operator provides a fallback value when the left side is null.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Once an exception is caught, the rest of the program cannot continue running.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("Wrapping an exception and rethrowing it can add extra context while preserving the original error.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Validating input before using it can prevent exceptions caused by bad data.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A single try block can have multiple catch blocks for different exception types.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Exceptions should be used for every normal, expected code path instead of if statements.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("`finally` is a good place to release resources (like closing a file) regardless of success or failure.", new List<string> { "True", "False" }, CorrectIndex: 0)
                }
            )
        }
    );
}
