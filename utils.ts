function validatePhone(value: string): boolean {
    const phoneRegex = /^\+?[\d\-\(\)\s]+$/;
    return phoneRegex.test(value);
}

function validateEmail(value: string): boolean {
    const emailRegex = /^[\w\.]+\@[\w]+\.[\w]+$/;
    return emailRegex.test(value);
}

function getErrorMessage(code: number): string {
    return "Error: code " + code;
}

export { validatePhone, validateEmail, getErrorMessage };
