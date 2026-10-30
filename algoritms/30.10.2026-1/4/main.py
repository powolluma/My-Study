import tkinter as tk


root = tk.Tk()
root.title("Абсолютное позиционирование")
root.geometry("300x180")
root.resizable(True, True)

# place задаёт координаты относительно верхнего левого угла окна.
# При изменении размера окна координаты остаются прежними, поэтому кнопки не смещаются.
# В задаче 3 pack с expand=True растягивает текстовое поле вместе с окном.
tk.Button(root, text="Кнопка 1").place(x=20, y=20)
tk.Button(root, text="Кнопка 2").place(x=20, y=60)
tk.Button(root, text="Кнопка 3").place(x=20, y=100)

root.mainloop()
