# C# to TypeScript

Convert C# Models, ViewModels and DTOs into their TypeScript equivalents.

**Checkout web-based version here: https://csharptotypescript.azurewebsites.net/**

## Requirements

- .NET Core

## Configuration

- Uses VS Code's indentation settings.
- `"csharpToTypeScript.export": true` property controls exporting.

## Know limitations / design choices

- Always outputs interface type.
- Converts names to camel case.
- Drops interface prefix: IType -> Type.
- Only includes public, non-static properties - not fields, not methods, not private members.
- Exports emitted type by default.

