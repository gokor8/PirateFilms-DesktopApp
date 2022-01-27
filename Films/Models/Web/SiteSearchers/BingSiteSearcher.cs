using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Films.Models.DataBaseLogic.LinksDataBase;
using Films.Models.Web.BingSearch;
using Films.Models.Web.BingSearch.BingObjects;
using Films.MVVMLogic.Models.DataBaseLogic.LinksDataBase;

namespace Films.Models.Web.SiteSearchers
{
    public class BingSiteSearcher : ISearcher
    {
        private const string SiteName = "lordfilm";

        public async IAsyncEnumerable<string> SearchWorkingSitesAsync()
        {
            var workingSiteLinks = await new Bing()
                .GetLinksAsync(SiteName, new SearchElement(), true);

            _ = Task.Run(() => refreshDatabaseLinks(workingSiteLinks));

            foreach (var link in workingSiteLinks)
            {
                yield return link;
            }
        }

        private void refreshDatabaseLinks(IEnumerable<string> workingSites)
        {
            using (var context = new SitesContext())
            {
                foreach (var workingLink in workingSites)
                {
                    if (context.Links.FirstOrDefault(l=>l.WorkingLink == workingLink) == null)
                    {
                        context.Links.Add(new Link() { WorkingLink = workingLink });
                    }
                }

                context.SaveChanges();
            }
        }
    }
}