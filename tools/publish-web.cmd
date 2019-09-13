pushd ..\src\CSharpToTypeScript.Web
libman restore
call tsc
dotnet publish -c Release
popd