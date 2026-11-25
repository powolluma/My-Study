import tkinter as tk
from tkinter import messagebox


class Task:
    def __init__(self, text):
        self.text = text
        self.done = False


class TaskList:
    def __init__(self):
        self.tasks = []

    def add(self, task):
        self.tasks.append(task)

    def mark_done(self, index):
        if 0 <= index < len(self.tasks):
            self.tasks[index].done = True

    def remove(self, index):
        if 0 <= index < len(self.tasks):
            self.tasks.pop(index)

    def clear_done(self):
        self.tasks = [task for task in self.tasks if not task.done]


task_list = TaskList()

window = tk.Tk()
window.title("Список задач")
window.geometry("400x450")

task_entry = tk.Entry(window, width=40)
task_entry.pack(pady=10)

listbox = tk.Listbox(window, width=45, height=15)
listbox.pack(pady=10)


def update_list():
    listbox.delete(0, tk.END)

    for task in task_list.tasks:
        if task.done:
            listbox.insert(tk.END, "[x] " + task.text)
        else:
            listbox.insert(tk.END, "[ ] " + task.text)


def add_task():
    text = task_entry.get()

    if text == "":
        messagebox.showinfo("Ошибка", "Введите задачу")
        return

    task = Task(text)
    task_list.add(task)

    task_entry.delete(0, tk.END)
    update_list()


def mark_task_done():
    selection = listbox.curselection()

    if not selection:
        messagebox.showinfo("Ошибка", "Выберите задачу")
        return

    index = selection[0]
    task_list.mark_done(index)
    update_list()


def remove_task():
    selection = listbox.curselection()

    if not selection:
        messagebox.showinfo("Ошибка", "Выберите задачу")
        return

    index = selection[0]
    task_list.remove(index)
    update_list()


def clear_done_tasks():
    task_list.clear_done()
    update_list()


tk.Button(
    window,
    text="Добавить",
    command=add_task
).pack(pady=3)

tk.Button(
    window,
    text="Отметить выполненной",
    command=mark_task_done
).pack(pady=3)

tk.Button(
    window,
    text="Удалить",
    command=remove_task
).pack(pady=3)

tk.Button(
    window,
    text="Очистить выполненные",
    command=clear_done_tasks
).pack(pady=3)

window.mainloop()
