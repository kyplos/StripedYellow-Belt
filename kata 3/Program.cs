namespace kata_3;

public interface IAbility
{
    string Name { get; }
    string Effect { get; }
}

public class AttackAbility : IAbility
{
    public string Name { get; private set; }
    public string Effect { get; private set; }

    public AttackAbility(string name, string effect)
    {
        Name = name;
        Effect = effect;
    }
}

public class HealAbility : IAbility
{
    public string Name { get; private set; }
    public string Effect { get; private set; }

    public HealAbility(string name, string effect)
    {
        Name = name;
        Effect = effect;
    }
}

public class AbilityContainer<T> where T : IAbility
{
    private readonly List<T> abilities = new List<T>();

    public void AddAbility(T ability)
    {
        abilities.Add(ability);
        Console.WriteLine($"- Added {ability.Name}: {ability.Effect}");
    }

    public void RemoveAbility(T ability)
    {
        if (abilities.Remove(ability))
        {
            Console.WriteLine($"- Removed {ability.Name}");
        }
        else
        {
            Console.WriteLine($"- Ability {ability.Name} not found.");
        }
    }

    public IEnumerable<T> GetAbilities()
    {
        return abilities;
    }

    public void DisplayAbilities()
    {
        Console.WriteLine("\nListing all abilities in the container:");
        foreach (var ability in abilities)
        {
            Console.WriteLine($"- {ability.Name} (Effect: {ability.Effect})");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var slashAttack = new AttackAbility("Slash Attack", "Deals 15 damage");
        var healingLight = new HealAbility("Healing Light", "Restores 20 health");
        
        var abilityContainer = new AbilityContainer<IAbility>();
        
        Console.WriteLine("Adding abilities to the container...");
        abilityContainer.AddAbility(slashAttack);
        abilityContainer.AddAbility(healingLight);
        
        abilityContainer.DisplayAbilities();
        
        Console.WriteLine("\nRemoving an ability...");
        abilityContainer.RemoveAbility(slashAttack);
        
        abilityContainer.DisplayAbilities();
    }
}