using System.Diagnostics;
using PatternsLab.Problems.Builder;
using PatternsLab.Problems.Prototype;
using PatternsLab.Problems.Singleton;

Console.WriteLine("=== SINGLETON: AFTER ===\n");

var db = new DatabaseService();
var ui = new UiService();

Console.WriteLine();

ui.Config.Theme = "Dark";
Console.WriteLine("Admin changed theme to Dark.");

ui.Render();

Console.WriteLine($"\nSame config object? {ReferenceEquals(db.Config, ui.Config)}");
Console.WriteLine($"Times config was loaded from disk: {AppConfig.LoadCount}");

bool singletonPassed = ReferenceEquals(db.Config, ui.Config) && AppConfig.LoadCount == 1 && db.Config.Theme == "Dark";
Console.WriteLine(singletonPassed ? " Singleton PASSED!" : " Singleton FAILED!");


Console.WriteLine("\n=== PROTOTYPE: AFTER ===\n");

var sw = Stopwatch.StartNew();
var army = new List<Enemy>();

// 1. Create original prototype (takes slow loading once)
var prototypeOrc = new Orc();

// 2. Fast cloning using Prototype pattern
for (int i = 1; i <= 5; i++)
{
    var orc = prototypeOrc.Clone();
    orc.Name = $"Orc-{i}";
    army.Add(orc);
}
sw.Stop();
Console.WriteLine($"Created 5 orcs via cloning in {sw.ElapsedMilliseconds} ms (Should be ~0ms)\n");

Enemy original = prototypeOrc;
original.Name = "Boss Orc";
Enemy copy = EnemyCopyHelper.CopyEnemy(original);

Console.WriteLine($"Original model id: {original.ModelId}");
Console.WriteLine($"Copy model id:     {copy.ModelId}");

copy.Weapon.Damage = 999;
Console.WriteLine($"\nWe changed the COPY's weapon damage to 999.");
Console.WriteLine($"Original's weapon damage is still: {original.Weapon.Damage}");

copy.Abilities.Add("Fire Breath");
Console.WriteLine($"Original abilities: {string.Join(", ", original.Abilities)}");

bool prototypePassed = original.ModelId == copy.ModelId
    && !ReferenceEquals(original, copy)
    && original.Weapon.Damage != copy.Weapon.Damage
    && original.Abilities.Count != copy.Abilities.Count;

Console.WriteLine(prototypePassed ? " Prototype PASSED!" : " Prototype FAILED!");


Console.WriteLine("\n=== BUILDER: AFTER ===\n");

try
{
    var liveStudent = RegistrationCallSites.CreateLiveStudent();
    var videoStudent = RegistrationCallSites.CreateVideosOnly();

    Console.WriteLine(liveStudent);
    Console.WriteLine(videoStudent);

    bool builderPassed = liveStudent.AccessMode == "LiveGroup"
        && liveStudent.GroupCode == "G1"
        && videoStudent.AccessMode == "VideosOnly"
        && videoStudent.GroupCode == null;

    Console.WriteLine(builderPassed ? " Builder PASSED!" : " Builder FAILED!");
}
catch (Exception ex)
{
    Console.WriteLine($" Builder Error: {ex.Message}");
}