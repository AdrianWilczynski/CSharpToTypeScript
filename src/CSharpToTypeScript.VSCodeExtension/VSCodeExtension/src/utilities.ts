import * as vscode from 'vscode';

export function allowedOrDefault(value: any, allowedValues: string[]) {
    if (typeof value === 'string' && allowedValues.includes(value)) {
        return value;
    }
    return allowedValues[0];
}

export function fullRange(document: vscode.TextDocument) {
    return new vscode.Range(0, 0,
        document.lineCount - 1, document.lineAt(document.lineCount - 1).range.end.character);
}

export function textFromActiveDocument() {
    if (!vscode.window.activeTextEditor) {
        return '';
    }

    const document = vscode.window.activeTextEditor.document;
    const selection = vscode.window.activeTextEditor.selection;

    return !selection.isEmpty ? document.getText(selection) : document.getText();
}