using System;
using System.Linq;
using System.Threading.Tasks;
using Films.Models.Web.BingSearch.SiteSearchers;
using Films.Models.Web.Parsers;
using Films.Web.HttpClients;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTest.Web_Test.Parsers_Tests
{
    [TestClass]
    public class LordfilmParserTests
    {
        [TestMethod]
        public async Task Get_One_bing_film_contains_site_name()
        {
            string workingSiteLink = await new LocalSiteSearcher().SearchWorkingSitesAsync().FirstAsync();

            if (workingSiteLink == null)
                Assert.Fail("В базе данных нет рабочей ссылки");

            string siteHtml = await PublicHttp.GetInstance().Client.GetStringAsync(workingSiteLink);

            var filmsName = await new LordfilmParser().GetPopularFilmsName(siteHtml, 5);

            Assert.IsNotNull(filmsName);
        }
    }
}