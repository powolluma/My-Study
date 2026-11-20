#include <iostream>
#include <string>
#include <vector>
#include <stdexcept>
#include <sstream>
using namespace std;

class BankAccount {
private:
    double balance;
    string owner;
    vector<string> history;

public:
    BankAccount(string owner, double balance) {
        this->owner = owner;
        this->balance = balance;
    }

    void deposit(double amount) {
        if (amount <= 0) {
            throw invalid_argument("Сумма должна быть больше нуля");
        }

        balance += amount;

        ostringstream s;
        s << "Пополнение: " << amount;
        history.push_back(s.str());
    }

    void withdraw(double amount) {
        if (amount <= 0) {
            throw invalid_argument("Сумма должна быть больше нуля");
        }

        if (amount > balance) {
            throw invalid_argument("Недостаточно средств");
        }

        balance -= amount;

        ostringstream s;
        s << "Снятие: " << amount;
        history.push_back(s.str());
    }

    double getBalance() const {
        return balance;
    }

    const vector<string>& getHistory() const {
        return history;
    }
};

int main() {
    BankAccount account("Иван", 1000);

    try {
        account.deposit(500);
        account.withdraw(200);

        cout << "Баланс: " << account.getBalance() << endl;

        cout << "История операций:" << endl;
        for (const string& operation : account.getHistory()) {
            cout << operation << endl;
        }

        account.deposit(-100);
    }
    catch (const std::invalid_argument& e) {
        cout << "Ошибка: " << e.what() << endl;
    }

    try {
        account.withdraw(5000);
    }
    catch (const std::invalid_argument& e) {
        cout << "Ошибка: " << e.what() << endl;
    }

    return 0;
}
