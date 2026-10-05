#include <iostream>
#include <vector>
#include <cstdlib>
#include <ctime>
#include <string>
using namespace std;

// Задача 1
/*
double add(double a, double b) { return a + b; }
double sub(double a, double b) { return a - b; }
double mul(double a, double b) { return a * b; }
double divide(double a, double b) { return a / b; }

int main() {
    double a, b;
    char op;
    cout << "Введите: число операция число (например 5 + 3): ";
    cin >> a >> op >> b;
    switch (op) {
        case '+': cout << add(a, b) << endl; break;
        case '-': cout << sub(a, b) << endl; break;
        case '*': cout << mul(a, b) << endl; break;
        case '/':
            if (b == 0) cout << "Ошибка: деление на ноль" << endl;
            else cout << divide(a, b) << endl;
            break;
        default: cout << "Неизвестная операция" << endl;
    }
    return 0;
}
*/

// Задача 2
/*
void swapValue(int a, int b) {
    int t = a; a = b; b = t;
    cout << "Внутри swapValue: " << a << " " << b << endl;
}

void swapRef(int &a, int &b) {
    int t = a; a = b; b = t;
}

int main() {
    int x = 5, y = 10;
    swapValue(x, y);
    cout << "После swapValue: " << x << " " << y << endl;
    swapRef(x, y);
    cout << "После swapRef: " << x << " " << y << endl;
    return 0;
}
*/

// Задача 3
/*
double powerIter(double x, int n) {
    double result = 1;
    for (int i = 0; i < n; i++) result *= x;
    return result;
}

double powerRec(double x, int n) {
    if (n == 0) return 1;
    return x * powerRec(x, n - 1);
}

int main() {
    double x = 2;
    int n = 10;
    double r1 = powerIter(x, n);
    double r2 = powerRec(x, n);
    cout << "Итеративно: " << r1 << endl;
    cout << "Рекурсивно: " << r2 << endl;
    cout << "Совпадают: " << (r1 == r2 ? "да" : "нет") << endl;
    return 0;
}
*/

// Задача 4
/*
void counter() {
    static int count = 0;
    count++;
    cout << "Функция вызвана раз: " << count << endl;
}

int main() {
    counter();
    counter();
    counter();
    return 0;
}
*/

// Задача 5
/*
typedef vector<vector<int> > Matrix;

void fillRandom(Matrix &m, int rows, int cols) {
    m.assign(rows, vector<int>(cols));
    for (int i = 0; i < rows; i++)
        for (int j = 0; j < cols; j++)
            m[i][j] = rand() % 10;
}

void printMatrix(const Matrix &m, int rows, int cols) {
    for (int i = 0; i < rows; i++) {
        for (int j = 0; j < cols; j++) cout << m[i][j] << " ";
        cout << endl;
    }
    cout << endl;
}

Matrix transpose(const Matrix &m, int rows, int cols) {
    Matrix t(cols, vector<int>(rows));
    for (int i = 0; i < rows; i++)
        for (int j = 0; j < cols; j++)
            t[j][i] = m[i][j];
    return t;
}

Matrix multiply(const Matrix &A, const Matrix &B) {
    int n = A.size(), m = B.size(), k = B[0].size();
    Matrix C(n, vector<int>(k, 0));
    for (int i = 0; i < n; i++)
        for (int j = 0; j < k; j++)
            for (int p = 0; p < m; p++)
                C[i][j] += A[i][p] * B[p][j];
    return C;
}

int main() {
    srand(time(0));
    Matrix A, B;
    fillRandom(A, 2, 3);
    fillRandom(B, 3, 2);
    cout << "A:" << endl; printMatrix(A, 2, 3);
    cout << "B:" << endl; printMatrix(B, 3, 2);
    cout << "A^T:" << endl; printMatrix(transpose(A, 2, 3), 3, 2);
    cout << "A*B:" << endl; printMatrix(multiply(A, B), 2, 2);
    return 0;
}
*/

// Задача 6
/*
int findMin(const vector<int> &arr) {
    int m = arr[0];
    for (size_t i = 1; i < arr.size(); i++)
        if (arr[i] < m) m = arr[i];
    return m;
}

int findMax(const vector<int> &arr) {
    int m = arr[0];
    for (size_t i = 1; i < arr.size(); i++)
        if (arr[i] > m) m = arr[i];
    return m;
}

double average(const vector<int> &arr) {
    double s = 0;
    for (size_t i = 0; i < arr.size(); i++) s += arr[i];
    return s / arr.size();
}

int countEven(const vector<int> &arr) {
    int c = 0;
    for (size_t i = 0; i < arr.size(); i++)
        if (arr[i] % 2 == 0) c++;
    return c;
}

void reverseArr(vector<int> &arr) {
    int i = 0, j = (int)arr.size() - 1;
    while (i < j) {
        int t = arr[i]; arr[i] = arr[j]; arr[j] = t;
        i++; j--;
    }
}

int main() {
    vector<int> arr = {3, 8, 1, 6, 9, 2};
    cout << "Минимум: " << findMin(arr) << endl;
    cout << "Максимум: " << findMax(arr) << endl;
    cout << "Среднее: " << average(arr) << endl;
    cout << "Чётных: " << countEven(arr) << endl;
    reverseArr(arr);
    cout << "После переворота: ";
    for (size_t i = 0; i < arr.size(); i++) cout << arr[i] << " ";
    cout << endl;
    return 0;
}
*/

// Задача 7
/*
int generateSecret() {
    return rand() % 100 + 1;
}

int readGuess() {
    int g;
    while (true) {
        cout << "Ваша догадка (1-100): ";
        if (cin >> g && g >= 1 && g <= 100) return g;
        cin.clear();
        cin.ignore(10000, '\n');
        cout << "Введите целое число от 1 до 100." << endl;
    }
}

string checkGuess(int secret, int guess) {
    if (guess < secret) return "больше";
    if (guess > secret) return "меньше";
    return "угадал";
}

void playGame() {
    int secret = generateSecret();
    int tries = 0;
    while (true) {
        int guess = readGuess();
        tries++;
        string res = checkGuess(secret, guess);
        if (res == "угадал") {
            cout << "Угадали за " << tries << " попыток!" << endl;
            break;
        }
        cout << "Загаданное число " << res << endl;
    }
}

int main() {
    srand(time(0));
    playGame();
    return 0;
}
*/