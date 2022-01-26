using System.Threading.Tasks;
using Films.Models.Web.SiteSearchers;
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

            await searcher.SearchWorkingSitesAsync();

            Assert.IsNotNull(searcher.WorkingSites);
        }

        [TestMethod]
        public async Task Check_came_first_site()
        {
            var searcher = new LocalSiteSearcher();

            await searcher.SearchWorkingSitesAsync();

            Assert.AreNotEqual(string.Empty, searcher.WorkingSite);
        }
    }
}