import * as assert from 'assert';

import * as vscode from 'vscode';

suite('Commands Tests', () => {
    test('Replace whole document', async () => {
        const document = await vscode.workspace.openTextDocument({
            language: 'csharp',
            content:
                `using System;

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
            content:
                `using System;

                namespace MyProject.DTOs
                {
                    public class Keep
                    {
                        public string KeepText { get; set; }
                    }

                    public class Replace
                    {
                        public int ReplaceNumber { get; set; }
                    }
                }`});
        await vscode.window.showTextDocument(document);

        vscode.window.activeTextEditor!.selection = new vscode.Selection(
            9, 0,
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
            content:
                `using System;

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

        await vscode.env.clipboard.writeText(
            `using System;

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
});
