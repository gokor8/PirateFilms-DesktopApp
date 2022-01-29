using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Films.Models.DataBaseLogic.LinksDataBase;
using Films.Models.Web.HttpClients;

namespace Films.Models.Web.BingSearch.SiteSearchers
{
    public class LocalSiteSearcher : ISearcher
    {
        private readonly PublicHttp _publicHttp = new PublicHttp();

        public IEnumerable<string> WorkingLinks { get; private set; }

        public async Task SearchWorkingSitesAsync(bool takeAll = false)
        {
            List<string> links = new List<string>();
            WorkingLinks = links;

            using (var context = new SitesContext())
            {
                foreach (var link in context.Links)
                {
                    HttpResponseMessage responseMessage;

                    try
                    {
                        responseMessage = await _publicHttp.Client.GetAsync(link.WorkingLink);
                    }
                    catch (Exception)
                    {
                        context.Links.Remove(link);
                        continue;
                    }

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        links.Add(link.WorkingLink);
                        if (!takeAll)
                            break;
                    }
                }
                await context.SaveChangesAsync();
            }
        }

    }
}