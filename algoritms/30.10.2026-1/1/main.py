import tkinter as tk


count = 0


def add_one():
    global count
    count += 1
    label_count.config(text=str(count))


def reset_count():
    global count
    count = 0
    label_count.config(text=str(count))


root = tk.Tk()
root.title("Кнопка-счетчик")
root.geometry("250x150")

label_count = tk.Label(root, text="0", font=("Arial", 24))
label_count.pack(pady=10)

button_add = tk.Button(root, text="+1", command=add_one)
button_add.pack(pady=5)

button_reset = tk.Button(root, text="Сброс", command=reset_count)
button_reset.pack(pady=5)

root.mainloop()
