# C# to TypeScript

*Checkout web-based version here: https://csharptotypescript.azurewebsites.net/ and .NET Core CLI Tool here: https://www.nuget.org/packages/CSharpToTypeScript.CLITool/*

Convert C# Models, ViewModels and DTOs into their TypeScript equivalents.

![In Action](https://raw.githubusercontent.com/AdrianWilczynski/CSharpToTypeScript/master/src/CSharpToTypeScript.VSCodeExtension/img/inAction.gif)

## Commands

- `"C# to TypeScript (Replace)"` - converts content of open document (or it's selected part) and replaces it.
- `"C# to TypeScript (Clipboard)"` (*keybinding:* `Alt + /`) - writes converted code to clipboard.

## Requirements

- .NET Core (2.2 or newer)

## Configuration

- Uses VS Code's indentation settings.
- `"csharpToTypeScript.export": true` controls exporting.
- `"csharpToTypeScript.convertDatesTo": string` sets output type for dates. You can pick between `string`, `Date` and `string | Date`.
- `"csharpToTypeScript.convertNullablesTo": null` sets output type for nullables (`int?`) to either `null` or `undefined`.
- `"csharpToTypeScript.toCamelCase": true` toggles field name conversion to camel case.
- `"csharpToTypeScript.removeInterfacePrefix": true` controls whether to remove interface prefixes (`IType` -> `Type`).
- `"csharpToTypeScript.generateImports": false` toggles simple import statements generation.
    - `"csharpToTypeScript.useKebabCase": false` - use kebab case for module names in import statements.
    - `"csharpToTypeScript.appendModelSuffix": false` - append ".model" suffix to module names in import statements.

## Known limitations / design choices

- Always outputs interface type.
- Only includes public, non-static properties - not fields, not methods, not private members.
- Import generation assumes flat output directory structure and file names corresponding to type names (e.g. `MyType`: `myType.ts`, `my-type.ts`, `my-type.model.ts`). 