namespace PatternsLab.Problems.Prototype;

public class Weapon
{
    public string Name { get; set; } = string.Empty;
    public int Damage { get; set; }

    public Weapon Clone() => new() { Name = this.Name, Damage = this.Damage };
}

public abstract class Enemy
{
    private string _modelData;

    public string Name { get; set; } = string.Empty;
    public int Health { get; set; }
    public Weapon Weapon { get; set; } = new();
    public List<string> Abilities { get; set; } = new();
    public string ModelId => _modelData;

    protected Enemy()
    {
        Console.WriteLine("    ...loading 3D model (slow)...");
        Thread.Sleep(500);
        _modelData = "MODEL_" + Guid.NewGuid().ToString("N")[..6];
    }

    //  (R1, R3)
    protected Enemy(Enemy source)
    {
        _modelData = source._modelData; // Copy private field directly
        Name = source.Name;
        Health = source.Health;

        // Deep copy of reference types (R4)
        Weapon = source.Weapon?.Clone() ?? new Weapon();
        Abilities = new List<string>(source.Abilities);
    }

    // R2
    public abstract Enemy Clone();
}

public class Orc : Enemy
{
    public Orc()
    {
        Name = "Orc";
        Health = 100;
        Weapon = new Weapon { Name = "Axe", Damage = 25 };
        Abilities.Add("Rage");
    }

    private Orc(Orc source) : base(source) { }

    public override Enemy Clone() => new Orc(this);
}

public class Elf : Enemy
{
    public Elf()
    {
        Name = "Elf";
        Health = 70;
        Weapon = new Weapon { Name = "Bow", Damage = 18 };
        Abilities.Add("Stealth");
    }

    private Elf(Elf source) : base(source) { }

    public override Enemy Clone() => new Elf(this);
}

public static class EnemyCopyHelper
{
    public static Enemy CopyEnemy(Enemy e)
    {
        // R2
        return e.Clone();
    }
}

// R5  Prototype Registry to store and spawn clones by key
public class EnemyRegistry
{
    private readonly Dictionary<string, Enemy> _prototypes = new(StringComparer.OrdinalIgnoreCase);

    public void Register(string key, Enemy prototype)
    {
        _prototypes[key] = prototype;
    }

    public Enemy Spawn(string key)
    {
        if (_prototypes.TryGetValue(key, out var prototype))
        {
            return prototype.Clone();
        }
        throw new KeyNotFoundException($"Enemy prototype with key '{key}' was not found.");
    }
}