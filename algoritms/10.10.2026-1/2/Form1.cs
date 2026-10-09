using System.Collections.Generic;

namespace Task2
{
    public class Form1
    {
        private void FindParts(int remaining, int count, int minimum, List<int> parts, List<string> results)
        {
            if (count == 0)
            {
                if (remaining == 0) results.Add(string.Join(" + ", parts));
                return;
            }
            for (int value = minimum; value <= remaining / count; value++)
            {
                parts.Add(value);
                FindParts(remaining - value, count - 1, value, parts, results);
                parts.RemoveAt(parts.Count - 1);
            }
        }
    }
}
