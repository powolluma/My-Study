from school.models import Teacher


def run() -> None:
    t = Teacher("Сидорова А.П.", "Информатика")
    print(t)


if __name__ == "__main__":
    run()