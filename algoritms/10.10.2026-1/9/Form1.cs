namespace Task9
{
    public class Form1
    {
        private string vowels = "аеёиоуыэюяАЕЁИОУЫЭЮЯaeiouAEIOU";

        private int CountVowels(string text, int index, bool russianOnly)
        {
            if (index == text.Length) return 0;
            string allowed = russianOnly ? "аеёиоуыэюяАЕЁИОУЫЭЮЯ" : vowels;
            bool isVowel = allowed.IndexOf(text[index]) >= 0;
            return (isVowel ? 1 : 0) + CountVowels(text, index + 1, russianOnly);
        }
    }
}
