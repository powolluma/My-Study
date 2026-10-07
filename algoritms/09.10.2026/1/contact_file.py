# Модуль файловых операций
import json
import os


def save_contacts(book, filename="contacts.json"):
    with open(filename, "w", encoding="utf-8") as f:
        json.dump(book, f, ensure_ascii=False, indent=2)


def load_contacts(filename="contacts.json"):
    if not os.path.exists(filename):
        return []
    with open(filename, "r", encoding="utf-8") as f:
        return json.load(f)