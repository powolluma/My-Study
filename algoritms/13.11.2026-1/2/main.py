import sys

from PyQt5.QtWidgets import (
    QApplication,
    QMainWindow,
    QWidget,
    QPushButton,
    QLineEdit,
    QLabel,
    QCheckBox,
    QVBoxLayout,
    QMessageBox
)

from PyQt5.QtCore import Qt, QEvent


class MainWindow(QMainWindow):

    def __init__(self):
        super().__init__()

        self.setWindowTitle("Демонстрация событий")
        self.setGeometry(300, 300, 350, 250)

        self.setMouseTracking(True)

        # Создание центрального виджета
        central_widget = QWidget()
        central_widget.setMouseTracking(True)
        self.setCentralWidget(central_widget)

        layout = QVBoxLayout()

        # Кнопка
        self.button = QPushButton("Нажми меня")
        self.button.setObjectName("button1")

        # Текстовое поле
        self.text_input = QLineEdit()
        self.text_input.setObjectName("textBox1")

        # Флажок
        self.checkbox = QCheckBox("Мой флажок")

        # Надпись
        self.label = QLabel("Здесь будут результаты")

        layout.addWidget(self.button)
        layout.addWidget(self.text_input)
        layout.addWidget(self.checkbox)
        layout.addWidget(self.label)

        central_widget.setLayout(layout)

        # Подключение событий
        self.button.clicked.connect(self.on_button_clicked)

        self.text_input.textChanged.connect(
            self.on_text_changed
        )

        self.checkbox.stateChanged.connect(
            self.on_checkbox_changed
        )

        # Отслеживание движения мыши
        widgets = [
            central_widget,
            self.button,
            self.text_input,
            self.checkbox,
            self.label
        ]

        for widget in widgets:
            widget.setMouseTracking(True)
            widget.installEventFilter(self)

    # Событие нажатия кнопки
    def on_button_clicked(self):
        sender = self.sender()

        if isinstance(sender, QPushButton):
            self.label.setText(
                "Событие вызвано: " + sender.objectName()
            )

            QMessageBox.information(
                self,
                "Событие",
                "Вы нажали на кнопку: " + sender.text()
            )

    # Событие изменения текста
    def on_text_changed(self, text):
        self.setWindowTitle(
            "Длина текста: " + str(len(text)) + " символов"
        )

        self.label.setText("Текущий текст: " + text)

    # Событие изменения флажка
    def on_checkbox_changed(self, state):
        if state == Qt.Checked:
            self.label.setText("Флажок включен")
        else:
            self.label.setText("Флажок выключен")

    # Событие движения мыши
    def mouseMoveEvent(self, event):
        self.setWindowTitle(
            "Координаты мыши: X=" +
            str(event.x()) + " Y=" + str(event.y())
        )

        super().mouseMoveEvent(event)

    # Отслеживание мыши над элементами окна
    def eventFilter(self, obj, event):
        if event.type() == QEvent.MouseMove:
            pos = self.mapFromGlobal(event.globalPos())

            self.setWindowTitle(
                "Координаты мыши: X=" +
                str(pos.x()) + " Y=" + str(pos.y())
            )

        return super().eventFilter(obj, event)


if __name__ == "__main__":
    app = QApplication(sys.argv)

    window = MainWindow()
    window.show()

    sys.exit(app.exec_())