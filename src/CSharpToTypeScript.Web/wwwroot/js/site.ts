declare const Prism: any;

const inputCodeHighlightedContainer = document.getElementById('inputCodeHighlightedContainer')!;
const inputCodeHighlighted = inputCodeHighlightedContainer.querySelector('code')!;
const inputCodeTextarea = document.getElementById('InputCode') as HTMLTextAreaElement;
const convertedCodeHiddenInput = document.getElementById('convertedCodeHiddenInput') as HTMLTextAreaElement;
const convertedCodeContainer = document.getElementById('convertedCodeContainer')!;

inputCodeHighlightedContainer.addEventListener('click', editInputCode);

inputCodeTextarea.addEventListener('blur', highlightInputCode)

addEventListener('keydown', ev => {
    if (ev.keyCode === 9) {
        ev.preventDefault();
        indent();
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
    const start = inputCodeTextarea.selectionStart;
    const end = inputCodeTextarea.selectionEnd;

    const indentation = '    ';

    const before = inputCodeTextarea.value.substring(0, start);
    const after = inputCodeTextarea.value.substring(end);

    inputCodeTextarea.value = before + indentation + after;

    inputCodeTextarea.selectionStart = inputCodeTextarea.selectionEnd = start + indentation.length;
}

const animationCssClasses = ['animated', 'pulse', 'faster'];
function animate() {
    convertedCodeContainer.classList.add(...animationCssClasses);
}

function cleanUpAnimations() {
    convertedCodeContainer.classList.remove(...animationCssClasses);
}