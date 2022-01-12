using Films.Web.BingSearch;
using Films.Web.BingSearch.BingObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ModelsTest.Web_Test.BingSearch_Test
{
    [TestClass]
    public class Bing_ImageElement_Test
    {
        [TestMethod]
        [DataRow("Дюна")]
        [DataRow("Матрица: Воскрешение")]
        public async Task GetLink_films_string_requestsAsync(string filmName)
        {
            Bing bing = new Bing();

            string imageLink = await bing.GetLink(filmName,
                new ImageElement("&qft=+filterui%3aimagesize-custom_1000_1000&first=1&tsc=ImageBasicHover"));//("&qft=+filterui:imagesize-custom_1000_1000&first=1&tsc=ImageBasicHover"));

            Assert.IsNotNull(imageLink);
        }
    }
}