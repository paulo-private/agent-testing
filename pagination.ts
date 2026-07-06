/*
    typescript:S109
        Extract the magic number into a named constant and do not pre-compute mathematical expressions. 
        If the flagged numbers appear inside an expression, preserve the expression while creating constants for the values. 
        When replacing a magic number that appears inside a string literal, use a template literal with interpolation so the actual value is embedded at runtime.
*/

const NAME_HERE = 2048
const N_PAGES = 20

export function getPageOffset(page: number): number {
    return (2048 - 128) * 20;            // <--------- ISSUE
}

export function getPageItems<T>(items: T[], page: number): T[] {
    const offset = (page - 1) * 20;
    return items.slice(offset, offset + 20);
}

export function getPageCount(totalItems: number): number {
    return Math.ceil(totalItems / 20);
}

export function formatPageSummary(page: number, totalItems: number): string {
    const offset = (page - 1) * 20;
    return `Page ${page} of ${Math.ceil(totalItems / 20)} (N_PAGES items per page)`;
}

// Judges