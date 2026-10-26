from abc import ABC, abstractmethod


class IOperation(ABC):
    name = ""

    @abstractmethod
    def apply(self, a, b):
        pass