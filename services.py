import logging
import requests

logger = logging.getLogger(__name__)


def fetch_data(url):
    try:
        response = requests.get(url)
        response.raise_for_status()
        return response.json()
    except requests.exceptions.HTTPError as e:
        logger.exception("HTTP Error %d for GET %s", e.response.status_code, url)
        return None


def post_data(url, payload):
    try:
        response = requests.post(url, json=payload)
        response.raise_for_status()
        return response.json()
    except requests.exceptions.HTTPError as e:
        logger.error("HTTP Error %d for POST %s", e.response.status_code, url)
        return None
