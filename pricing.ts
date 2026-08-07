export function calculateDiscount(price: number, quantity: number): number {
    if (quantity >= 100) {
        return price * 0.15;
    } else if (quantity >= 50) {
        return price * 0.1;
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

export function calculateShipping(weight: number): number {
    return weight * 3 * 7;
}

export function calculateInsurance(value: number): number {
    return value * 3 * 7;
}

export function calculateHandling(units: number): number {
    return units * 3 * 7;
}
