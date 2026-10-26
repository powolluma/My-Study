#include <iostream>
#include <stdexcept>
using namespace std;

class Thermometer {
private:
    double celsius;

public:
    Thermometer(double value) {
        setCelsius(value);
    }

    void setCelsius(double value) {
        if (value < -273.15) {
            throw invalid_argument(
                "Температура ниже нуля"
            );
        }

        celsius = value;
    }

    double getCelsius() const {
        return celsius;
    }

    double getFahrenheit() const {
        return celsius * 9 / 5 + 32;
    }
};

int main() {
    try {
        Thermometer t(25);

        cout << "Цельсий: " << t.getCelsius() << endl;
        cout << "Фаренгейт: " << t.getFahrenheit() << endl;

        t.setCelsius(30);
        cout << "Новая температура: "
             << t.getCelsius() << endl;

        t.setCelsius(-300);
    } catch (const invalid_argument& error) {
        cout << error.what() << endl;
    }

    return 0;
}