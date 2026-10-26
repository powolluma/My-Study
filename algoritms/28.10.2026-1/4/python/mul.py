from operation import IOperation


class Operation(IOperation):
    name = "*"

    def apply(self, a, b):
        return a * b