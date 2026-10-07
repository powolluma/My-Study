def row_average(row):
    return sum(row) / len(row)

def student_averages(grades):
    result = []
    for row in grades:
        result.append(row_average(row))
    return result

def subject_averages(grades):
    result = []
    for j in range(len(grades[0])):
        s = 0
        for i in range(len(grades)):
            s += grades[i][j]
        result.append(s / len(grades))
    return result

def best_student(averages):
    best = 0
    for i in range(len(averages)):
        if averages[i] > averages[best]:
            best = i
    return best

def count_excellent(averages):
    c = 0
    for a in averages:
        if a >= 4.5:
            c += 1
    return c

def count_failing(grades):
    c = 0
    for row in grades:
        if 2 in row:
            c += 1
    return c

def read_grades(m, k):
    grades = []
    for i in range(m):
        grades.append(list(map(int, input(f"Оценки студента {i + 1}: ").split())))
    return grades

def print_list(title, values):
    print(title)
    for i in range(len(values)):
        print(i + 1, round(values[i], 2))

def main():
    m = int(input("Студентов: "))
    k = int(input("Предметов: "))
    grades = read_grades(m, k)
    st = student_averages(grades)
    sub = subject_averages(grades)
    print_list("Средний балл студентов:", st)
    print_list("Средний балл по предметам:", sub)
    print("Лучший студент:", best_student(st) + 1)
    print("Отличников:", count_excellent(st))
    print("Двоечников:", count_failing(grades))

main()