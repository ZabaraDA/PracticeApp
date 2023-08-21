using PracticeConsoleApp.Models;

namespace PracticeTestProject
{
    public class SortedStringTests
    {
        private SortedString _sortedString;
        [SetUp]
        public void Setup()
        {
            _sortedString = new("bacd");
        }

        [Test]
        public void GetQuickSortLineSortedStringTest()
        {
            string rezult = _sortedString.GetQuickSortLine();
            Assert.That(rezult, Is.EqualTo("abcd"));
        }

        [Test]
        public void GetTreeSortLineSortedStringTest()
        {
            string rezult = _sortedString.GetTreeSortLine();
            Assert.That(rezult, Is.EqualTo("abcd"));
        }
    }
}