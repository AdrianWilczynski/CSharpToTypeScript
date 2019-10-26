# C# to TypeScript

*Checkout web-based version here: https://csharptotypescript.azurewebsites.net/ and .NET Core CLI Tool here: https://www.nuget.org/packages/CSharpToTypeScript.CLITool/*

Convert C# Models, ViewModels and DTOs into their TypeScript equivalents.

![In Action](https://raw.githubusercontent.com/AdrianWilczynski/CSharpToTypeScript/master/src/CSharpToTypeScript.VSCodeExtension/img/inAction.gif)

## Commands

- `"C# to TypeScript (Replace)"` - converts content of open document (or it's selected part) and replaces it.
- `"C# to TypeScript (Clipboard)"` (*keybinding:* `Alt + /`) - writes converted code to clipboard.

## Requirements

- .NET Core

## Configuration

- Uses VS Code's indentation settings.
- `"csharpToTypeScript.export": true` property controls exporting.

## Known limitations / design choices

- Always outputs interface type.
- Converts names to camel case.
- Drops interface prefix: IType -> Type.
- Only includes public, non-static properties - not fields, not methods, not private members.
- Exports emitted type by default.