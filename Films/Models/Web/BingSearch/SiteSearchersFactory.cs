using System.Linq;
using System.Threading.Tasks;
using Films.Models.Web.BingSearch.SiteSearchers;

namespace Films.Models.Web.BingSearch
{
    public class SiteSearcherFactory
    {
        public async Task<ISearcher> CreateSiteSearcherAsync()
        {
            ISearcher searcher = new LocalSiteSearcher();
            await searcher.SearchWorkingSitesAsync();

            if (!searcher.WorkingLinks.Any())
            {
                searcher = new BingSiteSearcher();
                await searcher.SearchWorkingSitesAsync();
            }

            return searcher;
        }
    }
}