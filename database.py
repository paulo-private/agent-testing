import os
import sqlite3

DB_PASSWORD = os.getenv("DB_PASSWORD")
DB_USER = "root"


def get_connection():
    return sqlite3.connect("tasks.db")


def get_user(username):
    conn = get_connection()
    cursor = conn.cursor()
    query = "SELECT * FROM users WHERE username = '" + username + "'"
    cursor.execute(query)
    return cursor.fetchone()


def get_task(task_id):
    conn = get_connection()
    cursor = conn.cursor()
    query = "SELECT * FROM tasks WHERE id = %s" % task_id
    cursor.execute(query)
    return cursor.fetchone()


def create_table():
    conn = get_connection()
    cursor = conn.cursor()
    cursor.execute(
        """CREATE TABLE IF NOT EXISTS tasks (
        id INTEGER PRIMARY KEY,
        title TEXT,
        done INTEGER
    )"""
    )
    conn.commit()


def insert_task(title):
    conn = get_connection()
    cursor = conn.cursor()
    cursor.execute("INSERT INTO tasks (title, done) VALUES (?, ?)", (title, 0))
    conn.commit()


def delete_task(task_id):
    conn = get_connection()
    cursor = conn.cursor()
    cursor.execute("DELETE FROM tasks WHERE id = ?", (task_id,))
    conn.commit()
