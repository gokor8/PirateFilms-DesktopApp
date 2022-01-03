using System.Threading.Tasks;
using Films.Web.HttpClients;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTest.Web_Test.HttpClients_Test
{
    [TestClass]
    public class SiteFilmsHttp_Test
    {
        [TestMethod]
        public async Task Initialize_working_site()
        {
            SiteFilmsHttp siteFilmsHttp = SiteFilmsHttp.GetInstance();

            string html = await siteFilmsHttp.Client.GetStringAsync(siteFilmsHttp.Client.BaseAddress);

            Assert.IsNotNull(html);
        }
    }
}