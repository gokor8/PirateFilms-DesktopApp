using System.Linq;
using System.Threading.Tasks;
using Films.Models.Web.BingSearch;
using Films.Models.Web.BingSearch.BingObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTest.Web_Test.BingSearch_Test
{
    [TestClass]
    public class BingSearchParserTests
    {
        [TestMethod]
        public async Task Finding_working_lordfilm_link()
        {
            var workingLinks = await new Bing().GetLinksAsync("lordfilm", new BingSearchParser());

            Assert.IsNotNull(workingLinks.FirstOrDefault());
        }

        [TestMethod]
        public async Task Finding_all_working_lordfilm_links()
        {
            bool areAllLinksContainName = true;

            var workingLinks = await new Bing().GetLinksAsync("lordfilm",
                new BingSearchParser(), true);

            foreach (var link in workingLinks)
                if (!link.Contains("lordfilm"))
                {
                    areAllLinksContainName = false;
                }

            Assert.IsTrue(areAllLinksContainName);
        }
    }
}