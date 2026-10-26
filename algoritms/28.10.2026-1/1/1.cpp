#include <iostream>
#include <thread>
#include <chrono>
#include <atomic>
#include <functional>
using namespace std;

class Timer {
private:
    int seconds;
    atomic<bool> paused{false};

public:
    function<void(int)> onTick;
    function<void()> onFinish;
    function<void()> onPause;
    function<void()> onResume;

    Timer(int s) {
        seconds = s;
    }

    void pause() {
        paused = true;
        if (onPause) onPause();
    }

    void resume() {
        paused = false;
        if (onResume) onResume();
    }

    void start() {
        while (seconds > 0) {
            for (int i = 0; i < 10; i++) {
                while (paused) {
                    this_thread::sleep_for(
                        chrono::milliseconds(100)
                    );
                }

                this_thread::sleep_for(
                    chrono::milliseconds(100)
                );
            }

            if (!paused) {
                seconds--;
                if (onTick) onTick(seconds);
            }
        }

        if (onFinish) onFinish();
    }
};

int main() {
    Timer timer(5);

    timer.onTick = [](int s) {
        cout << "Осталось: " << s << endl;
    };

    timer.onFinish = []() {
        cout << "Время вышло" << endl;
    };

    timer.onPause = []() {
        cout << "Пауза" << endl;
    };

    timer.onResume = []() {
        cout << "Продолжение" << endl;
    };

    thread t([&timer]() {
        timer.start();
    });

    this_thread::sleep_for(chrono::seconds(2));
    timer.pause();

    this_thread::sleep_for(chrono::seconds(2));
    timer.resume();

    t.join();

    return 0;
}