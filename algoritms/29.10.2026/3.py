class IDE:
    def __init__(self, name, min_ram, needs_ssd):
        self.name = name
        self.min_ram = min_ram
        self.needs_ssd = needs_ssd


def find_suitable(computer_ram, ides):
    suitable = []
    for ide in ides:
        if ide.min_ram <= computer_ram:
            suitable.append(ide.name)
    return suitable


ides = [
    IDE("VS Code", min_ram=4, needs_ssd=False),
    IDE("PyCharm", min_ram=8, needs_ssd=True),
    IDE("Sublime", min_ram=2, needs_ssd=False),
]

print(find_suitable(4, ides))
