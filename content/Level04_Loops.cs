namespace app.Content;

/// <summary>
/// Level 4 — Loops.
///
/// for, while, do-while, break/continue, and nested loops. Closes with this
/// game's first genuine mini-game task: a "Guess the Secret Number" simulation
/// that plays itself out once the student's loop logic is correct — the first
/// moment where finishing a task means watching something game-like actually run,
/// not just seeing a value printed.
/// </summary>
public static class Level04_Loops
{
    public static readonly Level Instance = new(
        Id: 4,
        Name: "Loops",
        Description: "Repeat yourself on purpose: for, while, do-while, and your first playable mini-game.",
        Tasks: new List<GameTask>
        {
            GameTask.FlashcardDeck(
                new GlossaryTerm("Loop", "Code that repeats an action multiple times instead of you copy-pasting the same line over and over."),
                new GlossaryTerm("Iteration", "One single pass through a loop's body — a loop that runs 5 times has performed 5 iterations."),
                new GlossaryTerm("For Loop", "A loop built for counting — it sets up a counter, checks a condition, and updates the counter, all in one line."),
                new GlossaryTerm("While Loop", "A loop that keeps going as long as a condition stays true — perfect when you don't know in advance how many times you'll repeat."),
                new GlossaryTerm("Do-While Loop", "Like a while loop, but it checks the condition AFTER running the body once — so it always runs at least one time."),
                new GlossaryTerm("Infinite Loop", "A loop whose condition never becomes false, so it never stops — usually a bug, unless you meant to build something like a game's main loop."),
                new GlossaryTerm("Break", "A keyword that immediately jumps out of the loop you're in, skipping any remaining iterations."),
                new GlossaryTerm("Continue", "A keyword that skips the rest of THIS iteration and jumps straight to the next one, without leaving the loop entirely."),
                new GlossaryTerm("Loop Counter", "The variable a loop uses to keep track of which iteration it's on — commonly named i, short for \"index\"."),
                new GlossaryTerm("Nested Loop", "A loop living inside another loop's body — the inner loop runs all the way through once for every single iteration of the outer one.")),
            new(
                Id: 1,
                Title: "Countdown",
                Description: "Use a for loop to print 5 down to 1, then print \"Liftoff!\".",
                Difficulty: Difficulty.Easy,
                Points: 15,
                StarterCode: "// Use a for loop to print 5, 4, 3, 2, 1\n// Then print \"Liftoff!\"\n",
                Example: "for (int i = 5; i >= 1; i--)\n{\n    Console.WriteLine(i);\n}\nConsole.WriteLine(\"Liftoff!\");",
                Hints: new List<HintTier>
                {
                    new("A for loop can count down too: for (int i = 5; i >= 1; i--)."),
                    new("for (int i = 5; i >= 1; i--)\n{\n    Console.WriteLine(i);\n}\nConsole.WriteLine(\"Liftoff!\");", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "5", "4", "3", "2", "1", "Liftoff!")
            ),
            new(
                Id: 2,
                Title: "Count By Twos",
                Description: "Use a for loop to print every even number from 0 to 10.",
                Difficulty: Difficulty.Easy,
                Points: 15,
                StarterCode: "// Use a for loop to print 0, 2, 4, 6, 8, 10\n",
                Example: "for (int i = 0; i <= 10; i += 2)\n{\n    Console.WriteLine(i);\n}",
                Hints: new List<HintTier>
                {
                    new("The loop's third part doesn't have to be i++ — try i += 2 to skip by twos."),
                    new("for (int i = 0; i <= 10; i += 2)\n{\n    Console.WriteLine(i);\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "0", "2", "4", "6", "8", "10")
            ),
            new(
                Id: 4,
                Title: "Add It All Up",
                Description: "Use a for loop to add up the numbers 1 through 10, then print the total.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// Create int sum = 0\n// Use a for loop to add 1 through 10 into sum\n// Print sum\n",
                Example: "int sum = 0;\nfor (int i = 1; i <= 10; i++)\n{\n    sum += i;\n}\nConsole.WriteLine(sum);\n// Output: 55",
                Hints: new List<HintTier>
                {
                    new("Start sum at 0 outside the loop, then add each i to it inside the loop body with sum += i."),
                    new("int sum = 0;\nfor (int i = 1; i <= 10; i++)\n{\n    sum += i;\n}\nConsole.WriteLine(sum);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "55"
            ),
            new(
                Id: 5,
                Title: "Lap Counter",
                Description: "Use a while loop to print \"Lap 1\", \"Lap 2\", \"Lap 3\".",
                Difficulty: Difficulty.Medium,
                Points: 15,
                StarterCode: "// Create int count = 1\n// Use a while loop to print \"Lap 1\" through \"Lap 3\"\n",
                Example: "int count = 1;\nwhile (count <= 3)\n{\n    Console.WriteLine(\"Lap \" + count);\n    count++;\n}",
                Hints: new List<HintTier>
                {
                    new("A while loop needs you to manually increment the counter inside the loop body, or it never ends."),
                    new("int count = 1;\nwhile (count <= 3)\n{\n    Console.WriteLine(\"Lap \" + count);\n    count++;\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "Lap 1", "Lap 2", "Lap 3")
            ),
            new(
                Id: 6,
                Title: "Always Say Hi",
                Description: "Use a do-while loop to print \"Welcome!\" — it should print even though the loop's condition (attempts < 0) starts out false.",
                Difficulty: Difficulty.Medium,
                Points: 15,
                StarterCode: "// Create int attempts = 0\n// Use do-while to print \"Welcome!\" once, checking attempts < 0\n",
                Example: "int attempts = 0;\ndo\n{\n    Console.WriteLine(\"Welcome!\");\n    attempts++;\n}\nwhile (attempts < 0);\n// Output: Welcome!",
                Hints: new List<HintTier>
                {
                    new("do { ... } while (condition); always runs the body at least once, before the condition is even checked."),
                    new("int attempts = 0;\ndo\n{\n    Console.WriteLine(\"Welcome!\");\n    attempts++;\n}\nwhile (attempts < 0);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Welcome!"
            ),
            new(
                Id: 7,
                Title: "Skip the Multiples",
                Description: "Loop from 1 to 10, but use continue to skip printing any multiple of 3.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// Loop from 1 to 10\n// Use continue to skip multiples of 3\n// Print everything else\n",
                Example: "for (int i = 1; i <= 10; i++)\n{\n    if (i % 3 == 0) continue;\n    Console.WriteLine(i);\n}",
                Hints: new List<HintTier>
                {
                    new("continue jumps straight to the next loop iteration, skipping whatever code comes after it in the body."),
                    new("for (int i = 1; i <= 10; i++)\n{\n    if (i % 3 == 0) continue;\n    Console.WriteLine(i);\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "1", "2", "4", "5", "7", "8", "10")
            ),
            new(
                Id: 8,
                Title: "First One Wins",
                Description: "Loop from 1 to 100 and print the first number that's evenly divisible by 7, then stop looping immediately with break.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// Loop from 1 to 100\n// Print and break on the first number divisible by 7\n",
                Example: "for (int i = 1; i <= 100; i++)\n{\n    if (i % 7 == 0)\n    {\n        Console.WriteLine(i);\n        break;\n    }\n}\n// Output: 7",
                Hints: new List<HintTier>
                {
                    new("break exits the loop entirely — use it right after printing so the loop doesn't keep going and find more matches."),
                    new("for (int i = 1; i <= 100; i++)\n{\n    if (i % 7 == 0)\n    {\n        Console.WriteLine(i);\n        break;\n    }\n}", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "7"
            ),
            new(
                Id: 9,
                Title: "Star Triangle",
                Description: "Use a nested loop to print a triangle of stars: one \"*\" on the first line, two on the second, three on the third.",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode: "// Use nested for loops to print:\n// *\n// **\n// ***\n",
                Example: "for (int i = 1; i <= 3; i++)\n{\n    for (int j = 1; j <= i; j++)\n    {\n        Console.Write(\"*\");\n    }\n    Console.WriteLine();\n}",
                Hints: new List<HintTier>
                {
                    new("Console.Write (no \"Line\") doesn't add a newline, so you can build up a row of stars before calling Console.WriteLine() once per row."),
                    new("for (int i = 1; i <= 3; i++)\n{\n    for (int j = 1; j <= i; j++)\n    {\n        Console.Write(\"*\");\n    }\n    Console.WriteLine();\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "*", "**", "***")
            ),
            new(
                Id: 10,
                Title: "Countdown Timer",
                Description: "Use a while loop to print \"Tick: 3\", \"Tick: 2\", \"Tick: 1\", then \"Time's up!\" after the loop ends.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// Create int time = 3\n// While time >= 1, print \"Tick: \" + time and count down\n// After the loop, print \"Time's up!\"\n",
                Example: "int time = 3;\nwhile (time >= 1)\n{\n    Console.WriteLine(\"Tick: \" + time);\n    time--;\n}\nConsole.WriteLine(\"Time's up!\");",
                Hints: new List<HintTier>
                {
                    new("Put \"Time's up!\" after the closing brace of the while loop, so it only prints once the loop is done."),
                    new("int time = 3;\nwhile (time >= 1)\n{\n    Console.WriteLine(\"Tick: \" + time);\n    time--;\n}\nConsole.WriteLine(\"Time's up!\");", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "Tick: 3", "Tick: 2", "Tick: 1", "Time's up!")
            ),
            new(
                Id: 11,
                Title: "🎮 Mini-Game: Guess the Secret Number",
                Description:
                    "Your first mini-game! The secret number is 42, and a friend has already guessed 10, then 50, then 42 " +
                    "for you (that part's already written below). Loop through the guesses one at a time and print whether " +
                    "each one was \"Too low\", \"Too high\", or \"Correct!\" — and stop checking as soon as you hit the right one. " +
                    "Once this passes, you'll see the whole guessing game play out in the output exactly like a real player " +
                    "would experience it. Try changing the secret number or the guesses afterward to replay it differently!",
                Difficulty: Difficulty.Hard,
                Points: 30,
                StarterCode:
                    "int secret = 42;\n" +
                    "int[] guesses = { 10, 50, 42 };\n" +
                    "int i = 0;\n" +
                    "// TODO: while there are still guesses left to check:\n" +
                    "//   - get the current guess: guesses[i]\n" +
                    "//   - if it's less than secret, print \"<guess> -> Too low\"\n" +
                    "//   - if it's more than secret, print \"<guess> -> Too high\"\n" +
                    "//   - if it matches, print \"<guess> -> Correct!\" and stop the loop\n" +
                    "//   - otherwise move to the next guess\n",
                Example:
                    "int secret = 42;\n" +
                    "int[] guesses = { 10, 50, 42 };\n" +
                    "int i = 0;\n" +
                    "while (i < guesses.Length)\n" +
                    "{\n" +
                    "    int guess = guesses[i];\n" +
                    "    if (guess < secret) { Console.WriteLine(guess + \" -> Too low\"); }\n" +
                    "    else if (guess > secret) { Console.WriteLine(guess + \" -> Too high\"); }\n" +
                    "    else { Console.WriteLine(guess + \" -> Correct!\"); break; }\n" +
                    "    i++;\n" +
                    "}",
                Hints: new List<HintTier>
                {
                    new("This combines everything from this level: a while loop over the array's indexes, an if/else-if/else to classify each guess, and break once you find the match."),
                    new("Every guess needs the same three-way check (too low / too high / correct) — write that as one if/else-if/else block inside the loop, and break only in the \"correct\" branch."),
                    new(
                        "int secret = 42;\n" +
                        "int[] guesses = { 10, 50, 42 };\n" +
                        "int i = 0;\n" +
                        "while (i < guesses.Length)\n" +
                        "{\n" +
                        "    int guess = guesses[i];\n" +
                        "    if (guess < secret) { Console.WriteLine(guess + \" -> Too low\"); }\n" +
                        "    else if (guess > secret) { Console.WriteLine(guess + \" -> Too high\"); }\n" +
                        "    else { Console.WriteLine(guess + \" -> Correct!\"); break; }\n" +
                        "    i++;\n" +
                        "}",
                        IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => OutputChecks.LinesEqual(output, "10 -> Too low", "50 -> Too high", "42 -> Correct!")
            ),
            new(
                Id: 3,
                Title: "🎮 Visual: Build a Staircase",
                Description:
                    "Use a for loop with i from 0 to 4 to draw 5 steps: each iteration, call " +
                    "Canvas.AddShape(\"step\" + i, \"rect\", i * 40, 200 - i * 30). After the loop, print \"Staircase built!\"",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// for (int i = 0; i < 5; i++)\n// Canvas.AddShape(\"step\" + i, \"rect\", i * 40, 200 - i * 30)\n// Print \"Staircase built!\" after the loop\n",
                Example: "for (int i = 0; i < 5; i++)\n{\n    Canvas.AddShape(\"step\" + i, \"rect\", i * 40, 200 - i * 30);\n}\nConsole.WriteLine(\"Staircase built!\");",
                Hints: new List<HintTier>
                {
                    new("Each step needs a unique id, so build it from i (like \"step\" + i) instead of reusing the same string every time."),
                    new("for (int i = 0; i < 5; i++)\n{\n    Canvas.AddShape(\"step\" + i, \"rect\", i * 40, 200 - i * 30);\n}\nConsole.WriteLine(\"Staircase built!\");", IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Staircase built!",
                CheckSource: source => source.Contains("Canvas.AddShape") && source.Contains("for")
            ),
            new(
                Id: 12,
                Title: "🎮 Visual: Loading Lights",
                Description:
                    "Use a for loop with i from 0 to 4 to draw 5 lights, each starting gray: call " +
                    "Canvas.AddShape(\"light\" + i, \"diamond\", i * 40 + 20, 100, 30, \"#334155\"). " +
                    "Then use a SECOND for loop over the same range to call Canvas.SetColor(\"light\" + i, \"#22c55e\") (green), " +
                    "turning each one on. Print \"Lights on!\" once both loops are done.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// First loop: for (int i = 0; i < 5; i++) Canvas.AddShape(\"light\" + i, \"diamond\", i * 40 + 20, 100, 30, \"#334155\")\n// Second loop: for (int i = 0; i < 5; i++) Canvas.SetColor(\"light\" + i, \"#22c55e\")\n// Then print \"Lights on!\"\n",
                Example: "for (int i = 0; i < 5; i++)\n{\n    Canvas.AddShape(\"light\" + i, \"diamond\", i * 40 + 20, 100, 30, \"#334155\");\n}\nfor (int i = 0; i < 5; i++)\n{\n    Canvas.SetColor(\"light\" + i, \"#22c55e\");\n}\nConsole.WriteLine(\"Lights on!\");",
                Hints: new List<HintTier>
                {
                    new("Two separate loops, run one after the other — the first draws every light gray, the second recolors each one green by the same id (\"light\" + i)."),
                    new("for (int i = 0; i < 5; i++)\n{\n    Canvas.AddShape(\"light\" + i, \"diamond\", i * 40 + 20, 100, 30, \"#334155\");\n}\nfor (int i = 0; i < 5; i++)\n{\n    Canvas.SetColor(\"light\" + i, \"#22c55e\");\n}\nConsole.WriteLine(\"Lights on!\");", IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Lights on!",
                CheckSource: source => source.Contains("Canvas.AddShape") && source.Contains("Canvas.SetColor") && source.Contains("for")
            )
        },
        QuizStages: new List<QuizStage>
        {
            new(
                Title: "Loops Check-In",
                AfterTaskId: 6,
                QuestionsPerAttempt: 5,
                Cards: new List<QuizCard>
                {
                    new("What does a `for` loop's second part (the condition) control?", new List<string> { "The starting value", "How the loop variable changes each time", "Whether the loop keeps running", "What gets printed" }, CorrectIndex: 2),
                    new("In `for (int i = 0; i < 5; i++)`, how many times does the loop body run?", new List<string> { "4", "5", "6", "Infinite"}, CorrectIndex: 1),
                    new("What does `i += 2` do inside a loop's update step?", new List<string> { "Increases i by 2 each iteration", "Sets i to 2", "Divides i by 2", "Compares i to 2" }, CorrectIndex: 0),
                    new("Which loop type checks its condition before running the body at all?", new List<string> { "do-while only", "for and while", "Neither", "Only foreach" }, CorrectIndex: 1),
                    new("What happens if a loop's condition is never false?", new List<string> { "It runs once and stops", "It throws an error immediately", "It becomes an infinite loop", "It skips the body" }, CorrectIndex: 2),
                    new("`for (int i = 1; i <= 10; i++)` prints numbers 1 through what?", new List<string> { "9", "10", "11", "0" }, CorrectIndex: 1),
                    new("Which keyword would you use to count down instead of up?", new List<string> { "down", "i--", "reverse", "back"}, CorrectIndex: 1)
                }
            ),
            new(
                Title: "Loops Mastery",
                AfterTaskId: 12,
                Format: QuizFormat.SwipeTrueFalse,
                QuestionsPerAttempt: 10,
                Cards: new List<QuizCard>
                {
                    new("A `for` loop always needs a starting value, a condition, and an update step.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`while` loops check their condition after running the body.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("`i++` and `i += 1` do the same thing.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A loop with a condition that's always true will run forever unless something stops it.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("You can use `%` inside a loop to check if a number is even or a multiple of something.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`break` immediately exits the loop it's inside.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`for (int i = 10; i > 0; i--)` counts upward.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("A loop body can contain an if statement.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Loops can only print things — they can't add numbers together across iterations.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("`i < 5` and `i <= 5` mean the exact same thing.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("A `do-while` loop always runs its body at least once, even if the condition starts false.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("You need a separate loop for every single value you want to print, even 100 of them.", new List<string> { "True", "False" }, CorrectIndex: 1)
                }
            )
        }
    );
}
