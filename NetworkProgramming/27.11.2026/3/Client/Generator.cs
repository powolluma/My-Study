using System;

namespace Client
{
    public static class PhraseGenerator
    {
        private static string[] phrases =
        {
            "Привет!",
            "Как дела?",
            "Интересно.",
            "Я тебя понял.",
            "Хорошо!",
            "Расскажи подробнее.",
            "Согласен.",
            "Понятно, продолжай."
        };

        public static string GetRandom()
        {
            Random random = Random.Shared;

            int index = random.Next(phrases.Length);

            return phrases[index];
        }
    }
}
