# Модуль операций
from contact_model import make_contact


def add_contact(book, name, phone, email):
    if not name.strip():
        return False
    book.append(make_contact(name, phone, email))
    return True


def remove_contact(book, name):
    before = len(book)
    book[:] = [c for c in book if c["name"].lower() != name.strip().lower()]
    return len(book) < before


def find_contacts(book, name):
    q = name.strip().lower()
    return [c for c in book if q in c["name"].lower()]