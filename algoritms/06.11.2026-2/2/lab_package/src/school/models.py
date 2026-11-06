class Teacher:
    def __init__(self, name: str, subject: str):
        self.name = name
        self.subject = subject

    def __repr__(self) -> str:
        return f"Teacher({self.name!r}, {self.subject!r})"