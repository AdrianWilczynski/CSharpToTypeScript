pushd ..\src\CSharpToTypeScript.CLITool
dotnet pack
dotnet tool install --global --add-source ./nupkg CSharpToTypeScript.CLITool
popd