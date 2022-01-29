using System.Linq;
using System.Threading.Tasks;
using Films.Models.Web.BingSearch;
using Films.Models.Web.BingSearch.SiteSearchers;

namespace Films.Models.Web
{
    public class SiteFabric
    {
        public async Task<ISearcher> CreateSiteSearcherAsync()
        {
            ISearcher searcher = new LocalSiteSearcher();
            await searcher.SearchWorkingSitesAsync();

            if (searcher.WorkingLinks.Count() <= 0)
            {
                searcher = new BingSiteSearcher();
                await searcher.SearchWorkingSitesAsync();
            }

            return searcher;
        }
    }
}