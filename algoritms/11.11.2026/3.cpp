#include <QApplication>
#include <QWidget>
#include <QLineEdit>
#include <QPushButton>
#include <QVBoxLayout>
#include <QMessageBox>

class PasswordWindow : public QWidget
{
    Q_OBJECT

public:
    PasswordWindow()
    {
        setWindowTitle("Проверка пароля");

        passwordEdit = new QLineEdit();
        passwordEdit->setEchoMode(QLineEdit::Password);
        passwordEdit->setPlaceholderText("Введите пароль");

        QPushButton *button = new QPushButton("Проверить");

        QVBoxLayout *layout = new QVBoxLayout();

        layout->addWidget(passwordEdit);
        layout->addWidget(button);

        setLayout(layout);

        connect(button, &QPushButton::clicked,
                this, &PasswordWindow::checkPassword);
    }

private slots:

    void checkPassword()
    {
        if (passwordEdit->text() == "12345")
        {
            QMessageBox::information(
                this, "Результат", "Доступ разрешен"
            );
        }
        else
        {
            QMessageBox::critical(
                this, "Ошибка", "Неверный пароль"
            );
        }
    }

private:
    QLineEdit *passwordEdit;
};

int main(int argc, char *argv[])
{
    QApplication app(argc, argv);

    PasswordWindow window;
    window.show();

    return app.exec();
}

#include "main.moc"