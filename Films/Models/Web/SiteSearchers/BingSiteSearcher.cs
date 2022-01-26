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

        public string WorkingSite { get; private set; } = string.Empty;
        public IEnumerable<string> WorkingSites { get; private set; } = new List<string>();

        public async Task SearchWorkingSitesAsync()
        {
            WorkingSites = await new Bing()
                .GetLinksAsync(SiteName, new SearchElement(), true);

            WorkingSite = WorkingSites.First();

            refreshDatabaseLinks();
        }

        private void refreshDatabaseLinks()
        {
            using (var context = new SitesContext())
            {
                foreach (var workinglink in WorkingSites)
                {
                    if (context.Links.Count(l=>l.WorkingLink == workinglink) <= 0)
                    {
                        context.Links.Add(new Link() { WorkingLink = workinglink });
                    }
                }

                context.SaveChanges();
            }
        }
    }
}