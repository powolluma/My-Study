#include <iostream>
#include <vector>
using namespace std;

int binarySearch(const vector<int>& a, int x, int left, int right) {
    if (left > right) return -1;
    int mid = (left + right) / 2;
    if (a[mid] == x) return mid;
    if (a[mid] < x) return binarySearch(a, x, mid + 1, right);
    return binarySearch(a, x, left, mid - 1);
}

int main() {
    int n, x;
    cout << "Размер массива: ";
    cin >> n;
    vector<int> a(n);
    for (int i = 0; i < n; i++) cin >> a[i];
    cout << "Искомое значение: ";
    cin >> x;
    cout << binarySearch(a, x, 0, n - 1) << endl;
    return 0;
}