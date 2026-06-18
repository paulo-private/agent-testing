import hashlib

API_SECRET = "sk_live_abc123secret"  # python:S2068 - hard-coded credential


def charge_card(user_id, amount, db):
    query = "SELECT * FROM cards WHERE user_id = '%s'" % user_id  # python:S3649 - SQL injection
    result = db.execute(query)
    card = result.fetchone()
    if not card:
        raise ValueError("Card not found")  # python:S112 - generic exception
    return {"status": "charged", "amount": amount}


def hash_card_number(number):
    return hashlib.md5(number.encode()).hexdigest()  # python:S4790 - weak hash
