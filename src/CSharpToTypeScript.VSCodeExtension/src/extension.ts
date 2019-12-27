import * as cp from 'child_process';
import * as path from 'path';
import * as readline from 'readline';
import * as vscode from 'vscode';
import { Input, dateOutputTypes, nullableOutputTypes, quotationMarks, Configuration } from './input';
import { Output } from './output';
import { allowedOrDefault, fullRange, textFromActiveDocument } from './utilities';
import { TextEncoder } from 'util';

let server: cp.ChildProcess | undefined;
let rl: readline.Interface | undefined;
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
    if (serverRunning && server) {
        server.stdin.write('EXIT\n');
    }
}

async function replaceCommand() {
    const code = textFromActiveDocument();

    await convert(code, async convertedCode => {
        if (!vscode.window.activeTextEditor) {
            return;
        }

        const document = vscode.window.activeTextEditor.document;
        const selection = vscode.window.activeTextEditor.selection;

        await vscode.window.activeTextEditor.edit(
            builder => builder.replace(!selection.isEmpty ? selection : fullRange(document), convertedCode));
    });
}

async function toClipboardCommand() {
    const code = textFromActiveDocument();

    await convert(code, async convertedCode => {
        await vscode.env.clipboard.writeText(convertedCode);
    });
}

async function pasteAsCommand() {
    const code = await vscode.env.clipboard.readText();

    await convert(code, async convertedCode => {
        if (!vscode.window.activeTextEditor) {
            return;
        }

        const selection = vscode.window.activeTextEditor.selection;

        await vscode.window.activeTextEditor.edit(
            builder => builder.replace(selection, convertedCode));
    });
}

async function toFileCommand(uri?: vscode.Uri) {
    uri = uri ?? vscode.window.activeTextEditor?.document.uri;
    if (!uri) {
        return;
    }

    const document = await vscode.workspace.openTextDocument(uri);
    const code = document.getText();
    const filePath = uri.path;

    await convert(code, async (convertedCode, convertedFileName) => {
        if (!convertedFileName) {
            return;
        }

        await vscode.workspace.fs.writeFile(
            vscode.Uri.file(path.join(path.dirname(filePath), convertedFileName)),
            new TextEncoder().encode(convertedCode));
    }, filePath);
}

async function convert(code: string, onConverted: (convertedCode: string, fileName?: string) => Promise<void>, fileName?: string) {
    if (!serverRunning) {
        vscode.window.showErrorMessage(`"C# to TypeScript" server isn't running! Reload Window to restart it.`);
        return;
    }

    if (!vscode.window.activeTextEditor || !rl || executingCommand) {
        return;
    }

    executingCommand = true;

    const configuration = vscode.workspace.getConfiguration()
        .get<Configuration>('csharpToTypeScript')!;

    const input: Input = {
        code: code,
        fileName: fileName,
        useTabs: !vscode.window.activeTextEditor.options.insertSpaces,
        tabSize: vscode.window.activeTextEditor.options.tabSize as number,
        export: !!configuration.export,
        convertDatesTo: allowedOrDefault(configuration.convertDatesTo, dateOutputTypes),
        convertNullablesTo: allowedOrDefault(configuration.convertNullablesTo, nullableOutputTypes),
        toCamelCase: !!configuration.toCamelCase,
        removeInterfacePrefix: !!configuration.removeInterfacePrefix,
        generateImports: !!configuration.generateImports,
        useKebabCase: !!configuration.useKebabCase,
        appendModelSuffix: !!configuration.appendModelSuffix,
        quotationMark: allowedOrDefault(configuration.quotationMark, quotationMarks)
    };

    const inputLine = JSON.stringify(input) + '\n';

    rl.question(inputLine, async outputLine => {
        const { convertedCode, convertedFileName: fileName, succeeded, errorMessage } = JSON.parse(outputLine) as Output;

        if (!succeeded) {
            if (errorMessage) {
                vscode.window.showErrorMessage(`"C# to TypeScript" extension encountered an error while converting your code: "${errorMessage}".`);
            } else {
                vscode.window.showErrorMessage(`"C# to TypeScript" extension encountered an unknown error while converting your code.`);
            }
        } else if (!convertedCode) {
            vscode.window.showWarningMessage(`Nothing to convert - C# to TypeScript conversion resulted in an empty string.`);
        } else {
            await onConverted(convertedCode, fileName);
        }

        executingCommand = false;
    });
}

