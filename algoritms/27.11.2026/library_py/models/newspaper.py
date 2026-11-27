from models.publication import Publication


class Newspaper(Publication):
    def __init__(self, inv_number, title, year, issue_date, periodicity):
        super().__init__(inv_number, title, year)
        self._issue_date = issue_date
        self._periodicity = periodicity

    @property
    def issue_date(self):
        return self._issue_date

    @property
    def periodicity(self):
        return self._periodicity

    def get_info(self):
        return f"Газета: {self.to_string()}, дата выпуска: {self._issue_date}, периодичность: {self._periodicity}"
