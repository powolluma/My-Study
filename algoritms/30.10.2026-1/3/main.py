import tkinter as tk


root = tk.Tk()
root.title("Растягивающееся текстовое поле")
root.geometry("400x300")

button_clear = tk.Button(root, text="Очистить", command=lambda: text_area.delete("1.0", tk.END))
button_clear.pack(side="bottom", pady=8)

# fill и expand позволяют текстовому полю занять свободное место при изменении окна.
text_area = tk.Text(root)
text_area.pack(fill="both", expand=True, padx=8, pady=8)

root.mainloop()
