namespace kata_2;

public class Character
{
    public string Name { get; set; }
    public int Health { get; private set; }

    public delegate void CharacterAction(Character target, int amount);

    public event Action<int> HealthChanged;

    public CharacterAction SpecialAction { get; set; }

    public Character(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public void Attack(Character target, int damage)
    {
        if (damage < 0) throw new ArgumentException("Damage cannot be negative.");
        Console.WriteLine($"{Name} attacks {target.Name} for {damage} damage.");
        target.ChangeHealth(-damage);
    }

    public void Heal(Character target, int amount)
    {
        if (amount <= 0) throw new ArgumentException("Healing amount must be positive.");
        Console.WriteLine($"{Name} heals {target.Name} for {amount} health.");
        target.ChangeHealth(amount);
    }

    public void PerformSpecialAction(Character target, int amount)
    {
        if (SpecialAction == null) throw new InvalidOperationException("No special action assigned.");
        SpecialAction.Invoke(target, amount);
    }

    public void ChangeHealth(int amount)
    {
        Health += amount;
        Health = Math.Max(0, Health);
        HealthChanged?.Invoke(Health);
    }
}

class kata2
{
    static void Main(string[] args)
    {
        var arin = new Character("Arin", 100);
        var dalia = new Character("Dalia", 100);

        arin.HealthChanged += (newHealth) => Console.WriteLine($"[Event] Arin's health changed to {newHealth}.");
        dalia.HealthChanged += (newHealth) => Console.WriteLine($"[Event] Dalia's health changed to {newHealth}.");

        Console.WriteLine("Arin attacks Dalia for 10 damage.");
        arin.Attack(dalia, 10);

        Console.WriteLine("Dalia heals herself for 15 health.");
        dalia.Heal(dalia, 15);

        Console.WriteLine("Arin performs a special action!");
        arin.SpecialAction = (target, amount) =>
        {
            Console.WriteLine($"{arin.Name} unleashes a powerful special attack on {target.Name} for {amount} damage!");
            target.ChangeHealth(-amount);
        };
        arin.PerformSpecialAction(dalia, 25); 
    }
}
