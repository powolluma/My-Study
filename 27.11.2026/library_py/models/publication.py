from abc import ABC, abstractmethod


class Publication(ABC):
    def __init__(self, inv_number, title, year):
        self._inv_number = inv_number
        self._title = title
        self._year = year
        self._available = True

    @property
    def inv_number(self):
        return self._inv_number

    @property
    def title(self):
        return self._title

    @property
    def year(self):
        return self._year

    @property
    def available(self):
        return self._available

    def issue(self):
        self._available = False

    def give_back(self):
        self._available = True

    @abstractmethod
    def get_info(self):
        pass

    def to_string(self):
        status = "доступно" if self._available else "выдано"
        return f"[{self._inv_number}] {self._title}, {self._year} г. ({status})"

    def __str__(self):
        return self.to_string()
