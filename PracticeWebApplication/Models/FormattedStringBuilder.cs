using PracticeConsoleApp.Models;
using System.Text;

namespace PracticeWebApplication.Models
{
    public class FormattedStringBuilder
    {
        public string? GetSubstring(string formattedString)
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y' };
            bool isVowel = false;
            int start = 0, end = 0;

            for (int i = 0; i < formattedString.Length; i++)
            {
                foreach (var vowel in vowels)
                {
                    if (vowel == formattedString[i])
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
                return null;
            }

            isVowel = false;
            for (int i = formattedString.Length - 1; i > 0; i--)
            {
                foreach (var vowel in vowels)
                {
                    if (vowel == formattedString[i])
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
                substringBuilder.Append(formattedString[i]);
            }
            return substringBuilder.ToString();
        }

        public List<SymbolСounter> GetSymbolСount(string formattedString)
        {
            List<SymbolСounter> symbolList = new();

            for (int i = 0; i < formattedString.Length; i++)
            {
                bool isRepeated = false;

                for (int j = 0; j < symbolList.Count; j++)
                {
                    if (symbolList[j].Symbol == formattedString[i])
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
                        Symbol = formattedString[i],
                        Count = 1
                    });
                }
            }
            return symbolList;
        }
        public List<char> GetInvalidCharacters(string unformattedString) 
        {
            List<char> invalidCharacters = new List<char>();

            for (int i = 0; i < unformattedString.Length; i++)
            {
                if (unformattedString[i] is < 'a' or > 'z')
                {
                    invalidCharacters.Add((unformattedString[i]));
                }
            }
            return invalidCharacters;
        }
        public string GetFormattedString(string unformattedString)
        {
            StringBuilder stringBuilder = new();
            int count = unformattedString.Length;

            if (count % 2 == 0)
            {
                for (int i = count / 2; i > 0; i--)
                {
                    stringBuilder.Append(unformattedString[i - 1]);
                }
                for (int i = count; i > count / 2; i--)
                {
                    stringBuilder.Append(unformattedString[i - 1]);
                }
            }
            else
            {
                for (int i = count; i > 0; i--)
                {
                    stringBuilder.Append(unformattedString[i - 1]);
                }
                stringBuilder.Append(unformattedString);
            }
            return stringBuilder.ToString();
        }
    }
}
