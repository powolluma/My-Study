#include <iostream>
#include <string>
using namespace std;

string reverseStr(const string& s) {
    if (s.size() <= 1) return s;
    return reverseStr(s.substr(1)) + s[0];
}

bool isPalindrome(const string& s, int l, int r) {
    if (l >= r) return true;
    if (s[l] != s[r]) return false;
    return isPalindrome(s, l + 1, r - 1);
}

int main() {
    string s;
    cout << "Введите строку: ";
    getline(cin, s);
    cout << "Разворот: " << reverseStr(s) << endl;
    cout << "Палиндром: " << (isPalindrome(s, 0, s.size() - 1) ? "да" : "нет") << endl;
    return 0;
}