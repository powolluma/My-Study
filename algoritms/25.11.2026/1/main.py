import tkinter as tk
from tkinter import messagebox


class Student:
    def __init__(self, surname, name, record_book, average):
        self.surname = surname
        self.name = name
        self.record_book = record_book
        self.average = average

    def получить_фамилию(self):
        return self.surname

    def получить_средний_балл(self):
        return self.average

    def изменить_средний_балл(self, новый_балл):
        self.average = новый_балл


class Group:
    def __init__(self):
        self.students = []

    def add_student(self, student):
        self.students.append(student)

    def best_student(self):
        if len(self.students) == 0:
            return None

        return max(self.students, key=lambda s: s.получить_средний_балл())


group = Group()

window = tk.Tk()
window.title("Список группы")
window.geometry("450x500")

tk.Label(window, text="Фамилия:").pack()
surname_entry = tk.Entry(window)
surname_entry.pack()

tk.Label(window, text="Имя:").pack()
name_entry = tk.Entry(window)
name_entry.pack()

tk.Label(window, text="Номер зачетки:").pack()
record_entry = tk.Entry(window)
record_entry.pack()

tk.Label(window, text="Средний балл:").pack()
average_entry = tk.Entry(window)
average_entry.pack()

listbox = tk.Listbox(window, width=55, height=12)
listbox.pack(pady=10)


def add_student():
    surname = surname_entry.get()
    name = name_entry.get()
    record = record_entry.get()

    try:
        average = float(average_entry.get())
    except ValueError:
        messagebox.showinfo("Ошибка", "Введите правильный средний балл")
        return

    if surname == "" or name == "" or record == "":
        messagebox.showinfo("Ошибка", "Заполните все поля")
        return

    student = Student(surname, name, record, average)
    group.add_student(student)

    listbox.insert(
        tk.END,
        f"{surname} {name}, зачетка: {record}, балл: {average}"
    )

    surname_entry.delete(0, tk.END)
    name_entry.delete(0, tk.END)
    record_entry.delete(0, tk.END)
    average_entry.delete(0, tk.END)


def show_best_student():
    student = group.best_student()

    if student is None:
        best_label.config(text="Список студентов пуст")
    else:
        best_label.config(
            text="Лучший студент: " + student.получить_фамилию()
        )


tk.Button(window, text="Добавить", command=add_student).pack(pady=5)

tk.Button(
    window,
    text="Лучший студент",
    command=show_best_student
).pack(pady=5)

best_label = tk.Label(window, text="Лучший студент: ")
best_label.pack(pady=10)

window.mainloop()
