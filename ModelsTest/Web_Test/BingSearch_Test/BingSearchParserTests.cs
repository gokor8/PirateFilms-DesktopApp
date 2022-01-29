using System.Linq;
using System.Threading.Tasks;
using Films.Models.Web.BingSearch;
using Films.Models.Web.BingSearch.Parsers;
using Films.Models.Web.BingSearch.SettingsSearch;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTest.Web_Test.BingSearch_Test
{
    [TestClass]
    public class BingSearchParserTests
    {
        [TestMethod]
        public async Task Finding_working_lordfilm_link()
        {
            var sitesHtml = await new Bing().GetSearchResultAsync("lordfilm", new SearchBingSettings());

            string siteLink = await new SearchBingParser().GetWorkingLinksAsync(sitesHtml).FirstAsync();

            Assert.AreNotEqual(string.Empty, siteLink);
        }

        [TestMethod]
        public async Task Finding_all_working_lordfilm_links()
        {
            bool areAllLinksContainName = true;

            var sitesHtml = await new Bing().GetSearchResultAsync("lordfilm", new SearchBingSettings());

            var sitesLinks = await new SearchBingParser().GetWorkingLinksAsync(sitesHtml).ToListAsync();

            foreach (var link in sitesLinks)
                if (!link.Contains("lordfilm"))
                {
                    areAllLinksContainName = false;
                }

            Assert.IsTrue(areAllLinksContainName);
        }
    }
}