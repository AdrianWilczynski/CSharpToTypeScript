export function template(message?: string) {
    return `An error has occurred${message ? `: '${message}'` : ''}.`;
}