def sum_to(n):
    s = 0
    for i in range(1, n + 1):
        s += i
    return s

def main():
    n = int(input())
    print(sum_to(n))

main()