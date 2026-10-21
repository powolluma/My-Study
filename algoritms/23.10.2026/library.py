class Book:
    def __init__(self, title, author, year, is_taken=False):
        self.title = title
        self.author = author
        self.year = year
        self.is_taken = is_taken

    def __str__(self):
        status = "taken" if self.is_taken else "available"
        return f"{self.title} - {self.author} ({self.year}), {status}"


class Library:
    def __init__(self):
        self.books = []

    def add_book(self, book):
        self.books.append(book)

    def find_by_author(self, author):
        return [book for book in self.books if book.author == author]

    def take_book(self, title):
        for book in self.books:
            if book.title == title:
                if book.is_taken:
                    return False
                book.is_taken = True
                return True
        return False

    def return_book(self, title):
        for book in self.books:
            if book.title == title:
                if not book.is_taken:
                    return False
                book.is_taken = False
                return True
        return False


if __name__ == "__main__":
    library = Library()
    library.add_book(Book("1984", "Война и Мир", 1949))
    library.add_book(Book("Война и Мир", "Толстой", 1945))
    library.add_book(Book("Хоббит", "Какой-нибудь Стив", 1937))

    print(f"Книга 1984 года: {library.take_book('1984')}")
    print(f"Книга 1984 года снова: {library.take_book('1984')}")
    print(f"Возвращение книги 1984 года: {library.return_book('1984')}")
    print("Книги от Стива:")
    for book in library.find_by_author("Какой-нибудь Стив"):
        print(book)
