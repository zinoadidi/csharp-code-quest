namespace app.Content;

/// <summary>
/// Level 12 — Capstone.
///
/// The "you can build real things now" payoff level. Mostly mini-games, mixing
/// everything from earlier levels — classes, inheritance, interfaces, Dictionary,
/// LINQ, try/catch — into a guessing game, an inventory tracker, a branching
/// story, a state machine, a wave spawner, an RPG battle, and a full dungeon
/// crawl finale.
/// </summary>
public static class Level12_Capstone
{
    public static readonly Level Instance = new(
        Id: 12,
        Name: "Capstone",
        Description: "Everything you've learned, combined. Build a guessing game, an inventory, a battle, and a dungeon crawl.",
        Tasks: new List<GameTask>
        {
            GameTask.FlashcardDeck(
                new GlossaryTerm("Algorithm", "A step-by-step plan for solving a problem, in an order that always gets the right answer. Code is just an algorithm written in a way a computer can follow."),
                new GlossaryTerm("Refactoring", "Rewriting code to be cleaner or better organized WITHOUT changing what it actually does — same behavior, better structure."),
                new GlossaryTerm("Abstraction", "Hiding complicated details behind a simple interface — you can call Console.WriteLine without knowing how it actually talks to your screen."),
                new GlossaryTerm("Design Pattern", "A proven, reusable solution shape for a problem that comes up again and again in software — a common vocabulary experienced developers share."),
                new GlossaryTerm("Technical Debt", "The extra work created later by choosing a quick, messy solution now — like a real debt, it tends to accumulate interest over time."),
                new GlossaryTerm("Code Review", "Having another person read your code before it ships — catches bugs, spreads knowledge, and keeps a codebase consistent."),
                new GlossaryTerm("Unit Test", "A small, automated check that a single piece of code behaves correctly — run often, so a change that breaks something gets caught immediately."),
                new GlossaryTerm("Version Control", "A system (like Git) that tracks every change to your code over time, so you can see history, undo mistakes, and work with others without overwriting each other."),
                new GlossaryTerm("Deployment", "Taking finished code and actually making it available for real users to use — publishing it, essentially."),
                new GlossaryTerm("Software Engineering", "The whole practice of building software thoughtfully — planning, writing, testing, and maintaining it — not just getting code to technically run once.")),
            new(
                Id: 1,
                Title: "Guess the Number",
                Description: "Given a secret of 42 and a list of guesses {10, 50, 42}, loop through them counting tries, printing \"Too low!\" or \"Too high!\", and on a match print \"Correct! You guessed it in <tries> tries.\" then stop.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "int secret = 42;\nList<int> guesses = new List<int> { 10, 50, 42 };\nint tries = 0;\nforeach (int g in guesses)\n{\n    tries++;\n    // if g < secret print \"Too low!\"\n    // else if g > secret print \"Too high!\"\n    // else print $\"Correct! You guessed it in {tries} tries.\" and break\n}\n",
                Example: "int secret = 42;\nList<int> guesses = new List<int> { 10, 50, 42 };\nint tries = 0;\nforeach (int g in guesses)\n{\n    tries++;\n    if (g < secret) Console.WriteLine(\"Too low!\");\n    else if (g > secret) Console.WriteLine(\"Too high!\");\n    else { Console.WriteLine($\"Correct! You guessed it in {tries} tries.\"); break; }\n}",
                Hints: new List<HintTier>
                {
                    new("This is the classic number-guessing game logic, minus the actual user input — walk through the fixed list of guesses instead."),
                    new("int secret = 42;\nList<int> guesses = new List<int> { 10, 50, 42 };\nint tries = 0;\nforeach (int g in guesses)\n{\n    tries++;\n    if (g < secret) Console.WriteLine(\"Too low!\");\n    else if (g > secret) Console.WriteLine(\"Too high!\");\n    else { Console.WriteLine($\"Correct! You guessed it in {tries} tries.\"); break; }\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "Too low!", "Too high!", "Correct! You guessed it in 3 tries.")
            ),
            new(
                Id: 2,
                Title: "Inventory Tracker",
                Description: "Track a Dictionary<string,int> inventory: add 3 potions, add 1 sword, remove 1 potion, add 1 shield. Print each item as \"<name>: <count>\", sorted alphabetically by name using LINQ.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "Dictionary<string, int> inventory = new Dictionary<string, int>();\ninventory[\"potion\"] = 3;\ninventory[\"sword\"] = 1;\ninventory[\"potion\"] = inventory[\"potion\"] - 1;\ninventory[\"shield\"] = 1;\n// Print each item sorted alphabetically by key: \"<name>: <count>\"\n",
                Example: "Dictionary<string, int> inventory = new Dictionary<string, int>();\ninventory[\"potion\"] = 3;\ninventory[\"sword\"] = 1;\ninventory[\"potion\"] = inventory[\"potion\"] - 1;\ninventory[\"shield\"] = 1;\nforeach (var kv in inventory.OrderBy(kv => kv.Key))\n{\n    Console.WriteLine($\"{kv.Key}: {kv.Value}\");\n}",
                Hints: new List<HintTier>
                {
                    new("You can combine what you learned about Dictionary indexing with LINQ's OrderBy to print entries in a predictable order."),
                    new("Dictionary<string, int> inventory = new Dictionary<string, int>();\ninventory[\"potion\"] = 3;\ninventory[\"sword\"] = 1;\ninventory[\"potion\"] = inventory[\"potion\"] - 1;\ninventory[\"shield\"] = 1;\nforeach (var kv in inventory.OrderBy(kv => kv.Key))\n{\n    Console.WriteLine($\"{kv.Key}: {kv.Value}\");\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "potion: 2", "shield: 1", "sword: 1")
            ),
            new(
                Id: 3,
                Title: "Branch the Story",
                Description: "Given path = \"forest\" and action = \"fight\", write nested if/else: if path is \"forest\" and action is \"fight\" print \"You battled the wolf and won!\"; if path is \"forest\" but action isn't \"fight\" print \"You fled into the trees.\"; otherwise print \"You wandered the plains.\"",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "string path = \"forest\";\nstring action = \"fight\";\nstring result;\n// Nested if/else producing one of the three outcomes, stored in result\nConsole.WriteLine(result);\n",
                Example: "string path = \"forest\";\nstring action = \"fight\";\nstring result;\nif (path == \"forest\")\n{\n    if (action == \"fight\")\n    {\n        result = \"You battled the wolf and won!\";\n    }\n    else\n    {\n        result = \"You fled into the trees.\";\n    }\n}\nelse\n{\n    result = \"You wandered the plains.\";\n}\nConsole.WriteLine(result);",
                Hints: new List<HintTier>
                {
                    new("A text adventure's branching is just nested if/else — one outer decision (which path) and one inner decision (what you do there)."),
                    new("string path = \"forest\";\nstring action = \"fight\";\nstring result;\nif (path == \"forest\")\n{\n    if (action == \"fight\")\n    {\n        result = \"You battled the wolf and won!\";\n    }\n    else\n    {\n        result = \"You fled into the trees.\";\n    }\n}\nelse\n{\n    result = \"You wandered the plains.\";\n}\nConsole.WriteLine(result);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "You battled the wolf and won!"
            ),
            new(
                Id: 4,
                Title: "Character Sheet",
                Description: "Define class Player12 with Name, HP, a constructor, and TakeDamage(int amount) that subtracts amount from HP and clamps it at 0 (never negative). Create Player12(\"Kai\", 30), deal 10 then 25 damage, then print \"<Name>: <HP> HP\".",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "class Player12\n{\n    public string Name;\n    public int HP;\n    public Player12(string name, int hp) { Name = name; HP = hp; }\n    public void TakeDamage(int amount)\n    {\n        HP -= amount;\n        // clamp HP at 0 if it went negative\n    }\n}\nPlayer12 hero = new Player12(\"Kai\", 30);\nhero.TakeDamage(10);\nhero.TakeDamage(25);\nConsole.WriteLine($\"{hero.Name}: {hero.HP} HP\");\n",
                Example: "class Player12\n{\n    public string Name;\n    public int HP;\n    public Player12(string name, int hp) { Name = name; HP = hp; }\n    public void TakeDamage(int amount)\n    {\n        HP -= amount;\n        if (HP < 0) HP = 0;\n    }\n}\nPlayer12 hero = new Player12(\"Kai\", 30);\nhero.TakeDamage(10);\nhero.TakeDamage(25);\nConsole.WriteLine($\"{hero.Name}: {hero.HP} HP\");",
                Hints: new List<HintTier>
                {
                    new("30 - 10 - 25 would be -5, but HP shouldn't go negative — clamp it back to 0 with a simple if check inside TakeDamage."),
                    new("class Player12\n{\n    public string Name;\n    public int HP;\n    public Player12(string name, int hp) { Name = name; HP = hp; }\n    public void TakeDamage(int amount)\n    {\n        HP -= amount;\n        if (HP < 0) HP = 0;\n    }\n}\nPlayer12 hero = new Player12(\"Kai\", 30);\nhero.TakeDamage(10);\nhero.TakeDamage(25);\nConsole.WriteLine($\"{hero.Name}: {hero.HP} HP\");", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Kai: 0 HP"
            ),
            new(
                Id: 5,
                Title: "Monster Roster",
                Description: "Define interface IMonster12 with an Attack() method and a Name property. Implement Goblin12 (Attack returns 5) and Troll12 (Attack returns 12). Put a Goblin12, Troll12, Goblin12 into a List<IMonster12> and use LINQ Sum to print \"Total incoming damage: <n>\".",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode: "interface IMonster12 { int Attack(); string Name { get; } }\nclass Goblin12 : IMonster12 { public string Name => \"Goblin\"; public int Attack() => 5; }\nclass Troll12 : IMonster12 { public string Name => \"Troll\"; public int Attack() => 12; }\nList<IMonster12> monsters = new List<IMonster12> { new Goblin12(), new Troll12(), new Goblin12() };\n// Use LINQ Sum to add up m.Attack() for every monster, then print \"Total incoming damage: <n>\"\n",
                Example: "interface IMonster12 { int Attack(); string Name { get; } }\nclass Goblin12 : IMonster12 { public string Name => \"Goblin\"; public int Attack() => 5; }\nclass Troll12 : IMonster12 { public string Name => \"Troll\"; public int Attack() => 12; }\nList<IMonster12> monsters = new List<IMonster12> { new Goblin12(), new Troll12(), new Goblin12() };\nint totalDamage = monsters.Sum(m => m.Attack());\nConsole.WriteLine($\"Total incoming damage: {totalDamage}\");",
                Hints: new List<HintTier>
                {
                    new("An interface lets you put different monster types in the same List<IMonster12> and call Attack() on each without caring which concrete class it is — that's polymorphism, now paired with LINQ's Sum."),
                    new("interface IMonster12 { int Attack(); string Name { get; } }\nclass Goblin12 : IMonster12 { public string Name => \"Goblin\"; public int Attack() => 5; }\nclass Troll12 : IMonster12 { public string Name => \"Troll\"; public int Attack() => 12; }\nList<IMonster12> monsters = new List<IMonster12> { new Goblin12(), new Troll12(), new Goblin12() };\nint totalDamage = monsters.Sum(m => m.Attack());\nConsole.WriteLine($\"Total incoming damage: {totalDamage}\");", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Total incoming damage: 22",
                CheckSource: source => source.Contains("interface IMonster12") && source.Contains(".Sum(")
            ),
            new(
                Id: 6,
                Title: "Leaderboard",
                Description: "Define class ScoreEntry (Name, Score, constructor). Build a list of Ann=50, Bo=90, Cy=70, Di=30. Use LINQ OrderByDescending + Take(2) to print the top two as \"<Name>: <Score>\".",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "class ScoreEntry { public string Name; public int Score; public ScoreEntry(string n, int s) { Name = n; Score = s; } }\nList<ScoreEntry> entries = new List<ScoreEntry> { new ScoreEntry(\"Ann\", 50), new ScoreEntry(\"Bo\", 90), new ScoreEntry(\"Cy\", 70), new ScoreEntry(\"Di\", 30) };\n// Order entries by Score descending, take the top 2, print \"<Name>: <Score>\" for each\n",
                Example: "class ScoreEntry { public string Name; public int Score; public ScoreEntry(string n, int s) { Name = n; Score = s; } }\nList<ScoreEntry> entries = new List<ScoreEntry> { new ScoreEntry(\"Ann\", 50), new ScoreEntry(\"Bo\", 90), new ScoreEntry(\"Cy\", 70), new ScoreEntry(\"Di\", 30) };\nList<ScoreEntry> top = entries.OrderByDescending(e => e.Score).Take(2).ToList();\nforeach (var e in top)\n{\n    Console.WriteLine($\"{e.Name}: {e.Score}\");\n}",
                Hints: new List<HintTier>
                {
                    new("`OrderByDescending` sorts highest first; `.Take(2)` then keeps only the first two results — a common leaderboard pattern."),
                    new("class ScoreEntry { public string Name; public int Score; public ScoreEntry(string n, int s) { Name = n; Score = s; } }\nList<ScoreEntry> entries = new List<ScoreEntry> { new ScoreEntry(\"Ann\", 50), new ScoreEntry(\"Bo\", 90), new ScoreEntry(\"Cy\", 70), new ScoreEntry(\"Di\", 30) };\nList<ScoreEntry> top = entries.OrderByDescending(e => e.Score).Take(2).ToList();\nforeach (var e in top)\n{\n    Console.WriteLine($\"{e.Name}: {e.Score}\");\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "Bo: 90", "Cy: 70"),
                CheckSource: source => source.Contains("OrderByDescending") && source.Contains("Take(2)")
            ),
            new(
                Id: 7,
                Title: "🎮 Visual: Traffic Light",
                Description:
                    "Define class TrafficLight12 with an Id, a private string[] of states {\"red\",\"yellow\",\"green\"}, a private index starting at 0, a constructor that draws a circle via Canvas.AddShape, and a Next() method that advances index (wrapping with %) and recolors the shape via Canvas.SetColor using a switch expression. " +
                    "Create one light and call Next() 4 times, then print \"Cycled through 4 state changes.\"",
                Difficulty: Difficulty.Hard,
                Points: 30,
                StarterCode:
                    "class TrafficLight12\n" +
                    "{\n" +
                    "    public string Id;\n" +
                    "    private string[] states = { \"red\", \"yellow\", \"green\" };\n" +
                    "    private int index = 0;\n" +
                    "    public TrafficLight12(string id) { Id = id; Canvas.AddShape(Id, \"circle\", 50, 50, 40, \"#ef4444\"); }\n" +
                    "    public void Next()\n" +
                    "    {\n" +
                    "        index = (index + 1) % states.Length;\n" +
                    "        // Build a color string from states[index] (red=#ef4444, yellow=#facc15, green=#22c55e)\n" +
                    "        // then call Canvas.SetColor(Id, color)\n" +
                    "    }\n" +
                    "}\n" +
                    "TrafficLight12 light = new TrafficLight12(\"light1\");\n" +
                    "for (int i = 0; i < 4; i++) light.Next();\n" +
                    "Console.WriteLine(\"Cycled through 4 state changes.\");\n",
                Example:
                    "class TrafficLight12\n" +
                    "{\n" +
                    "    public string Id;\n" +
                    "    private string[] states = { \"red\", \"yellow\", \"green\" };\n" +
                    "    private int index = 0;\n" +
                    "    public TrafficLight12(string id) { Id = id; Canvas.AddShape(Id, \"circle\", 50, 50, 40, \"#ef4444\"); }\n" +
                    "    public void Next()\n" +
                    "    {\n" +
                    "        index = (index + 1) % states.Length;\n" +
                    "        string color = states[index] switch { \"red\" => \"#ef4444\", \"yellow\" => \"#facc15\", \"green\" => \"#22c55e\", _ => \"#ffffff\" };\n" +
                    "        Canvas.SetColor(Id, color);\n" +
                    "    }\n" +
                    "}\n" +
                    "TrafficLight12 light = new TrafficLight12(\"light1\");\n" +
                    "for (int i = 0; i < 4; i++) light.Next();\n" +
                    "Console.WriteLine(\"Cycled through 4 state changes.\");",
                Hints: new List<HintTier>
                {
                    new("This is a state machine: `index` tracks which state you're in, and `% states.Length` wraps it back to 0 after the last state — a switch expression maps each state name to a color."),
                    new(
                        "class TrafficLight12\n" +
                        "{\n" +
                        "    public string Id;\n" +
                        "    private string[] states = { \"red\", \"yellow\", \"green\" };\n" +
                        "    private int index = 0;\n" +
                        "    public TrafficLight12(string id) { Id = id; Canvas.AddShape(Id, \"circle\", 50, 50, 40, \"#ef4444\"); }\n" +
                        "    public void Next()\n" +
                        "    {\n" +
                        "        index = (index + 1) % states.Length;\n" +
                        "        string color = states[index] switch { \"red\" => \"#ef4444\", \"yellow\" => \"#facc15\", \"green\" => \"#22c55e\", _ => \"#ffffff\" };\n" +
                        "        Canvas.SetColor(Id, color);\n" +
                        "    }\n" +
                        "}\n" +
                        "TrafficLight12 light = new TrafficLight12(\"light1\");\n" +
                        "for (int i = 0; i < 4; i++) light.Next();\n" +
                        "Console.WriteLine(\"Cycled through 4 state changes.\");",
                        IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Cycled through 4 state changes.",
                CheckSource: source => source.Contains("class TrafficLight12") && source.Contains("Canvas.SetColor") && source.Contains("switch")
            ),
            new(
                Id: 8,
                Title: "🎮 Visual: Wave Spawner",
                Description:
                    "Define class Enemy12 whose constructor draws a circle via Canvas.AddShape at a given x,y. Use a nested loop — 3 waves, 2 enemies per wave — to create and collect enemies into a List<Enemy12>, spacing them out with the wave and enemy index, then print \"Spawned <n> enemies across 3 waves.\"",
                Difficulty: Difficulty.Hard,
                Points: 30,
                StarterCode:
                    "class Enemy12 { public string Id; public Enemy12(string id, double x, double y) { Id = id; Canvas.AddShape(Id, \"circle\", x, y, 20, \"#f87171\"); } }\n" +
                    "List<Enemy12> allEnemies = new List<Enemy12>();\n" +
                    "int spawnedCount = 0;\n" +
                    "// Nested loop: wave from 0 to 2, i from 0 to 1\n" +
                    "// id = $\"w{wave}_e{i}\", x = 30 + i * 60, y = 30 + wave * 60\n" +
                    "// create the Enemy12, add it to allEnemies, and increment spawnedCount\n" +
                    "Console.WriteLine($\"Spawned {spawnedCount} enemies across 3 waves.\");\n",
                Example:
                    "class Enemy12 { public string Id; public Enemy12(string id, double x, double y) { Id = id; Canvas.AddShape(Id, \"circle\", x, y, 20, \"#f87171\"); } }\n" +
                    "List<Enemy12> allEnemies = new List<Enemy12>();\n" +
                    "int spawnedCount = 0;\n" +
                    "for (int wave = 0; wave < 3; wave++)\n" +
                    "{\n" +
                    "    for (int i = 0; i < 2; i++)\n" +
                    "    {\n" +
                    "        string id = $\"w{wave}_e{i}\";\n" +
                    "        double x = 30 + i * 60;\n" +
                    "        double y = 30 + wave * 60;\n" +
                    "        Enemy12 enemy = new Enemy12(id, x, y);\n" +
                    "        allEnemies.Add(enemy);\n" +
                    "        spawnedCount++;\n" +
                    "    }\n" +
                    "}\n" +
                    "Console.WriteLine($\"Spawned {spawnedCount} enemies across 3 waves.\");",
                Hints: new List<HintTier>
                {
                    new("The outer loop picks the wave, the inner loop spawns each enemy in that wave — the same nested-loop shape as a multiplication table, just spawning objects instead of printing numbers."),
                    new(
                        "class Enemy12 { public string Id; public Enemy12(string id, double x, double y) { Id = id; Canvas.AddShape(Id, \"circle\", x, y, 20, \"#f87171\"); } }\n" +
                        "List<Enemy12> allEnemies = new List<Enemy12>();\n" +
                        "int spawnedCount = 0;\n" +
                        "for (int wave = 0; wave < 3; wave++)\n" +
                        "{\n" +
                        "    for (int i = 0; i < 2; i++)\n" +
                        "    {\n" +
                        "        string id = $\"w{wave}_e{i}\";\n" +
                        "        double x = 30 + i * 60;\n" +
                        "        double y = 30 + wave * 60;\n" +
                        "        Enemy12 enemy = new Enemy12(id, x, y);\n" +
                        "        allEnemies.Add(enemy);\n" +
                        "        spawnedCount++;\n" +
                        "    }\n" +
                        "}\n" +
                        "Console.WriteLine($\"Spawned {spawnedCount} enemies across 3 waves.\");",
                        IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Spawned 6 enemies across 3 waves.",
                CheckSource: source => source.Contains("class Enemy12") && source.Contains("Canvas.AddShape") && source.Contains("for (int wave")
            ),
            new(
                Id: 9,
                Title: "🎮 Visual: RPG Battle",
                Description:
                    "Define abstract class Character12 (Id, HP, a constructor drawing a rect via Canvas.AddShape, abstract AttackPower(), and TakeDamage(int) that clamps HP at 0 and recolors via Canvas.SetColor). Subclass Hero12 (AttackPower 15) and Villain12 (AttackPower 10). " +
                    "Loop rounds while both are alive: the hero attacks the villain, then (if the villain survives) the villain attacks the hero. Print \"Battle ended after <rounds> rounds. Winner: <hero|villain>\".",
                Difficulty: Difficulty.Hard,
                Points: 35,
                StarterCode:
                    "abstract class Character12\n" +
                    "{\n" +
                    "    public string Id;\n" +
                    "    public int HP;\n" +
                    "    protected Character12(string id, int hp) { Id = id; HP = hp; Canvas.AddShape(Id, \"rect\", 20, 20, 30); }\n" +
                    "    public abstract int AttackPower();\n" +
                    "    public void TakeDamage(int amount)\n" +
                    "    {\n" +
                    "        HP -= amount;\n" +
                    "        // clamp HP at 0, then Canvas.SetColor(Id, ...) — gray if defeated, red otherwise\n" +
                    "    }\n" +
                    "}\n" +
                    "class Hero12 : Character12 { public Hero12(string id, int hp) : base(id, hp) { } public override int AttackPower() => 15; }\n" +
                    "class Villain12 : Character12 { public Villain12(string id, int hp) : base(id, hp) { } public override int AttackPower() => 10; }\n" +
                    "\n" +
                    "Hero12 hero = new Hero12(\"hero\", 40);\n" +
                    "Villain12 villain = new Villain12(\"villain\", 35);\n" +
                    "int round = 0;\n" +
                    "// while both are alive: round++, villain takes hero's damage, break if villain is down,\n" +
                    "// otherwise hero takes villain's damage\n" +
                    "string winner = hero.HP > 0 ? \"hero\" : \"villain\";\n" +
                    "Console.WriteLine($\"Battle ended after {round} rounds. Winner: {winner}\");\n",
                Example:
                    "abstract class Character12\n" +
                    "{\n" +
                    "    public string Id;\n" +
                    "    public int HP;\n" +
                    "    protected Character12(string id, int hp) { Id = id; HP = hp; Canvas.AddShape(Id, \"rect\", 20, 20, 30); }\n" +
                    "    public abstract int AttackPower();\n" +
                    "    public void TakeDamage(int amount)\n" +
                    "    {\n" +
                    "        HP -= amount;\n" +
                    "        if (HP < 0) HP = 0;\n" +
                    "        Canvas.SetColor(Id, HP == 0 ? \"#6b7280\" : \"#f87171\");\n" +
                    "    }\n" +
                    "}\n" +
                    "class Hero12 : Character12 { public Hero12(string id, int hp) : base(id, hp) { } public override int AttackPower() => 15; }\n" +
                    "class Villain12 : Character12 { public Villain12(string id, int hp) : base(id, hp) { } public override int AttackPower() => 10; }\n" +
                    "\n" +
                    "Hero12 hero = new Hero12(\"hero\", 40);\n" +
                    "Villain12 villain = new Villain12(\"villain\", 35);\n" +
                    "int round = 0;\n" +
                    "while (hero.HP > 0 && villain.HP > 0)\n" +
                    "{\n" +
                    "    round++;\n" +
                    "    villain.TakeDamage(hero.AttackPower());\n" +
                    "    if (villain.HP <= 0) break;\n" +
                    "    hero.TakeDamage(villain.AttackPower());\n" +
                    "}\n" +
                    "string winner = hero.HP > 0 ? \"hero\" : \"villain\";\n" +
                    "Console.WriteLine($\"Battle ended after {round} rounds. Winner: {winner}\");",
                Hints: new List<HintTier>
                {
                    new("Trace it by hand: villain starts at 35 HP and takes 15 damage per round from the hero — after how many hero attacks does villain's HP drop to 0 or below?"),
                    new("Villain: 35 → 20 → 5 → -10 (clamped to 0) after 3 hero attacks. Hero only gets hit twice (rounds 1 and 2) before the villain goes down in round 3, so hero ends at 40 - 10 - 10 = 20 HP — hero wins."),
                    new(
                        "abstract class Character12\n" +
                        "{\n" +
                        "    public string Id;\n" +
                        "    public int HP;\n" +
                        "    protected Character12(string id, int hp) { Id = id; HP = hp; Canvas.AddShape(Id, \"rect\", 20, 20, 30); }\n" +
                        "    public abstract int AttackPower();\n" +
                        "    public void TakeDamage(int amount)\n" +
                        "    {\n" +
                        "        HP -= amount;\n" +
                        "        if (HP < 0) HP = 0;\n" +
                        "        Canvas.SetColor(Id, HP == 0 ? \"#6b7280\" : \"#f87171\");\n" +
                        "    }\n" +
                        "}\n" +
                        "class Hero12 : Character12 { public Hero12(string id, int hp) : base(id, hp) { } public override int AttackPower() => 15; }\n" +
                        "class Villain12 : Character12 { public Villain12(string id, int hp) : base(id, hp) { } public override int AttackPower() => 10; }\n" +
                        "\n" +
                        "Hero12 hero = new Hero12(\"hero\", 40);\n" +
                        "Villain12 villain = new Villain12(\"villain\", 35);\n" +
                        "int round = 0;\n" +
                        "while (hero.HP > 0 && villain.HP > 0)\n" +
                        "{\n" +
                        "    round++;\n" +
                        "    villain.TakeDamage(hero.AttackPower());\n" +
                        "    if (villain.HP <= 0) break;\n" +
                        "    hero.TakeDamage(villain.AttackPower());\n" +
                        "}\n" +
                        "string winner = hero.HP > 0 ? \"hero\" : \"villain\";\n" +
                        "Console.WriteLine($\"Battle ended after {round} rounds. Winner: {winner}\");",
                        IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Battle ended after 3 rounds. Winner: hero",
                CheckSource: source => source.Contains("abstract class Character12") && source.Contains("override int AttackPower") && source.Contains("Canvas.SetColor")
            ),
            new(
                Id: 10,
                Title: "🎮 Visual: Dungeon Crawl Finale",
                Description:
                    "The final challenge — combine everything. Define interface IMonster12b (Name, Power) and class DungeonMonster implementing it, and class Room12 (Id, Monster, Item). Build a 4-room dungeon: r1 has a potion, r2 has a Rat (power 5), r3 has an Ogre (power 40) and a shield, r4 has gold. " +
                    "Starting at 50 HP with an empty Dictionary<string,int> inventory, walk the rooms: move the player's Canvas shape forward each room, fight any monster inside a try/catch (throw an InvalidOperationException if the monster's power would drop your HP to 0 or below), and collect any item into the inventory. " +
                    "If defeated, print the exception message and stop. Otherwise print \"Cleared <n> rooms with <hp> HP remaining and <items> items collected.\" once all rooms are done.",
                Difficulty: Difficulty.Hard,
                Points: 40,
                StarterCode:
                    "interface IMonster12b { string Name { get; } int Power { get; } }\n" +
                    "class DungeonMonster : IMonster12b { public string Name { get; } public int Power { get; } public DungeonMonster(string name, int power) { Name = name; Power = power; } }\n" +
                    "class Room12 { public string Id; public IMonster12b Monster; public string Item; public Room12(string id, IMonster12b monster, string item) { Id = id; Monster = monster; Item = item; } }\n" +
                    "\n" +
                    "List<Room12> rooms = new List<Room12>\n" +
                    "{\n" +
                    "    new Room12(\"r1\", null, \"potion\"),\n" +
                    "    new Room12(\"r2\", new DungeonMonster(\"Rat\", 5), null),\n" +
                    "    new Room12(\"r3\", new DungeonMonster(\"Ogre\", 40), \"shield\"),\n" +
                    "    new Room12(\"r4\", null, \"gold\"),\n" +
                    "};\n" +
                    "\n" +
                    "Dictionary<string, int> inventory = new Dictionary<string, int>();\n" +
                    "int playerHP = 50;\n" +
                    "double x = 20;\n" +
                    "Canvas.AddShape(\"player\", \"circle\", x, 100, 25, \"#38bdf8\");\n" +
                    "int roomsCleared = 0;\n" +
                    "foreach (Room12 room in rooms)\n" +
                    "{\n" +
                    "    Canvas.MoveTo(\"player\", x, 100);\n" +
                    "    try\n" +
                    "    {\n" +
                    "        // if room.Monster is not null: throw if its Power >= playerHP, else subtract Power from playerHP\n" +
                    "        // if room.Item is not null: add 1 to inventory[room.Item] (creating the entry if missing)\n" +
                    "        roomsCleared++;\n" +
                    "    }\n" +
                    "    catch (InvalidOperationException ex)\n" +
                    "    {\n" +
                    "        Console.WriteLine(ex.Message);\n" +
                    "        break;\n" +
                    "    }\n" +
                    "    x += 60;\n" +
                    "}\n" +
                    "int itemCount = inventory.Values.Sum();\n" +
                    "Console.WriteLine($\"Cleared {roomsCleared} rooms with {playerHP} HP remaining and {itemCount} items collected.\");\n",
                Example:
                    "interface IMonster12b { string Name { get; } int Power { get; } }\n" +
                    "class DungeonMonster : IMonster12b { public string Name { get; } public int Power { get; } public DungeonMonster(string name, int power) { Name = name; Power = power; } }\n" +
                    "class Room12 { public string Id; public IMonster12b Monster; public string Item; public Room12(string id, IMonster12b monster, string item) { Id = id; Monster = monster; Item = item; } }\n" +
                    "\n" +
                    "List<Room12> rooms = new List<Room12>\n" +
                    "{\n" +
                    "    new Room12(\"r1\", null, \"potion\"),\n" +
                    "    new Room12(\"r2\", new DungeonMonster(\"Rat\", 5), null),\n" +
                    "    new Room12(\"r3\", new DungeonMonster(\"Ogre\", 40), \"shield\"),\n" +
                    "    new Room12(\"r4\", null, \"gold\"),\n" +
                    "};\n" +
                    "\n" +
                    "Dictionary<string, int> inventory = new Dictionary<string, int>();\n" +
                    "int playerHP = 50;\n" +
                    "double x = 20;\n" +
                    "Canvas.AddShape(\"player\", \"circle\", x, 100, 25, \"#38bdf8\");\n" +
                    "int roomsCleared = 0;\n" +
                    "foreach (Room12 room in rooms)\n" +
                    "{\n" +
                    "    Canvas.MoveTo(\"player\", x, 100);\n" +
                    "    try\n" +
                    "    {\n" +
                    "        if (room.Monster != null)\n" +
                    "        {\n" +
                    "            if (room.Monster.Power >= playerHP)\n" +
                    "            {\n" +
                    "                throw new InvalidOperationException($\"Defeated by {room.Monster.Name} in {room.Id}!\");\n" +
                    "            }\n" +
                    "            playerHP -= room.Monster.Power;\n" +
                    "        }\n" +
                    "        if (room.Item != null)\n" +
                    "        {\n" +
                    "            if (!inventory.ContainsKey(room.Item))\n" +
                    "            {\n" +
                    "                inventory[room.Item] = 0;\n" +
                    "            }\n" +
                    "            inventory[room.Item] = inventory[room.Item] + 1;\n" +
                    "        }\n" +
                    "        roomsCleared++;\n" +
                    "    }\n" +
                    "    catch (InvalidOperationException ex)\n" +
                    "    {\n" +
                    "        Console.WriteLine(ex.Message);\n" +
                    "        break;\n" +
                    "    }\n" +
                    "    x += 60;\n" +
                    "}\n" +
                    "int itemCount = inventory.Values.Sum();\n" +
                    "Console.WriteLine($\"Cleared {roomsCleared} rooms with {playerHP} HP remaining and {itemCount} items collected.\");",
                Hints: new List<HintTier>
                {
                    new("Trace your HP through each room: 50 → (Rat, -5) → 45 → (Ogre, -40) → 5. It never drops to 0 or below, so the throw never fires and you clear all 4 rooms — this is defensive code for a fight you're strong enough to survive."),
                    new("The Dictionary pattern from Level 10 (check ContainsKey before incrementing) is exactly what collecting items into an inventory needs — one entry per item name, counting duplicates."),
                    new(
                        "interface IMonster12b { string Name { get; } int Power { get; } }\n" +
                        "class DungeonMonster : IMonster12b { public string Name { get; } public int Power { get; } public DungeonMonster(string name, int power) { Name = name; Power = power; } }\n" +
                        "class Room12 { public string Id; public IMonster12b Monster; public string Item; public Room12(string id, IMonster12b monster, string item) { Id = id; Monster = monster; Item = item; } }\n" +
                        "\n" +
                        "List<Room12> rooms = new List<Room12>\n" +
                        "{\n" +
                        "    new Room12(\"r1\", null, \"potion\"),\n" +
                        "    new Room12(\"r2\", new DungeonMonster(\"Rat\", 5), null),\n" +
                        "    new Room12(\"r3\", new DungeonMonster(\"Ogre\", 40), \"shield\"),\n" +
                        "    new Room12(\"r4\", null, \"gold\"),\n" +
                        "};\n" +
                        "\n" +
                        "Dictionary<string, int> inventory = new Dictionary<string, int>();\n" +
                        "int playerHP = 50;\n" +
                        "double x = 20;\n" +
                        "Canvas.AddShape(\"player\", \"circle\", x, 100, 25, \"#38bdf8\");\n" +
                        "int roomsCleared = 0;\n" +
                        "foreach (Room12 room in rooms)\n" +
                        "{\n" +
                        "    Canvas.MoveTo(\"player\", x, 100);\n" +
                        "    try\n" +
                        "    {\n" +
                        "        if (room.Monster != null)\n" +
                        "        {\n" +
                        "            if (room.Monster.Power >= playerHP)\n" +
                        "            {\n" +
                        "                throw new InvalidOperationException($\"Defeated by {room.Monster.Name} in {room.Id}!\");\n" +
                        "            }\n" +
                        "            playerHP -= room.Monster.Power;\n" +
                        "        }\n" +
                        "        if (room.Item != null)\n" +
                        "        {\n" +
                        "            if (!inventory.ContainsKey(room.Item))\n" +
                        "            {\n" +
                        "                inventory[room.Item] = 0;\n" +
                        "            }\n" +
                        "            inventory[room.Item] = inventory[room.Item] + 1;\n" +
                        "        }\n" +
                        "        roomsCleared++;\n" +
                        "    }\n" +
                        "    catch (InvalidOperationException ex)\n" +
                        "    {\n" +
                        "        Console.WriteLine(ex.Message);\n" +
                        "        break;\n" +
                        "    }\n" +
                        "    x += 60;\n" +
                        "}\n" +
                        "int itemCount = inventory.Values.Sum();\n" +
                        "Console.WriteLine($\"Cleared {roomsCleared} rooms with {playerHP} HP remaining and {itemCount} items collected.\");",
                        IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Cleared 4 rooms with 5 HP remaining and 3 items collected.",
                CheckSource: source => source.Contains("interface IMonster12b") && source.Contains("catch (InvalidOperationException") && source.Contains("Canvas.MoveTo") && source.Contains("Dictionary<string, int>")
            )
        },
        QuizStages: new List<QuizStage>
        {
            new(
                Title: "Capstone Check-In",
                AfterTaskId: 5,
                QuestionsPerAttempt: 5,
                Cards: new List<QuizCard>
                {
                    new("What does a `switch` expression let you do that a long if/else chain also can, but more concisely?", new List<string> { "Loop over an array", "Branch on multiple possible values of one thing", "Define a class", "Catch exceptions" }, CorrectIndex: 1),
                    new("What's the difference between a List<T> and a Dictionary<TKey, TValue>?", new List<string> { "They're identical", "A List holds ordered values; a Dictionary maps keys to values", "A Dictionary can't be looped over", "A List can't hold objects" }, CorrectIndex: 1),
                    new("Why use a class with fields instead of several separate loose variables to represent a character?", new List<string> { "It's required by C#", "It groups related data together so it can be passed around as one thing", "It runs faster", "There's no real difference" }, CorrectIndex: 1),
                    new("What does wrapping risky code in try/catch let you do?", new List<string> { "Skip writing the code", "Handle failures gracefully instead of crashing", "Make the code run twice", "Avoid using variables" }, CorrectIndex: 1),
                    new("If two classes both inherit from the same abstract base class, can a single list hold both?", new List<string> { "No, only one type per list", "Yes, since they share the base type", "Only if they're structs", "Only with LINQ" }, CorrectIndex: 1),
                    new("What's a method's return type for, in a method signature like `int CalculateDamage(...)`?", new List<string> { "It names the method", "It tells you what type of value the method sends back", "It's just documentation with no effect", "It controls how many parameters it takes" }, CorrectIndex: 1),
                    new("Why break a big program into multiple small classes and methods, like in this capstone level?", new List<string> { "It's slower but looks nicer", "It makes each piece easier to write, test, and reason about", "C# requires at least 5 classes per program", "It reduces the need for variables" }, CorrectIndex: 1)
                }
            ),
            new(
                Title: "Capstone Mastery",
                AfterTaskId: 10,
                Format: QuizFormat.SwipeTrueFalse,
                QuestionsPerAttempt: 10,
                Cards: new List<QuizCard>
                {
                    new("A class can combine fields, properties, methods, and even implement an interface all at once.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A Dictionary is a good choice for tracking an inventory of item names and quantities.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Polymorphism lets you treat different monster types in a list the same way when calling a shared method like Attack().", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("try/catch should only ever be used once per entire program.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("LINQ methods like `.Where()` and `.OrderBy()` can be combined with loops in the same program.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A well-designed program usually has many small, focused methods rather than one giant method doing everything.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("An interface guarantees that any class implementing it provides certain methods.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Once you've learned loops, arrays, classes, and error handling, you generally have enough to build a real small program.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A game loop that updates state and redraws the canvas each iteration is a common pattern for interactive programs.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Good variable and method names make code harder to understand, not easier.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("Combining classes, collections, LINQ, and error handling together is exactly what real-world C# programs do.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("There's only one correct way to structure any given program.", new List<string> { "True", "False" }, CorrectIndex: 1)
                }
            )
        }
    );
}
