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
call npm install
call npx webpack
popd

pushd ..\test
dotnet build CSharpToTypeScript.CLITool.Tests
dotnet build CSharpToTypeScript.Core.Tests
dotnet build CSharpToTypeScript.VSCodeExtension.Server.Tests
dotnet build CSharpToTypeScript.Blazor.Tests
dotnet build CSharpToTypeScript.Web.Tests
popd

pushd ..\samples
dotnet build RunOnBuild
popd