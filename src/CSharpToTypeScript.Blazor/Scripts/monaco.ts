import * as monaco from 'monaco-editor';
import { debounce } from 'lodash';

import { getSnippets, getKeywords, getAttributes, getStructs, getInterfaces, getClasses, getNames, getNamespaces } from './completions';
import { DotNetObject } from './interop';

(window as any)['initializeMonaco'] = (
    inputEditorContainer: HTMLDivElement, outputEditorContainer: HTMLDivElement,
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
                    }),
                    ...getAttributes().map(a => {
                        return {
                            ...a,
                            kind: monaco.languages.CompletionItemKind.Class,
                            range: range
                        }
                    }),
                    ...getStructs().map(t => {
                        return {
                            ...t,
                            kind: monaco.languages.CompletionItemKind.Struct,
                            range: range
                        }
                    }),
                    ...getInterfaces().map(i => {
                        return {
                            ...i,
                            kind: monaco.languages.CompletionItemKind.Interface,
                            range: range
                        }
                    }),
                    ...getClasses().map(c => {
                        return {
                            ...c,
                            kind: monaco.languages.CompletionItemKind.Class,
                            range: range
                        }
                    }),
                    ...getNamespaces().map(n => {
                        return {
                            ...n,
                            kind: monaco.languages.CompletionItemKind.Module,
                            range: range
                        }
                    }),
                    ...getNames().map(n => {
                        return {
                            ...n,
                            kind: monaco.languages.CompletionItemKind.Variable,
                            range: range
                        }
                    }),
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
        contextmenu: false
    });

    inputEditor.onDidChangeModelContent(debounce(async e =>
        await component.invokeMethodAsync('OnInputEditorChangeAsync', inputEditor.getValue()),
        500,
        {
            leading: true,
            trailing: true
        }));

    (window as any)['getInputEditorValue'] = () => inputEditor.getValue();

    const outputEditor = monaco.editor.create(outputEditorContainer, {
        language: 'typescript',
        theme: 'vs-dark',
        minimap: {
            enabled: false
        },
        contextmenu: false
    });

    (window as any)['setOutputEditorValue'] = (value: string) => outputEditor.setValue(value);

    (window as any)['copyToClipboard'] = () => {
        outputEditor.setSelection(
            outputEditor.getModel()!.getFullModelRange());

        outputEditor.trigger('copyToClipboard', 'editor.action.clipboardCopyAction', null);
    };

    window.addEventListener('resize', () => {
        inputEditor.layout();
        outputEditor.layout();
    });
}