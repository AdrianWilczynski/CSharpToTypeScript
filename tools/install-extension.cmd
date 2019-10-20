pushd ..\src\CSharpToTypeScript.VSCodeExtension
call vsce package
call code --install-extension csharp-to-typescript-%1.vsix
popd