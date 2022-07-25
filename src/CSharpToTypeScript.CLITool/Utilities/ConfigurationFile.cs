using System.IO;
using System.Text.Json;

namespace CSharpToTypeScript.CLITool.Utilities
{
    public static class ConfigurationFile
    {
        private static JsonSerializerOptions JsonSerializerOptions => new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public const string FileName = "cs2tsconfig.json";

        public static Configuration Load()
            => File.Exists(FileName)
            ? JsonSerializer.Deserialize<Configuration>(File.ReadAllText(FileName), JsonSerializerOptions)
            : null;

        public static void Create(Configuration configuration)
            => File.WriteAllText(FileName, JsonSerializer.Serialize(configuration, JsonSerializerOptions));
    }
}