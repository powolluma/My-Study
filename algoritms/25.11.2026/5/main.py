import tkinter as tk
from tkinter import messagebox


class Book:
    def __init__(self, author, title, year):
        self.author = author
        self.title = title
        self.year = year


class Library:
    def __init__(self):
        self.books = []

    def add(self, book):
        self.books.append(book)

    def find_by_author(self, author):
        result = []

        for book in self.books:
            if author.lower() in book.author.lower():
                result.append(book)

        return result

    def remove(self, book):
        if book in self.books:
            self.books.remove(book)

    def all_books(self):
        return self.books


library = Library()

window = tk.Tk()
window.title("Библиотека")
window.geometry("500x500")

tk.Label(window, text="Автор:").pack()
author_entry = tk.Entry(window, width=40)
author_entry.pack()

tk.Label(window, text="Название:").pack()
title_entry = tk.Entry(window, width=40)
title_entry.pack()

tk.Label(window, text="Год:").pack()
year_entry = tk.Entry(window, width=40)
year_entry.pack()

listbox = tk.Listbox(window, width=60, height=12)
listbox.pack(pady=10)

shown_books = []


def refresh_list(books=None):
    listbox.delete(0, tk.END)

    if books is None:
        books = library.all_books()

    shown_books.clear()
    shown_books.extend(books)

    for book in shown_books:
        listbox.insert(
            tk.END,
            f"{book.author} — {book.title} ({book.year})"
        )


def add_book():
    author = author_entry.get()
    title = title_entry.get()

    try:
        year = int(year_entry.get())
    except ValueError:
        messagebox.showinfo("Ошибка", "Введите правильный год")
        return

    if author == "" or title == "":
        messagebox.showinfo("Ошибка", "Заполните все поля")
        return

    book = Book(author, title, year)
    library.add(book)

    author_entry.delete(0, tk.END)
    title_entry.delete(0, tk.END)
    year_entry.delete(0, tk.END)

    refresh_list()


def find_book():
    author = search_entry.get()

    if author == "":
        refresh_list()
    else:
        result = library.find_by_author(author)
        refresh_list(result)


def remove_book():
    selection = listbox.curselection()

    if not selection:
        messagebox.showinfo("Ошибка", "Выберите книгу")
        return

    index = selection[0]
    book = shown_books[index]

    library.remove(book)
    refresh_list()


tk.Button(
    window,
    text="Добавить книгу",
    command=add_book
).pack(pady=3)

tk.Label(window, text="Поиск по автору:").pack()
search_entry = tk.Entry(window, width=40)
search_entry.pack()

tk.Button(
    window,
    text="Найти",
    command=find_book
).pack(pady=3)

tk.Button(
    window,
    text="Удалить выбранную книгу",
    command=remove_book
).pack(pady=3)

window.mainloop()
