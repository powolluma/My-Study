namespace LibraryApp.Models
{
    public class Book : Publication
    {
        private string _author;
        private int _pages;

        public Book(int invNumber, string title, int year, string author, int pages)
            : base(invNumber, title, year)
        {
            _author = author;
            _pages = pages;
        }

        public string Author
        {
            get { return _author; }
        }

        public int Pages
        {
            get { return _pages; }
        }

        public override string GetInfo()
        {
            return $"Книга: {TString()}, автор: {_author}, страниц: {_pages}";
        }
    }
}
