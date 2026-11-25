
import sys
import random

from PyQt5.QtWidgets import (
    QApplication,
    QWidget,
    QLabel,
    QLineEdit,
    QPushButton,
    QVBoxLayout
)
from PyQt5.QtCore import Qt


class GuessNumberGame(QWidget):
    def __init__(self):
        super().__init__()

        self.setWindowTitle("Угадай число")
        self.setFixedSize(350, 260)

        self.secret_number = 0
        self.attempts = 0
        self.max_attempts = 7

        # Создание элементов интерфейса
        self.label_title = QLabel("Игра «Угадай число»")
        self.label_title.setAlignment(Qt.AlignCenter)

        self.label_info = QLabel("Я загадал число от 1 до 100.")
        self.label_info.setAlignment(Qt.AlignCenter)

        self.line_edit_guess = QLineEdit()
        self.line_edit_guess.setPlaceholderText("Введите число от 1 до 100")

        self.push_button_check = QPushButton("Проверить")
        self.push_button_new_game = QPushButton("Новая игра")

        self.label_hint = QLabel("Удачи!")
        self.label_hint.setAlignment(Qt.AlignCenter)

        self.label_attempts = QLabel("Попытки: 0 / 7")
        self.label_attempts.setAlignment(Qt.AlignCenter)

        layout = QVBoxLayout()

        layout.addWidget(self.label_title)
        layout.addWidget(self.label_info)
        layout.addWidget(self.line_edit_guess)
        layout.addWidget(self.push_button_check)
        layout.addWidget(self.label_hint)
        layout.addWidget(self.label_attempts)
        layout.addWidget(self.push_button_new_game)

        self.setLayout(layout)

        self.push_button_check.clicked.connect(self.check_guess)
        self.push_button_new_game.clicked.connect(self.new_game)
        self.line_edit_guess.returnPressed.connect(self.check_guess)

        self.new_game()

    def new_game(self):
        self.secret_number = random.randint(1, 100)

        self.attempts = 0

        self.label_attempts.setText("Попытки: 0 / 7")
        self.label_hint.setText("Я загадал новое число. Попробуйте угадать!")

        self.line_edit_guess.clear()
        self.line_edit_guess.setEnabled(True)
        self.push_button_check.setEnabled(True)

        self.line_edit_guess.setFocus()

    def check_guess(self):
        if self.attempts >= self.max_attempts:
            return

        text = self.line_edit_guess.text().strip()

        if not text:
            self.label_hint.setText("Введите число!")
            return

        try:
            guess = int(text)
        except ValueError:
            self.label_hint.setText("Ошибка! Введите целое число.")
            return

        if guess < 1 or guess > 100:
            self.label_hint.setText("Число должно быть от 1 до 100!")
            return

        self.attempts += 1
        self.label_attempts.setText(
            f"Попытки: {self.attempts} / {self.max_attempts}"
        )

        if guess == self.secret_number:
            self.label_hint.setText("Вы угадали! Поздравляю!")
            self.finish_game()

        elif self.attempts == self.max_attempts:
            self.label_hint.setText(
                f"Попытки закончились! Число было: {self.secret_number}"
            )
            self.finish_game()

        elif guess < self.secret_number:
            self.label_hint.setText("Загаданное число больше.")

        else:
            self.label_hint.setText("Загаданное число меньше.")

        self.line_edit_guess.clear()

    def finish_game(self):
        self.line_edit_guess.setEnabled(False)
        self.push_button_check.setEnabled(False)


if __name__ == "__main__":
    app = QApplication(sys.argv)

    game = GuessNumberGame()
    game.show()

    sys.exit(app.exec_())
