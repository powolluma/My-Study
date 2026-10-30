class IDE:
    def __init__(self, name, min_ram, needs_ssd):
        self.name = name
        self.min_ram = min_ram
        self.needs_ssd = needs_ssd

    def __str__(self):
        return f"IDE: {self.name}, RAM: {self.min_ram} ГБ"

class LightIDE(IDE):
    def __init__(self, name, min_ram):
        super().__init__(name, min_ram, False)

    def __str__(self):
        return f"{super().__str__()} (легкая)"

class HeavyIDE(IDE):
    def __init__(self, name, min_ram):
        super().__init__(name, min_ram, True)

    def __str__(self):
        return f"{super().__str__()} (тяжелая)"

light_ide = LightIDE("VSCode", 4)
heavy_ide = HeavyIDE("PyCharm", 8)

print(light_ide)
print(heavy_ide)
