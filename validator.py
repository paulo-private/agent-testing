import re

full_name_re = re.compile(r"[a-zA-Z]+[\s\-_]+[a-zA-Z]+(?:[\s\-_][a-zA-Z]+)?\s*(?:jr|sr|ii|iii|iv)?", re.IGNORECASE)
date_re = re.compile(r"\d{1,2}\/\d{1,2}\/(?:\d{2}|\d{4})")
phone_re = re.compile(r"\(?\d{3}\)?[\s\-]?\d{3}[\s\-]?\d{4}(?:\s*(?:x|ext)\.?\s*\d{1,5})?")
_email_local_re = re.compile(r"[a-zA-Z0-9](?:[a-zA-Z0-9._%+\-]{0,62}[a-zA-Z0-9])?$")
_domain_label_re = re.compile(r"[a-zA-Z0-9](?:[a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?$")
_VALID_TLDS = frozenset([
    "com", "org", "net", "edu", "gov", "io", "co.uk", "co.in",
    "de", "fr", "es", "it", "nl", "se", "no", "dk", "fi", "be",
    "at", "ch", "au", "nz", "ca", "jp", "cn", "br", "mx", "ru", "za",
])
_us_postal_re = re.compile(r"\d{5}(?:-\d{4})?$")
_uk_postal_re = re.compile(r"[A-Z]{1,2}\d[A-Z\d]?\s*\d[A-Z]{2}$")
_ca_postal_re = re.compile(r"[A-Z]\d[A-Z]\s*\d[A-Z]\d$")
_url_scheme_re = re.compile(r"https?://", re.IGNORECASE)
_url_domain_label_re = re.compile(r"[A-Z0-9](?:[A-Z0-9\-]{0,61}[A-Z0-9])?$", re.IGNORECASE)
_url_tld_re = re.compile(r"[A-Z]{2,6}\.?$", re.IGNORECASE)
_url_path_re = re.compile(r"(?:/?|[/?]\S+)$")


def validate_full_name(name: str) -> bool:
    return bool(full_name_re.match(name))


def validate_date(date: str) -> bool:
    return bool(date_re.match(date))


def validate_phone(phone: str) -> bool:
    return bool(phone_re.match(phone))


def _check_email_domain(domain):
    for tld in _VALID_TLDS:
        if not domain.endswith("." + tld):
            continue
        prefix = domain[:-(len(tld) + 1)]
        labels = prefix.split(".")
        if all(_domain_label_re.match(label) for label in labels):
            return True
    return False


def validate_email(email: str) -> bool:
    if "@" not in email:
        return False
    local, domain = email.split("@", 1)
    if not _email_local_re.match(local):
        return False
    return _check_email_domain(domain)


def validate_postal_code(code: str) -> bool:
    return bool(
        _us_postal_re.match(code)
        or _uk_postal_re.match(code)
        or _ca_postal_re.match(code)
    )


def _is_valid_octet(s):
    if not s:
        return False
    try:
        val = int(s)
        return 0 <= val <= 255 and str(val) == s
    except ValueError:
        return False


def validate_ip(ip_str: str) -> bool:
    cidr_part = None
    if "/" in ip_str:
        ip_str, cidr_part = ip_str.split("/", 1)
    octets = ip_str.split(".")
    if len(octets) != 4 or not all(_is_valid_octet(o) for o in octets):
        return False
    if cidr_part is None:
        return True
    try:
        prefix = int(cidr_part)
        return 0 <= prefix <= 32 and str(prefix) == cidr_part
    except ValueError:
        return False


def _parse_url_host_and_path(rest):
    port_idx = rest.find(":", rest.rfind("]") + 1)
    if port_idx != -1:
        host = rest[:port_idx]
        after_host = rest[port_idx:]
        port_end = 1
        while port_end < len(after_host) and after_host[port_end].isdigit():
            port_end += 1
        if port_end == 1:
            return None, None
        return host, after_host[port_end:]
    sep = len(rest)
    for i, ch in enumerate(rest):
        if ch in ("/" , "?"):
            sep = i
            break
    return rest[:sep], rest[sep:]


def _validate_url_host(host):
    if host == "localhost":
        return True
    parts = host.split(".")
    if len(parts) == 4 and all(_is_valid_octet(p) for p in parts):
        return True
    if len(parts) >= 2 and all(_url_domain_label_re.match(p) for p in parts):
        return bool(_url_tld_re.match(parts[-1]))
    return False


def validate_url(url: str) -> bool:
    m = _url_scheme_re.match(url)
    if not m:
        return False
    host, path = _parse_url_host_and_path(url[m.end():])
    if host is None or not _validate_url_host(host):
        return False
    if not path:
        return True
    return bool(_url_path_re.match(path))


def validate_non_empty(value: str) -> bool:
    return bool(value and value.strip())
