import json
from pathlib import Path
from figures import Rectangle, Circle


class FigureFactory:
    def __init__(self):
        config_path = Path(__file__).parent / "config.json"
        with config_path.open("r", encoding="utf-8") as file:
            self.config = json.load(file)

    def create(self, name):
        name = name.lower()

        if name == "rectangle":
            return Rectangle(
                self.config["rectangle_width"],
                self.config["rectangle_height"]
            )
        if name == "circle":
            return Circle(self.config["circle_radius"])

        raise ValueError("Неизвестная фигура: " + name)
