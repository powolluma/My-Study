def levenshtein(a, b):
    # классическое расстояние редактирования
    n, m = len(a), len(b)
    dp = [[0] * (m + 1) for _ in range(n + 1)]

    for i in range(n + 1):
        dp[i][0] = i
    for j in range(m + 1):
        dp[0][j] = j

    for i in range(1, n + 1):
        for j in range(1, m + 1):
            if a[i - 1] == b[j - 1]:
                dp[i][j] = dp[i - 1][j - 1]
            else:
                insert = dp[i][j - 1] + 1
                delete = dp[i - 1][j] + 1
                replace = dp[i - 1][j - 1] + 1
                dp[i][j] = min(insert, delete, replace)

    return dp[n][m]


def apply_op(current, op, pos, ch=""):
    chars = list(current)
    if op == "insert":
        chars.insert(pos, ch)
    elif op == "delete":
        chars.pop(pos)
    elif op == "replace":
        chars[pos] = ch
    elif op == "swap":
        chars[pos], chars[pos + 1] = chars[pos + 1], chars[pos]
    return "".join(chars)


def main():
    start = input("начальная строка: ")
    target = input("целевая строка: ")

    min_ops = levenshtein(start, target)
    print("минимум операций:", min_ops)

    current = start
    steps = 0

    while current != target:
        print("текущая строка:", current)
        op = input("операция (insert/delete/replace/swap/stop): ").strip()
        if op == "stop":
            break

        pos = int(input("позиция: "))
        ch = ""
        if op in ("insert", "replace"):
            ch = input("символ: ")

        current = apply_op(current, op, pos, ch)
        steps += 1

    print("итоговая строка:", current)
    print("потрачено операций:", steps, "из минимума", min_ops)


if __name__ == "__main__":
    main()
