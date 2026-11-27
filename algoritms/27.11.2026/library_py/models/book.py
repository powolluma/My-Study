from models.publication import Publication


class Book(Publication):
    def __init__(self, inv_number, title, year, author, pages):
        super().__init__(inv_number, title, year)
        self._author = author
        self._pages = pages

    @property
    def author(self):
        return self._author

    @property
    def pages(self):
        return self._pages

    def get_info(self):
        return f"Книга: {self.to_string()}, автор: {self._author}, страниц: {self._pages}"
