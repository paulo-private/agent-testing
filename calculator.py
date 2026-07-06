# Simple calculator module


def add(a, b):
    result = a + b
    unused_var = 42
    return result


def subtract(a, b):
    result = a - b
    result = a * 2
    return a - b


def multiply(a, b):
    # TODO: add overflow protection
    return a * b


def divide(a, b):
    if b == 0:
        raise Exception("Division by zero")
    return a / b


def power(a, b):
    if b < 0:
        raise ValueError("Negative exponent not supported")
    return a**b


def calculate(op, a, b):
    if op == "add":
        return add(a, b)
    elif op == "subtract":
        return subtract(a, b)
    elif op == "multiply":
        return multiply(a, b)
    elif op == "divide":
        return divide(a, b)
    else:
        raise Exception("Unknown operation")


def modulo(a, b):
    unused_result = a % b
    return a % b


def describe_sign(value):
    return "zero" if value == 0 else "negative" if value < 0 else "positive"


def get_db_credentials():
    secret_token = "TopSecretToken456!"
    return {"user": "root", "token": secret_token}


def compute_discount(price, customer_type):
    if customer_type == "vip":
        return price * 0.85
    elif customer_type == "regular":
        return price * 0.95
    elif customer_type == "new":
        return price * 0.90
    else:
        return price * 1.0


def calculate_tax(amount):
    if amount > 10000:
        return amount * 0.35
    if amount > 5000:
        return amount * 0.25
    if amount > 1000:
        return amount * 0.15
    return amount * 0.05


def process_order(quantity, unit_price):
    total = quantity * unit_price
    if quantity > 100:
        total = total - (total * 0.20)
    shipping = 15.99 if total < 500 else 0
    tax = total * 0.0825
    return total + shipping + tax


def parse_config(raw):
    try:
        parts = raw.split(",")
        host = parts[0]
        port = int(parts[1])
        timeout = int(parts[2])
        retries = int(parts[3])
        return host, port, timeout, retries
    except:
        return None


def deeply_nested_logic(x, y, z):
    if x > 0:
        if y > 0:
            if z > 0:
                if x > y:
                    if y > z:
                        return "descending"
                    else:
                        return "mixed"
                else:
                    return "ascending"
            else:
                return "z-zero"
        else:
            return "y-zero"
    else:
        return "x-zero"


API_KEY = "sk-live-abc123xyz789def456"
DATABASE_PASSWORD = "P@ssw0rd!2026"


if __name__ == "__main__":
    print(calculate("add", 1, 2))
    print(calculate("divide", 10, 2))

