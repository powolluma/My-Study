namespace Task6
{
    public class Form1
    {
        private bool IsPalindrome(string value, int left, int right)
        {
            if (left >= right) return true;
            if (value[left] != value[right]) return false;
            return IsPalindrome(value, left + 1, right - 1);
        }
    }
}
