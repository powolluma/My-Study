namespace Task3
{
    public class Form1
    {
        private int[] values = new int[0];
        private int calls;

        private void ReverseArray(int left, int right)
        {
            calls++;
            if (left >= right) return;
            int temporary = values[left];
            values[left] = values[right];
            values[right] = temporary;
            ReverseArray(left + 1, right - 1);
        }
    }
}
