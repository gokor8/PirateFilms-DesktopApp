using System.Threading.Tasks;
using Films.Classes.BingSearch;
using Films.Web.BingSearch;
using Films.Web.HttpClients;

namespace Films.MVVMLogic.Models.Web.SiteSearchers
{
    public class BingSiteSearcher
    {
        private const string SiteName = "lordfilm";
        private Bing bingSearch = new Bing();

        public async Task SearchSite()
        {
            string worksSiteLink = await bingSearch.GetLink(SiteName, new SearchElement());


        }
    }
}