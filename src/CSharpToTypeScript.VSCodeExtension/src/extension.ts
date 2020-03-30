import * as cp from 'child_process';
import * as path from 'path';
import * as readline from 'readline';
import * as vscode from 'vscode';
import { Input, dateOutputTypes, nullableOutputTypes, quotationMarks, Configuration } from './input';
import { Output } from './output';
import { allowedOrDefault, fullRange, textFromActiveDocument } from './utilities';
import { TextEncoder } from 'util';

let server: cp.ChildProcess;
let rl: readline.Interface;
let serverRunning = false;
let executingCommand = false;

export function activate(context: vscode.ExtensionContext) {
    let standardError = '';
    serverRunning = true;

    server = cp.spawn('dotnet', [context.asAbsolutePath(path.join(
        'server', 'CSharpToTypeScript.Server', 'bin', 'Release', 'netcoreapp2.2', 'publish', 'CSharpToTypeScript.Server.dll'))]);

    server.on('error', err => {
        serverRunning = false;
        vscode.window.showErrorMessage(`"C# to TypeScript" server related error occurred: "${err.message}".`);
    });
    server.stderr.on('data', data => {
        standardError += data;
    });
    server.on('exit', code => {
        serverRunning = false;
        vscode.window.showWarningMessage(`"C# to TypeScript" server shutdown with code: "${code}". Standard error: "${standardError}".`);
    });

    rl = readline.createInterface(server.stdout, server.stdin);

    context.subscriptions.push(
        vscode.commands.registerCommand('csharpToTypeScript.csharpToTypeScriptReplace', replaceCommand),
        vscode.commands.registerCommand('csharpToTypeScript.csharpToTypeScriptToClipboard', toClipboardCommand),
        vscode.commands.registerCommand('csharpToTypeScript.csharpToTypeScriptPasteAs', pasteAsCommand),
        vscode.commands.registerCommand('csharpToTypeScript.csharpToTypeScriptToFile', toFileCommand));
}

export function deactivate() {
    if (serverRunning) {
        server.stdin.write('EXIT\n');
    }
}

async function replaceCommand() {
    try {
        if (!vscode.window.activeTextEditor) {
            return;
        }

        const result = await convert(textFromActiveDocument());

        const document = vscode.window.activeTextEditor.document;
        const selection = vscode.window.activeTextEditor.selection;

        await vscode.window.activeTextEditor.edit(
            builder => builder.replace(
                !selection.isEmpty ? selection : fullRange(document),
                result.convertedCode));
    }
    catch (error) {
        vscode.window.showWarningMessage((error as Error).message);
    }
}

async function toClipboardCommand() {
    try {
        const result = await convert(textFromActiveDocument());

        await vscode.env.clipboard.writeText(result.convertedCode!);
    }
    catch (error) {
        vscode.window.showWarningMessage((error as Error).message);
    }
}

async function pasteAsCommand() {
    try {
        if (!vscode.window.activeTextEditor) {
            return;
        }

        const result = await convert(await vscode.env.clipboard.readText());

        const selection = vscode.window.activeTextEditor.selection;

        await vscode.window.activeTextEditor.edit(
            builder => builder.replace(
                selection,
                result.convertedCode));
    }
    catch (error) {
        vscode.window.showWarningMessage((error as Error).message);
    }
}

async function toFileCommand(uri?: vscode.Uri) {
    try {
        uri = uri ?? vscode.window.activeTextEditor?.document.uri;
        if (!uri) {
            return;
        }

        const document = await vscode.workspace.openTextDocument(uri);
        const code = document.getText();
        const filePath = uri.path;

        const result = await convert(code, filePath);

        await vscode.workspace.fs.writeFile(
            vscode.Uri.file(path.join(path.dirname(filePath), result.convertedFileName!)),
            new TextEncoder().encode(result.convertedCode));
    }
    catch (error) {
        vscode.window.showWarningMessage((error as Error).message);
    }
}

function convert(code: string, fileName?: string) {
    return new Promise<{ convertedCode: string, convertedFileName?: string }>((resolve, reject) => {
        if (!serverRunning) {
            reject(new Error(`"C# to TypeScript" server isn't running! Reload Window to restart it.`));
            return;
        }

        if (executingCommand) {
            reject(new Error('Conversion in progress...'));
            return;
        }

        executingCommand = true;

        const configuration = vscode.workspace.getConfiguration()
            .get<Configuration>('csharpToTypeScript')!;

        const input: Input = {
            code: code,
            fileName: fileName,
            useTabs: !(vscode.window.activeTextEditor?.options.insertSpaces ?? true),
            tabSize: vscode.window.activeTextEditor?.options.tabSize as number ?? 4,
            export: !!configuration.export,
            convertDatesTo: allowedOrDefault(configuration.convertDatesTo, dateOutputTypes),
            convertNullablesTo: allowedOrDefault(configuration.convertNullablesTo, nullableOutputTypes),
            toCamelCase: !!configuration.toCamelCase,
            removeInterfacePrefix: !!configuration.removeInterfacePrefix,
            generateImports: !!configuration.generateImports,
            useKebabCase: !!configuration.useKebabCase,
            appendModelSuffix: !!configuration.appendModelSuffix,
            quotationMark: allowedOrDefault(configuration.quotationMark, quotationMarks),
            appendNewLine: !!configuration.appendNewLine
        };

        const inputLine = JSON.stringify(input) + '\n';

        rl.question(inputLine, outputLine => {
            const { convertedCode, convertedFileName, succeeded, errorMessage } = JSON.parse(outputLine) as Output;

            if (!succeeded) {
                reject(new Error(`"C# to TypeScript" extension encountered an error while converting your code: "${errorMessage}".`));
            } else {
                resolve({
                    convertedCode: convertedCode ?? '',
                    convertedFileName
                });
            }

            executingCommand = false;
        });
    });
}

