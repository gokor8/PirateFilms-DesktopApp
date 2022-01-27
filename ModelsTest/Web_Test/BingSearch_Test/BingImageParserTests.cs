using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Films.Models.Web.BingSearch.BingObjects;
using System.Linq;
using Films.Models.Web.BingSearch;

namespace ModelsTest.Web_Test.BingSearch_Test
{
    [TestClass]
    public class BingImageParserTests
    {
        [TestMethod]
        [DataRow("12+ребят,+которые+хотят+умереть+(2019)")]
        [DataRow("Новички(2019)")]
        [DataRow("Дюна")]
        [DataRow("Матрица: Воскрешение")]
        public async Task Finding_film_photo_link(string filmName)
        {
            var imagesLink = await new Bing().GetLinksAsync(filmName,
                new BingImageParser("&qft=+filterui%3aimagesize-custom_1000_1000&first=1&tsc=ImageBasicHover"));

            string imageLink = imagesLink.FirstOrDefault();

            Assert.IsNotNull(imageLink);
        }
    }
}