namespace app.Content;

/// <summary>
/// Level 1 — Fundamentals &amp; First Wins.
///
/// Deliberately the smallest, fastest tasks in the game (see REQUIREMENTS.md,
/// "dopamine rush" requirement). Every task here is a one-or-two-line snippet a
/// beginner can solve in under a minute, with points and a hint safety net on
/// every single one. Ramp: bare output -> typed variables (int/string/bool/double)
/// -> combining variables with arithmetic and concatenation -> a first taste of
/// if/else, which sets up Level 2.
/// </summary>
public static class Level01_Fundamentals
{
    public static readonly Level Instance = new(
        Id: 1,
        Name: "Fundamentals & First Wins",
        Description: "Your first steps in C# — printing things, storing values, and making decisions.",
        Tasks: new List<GameTask>
        {
            GameTask.FlashcardDeck(
                new GlossaryTerm("Programming", "Giving a computer a list of exact steps to follow, so it does exactly what you want. That's it — that's the whole idea behind everything in this app."),
                new GlossaryTerm("Code", "The actual text you write that gives a computer its steps — like a recipe, except the computer follows it exactly, with zero guessing."),
                new GlossaryTerm("IDE", "Short for Integrated Development Environment — the app (like this one!) where you write, run, and check your code, all in one place."),
                new GlossaryTerm("Variable", "A named container that stores a value your program can use and change later — think of it as a labeled box you can peek into or refill anytime."),
                new GlossaryTerm("Data Type", "The kind of value a variable holds — like int for whole numbers, string for text, or bool for true/false — so the computer knows what it's allowed to do with it."),
                new GlossaryTerm("Syntax", "The exact spelling and punctuation rules of a language. Miss a semicolon or bracket and the code won't run — just like a typo can break a sentence."),
                new GlossaryTerm("Compile", "Turning the code you wrote into something the computer can actually run — like translating a recipe into a language the kitchen robot understands."),
                new GlossaryTerm("Console", "The plain text window where a program prints its output and, in some apps, reads what you type — the simplest way a program talks to you."),
                new GlossaryTerm("Comment", "A note you leave in your code for humans to read — the computer skips right over it. Great for explaining why, not just what."),
                new GlossaryTerm("Bug", "A mistake in your code that makes it do the wrong thing, or crash. Named after an actual moth once found stuck inside an early computer!")),
            new(
                Id: 1,
                Title: "Say Hello",
                Description: "Print the exact text \"Hello, C#!\" to the console.",
                Difficulty: Difficulty.Easy,
                Points: 10,
                StarterCode: "// Write your code below\n",
                Example: "Console.WriteLine(\"Hi there!\");\n// Output: Hi there!",
                Hints: new List<HintTier>
                {
                    new("Console.WriteLine(...) prints text. Put the exact greeting inside the parentheses, in quotes."),
                    new("Console.WriteLine(\"Hello, C#!\");", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Hello, C#!"
            ),
            new(
                Id: 2,
                Title: "Introduce Yourself",
                Description: "Print any greeting that includes your name — for example \"Hi, I'm Sam!\".",
                Difficulty: Difficulty.Easy,
                Points: 10,
                StarterCode: "// Print a greeting with your name in it\n",
                Example: "Console.WriteLine(\"Hi, I'm Sam!\");",
                Hints: new List<HintTier>
                {
                    new("Use Console.WriteLine(\"...\") again, just with different text between the quotes — any greeting with your name in it works."),
                    new("Console.WriteLine(\"Hi, I'm Sam!\");", IsSolution: true)
                },
                CheckOutput: output => output.Trim().Length > 0
            ),
            new(
                Id: 3,
                Title: "Your First Variable",
                Description: "Create an int variable named 'age' set to 25, then print it.",
                Difficulty: Difficulty.Easy,
                Points: 10,
                StarterCode: "// Create an int variable named 'age' with value 25\n// Then print it\n",
                Example: "int age = 25;\nConsole.WriteLine(age);\n// Output: 25",
                Hints: new List<HintTier>
                {
                    new("Syntax: int variableName = value; then pass the variable (no quotes) to Console.WriteLine."),
                    new("int age = 25;\nConsole.WriteLine(age);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "25"
            ),
            new(
                Id: 5,
                Title: "Give It a Name",
                Description: "Create a string variable named 'name' with your name, then print it.",
                Difficulty: Difficulty.Easy,
                Points: 10,
                StarterCode: "// Create a string variable named 'name'\n// Then print it\n",
                Example: "string name = \"Alice\";\nConsole.WriteLine(name);\n// Output: Alice",
                Hints: new List<HintTier>
                {
                    new("Strings need double quotes: string name = \"Alice\"; Then print the variable, not the literal text again."),
                    new("string name = \"Alice\";\nConsole.WriteLine(name);", IsSolution: true)
                },
                CheckOutput: output => output.Trim().Length > 0
            ),
            new(
                Id: 6,
                Title: "True or False",
                Description: "Create a bool variable named 'isHungry' set to true, then print it.",
                Difficulty: Difficulty.Easy,
                Points: 10,
                StarterCode: "// Create a bool variable named 'isHungry'\n// Then print it\n",
                Example: "bool isHungry = true;\nConsole.WriteLine(isHungry);\n// Output: True",
                Hints: new List<HintTier>
                {
                    new("Booleans use lowercase true/false in your code: bool isHungry = true;"),
                    new("bool isHungry = true;\nConsole.WriteLine(isHungry);", IsSolution: true)
                },
                CheckOutput: output => output.Trim().Equals("True", StringComparison.OrdinalIgnoreCase)
            ),
            new(
                Id: 7,
                Title: "Exact Change",
                Description: "Create a double variable named 'price' set to 4.99, then print it.",
                Difficulty: Difficulty.Easy,
                Points: 10,
                StarterCode: "// Create a double variable named 'price' with value 4.99\n// Then print it\n",
                Example: "double price = 4.99;\nConsole.WriteLine(price);\n// Output: 4.99",
                Hints: new List<HintTier>
                {
                    new("double works like int but allows decimals: double price = 4.99;"),
                    new("double price = 4.99;\nConsole.WriteLine(price);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "4.99"
            ),
            new(
                Id: 8,
                Title: "Add It Up",
                Description: "Create two ints, a = 10 and b = 5, and print their sum.",
                Difficulty: Difficulty.Easy,
                Points: 15,
                StarterCode: "// Create int a = 10 and int b = 5\n// Print a + b\n",
                Example: "int a = 10;\nint b = 5;\nConsole.WriteLine(a + b);\n// Output: 15",
                Hints: new List<HintTier>
                {
                    new("You can put an expression like a + b directly inside Console.WriteLine(...)."),
                    new("int a = 10;\nint b = 5;\nConsole.WriteLine(a + b);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "15"
            ),
            new(
                Id: 9,
                Title: "Full Name",
                Description: "Create firstName = \"Ada\" and lastName = \"Lovelace\", then print them combined as one full name separated by a space.",
                Difficulty: Difficulty.Medium,
                Points: 15,
                StarterCode: "// Create firstName and lastName variables\n// Print them combined with a space between them\n",
                Example: "string firstName = \"Ada\";\nstring lastName = \"Lovelace\";\nConsole.WriteLine(firstName + \" \" + lastName);\n// Output: Ada Lovelace",
                Hints: new List<HintTier>
                {
                    new("Strings can be joined with +. Don't forget a \" \" in between so the names don't run together."),
                    new("string firstName = \"Ada\";\nstring lastName = \"Lovelace\";\nConsole.WriteLine(firstName + \" \" + lastName);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Ada Lovelace"
            ),
            new(
                Id: 10,
                Title: "Order Total",
                Description: "A coffee costs 3.50 and you're buying 4. Create variables for the price and quantity, then print the total cost.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// Create price = 3.50 and quantity = 4\n// Print price * quantity\n",
                Example: "double price = 3.50;\nint quantity = 4;\nConsole.WriteLine(price * quantity);\n// Output: 14",
                Hints: new List<HintTier>
                {
                    new("Total cost is price times quantity — multiply the two variables inside Console.WriteLine."),
                    new("double price = 3.50;\nint quantity = 4;\nConsole.WriteLine(price * quantity);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "14"
            ),
            new(
                Id: 11,
                Title: "Which Is Bigger?",
                Description: "Create two ints, x = 7 and y = 12. Use an if/else to print whichever value is larger.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// Create int x = 7 and int y = 12\n// Use if/else to print the larger one\n",
                Example: "int x = 7;\nint y = 12;\nif (x > y)\n{\n    Console.WriteLine(x);\n}\nelse\n{\n    Console.WriteLine(y);\n}\n// Output: 12",
                Hints: new List<HintTier>
                {
                    new("if (x > y) { ... } else { ... } lets you print one value or the other depending on the comparison."),
                    new("int x = 7;\nint y = 12;\nif (x > y)\n{\n    Console.WriteLine(x);\n}\nelse\n{\n    Console.WriteLine(y);\n}", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "12"
            ),
            new(
                Id: 4,
                Title: "🎮 Visual: Draw a Circle",
                Description:
                    "Put something on screen! Call Canvas.AddShape(\"c1\", \"circle\", 100, 100) to draw a circle, " +
                    "then print \"Drawn!\". Press Play Again afterward to watch it appear.",
                Difficulty: Difficulty.Easy,
                Points: 10,
                StarterCode: "// Call Canvas.AddShape(\"c1\", \"circle\", 100, 100)\n// Then print \"Drawn!\"\n",
                Example: "Canvas.AddShape(\"c1\", \"circle\", 100, 100);\nConsole.WriteLine(\"Drawn!\");",
                Hints: new List<HintTier>
                {
                    new("Canvas.AddShape(id, type, x, y) draws a shape at that position. Give it a matching id, \"circle\", and the coordinates."),
                    new("Canvas.AddShape(\"c1\", \"circle\", 100, 100);\nConsole.WriteLine(\"Drawn!\");", IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Drawn!",
                CheckSource: source => source.Contains("Canvas.AddShape")
            ),
            new(
                Id: 12,
                Title: "🎮 Visual: Hot or Cold",
                Description:
                    "Create int temperature = 85. Call Canvas.AddShape(\"sun\", \"circle\", 100, 100) to draw it, then use if/else: " +
                    "if temperature is above 80, call Canvas.SetColor(\"sun\", \"#ef4444\") (red, hot!) and print \"Hot day!\"; " +
                    "otherwise call Canvas.SetColor(\"sun\", \"#3b82f6\") (blue) and print \"Cool day.\"",
                Difficulty: Difficulty.Medium,
                Points: 15,
                StarterCode: "// Create int temperature = 85\n// Canvas.AddShape(\"sun\", \"circle\", 100, 100)\n// if/else: color red + \"Hot day!\" if above 80, else blue + \"Cool day.\"\n",
                Example: "int temperature = 85;\nCanvas.AddShape(\"sun\", \"circle\", 100, 100);\nif (temperature > 80)\n{\n    Canvas.SetColor(\"sun\", \"#ef4444\");\n    Console.WriteLine(\"Hot day!\");\n}\nelse\n{\n    Canvas.SetColor(\"sun\", \"#3b82f6\");\n    Console.WriteLine(\"Cool day.\");\n}",
                Hints: new List<HintTier>
                {
                    new("Draw the shape first, then branch on temperature with if/else — each branch needs both a SetColor call and a matching print."),
                    new("int temperature = 85;\nCanvas.AddShape(\"sun\", \"circle\", 100, 100);\nif (temperature > 80)\n{\n    Canvas.SetColor(\"sun\", \"#ef4444\");\n    Console.WriteLine(\"Hot day!\");\n}\nelse\n{\n    Canvas.SetColor(\"sun\", \"#3b82f6\");\n    Console.WriteLine(\"Cool day.\");\n}", IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Hot day!",
                CheckSource: source => source.Contains("Canvas.AddShape") && source.Contains("Canvas.SetColor")
            )
        },
        QuizStages: new List<QuizStage>
        {
            // Multiple-choice, middle-of-level check-in. Pool is bigger than
            // QuestionsPerAttempt so a retry (or a different player) doesn't
            // see the exact same 5 questions in the exact same order.
            new(
                Title: "Fundamentals Check-In",
                AfterTaskId: 6,
                QuestionsPerAttempt: 5,
                Cards: new List<QuizCard>
                {
                    new(
                        "Which method prints text to the console?",
                        new List<string> { "Console.WriteLine()", "Console.Print()", "System.Print()", "Print.Line()" },
                        CorrectIndex: 0
                    ),
                    new(
                        "Which type would you use to store a whole number like 25?",
                        new List<string> { "string", "bool", "int", "double" },
                        CorrectIndex: 2
                    ),
                    new(
                        "Which type stores text?",
                        new List<string> { "int", "string", "bool", "double" },
                        CorrectIndex: 1
                    ),
                    new(
                        "Which type can only ever be true or false?",
                        new List<string> { "double", "int", "string", "bool" },
                        CorrectIndex: 3
                    ),
                    new(
                        "How must text values be written in C# code?",
                        new List<string> { "In single quotes, like 'hi'", "In double quotes, like \"hi\"", "With no quotes at all", "Inside square brackets" },
                        CorrectIndex: 1
                    ),
                    new(
                        "What keyword declares a variable that holds a whole number?",
                        new List<string> { "int", "string", "bool", "double" },
                        CorrectIndex: 0
                    ),
                    new(
                        "What's printed by `Console.WriteLine(true);`?",
                        new List<string> { "true", "True", "1", "\"true\"" },
                        CorrectIndex: 1
                    ),
                    new(
                        "Which of these is a valid variable name in C#?",
                        new List<string> { "2ndPlace", "my-name", "isHungry", "class" },
                        CorrectIndex: 2
                    )
                }
            ),
            // End-of-level swipe format — a longer, faster-paced true/false
            // deck reviewing the whole level's vocabulary in one pass.
            new(
                Title: "Fundamentals Mastery",
                AfterTaskId: 12,
                Format: QuizFormat.SwipeTrueFalse,
                QuestionsPerAttempt: 10,
                Cards: new List<QuizCard>
                {
                    new("A double can store a decimal value like 4.99.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("The + operator can join two strings together.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("The * operator adds two numbers together.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("if/else lets your code choose between two paths based on a condition.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("In `if (x > y)`, `>` checks whether x equals y.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("A bool variable can hold values like 5 or 10.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("String literals in C# must be wrapped in double quotes.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Console.WriteLine can print the result of a math expression directly, like a + b.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("An int variable can store a value like 3.14.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("You need semicolons at the end of most C# statements.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("The % operator multiplies two numbers.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("A variable's type has to be declared before you can use it.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`true` and `false` in C# code must be capitalized, like `True` and `False`.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("You can combine a string and a number with + as long as the string comes first.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("An else block always has to be followed by another if.", new List<string> { "True", "False" }, CorrectIndex: 1)
                }
            )
        }
    );
}
