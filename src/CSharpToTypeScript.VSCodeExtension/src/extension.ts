import * as vscode from 'vscode';
import * as cp from 'child_process';
import * as readline from 'readline';
import * as path from 'path';
import { Output } from './output';
import { Input, dateOutputTypes, nullableOutputTypes } from './input';
import { allowedOrDefault, fullRange } from './utilities';

let server: cp.ChildProcess | undefined;
let rl: readline.Interface | undefined;
let serverRunning = false;
let executingCommand = false;

export function activate(context: vscode.ExtensionContext) {
    serverRunning = true;
    server = cp.spawn('dotnet', [context.asAbsolutePath(path.join(
        'server', 'CSharpToTypeScript.Server', 'bin', 'Release', 'netcoreapp2.2', 'publish', 'CSharpToTypeScript.Server.dll'))]);

    server.on('error', err => {
        serverRunning = false;
        vscode.window.showErrorMessage(`"C# to TypeScript" server related error occurred: "${err.message}".`);
    });
    server.on('exit', code => {
        serverRunning = false;
        vscode.window.showWarningMessage(`"C# to TypeScript" server shutdown with code: "${code}".`);
    });

    rl = readline.createInterface(server.stdout, server.stdin);

    context.subscriptions.push(
        vscode.commands.registerCommand('csharpToTypeScript.csharpToTypeScriptReplace', () => convert('document')),
        vscode.commands.registerCommand('csharpToTypeScript.csharpToTypeScriptToClipboard', () => convert('clipboard')));
}

export function deactivate() {
    if (serverRunning && server) {
        server.stdin.write('EXIT\n');
    }
}

export async function convert(target: 'document' | 'clipboard') {
    if (!serverRunning) {
        vscode.window.showErrorMessage(`"C# to TypeScript" server isn't running! Reload Window to restart it.`);
        return;
    }

    if (!vscode.window.activeTextEditor || !rl || executingCommand) {
        return;
    }

    executingCommand = true;

    const document = vscode.window.activeTextEditor.document;
    const selection = vscode.window.activeTextEditor.selection;

    const configuration = vscode.workspace.getConfiguration();

    const input: Input = {
        code: !selection.isEmpty ? document.getText(selection) : document.getText(),
        useTabs: !vscode.window.activeTextEditor.options.insertSpaces,
        tabSize: vscode.window.activeTextEditor.options.tabSize as number,
        export: !!configuration.get('csharpToTypeScript.export'),
        convertDatesTo: allowedOrDefault(configuration.get('csharpToTypeScript.convertDatesTo'), dateOutputTypes, 'string'),
        convertNullablesTo: allowedOrDefault(configuration.get('csharpToTypeScript.convertNullablesTo'), nullableOutputTypes, 'null'),
        toCamelCase: !!configuration.get('csharpToTypeScript.toCamelCase')
    };

    const inputLine = JSON.stringify(input) + '\n';

    rl.question(inputLine, async outputLine => {
        const { convertedCode, succeeded, errorMessage } = JSON.parse(outputLine) as Output;

        if (!succeeded && errorMessage) {
            vscode.window.showErrorMessage(`"C# to TypeScript" extension encountered an error while converting your code: "${errorMessage}".`);
        } else if (convertedCode && target === 'document' && vscode.window.activeTextEditor) {
            await vscode.window.activeTextEditor.edit(
                builder => builder.replace(!selection.isEmpty ? selection : fullRange(document), convertedCode));
        } else if (convertedCode && target === 'clipboard') {
            await vscode.env.clipboard.writeText(convertedCode);
        }

        executingCommand = false;
    });
}