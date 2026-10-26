#include <iostream>
using namespace std;

class PiggyBank {
private:
    int money;

public:
    PiggyBank() {
        money = 0;
    }

    void put(int amount) {
        money += amount;
    }

    void take(int amount) {
        if (amount <= money) {
            money -= amount;
        } else {
            cout << "Недостаточно денег" << endl;
        }
    }

    void show() {
        cout << "В копилке: " << money << endl;
    }
};

int main() {
    PiggyBank bank;

    bank.put(100);
    bank.put(50);
    bank.take(200);
    bank.show();

    return 0;
}