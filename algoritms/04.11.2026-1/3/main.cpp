#include <iostream>
#include <chrono>
using namespace std;

int main() {
    const int count = 10000000;
    double sum = 0.0;

    auto start = chrono::high_resolution_clock::now();

    for (int i = 1; i <= count; ++i) {
        sum += 1.0 / i;
    }

    auto finish = chrono::high_resolution_clock::now();
    chrono::duration<double> elapsed = finish - start;

    cout << "Sum: " << sum << endl;
    cout << "Time: " << elapsed.count() << " seconds" << endl;
    return 0;
}
