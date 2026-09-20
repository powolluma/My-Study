VALID_COMMANDS = ["идти на север", "взять меч", "взять ключ",
                   "открыть дверь", "выйти"]


def decode_command(cmd):
    # зашифрованная команда - реверс строки, метка #
    if cmd.startswith("#"):
        return cmd[1:][::-1]
    return cmd


def main():
    state = {"has_key": False, "has_sword": False, "door_open": False}
    print("подземелье, команды:", ", ".join(VALID_COMMANDS))
    print("зашифрованная команда начинается с #, пример: #ансетйов")

    while True:
        raw = input("команда: ").strip().lower()
        cmd = decode_command(raw)

        if cmd not in VALID_COMMANDS:
            print("неизвестная команда")
            continue

        if cmd == "взять меч":
            state["has_sword"] = True
            print("меч в руках")

        elif cmd == "взять ключ":
            state["has_key"] = True
            print("ключ в руках")

        elif cmd == "открыть дверь":
            if state["has_key"]:
                state["door_open"] = True
                print("дверь открыта")
            else:
                print("нужен ключ")

        elif cmd == "идти на север":
            print("шаг на север")

        elif cmd == "выйти":
            if state["door_open"]:
                print("подземелье пройдено, победа")
                break
            else:
                print("дверь ещё заперта")


if __name__ == "__main__":
    main()
