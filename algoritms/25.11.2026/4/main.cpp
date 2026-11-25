#include <QApplication>
#include <QWidget>
#include <QLineEdit>
#include <QPushButton>
#include <QListWidget>
#include <QVBoxLayout>
#include <QMessageBox>

#include <vector>
using namespace std;


class Task {
public:
    QString text;
    bool done;

    Task(QString t) {
        text = t;
        done = false;
    }
};


class TaskList {
private:
    vector<Task> tasks;

public:
    void add(Task task) {
        tasks.push_back(task);
    }

    void mark_done(int index) {
        if (index >= 0 && index < tasks.size()) {
            tasks[index].done = true;
        }
    }

    void remove(int index) {
        if (index >= 0 && index < tasks.size()) {
            tasks.erase(tasks.begin() + index);
        }
    }

    void clear_done() {
        for (int i = 0; i < tasks.size();) {
            if (tasks[i].done) {
                tasks.erase(tasks.begin() + i);
            } else {
                i++;
            }
        }
    }

    const vector<Task>& getTasks() const {
        return tasks;
    }
};


int main(int argc, char *argv[]) {
    QApplication app(argc, argv);

    TaskList taskList;

    QWidget window;
    window.setWindowTitle("Список задач");

    QVBoxLayout *layout = new QVBoxLayout(&window);

    QLineEdit *taskEdit = new QLineEdit();
    taskEdit->setPlaceholderText("Введите задачу");

    QPushButton *addButton = new QPushButton("Добавить");
    QPushButton *doneButton = new QPushButton("Отметить выполненной");
    QPushButton *removeButton = new QPushButton("Удалить");
    QPushButton *clearButton = new QPushButton("Очистить выполненные");

    QListWidget *listWidget = new QListWidget();

    layout->addWidget(taskEdit);
    layout->addWidget(addButton);
    layout->addWidget(listWidget);
    layout->addWidget(doneButton);
    layout->addWidget(removeButton);
    layout->addWidget(clearButton);

    auto updateList = [&]() {
        listWidget->clear();

        for (const Task& task : taskList.getTasks()) {
            if (task.done) {
                listWidget->addItem("[x] " + task.text);
            } else {
                listWidget->addItem("[ ] " + task.text);
            }
        }
    };

    QObject::connect(addButton, &QPushButton::clicked, [&]() {
        QString text = taskEdit->text();

        if (text.isEmpty()) {
            QMessageBox::information(&window, "Ошибка", "Введите задачу");
            return;
        }

        taskList.add(Task(text));
        taskEdit->clear();

        updateList();
    });

    QObject::connect(doneButton, &QPushButton::clicked, [&]() {
        int index = listWidget->currentRow();

        if (index == -1) {
            QMessageBox::information(&window, "Ошибка", "Выберите задачу");
            return;
        }

        taskList.mark_done(index);
        updateList();
    });

    QObject::connect(removeButton, &QPushButton::clicked, [&]() {
        int index = listWidget->currentRow();

        if (index == -1) {
            QMessageBox::information(&window, "Ошибка", "Выберите задачу");
            return;
        }

        taskList.remove(index);
        updateList();
    });

    QObject::connect(clearButton, &QPushButton::clicked, [&]() {
        taskList.clear_done();
        updateList();
    });

    window.show();

    return app.exec();
}
