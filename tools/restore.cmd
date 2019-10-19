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