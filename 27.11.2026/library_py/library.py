from exceptions import (
    DuplicateInventoryError,
    AlreadyIssuedError,
    NotIssuedError,
    PublicationNotFoundError,
)


class Library:
    def __init__(self):
        self._items = []

    def _find_by_number(self, inv_number):
        for item in self._items:
            if item.inv_number == inv_number:
                return item
        raise PublicationNotFoundError(f"инв. номер {inv_number}")

    def add(self, publication):
        for item in self._items:
            if item.inv_number == publication.inv_number:
                raise DuplicateInventoryError(publication.inv_number)
        self._items.append(publication)

    def get_all(self):
        return list(self._items)

    def search(self, title):
        found = [i for i in self._items if title.lower() in i.title.lower()]
        if not found:
            raise PublicationNotFoundError(f"название «{title}»")
        return found

    def issue(self, inv_number):
        item = self._find_by_number(inv_number)
        if not item.available:
            raise AlreadyIssuedError(inv_number)
        item.issue()

    def give_back(self, inv_number):
        item = self._find_by_number(inv_number)
        if item.available:
            raise NotIssuedError(inv_number)
        item.give_back()

    def remove(self, inv_number):
        item = self._find_by_number(inv_number)
        self._items.remove(item)

    def stats(self):
        total = len(self._items)
        available = sum(1 for i in self._items if i.available)
        return total, available, total - available
