using System;
using CSharpToTypeScript.Core.Services;
using CSharpToTypeScript.Server.DTOs;
using Newtonsoft.Json;

namespace CSharpToTypeScript.Server
{
    public static class Program
    {
        public static void Main(string[] _)
        {
            var converter = new CodeConverter();

            string inputLine;
            while ((inputLine = Console.ReadLine()) != "EXIT")
            {
                var input = JsonConvert.DeserializeObject<Input>(inputLine);

                var convertedCode = converter.ConvertToTypeScript(
                    input.Code, input.UseTabs, input.TabSize, input.Export);

                var output = new Output { Code = convertedCode };
                var outputLine = JsonConvert.SerializeObject(output);

                Console.WriteLine(outputLine);
            }
        }
    }
}
