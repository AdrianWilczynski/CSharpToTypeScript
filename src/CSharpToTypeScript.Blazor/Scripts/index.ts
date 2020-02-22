import * as monaco from 'monaco-editor';

import { getSnippets, getKeywords } from './completions';

interface DotNetObject {
    invokeMethod<T>(methodIdentifier: string, ...args: any[]): T;
    invokeMethodAsync<T>(methodIdentifier: string, ...args: any[]): Promise<T>;
}

(window as any)['initializeMonaco'] = (
    inputEditorContainer: HTMLDivElement,
    outputEditorContainer: HTMLDivElement,
    component: DotNetObject) => {

    monaco.languages.typescript.typescriptDefaults.setDiagnosticsOptions({
        diagnosticCodesToIgnore: [
            2307
        ]
    })

    monaco.languages.registerCompletionItemProvider('csharp', {
        provideCompletionItems: (model, position, context) => {
            const word = model.getWordUntilPosition(position);
            const range = {
                startLineNumber: position.lineNumber,
                endLineNumber: position.lineNumber,
                startColumn: word.startColumn,
                endColumn: word.endColumn
            };

            return {
                suggestions: [
                    ...getSnippets().map(s => {
                        return {
                            ...s,
                            kind: monaco.languages.CompletionItemKind.Snippet,
                            insertTextRules: monaco.languages.CompletionItemInsertTextRule.InsertAsSnippet,
                            range: range
                        }
                    }),
                    ...getKeywords().map(k => {
                        return {
                            ...k,
                            kind: monaco.languages.CompletionItemKind.Keyword,
                            range: range
                        }
                    })
                ]
            };
        }
    });

    const inputEditor = monaco.editor.create(inputEditorContainer, {
        language: 'csharp',
        theme: 'vs-dark',
        minimap: {
            enabled: false
        },

    });
    inputEditor.onDidChangeModelContent(async e =>
        await component.invokeMethodAsync('OnInputEditorChangeAsync', inputEditor.getValue()));

    const outputEditor = monaco.editor.create(outputEditorContainer, {
        language: 'typescript',
        theme: 'vs-dark',
        minimap: {
            enabled: false
        }
    });

    (window as any)['setOutputEditorValue'] = (value: string) => outputEditor.setValue(value);
}
