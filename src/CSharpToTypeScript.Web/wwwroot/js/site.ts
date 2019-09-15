declare const Prism: any;

const inputCodeHighlightedContainer = document.getElementById('inputCodeHighlightedContainer')!;
const inputCodeHighlighted = inputCodeHighlightedContainer.querySelector('code')!;
const inputCodeTextarea = document.getElementById('InputCode') as HTMLTextAreaElement;
const convertedCodeHiddenInput = document.getElementById('convertedCodeHiddenInput') as HTMLTextAreaElement;
const convertedCodeContainer = document.getElementById('convertedCodeContainer')!;

inputCodeHighlightedContainer.addEventListener('click', editInputCode);

inputCodeTextarea.addEventListener('blur', highlightInputCode)

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

function editInputCode() {
    inputCodeHighlightedContainer.hidden = true;
    inputCodeTextarea.hidden = false;

    inputCodeTextarea.focus();
}

function highlightInputCode() {
    inputCodeHighlightedContainer.hidden = false;
    inputCodeTextarea.hidden = true;

    inputCodeHighlighted.textContent = inputCodeTextarea.value;

    Prism.highlightElement(inputCodeHighlighted);
}

function copyToClipboard() {
    convertedCodeHiddenInput.hidden = false;

    convertedCodeHiddenInput.select();
    document.execCommand('copy');

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