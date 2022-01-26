using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Films.Models.DataBaseLogic.LinksDataBase;
using Films.Web.HttpClients;

namespace Films.Models.Web.SiteSearchers
{
    public class LocalSiteSearcher : ISearcher
    {
        private readonly PublicHttp _publicHttp = new PublicHttp();
        public string WorkingSite { get; private set; } = string.Empty;
        public IEnumerable<string> WorkingSites { get; private set; } = new List<string>();
        public async Task SearchWorkingSitesAsync()
        {
            List<string> workingSites = new List<string>();

            using (var context = new SitesContext())
            {
                foreach (var link in context.Links)
                {
                    try
                    {
                        var httpResponse = await _publicHttp.Client.GetAsync(link.WorkingLink);
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            workingSites.Add(link.WorkingLink);
                        }
                    }
                    catch (Exception)
                    {
                        context.Links.Remove(link);
                    }
                }

                await context.SaveChangesAsync();

                WorkingSites = workingSites;
                WorkingSite = workingSites.FirstOrDefault();
            }
        }
    }
}