pushd ..\src\CSharpToTypeScript.Web
libman restore
call tsc
dotnet build
popd