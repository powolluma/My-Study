class Student:
    def __init__(self):
        self.__name = ""
        self.__grades = []

    @property
    def name(self):
        return self.__name

    @name.setter
    def name(self, value):
        if not isinstance(value, str) or not value.strip():
            raise ValueError("Имя не должно быть пустым")
        self.__name = value

    @property
    def grades(self):
        return self.__grades.copy()

    def add_grade(self, value: int):
        if type(value) != int or value < 1 or value > 5:
            raise ValueError("Оценка должна быть целым числом от 1 до 5")
        self.__grades.append(value)

    def average(self) -> float:
        if len(self.__grades) == 0:
            return 0.0
        return sum(self.__grades) / len(self.__grades)


student = Student()

student.name = "Иван"

student.add_grade(5)
student.add_grade(4)
student.add_grade(3)

print("Имя:", student.name)
print("Оценки:", student.grades)
print("Средний балл:", student.average())

grades = student.grades
grades.append(1)

print("Измененный внешний список:", grades)
print("Оригинальные оценки:", student.grades)

try:
    student.add_grade(6)
except ValueError as e:
    print("Ошибка:", e)

try:
    student.name = ""
except ValueError as e:
    print("Ошибка:", e)
