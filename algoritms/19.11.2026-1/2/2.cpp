#include <iostream>
#include <vector>
#include <string>
#include <optional>
#include <algorithm>
#include <stdexcept>

using namespace std;

class Book {
public:
    int id;
    string title;
    bool available;

    Book(int id, string title) {
        this->id = id;
        this->title = title;
        available = true;
    }
};

class User {
public:
    int id;
    string name;

    User(int id, string name) {
        this->id = id;
        this->name = name;
    }
};

class Loan {
public:
    int id;
    int userId;
    int bookId;
    bool active;

    Loan(int id, int userId, int bookId) {
        this->id = id;
        this->userId = userId;
        this->bookId = bookId;
        active = true;
    }
};

class Library {
public:
    vector<Book> books;
    vector<User> users;
    vector<Loan> loans;

    int nextLoanId = 1;

    optional<Loan> borrowBook(int userId, int bookId) {

        auto user = find_if(users.begin(), users.end(),
            [userId](User& u) {
                return u.id == userId;
            });

        if (user == users.end()) {
            throw invalid_argument("Пользователь не найден");
        }

        auto book = find_if(books.begin(), books.end(),
            [bookId](Book& b) {
                return b.id == bookId;
            });

        if (book == books.end()) {
            throw invalid_argument("Книга не найдена");
        }

        int count = count_if(loans.begin(), loans.end(),
            [userId](Loan& l) {
                return l.userId == userId && l.active;
            });

        if (!book->available || count >= 3) {
            return nullopt;
        }

        book->available = false;

        Loan loan(nextLoanId++, userId, bookId);
        loans.push_back(loan);

        return loan;
    }

    bool returnBook(int loanId) {

        auto loan = find_if(loans.begin(), loans.end(),
            [loanId](Loan& l) {
                return l.id == loanId && l.active;
            });

        if (loan == loans.end()) {
            return false;
        }

        auto book = find_if(books.begin(), books.end(),
            [loan](Book& b) {
                return b.id == loan->bookId;
            });

        if (book == books.end()) {
            throw logic_error("Книга не найдена");
        }

        loan->active = false;
        book->available = true;

        return true;
    }
};

int main() {
    Library library;

    library.books.push_back(Book(1, "Война и мир"));
    library.books.push_back(Book(2, "Преступление и наказание"));

    library.users.push_back(User(1, "Иван"));

    auto result = library.borrowBook(1, 1);

    if (result.has_value()) {
        cout << "Книга выдана!" << endl;

        if (library.returnBook(result->id)) {
            cout << "Книга возвращена!" << endl;
        }
    }
    else {
        cout << "Книгу нельзя выдать!" << endl;
    }

    return 0;
}
