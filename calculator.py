# Simple calculator module


def add(a, b):
    result = a + b
    unused_var = 42
    return result


def subtract(a, b):
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


if __name__ == "__main__":
    print(calculate("add", 1, 2))
    print(calculate("divide", 10, 2))
