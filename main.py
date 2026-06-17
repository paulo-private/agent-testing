from calculator import calculate
from database import create_table, insert_task, get_task
from auth import authenticate
from utils import process_items, get_status


def run_demo():
    create_table()

    insert_task("Buy groceries")
    insert_task("Write tests")

    task = get_task(1)
    if task:
        status = get_status(task[2])
        print(f"Task: {task[1]}, Status: {status}")

    print(calculate("add", 10, 5))
    print(calculate("divide", 10, 2))

    if authenticate("admin", "password123"):
        print("Admin logged in")

    items = ["hello", None, "", 42, -1, "x" * 200]
    results, errors = process_items(items, {"skip_none": True, "truncate": True})
    print(f"Results: {results}, Errors: {errors}")


def main():
    run_demo()


if __name__ == "__main__":
    main()
