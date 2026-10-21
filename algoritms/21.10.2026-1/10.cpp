#include <iostream>
#include <map>
#include <string>
using namespace std;

int main() {
    map<string, string> book;
    string command, name, phone;

    while (cin >> command) {
        if (command == "ADD") {
            cin >> name >> phone;
            book[name] = phone;
        }
        else if (command == "FIND") {
            cin >> name;

            auto it = book.find(name);

            if (it != book.end())
                cout << it->second << '\n';
            else
                cout << "Контакт не найден\n";
        }
        else if (command == "DELETE" || command == "DEL") {
            cin >> name;

            if (book.erase(name))
                cout << "Удалено\n";
            else
                cout << "Контакт не найден\n";
        }
        else if (command == "LIST") {
            for (auto p : book)
                cout << p.first << ' ' << p.second << '\n';
        }
    }

    return 0;
}