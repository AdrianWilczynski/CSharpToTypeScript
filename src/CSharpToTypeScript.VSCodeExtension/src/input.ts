export interface Input {
    code: string;
    useTabs: boolean;
    tabSize?: number;
    export: boolean;
    convertDatesTo: string;
    convertNullablesTo: string;
}

export const dateOutputTypes = ['string', 'date', 'union'];
export const nullableOutputTypes = ['null', 'undefined'];