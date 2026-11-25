#include <QApplication>
#include <QWidget>
#include <QLabel>
#include <QLineEdit>
#include <QPushButton>
#include <QListWidget>
#include <QVBoxLayout>
#include <QFormLayout>
#include <QMessageBox>

#include <vector>
using namespace std;


class Student {
private:
    QString surname;
    QString name;
    QString recordBook;
    double average;

public:
    Student(QString s, QString n, QString r, double a) {
        surname = s;
        name = n;
        recordBook = r;
        average = a;
    }

    QString getSurname() const {
        return surname;
    }

    QString getName() const {
        return name;
    }

    QString getRecordBook() const {
        return recordBook;
    }

    double getAverage() const {
        return average;
    }

    void setAverage(double a) {
        average = a;
    }
};


class Group {
private:
    vector<Student> students;

public:
    void addStudent(Student student) {
        students.push_back(student);
    }

    Student* bestStudent() {
        if (students.empty()) {
            return nullptr;
        }

        Student* best = &students[0];

        for (int i = 1; i < students.size(); i++) {
            if (students[i].getAverage() > best->getAverage()) {
                best = &students[i];
            }
        }

        return best;
    }
};


int main(int argc, char *argv[]) {
    QApplication app(argc, argv);

    Group group;

    QWidget window;
    window.setWindowTitle("Список группы");

    QVBoxLayout *layout = new QVBoxLayout(&window);
    QFormLayout *form = new QFormLayout();

    QLineEdit *surnameEdit = new QLineEdit();
    QLineEdit *nameEdit = new QLineEdit();
    QLineEdit *recordEdit = new QLineEdit();
    QLineEdit *averageEdit = new QLineEdit();

    form->addRow("Фамилия:", surnameEdit);
    form->addRow("Имя:", nameEdit);
    form->addRow("Зачётка:", recordEdit);
    form->addRow("Средний балл:", averageEdit);

    layout->addLayout(form);

    QPushButton *addButton = new QPushButton("Добавить");
    QPushButton *bestButton = new QPushButton("Лучший студент");

    QListWidget *listWidget = new QListWidget();
    QLabel *bestLabel = new QLabel("Лучший студент: ");

    layout->addWidget(addButton);
    layout->addWidget(listWidget);
    layout->addWidget(bestButton);
    layout->addWidget(bestLabel);

    QObject::connect(addButton, &QPushButton::clicked, [&]() {
        QString surname = surnameEdit->text();
        QString name = nameEdit->text();
        QString record = recordEdit->text();

        bool ok;
        double average = averageEdit->text().toDouble(&ok);

        if (surname.isEmpty() || name.isEmpty() || record.isEmpty() || !ok) {
            QMessageBox::information(&window, "Ошибка", "Заполните все поля правильно");
            return;
        }

        Student student(surname, name, record, average);
        group.addStudent(student);

        listWidget->addItem(
            surname + " " + name +
            ", зачётка: " + record +
            ", балл: " + QString::number(average)
        );

        surnameEdit->clear();
        nameEdit->clear();
        recordEdit->clear();
        averageEdit->clear();
    });

    QObject::connect(bestButton, &QPushButton::clicked, [&]() {
        Student* best = group.bestStudent();

        if (best == nullptr) {
            bestLabel->setText("Список студентов пуст");
        } else {
            bestLabel->setText("Лучший студент: " + best->getSurname());
        }
    });

    window.show();

    return app.exec();
}
