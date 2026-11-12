import sys
from datetime import timedelta

from PyQt5.QtWidgets import (
    QApplication,
    QWidget,
    QVBoxLayout,
    QLabel,
    QDateTimeEdit,
    QSpinBox,
    QPushButton
)

from PyQt5.QtCore import QDateTime


class TaskPlanner(QWidget):

    def __init__(self):
        super().__init__()

        self.setWindowTitle("Планировщик задач")
        self.resize(400, 300)

        layout = QVBoxLayout()

        layout.addWidget(QLabel("Дата и время начала:"))

        self.dt_start = QDateTimeEdit()
        self.dt_start.setCalendarPopup(True)
        self.dt_start.setDateTime(QDateTime.currentDateTime())
        self.dt_start.setDisplayFormat("dd.MM.yyyy HH:mm")

        layout.addWidget(self.dt_start)

        layout.addWidget(QLabel("Длительность:"))

        self.spin_duration = QSpinBox()
        self.spin_duration.setRange(1, 1440)
        self.spin_duration.setSingleStep(5)
        self.spin_duration.setValue(60)
        self.spin_duration.setSuffix(" мин")

        layout.addWidget(self.spin_duration)

        self.btn_calculate = QPushButton("Рассчитать")
        self.btn_calculate.clicked.connect(self.calculate_end)

        layout.addWidget(self.btn_calculate)


        self.lbl_result = QLabel("Результат появится здесь")
        self.lbl_result.setWordWrap(True)

        layout.addWidget(self.lbl_result)

        self.setLayout(layout)

    def calculate_end(self):

        start_dt = self.dt_start.dateTime().toPyDateTime()

        minutes = self.spin_duration.value()

        
        end_dt = start_dt + timedelta(minutes=minutes)

        
        weekdays = [
            "понедельник",
            "вторник",
            "среда",
            "четверг",
            "пятница",
            "суббота",
            "воскресенье"
        ]

        start_day = weekdays[start_dt.weekday()]
        end_day = weekdays[end_dt.weekday()]

        result_text = (
            f"Событие начинается: "
            f"{start_dt.strftime('%d.%m.%Y %H:%M')} ({start_day})\n\n"

            f"Событие заканчивается: "
            f"{end_dt.strftime('%d.%m.%Y %H:%M')} ({end_day})"
        )

        self.lbl_result.setText(result_text)


if __name__ == "__main__":

    app = QApplication(sys.argv)

    window = TaskPlanner()
    window.show()

    sys.exit(app.exec_())