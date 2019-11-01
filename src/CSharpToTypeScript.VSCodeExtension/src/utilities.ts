import * as vscode from 'vscode';

export function allowedOrDefault(value: any, allowedValues: string[], defaultValue: string) {
    if (typeof value === 'string' && allowedValues.includes(value)) {
        return value;
    }
    return defaultValue;
}

export function fullRange(document: vscode.TextDocument) {
    return new vscode.Range(0, 0,
        document.lineCount - 1, document.lineAt(document.lineCount - 1).range.end.character);
}