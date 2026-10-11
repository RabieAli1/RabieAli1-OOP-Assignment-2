namespace PatternsLab.Problems.Singleton;

public class AppConfig
{
    // R1 & R4
    private static readonly Lazy<AppConfig> _instance = new(() => new AppConfig());

    public static int LoadCount;

    // R1
    public static AppConfig Instance => _instance.Value;

    public string DbConnection { get; set; }
    public string Theme { get; set; }

    // R5 Private constructor
    private AppConfig()
    {
        LoadCount++;
        Console.WriteLine($"[AppConfig] Loading settings from disk... (load #{LoadCount})");
        Thread.Sleep(300); // R3
        DbConnection = "Server=localhost;Db=School";
        Theme = "Light";
    }
}

public class DatabaseService
{
    // R2
    public AppConfig Config => AppConfig.Instance;

    public void Connect() => Console.WriteLine($"Connecting to {Config.DbConnection}");
}

public class UiService
{
    // R2: Uses the same single shared instance
    public AppConfig Config => AppConfig.Instance;

    public void Render() => Console.WriteLine($"UI is using theme: {Config.Theme}");
}