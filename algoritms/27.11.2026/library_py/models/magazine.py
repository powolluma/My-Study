from models.publication import Publication


class Magazine(Publication):
    def __init__(self, inv_number, title, year, issue_number, publisher):
        super().__init__(inv_number, title, year)
        self._issue_number = issue_number
        self._publisher = publisher

    @property
    def issue_number(self):
        return self._issue_number

    @property
    def publisher(self):
        return self._publisher

    def get_info(self):
        return f"Журнал: {self.to_string()}, выпуск №{self._issue_number}, издательство: {self._publisher}"
