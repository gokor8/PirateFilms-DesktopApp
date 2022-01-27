using System.Linq;
using System.Threading.Tasks;
using Films.Models.Web.BingSearch.SiteSearchers;

namespace Films.Models.Web
{
    public class SiteFabric
    {
        public async Task<ISearcher> CreateSiteSearcherAsync()
        {
            ISearcher searcher = new LocalSiteSearcher();
            var workingLink = await searcher.SearchWorkingSitesAsync().Take(1).FirstAsync();

            if (workingLink == string.Empty)
                searcher = new BingSiteSearcher();

            return searcher;
        }

        public async Task<string> CreateSiteLink()
        {
            ISearcher searcher = new BingSiteSearcher();
            
            return await searcher.SearchWorkingSitesAsync().Take(1).FirstAsync();
        }
    }
}