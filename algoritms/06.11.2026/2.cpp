#include <iostream>
#include <string>
#include <stdexcept>

using namespace std;

class Student {
private:
    string name_;
    int age_;
    double grade_;

public:
    Student(string name, int age, double grade) {
        setName(name);
        setAge(age);
        setGrade(grade);
    }

    string getName() const {
        return name_;
    }

    void setName(string name) {
        if (name.empty()) {
            throw invalid_argument("Имя не должно быть пустым");
        }
        name_ = name;
    }

    int getAge() const {
        return age_;
    }

    void setAge(int age) {
        if (age < 16 || age > 100) {
            throw invalid_argument("Возраст должен быть от 16 до 100");
        }
        age_ = age;
    }

    double getGrade() const {
        return grade_;
    }

    void setGrade(double grade) {
        if (grade < 0 || grade > 100) {
            throw invalid_argument("Оценка должна быть от 0 до 100");
        }
        grade_ = grade;
    }

    bool isExcellent() const {
        return grade_ >= 90;
    }

    // Свойства
    __declspec(property(get = getAge, put = setAge)) int Age;
    __declspec(property(get = getGrade)) double Grade;
};

int main() {
    Student student("Иван", 20, 95);

    cout << "Имя: " << student.getName() << endl;
    cout << "Возраст: " << student.getAge() << endl;
    cout << "Оценка: " << student.getGrade() << endl;

    cout << "Возраст через свойство: " << student.Age << endl;
    cout << "Оценка через свойство: " << student.Grade << endl;

    if (student.isExcellent()) {
        cout << "Отличник" << endl;
    }
    else {
        cout << "Не отличник" << endl;
    }

    return 0;
}