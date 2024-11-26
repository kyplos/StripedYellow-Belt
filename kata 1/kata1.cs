namespace kata_1;

class Character
{
    public string Name { get; set; }
    public int Health { get; set; }
    public string Role { get; set; }
    public Action PrimaryAction { get; set; }

    public Character(string name, int health, string role, Action primaryAction)
    {
        Name = name;
        Health = health;
        Role = role;
        PrimaryAction = primaryAction;
    }
}

class kata1
{
    static void Main(string[] args)
    {
        // Create characters
        var warrior1 = new Character("Bran", 30, "Warrior", () => Console.WriteLine("Bran charges with a fierce attack!"));
        var warrior2 = new Character("Arin", 90, "Warrior", () => Console.WriteLine("Arin charges with a fierce attack!"));
        var healer = new Character("Dalia", 70, "Healer", () => { });

        var characters = new List<Character> { warrior1, warrior2, healer };

        Console.WriteLine("Starting actions based on character health...\n");
        
        Console.WriteLine("Characters attacking first (health < 50):");
        foreach (var character in characters.Where(c => c.Health < 50))
        {
            Console.WriteLine($"{character.Name} is attacking first due to low health!");
            Console.WriteLine($"{character.Name} is standing by with health: {character.Health}");
            character.PrimaryAction();
            Console.WriteLine();
        }
        
        Console.WriteLine("Additional character actions based on role:");
        foreach (var character in characters)
        {
            if (character.Role == "Healer")
            {
                var target = characters.Where(c => c.Health < 50).OrderBy(c => c.Health).FirstOrDefault();
                if (target != null)
                {
                    Console.WriteLine($"{character.Name} is prioritizing healing for {target.Name} who has the lowest health.");
                    character.PrimaryAction = () =>
                    {
                        target.Health += 15;
                        Console.WriteLine($"{character.Name} heals {target.Name} for 15 health!");
                        Console.WriteLine($"{target.Name}'s health: {target.Health}");
                    };
                    character.PrimaryAction();
                }
            }
            else if (character.Health >= 50)
            {
                Console.WriteLine($"{character.Name} is standing by with health: {character.Health}");
                character.PrimaryAction();
            }

            Console.WriteLine();
        }
    }
}