using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PracticeConsoleApp.Infrastructure;
using PracticeConsoleApp.Models;
using PracticeWebApplication.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Text;

namespace PracticeWebApplication.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LineController : Controller
    {
        private readonly IConfiguration _configuration;
        public LineController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetFormattedString")]
        public IActionResult GetFormattedString(string? unformattedString, [Required] TypeSort typeSort)
        {
            if (unformattedString == null || unformattedString.Length == 0)
            {
                return BadRequest(new { ErrorMessage = "Введена пустая строка" });
            }

            int count = unformattedString.Length;
            bool isCorrect = true;
            List<char> invalidCharacters = new List<char>();

            for (int i = 0; i < count; i++)
            {
                if (unformattedString[i] is < 'a' or > 'z')
                {
                    isCorrect = false;
                    invalidCharacters.Add((unformattedString[i]));
                }
            }
            if (!isCorrect)
            {
                return BadRequest(new
                {
                    ErrorMessage = "Введенны нодопустимые символы",
                    InvalidCharacters = invalidCharacters
                });
            }

            string[] blackList = _configuration.GetSection("Settings").GetSection("BlackList").Get<string[]>();
            foreach (var invalidWord in blackList)
            {
                if (invalidWord == unformattedString)
                {
                    return BadRequest(new
                    {
                        ErrorMessage = "Введена недопустимая строка",
                        BlackListString = unformattedString
                    });
                }
            }

            StringBuilder stringBuilder = new();

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
            string formattedString = stringBuilder.ToString();

            SortedString unsortedString = new SortedString(formattedString);
            string sortedString;

            if (typeSort == TypeSort.QuickSort)
            {
                sortedString = unsortedString.GetQuickSortLine();
            }
            else
            {
                sortedString = unsortedString.GetTreeSortLine();
            }

            int randomNumber;
            bool isRemovedApi;

            try
            {
                randomNumber = RemoteApiParser.GetRezult($"{_configuration.GetValue<string>("RandomApi")}?num=1&min=1&max={formattedString.Length}&col=1&base=10&format=html&rnd=new", "pre", "data");
                isRemovedApi = true;
            }
            catch
            {
                Random random = new();
                randomNumber = random.Next(1, formattedString.Length + 1);
                isRemovedApi = false;
            }

            string truncatedString = formattedString.Remove(randomNumber - 1, 1);

            return Json(new
            {
                FormattedString = formattedString,
                SymbolCount = GetSymbolСount(formattedString),
                LongestSubstring = GetSubstring(formattedString),
                TypeSort = typeSort,
                SortedString = sortedString,
                RandomNumber = randomNumber,
                SourceRandomNumber = isRemovedApi ? "Removed API" : ".NET Tools",
                TruncatedString = truncatedString
            });
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
