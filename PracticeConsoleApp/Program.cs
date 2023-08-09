using System.Text;
using PracticeConsoleApp.Model;

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
            bool isCorrect = true;

            for (int i = 0; i < count; i++)
            {
                if (line[i] is < 'a' or > 'z')
                {
                    isCorrect = false;
                    Console.WriteLine($"Неверный символ '{line[i]}'");
                }
            }

            if (!isCorrect)
            {
                Console.WriteLine("Для ввода допустимы только буквы английского алфавита в нижнем регистре");
                return;
            }

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

            foreach (var item in GetSymbolСount(stringBuilder.ToString()))
            {
                Console.WriteLine($"Символ: '{item.Symbol}' повторяется {item.Count} раз(а)");
            }
        }

        private static List<SymbolСounter> GetSymbolСount(string line)
        {
            List<SymbolСounter> symbolList = new();

            for (int i = 0; i < line.Length; i++)
            {
                bool isRepeated = false;

                for (int j = 0; j < symbolList.Count; j++)
                {
                    if (symbolList[j].Symbol == line[i])
                    {
                        symbolList[j].Count++;
                        isRepeated = true;
                        break;
                    }
                }
                if (!isRepeated)
                {
                    symbolList.Add(new SymbolСounter
                    {
                        Symbol = line[i],
                        Count = 1
                    });
                }
            }
            return symbolList;
        }
    }
}