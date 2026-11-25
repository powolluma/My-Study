
import sys
import random

from PyQt5.QtWidgets import QApplication, QWidget
from ui_game import Ui_GameWindow


class GuessNumberGame(QWidget):
    def __init__(self):
        super().__init__()

        # Подключение интерфейса
        self.ui = Ui_GameWindow()
        self.ui.setupUi(self)

        self.secret_number = 0
        self.attempts = 0
        self.max_attempts = 7

        # Подключение кнопок
        self.ui.pushButtonCheck.clicked.connect(self.check_guess)
        self.ui.pushButtonNewGame.clicked.connect(self.new_game)

        # Проверка числа
        self.ui.lineEditGuess.returnPressed.connect(self.check_guess)

        # Начало игры
        self.new_game()

    def new_game(self):
        self.secret_number = random.randint(1, 100)

        self.attempts = 0

        self.ui.labelAttempts.setText("Попытки: 0 / 7")
        self.ui.labelHint.setText("Попробуйте угадать число!")

        self.ui.lineEditGuess.clear()
        self.ui.lineEditGuess.setEnabled(True)
        self.ui.pushButtonCheck.setEnabled(True)

        self.ui.lineEditGuess.setFocus()

    def check_guess(self):
        if self.attempts >= self.max_attempts:
            return

        text = self.ui.lineEditGuess.text().strip()

        if not text:
            self.ui.labelHint.setText("Введите число!")
            return

        try:
            guess = int(text)
        except ValueError:
            self.ui.labelHint.setText("Введите целое число!")
            return

        if guess < 1 or guess > 100:
            self.ui.labelHint.setText("Число должно быть от 1 до 100!")
            return

        self.attempts += 1

        self.ui.labelAttempts.setText(
            f"Попытки: {self.attempts} / 7"
        )

        if guess == self.secret_number:
            self.ui.labelHint.setText("Вы угадали! Поздравляю!")
            self.finish_game()

        elif self.attempts == self.max_attempts:
            self.ui.labelHint.setText(
                f"Попытки закончились! Число было: {self.secret_number}"
            )
            self.finish_game()

        elif guess < self.secret_number:
            self.ui.labelHint.setText("Загаданное число больше.")

        else:
            self.ui.labelHint.setText("Загаданное число меньше.")

        self.ui.lineEditGuess.clear()

    def finish_game(self):
        self.ui.lineEditGuess.setEnabled(False)
        self.ui.pushButtonCheck.setEnabled(False)


if __name__ == "__main__":
    app = QApplication(sys.argv)

    game = GuessNumberGame()
    game.show()

    sys.exit(app.exec_())
