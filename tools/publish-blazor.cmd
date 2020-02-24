pushd ..\src\CSharpToTypeScript.Blazor
call npm install
call npx webpack --mode production
dotnet publish -c Release
popd