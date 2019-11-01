export interface Input {
    code: string;
    useTabs: boolean;
    tabSize?: number;
    export: boolean;
    convertDatesTo: string;
    convertNullablesTo: string;
    toCamelCase: boolean;
}

export const dateOutputTypes = ['string', 'date', 'union'];
export const nullableOutputTypes = ['null', 'undefined'];