using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CSharpToTypeScript.CLITool.Utilities
{
    public static class ConfigurationFile
    {
        private static JsonSerializerSettings JsonSerializerSettings => new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented,
            Converters = new[] { new StringEnumConverter(new CamelCaseNamingStrategy()) }
        };

        public const string FileName = "cs2tsconfig.json";

        public static Configuration Load()
            => File.Exists(FileName)
            ? JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(FileName), JsonSerializerSettings)
            : null;

        public static void Create(Configuration configuration)
            => File.WriteAllText(FileName, JsonConvert.SerializeObject(configuration, JsonSerializerSettings));
    }
}