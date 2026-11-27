namespace LibraryApp.Models
{
    public abstract class Publication
    {
        private int _invNumber;
        private string _title;
        private int _year;
        private bool _available;

        protected Publication(int invNumber, string title, int year)
        {
            _invNumber = invNumber;
            _title = title;
            _year = year;
            _available = true;
        }

        public int InvNumber
        {
            get { return _invNumber; }
        }

        public string Title
        {
            get { return _title; }
        }

        public int Year
        {
            get { return _year; }
        }

        public bool Available
        {
            get { return _available; }
        }

        public void Issue()
        {
            _available = false;
        }

        public void GiveBack()
        {
            _available = true;
        }

        public abstract string GetInfo();

        public virtual string TString()
        {
            string status = _available ? "доступно" : "выдано";
            return $"[{_invNumber}] {_title}, {_year} г. ({status})";
        }

        public override string ToString()
        {
            return TString();
        }
    }
}
