using System.Collections.Generic;
using System.Text;

namespace Task1
{
    public class Form1
    {
        private int CountOnes(int number)
        {
            if (number == 0) return 0;
            return number % 2 + CountOnes(number / 2);
        }

        private void AddSteps(int number, List<string> steps)
        {
            if (number == 0) return;
            steps.Add(number + " : 2 = " + number / 2 + ", остаток " + number % 2);
            AddSteps(number / 2, steps);
        }
    }
}
