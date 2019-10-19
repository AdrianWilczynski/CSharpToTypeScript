using System;
using CSharpToTypeScript.Core.Services;
using CSharpToTypeScript.Server.DTOs;
using Newtonsoft.Json;
using Server.Services;

namespace CSharpToTypeScript.Server
{
    public class Server
    {
        private readonly ICodeConverter _codeConverter;
        private readonly IStdio _stdio;

        public Server(ICodeConverter codeConverter, IStdio stdio)
        {
            _codeConverter = codeConverter;
            _stdio = stdio;
        }

        public void Handle()
        {
            string inputLine;
            while ((inputLine = _stdio.ReadLine()) != "EXIT")
            {
                Output output;

                try
                {
                    var input = JsonConvert.DeserializeObject<Input>(inputLine);

                    var convertedCode = _codeConverter.ConvertToTypeScript(input.Code, input.MapToCodeConversionOptions());

                    output = new Output { Succeeded = true, ConvertedCode = convertedCode };
                }
                catch (Exception ex)
                {
                    output = new Output { Succeeded = false, ErrorMessage = ex.Message };
                }

                var outputLine = JsonConvert.SerializeObject(output);

                _stdio.WriteLine(outputLine);
            }
        }
    }
}