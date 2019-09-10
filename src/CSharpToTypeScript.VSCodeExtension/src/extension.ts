import * as vscode from 'vscode';
import * as cp from 'child_process';
import * as readline from 'readline';
import * as path from 'path';

let server: cp.ChildProcess | undefined;
let running = false;
let executingCommand = false;

export function activate(context: vscode.ExtensionContext) {
    running = true;
    server = cp.spawn('dotnet', [context.asAbsolutePath(
        path.join('server', 'CSharpToTypeScript.Server', 'bin', 'Release',
            'netcoreapp2.2', 'publish', 'CSharpToTypeScript.Server.dll'))]);

    server.on('error', err => {
        running = false;
        vscode.window.showErrorMessage(`"C# to TypeScript" server related error occurred: "${err.message}".`);
    });
    server.on('exit', code => {
        running = false;
        vscode.window.showWarningMessage(`"C# to TypeScript" server shutdown with code: "${code}".`);
    });

    const rl = readline.createInterface(server.stdout, server.stdin);

    const csharpToTypeScript = async (target: 'document' | 'clipboard') => {
        if (!vscode.window.activeTextEditor || !rl) {
            return;
        }

        if (!running) {
            vscode.window.showErrorMessage(`"C# to TypeScript" server isn't running!`);
            return;
        }

        if (executingCommand) {
            return;
        }
        executingCommand = true;

        const document = vscode.window.activeTextEditor.document;
        const selection = vscode.window.activeTextEditor.selection;
        const fullRange = new vscode.Range(
            0, 0,
            document.lineCount - 1, document.lineAt(document.lineCount - 1).range.end.character);

        const code = !selection.isEmpty ? document.getText(selection) : document.getText();

        const tabSize = vscode.window.activeTextEditor.options.tabSize as number;
        const useTabs = !vscode.window.activeTextEditor.options.insertSpaces;
        const addExport = !!vscode.workspace.getConfiguration().get('csharpToTypeScript.export');

        const input = {
            Code: code,
            UseTabs: useTabs,
            TabSize: tabSize,
            Export: addExport
        };
        const inputLine = JSON.stringify(input) + '\n';

        rl.question(inputLine, async outputLine => {
            const convertedCode = JSON.parse(outputLine).Code;

            if (target === 'document' && vscode.window.activeTextEditor) {
                await vscode.window.activeTextEditor.edit(
                    builder => builder.replace(!selection.isEmpty ? selection : fullRange, convertedCode));
            } else if (target === 'clipboard') {
                await vscode.env.clipboard.writeText(convertedCode);
            }

            executingCommand = false;
        });
    };

    context.subscriptions.push(
        vscode.commands.registerCommand('csharpToTypeScript.csharpToTypeScriptReplace', () => csharpToTypeScript('document')),
        vscode.commands.registerCommand('csharpToTypeScript.csharpToTypeScriptToClipboard', () => csharpToTypeScript('clipboard')));
}

export function deactivate() {
    if (running && server) {
        server.stdin.write('EXIT\n');
    }
}