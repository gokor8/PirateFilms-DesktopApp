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

            await searcher.SearchWorkingSitesAsync(true);

            HashSet<string> dbWorkingLinkList = new HashSet<string>();

            Task.WaitAll(searcher.TrackedTasks.ToArray());

            using (var context = new SitesContext())
            {
                foreach (var dbLink in context.Links)
                {
                    if (searcher.WorkingLinks.Contains(dbLink.WorkingLink))
                    {
                        dbWorkingLinkList.Add(dbLink.WorkingLink);
                    }
                }
            }

            // Ожидание добавления в бд

            Assert.AreEqual(searcher.WorkingLinks.Count(), dbWorkingLinkList.Count);
        }

        [TestMethod]
        public async Task Check_came_first_site()
        {
            var searcher = new BingSiteSearcher();

            await searcher.SearchWorkingSitesAsync();

            Assert.AreNotEqual(string.Empty, searcher.WorkingLinks.FirstOrDefault());
        }
    }
}