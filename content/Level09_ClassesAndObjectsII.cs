namespace app.Content;

/// <summary>
/// Level 9 — Classes &amp; Objects II.
///
/// Inheritance, virtual/override, interfaces, and abstract classes. Closes with
/// the biggest visual task yet: an abstract GameObject base class with two
/// different subclasses, each driving its own Canvas behavior polymorphically.
/// </summary>
public static class Level09_ClassesAndObjectsII
{
    public static readonly Level Instance = new(
        Id: 9,
        Name: "Classes & Objects II",
        Description: "Share behavior through inheritance, and let objects decide how they act for themselves.",
        Tasks: new List<GameTask>
        {
            GameTask.FlashcardDeck(
                new GlossaryTerm("Inheritance", "Letting one class reuse and extend another class's fields and methods, instead of copy-pasting them — a Dog class can inherit from a general Animal class."),
                new GlossaryTerm("Base Class", "The class being inherited FROM — also called a parent class. Animal is the base class if Dog inherits from it."),
                new GlossaryTerm("Derived Class", "The class doing the inheriting — also called a subclass or child class. Dog is a derived class of Animal."),
                new GlossaryTerm("Override", "Replacing a method a derived class inherited from its base class with its own version, tailored to that specific subclass."),
                new GlossaryTerm("Virtual", "Marking a base class method as override-able — without this keyword, a derived class isn't allowed to replace that method's behavior."),
                new GlossaryTerm("Polymorphism", "Treating different derived-class objects the same way through their shared base type, while each one still runs its own overridden behavior."),
                new GlossaryTerm("Abstract Class", "A base class that can't be built directly (no objects of it exist on its own) — it exists purely to be inherited from."),
                new GlossaryTerm("Interface", "A contract that says \"anything implementing me must have these methods\" — without providing any actual code for them itself."),
                new GlossaryTerm("\"base\" Keyword", "Inside a derived class, base refers to its parent class — commonly used to call the base class's own version of a method or constructor."),
                new GlossaryTerm("Is-A Relationship", "The kind of relationship inheritance models — a Dog IS-A(n) Animal, which is different from a Car HAS-A(n) Engine (that's composition, not inheritance).")),
            new(
                Id: 1,
                Title: "Inherit the Basics",
                Description: "Define class Animal with public string Name and a method Eat() that prints \"<Name> is eating.\". Define class Dog : Animal with a method Bark() that prints \"<Name> says Woof!\". Create a Dog named \"Rex\" and call both methods.",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "class Animal\n{\n    public string Name;\n    public void Eat()\n    {\n        Console.WriteLine($\"{Name} is eating.\");\n    }\n}\nclass Dog : Animal\n{\n    public void Bark()\n    {\n        Console.WriteLine($\"{Name} says Woof!\");\n    }\n}\n// Create a Dog named \"Rex\", call Eat() then Bark()\n",
                Example: "class Animal\n{\n    public string Name;\n    public void Eat()\n    {\n        Console.WriteLine($\"{Name} is eating.\");\n    }\n}\nclass Dog : Animal\n{\n    public void Bark()\n    {\n        Console.WriteLine($\"{Name} says Woof!\");\n    }\n}\nDog dog = new Dog();\ndog.Name = \"Rex\";\ndog.Eat();\ndog.Bark();",
                Hints: new List<HintTier>
                {
                    new("`class Dog : Animal` means Dog inherits everything Animal has (Name, Eat()) and adds its own (Bark())."),
                    new("class Animal\n{\n    public string Name;\n    public void Eat()\n    {\n        Console.WriteLine($\"{Name} is eating.\");\n    }\n}\nclass Dog : Animal\n{\n    public void Bark()\n    {\n        Console.WriteLine($\"{Name} says Woof!\");\n    }\n}\nDog dog = new Dog();\ndog.Name = \"Rex\";\ndog.Eat();\ndog.Bark();", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "Rex is eating.", "Rex says Woof!")
            ),
            new(
                Id: 2,
                Title: "Override the Behavior",
                Description: "Define class Shape with a virtual method Area() returning 0. Define class Square : Shape with a Side field and an override of Area() returning Side * Side. Store a Square(4) in a Shape variable and print its Area().",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "class Shape\n{\n    public virtual double Area()\n    {\n        return 0;\n    }\n}\nclass Square : Shape\n{\n    public double Side;\n    public Square(double side) { Side = side; }\n    public override double Area()\n    {\n        // return Side * Side\n    }\n}\n// Store new Square(4) in a Shape variable, print its Area()\n",
                Example: "class Shape\n{\n    public virtual double Area()\n    {\n        return 0;\n    }\n}\nclass Square : Shape\n{\n    public double Side;\n    public Square(double side) { Side = side; }\n    public override double Area()\n    {\n        return Side * Side;\n    }\n}\nShape sh = new Square(4);\nConsole.WriteLine(sh.Area());\n// Output: 16",
                Hints: new List<HintTier>
                {
                    new("`virtual` on the base method and `override` on the derived one means calling Area() through a Shape variable still runs Square's version — that's polymorphism."),
                    new("class Shape\n{\n    public virtual double Area()\n    {\n        return 0;\n    }\n}\nclass Square : Shape\n{\n    public double Side;\n    public Square(double side) { Side = side; }\n    public override double Area()\n    {\n        return Side * Side;\n    }\n}\nShape sh = new Square(4);\nConsole.WriteLine(sh.Area());", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "16"
            ),
            new(
                Id: 4,
                Title: "Pass It Up",
                Description: "Define class Vehicle with a constructor(string name) and a virtual Describe() returning \"Vehicle: <Name>\". Define class Car : Vehicle whose constructor calls base(name), overriding Describe() to return \"Car: <Name>\". Store a Car(\"Tesla\") in a Vehicle variable and print Describe().",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "class Vehicle\n{\n    public string Name;\n    public Vehicle(string name)\n    {\n        Name = name;\n    }\n    public virtual string Describe()\n    {\n        return $\"Vehicle: {Name}\";\n    }\n}\nclass Car : Vehicle\n{\n    public Car(string name) : base(name) { }\n    public override string Describe()\n    {\n        // return \"Car: <Name>\"\n    }\n}\n// Store new Car(\"Tesla\") in a Vehicle variable, print Describe()\n",
                Example: "class Vehicle\n{\n    public string Name;\n    public Vehicle(string name)\n    {\n        Name = name;\n    }\n    public virtual string Describe()\n    {\n        return $\"Vehicle: {Name}\";\n    }\n}\nclass Car : Vehicle\n{\n    public Car(string name) : base(name) { }\n    public override string Describe()\n    {\n        return $\"Car: {Name}\";\n    }\n}\nVehicle v = new Car(\"Tesla\");\nConsole.WriteLine(v.Describe());",
                Hints: new List<HintTier>
                {
                    new("`: base(name)` forwards the constructor argument up to Vehicle's own constructor, so Name gets set there."),
                    new("class Vehicle\n{\n    public string Name;\n    public Vehicle(string name)\n    {\n        Name = name;\n    }\n    public virtual string Describe()\n    {\n        return $\"Vehicle: {Name}\";\n    }\n}\nclass Car : Vehicle\n{\n    public Car(string name) : base(name) { }\n    public override string Describe()\n    {\n        return $\"Car: {Name}\";\n    }\n}\nVehicle v = new Car(\"Tesla\");\nConsole.WriteLine(v.Describe());", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Car: Tesla"
            ),
            new(
                Id: 5,
                Title: "Promise an Interface",
                Description: "Define interface IGreetable with a string Greet() method. Define class Robot : IGreetable implementing it to return \"Beep boop hello!\". Store a Robot in an IGreetable variable and print Greet().",
                Difficulty: Difficulty.Medium,
                Points: 20,
                StarterCode: "interface IGreetable\n{\n    string Greet();\n}\nclass Robot : IGreetable\n{\n    public string Greet()\n    {\n        // return \"Beep boop hello!\"\n    }\n}\n// Store new Robot() in an IGreetable variable, print Greet()\n",
                Example: "interface IGreetable\n{\n    string Greet();\n}\nclass Robot : IGreetable\n{\n    public string Greet()\n    {\n        return \"Beep boop hello!\";\n    }\n}\nIGreetable g = new Robot();\nConsole.WriteLine(g.Greet());",
                Hints: new List<HintTier>
                {
                    new("An interface only declares WHAT a method's signature is — no body. A class that implements it must supply the actual code."),
                    new("interface IGreetable\n{\n    string Greet();\n}\nclass Robot : IGreetable\n{\n    public string Greet()\n    {\n        return \"Beep boop hello!\";\n    }\n}\nIGreetable g = new Robot();\nConsole.WriteLine(g.Greet());", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "Beep boop hello!"
            ),
            new(
                Id: 6,
                Title: "Build on the Base",
                Description: "Define class Employee with a virtual GetPay() returning 1000. Define class Manager : Employee overriding GetPay() to return base.GetPay() + 500. Store a Manager in an Employee variable and print GetPay().",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode: "class Employee\n{\n    public string Name;\n    public virtual int GetPay()\n    {\n        return 1000;\n    }\n}\nclass Manager : Employee\n{\n    public override int GetPay()\n    {\n        // return base.GetPay() + 500\n    }\n}\n// Store new Manager() in an Employee variable, print GetPay()\n",
                Example: "class Employee\n{\n    public string Name;\n    public virtual int GetPay()\n    {\n        return 1000;\n    }\n}\nclass Manager : Employee\n{\n    public override int GetPay()\n    {\n        return base.GetPay() + 500;\n    }\n}\nEmployee e = new Manager();\nConsole.WriteLine(e.GetPay());\n// Output: 1500",
                Hints: new List<HintTier>
                {
                    new("`base.GetPay()` calls the parent class's version of the method instead of the overridden one — useful for extending behavior rather than replacing it entirely."),
                    new("class Employee\n{\n    public string Name;\n    public virtual int GetPay()\n    {\n        return 1000;\n    }\n}\nclass Manager : Employee\n{\n    public override int GetPay()\n    {\n        return base.GetPay() + 500;\n    }\n}\nEmployee e = new Manager();\nConsole.WriteLine(e.GetPay());", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "1500"
            ),
            new(
                Id: 7,
                Title: "Abstract Instruments",
                Description: "Define abstract class Instrument with an abstract string Play(). Define class Guitar : Instrument returning \"Strum strum\" and class Drum : Instrument returning \"Boom boom\". Put one of each in a List<Instrument> and print each one's Play().",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode: "abstract class Instrument\n{\n    public abstract string Play();\n}\nclass Guitar : Instrument\n{\n    public override string Play()\n    {\n        return \"Strum strum\";\n    }\n}\nclass Drum : Instrument\n{\n    public override string Play()\n    {\n        return \"Boom boom\";\n    }\n}\n// Put a Guitar and a Drum in a List<Instrument>, print each Play()\n",
                Example: "abstract class Instrument\n{\n    public abstract string Play();\n}\nclass Guitar : Instrument\n{\n    public override string Play()\n    {\n        return \"Strum strum\";\n    }\n}\nclass Drum : Instrument\n{\n    public override string Play()\n    {\n        return \"Boom boom\";\n    }\n}\nList<Instrument> band = new List<Instrument> { new Guitar(), new Drum() };\nforeach (Instrument i in band)\n{\n    Console.WriteLine(i.Play());\n}",
                Hints: new List<HintTier>
                {
                    new("`abstract class Instrument` can never be instantiated directly with `new Instrument()` — it exists only to be inherited from. `abstract` methods have no body; every non-abstract subclass must override them."),
                    new("abstract class Instrument\n{\n    public abstract string Play();\n}\nclass Guitar : Instrument\n{\n    public override string Play()\n    {\n        return \"Strum strum\";\n    }\n}\nclass Drum : Instrument\n{\n    public override string Play()\n    {\n        return \"Boom boom\";\n    }\n}\nList<Instrument> band = new List<Instrument> { new Guitar(), new Drum() };\nforeach (Instrument i in band)\n{\n    Console.WriteLine(i.Play());\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "Strum strum", "Boom boom")
            ),
            new(
                Id: 8,
                Title: "One List, Many Shapes",
                Description: "Define interface IShape2 with double Area(). Define Rect2 (W, H) and Tri2 (Base, Height) both implementing it correctly. Put a Rect2(4,5) and a Tri2(6,4) in a List<IShape2>, sum their Area()s, and print the total.",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode: "interface IShape2\n{\n    double Area();\n}\nclass Rect2 : IShape2\n{\n    public double W, H;\n    public Rect2(double w, double h) { W = w; H = h; }\n    public double Area() { return W * H; }\n}\nclass Tri2 : IShape2\n{\n    public double Base, Height;\n    public Tri2(double b, double h) { Base = b; Height = h; }\n    public double Area() { return Base * Height / 2; }\n}\n// Put a Rect2(4,5) and Tri2(6,4) in a List<IShape2>, sum and print their Area()s\n",
                Example: "interface IShape2\n{\n    double Area();\n}\nclass Rect2 : IShape2\n{\n    public double W, H;\n    public Rect2(double w, double h) { W = w; H = h; }\n    public double Area() { return W * H; }\n}\nclass Tri2 : IShape2\n{\n    public double Base, Height;\n    public Tri2(double b, double h) { Base = b; Height = h; }\n    public double Area() { return Base * Height / 2; }\n}\nList<IShape2> shapes = new List<IShape2> { new Rect2(4, 5), new Tri2(6, 4) };\ndouble totalArea = 0;\nforeach (IShape2 s in shapes)\n{\n    totalArea += s.Area();\n}\nConsole.WriteLine(totalArea);\n// Output: 32",
                Hints: new List<HintTier>
                {
                    new("The list doesn't care that Rect2 and Tri2 are unrelated classes — it only cares that both promise an Area() method, because both implement IShape2."),
                    new("interface IShape2\n{\n    double Area();\n}\nclass Rect2 : IShape2\n{\n    public double W, H;\n    public Rect2(double w, double h) { W = w; H = h; }\n    public double Area() { return W * H; }\n}\nclass Tri2 : IShape2\n{\n    public double Base, Height;\n    public Tri2(double b, double h) { Base = b; Height = h; }\n    public double Area() { return Base * Height / 2; }\n}\nList<IShape2> shapes = new List<IShape2> { new Rect2(4, 5), new Tri2(6, 4) };\ndouble totalArea = 0;\nforeach (IShape2 s in shapes)\n{\n    totalArea += s.Area();\n}\nConsole.WriteLine(totalArea);", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "32"
            ),
            new(
                Id: 9,
                Title: "Protected Access",
                Description: "Define class Base8 with a protected int value = 10 and a method GetValue(). Define class Derived8 : Base8 with a method AddFive() that adds 5 to value directly. Create a Derived8, call AddFive(), print GetValue().",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode: "class Base8\n{\n    protected int value = 10;\n    public int GetValue() { return value; }\n}\nclass Derived8 : Base8\n{\n    public void AddFive()\n    {\n        // add 5 to value\n    }\n}\n// Create a Derived8, call AddFive(), print GetValue()\n",
                Example: "class Base8\n{\n    protected int value = 10;\n    public int GetValue() { return value; }\n}\nclass Derived8 : Base8\n{\n    public void AddFive()\n    {\n        value += 5;\n    }\n}\nDerived8 d8 = new Derived8();\nd8.AddFive();\nConsole.WriteLine(d8.GetValue());",
                Hints: new List<HintTier>
                {
                    new("`protected` is between `private` and `public`: outside code still can't touch it, but a subclass can, unlike `private`."),
                    new("class Base8\n{\n    protected int value = 10;\n    public int GetValue() { return value; }\n}\nclass Derived8 : Base8\n{\n    public void AddFive()\n    {\n        value += 5;\n    }\n}\nDerived8 d8 = new Derived8();\nd8.AddFive();\nConsole.WriteLine(d8.GetValue());", IsSolution: true)
                },
                CheckOutput: output => output.Trim() == "15"
            ),
            new(
                Id: 10,
                Title: "A Boss Fight",
                Description: "Define class Enemy with Name, Health = 50, a constructor(name), and virtual Attack() printing \"<Name> attacks!\". Define class Boss : Enemy whose constructor sets Health to 200 and whose Attack() calls base.Attack() then prints \"<Name> uses a special move!\". Put an Enemy(\"Goblin\") and a Boss(\"Dragon\") in a List<Enemy> and call Attack() on each.",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode: "class Enemy\n{\n    public string Name;\n    public int Health = 50;\n    public Enemy(string name) { Name = name; }\n    public virtual void Attack()\n    {\n        Console.WriteLine($\"{Name} attacks!\");\n    }\n}\nclass Boss : Enemy\n{\n    public Boss(string name) : base(name) { /* set Health to 200 */ }\n    public override void Attack()\n    {\n        // call base.Attack(), then print the special move line\n    }\n}\n// Put an Enemy(\"Goblin\") and Boss(\"Dragon\") in a List<Enemy>, call Attack() on each\n",
                Example: "class Enemy\n{\n    public string Name;\n    public int Health = 50;\n    public Enemy(string name) { Name = name; }\n    public virtual void Attack()\n    {\n        Console.WriteLine($\"{Name} attacks!\");\n    }\n}\nclass Boss : Enemy\n{\n    public Boss(string name) : base(name) { Health = 200; }\n    public override void Attack()\n    {\n        base.Attack();\n        Console.WriteLine($\"{Name} uses a special move!\");\n    }\n}\nList<Enemy> enemies = new List<Enemy> { new Enemy(\"Goblin\"), new Boss(\"Dragon\") };\nforeach (Enemy en in enemies)\n{\n    en.Attack();\n}",
                Hints: new List<HintTier>
                {
                    new("A constructor body runs after `: base(name)` finishes, so you can adjust fields like Health right after the base constructor sets Name."),
                    new("class Enemy\n{\n    public string Name;\n    public int Health = 50;\n    public Enemy(string name) { Name = name; }\n    public virtual void Attack()\n    {\n        Console.WriteLine($\"{Name} attacks!\");\n    }\n}\nclass Boss : Enemy\n{\n    public Boss(string name) : base(name) { Health = 200; }\n    public override void Attack()\n    {\n        base.Attack();\n        Console.WriteLine($\"{Name} uses a special move!\");\n    }\n}\nList<Enemy> enemies = new List<Enemy> { new Enemy(\"Goblin\"), new Boss(\"Dragon\") };\nforeach (Enemy en in enemies)\n{\n    en.Attack();\n}", IsSolution: true)
                },
                CheckOutput: output => OutputChecks.LinesEqual(output, "Goblin attacks!", "Dragon attacks!", "Dragon uses a special move!")
            ),
            new(
                Id: 11,
                Title: "🎮 Visual: Polymorphic Arena",
                Description:
                    "Define abstract class GameObject with Id, X, Y, a constructor that stores them and calls " +
                    "Canvas.AddShape(Id, ShapeType(), X, Y), an abstract string ShapeType(), and an abstract void Update(). " +
                    "Define class Bouncer : GameObject whose ShapeType() is \"circle\" and whose Update() moves X back and forth " +
                    "(bounce between 0 and 200) using Canvas.MoveTo. Define class Spinner : GameObject whose ShapeType() is " +
                    "\"square\" and whose Update() calls Canvas.SetColor(Id, \"#f472b6\"). Put one Bouncer and one Spinner in a " +
                    "List<GameObject>, run 4 simulation steps calling Update() on each, then print \"Simulated <count> objects.\"",
                Difficulty: Difficulty.Hard,
                Points: 35,
                StarterCode:
                    "abstract class GameObject\n" +
                    "{\n" +
                    "    public string Id;\n" +
                    "    public double X, Y;\n" +
                    "    public GameObject(string id, double x, double y)\n" +
                    "    {\n" +
                    "        Id = id; X = x; Y = y;\n" +
                    "        Canvas.AddShape(Id, ShapeType(), X, Y);\n" +
                    "    }\n" +
                    "    public abstract string ShapeType();\n" +
                    "    public abstract void Update();\n" +
                    "}\n" +
                    "class Bouncer : GameObject\n" +
                    "{\n" +
                    "    private int direction = 1;\n" +
                    "    public Bouncer(string id, double x, double y) : base(id, x, y) { }\n" +
                    "    public override string ShapeType() { return \"circle\"; }\n" +
                    "    public override void Update()\n" +
                    "    {\n" +
                    "        // move X by 20 * direction, flip direction past 0 or 200, Canvas.MoveTo\n" +
                    "    }\n" +
                    "}\n" +
                    "class Spinner : GameObject\n" +
                    "{\n" +
                    "    public Spinner(string id, double x, double y) : base(id, x, y) { }\n" +
                    "    public override string ShapeType() { return \"square\"; }\n" +
                    "    public override void Update()\n" +
                    "    {\n" +
                    "        // Canvas.SetColor(Id, \"#f472b6\")\n" +
                    "    }\n" +
                    "}\n" +
                    "// Put a Bouncer(\"b1\", 0, 50) and Spinner(\"sp1\", 100, 100) in a List<GameObject>\n" +
                    "// Run 4 steps calling Update() on each\n" +
                    "// Print $\"Simulated {objects.Count} objects.\"\n",
                Example:
                    "abstract class GameObject\n" +
                    "{\n" +
                    "    public string Id;\n" +
                    "    public double X, Y;\n" +
                    "    public GameObject(string id, double x, double y)\n" +
                    "    {\n" +
                    "        Id = id; X = x; Y = y;\n" +
                    "        Canvas.AddShape(Id, ShapeType(), X, Y);\n" +
                    "    }\n" +
                    "    public abstract string ShapeType();\n" +
                    "    public abstract void Update();\n" +
                    "}\n" +
                    "class Bouncer : GameObject\n" +
                    "{\n" +
                    "    private int direction = 1;\n" +
                    "    public Bouncer(string id, double x, double y) : base(id, x, y) { }\n" +
                    "    public override string ShapeType() { return \"circle\"; }\n" +
                    "    public override void Update()\n" +
                    "    {\n" +
                    "        X += 20 * direction;\n" +
                    "        if (X > 200 || X < 0) direction *= -1;\n" +
                    "        Canvas.MoveTo(Id, X, Y);\n" +
                    "    }\n" +
                    "}\n" +
                    "class Spinner : GameObject\n" +
                    "{\n" +
                    "    public Spinner(string id, double x, double y) : base(id, x, y) { }\n" +
                    "    public override string ShapeType() { return \"square\"; }\n" +
                    "    public override void Update()\n" +
                    "    {\n" +
                    "        Canvas.SetColor(Id, \"#f472b6\");\n" +
                    "    }\n" +
                    "}\n" +
                    "List<GameObject> objects = new List<GameObject> { new Bouncer(\"b1\", 0, 50), new Spinner(\"sp1\", 100, 100) };\n" +
                    "for (int step = 0; step < 4; step++)\n" +
                    "{\n" +
                    "    foreach (GameObject obj in objects)\n" +
                    "    {\n" +
                    "        obj.Update();\n" +
                    "    }\n" +
                    "}\n" +
                    "Console.WriteLine($\"Simulated {objects.Count} objects.\");",
                Hints: new List<HintTier>
                {
                    new("The constructor runs ShapeType() before either subclass's own constructor body finishes — that's fine here since ShapeType() just returns a fixed string, but it's worth noticing that abstract methods can be called from the base constructor."),
                    new("Calling obj.Update() in the loop runs whichever subclass's Update() the object actually is — that's polymorphism doing the work; the loop itself never needs to know which kind of object it's touching."),
                    new(
                        "abstract class GameObject\n" +
                        "{\n" +
                        "    public string Id;\n" +
                        "    public double X, Y;\n" +
                        "    public GameObject(string id, double x, double y)\n" +
                        "    {\n" +
                        "        Id = id; X = x; Y = y;\n" +
                        "        Canvas.AddShape(Id, ShapeType(), X, Y);\n" +
                        "    }\n" +
                        "    public abstract string ShapeType();\n" +
                        "    public abstract void Update();\n" +
                        "}\n" +
                        "class Bouncer : GameObject\n" +
                        "{\n" +
                        "    private int direction = 1;\n" +
                        "    public Bouncer(string id, double x, double y) : base(id, x, y) { }\n" +
                        "    public override string ShapeType() { return \"circle\"; }\n" +
                        "    public override void Update()\n" +
                        "    {\n" +
                        "        X += 20 * direction;\n" +
                        "        if (X > 200 || X < 0) direction *= -1;\n" +
                        "        Canvas.MoveTo(Id, X, Y);\n" +
                        "    }\n" +
                        "}\n" +
                        "class Spinner : GameObject\n" +
                        "{\n" +
                        "    public Spinner(string id, double x, double y) : base(id, x, y) { }\n" +
                        "    public override string ShapeType() { return \"square\"; }\n" +
                        "    public override void Update()\n" +
                        "    {\n" +
                        "        Canvas.SetColor(Id, \"#f472b6\");\n" +
                        "    }\n" +
                        "}\n" +
                        "List<GameObject> objects = new List<GameObject> { new Bouncer(\"b1\", 0, 50), new Spinner(\"sp1\", 100, 100) };\n" +
                        "for (int step = 0; step < 4; step++)\n" +
                        "{\n" +
                        "    foreach (GameObject obj in objects)\n" +
                        "    {\n" +
                        "        obj.Update();\n" +
                        "    }\n" +
                        "}\n" +
                        "Console.WriteLine($\"Simulated {objects.Count} objects.\");",
                        IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Simulated 2 objects.",
                CheckSource: source => source.Contains("abstract class GameObject") && source.Contains("Canvas.MoveTo") && source.Contains("Canvas.SetColor")
            ),
            new(
                Id: 3,
                Title: "🎮 Visual: Shape Family",
                Description:
                    "Define abstract class Shape9 with a string Id, a constructor that stores it, and an abstract void Draw(). " +
                    "Define class Circle9 : Shape9 whose Draw() calls Canvas.AddShape(Id, \"circle\", 60, 100). Define class " +
                    "Square9 : Shape9 whose Draw() calls Canvas.AddShape(Id, \"rect\", 160, 100). Put a Circle9(\"c1\") and " +
                    "Square9(\"s1\") in a List<Shape9>, loop through calling Draw() on each, then print \"Drew 2 shapes.\"",
                Difficulty: Difficulty.Hard,
                Points: 25,
                StarterCode:
                    "// abstract class Shape9 { string Id; constructor; abstract void Draw(); }\n" +
                    "// class Circle9 : Shape9 { Draw() -> Canvas.AddShape(Id, \"circle\", 60, 100); }\n" +
                    "// class Square9 : Shape9 { Draw() -> Canvas.AddShape(Id, \"rect\", 160, 100); }\n" +
                    "// List<Shape9> with one of each, loop calling Draw(), print \"Drew 2 shapes.\"\n",
                Example:
                    "abstract class Shape9\n{\n    public string Id;\n    public Shape9(string id) { Id = id; }\n    public abstract void Draw();\n}\nclass Circle9 : Shape9\n{\n    public Circle9(string id) : base(id) { }\n    public override void Draw() => Canvas.AddShape(Id, \"circle\", 60, 100);\n}\nclass Square9 : Shape9\n{\n    public Square9(string id) : base(id) { }\n    public override void Draw() => Canvas.AddShape(Id, \"rect\", 160, 100);\n}\nList<Shape9> shapes = new List<Shape9> { new Circle9(\"c1\"), new Square9(\"s1\") };\nforeach (var shape in shapes)\n{\n    shape.Draw();\n}\nConsole.WriteLine(\"Drew 2 shapes.\");",
                Hints: new List<HintTier>
                {
                    new("Each subclass overrides Draw() differently, but the loop can call shape.Draw() the same way for every item in the list — that's polymorphism."),
                    new("abstract class Shape9\n{\n    public string Id;\n    public Shape9(string id) { Id = id; }\n    public abstract void Draw();\n}\nclass Circle9 : Shape9\n{\n    public Circle9(string id) : base(id) { }\n    public override void Draw() => Canvas.AddShape(Id, \"circle\", 60, 100);\n}\nclass Square9 : Shape9\n{\n    public Square9(string id) : base(id) { }\n    public override void Draw() => Canvas.AddShape(Id, \"rect\", 160, 100);\n}\nList<Shape9> shapes = new List<Shape9> { new Circle9(\"c1\"), new Square9(\"s1\") };\nforeach (var shape in shapes)\n{\n    shape.Draw();\n}\nConsole.WriteLine(\"Drew 2 shapes.\");", IsSolution: true)
                },
                Kind: TaskKind.MiniGame,
                CheckOutput: output => output.Trim() == "Drew 2 shapes.",
                CheckSource: source => source.Contains("abstract class Shape9") && source.Contains("Canvas.AddShape")
            )
        },
        QuizStages: new List<QuizStage>
        {
            new(
                Title: "Inheritance Check-In",
                AfterTaskId: 6,
                QuestionsPerAttempt: 5,
                Cards: new List<QuizCard>
                {
                    new("What keyword lets a class inherit from another class?", new List<string> { "extends", "implements", ": (colon)", "inherit" }, CorrectIndex: 2),
                    new("What does `override` let a subclass do?", new List<string> { "Delete a base method", "Provide its own version of a base method", "Rename a base method", "Hide a field" }, CorrectIndex: 1),
                    new("What keyword must a base method have for a subclass to override it?", new List<string> { "sealed", "virtual (or abstract)", "static", "private" }, CorrectIndex: 1),
                    new("What does `base.MethodName()` call?", new List<string> { "The subclass's own version", "The parent class's version", "A static method", "Nothing, it's invalid" }, CorrectIndex: 1),
                    new("What is an interface used for?", new List<string> { "Storing data", "Promising a set of methods a class must implement", "Looping", "Creating constants" }, CorrectIndex: 1),
                    new("What does an `abstract` class mean?", new List<string> { "It can never be inherited from", "It can't be instantiated directly, only inherited", "It has no methods", "It's the same as an interface" }, CorrectIndex: 1),
                    new("If a list holds several different subclasses of the same base type, what's it called when each responds differently to the same method call?", new List<string> { "Overloading", "Polymorphism", "Encapsulation", "Casting" }, CorrectIndex: 1)
                }
            ),
            new(
                Title: "Inheritance Mastery",
                AfterTaskId: 11,
                Format: QuizFormat.SwipeTrueFalse,
                QuestionsPerAttempt: 10,
                Cards: new List<QuizCard>
                {
                    new("A subclass inherits the fields and methods of its base class.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A method must be marked `virtual` or `abstract` in the base class before a subclass can `override` it.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`base.Attack()` calls the subclass's own version of Attack, not the parent's.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("An abstract class can be created directly with `new`.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("An interface can contain method signatures without implementations that a class must then fill in.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A single list can hold multiple different subclasses that all inherit from the same base type.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("`protected` members are accessible from subclasses but not from unrelated outside code.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Polymorphism means calling the same method name on different objects can produce different behavior.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A class can only implement one interface at a time.", new List<string> { "True", "False" }, CorrectIndex: 1),
                    new("An abstract method has no body in the base class — subclasses must supply one.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("Inheritance lets you reuse and specialize behavior instead of rewriting it in every related class.", new List<string> { "True", "False" }, CorrectIndex: 0),
                    new("A class can inherit from more than one class at once in C#.", new List<string> { "True", "False" }, CorrectIndex: 1)
                }
            )
        }
    );
}
