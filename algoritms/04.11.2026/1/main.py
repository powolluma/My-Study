import tkinter as tk
from tkinter import ttk

root = tk.Tk()
root.title("Ползунок и прогресс-бар")
root.geometry("400x250")

# Настройка цвета прогресс-бара
style = ttk.Style()
style.theme_use("clam")
style.configure(
    "Custom.Horizontal.TProgressbar",
    background="green",
    troughcolor="lightgray"
)

# Функция изменения прогресс-бара
def update_progress(value):
    progress["value"] = float(value)

# Создание ползунка
scale = tk.Scale(
    root,
    from_=0,
    to=100,
    orient=tk.HORIZONTAL,
    length=300,
    label="Выберите значение",
    bg="white",
    troughcolor="lightgray",
    command=update_progress
)
scale.pack(pady=15)

# Создание прогресс-бара
progress = ttk.Progressbar(
    root,
    orient=tk.HORIZONTAL,
    length=300,
    mode="determinate",
    maximum=100,
    style="Custom.Horizontal.TProgressbar"
)
progress.pack(pady=15)

# Изменение цвета ползунка после его создания
# configure() позволяет менять свойства уже созданного компонента
scale.configure(troughcolor="lightblue")

root.mainloop()