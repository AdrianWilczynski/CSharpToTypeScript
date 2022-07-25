import * as assert from 'assert';
import * as fs from 'fs';
import * as path from 'path';
import * as vscode from 'vscode';

suite('Commands Tests', () => {
    test('Replace whole document', async () => {
        const document = await vscode.workspace.openTextDocument({
            language: 'csharp',
            content: `using System;

namespace MyProject.DTOs
{
    public class Item
    {
        public string Text { get; set; }
    }
}`});

        await vscode.window.showTextDocument(document);

        await vscode.commands.executeCommand('csharpToTypeScript.csharpToTypeScriptReplace');

        const convertedContent = document.getText();

        assert.ok(convertedContent.includes('interface Item {'));
        assert.ok(convertedContent.includes(': string;'));
    });

    test('Replace selection', async () => {
        const document = await vscode.workspace.openTextDocument({
            language: 'csharp',
            content: `using System;

namespace MyProject.DTOs
{
    public class Keep
    {
        public string KeepText { get; set; }

    public class Replace
    {
        public int ReplaceNumber { get; set; }
    }
}`});

        await vscode.window.showTextDocument(document);

        vscode.window.activeTextEditor!.selection = new vscode.Selection(
            8, 0,
            document.lineCount - 1, document.lineAt(document.lineCount - 1).range.end.character);

        await vscode.commands.executeCommand('csharpToTypeScript.csharpToTypeScriptReplace');

        const convertedContent = document.getText();

        assert.ok(convertedContent.includes('class Keep'));
        assert.ok(convertedContent.includes('public string KeepText { get; set; }'));

        assert.ok(convertedContent.includes('interface Replace'));
        assert.ok(convertedContent.includes(': number;'));
    });

    test('Write to clipboard', async () => {
        const document = await vscode.workspace.openTextDocument({
            language: 'csharp',
            content: `using System;

namespace MyProject.DTOs
{
    public class WriteMe
    {
        public string Text { get; set; }
    }
}`});

        await vscode.window.showTextDocument(document);

        await vscode.commands.executeCommand('csharpToTypeScript.csharpToTypeScriptToClipboard');

        const convertedContent = await vscode.env.clipboard.readText();

        assert.ok(convertedContent.includes('interface WriteMe {'));
        assert.ok(convertedContent.includes(': string;'));
    });

    test('Convert & paste from clipboard', async () => {
        const document = await vscode.workspace.openTextDocument({
            language: 'typescript'
        });

        await vscode.env.clipboard.writeText(`using System;

namespace MyProject.DTOs
{
    public class PasteMe
    {
        public string Text { get; set; }
    }
}`);

        await vscode.window.showTextDocument(document);

        await vscode.commands.executeCommand('csharpToTypeScript.csharpToTypeScriptPasteAs');

        const convertedContent = document.getText();

        assert.ok(convertedContent.includes('interface PasteMe {'));
        assert.ok(convertedContent.includes(': string;'));
    });

    test('Convert to new file (passed/selected document)', async () => {
        const tempDirPath = path.join(__dirname, 'temp');
        const csharpFilePath = path.join(tempDirPath, 'Item.cs');
        const tsFilePath = path.join(tempDirPath, 'item.ts');

        if (!fs.existsSync(tempDirPath)) {
            fs.mkdirSync(tempDirPath);
        }
        if (fs.existsSync(csharpFilePath)) {
            fs.unlinkSync(csharpFilePath);
        }
        if (fs.existsSync(tsFilePath)) {
            fs.unlinkSync(tsFilePath);
        }

        fs.writeFileSync(csharpFilePath, `using System;

namespace MyProject.DTOs
{
    public class Item
    {
        public string Text { get; set; }
    }
}`);

        await vscode.commands.executeCommand(
            'csharpToTypeScript.csharpToTypeScriptToFile',
            vscode.Uri.file(csharpFilePath));

        assert.ok(fs.existsSync(tsFilePath));

        const tsFileContent = fs.readFileSync(tsFilePath, { encoding: 'utf-8' });

        assert.ok(tsFileContent.includes('interface Item {'));
        assert.ok(tsFileContent.includes(': string;'));
    });

    test('Convert to new file (open/active document)', async () => {
        const tempDirPath = path.join(__dirname, 'temp');
        const csharpFilePath = path.join(tempDirPath, 'thing.cs');
        const tsFilePath = path.join(tempDirPath, 'thing.ts');

        if (!fs.existsSync(tempDirPath)) {
            fs.mkdirSync(tempDirPath);
        }
        if (fs.existsSync(csharpFilePath)) {
            fs.unlinkSync(csharpFilePath);
        }
        if (fs.existsSync(tsFilePath)) {
            fs.unlinkSync(tsFilePath);
        }

        fs.writeFileSync(csharpFilePath, `using System;

namespace MyProject.DTOs
{
    public class Thing
    {
        public string Text { get; set; }
    }
}`);

        const document = await vscode.workspace.openTextDocument(csharpFilePath);
        await vscode.window.showTextDocument(document);

        await vscode.commands.executeCommand('csharpToTypeScript.csharpToTypeScriptToFile');

        assert.ok(fs.existsSync(tsFilePath));

        const tsFileContent = fs.readFileSync(tsFilePath, { encoding: 'utf-8' });

        assert.ok(tsFileContent.includes('interface Thing {'));
        assert.ok(tsFileContent.includes(': string;'));
    });
});
