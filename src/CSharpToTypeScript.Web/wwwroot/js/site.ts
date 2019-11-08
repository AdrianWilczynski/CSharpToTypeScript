const inputCodeTextarea = document.getElementById('InputCode') as HTMLTextAreaElement;
const convertedCodeHiddenInput = document.getElementById('convertedCodeHiddenInput') as HTMLTextAreaElement;
const convertedCodeContainer = document.getElementById('convertedCodeContainer')!;
const moduleNameSettings = document.getElementById('moduleNameSettings') as HTMLDetailsElement;
const generateImportsToggle = document.getElementById('Settings_GenerateImports') as HTMLInputElement;

inputCodeTextarea.addEventListener('keydown', ev => {
    if (ev.keyCode === 9 /* Tab */) {
        ev.preventDefault();

        if (isPrecededBySnippetPrefix(SnippetPrefix.Class)) {
            insertClassSnippet();
        } else if (isPrecededBySnippetPrefix(SnippetPrefix.Property)) {
            insertPropertySnippet();
        } else {
            indent();
        }
    }
})

convertedCodeContainer.addEventListener('click', () => {
    copyToClipboard();
    animate();
})

convertedCodeContainer.addEventListener('animationend', cleanUpAnimations);

generateImportsToggle.addEventListener('change', () => {
    moduleNameSettings.open = generateImportsToggle.checked;
})

function copyToClipboard() {
    convertedCodeHiddenInput.hidden = false;

    convertedCodeHiddenInput.select();
    document.execCommand('copy');
    convertedCodeHiddenInput.blur();

    convertedCodeHiddenInput.hidden = true;
}

function indent() {
    const indentation = '    ';

    const before = getTextBeforeSelection();
    const after = getTextAfterSelection();

    inputCodeTextarea.value = before + indentation + after;

    inputCodeTextarea.selectionEnd = inputCodeTextarea.selectionStart = before.length + indentation.length;
}

enum SnippetPrefix {
    Class = 'class',
    Property = 'prop'
}

function isPrecededBySnippetPrefix(prefix: SnippetPrefix) {
    return inputCodeTextarea.selectionStart === inputCodeTextarea.selectionEnd
        && new RegExp('(^|\\s)' + prefix + '$').test(getTextBeforeSelection());
}

function insertClassSnippet() {
    insertSnippet(SnippetPrefix.Class, 'class ', 'Name', '\r\n{\r\n    \r\n}');
}

function insertPropertySnippet() {
    insertSnippet(SnippetPrefix.Property, 'public ', 'int MyProperty', ' { get; set; }');
}

function insertSnippet(prefix: SnippetPrefix, beginningPart: string, selectedPart: string, endPart: string) {
    const before = getTextBeforeSelection();
    const after = getTextAfterSelection();

    const beforeWithoutPrefix = before.substring(0, before.length - prefix.length);

    inputCodeTextarea.value = beforeWithoutPrefix + beginningPart + selectedPart + endPart + after;

    inputCodeTextarea.selectionStart = beforeWithoutPrefix.length + beginningPart.length;
    inputCodeTextarea.selectionEnd = inputCodeTextarea.selectionStart + selectedPart.length;
}

function getTextBeforeSelection() {
    return inputCodeTextarea.value.substring(0, inputCodeTextarea.selectionStart);
}

function getTextAfterSelection() {
    return inputCodeTextarea.value.substring(inputCodeTextarea.selectionEnd);
}

const animationCssClasses = ['animated', 'pulse', 'faster'];
function animate() {
    convertedCodeContainer.classList.add(...animationCssClasses);
}

function cleanUpAnimations() {
    convertedCodeContainer.classList.remove(...animationCssClasses);
}