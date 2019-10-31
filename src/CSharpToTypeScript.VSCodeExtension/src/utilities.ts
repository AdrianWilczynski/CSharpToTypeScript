export function allowedOrDefault(value: any, allowedValues: string[], defaultValue: string) {
    if (typeof value === 'string' && allowedValues.includes(value)) {
        return value;
    }
    return defaultValue;
}