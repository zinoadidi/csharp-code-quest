namespace app.Content;

/// <summary>
/// Level 8 — Classes &amp; Objects I.
///
/// Fields, constructors, methods, properties, and encapsulation. Closes with a
/// visual task where a class (Sprite) owns its own Canvas calls — objects now
/// drive the animation instead of loose top-level code.
/// </summary>
public static class Level08_ClassesAndObjectsI
{
    public static readonly Level Instance = new(
        Id: 8,
        Name: "Classes & Objects I",
        Description: "Bundle data and behavior together into your own types.",
        Tasks: new List<GameTask>
        {
            GameTask.FlashcardDeck(
                new GlossaryTerm("Class", "A blueprint for creating objects — it defines what data and behavior every object built from it will have."),
                new GlossaryTerm("Object", "One actual thing built from a class — if Dog is the blueprint, your specific dog Rex is an object (an \"instance\") of it."),
                new GlossaryTerm("Instance", "Another word for one specific object created from a class — \"instantiating\" a class means building an instance of it."),
                new GlossaryTerm("Field", "A variable that belongs to a class — every object built from that class gets its own copy to store its own data."),
                new GlossaryTerm("Property", "A polished, controlled way to expose a class's data to the outside world — looks like a field from the outside, but can add rules for reading or writing it."),
                new GlossaryTerm("Constructor", "A special method that runs automatically when you create a new object — its job is to set up that object's starting data."),
                new GlossaryTerm("Encapsulation", "Bundling an object's data and the methods that work on it together, and hiding the messy internal details from outside code."),
                new GlossaryTerm("\"this\" Keyword", "Inside a class's own code, this refers to \"the current object\" — useful for telling a field apart from a parameter with the same name."),
                new GlossaryTerm("\"new\" Keyword", "The keyword that actually builds a new object from a class, running its constructor in the process."),
                new GlossaryTerm("State", "The current values of all of an object's fields at any given moment — two objects from the same class can have completely different state.")),
            new(
                Id: 1,
                Title: "Your First Class",
                Description: "Define a class Dog with public fields string Name and int Age. Create one, set Name to \"Rex\" and Age to 3, then print both.",
                Difficulty: Difficulty.Easy,
                Points: 15,
                StarterCode: "class Dog\n{\n    public string Name;\n    public int Age;\n}\n// Create a Dog, set Name and Age, print both\n",
                Example: "class Dog\n{\n    public string Name;\n    public int Age;\n}\nDog d = new Dog();\nd.Name = \"Rex\";\nd.Age = 3;\nConsole.WriteLine(d.Name);\nConsole.WriteLine(d.Age);",
                Hints: new List<HintTier>
                {
                    new("`new Dog()` creates an instance. You reach its fields with dot notation: d.Name = ...;"),
                    new("class Dog\n{\n    public string Name;\n    public int Age;\n}\nDog d = new Dog();\nd.Name = \"Rex\";\nd.Age = 3;\nConsole.WriteLine(d.Name);\nConsole.WriteLine(d.Age);", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "Rex", "3")
            ),
            new(
                Id: 2,
                Title: "Two of a Kind",
                Description: "Define class Point with public int X, Y. Create two separate Points with different coordinates and print each as \"X,Y\".",
                Difficulty: Difficulty.Easy,
                Points: 15,
                StarterCode: "class Point\n{\n    public int X;\n    public int Y;\n}\n// Create p1 (5, 10) and p2 (100, 200), print each as $\"{X},{Y}\"\n",
                Example: "class Point\n{\n    public int X;\n    public int Y;\n}\nPoint p1 = new Point();\np1.X = 5;\np1.Y = 10;\nPoint p2 = new Point();\np2.X = 100;\np2.Y = 200;\nConsole.WriteLine($\"{p1.X},{p1.Y}\");\nConsole.WriteLine($\"{p2.X},{p2.Y}\");",
                Hints: new List<HintTier>
                {
                    new("Each `new Point()` is its own independent object — changing p1's fields never affects p2's."),
                    new("class Point\n{\n    public int X;\n    public int Y;\n}\nPoint p1 = new Point();\np1.X = 5;\np1.Y = 10;\nPoint p2 = new Point();\np2.X = 100;\np2.Y = 200;\nConsole.WriteLine($\"{p1.X},{p1.Y}\");\nConsole.WriteLine($\"{p2.X},{p2.Y}\");", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "5,10", "100,200")
            ),
            new(
                Id: 4,
                Title: "Build It With a Constructor",
                Description: "Define class Person with a public string Name and a constructor Person(string name) that sets it. Create a Person named \"Ada\" and print its Name.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "class Person\n{\n    public string Name;\n    public Person(string name)\n    {\n        // set Name\n    }\n}\n// Create new Person(\"Ada\"), print its Name\n",
                Example: "class Person\n{\n    public string Name;\n    public Person(string name)\n    {\n        Name = name;\n    }\n}\nPerson per = new Person(\"Ada\");\nConsole.WriteLine(per.Name);",
                Hints: new List<HintTier>
                {
                    new("A constructor is a method with the same name as the class and no return type — it runs automatically when you `new` the object."),
                    new("class Person\n{\n    public string Name;\n    public Person(string name)\n    {\n        Name = name;\n    }\n}\nPerson per = new Person(\"Ada\");\nConsole.WriteLine(per.Name);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Ada"
            ),
            new(
                Id: 5,
                Title: "A Method That Computes",
                Description: "Define class Circle with a constructor taking double radius and a method Area() returning Math.PI * Radius * Radius. Create a Circle with radius 2, print Math.Round(c.Area(), 2).",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "class Circle\n{\n    public double Radius;\n    public Circle(double radius)\n    {\n        Radius = radius;\n    }\n    public double Area()\n    {\n        // return the area\n    }\n}\n// Create Circle(2), print Math.Round(c.Area(), 2)\n",
                Example: "class Circle\n{\n    public double Radius;\n    public Circle(double radius)\n    {\n        Radius = radius;\n    }\n    public double Area()\n    {\n        return Math.PI * Radius * Radius;\n    }\n}\nCircle c = new Circle(2);\nConsole.WriteLine(Math.Round(c.Area(), 2));\n// Output: 12.57",
                Hints: new List<HintTier>
                {
                    new("A method inside a class can read the object's own fields directly — no need to pass Radius in as a parameter."),
                    new("class Circle\n{\n    public double Radius;\n    public Circle(double radius)\n    {\n        Radius = radius;\n    }\n    public double Area()\n    {\n        return Math.PI * Radius * Radius;\n    }\n}\nCircle c = new Circle(2);\nConsole.WriteLine(Math.Round(c.Area(), 2));", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "12.57"
            ),
            new(
                Id: 6,
                Title: "Keep It Private",
                Description: "Define class Account with public string Owner, a private int balance, a constructor Account(string owner, int startingBalance), a method Deposit(int amount), and a method GetBalance(). Create an Account(\"Sam\", 100), deposit 50, print GetBalance().",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "class Account\n{\n    public string Owner;\n    private int balance;\n    public Account(string owner, int startingBalance)\n    {\n        Owner = owner;\n        balance = startingBalance;\n    }\n    public void Deposit(int amount)\n    {\n        // add amount to balance\n    }\n    public int GetBalance()\n    {\n        return balance;\n    }\n}\n// Create Account(\"Sam\", 100), Deposit(50), print GetBalance()\n",
                Example: "class Account\n{\n    public string Owner;\n    private int balance;\n    public Account(string owner, int startingBalance)\n    {\n        Owner = owner;\n        balance = startingBalance;\n    }\n    public void Deposit(int amount)\n    {\n        balance += amount;\n    }\n    public int GetBalance()\n    {\n        return balance;\n    }\n}\nAccount acc = new Account(\"Sam\", 100);\nacc.Deposit(50);\nConsole.WriteLine(acc.GetBalance());",
                Hints: new List<HintTier>
                {
                    new("`private` means balance can only be touched from inside the class itself — outside code has to go through Deposit() and GetBalance(). That's encapsulation."),
                    new("class Account\n{\n    public string Owner;\n    private int balance;\n    public Account(string owner, int startingBalance)\n    {\n        Owner = owner;\n        balance = startingBalance;\n    }\n    public void Deposit(int amount)\n    {\n        balance += amount;\n    }\n    public int GetBalance()\n    {\n        return balance;\n    }\n}\nAccount acc = new Account(\"Sam\", 100);\nacc.Deposit(50);\nConsole.WriteLine(acc.GetBalance());", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "150"
            ),
            new(
                Id: 7,
                Title: "Auto-Properties",
                Description: "Define class Rectangle with auto-properties double Width { get; set; } and double Height { get; set; }. Create one with Width 4 and Height 5, print Width * Height.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "class Rectangle\n{\n    public double Width { get; set; }\n    public double Height { get; set; }\n}\n// Create a Rectangle, set Width=4, Height=5, print Width * Height\n",
                Example: "class Rectangle\n{\n    public double Width { get; set; }\n    public double Height { get; set; }\n}\nRectangle r = new Rectangle();\nr.Width = 4;\nr.Height = 5;\nConsole.WriteLine(r.Width * r.Height);\n// Output: 20",
                Hints: new List<HintTier>
                {
                    new("{ get; set; } is shorthand for a property with an automatically-generated backing field — you use it exactly like a public field."),
                    new("class Rectangle\n{\n    public double Width { get; set; }\n    public double Height { get; set; }\n}\nRectangle r = new Rectangle();\nr.Width = 4;\nr.Height = 5;\nConsole.WriteLine(r.Width * r.Height);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "20"
            ),
            new(
                Id: 8,
                Title: "Read-Only From Outside",
                Description: "Define class Counter with public int Value { get; private set; } and a method Increment() that increases it. Create one, call Increment() three times, print Value.",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode: "class Counter\n{\n    public int Value { get; private set; }\n    public void Increment()\n    {\n        // increase Value\n    }\n}\n// Create a Counter, call Increment() three times, print Value\n",
                Example: "class Counter\n{\n    public int Value { get; private set; }\n    public void Increment()\n    {\n        Value++;\n    }\n}\nCounter counter = new Counter();\ncounter.Increment();\ncounter.Increment();\ncounter.Increment();\nConsole.WriteLine(counter.Value);",
                Hints: new List<HintTier>
                {
                    new("`private set` means only code inside Counter can assign Value — outside code can read it but never set it directly, forcing everyone through Increment()."),
                    new("class Counter\n{\n    public int Value { get; private set; }\n    public void Increment()\n    {\n        Value++;\n    }\n}\nCounter counter = new Counter();\ncounter.Increment();\ncounter.Increment();\ncounter.Increment();\nConsole.WriteLine(counter.Value);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "3"
            ),
            new(
                Id: 9,
                Title: "Guard the Field",
                Description: "Define class Player with public string Name, public int Health = 100, and a method TakeDamage(int amount) that subtracts amount from Health but never lets it go below 0. Create a Player named \"Hero\", call TakeDamage(30) then TakeDamage(90), print \"<Name>: <Health>\".",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode: "class Player\n{\n    public string Name;\n    public int Health = 100;\n    public void TakeDamage(int amount)\n    {\n        // subtract amount, but clamp at 0\n    }\n}\n// Create a Player named \"Hero\", TakeDamage(30), TakeDamage(90), print $\"{Name}: {Health}\"\n",
                Example: "class Player\n{\n    public string Name;\n    public int Health = 100;\n    public void TakeDamage(int amount)\n    {\n        Health -= amount;\n        if (Health < 0) Health = 0;\n    }\n}\nPlayer pl = new Player();\npl.Name = \"Hero\";\npl.TakeDamage(30);\npl.TakeDamage(90);\nConsole.WriteLine($\"{pl.Name}: {pl.Health}\");",
                Hints: new List<HintTier>
                {
                    new("A field can have a default value right where it's declared: public int Health = 100;"),
                    new("After subtracting, check if Health dropped below 0 and clamp it back to 0 if so."),
                    new("class Player\n{\n    public string Name;\n    public int Health = 100;\n    public void TakeDamage(int amount)\n    {\n        Health -= amount;\n        if (Health < 0) Health = 0;\n    }\n}\nPlayer pl = new Player();\npl.Name = \"Hero\";\npl.TakeDamage(30);\npl.TakeDamage(90);\nConsole.WriteLine($\"{pl.Name}: {pl.Health}\");", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Hero: 0"
            ),
            new(
                Id: 10,
                Title: "Describe Yourself",
                Description: "Define class Book with public string Title, Author, and an override of ToString() returning \"<Title> by <Author>\". Create one for \"Dune\" by \"Frank Herbert\" and print it both by calling ToString() explicitly and by printing the object directly.",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode: "class Book\n{\n    public string Title;\n    public string Author;\n    public override string ToString()\n    {\n        // return \"<Title> by <Author>\"\n    }\n}\n// Create a Book, print b.ToString() then print b directly\n",
                Example: "class Book\n{\n    public string Title;\n    public string Author;\n    public override string ToString()\n    {\n        return $\"{Title} by {Author}\";\n    }\n}\nBook b = new Book();\nb.Title = \"Dune\";\nb.Author = \"Frank Herbert\";\nConsole.WriteLine(b.ToString());\nConsole.WriteLine(b);",
                Hints: new List<HintTier>
                {
                    new("Every object already has a ToString() — overriding it lets you control what Console.WriteLine(obj) prints when you pass the object directly, not just a field."),
                    new("class Book\n{\n    public string Title;\n    public string Author;\n    public override string ToString()\n    {\n        return $\"{Title} by {Author}\";\n    }\n}\nBook b = new Book();\nb.Title = \"Dune\";\nb.Author = \"Frank Herbert\";\nConsole.WriteLine(b.ToString());\nConsole.WriteLine(b);", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "Dune by Frank Herbert", "Dune by Frank Herbert")
            ),
            new(
                Id: 11,
                Title: "🎮 Visual: Sprite Class",
                Description:
                    "Define class Sprite with string Id, double X, Y, a constructor Sprite(string id, double x, double y) that " +
                    "stores them and calls Canvas.AddShape(Id, \"circle\", X, Y), and a method MoveRight(double amount) that " +
                    "increases X and calls Canvas.MoveTo(Id, X, Y). Create two Sprites — s1 at (20, 50) and s2 at (20, 150) — " +
                    "then loop 3 times calling s1.MoveRight(40) and s2.MoveRight(70) each time. Print \"s1 at <X>, s2 at <X>\".",
                Difficulty: Difficulty.Hard,
                Points: 30,
                StarterCode:
                    "class Sprite\n" +
                    "{\n" +
                    "    public string Id;\n" +
                    "    public double X;\n" +
                    "    public double Y;\n" +
                    "    public Sprite(string id, double x, double y)\n" +
                    "    {\n" +
                    "        // store fields, then Canvas.AddShape(Id, \"circle\", X, Y)\n" +
                    "    }\n" +
                    "    public void MoveRight(double amount)\n" +
                    "    {\n" +
                    "        // increase X, then Canvas.MoveTo(Id, X, Y)\n" +
                    "    }\n" +
                    "}\n" +
                    "// Create s1 (\"s1\", 20, 50) and s2 (\"s2\", 20, 150)\n" +
                    "// Loop 3 times: s1.MoveRight(40); s2.MoveRight(70);\n" +
                    "// Print $\"s1 at {s1.X}, s2 at {s2.X}\"\n",
                Example:
                    "class Sprite\n" +
                    "{\n" +
                    "    public string Id;\n" +
                    "    public double X;\n" +
                    "    public double Y;\n" +
                    "    public Sprite(string id, double x, double y)\n" +
                    "    {\n" +
                    "        Id = id;\n" +
                    "        X = x;\n" +
                    "        Y = y;\n" +
                    "        Canvas.AddShape(Id, \"circle\", X, Y);\n" +
                    "    }\n" +
                    "    public void MoveRight(double amount)\n" +
                    "    {\n" +
                    "        X += amount;\n" +
                    "        Canvas.MoveTo(Id, X, Y);\n" +
                    "    }\n" +
                    "}\n" +
                    "Sprite s1 = new Sprite(\"s1\", 20, 50);\n" +
                    "Sprite s2 = new Sprite(\"s2\", 20, 150);\n" +
                    "for (int i = 0; i < 3; i++)\n" +
                    "{\n" +
                    "    s1.MoveRight(40);\n" +
                    "    s2.MoveRight(70);\n" +
                    "}\n" +
                    "Console.WriteLine($\"s1 at {s1.X}, s2 at {s2.X}\");",
                Hints: new List<HintTier>
                {
                    new("The constructor both sets up the object's fields AND registers it on the canvas — the two Sprites will move independently because each owns its own X and Id."),
                    new("MoveRight only needs to update X and re-issue a MoveTo — the AddShape already happened once, in the constructor."),
                    new(
                        "class Sprite\n" +
                        "{\n" +
                        "    public string Id;\n" +
                        "    public double X;\n" +
                        "    public double Y;\n" +
                        "    public Sprite(string id, double x, double y)\n" +
                        "    {\n" +
                        "        Id = id;\n" +
                        "        X = x;\n" +
                        "        Y = y;\n" +
                        "        Canvas.AddShape(Id, \"circle\", X, Y);\n" +
                        "    }\n" +
                        "    public void MoveRight(double amount)\n" +
                        "    {\n" +
                        "        X += amount;\n" +
                        "        Canvas.MoveTo(Id, X, Y);\n" +
                        "    }\n" +
                        "}\n" +
                        "Sprite s1 = new Sprite(\"s1\", 20, 50);\n" +
                        "Sprite s2 = new Sprite(\"s2\", 20, 150);\n" +
                        "for (int i = 0; i < 3; i++)\n" +
                        "{\n" +
                        "    s1.MoveRight(40);\n" +
                        "    s2.MoveRight(70);\n" +
                        "}\n" +
                        "Console.WriteLine($\"s1 at {s1.X}, s2 at {s2.X}\");",
                        IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "s1 at 140, s2 at 230",
                CheckSource: source => source.Contains("Canvas.AddShape") && source.Contains("Canvas.MoveTo") && source.Contains("class Sprite")
            ),
            new(
                Id: 3,
                Title: "🎮 Visual: Health Bar Object",
                Description:
                    "Define class HealthBar with string Id and int Hp, a constructor that stores them and calls " +
                    "Canvas.AddShape(Id, \"rect\", 50, 50, Hp), and a method Damage(int amount) that subtracts amount from Hp and " +
                    "calls Canvas.SetColor(Id, Hp <= 30 ? \"#ef4444\" : \"#22c55e\"). Create a HealthBar(\"hp1\", 100), call " +
                    "Damage(80), then print Hp.",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode:
                    "// Define class HealthBar:\n" +
                    "//   string Id, int Hp\n" +
                    "//   Constructor stores them, calls Canvas.AddShape(Id, \"rect\", 50, 50, Hp)\n" +
                    "//   Damage(int amount) subtracts from Hp, then Canvas.SetColor based on Hp\n" +
                    "// Create HealthBar(\"hp1\", 100), Damage(80), print Hp\n",
                Example:
                    "class HealthBar\n{\n    public string Id;\n    public int Hp;\n    public HealthBar(string id, int hp)\n    {\n        Id = id;\n        Hp = hp;\n        Canvas.AddShape(Id, \"rect\", 50, 50, Hp);\n    }\n    public void Damage(int amount)\n    {\n        Hp -= amount;\n        Canvas.SetColor(Id, Hp <= 30 ? \"#ef4444\" : \"#22c55e\");\n    }\n}\nHealthBar hp1 = new HealthBar(\"hp1\", 100);\nhp1.Damage(80);\nConsole.WriteLine(hp1.Hp);",
                Hints: new List<HintTier>
                {
                    new("The constructor both stores the fields and draws the shape; Damage both changes Hp and recolors based on the new value."),
                    new("class HealthBar\n{\n    public string Id;\n    public int Hp;\n    public HealthBar(string id, int hp)\n    {\n        Id = id;\n        Hp = hp;\n        Canvas.AddShape(Id, \"rect\", 50, 50, Hp);\n    }\n    public void Damage(int amount)\n    {\n        Hp -= amount;\n        Canvas.SetColor(Id, Hp <= 30 ? \"#ef4444\" : \"#22c55e\");\n    }\n}\nHealthBar hp1 = new HealthBar(\"hp1\", 100);\nhp1.Damage(80);\nConsole.WriteLine(hp1.Hp);", IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "20",
                CheckSource: source => source.Contains("Canvas.AddShape") && source.Contains("Canvas.SetColor") && source.Contains("class HealthBar")
            )
        },
        QuizStages: new List<QuizStage>
        {
            new(
                Title: "Classes Check-In",
                AfterTaskId: 6,
                QuestionsPerAttempt: 5,
                Cards: new List<QuizCard>
                {
                    new("What is a class in C#?", new List<string> { "A loop type", "A blueprint for creating objects", "A kind of array", "A built-in method" }, CorrectIndex: 1),
                    new("What is an instance of a class called?", new List<string> { "A method", "An object", "A namespace", "A parameter" }, CorrectIndex: 1),
                    new("What special method runs automatically when you create an object with `new`?", new List<string> { "The destructor", "The constructor", "Main", "ToString" }, CorrectIndex: 1),
                    new("What does marking a field `private` do?", new List<string> { "Deletes it", "Hides it from outside the class", "Makes it read-only", "Makes it static" }, CorrectIndex: 1),
                    new("What's the shorthand for a simple get/set field called?", new List<string> { "A local variable", "An auto-property", "A constant", "A constructor" }, CorrectIndex: 1),
                    new("Why keep a field private and expose it only through a property?", new List<string> { "It's required by C#", "It lets you control/validate how it's changed", "It makes the code run faster", "It's just a style preference with no effect" }, CorrectIndex: 1),
                    new("What does an auto-property like `public int Age { get; set; }` save you from writing?", new List<string> { "A whole class", "A separate private field and manual get/set methods", "A constructor", "A namespace" }, CorrectIndex: 1)
                }
            ),
            new(
                Title: "Classes Mastery",
                AfterTaskId: 11,
                Format: QuizFormat.SwipeTrueFalse,
                QuestionsPerAttempt: 10,
                Cards: new List<QuizCard>
                {
                    new("A class can have multiple objects created from it, each with its own field values.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A constructor always has the same name as the class.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`private` fields can be accessed directly from outside the class.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("An auto-property like `public int Age { get; set; }` is shorthand for a private field plus get/set methods.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A read-only property (`get;` with no `set;`) can be changed freely from outside the class.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("A method inside a class can read and use that class's own private fields.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("You can validate input inside a property's `set` before storing it.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Every class must define its own constructor explicitly, or the program won't compile.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("Overriding `ToString()` lets you control what gets printed when you pass an object to Console.WriteLine.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A class is the same thing as an object — the terms are fully interchangeable.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("Guarding a field means checking/validating a value before allowing it to be assigned.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Once created, an object's fields can never be changed for the rest of the program.", new List<string> { "True", "False" }, CorrectIndex: 1)
                }
            )
        }
    );
}
