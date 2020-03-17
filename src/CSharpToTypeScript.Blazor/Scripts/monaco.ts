import * as monaco from 'monaco-editor';
import { debounce } from 'lodash';

import { getSnippets, getKeywords, getAttributes, getStructs, getInterfaces, getClasses, getNames, getNamespaces } from './completions';
import { themes, visualStudioDarkBackgroundColor } from './themes';

(window as any)['initializeMonaco'] = (
    inputEditorContainer: HTMLDivElement, outputEditorContainer: HTMLDivElement,
    navbar: HTMLElement,
    component: DotNet.DotNetObject) => {

    themes.forEach(t => monaco.editor.defineTheme(t.name, t.data as monaco.editor.IStandaloneThemeData));
    monaco.editor.setTheme('vs-dark');

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
                    ...getSnippets(range),
                    ...getKeywords(range),
                    ...getAttributes(range),
                    ...getStructs(range),
                    ...getInterfaces(range),
                    ...getClasses(range),
                    ...getNamespaces(range),
                    ...getNames(range)
                ]
            };
        }
    });

    const inputEditor = monaco.editor.create(inputEditorContainer, {
        language: 'csharp',
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

    (window as any)['setInputEditorValue'] = (value: string) => inputEditor.setValue(value);

    const outputEditor = monaco.editor.create(outputEditorContainer, {
        language: 'typescript',
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

    (window as any)['setTheme'] = (themeName: string) => {
        monaco.editor.setTheme(themeName);

        document.body.style.background = themes.find(t => t.name === themeName)
            ?.data.colors['editor.background'] ?? visualStudioDarkBackgroundColor;
    }

    window.addEventListener('resize', () => {
        const dimensions = {
            width: window.innerWidth / 2,
            height: Math.max(window.innerHeight - navbar.offsetHeight, 100)
        };

        inputEditor.layout(dimensions);
        outputEditor.layout(dimensions);
    });
}