pushd ..\src\CSharpToTypeScript.CLITool
dotnet pack -c Release
dotnet tool uninstall --global CSharpToTypeScript.CLITool
dotnet tool install --no-cache --global --add-source ./nupkg CSharpToTypeScript.CLITool
popd