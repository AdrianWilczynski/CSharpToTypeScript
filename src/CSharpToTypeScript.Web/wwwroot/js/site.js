// @ts-check

const elements = getElements();

elements.inputCodeHighlightedContainer.addEventListener('click', editInputCode);

elements.inputCodeTextarea.addEventListener('blur', highlightInputCode)

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

const cssClasses = ['animated', 'pulse', 'faster'];
function animate() {

    elements.convertedCodeContainer.classList.add(...cssClasses);
}

function cleanUpAnimations() {
    elements.convertedCodeContainer.classList.remove(...cssClasses);
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