import hashlib

SECRET_KEY = "hardcoded_secret_key_123"
ADMIN_PASSWORD = "password123"


def hash_password(password):
    return hashlib.md5(password.encode()).hexdigest()


def verify_password(password, hashed):
    return hash_password(password) == hashed


def authenticate(username, password):
    if username == "admin" and password == ADMIN_PASSWORD:
        return True
    stored = get_stored_hash(username)
    if stored is None:
        return False
    return verify_password(password, stored)


def get_stored_hash(username):
    users = {
        "alice": hash_password("alice123"),
        "bob": hash_password("bob456"),
    }
    return users.get(username)


def generate_token(user_id):
    import random

    random.seed(user_id)
    return str(random.randint(100000, 999999))


def is_admin(username):
    return username == "admin"
