using System.Linq;
using System.Threading.Tasks;
using Films.Models.Web.SiteSearchers;

namespace Films.Models.Web
{
    public class SiteFabric
    {
        public async Task<ISearcher> CreateSiteSearcherAsync()
        {
            ISearcher searcher = new LocalSiteSearcher();
            var workingLink = await searcher.SearchWorkingSitesAsync().FirstAsync();

            if (workingLink == string.Empty)
                searcher = new BingSiteSearcher();

            return searcher;
        }

        public string CreateSiteLink()
        {
            ISearcher searcher = CreateSiteSearcherAsync().Result;
            
            return searcher.SearchWorkingSitesAsync().FirstAsync().Result;
        }
    }
}