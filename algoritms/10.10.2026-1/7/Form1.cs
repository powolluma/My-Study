namespace Task7
{
    public class Form1
    {
        private double SumTo(int current, int end)
        {
            if (current > end) return 0;
            return 1.0 / current + SumTo(current + 1, end);
        }
    }
}
