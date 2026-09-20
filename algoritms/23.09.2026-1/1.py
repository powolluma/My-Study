# шифровальная машина

RU_LOW = "абвгдежзийклмнопрстуфхцчшщъыьэюя"
RU_UP = RU_LOW.upper()
EN_LOW = "abcdefghijklmnopqrstuvwxyz"
EN_UP = EN_LOW.upper()

ALPHABETS = [RU_LOW, RU_UP, EN_LOW, EN_UP]


def shift_char(ch, k):
    # сдвиг одного символа по своему алфавиту
    for alpha in ALPHABETS:
        idx = alpha.find(ch)
        if idx != -1:
            new_idx = (idx + k) % len(alpha)
            return alpha[new_idx]
    return ch  # не буква - без изменений


def shift_text(text, k):
    # сдвиг всей строки
    return "".join(shift_char(c, k) for c in text)


def encrypt(text, key, k):
    # шаг 1: сдвиг на k
    step1 = shift_text(text, k)
    # шаг 2: разворот
    step2 = step1[::-1]
    # шаг 3: каждый второй символ на #
    chars = list(step2)
    for i in range(1, len(chars), 2):
        chars[i] = "#"
    step3 = "".join(chars)
    # шаг 4: ключ в начало
    result = key + step3
    return result


def decrypt(enc, key, k):
    # удаление ключа - только первое вхождение
    pos = enc.find(key)
    if pos != -1:
        enc = enc[:pos] + enc[pos + len(key):]

    # восстановление # невозможно - пометка ?
    chars = list(enc)
    for i in range(1, len(chars), 2):
        if chars[i] == "#":
            chars[i] = "?"
    restored = "".join(chars)

    # разворот обратно
    reversed_back = restored[::-1]

    # сдвиг на -k
    result = shift_text(reversed_back, -k)
    return result


if __name__ == "__main__":
    print("1 - зашифровать, 2 - расшифровать")
    mode = input("режим: ").strip()

    if mode == "1":
        text = input("сообщение: ")
        key = input("ключ: ")
        k = int(input("сдвиг K: "))
        print("зашифровано:", encrypt(text, key, k))
    else:
        enc = input("зашифрованная строка: ")
        key = input("ключ: ")
        k = int(input("сдвиг K: "))
        print("расшифровано:", decrypt(enc, key, k))
