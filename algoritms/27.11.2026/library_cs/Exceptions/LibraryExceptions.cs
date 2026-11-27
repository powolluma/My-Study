using System;

namespace LibraryApp.Exceptions
{
    public class LibraryException : Exception
    {
        public LibraryException(string message) : base(message) { }
    }

    public class DuplicateInventoryException : LibraryException
    {
        public DuplicateInventoryException(int invNumber)
            : base($"Издание с инвентарным номером {invNumber} уже существует") { }
    }

    public class AlreadyIssuedException : LibraryException
    {
        public AlreadyIssuedException(int invNumber)
            : base($"Издание {invNumber} уже выдано") { }
    }

    public class NotIssuedException : LibraryException
    {
        public NotIssuedException(int invNumber)
            : base($"Издание {invNumber} не было выдано") { }
    }

    public class PublicationNotFoundException : LibraryException
    {
        public PublicationNotFoundException(string what)
            : base($"Издание не найдено: {what}") { }
    }
}
