using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PracticeConsoleApp.Infrastructure;
using PracticeConsoleApp.Models;
using PracticeWebApplication.Models;
using PracticeWebApplication.Services;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;



namespace PracticeWebApplication.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LineController : Controller
    {
        private readonly IConfiguration _configuration;
        private ParallelLimit _parallelLimit;
        public LineController(IConfiguration configuration, ParallelLimit parallelLimit)
        {
            _configuration = configuration;
            _parallelLimit = parallelLimit;
        }
        [HttpGet("GetFormattedString")]
        public IActionResult GetFormattedString(string? unformattedString, [Required] TypeSort typeSort)
        {

            if (_parallelLimit.CurrentLimit >= _parallelLimit.MaxLimit)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,new { ErrorMessage = "Превышено максимальное число запросов" });
            }
            _parallelLimit.CurrentLimit++;

            if (unformattedString == null || unformattedString.Length == 0)
            {
                return BadRequest(new { ErrorMessage = "Введена пустая строка"});
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
            FormattedStringBuilder formattedStringBuilder = new();
           
            List<char> invalidCharacters = formattedStringBuilder.GetInvalidCharacters(unformattedString);
            if (invalidCharacters.Count > 0)
            {
                return BadRequest(new
                {
                    ErrorMessage = "Введенны недопустимые символы",
                    InvalidCharacters = invalidCharacters
                });
            }

            string formattedString = formattedStringBuilder.GetFormattedString(unformattedString);
            List<SymbolСounter> symbolСounterList = formattedStringBuilder.GetSymbolСount(formattedString);
            string? longestSubstring = formattedStringBuilder.GetSubstring(formattedString);

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
           
            try
            {
                return Json(new
                {
                    FormattedString = formattedString,
                    SymbolCount = symbolСounterList,
                    LongestSubstring = longestSubstring != null ? longestSubstring : "В строке отсутствуют гласные буквы",
                    TypeSort = typeSort,
                    SortedString = sortedString,
                    RandomNumber = randomNumber,
                    SourceRandomNumber = isRemovedApi ? "Removed API" : ".NET Tools",
                    TruncatedString = truncatedString
                });
            }
            finally
            {
                _parallelLimit.CurrentLimit--;
            }
        }   
    }
}
