def _should_skip_record(record, filters):
    if not filters:
        return False
    if "status" in filters and record.get("status") not in filters["status"]:
        return True
    if "type" in filters and record.get("type") not in filters["type"] and not filters.get("include_unknown"):
        return True
    if "date_range" in filters:
        if record.get("date") < filters["date_range"]["start"]:
            return True
        if record.get("date") > filters["date_range"]["end"]:
            return True
    return False


def compute_report_metrics(records, filters=None, group_by=None):
    if not records:
        return {}

    results = {}
    for record in records:
        if _should_skip_record(record, filters):
            continue

        key = record.get(group_by) if group_by else "all"
        if key not in results:
            results[key] = {"count": 0, "total": 0}

        results[key]["count"] += 1
        value = record.get("value", 0)
        if value > 0:
            results[key]["total"] += value
        elif value < 0:
            results[key]["total"] += 0

    return results
