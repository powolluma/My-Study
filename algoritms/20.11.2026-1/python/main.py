import sys

from PyQt5.QtCore import pyqtSignal

from PyQt5.QtWidgets import (

    QApplication, QWidget, QLabel, QLineEdit,

    QPushButton, QVBoxLayout

)





class ResultWindow(QWidget):

    closed = pyqtSignal()



    def __init__(self, student_name, group):

        super().__init__()

        self.setWindowTitle("Информация о студенте")

        self.setFixedSize(360, 180)



        layout = QVBoxLayout()

        self.result_label = QLabel(

            f"Студент: {student_name}, группа: {group}"

        )

        self.result_label.setWordWrap(True)

        layout.addWidget(self.result_label)



        back_button = QPushButton("Назад")

        back_button.clicked.connect(self.close)

        layout.addWidget(back_button)

        self.setLayout(layout)



    def closeEvent(self, event):

        self.closed.emit()

        event.accept()





class MainWindow(QWidget):

    def __init__(self):

        super().__init__()

        self.setWindowTitle("Главная форма")

        self.setFixedSize(400, 260)



        layout = QVBoxLayout()

        layout.addWidget(QLabel("Фамилия и имя:"))

        self.name_input = QLineEdit()

        layout.addWidget(self.name_input)



        layout.addWidget(QLabel("Группа:"))

        self.group_input = QLineEdit()

        layout.addWidget(self.group_input)



        self.error_label = QLabel("")

        self.error_label.setStyleSheet("color: red;")

        layout.addWidget(self.error_label)



        next_button = QPushButton("Далее")

        next_button.clicked.connect(self.open_result)

        layout.addWidget(next_button)



        exit_button = QPushButton("Выход")

        exit_button.clicked.connect(self.close)

        layout.addWidget(exit_button)

        self.setLayout(layout)



        self.result_window = None



    def open_result(self):

        student_name = self.name_input.text().strip()

        group = self.group_input.text().strip()



        if not student_name or not group:

            self.error_label.setText("Заполните оба поля.")

            return



        self.error_label.setText("")

        self.hide()

        self.result_window = ResultWindow(student_name, group)

        self.result_window.closed.connect(self.show)

        self.result_window.show()





if __name__ == "__main__":

    app = QApplication(sys.argv)

    window = MainWindow()

    window.show()

    sys.exit(app.exec_())
