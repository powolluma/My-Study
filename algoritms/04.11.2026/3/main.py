import tkinter as tk

root = tk.Tk()
root.title("Работа с текстом")
root.geometry("500x400")

# Текстовое поле
text_box = tk.Text(root, width=50, height=12)
text_box.pack(pady=10)


# Функция подсчета символов
def update_count():
    text = text_box.get("1.0", "end-1c")
    count_label.config(text=f"Количество символов: {len(text)}")


# Перевод в верхний регистр
def to_upper():
    text = text_box.get("1.0", "end-1c")
    text_box.delete("1.0", tk.END)
    text_box.insert("1.0", text.upper())
    update_count()


# Перевод в нижний регистр
def to_lower():
    text = text_box.get("1.0", "end-1c")
    text_box.delete("1.0", tk.END)
    text_box.insert("1.0", text.lower())
    update_count()


# Очистка текста
def clear_text():
    text_box.delete("1.0", tk.END)
    update_count()


# Кнопки
tk.Button(root, text="В верхний регистр",
          command=to_upper).pack(pady=5)

tk.Button(root, text="В нижний регистр",
          command=to_lower).pack(pady=5)

tk.Button(root, text="Очистить",
          command=clear_text).pack(pady=5)

# Счетчик символов
count_label = tk.Label(root, text="Количество символов: 0")
count_label.pack(pady=10)

root.mainloop()