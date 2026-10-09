namespace Task5
{
    public class Form1
    {
        private int[] values = new int[0];

        private int Search(int index, int value)
        {
            if (index == values.Length) return -1;
            if (values[index] == value) return index;
            return Search(index + 1, value);
        }
    }
}
