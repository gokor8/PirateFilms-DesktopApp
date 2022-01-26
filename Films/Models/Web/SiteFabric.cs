using System.Threading.Tasks;
using Films.Models.Web.SiteSearchers;

namespace Films.Models.Web
{
    public class SiteFabric
    {
        public async Task<ISearcher> CreateSiteSearcherAsync()
        {
            ISearcher searcher = new LocalSiteSearcher();
            await searcher.SearchWorkingSitesAsync();

            if (searcher.WorkingSite == string.Empty)
            {
                searcher = new BingSiteSearcher();
                await searcher.SearchWorkingSitesAsync();
            }

            return searcher;
        }

        public string CreateSiteLink()
        {
            ISearcher searcher = null;
            Task.Run(async() =>
            {
                searcher = await CreateSiteSearcherAsync();
            }).Wait();

            return searcher.WorkingSite;
        }
    }
}