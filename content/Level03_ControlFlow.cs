namespace app.Content;

/// <summary>
/// Level 3 — Control Flow.
///
/// Builds on Level 1's single if/else and Level 2's operators: richer branching
/// with if/else-if chains, logical operators (&amp;&amp;, ||, !), nested conditions,
/// and switch statements. Ends with a classic single-number FizzBuzz check and a
/// traffic-light switch as real-world-flavored capstones for the level.
/// </summary>
public static class Level03_ControlFlow
{
    public static readonly Level Instance = new(
        Id: 3,
        Name: "Control Flow",
        Description: "Make your programs branch: if/else-if chains, logical operators, and switch statements.",
        Tasks: new List<GameTask>
        {
            GameTask.FlashcardDeck(
                new GlossaryTerm("Conditional Statement", "Code that only runs when a certain condition is true — the computer's version of \"if this, then that\"."),
                new GlossaryTerm("Branch", "A fork in your program's path — an if/else creates two branches, and only one of them actually runs each time."),
                new GlossaryTerm("Comparison Operator", "Symbols like ==, >, <, and != that compare two values and produce a true/false answer."),
                new GlossaryTerm("Logical Operator", "&& (AND), || (OR), and ! (NOT) — combine or flip true/false values to build more complex conditions."),
                new GlossaryTerm("Nested If", "An if statement living inside another if statement's body — lets you check a second condition, but only once the first one already passed."),
                new GlossaryTerm("Switch Statement", "A cleaner alternative to a long chain of if/else-if when you're comparing ONE variable against many possible exact values."),
                new GlossaryTerm("Case", "One branch inside a switch statement — \"in the case that the value is this, do that.\""),
                new GlossaryTerm("Short-Circuit Evaluation", "How && and || skip checking the second condition if the first one already decided the answer — a handy performance and safety trick."),
                new GlossaryTerm("Ternary Operator", "A one-line shortcut for a simple if/else, written as condition ? valueIfTrue : valueIfFalse."),
                new GlossaryTerm("Guard Clause", "An early if-check at the top of your code that exits or returns right away for an invalid case, so the rest of the code doesn't have to worry about it.")),
            new(
                Id: 1,
                Title: "Grade Calculator",
                Description: "Create int score = 82. Using if/else if/else, print \"A\" for 90+, \"B\" for 80-89, \"C\" for 70-79, otherwise \"F\".",
                Difficulty: Difficulty.Medium,
                Points: 15,
                StarterCode: "// Create int score = 82\n// Print the letter grade using if/else if/else\n",
                Example: "int score = 82;\nif (score >= 90) { Console.WriteLine(\"A\"); }\nelse if (score >= 80) { Console.WriteLine(\"B\"); }\nelse if (score >= 70) { Console.WriteLine(\"C\"); }\nelse { Console.WriteLine(\"F\"); }\n// Output: B",
                Hints: new List<HintTier>
                {
                    new("Chain else if blocks and check from highest to lowest so each condition only needs a single >= comparison."),
                    new("int score = 82;\nif (score >= 90) { Console.WriteLine(\"A\"); }\nelse if (score >= 80) { Console.WriteLine(\"B\"); }\nelse if (score >= 70) { Console.WriteLine(\"C\"); }\nelse { Console.WriteLine(\"F\"); }", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "B"
            ),
            new(
                Id: 2,
                Title: "In Range?",
                Description: "Create int age = 25. Print \"Eligible\" if age is 18 or older AND 65 or younger, otherwise print \"Not Eligible\".",
                Difficulty: Difficulty.Medium,
                Points: 15,
                StarterCode: "// Create int age = 25\n// Print \"Eligible\" if age is between 18 and 65 inclusive, else \"Not Eligible\"\n",
                Example: "int age = 25;\nif (age >= 18 && age <= 65)\n{\n    Console.WriteLine(\"Eligible\");\n}\nelse\n{\n    Console.WriteLine(\"Not Eligible\");\n}\n// Output: Eligible",
                Hints: new List<HintTier>
                {
                    new("&& means both conditions must be true. Combine two comparisons with it in a single if."),
                    new("int age = 25;\nif (age >= 18 && age <= 65)\n{\n    Console.WriteLine(\"Eligible\");\n}\nelse\n{\n    Console.WriteLine(\"Not Eligible\");\n}", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Eligible"
            ),
            new(
                Id: 3,
                Title: "Out of Bounds",
                Description: "Create int temperature = -5. Print \"Warning\" if temperature is below 0 OR above 100, otherwise print \"Normal\".",
                Difficulty: Difficulty.Medium,
                Points: 15,
                StarterCode: "// Create int temperature = -5\n// Print \"Warning\" if it's below 0 OR above 100, else \"Normal\"\n",
                Example: "int temperature = -5;\nif (temperature < 0 || temperature > 100)\n{\n    Console.WriteLine(\"Warning\");\n}\nelse\n{\n    Console.WriteLine(\"Normal\");\n}\n// Output: Warning",
                Hints: new List<HintTier>
                {
                    new("|| means at least one condition must be true — perfect for \"either extreme\" checks."),
                    new("int temperature = -5;\nif (temperature < 0 || temperature > 100)\n{\n    Console.WriteLine(\"Warning\");\n}\nelse\n{\n    Console.WriteLine(\"Normal\");\n}", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Warning"
            ),
            new(
                Id: 5,
                Title: "Flip It",
                Description: "Create bool isRaining = false. Print \"Bring an umbrella\" if it is NOT raining... wait, actually: print \"No umbrella needed\" when isRaining is false, using the ! operator in your condition.",
                Difficulty: Difficulty.Easy,
                Points: 10,
                StarterCode: "// Create bool isRaining = false\n// Use ! to check if it's NOT raining, then print \"No umbrella needed\"\n",
                Example: "bool isRaining = false;\nif (!isRaining)\n{\n    Console.WriteLine(\"No umbrella needed\");\n}\n// Output: No umbrella needed",
                Hints: new List<HintTier>
                {
                    new("!isRaining reads as \"not raining\" — it flips true to false and false to true."),
                    new("bool isRaining = false;\nif (!isRaining)\n{\n    Console.WriteLine(\"No umbrella needed\");\n}", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "No umbrella needed"
            ),
            new(
                Id: 6,
                Title: "Double Check",
                Description: "Create int balance = 500 and bool hasCard = true. Print \"Purchase approved\" only if hasCard is true AND balance is at least 100, checked with a nested if.",
                Difficulty: Difficulty.Medium,
                Points: 15,
                StarterCode: "// Create int balance = 500 and bool hasCard = true\n// Nest two if statements to check both conditions\n",
                Example: "int balance = 500;\nbool hasCard = true;\nif (hasCard)\n{\n    if (balance >= 100)\n    {\n        Console.WriteLine(\"Purchase approved\");\n    }\n}\n// Output: Purchase approved",
                Hints: new List<HintTier>
                {
                    new("Put one if statement's body inside another if statement's body — that's nesting."),
                    new("int balance = 500;\nbool hasCard = true;\nif (hasCard)\n{\n    if (balance >= 100)\n    {\n        Console.WriteLine(\"Purchase approved\");\n    }\n}", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Purchase approved"
            ),
            new(
                Id: 7,
                Title: "Day Namer",
                Description: "Create int day = 3. Use a switch statement to print the day name (1=Monday, 2=Tuesday, 3=Wednesday, ... 7=Sunday).",
                Difficulty: Difficulty.Medium,
                Points: 15,
                StarterCode: "// Create int day = 3\n// Use switch to print the matching day name\n",
                Example: "int day = 3;\nswitch (day)\n{\n    case 1: Console.WriteLine(\"Monday\"); break;\n    case 2: Console.WriteLine(\"Tuesday\"); break;\n    case 3: Console.WriteLine(\"Wednesday\"); break;\n    default: Console.WriteLine(\"Unknown\"); break;\n}\n// Output: Wednesday",
                Hints: new List<HintTier>
                {
                    new("switch (day) { case 1: ...; break; case 2: ...; break; } — don't forget break after each case."),
                    new("int day = 3;\nswitch (day)\n{\n    case 1: Console.WriteLine(\"Monday\"); break;\n    case 2: Console.WriteLine(\"Tuesday\"); break;\n    case 3: Console.WriteLine(\"Wednesday\"); break;\n    case 4: Console.WriteLine(\"Thursday\"); break;\n    case 5: Console.WriteLine(\"Friday\"); break;\n    case 6: Console.WriteLine(\"Saturday\"); break;\n    case 7: Console.WriteLine(\"Sunday\"); break;\n}", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Wednesday"
            ),
            new(
                Id: 8,
                Title: "Weekday or Weekend?",
                Description: "Create int day = 6. Use a switch statement where cases 1-5 print \"Weekday\" (grouped together) and cases 6-7 print \"Weekend\" (grouped together).",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// Create int day = 6\n// Use switch with grouped cases to print \"Weekday\" or \"Weekend\"\n",
                Example: "int day = 6;\nswitch (day)\n{\n    case 1:\n    case 2:\n    case 3:\n    case 4:\n    case 5:\n        Console.WriteLine(\"Weekday\");\n        break;\n    case 6:\n    case 7:\n        Console.WriteLine(\"Weekend\");\n        break;\n}\n// Output: Weekend",
                Hints: new List<HintTier>
                {
                    new("Stack multiple case labels with no code between them to share one body — they all fall through to the same block."),
                    new("int day = 6;\nswitch (day)\n{\n    case 1:\n    case 2:\n    case 3:\n    case 4:\n    case 5:\n        Console.WriteLine(\"Weekday\");\n        break;\n    case 6:\n    case 7:\n        Console.WriteLine(\"Weekend\");\n        break;\n}", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Weekend"
            ),
            new(
                Id: 9,
                Title: "Can They Vote?",
                Description: "Create int age = 20 and bool isCitizen = true. Print \"Can vote\" only if age is 18+ AND isCitizen is true.",
                Difficulty: Difficulty.Medium,
                Points: 15,
                StarterCode: "// Create int age = 20 and bool isCitizen = true\n// Print \"Can vote\" if both conditions are true\n",
                Example: "int age = 20;\nbool isCitizen = true;\nif (age >= 18 && isCitizen)\n{\n    Console.WriteLine(\"Can vote\");\n}\n// Output: Can vote",
                Hints: new List<HintTier>
                {
                    new("A bool variable can be used directly in a condition — you don't need isCitizen == true, just isCitizen."),
                    new("int age = 20;\nbool isCitizen = true;\nif (age >= 18 && isCitizen)\n{\n    Console.WriteLine(\"Can vote\");\n}", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Can vote"
            ),
            new(
                Id: 10,
                Title: "Fizz, Buzz, or Neither?",
                Description: "Create int number = 15. Print \"FizzBuzz\" if divisible by both 3 and 5, \"Fizz\" if only by 3, \"Buzz\" if only by 5, otherwise print the number itself.",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode: "// Create int number = 15\n// Print FizzBuzz / Fizz / Buzz / the number, based on divisibility by 3 and 5\n",
                Example: "int number = 15;\nif (number % 3 == 0 && number % 5 == 0)\n{\n    Console.WriteLine(\"FizzBuzz\");\n}\nelse if (number % 3 == 0)\n{\n    Console.WriteLine(\"Fizz\");\n}\nelse if (number % 5 == 0)\n{\n    Console.WriteLine(\"Buzz\");\n}\nelse\n{\n    Console.WriteLine(number);\n}\n// Output: FizzBuzz",
                Hints: new List<HintTier>
                {
                    new("Check the \"both\" case first with &&, since a number divisible by both 3 and 5 is also divisible by each individually."),
                    new("int number = 15;\nif (number % 3 == 0 && number % 5 == 0)\n{\n    Console.WriteLine(\"FizzBuzz\");\n}\nelse if (number % 3 == 0)\n{\n    Console.WriteLine(\"Fizz\");\n}\nelse if (number % 5 == 0)\n{\n    Console.WriteLine(\"Buzz\");\n}\nelse\n{\n    Console.WriteLine(number);\n}", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "FizzBuzz"
            ),
            new(
                Id: 11,
                Title: "Traffic Light",
                Description: "Create string light = \"yellow\". Use a switch statement to print \"Go\" for \"green\", \"Slow down\" for \"yellow\", and \"Stop\" for \"red\".",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// Create string light = \"yellow\"\n// Use switch to print the matching action\n",
                Example: "string light = \"yellow\";\nswitch (light)\n{\n    case \"green\": Console.WriteLine(\"Go\"); break;\n    case \"yellow\": Console.WriteLine(\"Slow down\"); break;\n    case \"red\": Console.WriteLine(\"Stop\"); break;\n}\n// Output: Slow down",
                Hints: new List<HintTier>
                {
                    new("switch works on strings too, not just numbers — match the case labels exactly, including lowercase."),
                    new("string light = \"yellow\";\nswitch (light)\n{\n    case \"green\": Console.WriteLine(\"Go\"); break;\n    case \"yellow\": Console.WriteLine(\"Slow down\"); break;\n    case \"red\": Console.WriteLine(\"Stop\"); break;\n}", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Slow down"
            ),
            new(
                Id: 4,
                Title: "🎮 Visual: Danger Zone",
                Description:
                    "Create int health = 20. Call Canvas.AddShape(\"hero\", \"circle\", 100, 100) to draw the hero, then use if/else: " +
                    "if health is below 30, call Canvas.SetColor(\"hero\", \"#ef4444\") (red, danger!) and print \"Low health!\"; " +
                    "otherwise call Canvas.SetColor(\"hero\", \"#22c55e\") (green) and print \"All good.\"",
                Difficulty: Difficulty.Medium,
                Points: 15,
                StarterCode: "// Create int health = 20\n// Canvas.AddShape(\"hero\", \"circle\", 100, 100)\n// if/else: color red + \"Low health!\" if below 30, else green + \"All good.\"\n",
                Example: "int health = 20;\nCanvas.AddShape(\"hero\", \"circle\", 100, 100);\nif (health < 30)\n{\n    Canvas.SetColor(\"hero\", \"#ef4444\");\n    Console.WriteLine(\"Low health!\");\n}\nelse\n{\n    Canvas.SetColor(\"hero\", \"#22c55e\");\n    Console.WriteLine(\"All good.\");\n}",
                Hints: new List<HintTier>
                {
                    new("Draw the shape first, then branch on health with if/else — each branch needs both a SetColor call and a matching print."),
                    new("int health = 20;\nCanvas.AddShape(\"hero\", \"circle\", 100, 100);\nif (health < 30)\n{\n    Canvas.SetColor(\"hero\", \"#ef4444\");\n    Console.WriteLine(\"Low health!\");\n}\nelse\n{\n    Canvas.SetColor(\"hero\", \"#22c55e\");\n    Console.WriteLine(\"All good.\");\n}", IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Low health!",
                CheckSource: source => source.Contains("Canvas.AddShape") && source.Contains("Canvas.SetColor")
            ),
            new(
                Id: 12,
                Title: "🎮 Visual: Weather Icon",
                Description:
                    "Create int weather = 1 (0 = sunny, 1 = rainy, 2 = snowy). Call Canvas.AddShape(\"icon\", \"circle\", 100, 100), " +
                    "then use a switch on weather to Canvas.SetColor(\"icon\", ...) and print the matching name: " +
                    "case 0 -> \"#facc15\" (yellow) and \"Sunny\"; case 1 -> \"#38bdf8\" (blue) and \"Rainy\"; case 2 -> \"#e2e8f0\" (white) and \"Snowy\".",
                Difficulty: Difficulty.Medium,
                Points: 15,
                StarterCode: "// Create int weather = 1\n// Canvas.AddShape(\"icon\", \"circle\", 100, 100)\n// switch (weather): 0 -> yellow \"Sunny\", 1 -> blue \"Rainy\", 2 -> white \"Snowy\"\n",
                Example: "int weather = 1;\nCanvas.AddShape(\"icon\", \"circle\", 100, 100);\nswitch (weather)\n{\n    case 0:\n        Canvas.SetColor(\"icon\", \"#facc15\");\n        Console.WriteLine(\"Sunny\");\n        break;\n    case 1:\n        Canvas.SetColor(\"icon\", \"#38bdf8\");\n        Console.WriteLine(\"Rainy\");\n        break;\n    case 2:\n        Canvas.SetColor(\"icon\", \"#e2e8f0\");\n        Console.WriteLine(\"Snowy\");\n        break;\n}",
                Hints: new List<HintTier>
                {
                    new("Same switch shape as \"Day Namer\" earlier in this level — each case needs both a Canvas.SetColor call and a matching print, then break."),
                    new("int weather = 1;\nCanvas.AddShape(\"icon\", \"circle\", 100, 100);\nswitch (weather)\n{\n    case 0:\n        Canvas.SetColor(\"icon\", \"#facc15\");\n        Console.WriteLine(\"Sunny\");\n        break;\n    case 1:\n        Canvas.SetColor(\"icon\", \"#38bdf8\");\n        Console.WriteLine(\"Rainy\");\n        break;\n    case 2:\n        Canvas.SetColor(\"icon\", \"#e2e8f0\");\n        Console.WriteLine(\"Snowy\");\n        break;\n}", IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Rainy",
                CheckSource: source => source.Contains("Canvas.AddShape") && source.Contains("switch") && source.Contains("Canvas.SetColor")
            )
        },
        QuizStages: new List<QuizStage>
        {
            new(
                Title: "Control Flow Check-In",
                AfterTaskId: 6,
                QuestionsPerAttempt: 5,
                Cards: new List<QuizCard>
                {
                    new("Which keyword chains an extra condition onto an if?", new List<string> { "else if", "then if", "or if", "next if" }, CorrectIndex: 0),
                    new("What does && mean?", new List<string> { "OR", "NOT", "AND", "EQUALS" }, CorrectIndex: 2),
                    new("What does || mean?", new List<string> { "AND", "OR", "NOT", "XOR" }, CorrectIndex: 1),
                    new("What does the ! operator do to a bool?", new List<string> { "Doubles it", "Flips it to the opposite", "Converts it to an int", "Nothing"}, CorrectIndex: 1),
                    new("For `age >= 18 && age <= 65` to be true, what must hold?", new List<string> { "Only one of the two conditions", "Neither condition", "Both conditions", "Exactly one condition, but not both" }, CorrectIndex: 2),
                    new("What's a nested if?", new List<string> { "An if inside another if's body", "Two ifs joined with &&", "An if with no else", "A switch statement" }, CorrectIndex: 0)
                }
            ),
            new(
                Title: "Control Flow Mastery",
                AfterTaskId: 12,
                Format: QuizFormat.SwipeTrueFalse,
                QuestionsPerAttempt: 10,
                Cards: new List<QuizCard>
                {
                    new("A switch statement can replace a long chain of if/else-if comparing the same variable.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Multiple switch cases can share the same body by listing them one after another.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`default` in a switch runs when no case matches.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A number divisible by both 3 and 5 is also divisible by 15.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`&&` still evaluates the second condition even if the first is already false.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("`||` is true if at least one side is true.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A switch statement can only compare strings, never numbers.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("`!isRaining` is true exactly when isRaining is false.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("An if/else-if chain stops at the first branch whose condition is true.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("You can nest an if statement inside another if statement's body.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("FizzBuzz-style logic needs to check divisibility by both numbers before checking either alone.", new List<string> { "True", "False" }, CorrectIndex: 0)
                }
            )
        }
    );
}
