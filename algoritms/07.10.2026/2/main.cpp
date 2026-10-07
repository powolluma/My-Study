#include <iostream>
#include <string>
#include <cctype>
using namespace std;

bool isVowel(char ch) {
    ch = tolower(ch);
    return ch == 'a' || ch == 'e' || ch == 'i' || ch == 'o' || ch == 'u' || ch == 'y';
}

int countVowels(const string& s) {
    int c = 0;
    for (char ch : s)
        if (isalpha(ch) && isVowel(ch)) c++;
    return c;
}

int countConsonants(const string& s) {
    int c = 0;
    for (char ch : s)
        if (isalpha(ch) && !isVowel(ch)) c++;
    return c;
}

string longestWord(const string& s) {
    string best = "", cur = "";
    for (char ch : s + " ") {
        if (ch == ' ') {
            if (cur.size() > best.size()) best = cur;
            cur = "";
        } else {
            cur += ch;
        }
    }
    return best;
}

bool isPalindrome(const string& s) {
    string t = "";
    for (char ch : s)
        if (ch != ' ') t += tolower(ch);
    int n = t.size();
    for (int i = 0; i < n / 2; i++)
        if (t[i] != t[n - 1 - i]) return false;
    return true;
}

int main() {
    string s;
    cout << "Введите строку: ";
    getline(cin, s);
    cout << "Гласных: " << countVowels(s) << endl;
    cout << "Согласных: " << countConsonants(s) << endl;
    cout << "Самое длинное слово: " << longestWord(s) << endl;
    cout << "Палиндром: " << (isPalindrome(s) ? "да" : "нет") << endl;
    return 0;
}