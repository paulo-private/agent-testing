def compute_report_metrics(records, filters=None, group_by=None):
    if not records:
        return {}

    results = {}
    for record in records:
        if filters:
            if "status" in filters:
                if record.get("status") not in filters["status"]:
                    continue
            if "type" in filters:
                if record.get("type") not in filters["type"]:
                    if not filters.get("include_unknown"):
                        continue
            if "date_range" in filters:
                if record.get("date") < filters["date_range"]["start"]:
                    continue
                if record.get("date") > filters["date_range"]["end"]:
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
