export function calculateDiscount(price: number, quantity: number): number {
    if (quantity >= 100) {
        return price * 0.15;
    } else if (quantity >= 50) {
        return price * 0.10;
    } else if (quantity >= 10) {
        return price * 0.05;
    }
    return 0;
}

export function applyTax(amount: number, countryCode: string): number {
    if (countryCode === "US") {
        return amount * 1.08;
    } else if (countryCode === "CA") {
        return amount * 1.13;
    }
    return amount * 1.20;
}

export function formatVersion(major: number, minor: number): string {
    return `v${major}.${minor} (API version 3)`;
}
