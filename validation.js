/*
    javascript:S6535
        Always provide a one-character deletion by removing the flagged backslash. 
        Ensure that regex expressions remain semantically equivalent
*/
function isValidEmail(email) {
    const pattern = /^[\w\.]+\@[\w]+\.[\w]{2,4}$/;            // <--------- ISSUE
    return pattern.test(email);
}

function isValidSlug(slug) {
    const pattern = /^[a-z0\-9]+(?:\-[a-z0\-9]+)*$/;
    return pattern.test(slug);
}

function isValidHexColor(color) {
    const pattern = /^\#[0-9a-fA-F]{6}$/;
    return pattern.test(color);
}

module.exports = { isValidEmail, isValidSlug, isValidHexColor };
