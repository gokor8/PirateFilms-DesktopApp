using System.Linq;
using System.Threading.Tasks;
using Films.Models.Web.BingSearch.SiteSearchers;
using Films.Models.Web.HttpClients;
using Films.Models.Web.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTest.Models_Test.Web_Test.Parsers_Tests
{
    [TestClass]
    public class NamesPreviewLordfilmParserTests
    {
        private string _siteLink;
        
        [TestInitialize]
        public async Task Search_site_link()
        {
            var localSiteSearcher = new LocalSiteSearcher();
            await localSiteSearcher.SearchWorkingSitesAsync();

            _siteLink = localSiteSearcher.WorkingLinks.FirstOrDefault();

            if (_siteLink == null)
                Assert.Fail("В базе данных нет рабочей ссылки");
        }
        
        [TestMethod]
        public async Task Get_5_bing_film_contains_site_name()
        {
            string siteHtml = await PublicHttp.GetInstance().Client.GetStringAsync(_siteLink);

            var filmsName = await new NamesPreviewLordfilmParser().GetFilms(siteHtml).Take(5).ToListAsync();

            Assert.IsTrue(filmsName.Count == 5);
        }
        
        [TestMethod]
        public async Task Get_All_bing_film_contains_site_name()
        {
            string siteHtml = await PublicHttp.GetInstance().Client.GetStringAsync(_siteLink);

            var filmsName = await new NamesPreviewLordfilmParser().GetFilms(siteHtml).ToListAsync();

            Assert.IsTrue(filmsName.Count > 0);
        }
    }
}