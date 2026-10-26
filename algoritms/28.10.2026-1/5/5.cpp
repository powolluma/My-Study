#include <iostream>
#include <string>
#include <vector>
#include <unordered_map>
#include <functional>
using namespace std;

class EventBus {
private:
    unordered_map<string, vector<function<void(double)>>> handlers;

public:
    void subscribe(string event, function<void(double)> handler) {
        handlers[event].push_back(handler);
    }

    void publish(string event, double value) {
        vector<function<void(double)>> copy = handlers[event];

        for (auto& handler : copy) {
            handler(value);
        }
    }
};

class TemperatureSensor {
private:
    EventBus& bus;

public:
    TemperatureSensor(EventBus& b) : bus(b) {}

    void setTemperature(double value) {
        cout << "\nТемпература изменилась: "
             << value << endl;

        bus.publish("temperature_changed", value);
    }
};

class Heater {
private:
    bool on = true;

public:
    Heater(EventBus& bus) {
        bus.subscribe("temperature_changed",
            [this](double temperature) {
                if (temperature >= 22 && on) {
                    on = false;
                    cout << "Нагреватель выключен" << endl;
                } else if (temperature <= 18 && !on) {
                    on = true;
                    cout << "Нагреватель включен" << endl;
                }
            }
        );
    }
};

class Logger {
public:
    vector<double> history;

    Logger(EventBus& bus) {
        bus.subscribe("temperature_changed",
            [this](double temperature) {
                history.push_back(temperature);

                cout << "Запись в журнал: "
                     << temperature << endl;
            }
        );
    }
};

class Alarm {
private:
    double limit;

public:
    Alarm(EventBus& bus, double value) {
        limit = value;

        bus.subscribe("temperature_changed",
            [this](double temperature) {
                if (temperature > limit) {
                    cout << "Перегрев"
                         << endl;
                }
            }
        );
    }
};

int main() {
    EventBus bus;

    TemperatureSensor sensor(bus);
    Heater heater(bus);
    Logger logger(bus);
    Alarm alarm(bus, 30);

    sensor.setTemperature(15);
    sensor.setTemperature(22);
    sensor.setTemperature(35);

    cout << "\nИстория: ";

    for (double t : logger.history) {
        cout << t << " ";
    }

    cout << endl;

    return 0;
}