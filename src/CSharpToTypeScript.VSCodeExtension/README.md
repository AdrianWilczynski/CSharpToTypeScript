# C# to TypeScript

*Checkout other versions:* 
- *Server-side web app* - [Azure](https://csharptotypescript.azurewebsites.net/ )
- *Client-side web app* - [GitHub Pages](https://adrianwilczynski.github.io/CSharpToTypeScript/)
- *CLI (.NET Core Global Tool)* - [NuGet](https://www.nuget.org/packages/CSharpToTypeScript.CLITool/), [GitHub](https://github.com/AdrianWilczynski/CSharpToTypeScript/tree/master/src/CSharpToTypeScript.CLITool) 

Convert C# Models, ViewModels and DTOs into their TypeScript equivalents.

![In Action](https://raw.githubusercontent.com/AdrianWilczynski/CSharpToTypeScript/master/src/CSharpToTypeScript.VSCodeExtension/img/inAction.gif)

![Conversion Example](https://raw.githubusercontent.com/AdrianWilczynski/CSharpToTypeScript/master/src/CSharpToTypeScript.VSCodeExtension/img/example.png)

## Commands

- `"C# to TypeScript (Replace)"` - converts content of open document (or it's selected part) and replaces it.
- `"C# to TypeScript (To Clipboard)"` (*keybinding:* `Alt + /`) - writes converted code to clipboard.
- `"C# to TypeScript (Paste As)"` (*keybinding:* `Alt + .`, *editor context menu*) - converts content of clipboard and pastes it.
- `"C# to TypeScript (To File)"` (*explorer context menu*) - converts picked file into new file.

## Requirements

- .NET Core (2.2 or newer)

## Configuration

- Uses VS Code's indentation settings.
- `"csharpToTypeScript.export": true` controls exporting.
- `"csharpToTypeScript.convertDatesTo": "string"` sets output type for dates. You can pick between `string`, `Date` and `string | Date`.
- `"csharpToTypeScript.convertNullablesTo": "null"` sets output type for nullables (`int?`) to either `null` or `undefined`.
- `"csharpToTypeScript.toCamelCase": true` toggles field name conversion to camel case.
- `"csharpToTypeScript.removeInterfacePrefix": true` controls whether to remove interface prefixes (`IType` -> `Type`).
- `"csharpToTypeScript.generateImports": false` toggles simple import statements generation.
- `"csharpToTypeScript.quotationMark": "double"` sets quotation marks for import statements & identifiers (`double` or `single`).
- `"csharpToTypeScript.useKebabCase": false` - use kebab case for file names.
- `"csharpToTypeScript.appendModelSuffix": false` - append ".model" suffix to file names.

## Known limitations / design choices

- Always outputs interface type.
- Only includes public, non-static properties & fields - not methods, not private members.
- Import generation assumes flat output directory structure and file names corresponding to type names (e.g. `MyType`: `myType.ts`, `my-type.ts`, `my-type.model.ts`). 