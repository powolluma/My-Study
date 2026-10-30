import tkinter as tk


def add_numbers():
    try:
        number_a = float(entry_a.get())
        number_b = float(entry_b.get())
        result_label.config(text=f"Сумма: {number_a + number_b:g}")
    except ValueError:
        result_label.config(text="Введите два числа")


root = tk.Tk()
root.title("Калькулятор суммы")
root.geometry("300x200")

tk.Label(root, text="Первое число:").pack(pady=(10, 0))
entry_a = tk.Entry(root)
entry_a.pack()

tk.Label(root, text="Второе число:").pack(pady=(10, 0))
entry_b = tk.Entry(root)
entry_b.pack()

tk.Button(root, text="Сложить", command=add_numbers).pack(pady=10)
result_label = tk.Label(root, text="Результат появится здесь")
result_label.pack()

root.mainloop()
