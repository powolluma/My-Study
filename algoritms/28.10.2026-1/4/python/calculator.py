class Calculator:
    def __init__(self, operations):
        self.operations = {
            op.name: op for op in operations
        }

    def calculate(self, name, a, b):
        if name not in self.operations:
            raise ValueError("Неизвестная операция")

        return self.operations[name].apply(a, b)