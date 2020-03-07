import * as assert from 'assert';

import * as vscode from 'vscode';

suite('Commands Tests', () => {
    test('Replace whole document', async () => {
        const document = await vscode.workspace.openTextDocument({
            language: 'csharp',
            content: `
                using System;

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
});
