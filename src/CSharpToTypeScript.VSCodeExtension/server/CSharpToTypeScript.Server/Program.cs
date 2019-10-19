using CSharpToTypeScript.Core.DI;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Server.Services;

namespace CSharpToTypeScript.Server
{
    public static class Program
    {
        public static void Main(string[] _)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var services = new ServiceCollection()
                .AddCSharpToTypeScript()
                .AddTransient<IStdio, Stdio>()
                .AddTransient<Server>();

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<Server>()
                .Handle();
        }
    }
}
