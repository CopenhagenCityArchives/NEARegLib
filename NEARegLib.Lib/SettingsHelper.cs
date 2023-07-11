using System.IO;
using System.Text.Json;

namespace NEARegLib
{
    // Class that loads settings from an external json file
    public class SettingsLoader
    {
        public static Settings Load()
        {
            return JsonSerializer.Deserialize<Settings>(File.ReadAllText(Properties.Resources.settingsLocation));
        }
    }

    public class Settings
    {
        public string DbHost { get; set; }
        public int DbPort { get; set; }
        public string DbName { get; set; }
        public string DbUserName { get; set; }
        public string DbPassword { get; set; }
    }

    public class ConnectionStringFactory
    {
        public static string GetConnectionString(Settings settings)
        {
            return $"Server={settings.DbHost};Database={settings.DbName};Port={settings.DbPort};Uid={settings.DbUserName};Pwd={settings.DbPassword};Convert Zero Datetime=True;DefaultCommandTimeout=3600;Connect Timeout=10;SslMode=None;AllowPublicKeyRetrieval=True;";
        }
    }
}