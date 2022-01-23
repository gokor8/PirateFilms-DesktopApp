using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using Films.MVVMLogic.Models.Web.Parsers;
using Films.Web.BingSearch;
using Films.Web.HttpClients;

namespace Films.Classes.BingSearch
{
    public class SearchElement : IBingElement
    {
        private const string SEARCH = "";
        
        private PublicHttp _publicHttp = PublicHttp.GetInstance();

        public SearchElement()
        {}
        public SearchElement(string searchParametrs) 
            => SearchParametrs= searchParametrs;

        public string SearchParametrs { get; private set; }

        public async Task<string> GetWorkingLink(string htmlContent)
        {
            IDocument angleHtml = await _publicHttp.Context.OpenAsync(html => html.Content(htmlContent));
            string worksSiteLink = string.Empty;

            foreach (var linksElement in angleHtml.QuerySelector("#b_results").QuerySelectorAll("cite"))
            {
                worksSiteLink = linksElement.TextContent;

                var httpResponse = await _publicHttp.Client.GetAsync(worksSiteLink);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    var siteHtml = await httpResponse.Content.ReadAsStringAsync();
                    var siteFilmNamesCollection = await new SiteParser().GetPopularFilmsName(siteHtml, 5);

                    if (siteFilmNamesCollection.Count() == 5)
                        return worksSiteLink;
                }
            }
            
            return System.Configuration.ConfigurationManager.
                ConnectionStrings["BackUpLink"].ConnectionString;
        }

        public string GetObjectType()
        {
            return SEARCH;
        }
    }
}
