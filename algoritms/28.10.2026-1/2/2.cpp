#include <iostream>
#include <vector>
using namespace std;

class IObserver {
public:
    virtual void update(double temperature) = 0;
    virtual ~IObserver() {}
};

class WeatherStation {
private:
    double temperature = 0;
    vector<IObserver*> observers;

public:
    void subscribe(IObserver* observer) {
        observers.push_back(observer);
    }

    void unsubscribe(IObserver* observer) {
        for (int i = 0; i < (int)observers.size(); i++) {
            if (observers[i] == observer) {
                observers.erase(observers.begin() + i);
                return;
            }
        }
    }

    void setTemperature(double value) {
        if (value == temperature) return;

        temperature = value;

        vector<IObserver*> copy = observers;

        for (IObserver* observer : copy) {
            observer->update(temperature);
        }
    }
};

class Display : public IObserver {
public:
    void update(double temperature) override {
        cout << "Температура: "
             << temperature << endl;
    }
};

class Logger : public IObserver {
public:
    vector<double> history;

    void update(double temperature) override {
        history.push_back(temperature);

        cout << "Записано в журнал: "
             << temperature << endl;
    }
};

class Alarm : public IObserver {
private:
    double limit;

public:
    Alarm(double value) {
        limit = value;
    }

    void update(double temperature) override {
        if (temperature > limit) {
            cout << "Высокая температура!"
                 << endl;
        }
    }
};

class OneTimeObserver : public IObserver {
private:
    WeatherStation& station;

public:
    OneTimeObserver(WeatherStation& s)
        : station(s) {}

    void update(double temperature) override {
        cout << "Одноразовый наблюдатель: "
             << temperature << endl;

        station.unsubscribe(this);
    }
};

int main() {
    WeatherStation station;

    Display display;
    Logger logger;
    Alarm alarm(30);
    OneTimeObserver observer(station);

    station.subscribe(&display);
    station.subscribe(&logger);
    station.subscribe(&alarm);
    station.subscribe(&observer);

    station.setTemperature(20);
    station.setTemperature(35);

    station.unsubscribe(&display);
    station.setTemperature(40);

    cout << "История: ";

    for (double t : logger.history) {
        cout << t << " ";
    }

    cout << endl;

    return 0;
}