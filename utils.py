def process_items(items, config):
    results = []
    errors = []

    for item in items:
        if item is None:
            if config.get("skip_none"):
                if config.get("log_skipped"):
                    print("Skipping None item")
                continue
            else:
                if config.get("strict"):
                    if config.get("raise_on_none"):
                        raise ValueError("None item found")
                    else:
                        errors.append("None item")
                else:
                    errors.append("None item (non-strict)")
        else:
            if isinstance(item, str):
                if item.strip():
                    if len(item) > 100:
                        if config.get("truncate"):
                            results.append(item[:100])
                        else:
                            results.append(item)
                    else:
                        results.append(item)
                else:
                    errors.append("Empty string")
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
