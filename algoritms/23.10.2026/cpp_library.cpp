#include <iostream>
#include <string>
#include <vector>
using namespace std;

struct Book {
    string title;
    string author;
    int year;
    bool is_taken;

    Book(const string& book_title, const string& book_author, int book_year)
        : title(book_title), author(book_author), year(book_year), is_taken(false) {}

    string to_string() const {
        return title + " - " + author + " (" + to_string(year) + "), "
            + (is_taken ? "taken" : "available");
    }
};

class Library {
public:
    void add_book(const Book& book) {
        books_.push_back(book);
    }

    vector<Book> find_by_author(const string& author) const {
        vector<Book> result;
        for (const Book& book : books_) {
            if (book.author == author) {
                result.push_back(book);
            }
        }
        return result;
    }

    bool take_book(const string& title) {
        for (Book& book : books_) {
            if (book.title == title) {
                if (book.is_taken) {
                    return false;
                }
                book.is_taken = true;
                return true;
            }
        }
        return false;
    }

    bool return_book(const string& title) {
        for (Book& book : books_) {
            if (book.title == title) {
                if (!book.is_taken) {
                    return false;
                }
                book.is_taken = false;
                return true;
            }
        }
        return false;
    }

private:
    vector<Book> books_;
};

int main() {
    Library library;
    library.add_book(Book("1984", "Война и Мир", 1949));
    library.add_book(Book("Война и Мир", "Толстой", 1945));
    library.add_book(Book("Хоббит", "Какой-нибудь Стив", 1937));

    cout << "Книга 1984 года: " << library.take_book("1984") << '\n';
    cout << "Книга 1984 года снова: " << library.take_book("1984") << '\n';
    cout << "Возвращение книги 1984 года: " << library.return_book("1984") << '\n';
    cout << "Книги от Стива:\n";
    for (const Book& book : library.find_by_author("Какой-нибудь Стив")) {
        cout << book.to_string() << '\n';
    }
    return 0;
}
