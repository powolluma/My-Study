import math
import sys

from PyQt5.QtWidgets import (
    QApplication,
    QWidget,
    QLabel,
    QLineEdit,
    QPushButton,
    QVBoxLayout,
    QHBoxLayout,
)


class BmiCalculator(QWidget):
    def __init__(self):
        super().__init__()

        self.setWindowTitle("Калькулятор ИМТ")
        self.resize(380, 300)

        layout = QVBoxLayout()
        layout.setSpacing(12)

        title = QLabel("Калькулятор индекса массы тела")
        title.setStyleSheet("font-size: 18px; font-weight: bold;")
        layout.addWidget(title)

        layout.addWidget(QLabel("Рост (м):"))
        self.height_input = QLineEdit()
        self.height_input.setPlaceholderText("Например, 1.75")
        layout.addWidget(self.height_input)

        layout.addWidget(QLabel("Вес (кг):"))
        self.weight_input = QLineEdit()
        self.weight_input.setPlaceholderText("Например, 70")
        layout.addWidget(self.weight_input)

        buttons_layout = QHBoxLayout()
        self.calculate_button = QPushButton("Рассчитать")
        self.clear_button = QPushButton("Очистить")
        buttons_layout.addWidget(self.calculate_button)
        buttons_layout.addWidget(self.clear_button)
        layout.addLayout(buttons_layout)

        self.result_label = QLabel("Результат появится здесь")
        self.result_label.setWordWrap(True)
        layout.addWidget(self.result_label)

        self.setLayout(layout)

        self.calculate_button.clicked.connect(self.calculate)
        self.clear_button.clicked.connect(self.clear_fields)

    def calculate(self):
        try:
            height_text = self.height_input.text().strip().replace(",", ".")
            weight_text = self.weight_input.text().strip().replace(",", ".")

            height = float(height_text)
            weight = float(weight_text)

            if not math.isfinite(height) or not math.isfinite(weight):
                self.result_label.setText("Ошибка: введите обычные числовые значения.")
                return

            if height <= 0 or weight <= 0:
                self.result_label.setText("Ошибка: рост и вес должны быть больше нуля.")
                return

            bmi = weight / (height * height)

            if bmi < 18.5:
                interpretation = "Недостаточный вес"
            elif bmi < 25:
                interpretation = "Норма"
            elif bmi < 30:
                interpretation = "Избыточный вес"
            else:
                interpretation = "Ожирение"

            self.result_label.setText(f"ИМТ: {bmi:.2f}\n{interpretation}")

        except ValueError:
            self.result_label.setText("Ошибка: введите числа в поля роста и веса.")
        except OverflowError:
            self.result_label.setText("Ошибка: слишком большое значение.")

    def clear_fields(self):
        self.height_input.clear()
        self.weight_input.clear()
        self.result_label.setText("Результат появится здесь")
        self.height_input.setFocus()


if __name__ == "__main__":
    app = QApplication(sys.argv)
    window = BmiCalculator()
    window.show()
    sys.exit(app.exec_())
