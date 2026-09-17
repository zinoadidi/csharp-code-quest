namespace app.Content;

/// <summary>
/// Level 10 — Collections &amp; LINQ.
///
/// Dictionary, HashSet, and the core LINQ operators (Where/Select/OrderBy/Sum/
/// Average/Max). Closes with a LINQ-filtered, price-sorted item list drawn to
/// the canvas as a row of bars.
/// </summary>
public static class Level10_CollectionsAndLinq
{
    public static readonly Level Instance = new(
        Id: 10,
        Name: "Collections & LINQ",
        Description: "Store data by key, keep only unique values, and let LINQ filter, transform, and summarize lists for you.",
        Tasks: new List<GameTask>
        {
            GameTask.FlashcardDeck(
                new GlossaryTerm("Dictionary", "A collection that stores values under unique keys instead of numeric positions — like a real dictionary mapping words to definitions."),
                new GlossaryTerm("Key-Value Pair", "One entry in a dictionary — a key (like a name) paired with its value (like that person's score)."),
                new GlossaryTerm("HashSet", "A collection that stores only unique values — adding the same value twice has no effect, since duplicates are automatically rejected."),
                new GlossaryTerm("LINQ", "Language-Integrated Query — a toolkit built into C# for filtering, transforming, and summarizing collections without writing manual loops."),
                new GlossaryTerm("Lambda Expression", "A tiny, unnamed function written inline, often as x => x * 2 — LINQ methods use these to say what to do with each item."),
                new GlossaryTerm("Where (Filter)", "A LINQ method that keeps only the items in a collection matching a condition, throwing the rest away."),
                new GlossaryTerm("Select (Projection)", "A LINQ method that transforms every item in a collection into something else, like turning a list of People into just their Names."),
                new GlossaryTerm("OrderBy", "A LINQ method that sorts a collection by some value you choose, from smallest/earliest to largest/latest by default."),
                new GlossaryTerm("Aggregate", "Combining every item in a collection down into a single result, like Sum(), Count(), or Max() — LINQ's word for \"boil it all down to one number.\""),
                new GlossaryTerm("Query", "A description of what data you want and how to shape it — a chain of LINQ methods like .Where(...).OrderBy(...) reads almost like a sentence describing a query.")),
            new(
                Id: 1,
                Title: "Look It Up by Name",
                Description: "Create a Dictionary<string, int> called ages. Set ages[\"Ada\"] = 36 and ages[\"Sam\"] = 29. Print both values.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "Dictionary<string, int> ages = new Dictionary<string, int>();\n// Set ages[\"Ada\"] = 36 and ages[\"Sam\"] = 29\n// Print both values\n",
                Example: "Dictionary<string, int> ages = new Dictionary<string, int>();\nages[\"Ada\"] = 36;\nages[\"Sam\"] = 29;\nConsole.WriteLine(ages[\"Ada\"]);\nConsole.WriteLine(ages[\"Sam\"]);",
                Hints: new List<HintTier>
                {
                    new("A Dictionary<TKey, TValue> maps keys to values — `dict[key] = value` sets it, `dict[key]` reads it back."),
                    new("Dictionary<string, int> ages = new Dictionary<string, int>();\nages[\"Ada\"] = 36;\nages[\"Sam\"] = 29;\nConsole.WriteLine(ages[\"Ada\"]);\nConsole.WriteLine(ages[\"Sam\"]);", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "36", "29")
            ),
            new(
                Id: 2,
                Title: "Check Before You Look",
                Description: "Given Dictionary<string,int> scores = { {\"Alice\",90}, {\"Bob\",75} }, use ContainsKey to print scores[\"Alice\"] if it exists, and print \"No Carl\" if \"Carl\" is not a key.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "Dictionary<string, int> scores = new Dictionary<string, int> { { \"Alice\", 90 }, { \"Bob\", 75 } };\n// If scores contains \"Alice\", print scores[\"Alice\"]\n// If scores does NOT contain \"Carl\", print \"No Carl\"\n",
                Example: "Dictionary<string, int> scores = new Dictionary<string, int> { { \"Alice\", 90 }, { \"Bob\", 75 } };\nif (scores.ContainsKey(\"Alice\"))\n{\n    Console.WriteLine(scores[\"Alice\"]);\n}\nif (!scores.ContainsKey(\"Carl\"))\n{\n    Console.WriteLine(\"No Carl\");\n}",
                Hints: new List<HintTier>
                {
                    new("Looking up a missing key directly (`scores[\"Carl\"]`) throws an exception — always check `ContainsKey` first when you're not sure the key is there."),
                    new("Dictionary<string, int> scores = new Dictionary<string, int> { { \"Alice\", 90 }, { \"Bob\", 75 } };\nif (scores.ContainsKey(\"Alice\"))\n{\n    Console.WriteLine(scores[\"Alice\"]);\n}\nif (!scores.ContainsKey(\"Carl\"))\n{\n    Console.WriteLine(\"No Carl\");\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "90", "No Carl")
            ),
            new(
                Id: 4,
                Title: "Walk Every Pair",
                Description: "Given Dictionary<string,string> capitals = { {\"France\",\"Paris\"}, {\"Japan\",\"Tokyo\"} }, loop over it with foreach and a KeyValuePair, printing \"<Key> -> <Value>\" for each.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "Dictionary<string, string> capitals = new Dictionary<string, string> { { \"France\", \"Paris\" }, { \"Japan\", \"Tokyo\" } };\n// foreach (KeyValuePair<string, string> kv in capitals) print \"<kv.Key> -> <kv.Value>\"\n",
                Example: "Dictionary<string, string> capitals = new Dictionary<string, string> { { \"France\", \"Paris\" }, { \"Japan\", \"Tokyo\" } };\nforeach (KeyValuePair<string, string> kv in capitals)\n{\n    Console.WriteLine($\"{kv.Key} -> {kv.Value}\");\n}",
                Hints: new List<HintTier>
                {
                    new("Iterating a Dictionary gives you KeyValuePair<TKey,TValue> items, each with a `.Key` and a `.Value`."),
                    new("Dictionary<string, string> capitals = new Dictionary<string, string> { { \"France\", \"Paris\" }, { \"Japan\", \"Tokyo\" } };\nforeach (KeyValuePair<string, string> kv in capitals)\n{\n    Console.WriteLine($\"{kv.Key} -> {kv.Value}\");\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "France -> Paris", "Japan -> Tokyo")
            ),
            new(
                Id: 5,
                Title: "No Duplicates Allowed",
                Description: "Create a HashSet<int> and Add 1, 2, 2, then 3 to it. Print its Count (duplicates are silently ignored).",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "HashSet<int> uniqueNums = new HashSet<int>();\n// Add 1, 2, 2, 3 - then print uniqueNums.Count\n",
                Example: "HashSet<int> uniqueNums = new HashSet<int>();\nuniqueNums.Add(1);\nuniqueNums.Add(2);\nuniqueNums.Add(2);\nuniqueNums.Add(3);\nConsole.WriteLine(uniqueNums.Count);\n// Output: 3",
                Hints: new List<HintTier>
                {
                    new("A HashSet<T> can never contain the same value twice — adding a value that's already there does nothing."),
                    new("HashSet<int> uniqueNums = new HashSet<int>();\nuniqueNums.Add(1);\nuniqueNums.Add(2);\nuniqueNums.Add(2);\nuniqueNums.Add(3);\nConsole.WriteLine(uniqueNums.Count);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "3"
            ),
            new(
                Id: 6,
                Title: "Filter With Where",
                Description: "Given a List<int> of 1 through 10, use LINQ's Where to keep only the even numbers, then print each one on its own line.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };\n// Use numbers.Where(n => n % 2 == 0).ToList() to get evens, print each\n",
                Example: "List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };\nList<int> evens = numbers.Where(n => n % 2 == 0).ToList();\nforeach (int n in evens)\n{\n    Console.WriteLine(n);\n}",
                Hints: new List<HintTier>
                {
                    new("`Where(condition)` keeps only the items where the lambda returns true. `.ToList()` turns the LINQ result back into a normal List<int> you can loop over."),
                    new("List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };\nList<int> evens = numbers.Where(n => n % 2 == 0).ToList();\nforeach (int n in evens)\n{\n    Console.WriteLine(n);\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "2", "4", "6", "8", "10")
            ),
            new(
                Id: 7,
                Title: "Transform With Select",
                Description: "Given a List<int> { 1, 2, 3, 4 }, use LINQ's Select to double every number, then print each result on its own line.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "List<int> numbers = new List<int> { 1, 2, 3, 4 };\n// Use numbers.Select(n => n * 2).ToList() to double, print each\n",
                Example: "List<int> numbers = new List<int> { 1, 2, 3, 4 };\nList<int> doubled = numbers.Select(n => n * 2).ToList();\nforeach (int n in doubled)\n{\n    Console.WriteLine(n);\n}",
                Hints: new List<HintTier>
                {
                    new("`Select(transform)` produces a new sequence by running the lambda over every item — unlike Where, it doesn't remove items, it changes them."),
                    new("List<int> numbers = new List<int> { 1, 2, 3, 4 };\nList<int> doubled = numbers.Select(n => n * 2).ToList();\nforeach (int n in doubled)\n{\n    Console.WriteLine(n);\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "2", "4", "6", "8")
            ),
            new(
                Id: 8,
                Title: "Put It In Order",
                Description: "Given a List<int> { 5, 3, 8, 1, 9 }, use LINQ's OrderBy to sort it ascending, then print each number on its own line.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "List<int> numbers = new List<int> { 5, 3, 8, 1, 9 };\n// Use numbers.OrderBy(n => n).ToList() to sort ascending, print each\n",
                Example: "List<int> numbers = new List<int> { 5, 3, 8, 1, 9 };\nList<int> sorted = numbers.OrderBy(n => n).ToList();\nforeach (int n in sorted)\n{\n    Console.WriteLine(n);\n}",
                Hints: new List<HintTier>
                {
                    new("`OrderBy(key)` sorts ascending by whatever the lambda returns; for plain numbers, `n => n` sorts by the number itself."),
                    new("List<int> numbers = new List<int> { 5, 3, 8, 1, 9 };\nList<int> sorted = numbers.OrderBy(n => n).ToList();\nforeach (int n in sorted)\n{\n    Console.WriteLine(n);\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "1", "3", "5", "8", "9")
            ),
            new(
                Id: 9,
                Title: "Chain It Together",
                Description: "Given List<string> words = { \"kiwi\", \"apple\", \"fig\", \"banana\" }, chain Where(length > 3) with OrderBy to keep only longer words, sorted alphabetically, then print each.",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode: "List<string> words = new List<string> { \"kiwi\", \"apple\", \"fig\", \"banana\" };\n// Chain .Where(w => w.Length > 3).OrderBy(w => w).ToList(), print each\n",
                Example: "List<string> words = new List<string> { \"kiwi\", \"apple\", \"fig\", \"banana\" };\nList<string> chained = words.Where(w => w.Length > 3).OrderBy(w => w).ToList();\nforeach (string w in chained)\n{\n    Console.WriteLine(w);\n}",
                Hints: new List<HintTier>
                {
                    new("LINQ methods can be chained: `.Where(...).OrderBy(...)` filters first, then sorts what's left — order of the chain matters for readability, not for the final result here."),
                    new("List<string> words = new List<string> { \"kiwi\", \"apple\", \"fig\", \"banana\" };\nList<string> chained = words.Where(w => w.Length > 3).OrderBy(w => w).ToList();\nforeach (string w in chained)\n{\n    Console.WriteLine(w);\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "apple", "banana", "kiwi")
            ),
            new(
                Id: 10,
                Title: "Summarize a List",
                Description: "Given List<int> numbers = { 4, 8, 15, 16, 23, 42 }, print numbers.Sum(), numbers.Average(), and numbers.Max(), each on its own line.",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode: "List<int> numbers = new List<int> { 4, 8, 15, 16, 23, 42 };\n// Print numbers.Sum(), numbers.Average(), numbers.Max()\n",
                Example: "List<int> numbers = new List<int> { 4, 8, 15, 16, 23, 42 };\nint total = numbers.Sum();\ndouble avg = numbers.Average();\nint max = numbers.Max();\nConsole.WriteLine(total);\nConsole.WriteLine(avg);\nConsole.WriteLine(max);\n// Output: 108 / 18 / 42",
                Hints: new List<HintTier>
                {
                    new("LINQ has built-in aggregate methods: Sum(), Average(), Max(), Min(), Count() — no manual loop required."),
                    new("List<int> numbers = new List<int> { 4, 8, 15, 16, 23, 42 };\nint total = numbers.Sum();\ndouble avg = numbers.Average();\nint max = numbers.Max();\nConsole.WriteLine(total);\nConsole.WriteLine(avg);\nConsole.WriteLine(max);", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "108", "18", "42")
            ),
            new(
                Id: 11,
                Title: "🎮 Visual: Loot Display",
                Description:
                    "Define class Item with Name, Category, Price, and a constructor. Build a List<Item> of 5 items across " +
                    "categories \"weapon\", \"armor\", and \"consumable\". Use LINQ to filter to only \"weapon\" items, order them " +
                    "by Price descending, then draw each one as a rect on the canvas (size = Price, x increasing by 80 each " +
                    "time, starting at 20, y = 100). Print \"Displayed <count> weapons.\"",
                Difficulty: Difficulty.Hard,
                Points: 35,
                StarterCode:
                    "class Item\n" +
                    "{\n" +
                    "    public string Name;\n" +
                    "    public string Category;\n" +
                    "    public int Price;\n" +
                    "    public Item(string name, string category, int price)\n" +
                    "    {\n" +
                    "        Name = name; Category = category; Price = price;\n" +
                    "    }\n" +
                    "}\n" +
                    "List<Item> items = new List<Item>\n" +
                    "{\n" +
                    "    new Item(\"Sword\", \"weapon\", 50),\n" +
                    "    new Item(\"Shield\", \"armor\", 40),\n" +
                    "    new Item(\"Potion\", \"consumable\", 10),\n" +
                    "    new Item(\"Bow\", \"weapon\", 35),\n" +
                    "    new Item(\"Helmet\", \"armor\", 20),\n" +
                    "};\n" +
                    "// Filter to \"weapon\" items, OrderByDescending Price\n" +
                    "// Draw each as Canvas.AddShape(item.Name, \"rect\", x, 100, item.Price), x starting at 20 stepping by 80\n" +
                    "// Print $\"Displayed {weapons.Count} weapons.\"\n",
                Example:
                    "class Item\n" +
                    "{\n" +
                    "    public string Name;\n" +
                    "    public string Category;\n" +
                    "    public int Price;\n" +
                    "    public Item(string name, string category, int price)\n" +
                    "    {\n" +
                    "        Name = name; Category = category; Price = price;\n" +
                    "    }\n" +
                    "}\n" +
                    "List<Item> items = new List<Item>\n" +
                    "{\n" +
                    "    new Item(\"Sword\", \"weapon\", 50),\n" +
                    "    new Item(\"Shield\", \"armor\", 40),\n" +
                    "    new Item(\"Potion\", \"consumable\", 10),\n" +
                    "    new Item(\"Bow\", \"weapon\", 35),\n" +
                    "    new Item(\"Helmet\", \"armor\", 20),\n" +
                    "};\n" +
                    "List<Item> weapons = items.Where(i => i.Category == \"weapon\").OrderByDescending(i => i.Price).ToList();\n" +
                    "double x = 20;\n" +
                    "foreach (Item w in weapons)\n" +
                    "{\n" +
                    "    Canvas.AddShape(w.Name, \"rect\", x, 100, w.Price);\n" +
                    "    x += 80;\n" +
                    "}\n" +
                    "Console.WriteLine($\"Displayed {weapons.Count} weapons.\");",
                Hints: new List<HintTier>
                {
                    new("Filter with Where, then sort the filtered result with OrderByDescending — chaining works the same way it did with words earlier in this level."),
                    new("Each iteration of the foreach needs its own Canvas.AddShape call, and x needs to grow between calls so the bars don't stack on top of each other."),
                    new(
                        "class Item\n" +
                        "{\n" +
                        "    public string Name;\n" +
                        "    public string Category;\n" +
                        "    public int Price;\n" +
                        "    public Item(string name, string category, int price)\n" +
                        "    {\n" +
                        "        Name = name; Category = category; Price = price;\n" +
                        "    }\n" +
                        "}\n" +
                        "List<Item> items = new List<Item>\n" +
                        "{\n" +
                        "    new Item(\"Sword\", \"weapon\", 50),\n" +
                        "    new Item(\"Shield\", \"armor\", 40),\n" +
                        "    new Item(\"Potion\", \"consumable\", 10),\n" +
                        "    new Item(\"Bow\", \"weapon\", 35),\n" +
                        "    new Item(\"Helmet\", \"armor\", 20),\n" +
                        "};\n" +
                        "List<Item> weapons = items.Where(i => i.Category == \"weapon\").OrderByDescending(i => i.Price).ToList();\n" +
                        "double x = 20;\n" +
                        "foreach (Item w in weapons)\n" +
                        "{\n" +
                        "    Canvas.AddShape(w.Name, \"rect\", x, 100, w.Price);\n" +
                        "    x += 80;\n" +
                        "}\n" +
                        "Console.WriteLine($\"Displayed {weapons.Count} weapons.\");",
                        IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Displayed 2 weapons.",
                CheckSource: source => source.Contains("Where") && source.Contains("OrderByDescending") && source.Contains("Canvas.AddShape")
            ),
            new(
                Id: 3,
                Title: "🎮 Visual: Score Bars",
                Description:
                    "Given Dictionary<string, int> scores with \"a\" = 30, \"b\" = 90, \"c\" = 60, loop through the dictionary with " +
                    "foreach and for each entry call Canvas.AddShape(entry.Key, \"rect\", 50, 100, entry.Value) so each bar's size " +
                    "matches its score. Print \"Drew \" + scores.Count + \" bars.\"",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "// Dictionary<string, int> scores with a=30, b=90, c=60\n// foreach entry, Canvas.AddShape(entry.Key, \"rect\", 50, 100, entry.Value)\n// Print \"Drew \" + scores.Count + \" bars.\"\n",
                Example: "Dictionary<string, int> scores = new Dictionary<string, int> { [\"a\"] = 30, [\"b\"] = 90, [\"c\"] = 60 };\nforeach (var entry in scores)\n{\n    Canvas.AddShape(entry.Key, \"rect\", 50, 100, entry.Value);\n}\nConsole.WriteLine(\"Drew \" + scores.Count + \" bars.\");",
                Hints: new List<HintTier>
                {
                    new("A foreach over a Dictionary gives you entry.Key and entry.Value pairs — use both when drawing each bar."),
                    new("Dictionary<string, int> scores = new Dictionary<string, int> { [\"a\"] = 30, [\"b\"] = 90, [\"c\"] = 60 };\nforeach (var entry in scores)\n{\n    Canvas.AddShape(entry.Key, \"rect\", 50, 100, entry.Value);\n}\nConsole.WriteLine(\"Drew \" + scores.Count + \" bars.\");", IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Drew 3 bars.",
                CheckSource: source => source.Contains("Canvas.AddShape") && source.Contains("foreach")
            )
        },
        QuizStages: new List<QuizStage>
        {
            new(
                Title: "LINQ Check-In",
                AfterTaskId: 6,
                QuestionsPerAttempt: 5,
                Cards: new List<QuizCard>
                {
                    new("What is a Dictionary<TKey, TValue> used for?", new List<string> { "Sorting numbers", "Looking up values by a key", "Storing unique unordered values only", "Reversing a string" }, CorrectIndex: 1),
                    new("What does `dict.ContainsKey(x)` check?", new List<string> { "If x is a value in the dictionary", "If x is a key in the dictionary", "If the dictionary is empty", "If x is a duplicate" }, CorrectIndex: 1),
                    new("What does a HashSet<T> guarantee that a List<T> doesn't?", new List<string> { "Order is preserved", "No duplicate elements", "Faster printing", "Fixed size" }, CorrectIndex: 1),
                    new("What does LINQ's `.Where(x => x > 5)` do?", new List<string> { "Sorts elements greater than 5", "Filters to only elements greater than 5", "Counts elements greater than 5", "Removes elements greater than 5" }, CorrectIndex: 1),
                    new("What does `.Select(x => x * 2)` do?", new List<string> { "Filters even numbers", "Transforms each element (doubles it)", "Picks a random element", "Sorts descending" }, CorrectIndex: 1),
                    new("What does `.OrderBy(x => x)` do to a sequence?", new List<string> { "Reverses it", "Sorts it ascending", "Filters duplicates", "Groups it" }, CorrectIndex: 1),
                    new("Can you chain multiple LINQ methods together, like `.Where(...).OrderBy(...).Select(...)`?", new List<string> { "No, only one at a time", "Yes, that's a common pattern", "Only with arrays, not lists", "Only if using a foreach loop" }, CorrectIndex: 1)
                }
            ),
            new(
                Title: "LINQ Mastery",
                AfterTaskId: 11,
                Format: QuizFormat.SwipeTrueFalse,
                QuestionsPerAttempt: 10,
                Cards: new List<QuizCard>
                {
                    new("A Dictionary lets you look up a value quickly using an associated key.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A HashSet<T> can contain duplicate values.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("`.Where()` returns a filtered sequence, not a modified copy of the original.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`.Select()` is used to transform each element of a sequence into something new.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`.OrderByDescending()` sorts a sequence from smallest to largest.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("LINQ methods can be chained together in a single expression.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`.Count()` from LINQ tells you how many elements match a condition.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Dictionaries preserve the exact order items were inserted, guaranteed forever.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("`.Sum()` adds up all the numeric values in a sequence.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("You must always convert a List to an array before using LINQ on it.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("`.Any(x => condition)` tells you whether at least one element matches the condition.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`dict.TryGetValue(key, out value)` is a safe way to look up a key that might not exist.", new List<string> { "True", "False" }, CorrectIndex: 0)
                }
            )
        }
    );
}
