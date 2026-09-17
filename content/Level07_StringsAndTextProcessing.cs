namespace app.Content;

/// <summary>
/// Level 7 — Strings &amp; Text Processing.
///
/// String methods, splitting/joining, interpolation, and StringBuilder. Closes
/// with a visual task that splits a sentence into words and lays each one out
/// on the canvas, sized by its own length.
/// </summary>
public static class Level07_StringsAndTextProcessing
{
    public static readonly Level Instance = new(
        Id: 7,
        Name: "Strings & Text Processing",
        Description: "Slice, search, and rebuild text — the everyday toolkit for working with words.",
        Tasks: new List<GameTask>
        {
            GameTask.FlashcardDeck(
                new GlossaryTerm("String", "A piece of text — letters, words, or whole sentences — wrapped in quotes so the computer knows to treat it as text, not a number or code."),
                new GlossaryTerm("Substring", "A smaller piece cut out of a larger string — like pulling just \"Ada\" out of \"Ada Lovelace\"."),
                new GlossaryTerm("Split", "Breaking one string into several smaller strings based on a separator, like turning \"a,b,c\" into [\"a\", \"b\", \"c\"] by splitting on commas."),
                new GlossaryTerm("Trim", "Removing whitespace (spaces, tabs, line breaks) from the start and end of a string, without touching anything in the middle."),
                new GlossaryTerm("Case Conversion", "Changing a string to all-uppercase or all-lowercase, using .ToUpper() or .ToLower() — handy for comparisons that shouldn't care about capitalization."),
                new GlossaryTerm("String Length", "How many characters are in a string, including spaces and punctuation — found with .Length, just like an array."),
                new GlossaryTerm("IndexOf", "A method that tells you WHERE inside a string a smaller piece of text first appears, counting from 0 — or -1 if it's not found at all."),
                new GlossaryTerm("Immutable", "Once created, a string in C# can never be changed in place — every \"edit\" (like .ToUpper()) actually builds and returns a brand new string."),
                new GlossaryTerm("Escape Character", "A special backslash sequence inside a string for characters you couldn't otherwise type directly, like \\n for a new line or \\\" for a literal quote mark."),
                new GlossaryTerm("Whitespace", "Any character that creates visible empty space but isn't itself visible — spaces, tabs, and line breaks are all whitespace.")),
            new(
                Id: 1,
                Title: "Shout It",
                Description: "Given string name = \"grace hopper\", print it in all uppercase.",
                Difficulty: Difficulty.Easy,
                Points: 15,
                StarterCode: "string name = \"grace hopper\";\n// Print name.ToUpper()\n",
                Example: "string name = \"grace hopper\";\nConsole.WriteLine(name.ToUpper());\n// Output: GRACE HOPPER",
                Hints: new List<HintTier>
                {
                    new("Every string has a .ToUpper() method that returns a new, uppercase copy."),
                    new("string name = \"grace hopper\";\nConsole.WriteLine(name.ToUpper());", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "GRACE HOPPER"
            ),
            new(
                Id: 2,
                Title: "Trim the Fat",
                Description: "Given string s = \"  padded  \", print it wrapped in brackets after removing the leading/trailing spaces, like [padded].",
                Difficulty: Difficulty.Easy,
                Points: 15,
                StarterCode: "string s = \"  padded  \";\n// Print \"[\" + s.Trim() + \"]\"\n",
                Example: "string s = \"  padded  \";\nConsole.WriteLine(\"[\" + s.Trim() + \"]\");\n// Output: [padded]",
                Hints: new List<HintTier>
                {
                    new(".Trim() removes whitespace from both ends of a string, but leaves whitespace in the middle alone."),
                    new("string s = \"  padded  \";\nConsole.WriteLine(\"[\" + s.Trim() + \"]\");", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "[padded]"
            ),
            new(
                Id: 4,
                Title: "How Long?",
                Description: "Given string sentence = \"The quick brown fox\", print its length (character count, including spaces).",
                Difficulty: Difficulty.Easy,
                Points: 15,
                StarterCode: "string sentence = \"The quick brown fox\";\n// Print sentence.Length\n",
                Example: "string sentence = \"The quick brown fox\";\nConsole.WriteLine(sentence.Length);\n// Output: 19",
                Hints: new List<HintTier>
                {
                    new(".Length is a property, not a method — no parentheses."),
                    new("string sentence = \"The quick brown fox\";\nConsole.WriteLine(sentence.Length);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "19"
            ),
            new(
                Id: 5,
                Title: "Slice It Out",
                Description: "Given string word = \"Hello\", print the 3 characters starting at index 1 (should print \"ell\").",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "string word = \"Hello\";\n// Print word.Substring(1, 3)\n",
                Example: "string word = \"Hello\";\nConsole.WriteLine(word.Substring(1, 3));\n// Output: ell",
                Hints: new List<HintTier>
                {
                    new("Substring(startIndex, length) takes a starting position and how many characters to grab from there."),
                    new("string word = \"Hello\";\nConsole.WriteLine(word.Substring(1, 3));", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "ell"
            ),
            new(
                Id: 6,
                Title: "Break It Apart",
                Description: "Given string csv = \"apple,banana,cherry\", split it on commas and print each piece on its own line.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "string csv = \"apple,banana,cherry\";\n// Split on ',' and print each piece\n",
                Example: "string csv = \"apple,banana,cherry\";\nstring[] parts = csv.Split(',');\nforeach (string p in parts)\n{\n    Console.WriteLine(p);\n}",
                Hints: new List<HintTier>
                {
                    new(".Split(',') returns a string[] — one element per piece, with the comma removed."),
                    new("string csv = \"apple,banana,cherry\";\nstring[] parts = csv.Split(',');\nforeach (string p in parts)\n{\n    Console.WriteLine(p);\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "apple", "banana", "cherry")
            ),
            new(
                Id: 7,
                Title: "Find and Swap",
                Description: "Given string msg = \"I like cats\", print a new version with \"cats\" replaced by \"dogs\".",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "string msg = \"I like cats\";\n// Print msg.Replace(\"cats\", \"dogs\")\n",
                Example: "string msg = \"I like cats\";\nConsole.WriteLine(msg.Replace(\"cats\", \"dogs\"));\n// Output: I like dogs",
                Hints: new List<HintTier>
                {
                    new(".Replace(old, new) swaps every occurrence of old with new and returns the result — it doesn't change the original string."),
                    new("string msg = \"I like cats\";\nConsole.WriteLine(msg.Replace(\"cats\", \"dogs\"));", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "I like dogs"
            ),
            new(
                Id: 8,
                Title: "Interpolate It",
                Description: "Given string a = \"Score\" and int b = 42, print \"Score: 42\" using string interpolation ($\"...\").",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "string a = \"Score\";\nint b = 42;\n// Print $\"{a}: {b}\"\n",
                Example: "string a = \"Score\";\nint b = 42;\nConsole.WriteLine($\"{a}: {b}\");\n// Output: Score: 42",
                Hints: new List<HintTier>
                {
                    new("A $\"...\" string lets you embed expressions directly inside {curly braces} — no + concatenation needed."),
                    new("string a = \"Score\";\nint b = 42;\nConsole.WriteLine($\"{a}: {b}\");", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Score: 42"
            ),
            new(
                Id: 9,
                Title: "Ask the String",
                Description: "Given string text = \"hello world\", print whether it Contains(\"world\") and whether it StartsWith(\"hello\").",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "string text = \"hello world\";\n// Print text.Contains(\"world\")\n// Print text.StartsWith(\"hello\")\n",
                Example: "string text = \"hello world\";\nConsole.WriteLine(text.Contains(\"world\"));\nConsole.WriteLine(text.StartsWith(\"hello\"));",
                Hints: new List<HintTier>
                {
                    new("Both .Contains(...) and .StartsWith(...) return a bool — true or false."),
                    new("string text = \"hello world\";\nConsole.WriteLine(text.Contains(\"world\"));\nConsole.WriteLine(text.StartsWith(\"hello\"));", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "True", "True")
            ),
            new(
                Id: 10,
                Title: "Build It Efficiently",
                Description: "Use a System.Text.StringBuilder to build the string \"1-2-3-4-5\" by appending numbers 1 through 5 separated by dashes (no dash after the last number), then print it.",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode: "System.Text.StringBuilder sb = new System.Text.StringBuilder();\n// Loop i from 1 to 5, sb.Append(i), and sb.Append(\"-\") when i < 5\n// Print sb.ToString()\n",
                Example: "System.Text.StringBuilder sb = new System.Text.StringBuilder();\nfor (int i = 1; i <= 5; i++)\n{\n    sb.Append(i);\n    if (i < 5) sb.Append(\"-\");\n}\nConsole.WriteLine(sb.ToString());\n// Output: 1-2-3-4-5",
                Hints: new List<HintTier>
                {
                    new("StringBuilder lets you append pieces one at a time without creating a brand-new string on every step, which regular string += does."),
                    new("Only append the dash when i is less than 5 — otherwise you'd get a trailing dash after the last number."),
                    new("System.Text.StringBuilder sb = new System.Text.StringBuilder();\nfor (int i = 1; i <= 5; i++)\n{\n    sb.Append(i);\n    if (i < 5) sb.Append(\"-\");\n}\nConsole.WriteLine(sb.ToString());", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "1-2-3-4-5"
            ),
            new(
                Id: 11,
                Title: "🎮 Visual: Word Blocks",
                Description:
                    "Given string sentence2 = \"the sky is blue today\", split it into words. Starting at x = 20, loop over the " +
                    "words: for each one, call Canvas.AddShape(word, \"rect\", x, 100, word.Length * 10) (the block's size scales " +
                    "with the word's length), then add 60 to x before the next word. Afterward print \"Placed <count> words.\"",
                Difficulty: Difficulty.Hard,
                Points: 30,
                StarterCode:
                    "string sentence2 = \"the sky is blue today\";\n" +
                    "string[] words = sentence2.Split(' ');\n" +
                    "int x = 20;\n" +
                    "// Loop over words:\n" +
                    "//   Canvas.AddShape(w, \"rect\", x, 100, w.Length * 10)\n" +
                    "//   x += 60\n" +
                    "// Print $\"Placed {words.Length} words.\"\n",
                Example:
                    "string sentence2 = \"the sky is blue today\";\n" +
                    "string[] words = sentence2.Split(' ');\n" +
                    "int x = 20;\n" +
                    "foreach (string w in words)\n" +
                    "{\n" +
                    "    Canvas.AddShape(w, \"rect\", x, 100, w.Length * 10);\n" +
                    "    x += 60;\n" +
                    "}\n" +
                    "Console.WriteLine($\"Placed {words.Length} words.\");",
                Hints: new List<HintTier>
                {
                    new("Split(' ') turns the sentence into a string[] of words — loop over it the same way you'd loop over any array."),
                    new("Each word's own .Length drives the size argument, so longer words become bigger blocks."),
                    new(
                        "string sentence2 = \"the sky is blue today\";\n" +
                        "string[] words = sentence2.Split(' ');\n" +
                        "int x = 20;\n" +
                        "foreach (string w in words)\n" +
                        "{\n" +
                        "    Canvas.AddShape(w, \"rect\", x, 100, w.Length * 10);\n" +
                        "    x += 60;\n" +
                        "}\n" +
                        "Console.WriteLine($\"Placed {words.Length} words.\");",
                        IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Placed 5 words.",
                CheckSource: source => source.Contains("Canvas.AddShape") && source.Contains("Split")
            ),
            new(
                Id: 3,
                Title: "🎮 Visual: Name Tag",
                Description:
                    "Create string name = \"ada\". Convert it to uppercase, then call Canvas.AddShape(\"tag\", \"rect\", 100, 100, " +
                    "name.Length * 20) so the tag's size scales with the name's length. Print the uppercase name.",
                Difficulty: Difficulty.Medium,
                Points: 15,
                StarterCode: "// Create string name = \"ada\", convert to uppercase\n// Canvas.AddShape(\"tag\", \"rect\", 100, 100, name.Length * 20)\n// Print the uppercase name\n",
                Example: "string name = \"ada\";\nname = name.ToUpper();\nCanvas.AddShape(\"tag\", \"rect\", 100, 100, name.Length * 20);\nConsole.WriteLine(name);",
                Hints: new List<HintTier>
                {
                    new("Convert name to uppercase first and store it back, so both the shape's size and the printed text use the updated value."),
                    new("string name = \"ada\";\nname = name.ToUpper();\nCanvas.AddShape(\"tag\", \"rect\", 100, 100, name.Length * 20);\nConsole.WriteLine(name);", IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "ADA",
                CheckSource: source => source.Contains("Canvas.AddShape") && source.Contains("ToUpper")
            )
        },
        QuizStages: new List<QuizStage>
        {
            new(
                Title: "Strings Check-In",
                AfterTaskId: 6,
                QuestionsPerAttempt: 5,
                Cards: new List<QuizCard>
                {
                    new("What does `.ToUpper()` do to a string?", new List<string> { "Removes spaces", "Converts it to uppercase", "Reverses it", "Measures its length" }, CorrectIndex: 1),
                    new("What does `.Trim()` remove from a string?", new List<string> { "Vowels", "Leading/trailing whitespace", "Numbers", "Punctuation" }, CorrectIndex: 1),
                    new("What does `.Length` give you for a string?", new List<string> { "Its first character", "The number of characters", "Whether it's empty", "Its uppercase version" }, CorrectIndex: 1),
                    new("What does `.Substring(0, 3)` return?", new List<string> { "The last 3 characters", "The first 3 characters", "Every 3rd character", "The string reversed" }, CorrectIndex: 1),
                    new("What does `.Split(\" \")` do to a sentence?", new List<string> { "Joins words together", "Breaks it into an array of words", "Removes all spaces", "Capitalizes each word" }, CorrectIndex: 1),
                    new("What does `.Replace(\"a\", \"b\")` do?", new List<string> { "Deletes all a's", "Swaps every a for a b", "Counts the a's", "Adds a b after every a" }, CorrectIndex: 1),
                    new("What's a more efficient way to build a long string piece by piece in a loop than `+=`?", new List<string> { "StringBuilder", "Array.Sort", "Console.Write", "Substring"}, CorrectIndex: 0)
                }
            ),
            new(
                Title: "Strings Mastery",
                AfterTaskId: 11,
                Format: QuizFormat.SwipeTrueFalse,
                QuestionsPerAttempt: 10,
                Cards: new List<QuizCard>
                {
                    new("Strings in C# are indexed starting at 0, like arrays.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`.ToUpper()` changes the original string variable in place.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("`.Contains(\"cat\")` tells you whether \"cat\" appears anywhere in the string.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`.Split()` turns a single string into an array of smaller strings.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("String interpolation with `$\"...\"` can call methods inside the curly braces, like `{name.ToUpper()}`.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`.Trim()` removes whitespace from the middle of a string, not just the ends.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("Strings are immutable in C# — methods like `.Replace()` return a new string rather than modifying the original.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`StringBuilder` is generally faster than repeated `+=` when building a string across many loop iterations.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`.IndexOf(\"x\")` returns -1 if \"x\" is not found in the string.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`.Length` on a string counts words, not characters.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("You can compare two strings for equality using `==`.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`.ToLower()` and `.ToUpper()` are useful for case-insensitive comparisons.", new List<string> { "True", "False" }, CorrectIndex: 0)
                }
            )
        }
    );
}
