import sys

from PyQt5.QtWidgets import QApplication, QMainWindow
from ui_calculator import Ui_MainWindow


class CalculatorApp(QMainWindow):

    def __init__(self):
        super().__init__()

        self.ui = Ui_MainWindow()
        self.ui.setupUi(self)

        self.first_number = 0
        self.operation = ""

        buttons = [
            (self.ui.btn0, "0"),
            (self.ui.btn1, "1"),
            (self.ui.btn2, "2"),
            (self.ui.btn3, "3"),
            (self.ui.btn4, "4"),
            (self.ui.btn5, "5"),
            (self.ui.btn6, "6"),
            (self.ui.btn7, "7"),
            (self.ui.btn8, "8"),
            (self.ui.btn9, "9")
        ]

        for button, digit in buttons:
            button.clicked.connect(
                lambda checked=False, d=digit: self.add_digit(d)
            )

        self.ui.btnAdd.clicked.connect(
            lambda checked=False: self.set_operation("+")
        )

        self.ui.btnSub.clicked.connect(
            lambda checked=False: self.set_operation("-")
        )

        self.ui.btnMul.clicked.connect(
            lambda checked=False: self.set_operation("*")
        )

        self.ui.btnDiv.clicked.connect(
            lambda checked=False: self.set_operation("/")
        )

        self.ui.btnClear.clicked.connect(
            lambda checked=False: self.clear()
        )

        self.ui.btnEquals.clicked.connect(
            lambda checked=False: self.calculate()
        )

    def add_digit(self, digit):
        if self.ui.display.text() == "Error":
            self.ui.display.clear()

        self.ui.display.setText(
            self.ui.display.text() + digit
        )

    def set_operation(self, op):
        try:
            self.first_number = float(self.ui.display.text())
            self.operation = op
            self.ui.display.clear()

        except ValueError:
            self.ui.display.setText("Error")

    def calculate(self):
        try:
            second_number = float(self.ui.display.text())

            if self.operation == "+":
                result = self.first_number + second_number

            elif self.operation == "-":
                result = self.first_number - second_number

            elif self.operation == "*":
                result = self.first_number * second_number

            elif self.operation == "/":
                if second_number == 0:
                    self.ui.display.setText("Error")
                    return

                result = self.first_number / second_number

            else:
                return

            self.ui.display.setText(str(result))
            self.operation = ""

        except ValueError:
            self.ui.display.setText("Error")

    def clear(self):
        self.ui.display.clear()
        self.first_number = 0
        self.operation = ""


if __name__ == "__main__":
    app = QApplication(sys.argv)

    window = CalculatorApp()
    window.show()

    sys.exit(app.exec_())