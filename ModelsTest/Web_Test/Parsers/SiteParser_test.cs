using System.Threading.Tasks;
using Films.Classes.BingSearch;
using Films.Web.BingSearch;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTest.Web_Test.Parsers
{
    [TestClass]
    public class SiteParserTest
    {
        [TestMethod]
        public async Task GetPopularFilmsName()
        {
            string worksSiteLink = await new Bing().GetLink("lordfilm", new SearchElement());

            Assert.IsTrue(worksSiteLink.Contains("lordfilm"));
        }
    }
}