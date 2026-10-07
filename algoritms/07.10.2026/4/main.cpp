#include <iostream>
#include <vector>
using namespace std;

typedef vector<vector<int>> Matrix;

double rowAverage(const vector<int>& row) {
    double s = 0;
    for (int x : row) s += x;
    return s / row.size();
}

vector<double> studentAverages(const Matrix& g) {
    vector<double> r;
    for (const auto& row : g)
        r.push_back(rowAverage(row));
    return r;
}

vector<double> subjectAverages(const Matrix& g) {
    vector<double> r;
    for (size_t j = 0; j < g[0].size(); j++) {
        double s = 0;
        for (size_t i = 0; i < g.size(); i++)
            s += g[i][j];
        r.push_back(s / g.size());
    }
    return r;
}

int bestStudent(const vector<double>& avg) {
    int best = 0;
    for (size_t i = 0; i < avg.size(); i++)
        if (avg[i] > avg[best]) best = i;
    return best;
}

int countExcellent(const vector<double>& avg) {
    int c = 0;
    for (double a : avg)
        if (a >= 4.5) c++;
    return c;
}

int countFailing(const Matrix& g) {
    int c = 0;
    for (const auto& row : g)
        for (int x : row)
            if (x == 2) { c++; break; }
    return c;
}

Matrix readGrades(int m, int k) {
    Matrix g(m, vector<int>(k));
    for (int i = 0; i < m; i++)
        for (int j = 0; j < k; j++)
            cin >> g[i][j];
    return g;
}

void printList(const string& title, const vector<double>& v) {
    cout << title << endl;
    for (size_t i = 0; i < v.size(); i++)
        cout << i + 1 << ": " << v[i] << endl;
}

int main() {
    int m, k;
    cout << "Студентов и предметов: ";
    cin >> m >> k;
    Matrix g = readGrades(m, k);
    vector<double> st = studentAverages(g);
    vector<double> sub = subjectAverages(g);
    printList("Средний балл студентов:", st);
    printList("Средний балл по предметам:", sub);
    cout << "Лучший студент: " << bestStudent(st) + 1 << endl;
    cout << "Отличников: " << countExcellent(st) << endl;
    cout << "Двоечников: " << countFailing(g) << endl;
    return 0;
}