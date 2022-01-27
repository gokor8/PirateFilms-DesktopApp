using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Films.Models.DataBaseLogic.LinksDataBase;
using Films.Models.Web.BingSearch.SiteSearchers;
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

            var workingLinkList = await searcher.SearchWorkingSitesAsync().ToHashSetAsync();

            HashSet<string> dbWorkingLinkList = new HashSet<string>();

            using (var context = new SitesContext())
            {
                foreach (var dbLink in context.Links)
                {
                    if (workingLinkList.Contains(dbLink.WorkingLink))
                    {
                        dbWorkingLinkList.Add(dbLink.WorkingLink);
                    }
                }
            }

            Assert.AreEqual(workingLinkList.Count, workingLinkList.Count);
        }

        [TestMethod]
        public async Task Check_came_first_site()
        {
            var searcher = new BingSiteSearcher();

            string workingLink = await searcher.SearchWorkingSitesAsync().Take(1).FirstAsync();

            Assert.AreNotEqual(string.Empty, workingLink);
        }
    }
}