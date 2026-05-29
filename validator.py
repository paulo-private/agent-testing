import re

full_name_re = re.compile(r"[a-zA-Z]+[\s\-_]+[a-zA-Z]+(?:[\s\-_][a-zA-Z]+)?\s*(?:jr|sr|ii|iii|iv)?", re.IGNORECASE)
date_re = re.compile(r"\d{1,2}\/\d{1,2}\/(?:\d{2}|\d{4})")
phone_re = re.compile(r"\(?\d{3}\)?[\s\-]?\d{3}[\s\-]?\d{4}(?:\s*(?:x|ext)\.?\s*\d{1,5})?")
_email_local_re = re.compile(r"[a-zA-Z0-9][a-zA-Z0-9._%+\-]{0,62}[a-zA-Z0-9]|[a-zA-Z0-9]")
_email_domain_part_re = re.compile(r"[a-zA-Z0-9][a-zA-Z0-9\-]{0,61}[a-zA-Z0-9]|[a-zA-Z0-9]")
_valid_tlds = {
    "com", "org", "net", "edu", "gov", "io", "co.uk", "co.in",
    "de", "fr", "es", "it", "nl", "se", "no", "dk", "fi", "be",
    "at", "ch", "au", "nz", "ca", "jp", "cn", "br", "mx", "ru", "za",
}
postal_code_re = re.compile(r"(?:\d{5}(?:-\d{4})?|[A-Z]{1,2}\d[A-Z\d]?\s*\d[A-Z]{2}|[A-Z]\d[A-Z]\s*\d[A-Z]\d)")
ip_re = re.compile(r"(?:(?:25[0-5]|2[0-4]\d|1\d{2}|[1-9]\d|\d)\.){3}(?:25[0-5]|2[0-4]\d|1\d{2}|[1-9]\d|\d)(?:\/(?:3[0-2]|[12]\d|\d))?")


def validate_full_name(name: str) -> bool:
    return bool(full_name_re.match(name))


def validate_date(date: str) -> bool:
    return bool(date_re.match(date))


def validate_phone(phone: str) -> bool:
    return bool(phone_re.match(phone))


def validate_email(email: str) -> bool:
    if "@" not in email:
        return False
    local, _, domain = email.partition("@")
    if not local or not domain:
        return False
    if not _email_local_re.fullmatch(local):
        return False
    # Check that the domain ends with a valid TLD
    for tld in _valid_tlds:
        if domain.endswith("." + tld):
            domain_prefix = domain[: -(len(tld) + 1)]
            break
    else:
        return False
    # Validate each domain part before the TLD
    parts = domain_prefix.split(".") if domain_prefix else []
    if not parts:
        return False
    for part in parts:
        if not _email_domain_part_re.fullmatch(part):
            return False
    return True


def validate_postal_code(code: str) -> bool:
    return bool(postal_code_re.match(code))


def validate_ip(ip: str) -> bool:
    return bool(ip_re.match(ip))
