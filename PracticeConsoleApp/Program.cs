using System.Text;

namespace PracticeConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку");

            string? line = Console.ReadLine();

            if (line == null || line.Length == 0)
            {
                Console.WriteLine("Введена пустая строка");
                return;
            }

            int count = line.Length;
            StringBuilder stringBuilder = new();

            if (count % 2 == 0)
            {
                for (int i = count / 2; i > 0; i--)
                {
                    stringBuilder.Append(line[i - 1]);
                }
                for (int i = count; i > count / 2; i--)
                {
                    stringBuilder.Append(line[i - 1]);
                }
            }
            else
            {
                for (int i = count; i > 0; i--)
                {
                    stringBuilder.Append(line[i - 1]);
                }
                stringBuilder.Append(line);
            }

            Console.WriteLine($"Результат: {stringBuilder}");
        }
    }
}