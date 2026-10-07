#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
using namespace std;

struct Book {
    string author;
    string title;
    int year;
    int pages;
};

int readInt(const string& prompt) {
    string s;
    while (true) {
        cout << prompt;
        getline(cin, s);
        try {
            size_t pos;
            int v = stoi(s, &pos);
            if (pos == s.size()) return v;
        } catch (...) {
        }
        cout << "Ошибка: введите целое число" << endl;
    }
}

int readPositiveInt(const string& prompt) {
    while (true) {
        int v = readInt(prompt);
        if (v > 0) return v;
        cout << "Ошибка: число должно быть больше нуля" << endl;
    }
}

string readText(const string& prompt) {
    string s;
    while (true) {
        cout << prompt;
        getline(cin, s);
        if (!s.empty()) return s;
        cout << "Ошибка: строка не должна быть пустой" << endl;
    }
}

string toLower(string s) {
    for (char& c : s) c = tolower(c);
    return s;
}

void printBooks(const vector<Book>& books) {
    if (books.empty()) {
        cout << "Ничего не найдено" << endl;
        return;
    }
    for (const Book& b : books)
        cout << b.author << " | " << b.title << " | " << b.year << " | " << b.pages << endl;
}

void addBook(vector<Book>& books) {
    Book b;
    b.author = readText("Автор: ");
    b.title = readText("Название: ");
    b.year = readPositiveInt("Год: ");
    b.pages = readPositiveInt("Страниц: ");
    books.push_back(b);
}

void removeBook(vector<Book>& books) {
    string title = toLower(readText("Название для удаления: "));
    for (size_t i = 0; i < books.size(); i++) {
        if (toLower(books[i].title) == title) {
            books.erase(books.begin() + i);
            cout << "Удалено" << endl;
            return;
        }
    }
    cout << "Книга не найдена" << endl;
}

void findByAuthor(const vector<Book>& books) {
    string author = toLower(readText("Автор: "));
    vector<Book> found;
    for (const Book& b : books)
        if (toLower(b.author) == author) found.push_back(b);
    printBooks(found);
}

void booksAfterYear(const vector<Book>& books) {
    int year = readInt("Год: ");
    vector<Book> found;
    for (const Book& b : books)
        if (b.year > year) found.push_back(b);
    printBooks(found);
}

bool byYear(const Book& a, const Book& b) {
    return a.year < b.year;
}

void sortByYear(vector<Book>& books) {
    sort(books.begin(), books.end(), byYear);
    printBooks(books);
}

void statistics(const vector<Book>& books) {
    if (books.empty()) {
        cout << "Библиотека пуста" << endl;
        return;
    }
    double total = 0;
    for (const Book& b : books) total += b.pages;
    cout << "Всего книг: " << books.size() << endl;
    cout << "Средний объём: " << total / books.size() << endl;
}

void printMenu() {
    cout << "\n1. Добавить книгу" << endl;
    cout << "2. Удалить книгу" << endl;
    cout << "3. Найти по автору" << endl;
    cout << "4. Книги после года" << endl;
    cout << "5. Сортировать по году" << endl;
    cout << "6. Статистика" << endl;
    cout << "7. Показать все" << endl;
    cout << "0. Выход" << endl;
}

int main() {
    vector<Book> books;
    int choice;
    do {
        printMenu();
        choice = readInt("Выбор: ");
        switch (choice) {
            case 1: addBook(books); break;
            case 2: removeBook(books); break;
            case 3: findByAuthor(books); break;
            case 4: booksAfterYear(books); break;
            case 5: sortByYear(books); break;
            case 6: statistics(books); break;
            case 7: printBooks(books); break;
            case 0: break;
            default: cout << "Неверный пункт меню" << endl;
        }
    } while (choice != 0);
    return 0;
}