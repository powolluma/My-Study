using System;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    static void Main()
    {
        bool playAgain = true;

        while (playAgain)
        {
            int min = 0;
            int max = 100;
            bool guessed = false;

            while (min <= max)
            {
                int number = (min + max) / 2;

                int answer = MessageBox(
                    IntPtr.Zero,
                    $"Вы загадали число {number}?",
                    "Компьютер угадывает",
                    4
                );

                if (answer == 6)
                {
                    MessageBox(IntPtr.Zero, $"Я угадал! Ваше число: {number}", "Победа", 0);
                    guessed = true;
                    break;
                }

                int hint = MessageBox(
                    IntPtr.Zero,
                    $"Ваше число больше {number}?",
                    "Подсказка",
                    4
                );

                if (hint == 6)
                {
                    min = number + 1;
                }
                else
                {
                    max = number - 1;
                }
            }

            if (!guessed)
            {
                MessageBox(
                    IntPtr.Zero,
                    "Вы дали неправильные подсказки. Попробуйте еще раз!",
                    "Ошибка",
                    0
                );
            }

            int again = MessageBox(
                IntPtr.Zero,
                "Хотите сыграть еще раз?",
                "Повторная игра",
                4
            );

            if (again != 6)
            {
                playAgain = false;
            }
        }
    }
}