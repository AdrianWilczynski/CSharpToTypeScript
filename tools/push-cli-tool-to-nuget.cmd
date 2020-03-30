pushd ..\src\CSharpToTypeScript.CLITool\nupkg
dotnet nuget push CSharpToTypeScript.CLITool.%1.nupkg -k %2 -s https://api.nuget.org/v3/index.json
popd