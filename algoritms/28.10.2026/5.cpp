#include <iostream>
#include <string>
#include <cstdlib>
#include <ctime>
#include <algorithm>
using namespace std;

class Pet {
private:
    string name;
    int hunger;
    int happiness;

    void setHunger(int value) {
        hunger = max(0, min(100, value));
    }

    void setHappiness(int value) {
        happiness = max(0, min(100, value));
    }

public:
    Pet(string petName) {
        name = petName;
        hunger = 50;
        happiness = 50;
    }

    void feed() {
        setHunger(hunger - 20);
        setHappiness(happiness + 5);
    }

    void play() {
        setHappiness(happiness + 15);
        setHunger(hunger + 10);
    }

    void tick() {
        setHunger(hunger + 5);
        setHappiness(happiness - 3);
    }

    string mood() const {
        if (happiness >= 70) {
            return "счастлив";
        } else if (happiness >= 40) {
            return "нормально";
        } else {
            return "грустен";
        }
    }

    void show() const {
        cout << "Имя: " << name
             << ", голод: " << hunger
             << ", счастье: " << happiness
             << ", настроение: " << mood()
             << endl;
    }
};

int main() {
    srand(time(0));

    Pet pet("Barsik");

    for (int i = 0; i < 10; i++) {
        int action = rand() % 3;

        cout << "\nШаг " << i + 1 << ". Действие: ";

        if (action == 0) {
            cout << "кормить" << endl;
            pet.feed();
        } else if (action == 1) {
            cout << "играть" << endl;
            pet.play();
        } else {
            cout << "ждать" << endl;
            pet.tick();
        }

        pet.show();
    }

    return 0;
}