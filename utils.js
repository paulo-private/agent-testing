function validatePhone(value) {
    const phoneRegex = /^\+?[\d\-\(\)\s]+$/;
    return phoneRegex.test(value);
}

function validateEmail(value) {
    const emailRegex = /^[\w\.]+\@[\w]+\.[\w]+$/;
    return emailRegex.test(value);
}

module.exports = { validatePhone, validateEmail };
