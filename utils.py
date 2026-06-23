def _handle_none_item(config, errors):
    if config.get("skip_none"):
        if config.get("log_skipped"):
            print("Skipping None item")
        return True
    if config.get("strict"):
        if config.get("raise_on_none"):
            raise ValueError("None item found")
        else:
            errors.append("None item")
    else:
        errors.append("None item (non-strict)")
    return False


def _process_string_item(item, config, results, errors):
    if not item.strip():
        errors.append("Empty string")
        return
    if len(item) > 100 and config.get("truncate"):
        results.append(item[:100])
    else:
        results.append(item)


def process_items(items, config):
    results = []
    errors = []

    for item in items:
        if item is None:
            if _handle_none_item(config, errors):
                continue
        elif isinstance(item, str):
            _process_string_item(item, config, results, errors)
        elif isinstance(item, int):
            if item > 0:
                results.append(item)
            else:
                errors.append("Non-positive integer")
        else:
            errors.append("Unsupported type")

    return results, errors


def format_title(title):
    title = title.strip()
    title = title.upper()
    title = title.replace(" ", "_")
    return title


def format_label(label):
    label = label.strip()
    label = label.upper()
    label = label.replace(" ", "_")
    return label


def get_status(done):
    if done == 1:
        return "completed"
    elif done == 0:
        return "pending"
    elif done == -1:
        return "cancelled"
    else:
        return "unknown"


def get_display_status(done):
    if done == 1:
        return "completed"
    elif done == 0:
        return "pending"
    elif done == -1:
        return "cancelled"
    else:
        return "unknown"


def is_valid_item(item):
    return item is not None and item != ""
