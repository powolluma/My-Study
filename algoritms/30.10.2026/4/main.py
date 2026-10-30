import sys
from pathlib import Path

from PySide6.QtCore import QFile, QIODevice
from PySide6.QtUiTools import QUiLoader
from PySide6.QtWidgets import QApplication, QLabel, QPushButton


class MainWindow:
    def __init__(self):
        ui_file = QFile(str(Path(__file__).with_name("mainwindow.ui")))
        ui_file.open(QIODevice.OpenModeFlag.ReadOnly)
        self.window = QUiLoader().load(ui_file)
        ui_file.close()

        self.count = 0
        self.label = self.window.findChild(QLabel, "labelCount")
        self.button = self.window.findChild(QPushButton, "btnClick")
        self.button.clicked.connect(self.button_clicked)

    def button_clicked(self):
        self.count += 1
        self.label.setText(f"Нажатий: {self.count}")

        if self.count == 10:
            self.button.setEnabled(False)

    def show(self):
        self.window.show()


if __name__ == "__main__":
    app = QApplication(sys.argv)
    main_window = MainWindow()
    main_window.show()
    sys.exit(app.exec())
