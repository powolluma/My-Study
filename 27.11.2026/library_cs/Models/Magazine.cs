namespace LibraryApp.Models
{
    public class Magazine : Publication
    {
        private int _issueNumber;
        private string _publisher;

        public Magazine(int invNumber, string title, int year, int issueNumber, string publisher)
            : base(invNumber, title, year)
        {
            _issueNumber = issueNumber;
            _publisher = publisher;
        }

        public int IssueNumber
        {
            get { return _issueNumber; }
        }

        public string Publisher
        {
            get { return _publisher; }
        }

        public override string GetInfo()
        {
            return $"Журнал: {TString()}, выпуск №{_issueNumber}, издательство: {_publisher}";
        }
    }
}
