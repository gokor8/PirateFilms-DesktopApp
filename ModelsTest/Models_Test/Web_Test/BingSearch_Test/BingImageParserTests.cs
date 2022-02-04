using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;
using Films.Models.Web.BingSearch;
using Films.Models.Web.BingSearch.Parsers;
using Films.Models.Web.BingSearch.SettingsSearch;

namespace ModelsTest.Web_Test.BingSearch_Test
{
    [TestClass]
    public class BingImageParserTests
    {
        private Bing bing = new Bing();

        [TestMethod]
        [DataRow("12+ребят,+которые+хотят+умереть+(2019)")]
        [DataRow("Новички(2019)")]
        [DataRow("Дюна")]
        [DataRow("Матрица: Воскрешение")]
        public async Task Finding_film_photo_link(string filmName)
        {
            // Довольно неправильные тесты у парсеров, но я иначе не могу сделать, так как все завязанно на бинге

            var imagesHtml = await bing.GetSearchResultAsync(filmName,
                new ImageBingSettings("&qft=+filterui%3aimagesize-custom_1000_1000&first=1&tsc=ImageBasicHover"));

            var photoLink = await new ImageBingParser().GetWorkingLinksAsync(imagesHtml).FirstAsync();

            Assert.IsNotNull(photoLink);
        }

        [TestMethod]
        [DataRow("12+ребят,+которые+хотят+умереть+(2019)")]
        [DataRow("Новички(2019)")]
        [DataRow("Дюна")]
        [DataRow("Матрица: Воскрешение")]
        public async Task Finding_film_photos_link(string filmName)
        {
            var imagesHtml = await bing.GetSearchResultAsync(filmName,
                new ImageBingSettings("&qft=+filterui%3aimagesize-custom_1000_1000&first=1&tsc=ImageBasicHover"));

            var photosLinks = await new ImageBingParser().GetWorkingLinksAsync(imagesHtml).Take(10).ToListAsync();

            Assert.IsNotNull(photosLinks);
        }
    }
}