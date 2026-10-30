import random

class IDE:
    def __init__(self, name, min_ram, needs_ssd):
        self.name = name
        self.min_ram = min_ram
        self.needs_ssd = needs_ssd

    def can_run(self, ram, ssd):
        return ram >= self.min_ram and (not self.needs_ssd or ssd)

def show_result(ide, ram, ssd):
    if ide.can_run(ram, ssd):
        print("Эта IDE запустится")
    else:
        requirements = []
        if ram < ide.min_ram:
            requirements.append(f"минимум {ide.min_ram} ГБ RAM")
        if ide.needs_ssd and not ssd:
            requirements.append("SSD")
        print("Не потянет! Нужно " + " и ".join(requirements))

def main():
    ides = [
        IDE("VS Code", 4, False),
        IDE("PyCharm", 8, True),
        IDE("Sublime", 2, False),
    ]
    secret_ide = random.choice(ides)

    print("Угадай IDE за 3 попытки")
    print("Варианты:", ", ".join(ide.name for ide in ides))

    ram = int(input("Сколько ГБ RAM у компьютера? "))
    ssd_answer = input("У компьютера SSD? (да/нет): ").strip().lower()
    ssd = ssd_answer in ("да", "д")

    for attempt in range(1, 4):
        guess = input(f"Попытка {attempt}: какую IDE я загадал? ").strip().lower()
        guessed_ide = None
        for ide in ides:
            if guess == ide.name.lower():
                guessed_ide = ide
                break

        if guessed_ide is None:
            print("Такой IDE нет в списке")
        elif guessed_ide == secret_ide:
            show_result(guessed_ide, ram, ssd)
            print(f"Это {secret_ide.name}! Ты угадал с {attempt} попытки")
            return
        else:
            show_result(guessed_ide, ram, ssd)
            print("Не угадал")

    print(f"Я загадал IDE {secret_ide.name}.")

if __name__ == "__main__":
    main()
