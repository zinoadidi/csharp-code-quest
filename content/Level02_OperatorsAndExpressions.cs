namespace app.Content;

/// <summary>
/// Level 2 — Operators &amp; Expressions.
///
/// Builds directly on Level 1's variables: now we do things *to* them.
/// Increment/compound assignment, modulo, casting to avoid integer division,
/// string interpolation, and the ternary operator. Ends with a real-world "split
/// the bill" task that combines several of the level's concepts at once.
/// </summary>
public static class Level02_OperatorsAndExpressions
{
    public static readonly Level Instance = new(
        Id: 2,
        Name: "Operators & Expressions",
        Description: "Do more with your variables: math shortcuts, casting, and string interpolation.",
        Tasks: new List<GameTask>
        {
            GameTask.FlashcardDeck(
                new GlossaryTerm("Operator", "A symbol that does something to a value or two, like + to add or > to compare — the verbs of a programming language."),
                new GlossaryTerm("Expression", "Any bit of code that produces a value, like 2 + 3 or age > 18 — you can print it, store it, or use it in a condition."),
                new GlossaryTerm("Arithmetic Operator", "The math symbols +, -, *, /, and % — do addition, subtraction, multiplication, division, and remainder, just like on a calculator."),
                new GlossaryTerm("Increment", "Bumping a number up by one, usually written as a shortcut like count++ instead of count = count + 1."),
                new GlossaryTerm("Modulo", "The % operator — gives you the remainder left over after dividing, which is why 7 % 2 is 1, not 3.5."),
                new GlossaryTerm("Casting", "Converting a value from one data type to another on purpose, like turning the text \"42\" into the actual number 42."),
                new GlossaryTerm("Concatenation", "Joining strings together end to end, usually with +, so \"Hello, \" + \"World\" becomes \"Hello, World\"."),
                new GlossaryTerm("String Interpolation", "Building a string with variables baked right in, using $\"...\" and curly braces — like $\"Hi, {name}!\" instead of gluing pieces together with +."),
                new GlossaryTerm("Boolean", "A value that can only ever be true or false — the simplest possible answer to a yes-or-no question in code."),
                new GlossaryTerm("Precedence", "The order operations happen in when a line has more than one — just like math class, multiplication happens before addition unless parentheses say otherwise.")),
            new(
                Id: 1,
                Title: "Count Up",
                Description: "Create int counter = 0. Increment it twice using counter++, then print it.",
                Difficulty: Difficulty.Easy,
                Points: 10,
                StarterCode: "// Create int counter = 0\n// Increment it twice with counter++\n// Print it\n",
                Example: "int counter = 0;\ncounter++;\ncounter++;\nConsole.WriteLine(counter);\n// Output: 2",
                Hints: new List<HintTier>
                {
                    new("counter++ adds 1 to counter each time it runs. Use it twice before printing."),
                    new("int counter = 0;\ncounter++;\ncounter++;\nConsole.WriteLine(counter);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "2"
            ),
            new(
                Id: 2,
                Title: "Add to the Total",
                Description: "Create int total = 10. Add 5 to it using += then print the result.",
                Difficulty: Difficulty.Easy,
                Points: 10,
                StarterCode: "// Create int total = 10\n// Add 5 using +=\n// Print it\n",
                Example: "int total = 10;\ntotal += 5;\nConsole.WriteLine(total);\n// Output: 15",
                Hints: new List<HintTier>
                {
                    new("total += 5; is shorthand for total = total + 5;"),
                    new("int total = 10;\ntotal += 5;\nConsole.WriteLine(total);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "15"
            ),
            new(
                Id: 3,
                Title: "What's Left Over?",
                Description: "Create int number = 7. Print the remainder of number divided by 2 using the % operator.",
                Difficulty: Difficulty.Easy,
                Points: 10,
                StarterCode: "// Create int number = 7\n// Print number % 2\n",
                Example: "int number = 7;\nConsole.WriteLine(number % 2);\n// Output: 1",
                Hints: new List<HintTier>
                {
                    new("% gives you the remainder of a division — it's how you check even/odd."),
                    new("int number = 7;\nConsole.WriteLine(number % 2);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "1"
            ),
            new(
                Id: 5,
                Title: "Weather Report",
                Description: "Convert 20 degrees Celsius to Fahrenheit using the formula F = C * 9 / 5 + 32, and print the result.",
                Difficulty: Difficulty.Medium,
                Points: 15,
                StarterCode: "// Create double celsius = 20\n// Compute fahrenheit using F = C * 9 / 5 + 32\n// Print it\n",
                Example: "double celsius = 20;\ndouble fahrenheit = celsius * 9 / 5 + 32;\nConsole.WriteLine(fahrenheit);\n// Output: 68",
                Hints: new List<HintTier>
                {
                    new("Follow the formula exactly, using a double so the math has room for decimals even though this example comes out whole."),
                    new("double celsius = 20;\ndouble fahrenheit = celsius * 9 / 5 + 32;\nConsole.WriteLine(fahrenheit);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "68"
            ),
            new(
                Id: 6,
                Title: "Scoreboard",
                Description: "Create string name = \"Max\" and int score = 95. Print \"Max scored 95 points!\" using string interpolation ($\"...\").",
                Difficulty: Difficulty.Medium,
                Points: 15,
                StarterCode: "// Create string name = \"Max\" and int score = 95\n// Print \"Max scored 95 points!\" using $\"...\"\n",
                Example: "string name = \"Max\";\nint score = 95;\nConsole.WriteLine($\"{name} scored {score} points!\");\n// Output: Max scored 95 points!",
                Hints: new List<HintTier>
                {
                    new("A $\"...\" string lets you drop variables straight into the text using {curly braces} — no + concatenation needed."),
                    new("string name = \"Max\";\nint score = 95;\nConsole.WriteLine($\"{name} scored {score} points!\");", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Max scored 95 points!"
            ),
            new(
                Id: 7,
                Title: "Precise Division",
                Description: "Divide 7 by 2 and print a precise decimal result (not the whole-number 3). Hint: cast one of the ints to double first.",
                Difficulty: Difficulty.Medium,
                Points: 15,
                StarterCode: "// Create int a = 7 and int b = 2\n// Print a precise division result (should be 3.5, not 3)\n",
                Example: "int a = 7;\nint b = 2;\ndouble result = (double)a / b;\nConsole.WriteLine(result);\n// Output: 3.5",
                Hints: new List<HintTier>
                {
                    new("int divided by int always truncates. Cast at least one side to double with (double)a before dividing."),
                    new("int a = 7;\nint b = 2;\ndouble result = (double)a / b;\nConsole.WriteLine(result);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "3.5"
            ),
            new(
                Id: 8,
                Title: "Order Matters",
                Description: "Print the result of (5 + 3) * 2.",
                Difficulty: Difficulty.Easy,
                Points: 10,
                StarterCode: "// Print (5 + 3) * 2\n",
                Example: "Console.WriteLine((5 + 3) * 2);\n// Output: 16",
                Hints: new List<HintTier>
                {
                    new("Parentheses control order of operations just like in math class."),
                    new("Console.WriteLine((5 + 3) * 2);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "16"
            ),
            new(
                Id: 9,
                Title: "Adult or Minor?",
                Description: "Create int age = 20. Use the ternary operator (condition ? a : b) to print \"Adult\" if age is 18 or older, otherwise \"Minor\".",
                Difficulty: Difficulty.Medium,
                Points: 15,
                StarterCode: "// Create int age = 20\n// Print \"Adult\" or \"Minor\" using the ternary operator\n",
                Example: "int age = 20;\nConsole.WriteLine(age >= 18 ? \"Adult\" : \"Minor\");\n// Output: Adult",
                Hints: new List<HintTier>
                {
                    new("condition ? valueIfTrue : valueIfFalse — it's a compact if/else that produces a value directly."),
                    new("int age = 20;\nConsole.WriteLine(age >= 18 ? \"Adult\" : \"Minor\");", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Adult"
            ),
            new(
                Id: 10,
                Title: "Are They Equal?",
                Description: "Create int a = 5 and int b = 5. Print whether a equals b using ==.",
                Difficulty: Difficulty.Easy,
                Points: 10,
                StarterCode: "// Create int a = 5 and int b = 5\n// Print a == b\n",
                Example: "int a = 5;\nint b = 5;\nConsole.WriteLine(a == b);\n// Output: True",
                Hints: new List<HintTier>
                {
                    new("== compares values and produces a bool (True/False), unlike = which assigns."),
                    new("int a = 5;\nint b = 5;\nConsole.WriteLine(a == b);", IsSolution: true)
                },
                CheckOutput: output => output.Trim().Equals("True", StringComparison.OrdinalIgnoreCase)
            ),
            new(
                Id: 11,
                Title: "Split the Bill",
                Description: "A $47.60 restaurant bill is split evenly among 4 friends. Print \"Each person pays $11.9\" using string interpolation.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// Create double bill = 47.60 and int people = 4\n// Print each person's share using string interpolation\n",
                Example: "double bill = 47.60;\nint people = 4;\ndouble share = bill / people;\nConsole.WriteLine($\"Each person pays ${share}\");\n// Output: Each person pays $11.9",
                Hints: new List<HintTier>
                {
                    new("Combine everything from this level: divide to get a precise share, then use $\"...\" to build the sentence."),
                    new("double bill = 47.60;\nint people = 4;\ndouble share = bill / people;\nConsole.WriteLine($\"Each person pays ${share}\");", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Each person pays $11.9"
            ),
            new(
                Id: 4,
                Title: "🎮 Visual: Slide the Box",
                Description:
                    "Create int x = 50. Add 120 to x using +=. Call Canvas.AddShape(\"box\", \"rect\", 50, 100) to place the box, " +
                    "then Canvas.MoveTo(\"box\", x, 100) to slide it to its new spot. Print \"Slid to \" + x.",
                Difficulty: Difficulty.Medium,
                Points: 15,
                StarterCode: "// Create int x = 50, add 120 with +=\n// Canvas.AddShape(\"box\", \"rect\", 50, 100)\n// Canvas.MoveTo(\"box\", x, 100)\n// Print \"Slid to \" + x\n",
                Example: "int x = 50;\nx += 120;\nCanvas.AddShape(\"box\", \"rect\", 50, 100);\nCanvas.MoveTo(\"box\", x, 100);\nConsole.WriteLine(\"Slid to \" + x);",
                Hints: new List<HintTier>
                {
                    new("Add to x first with +=, then use the new x value in Canvas.MoveTo so the box slides to the updated position."),
                    new("int x = 50;\nx += 120;\nCanvas.AddShape(\"box\", \"rect\", 50, 100);\nCanvas.MoveTo(\"box\", x, 100);\nConsole.WriteLine(\"Slid to \" + x);", IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Slid to 170",
                CheckSource: source => source.Contains("Canvas.AddShape") && source.Contains("Canvas.MoveTo")
            ),
            new(
                Id: 12,
                Title: "🎮 Visual: Score Meter",
                Description:
                    "Create int score = 7. Call Canvas.AddShape(\"meter\", \"rect\", 20, 100, score * 10) to draw a meter whose " +
                    "size is the score multiplied by 10, then print \"Score: \" + score.",
                Difficulty: Difficulty.Easy,
                Points: 15,
                StarterCode: "// Create int score = 7\n// Canvas.AddShape(\"meter\", \"rect\", 20, 100, score * 10)\n// Print \"Score: \" + score\n",
                Example: "int score = 7;\nCanvas.AddShape(\"meter\", \"rect\", 20, 100, score * 10);\nConsole.WriteLine(\"Score: \" + score);",
                Hints: new List<HintTier>
                {
                    new("The meter's size (5th argument to AddShape) should be the multiplication expression itself, not a separately computed variable — score * 10."),
                    new("int score = 7;\nCanvas.AddShape(\"meter\", \"rect\", 20, 100, score * 10);\nConsole.WriteLine(\"Score: \" + score);", IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Score: 7",
                CheckSource: source => source.Contains("Canvas.AddShape") && source.Contains("*")
            )
        },
        QuizStages: new List<QuizStage>
        {
            new(
                Title: "Operators Check-In",
                AfterTaskId: 6,
                QuestionsPerAttempt: 5,
                Cards: new List<QuizCard>
                {
                    new("What does `counter++` do to counter?", new List<string> { "Doubles it", "Adds 1 to it", "Subtracts 1 from it", "Resets it to 0" }, CorrectIndex: 1),
                    new("What does `total += 5` do?", new List<string> { "Sets total to 5", "Compares total to 5", "Adds 5 to total", "Divides total by 5" }, CorrectIndex: 2),
                    new("What does the % operator give you?", new List<string> { "A percentage", "The remainder of a division", "The square root", "A random number" }, CorrectIndex: 1),
                    new("What is 7 % 2?", new List<string> { "3.5", "3", "1", "0" }, CorrectIndex: 2),
                    new("Which symbol starts a string interpolation literal?", new List<string> { "@", "#", "$", "%" }, CorrectIndex: 2),
                    new("Inside an interpolated string, how do you insert a variable's value?", new List<string> { "Curly braces, like {name}", "Square brackets, like [name]", "Parentheses, like (name)", "Angle brackets, like <name>" }, CorrectIndex: 0),
                    new("What does `total += 5` change total's value to, if it started at 10?", new List<string> { "5", "10", "15", "50" }, CorrectIndex: 2)
                }
            ),
            new(
                Title: "Operators Mastery",
                AfterTaskId: 12,
                Format: QuizFormat.SwipeTrueFalse,
                QuestionsPerAttempt: 10,
                Cards: new List<QuizCard>
                {
                    new("Dividing two ints in C# always gives you a precise decimal result.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("Casting one operand to double before dividing gives you a decimal result.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("The ternary operator has the form `condition ? a : b`.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`==` assigns a value to a variable.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("Parentheses can control the order operations happen in, just like in math.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`(5 + 3) * 2` evaluates to 16.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`5 + 3 * 2` (no parentheses) also evaluates to 16.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("String interpolation needs the `$` prefix before the opening quote.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`total += 5` is shorthand for `total = total + 5`.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("The % operator only works on strings, not numbers.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("A ternary expression can replace a simple if/else that just picks between two values.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Casting an int to double can lose precision.", new List<string> { "True", "False" }, CorrectIndex: 1)
                }
            )
        }
    );
}
