def count_words(text):
    return len(text.split())


def reverse(text):
    return text[::-1]


def find_substring(text, sub):
    # возвращает список всех позиций вхождения
    positions = []
    start = text.find(sub)
    while sub and start != -1:
        positions.append(start)
        start = text.find(sub, start + 1)
    return positions


def replace_substring(text, old, new):
    return text.replace(old, new)


def to_upper(text):
    return text.upper()


def to_lower(text):
    return text.lower()