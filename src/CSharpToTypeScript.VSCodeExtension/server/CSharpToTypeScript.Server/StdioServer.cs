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
        private readonly IFileNameConverter _fileNameConverter;
        private readonly IStdio _stdio;

        public StdioServer(ICodeConverter codeConverter, IStdio stdio, IFileNameConverter fileNameConverter)
        {
            _codeConverter = codeConverter;
            _stdio = stdio;
            _fileNameConverter = fileNameConverter;
        }

        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new[] { new StringEnumConverter() }
        };

        public void Handle()
        {
            while (_stdio.ReadLine() is var inputLine && inputLine != "EXIT")
            {
                Output output;

                try
                {
                    var input = JsonConvert.DeserializeObject<Input>(inputLine, _serializerSettings);

                    var codeConversionOptions = input.MapToCodeConversionOptions();

                    var convertedCode = _codeConverter.ConvertToTypeScript(input.Code, codeConversionOptions);
                    var convertedFileName = string.IsNullOrWhiteSpace(input.FileName)
                        ? null
                        : _fileNameConverter.ConvertToTypeScript(input.FileName, codeConversionOptions);

                    output = new Output { Succeeded = true, ConvertedCode = convertedCode, ConvertedFileName = convertedFileName };
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