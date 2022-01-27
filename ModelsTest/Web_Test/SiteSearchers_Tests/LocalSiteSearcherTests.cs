using System.Linq;
using System.Threading.Tasks;
using Films.Models.Web.BingSearch.SiteSearchers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTest.Web_Test.SiteSearchers_Tests
{
    [TestClass]
    public class LocalSiteSearcherTests
    {
        [TestMethod]
        public async Task Check_added_in_db_searched_sites()
        {
            var searcher = new LocalSiteSearcher();

            var workingLinks = await searcher.SearchWorkingSitesAsync().ToListAsync();

            Assert.IsNotNull(workingLinks);
        }

        [TestMethod]
        public async Task Check_came_first_site()
        {
            var searcher = new LocalSiteSearcher();

            string firstLink = await searcher.SearchWorkingSitesAsync().FirstAsync();

            Assert.AreNotEqual(string.Empty, firstLink);
        }
    }
}