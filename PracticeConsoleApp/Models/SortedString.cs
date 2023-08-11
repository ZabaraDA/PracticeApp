using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeConsoleApp.Models
{
    public class SortedString
    {
        private readonly char[] _line;

        public SortedString(string line)
        {
            _line = line.ToArray();
        }
        public SortedString(char[] line)
        {
            _line = line;
        }

        public string GetTreeSortLine()
        {
            return new string (TreeSort().ToArray());
        }
        private List<char> TreeSort()
        {
            Node node = new Node(_line[0]);
            for (int i = 1; i < _line.Length; i++)
            {
                node.Insert(new Node(_line[i]));
            }

            return node.Transform();
        }

        public string GetQuickSortLine()
        {
             return new string (QuickSort(_line.ToArray(), 0, _line.Length - 1));
        }
        private char[] QuickSort(char[] line, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return line;
            }

            int pivot = GetPivot(line, minIndex, maxIndex);

            QuickSort(line, minIndex, pivot - 1);

            QuickSort(line, pivot + 1, maxIndex);

            return line;
        }

        private int GetPivot(char[] line, int minIndex, int maxIndex)
        {
            int pivot = minIndex - 1;

            for (int i = minIndex; i <= maxIndex; i++)
            {
                if (line[i] < line[maxIndex])
                {
                    pivot++;
                    ElementReplacement(ref line[pivot], ref line[i]);
                }
            }
            pivot++;
            ElementReplacement(ref line[pivot], ref line[maxIndex]);
            return pivot;
        }

        private void ElementReplacement(ref char leftItem, ref char rightItem)
        {
            char symbol = leftItem;
            leftItem = rightItem;
            rightItem = symbol;
        }

    }
}
