using System;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;
using PracticeConsoleApp.Infrastructure;
using PracticeConsoleApp.Models;

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
            Console.WriteLine($"Самая длинная подстрока начинающаяся и заканчивающаяся на гласную: {GetSubstring(stringBuilder.ToString())}");


            SortedString sortedString = new SortedString(stringBuilder.ToString());
            Console.WriteLine("Выберите тип сортировки\nВведите Q для быстрой сортировки\nВведите T для сортировки деревом");

            string? option = Console.ReadLine();
            if (option == "Q" || option == "q")
            {
                Console.WriteLine($"Отсортированная методом быстрой сортировки обработанная строка: {sortedString.GetQuickSortLine()}");
            }
            else if (option == "T" || option == "t")
            {
                Console.WriteLine($"Отсортированная методом сортировки деревом обработанная строка: {sortedString.GetTreeSortLine()}");
            }
            else
            {
                Console.WriteLine("Введено неверное значение. Сортировка не выполнена");
            }

            int randomNumber;
            try
            {
                randomNumber = RemoteApiParser.GetRezult($"https://www.random.org/integers/?num=1&min=1&max={stringBuilder.Length}&col=1&base=10&format=html&rnd=new", "pre", "data");
                Console.WriteLine($"Случайное число полученное с удалённого API: {randomNumber}");
            }
            catch
            {
                Random random = new Random();
                randomNumber = random.Next(1, stringBuilder.Length+1);
                Console.WriteLine($"Не удалось получить случайное число с удалённого API\nСлучайное число сгенерированное платформой: {randomNumber}");
            }


            Console.WriteLine($"Обработанная строка без одного символа: {stringBuilder.ToString().Remove(randomNumber - 1,1)}");


        }

        private static string GetSubstring(string line)
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y' };
            bool isVowel = false;
            int start = 0, end = 0;

            for (int i = 0; i < line.Length; i++)
            {
                foreach (var vowel in vowels)
                {
                    if (vowel == line[i])
                    {
                        isVowel = true;
                        start = i;
                        break;
                    }
                }
                if (isVowel)
                {
                    break;
                }
            }
            if (isVowel == false)
            {
                return "В строке отсутствуют гласные буквы";
            }

            isVowel = false;
            for (int i = line.Length - 1; i > 0; i--)
            {
                foreach (var vowel in vowels)
                {
                    if (vowel == line[i])
                    {
                        isVowel = true;
                        end = i;
                        break;
                    }
                }
                if (isVowel)
                {
                    break;
                }
            }

            StringBuilder substringBuilder = new();

            for (int i = start; i <= end; i++)
            {
                substringBuilder.Append(line[i]);
            }

            return substringBuilder.ToString();
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