class LibraryError(Exception):
    pass


class DuplicateInventoryError(LibraryError):
    def __init__(self, inv_number):
        super().__init__(f"Издание с инвентарным номером {inv_number} уже существует")


class AlreadyIssuedError(LibraryError):
    def __init__(self, inv_number):
        super().__init__(f"Издание {inv_number} уже выдано")


class NotIssuedError(LibraryError):
    def __init__(self, inv_number):
        super().__init__(f"Издание {inv_number} не было выдано")


class PublicationNotFoundError(LibraryError):
    def __init__(self, what):
        super().__init__(f"Издание не найдено: {what}")
