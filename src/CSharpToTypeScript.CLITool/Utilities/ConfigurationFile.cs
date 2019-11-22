using System.IO;
using CSharpToTypeScript.CLITool.Options;
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
            Converters = new[] { new StringEnumConverter() }
        };

        public const string FileName = "cs2tsconfig.json";

        public static ConfigurationFileOptions Load()
            => File.Exists(FileName)
            ? JsonConvert.DeserializeObject<ConfigurationFileOptions>(File.ReadAllText(FileName), JsonSerializerSettings)
            : null;

        public static void Create(ConfigurationFileOptions configurationFileOptions)
            => File.WriteAllText(FileName, JsonConvert.SerializeObject(configurationFileOptions, JsonSerializerSettings));
    }
}