import sys
from PyQt6.QtWidgets import (
    QApplication, QWidget, QPushButton, QVBoxLayout
)


class ColorWindow(QWidget):
    def __init__(self):
        super().__init__()

        self.setWindowTitle("Переключатель цвета")

        layout = QVBoxLayout()

        self.red_button = QPushButton("Красный")
        self.green_button = QPushButton("Зеленый")
        self.blue_button = QPushButton("Синий")
        self.reset_button = QPushButton("Сброс")

        layout.addWidget(self.red_button)
        layout.addWidget(self.green_button)
        layout.addWidget(self.blue_button)
        layout.addWidget(self.reset_button)

        self.setLayout(layout)

        self.red_button.clicked.connect(self.change_color)
        self.green_button.clicked.connect(self.change_color)
        self.blue_button.clicked.connect(self.change_color)
        self.reset_button.clicked.connect(self.change_color)

    def change_color(self):
        button = self.sender()

        colors = {
            "Красный": "red",
            "Зелёный": "green",
            "Синий": "blue",
            "Сброс": "white"
        }

        color = colors[button.text()]
        self.setStyleSheet(f"background-color: {color};")


app = QApplication(sys.argv)
window = ColorWindow()
window.show()
sys.exit(app.exec())