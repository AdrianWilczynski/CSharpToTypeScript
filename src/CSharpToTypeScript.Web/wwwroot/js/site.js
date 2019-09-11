// @ts-check

const elements = getElements();

elements.inputCodeHighlightedContainer.addEventListener('click', editInputCode);

elements.inputCodeTextarea.addEventListener('blur', highlightInputCode)

elements.inputCodeTextarea.addEventListener('keydown', ev => {
    if (ev.keyCode === 9) {
        ev.preventDefault();
        indent();
    }
})

elements.convertedCodeContainer.addEventListener('click', () => {
    copyToClipboard();
    animate();
})

elements.convertedCodeContainer.addEventListener('animationend', cleanUpAnimations);

function editInputCode() {
    elements.inputCodeHighlightedContainer.hidden = true;
    elements.inputCodeTextarea.hidden = false;

    elements.inputCodeTextarea.focus();
}

function highlightInputCode() {
    elements.inputCodeHighlightedContainer.hidden = false;
    elements.inputCodeTextarea.hidden = true;

    elements.inputCodeHighlighted.textContent = elements.inputCodeTextarea.value;

    // @ts-ignore
    Prism.highlightElement(elements.inputCodeHighlighted);
}

function copyToClipboard() {
    elements.convertedCodeHiddenInput.hidden = false;

    elements.convertedCodeHiddenInput.select();
    document.execCommand('copy');

    elements.convertedCodeHiddenInput.hidden = true;
}

function indent() {
    const start = elements.inputCodeTextarea.selectionStart;
    const end = elements.inputCodeTextarea.selectionEnd;

    const indentation = '    ';

    const before = elements.inputCodeTextarea.value.substring(0, start);
    const after = elements.inputCodeTextarea.value.substring(end);

    elements.inputCodeTextarea.value = before + indentation + after;

    elements.inputCodeTextarea.selectionStart = elements.inputCodeTextarea.selectionEnd = start + indentation.length;
}

const animationCssClasses = ['animated', 'pulse', 'faster'];
function animate() {
    elements.convertedCodeContainer.classList.add(...animationCssClasses);
}

function cleanUpAnimations() {
    elements.convertedCodeContainer.classList.remove(...animationCssClasses);
}

function getElements() {
    const inputCodeHighlightedContainer = document.getElementById('inputCodeHighlightedContainer');
    const inputCodeHighlighted = inputCodeHighlightedContainer.querySelector('code');

    const inputCodeTextarea = document.getElementById('InputCode');
    if (!(inputCodeTextarea instanceof HTMLTextAreaElement)) {
        throw new Error(`"inputCodeTextarea" isn't an "HTMLTextAreaElement".`);
    }

    const convertedCodeHiddenInput = document.getElementById('convertedCodeHiddenInput');
    if (!(convertedCodeHiddenInput instanceof HTMLTextAreaElement)) {
        throw new Error(`"convertedCodeHiddenInput" isn't an "HTMLTextAreaElement".`);
    }

    const convertedCodeContainer = document.getElementById('convertedCodeContainer');

    return {
        inputCodeHighlightedContainer,
        inputCodeHighlighted,
        inputCodeTextarea,
        convertedCodeContainer,
        convertedCodeHiddenInput
    }
}