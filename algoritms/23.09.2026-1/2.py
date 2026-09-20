# текстовый квест: комната с записками

import string

SECRET_PHRASE = "выход"  # кодовая фраза для победы
FAKE_KEYWORD = "ложь"  # ключевое слово ложных записок


def clean_note(note):
    # без знаков препинания
    result = ""
    for ch in note:
        if ch not in string.punctuation:
            result += ch
    return result


def first_letter(note):
    words = note.split()
    if not words:
        return ""
    first_word = words[0]
    return first_word[0].lower()


def main():
    code_phrase = ""
    print("вводите записки, 'end' для остановки")

    while True:
        note = input("записка: ")
        if note.strip().lower() == "end":
            break

        # ложная записка - пропуск
        if FAKE_KEYWORD in note.lower():
            print("ложная записка, пропуск")
            continue

        cleaned = clean_note(note)
        letter = first_letter(cleaned)
        code_phrase += letter
        print("кодовая фраза сейчас:", code_phrase)

    print("итоговая фраза:", code_phrase)
    if code_phrase == SECRET_PHRASE:
        print("дверь открыта, победа")
    else:
        print("фраза не совпала, дверь заперта")


if __name__ == "__main__":
    main()
