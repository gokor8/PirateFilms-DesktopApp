using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Films.Models.DataBaseLogic.LinksDataBase;
using Films.Models.Web.SiteSearchers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTest.Web_Test.SiteSearchers_Tests
{
    [TestClass]
    public class BingSiteSearcherTests
    {
        [TestMethod]
        public async Task Check_added_in_db_searched_sites()
        {
            var searcher = new BingSiteSearcher();

            await searcher.SearchWorkingSitesAsync();

            List<string> workingLinkList = new List<string>();

            using (var context = new SitesContext())
                await context.Links.ForEachAsync(l=> workingLinkList.Add(l.WorkingLink));

            Assert.AreEqual(searcher.WorkingSites.Count(), workingLinkList.Count);
        }

        [TestMethod]
        public async Task Check_came_first_site()
        {
            var searcher = new BingSiteSearcher();

            await searcher.SearchWorkingSitesAsync();

            Assert.AreNotEqual(string.Empty, searcher.WorkingSite);
        }
    }
}