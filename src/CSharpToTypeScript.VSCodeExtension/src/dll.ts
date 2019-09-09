import { join } from 'path';

export const path = join('backend', 'CSharpToTypeScript.Console', 'bin', 'Release',
    'netcoreapp2.2', 'publish', 'CSharpToTypeScript.Console.dll');

export function args(code: string, useTabs: boolean, tabSize: number, addExport: boolean) {
    return [code, useTabs + '', tabSize + '', addExport + ''];
}