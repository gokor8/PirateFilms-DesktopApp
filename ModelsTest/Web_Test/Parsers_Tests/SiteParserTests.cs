using System.Linq;
using System.Threading.Tasks;
using Films.Models.Web.BingSearch;
using Films.Models.Web.BingSearch.BingObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTest.Web_Test.Parsers
{
    [TestClass]
    public class SiteParserTests
    {
        [TestMethod]
        public async Task Get_One_bing_film_contains_site_name()
        {
            string workingSiteLink = string.Empty;

            var linkCollection = await new Bing().GetLinksAsync("lordfilm",
                new SearchElement());

            workingSiteLink = linkCollection.First();

            Assert.IsTrue(workingSiteLink.Contains("lordfilm"));
        }

        [TestMethod] 
        public async Task Get_All_bing_films_contains_site_name()
        {
            bool areAllLinksContainName = true;

            var linkCollection = await new Bing().GetLinksAsync("lordfilm",
                new SearchElement(), true);

            foreach (var link in linkCollection)
                if (!link.Contains("lordfilm"))
                {
                    areAllLinksContainName = false;
                }

            Assert.IsTrue(areAllLinksContainName);
        }

    }
}