#include <QApplication>
#include <QWidget>
#include <QLineEdit>
#include <QPushButton>
#include <QListWidget>
#include <QVBoxLayout>
#include <QFormLayout>
#include <QMessageBox>

#include <vector>
#include <algorithm>
#include <iterator>

using namespace std;


class Book {
private:
    QString author;
    QString title;
    int year;

public:
    Book(QString a, QString t, int y) {
        author = a;
        title = t;
        year = y;
    }

    QString getAuthor() const {
        return author;
    }

    QString getTitle() const {
        return title;
    }

    int getYear() const {
        return year;
    }
};


class Library {
private:
    vector<Book> books;

public:
    void add(Book book) {
        books.push_back(book);
    }

    vector<int> find_by_author(QString author) const {
        vector<int> result;

        auto it = books.begin();

        while (it != books.end()) {
            it = find_if(it, books.end(), [&](const Book& book) {
                return book.getAuthor().contains(
                    author,
                    Qt::CaseInsensitive
                );
            });

            if (it == books.end()) {
                break;
            }

            result.push_back(distance(books.begin(), it));

            ++it;
        }

        return result;
    }

    void remove(int index) {
        if (index >= 0 && index < books.size()) {
            books.erase(books.begin() + index);
        }
    }

    const vector<Book>& all_books() const {
        return books;
    }
};


int main(int argc, char *argv[]) {
    QApplication app(argc, argv);

    Library library;

    QWidget window;
    window.setWindowTitle("Библиотека");

    QVBoxLayout *layout = new QVBoxLayout(&window);
    QFormLayout *form = new QFormLayout();

    QLineEdit *authorEdit = new QLineEdit();
    QLineEdit *titleEdit = new QLineEdit();
    QLineEdit *yearEdit = new QLineEdit();
    QLineEdit *searchEdit = new QLineEdit();

    form->addRow("Автор:", authorEdit);
    form->addRow("Название:", titleEdit);
    form->addRow("Год:", yearEdit);
    form->addRow("Поиск по автору:", searchEdit);

    layout->addLayout(form);

    QPushButton *addButton = new QPushButton("Добавить книгу");
    QPushButton *findButton = new QPushButton("Найти");
    QPushButton *removeButton = new QPushButton("Удалить выбранную книгу");

    QListWidget *listWidget = new QListWidget();

    layout->addWidget(addButton);
    layout->addWidget(findButton);
    layout->addWidget(listWidget);
    layout->addWidget(removeButton);

    vector<int> shownIndexes;

    auto showBooks = [&]() {
        listWidget->clear();
        shownIndexes.clear();

        for (int i = 0; i < library.all_books().size(); i++) {
            const Book& book = library.all_books()[i];

            listWidget->addItem(
                book.getAuthor() + " — " +
                book.getTitle() + " (" +
                QString::number(book.getYear()) + ")"
            );

            shownIndexes.push_back(i);
        }
    };

    QObject::connect(addButton, &QPushButton::clicked, [&]() {
        QString author = authorEdit->text();
        QString title = titleEdit->text();

        bool ok;
        int year = yearEdit->text().toInt(&ok);

        if (author.isEmpty() || title.isEmpty() || !ok) {
            QMessageBox::information(
                &window,
                "Ошибка",
                "Заполните все поля правильно"
            );
            return;
        }

        Book book(author, title, year);
        library.add(book);

        authorEdit->clear();
        titleEdit->clear();
        yearEdit->clear();

        showBooks();
    });

    QObject::connect(findButton, &QPushButton::clicked, [&]() {
        QString author = searchEdit->text();

        if (author.isEmpty()) {
            showBooks();
            return;
        }

        listWidget->clear();
        shownIndexes.clear();

        vector<int> result = library.find_by_author(author);

        for (int index : result) {
            const Book& book = library.all_books()[index];

            listWidget->addItem(
                book.getAuthor() + " — " +
                book.getTitle() + " (" +
                QString::number(book.getYear()) + ")"
            );

            shownIndexes.push_back(index);
        }
    });

    QObject::connect(removeButton, &QPushButton::clicked, [&]() {
        int row = listWidget->currentRow();

        if (row == -1) {
            QMessageBox::information(
                &window,
                "Ошибка",
                "Выберите книгу"
            );
            return;
        }

        int index = shownIndexes[row];

        library.remove(index);

        showBooks();
    });

    window.show();

    return app.exec();
}
