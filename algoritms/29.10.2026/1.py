class Computer:
    def __init__(self, ram, disk_type, cores):
        self.ram = ram
        self.disk_type = disk_type.upper()
        self.cores = cores

    def can_run(self, ide):
        if self.ram < ide.get("ram", 0):
            return False
        if ide.get("ssd", False) and self.disk_type != "SSD":
            return False
        if self.cores < ide.get("cores", 0):
            return False
        return True

pycharm = {"name": "PyCharm", "ram": 8, "ssd": True, "cores": 2}
vscode = {"name": "VS Code", "ram": 4, "ssd": False, "cores": 2}
computer = Computer(ram=4, disk_type="HDD", cores=2)

print(computer.can_run(pycharm))
print(computer.can_run(vscode))
