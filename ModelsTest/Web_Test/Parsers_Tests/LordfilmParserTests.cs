﻿using System.Linq;
using System.Threading.Tasks;
using Films.Models.Web.BingSearch.SiteSearchers;
using Films.Models.Web.HttpClients;
using Films.Models.Web.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTest.Web_Test.Parsers_Tests
{
    [TestClass]
    public class LordfilmParserTests
    {
        [TestMethod]
        public async Task Get_One_bing_film_contains_site_name()
        {

            var localSiteSearcher = new LocalSiteSearcher();
            await localSiteSearcher.SearchWorkingSitesAsync();

            var workingLink = localSiteSearcher.WorkingLinks.FirstOrDefault();

            if (workingLink == null)
                Assert.Fail("В базе данных нет рабочей ссылки");

            string siteHtml = await PublicHttp.GetInstance().Client.GetStringAsync(workingLink);

            var filmsName = await new LordfilmParser().GetPopularFilmsName(siteHtml, 5);

            Assert.IsTrue(5 == filmsName.Count());
        }
    }
}