namespace Task8
{
    public class Form1
    {
        private string ToBase(int number, int numberBase)
        {
            if (number < numberBase) return "0123456789ABCDEF"[number].ToString();
            return ToBase(number / numberBase, numberBase) + "0123456789ABCDEF"[number % numberBase];
        }
    }
}
