using System;
using CSharpToTypeScript.Core.Services;
using CSharpToTypeScript.Server.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CSharpToTypeScript.Server
{
    public static class Program
    {
        public static void Main(string[] _)
        {
            var converter = new CodeConverter();

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string inputLine;
            while ((inputLine = Console.ReadLine()) != "EXIT")
            {
                Output output;

                try
                {
                    var input = JsonConvert.DeserializeObject<Input>(inputLine);

                    var convertedCode = converter.ConvertToTypeScript(
                        input.Code, input.UseTabs, input.TabSize, input.Export);

                    output = new Output { ConvertedCode = convertedCode, Succeeded = true };
                }
                catch
                {
                    output = new Output { Succeeded = false };
                }

                var outputLine = JsonConvert.SerializeObject(output);

                Console.WriteLine(outputLine);
            }
        }
    }
}
