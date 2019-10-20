pushd ..\src\CSharpToTypeScript.VSCodeExtension
call vsce package
code --install-extension csharp-to-typescript-%1.vsix
popd