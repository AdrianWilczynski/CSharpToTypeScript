pushd ..\src\CSharpToTypeScript.Web
libman restore
call tsc
dotnet build
popd

pushd ..\src\CSharpToTypeScript.CLITool
dotnet build
popd

pushd ..\src\CSharpToTypeScript.VSCodeExtension
call npm install
call npm run compile-server
popd

pushd ..\src\CSharpToTypeScript.Blazor
dotnet build
popd

pushd ..\test
dotnet build CSharpToTypeScript.CLITool.Tests
dotnet build CSharpToTypeScript.Core.Tests
dotnet build CSharpToTypeScript.VSCodeExtension.Server.Tests
popd

pushd ..\samples
dotnet build RunOnBuild
popd