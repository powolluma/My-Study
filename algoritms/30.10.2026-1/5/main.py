import tkinter as tk
from tkinter import ttk


def submit_form():
    name = entry_name.get().strip()
    age = entry_age.get().strip()
    city = city_box.get()

    if not name or not age or not city:
        result_label.config(text="Заполните все поля")
        return

    result_label.config(text=f"{name}, {age}, {city}")


root = tk.Tk()
root.title("Анкета")
root.geometry("350x260")

tk.Label(root, text="Имя:").pack(pady=(10, 0))
entry_name = tk.Entry(root)
entry_name.pack()

tk.Label(root, text="Возраст:").pack(pady=(10, 0))
entry_age = tk.Entry(root)
entry_age.pack()

tk.Label(root, text="Город:").pack(pady=(10, 0))
city_box = ttk.Combobox(root, values=("Москва", "Санкт-Петербург", "Новосибирск"), state="readonly")
city_box.pack()

tk.Button(root, text="Отправить", command=submit_form).pack(pady=12)
result_label = tk.Label(root, text="")
result_label.pack()

root.mainloop()
