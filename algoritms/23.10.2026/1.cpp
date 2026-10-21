#include <iostream>
using namespace std;

class Bicycle {
public:
    int speed;
    int gear;

    Bicycle(int initial_speed, int initial_gear)
        : speed(initial_speed), gear(initial_gear) {}

    void speedUp(int amount) {
        speed += amount;
    }
};

int main() {
    Bicycle bicycle(10, 3);
    bicycle.speedUp(5);
    cout << bicycle.speed << '\n';
    return 0;
}
