import random

class Stack:
    def __init__(self, capacity):
        self.data = [None] * capacity
        self.top_index = -1

    def is_empty(self):
        return self.top_index == -1

    def push(self, x):
        if self.top_index == len(self.data) - 1:
            print("Стек переполнен")
            return
        self.top_index += 1
        self.data[self.top_index] = x

    def pop(self):
        if self.is_empty():
            print("Стек пуст")
            return None

        x = self.data[self.top_index]
        self.data[self.top_index] = None
        self.top_index -= 1
        return x

    def top(self):
        if self.is_empty():
            print("Стек пуст")
            return None
        return self.data[self.top_index]


s = Stack(10)

for i in range(10):
    x = random.randint(1, 100)
    print("Число:", x)
    s.push(x)

print("Вершина:", s.top())

while not s.is_empty():
    print("Удалено:", s.pop())