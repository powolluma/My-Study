#include <iostream>
#include <utility>
#include "Matrix.h"
using namespace std;

int main() {
    Matrix first(2, 3);
    first.at(0, 0) = 1.5;
    first.at(1, 2) = 7.0;

    Matrix second(move(first));

    cout << second.at(0, 0) << endl;
    cout << second.at(1, 2) << endl;
    cout << "Rows: " << second.getRows() << endl;
    return 0;
}
