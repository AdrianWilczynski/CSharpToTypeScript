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
});
