class IDE:
    def __init__(self, name, min_ram, needs_ssd):
        self.name = name
        self.min_ram = min_ram
        self.needs_ssd = needs_ssd

    def is_lighter_than(self, other):
        return self.min_ram < other.min_ram

    def __str__(self):
        return f"IDE: {self.name}, RAM: {self.min_ram} ГБ"

pycharm = IDE("PyCharm", 8, True)
vscode = IDE("VS Code", 4, False)

print(pycharm)
print(vscode.is_lighter_than(pycharm))
print(pycharm.is_lighter_than(vscode))
