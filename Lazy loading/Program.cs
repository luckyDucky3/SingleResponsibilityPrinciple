namespace Lazy_loading;

public class Configuration
{
    private Dictionary<string, string> PrimarySettings { get; set; }
    private readonly Lazy<SecondarySettings> _optionalSettings = new(() => new SecondarySettings());
    public Configuration()
    {
        Console.WriteLine("Loading configuration...");
        PrimarySettings = new Dictionary<string, string>
        {
            { "Theme", "Dark" },
            { "Language", "English" },
            { "Notifications", "Enabled" }
        };
        Console.WriteLine("Configuration loaded!");
    }

    public string? GetPrimarySetting(string key)
    {
        return PrimarySettings.GetValueOrDefault(key);
    }
    
    public void SetSecondarySettings(Dictionary<string, string> settings)
    {
        _optionalSettings.Value.SecondarySettingsDictionary = new Dictionary<string, string>(settings);
    }
    
    public string? GetSecondarySetting(string key)
    {
        return _optionalSettings.Value.SecondarySettingsDictionary.GetValueOrDefault(key);
    }

    private class SecondarySettings
    
    {
        public Dictionary<string, string> SecondarySettingsDictionary { get; set; }
        public SecondarySettings()
        {
            Thread.Sleep(5000);
            SecondarySettingsDictionary = new Dictionary<string, string>();
        }
    }
}

public class Application
{
    private readonly Configuration _config  = new();

    public void Run()
    {
        Console.WriteLine("Приложение запущено");

        Console.WriteLine("Использование конфигурации:");
        Console.WriteLine($"Theme: {_config.GetPrimarySetting("Theme")}");
        Console.WriteLine($"Language: {_config.GetPrimarySetting("Language")}");
    }
}

internal static class Program
{
    private static void Main(string[] args)
    {
        var app = new Application();
        app.Run();
    }
}