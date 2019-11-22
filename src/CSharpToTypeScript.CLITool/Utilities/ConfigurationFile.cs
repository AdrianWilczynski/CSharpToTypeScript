using System.IO;
using CSharpToTypeScript.CLITool.Arguments;
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

        public static ConfigurationFileArguments Load()
            => File.Exists(FileName)
            ? JsonConvert.DeserializeObject<ConfigurationFileArguments>(File.ReadAllText(FileName), JsonSerializerSettings)
            : null;

        public static void Create(ConfigurationFileArguments configuration)
            => File.WriteAllText(FileName, JsonConvert.SerializeObject(configuration, JsonSerializerSettings));
    }
}