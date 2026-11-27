namespace LibraryApp.Models
{
    public class Newspaper : Publication
    {
        private string _issueDate;
        private string _periodicity;

        public Newspaper(int invNumber, string title, int year, string issueDate, string periodicity)
            : base(invNumber, title, year)
        {
            _issueDate = issueDate;
            _periodicity = periodicity;
        }

        public string IssueDate
        {
            get { return _issueDate; }
        }

        public string Periodicity
        {
            get { return _periodicity; }
        }

        public override string GetInfo()
        {
            return $"Газета: {TString()}, дата выпуска: {_issueDate}, периодичность: {_periodicity}";
        }
    }
}
