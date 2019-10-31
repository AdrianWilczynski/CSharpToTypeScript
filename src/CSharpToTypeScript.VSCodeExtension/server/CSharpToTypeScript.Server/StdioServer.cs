using System;
using CSharpToTypeScript.Core.Services;
using CSharpToTypeScript.Server.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Server.Services;

namespace CSharpToTypeScript.Server
{
    public class StdioServer
    {
        private readonly ICodeConverter _codeConverter;
        private readonly IStdio _stdio;

        public StdioServer(ICodeConverter codeConverter, IStdio stdio)
        {
            _codeConverter = codeConverter;
            _stdio = stdio;
        }

        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new[] { new StringEnumConverter() }
        };

        public void Handle()
        {
            string inputLine;
            while ((inputLine = _stdio.ReadLine()) != "EXIT")
            {
                Output output;

                try
                {
                    var input = JsonConvert.DeserializeObject<Input>(inputLine, _serializerSettings);

                    var convertedCode = _codeConverter.ConvertToTypeScript(input.Code, input.MapToCodeConversionOptions());

                    output = new Output { Succeeded = true, ConvertedCode = convertedCode };
                }
                catch (Exception ex)
                {
                    output = new Output { Succeeded = false, ErrorMessage = ex.Message };
                }

                var outputLine = JsonConvert.SerializeObject(output, _serializerSettings);

                _stdio.WriteLine(outputLine);
            }
        }
    }
}