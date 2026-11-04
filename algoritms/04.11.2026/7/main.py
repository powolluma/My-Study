import tkinter as tk


class ShoppingList:
    def __init__(self, root):
        self.root = root
        self.root.title("Список покупок")
        self.root.geometry("400x450")

        # Поле ввода
        self.entry = tk.Entry(root, width=35)
        self.entry.pack(pady=10)

        # Кнопка добавления
        self.add_button = tk.Button(
            root,
            text="Добавить",
            command=self.add_item
        )
        self.add_button.pack(pady=5)

        # Список покупок
        self.listbox = tk.Listbox(root, width=40, height=12)
        self.listbox.pack(pady=10)

        # Кнопка удаления выбранного
        self.delete_button = tk.Button(
            root,
            text="Удалить выбранное",
            command=self.delete_item
        )
        self.delete_button.pack(pady=5)

        # Кнопка очистки списка
        self.clear_button = tk.Button(
            root,
            text="Очистить всё",
            command=self.clear_all
        )
        self.clear_button.pack(pady=5)

    # Добавление товара
    def add_item(self):
        item = self.entry.get().strip()

        if item:
            self.listbox.insert(tk.END, item)
            self.entry.delete(0, tk.END)

    # Удаление выбранного товара
    def delete_item(self):
        selected = self.listbox.curselection()

        if selected:
            self.listbox.delete(selected[0])

    # Очистка всего списка
    def clear_all(self):
        self.listbox.delete(0, tk.END)
        self.entry.delete(0, tk.END)


if __name__ == "__main__":
    root = tk.Tk()
    app = ShoppingList(root)
    root.mainloop()