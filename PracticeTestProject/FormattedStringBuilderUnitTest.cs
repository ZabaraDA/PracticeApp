using PracticeConsoleApp.Models;
using PracticeWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTestProject
{
    [TestFixture]
    public class FormattedStringBuilderUnitTests
    {
        private FormattedStringBuilder _formattedStringBuilder;

        private string _oneUnformattedString;
        private string _twoUnformattedString;

        private string _invalidString;
        private string _validString;

        private string _formattedString;
        private string _noVowelsString;

        private List<SymbolСounter> _symbolCountList;

        [SetUp]
        public void Setup()
        {
            _formattedStringBuilder = new();

            _oneUnformattedString = "abcdef";
            _twoUnformattedString = "abcde";

            _invalidString = "aбbдc3";
            _validString = "abc";

            _formattedString = "abcadf";
            _noVowelsString = "bdfrth";

            _symbolCountList = new List<SymbolСounter>()
            {
                new SymbolСounter { Symbol = 'a', Count = 2 },
                new SymbolСounter { Symbol = 'b', Count = 1 },
                new SymbolСounter { Symbol = 'c', Count = 1 },
                new SymbolСounter { Symbol = 'd', Count = 1 },
                new SymbolСounter { Symbol = 'f', Count = 1 }
            };
        }

        [Test]
        public void GetVowelToVowelSubstringTest()
        {
            string? rezult = _formattedStringBuilder.GetSubstring(_formattedString);
            Assert.That(rezult, Is.EqualTo("abca"));
        }

        [Test]
        public void GetNullableVowelToVowelSubstringTest()
        {
            string? rezult = _formattedStringBuilder.GetSubstring(_noVowelsString);
            Assert.That(rezult, Is.EqualTo(null));
        }

        [Test]
        public void GetSymbolCountFormattedStringTest()
        {
            List<SymbolСounter> rezult = _formattedStringBuilder.GetSymbolСount(_formattedString);
            for (int i = 0; i < rezult.Count; i++)
            {
                Assert.That(rezult[i].Symbol, Is.EqualTo(_symbolCountList[i].Symbol));
                Assert.That(rezult[i].Count, Is.EqualTo(_symbolCountList[i].Count));
            }
        }

        [Test]
        public void GetInvalidCharactersInvalidStringTest()
        {
            List<char> rezult = _formattedStringBuilder.GetInvalidCharacters(_invalidString);
            Assert.That(rezult, Is.EqualTo(new List<char>() { 'б', 'д', '3' }));
        }
        [Test]
        public void GetInvalidCharactersValidStringTest()
        {
            List<char> rezult = _formattedStringBuilder.GetInvalidCharacters(_validString);
            Assert.That(rezult.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetFormattedEvenStringTest()
        {
            string rezult = _formattedStringBuilder.GetFormattedString(_oneUnformattedString);
            Assert.That(rezult, Is.EqualTo("cbafed"));
        }

        [Test]
        public void GetFormattedOddStringTest()
        {
            string rezult = _formattedStringBuilder.GetFormattedString(_twoUnformattedString);
            Assert.That(rezult, Is.EqualTo("edcbaabcde"));
        }

    }
}
