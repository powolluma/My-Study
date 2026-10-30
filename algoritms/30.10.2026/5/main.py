import sys
from pathlib import Path

from PySide6.QtCore import QFile, QIODevice
from PySide6.QtUiTools import QUiLoader
from PySide6.QtWidgets import QApplication, QComboBox, QLabel, QLineEdit, QPushButton


class MainWindow:
    def __init__(self):
        ui_file = QFile(str(Path(__file__).with_name("mainwindow.ui")))
        ui_file.open(QIODevice.OpenModeFlag.ReadOnly)
        self.window = QUiLoader().load(ui_file)
        ui_file.close()

        self.temperature_input = self.window.findChild(QLineEdit, "lineEditTemperature")
        self.mode_box = self.window.findChild(QComboBox, "comboBoxMode")
        self.button = self.window.findChild(QPushButton, "btnConvert")
        self.result_label = self.window.findChild(QLabel, "labelResult")

        self.temperature_input.textChanged.connect(self.update_button)
        self.button.clicked.connect(self.convert_temperature)

    def update_button(self, text):
        self.button.setEnabled(bool(text.strip()))

    def convert_temperature(self):
        text = self.temperature_input.text().strip().replace(",", ".")

        try:
            temperature = float(text)
        except ValueError:
            self.result_label.setText("Введите число")
            return

        if self.mode_box.currentIndex() == 0:
            result = temperature * 9 / 5 + 32
            self.result_label.setText(f"Результат: {result:g} °F")
        else:
            result = (temperature - 32) * 5 / 9
            self.result_label.setText(f"Результат: {result:g} °C")

    def show(self):
        self.window.show()


if __name__ == "__main__":
    app = QApplication(sys.argv)
    main_window = MainWindow()
    main_window.show()
    sys.exit(app.exec())
