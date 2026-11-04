import tkinter as tk


class TrafficLight:
    def __init__(self, root):
        self.root = root
        self.root.title("Светофор")
        self.root.geometry("300x400")

        # Создание canvas
        self.canvas = tk.Canvas(
            root,
            width=150,
            height=250,
            bg="black"
        )
        self.canvas.pack(pady=10)

        # Создание трех лампочек
        self.red_light = self.canvas.create_oval(
            40, 10, 110, 80, fill="gray"
        )

        self.yellow_light = self.canvas.create_oval(
            40, 90, 110, 160, fill="gray"
        )

        self.green_light = self.canvas.create_oval(
            40, 170, 110, 240, fill="gray"
        )

        # Создание кнопок
        self.red_button = tk.Button(
            root, text="Красный",
            command=self.turn_red
        )
        self.red_button.pack(pady=5)

        self.yellow_button = tk.Button(
            root, text="Жёлтый",
            command=self.turn_yellow
        )
        self.yellow_button.pack(pady=5)

        self.green_button = tk.Button(
            root, text="Зелёный",
            command=self.turn_green
        )
        self.green_button.pack(pady=5)

        self.turn_red()

    # Включение красного цвета
    def turn_red(self):
        self.canvas.itemconfig(self.red_light, fill="red")
        self.canvas.itemconfig(self.yellow_light, fill="gray")
        self.canvas.itemconfig(self.green_light, fill="gray")

    # Включение желтого цвета
    def turn_yellow(self):
        self.canvas.itemconfig(self.red_light, fill="gray")
        self.canvas.itemconfig(self.yellow_light, fill="yellow")
        self.canvas.itemconfig(self.green_light, fill="gray")

    # Включение зеленого цвета
    def turn_green(self):
        self.canvas.itemconfig(self.red_light, fill="gray")
        self.canvas.itemconfig(self.yellow_light, fill="gray")
        self.canvas.itemconfig(self.green_light, fill="green")
        