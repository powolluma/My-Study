class Student:
    def __init__(self, name, course, marks=None):
        self.name = name
        self.course = course
        self.marks = list(marks) if marks is not None else []

    def add_mark(self, m):
        self.marks.append(m)

    def average(self):
        return sum(self.marks) / len(self.marks) if self.marks else 0

    def next_course(self):
        self.course += 1

    def is_excellent(self):
        return bool(self.marks) and self.average() >= 4.5

    def __str__(self):
        return f"{self.name}, course {self.course}, marks: {self.marks}"


if __name__ == "__main__":
    students = [Student("Анна", 1), Student("Павел", 2)]
    students[0].add_mark(5)
    students[0].add_mark(4)
    students[1].add_mark(5)
    students[1].add_mark(5)

    for student in students:
        student.next_course()
        print(f"{student}; average: {student.average()}")
