#include <iostream>
#include <string>
using namespace std;

class TrafficLight {
private:
    string color;

public:
    TrafficLight() {
        color = "red";
    }

    void next() {
        if (color == "red") {
            color = "green";
        } else if (color == "green") {
            color = "yellow";
        } else {
            color = "red";
        }
    }

    void show() {
        cout << color << endl;
    }
};

int main() {
    TrafficLight light;

    for (int i = 0; i < 5; i++) {
        light.next();
        light.show();
    }

    return 0;
}