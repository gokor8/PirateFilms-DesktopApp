using System.Threading.Tasks;
using Films.Models.Web.HttpClients;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTest.Web_Test.HttpClients_Test
{
    [TestClass]
    public class SiteFilmsHttpTests
    {
        [TestMethod]
        public async Task Check_site_performance()
        {
            SiteFilmsHttp siteFilmsHttp = SiteFilmsHttp.GetInstance();

            string html = await siteFilmsHttp.Client.GetStringAsync(siteFilmsHttp.Client.BaseAddress);

            Assert.IsNotNull(html);
        }
    }
}