def compute_report_metrics(records, filters=None, group_by=None):
    if not records:
        return {}

    results = {}
    for record in records:
        if filters and not _matches_filters(record, filters):
            continue

        _add_record_metrics(results, record, group_by)

    return results


def _matches_filters(record, filters):
    if _has_unmatched_status(record, filters):
        return False
    if _has_unmatched_type(record, filters):
        return False
    if _is_outside_date_range(record, filters):
        return False
    return True


def _has_unmatched_status(record, filters):
    return "status" in filters and record.get("status") not in filters["status"]


def _has_unmatched_type(record, filters):
    return (
        "type" in filters
        and record.get("type") not in filters["type"]
        and not filters.get("include_unknown")
    )


def _is_outside_date_range(record, filters):
    if "date_range" not in filters:
        return False

    date = record.get("date")
    date_range = filters["date_range"]
    return date < date_range["start"] or date > date_range["end"]


def _add_record_metrics(results, record, group_by):
    key = record.get(group_by) if group_by else "all"
    if key not in results:
        results[key] = {"count": 0, "total": 0}

    results[key]["count"] += 1
    value = record.get("value", 0)
    if value > 0:
        results[key]["total"] += value
    elif value < 0:
        results[key]["total"] += 0
