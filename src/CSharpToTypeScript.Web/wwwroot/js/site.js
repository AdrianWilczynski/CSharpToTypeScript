// @ts-check

document.getElementById('convertedCode')
    .addEventListener('click', () => {
        copyToClipboard();
        animate();
    });

function copyToClipboard() {
    const input = document.getElementById('convertedCodeHiddenInput');
    if (!(input instanceof HTMLTextAreaElement)) {
        throw new Error(`"convertedCodeHiddenInput" isn't an "HTMLTextAreaElement".`)
    }

    input.hidden = false;

    input.select();
    document.execCommand('copy');

    input.hidden = true;
}

function animate() {
    const classes = ['animated', 'pulse', 'faster'];

    const codeContainer = document.getElementById('convertedCode');

    codeContainer.classList.add(...classes);

    codeContainer.addEventListener('animationend', () => codeContainer.classList.remove(...classes));
}