import sys
from PyQt6.QtWidgets import (
    QApplication, QWidget, QLineEdit,
    QPushButton, QLabel, QVBoxLayout, QMessageBox
)


class Calculator(QWidget):
    def __init__(self):
        super().__init__()

        self.setWindowTitle("Калькулятор")

        self.input1 = QLineEdit()
        self.input1.setPlaceholderText("Первое число")

        self.input2 = QLineEdit()
        self.input2.setPlaceholderText("Второе число")

        self.button = QPushButton("Сложить")
        self.result = QLabel("Результат: ")

        self.button.clicked.connect(self.add_numbers)

        layout = QVBoxLayout()
        layout.addWidget(self.input1)
        layout.addWidget(self.input2)
        layout.addWidget(self.button)
        layout.addWidget(self.result)

        self.setLayout(layout)

    def add_numbers(self):
        try:
            a = float(self.input1.text())
            b = float(self.input2.text())

            self.result.setText(f"Результат: {a + b}")

        except ValueError:
            QMessageBox.warning(self, "Ошибка", "Введите правильные числа!")


app = QApplication(sys.argv)
window = Calculator()
window.show()
sys.exit(app.exec())