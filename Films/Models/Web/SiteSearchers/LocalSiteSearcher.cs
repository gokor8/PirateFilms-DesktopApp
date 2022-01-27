using System;
using System.Collections.Generic;
using System.Net.Http;
using Films.Models.DataBaseLogic.LinksDataBase;
using Films.Web.HttpClients;

namespace Films.Models.Web.SiteSearchers
{
    public class LocalSiteSearcher : ISearcher
    {
        private readonly PublicHttp _publicHttp = new PublicHttp();

        public async IAsyncEnumerable<string> SearchWorkingSitesAsync()
        {
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
                        await context.SaveChangesAsync();
                        yield return link.WorkingLink;
                    }
                }
                await context.SaveChangesAsync();
            }
        }

        /*public async Task<string> GetFirstLinkAsync()
        {
            return await SearchWorkingSitesAsync().FirstAsync();
        }

        public IEnumerable<string> GetFirstWorkingLink()
        {

        }*/
    }
}