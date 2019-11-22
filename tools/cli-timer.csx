#pragma warning disable RCS1018

#r "nuget: Microsoft.Extensions.DependencyInjection, 2.2.0"
#r "..\src\CSharpToTypeScript.CLITool\bin\Debug\netcoreapp2.2\CSharpToTypeScript.Core.dll"
#r "..\src\CSharpToTypeScript.CLITool\bin\Debug\netcoreapp2.2\CSharpToTypeScript.CLITool.dll"

using Microsoft.Extensions.DependencyInjection;
using CSharpToTypeScript.CLITool;
using CSharpToTypeScript.Core.DependencyInjection;
using CSharpToTypeScript.CLITool.Commands;

var convertCommand = new ServiceCollection()
    .AddCSharpToTypeScript()
    .AddTransient<ConvertCommand>()
    .BuildServiceProvider()
    .GetRequiredService<ConvertCommand>();

var count = int.Parse(Args[0]);

const string tempDirectoryPath = "temp";
Directory.CreateDirectory(tempDirectoryPath);

for (int i = 0; i < count; i++)
{
    File.WriteAllText(Path.Join(tempDirectoryPath, i + ".cs"), "class Item { }");
}

var stopwatch = Stopwatch.StartNew();

convertCommand.Input = tempDirectoryPath;
convertCommand.OnExecute();

stopwatch.Stop();

WriteLine($"Elapsed: {stopwatch.ElapsedMilliseconds}");

Directory.Delete(tempDirectoryPath, true);
