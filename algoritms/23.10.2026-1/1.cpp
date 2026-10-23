#include <iostream>
using namespace std;

class BankAccount {
private:
    double balance_;

public:
    BankAccount(double balance) {
        if (balance < 0)
            balance_ = 0;
        else
            balance_ = balance;
    }

    void deposit(double amount) {
        if (amount > 0)
            balance_ += amount;
        else
            cout << "Сумма должна быть положительной" << endl;
    }

    void withdraw(double amount) {
        if (amount <= 0) {
            cout << "Сумма должна быть положительной" << endl;
        }
        else if (amount > balance_) {
            cout << "Недостаточно средств" << endl;
        }
        else {
            balance_ -= amount;
        }
    }

    double getBalance() const {
        return balance_;
    }
};

int main() {
    BankAccount account(1000);

    account.deposit(500);
    account.withdraw(200);

    cout << "Баланс: " << account.getBalance() << endl;

    return 0;
}