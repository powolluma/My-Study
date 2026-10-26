from operation import IOperation


class Operation(IOperation):
    name = "/"

    def apply(self, a, b):
        if b == 0:
            raise ZeroDivisionError("Деление на ноль!")

        return a / b