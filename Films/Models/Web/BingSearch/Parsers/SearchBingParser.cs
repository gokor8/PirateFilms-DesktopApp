using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using AngleSharp;
using AngleSharp.Dom;
using Films.Models.Web.HttpClients;
using Films.Models.Web.Parsers;

namespace Films.Models.Web.BingSearch.Parsers
{
    public sealed class SearchBingParser : IBingParser
    {
        private readonly PublicHttp _publicHttp = PublicHttp.GetInstance();

        public async IAsyncEnumerable<string> GetWorkingLinksAsync(string htmlContent)
        {
            IDocument angleDocument = await new ParserCore().Context.OpenAsync(html => html.Content(htmlContent));

            foreach (var linksElement in angleDocument.QuerySelector("#b_results")?.QuerySelectorAll("li.b_algo"))
            {
                string workingLink = linksElement.QuerySelector("a")?.GetAttribute("href");

                HttpResponseMessage httpResponse;

                try
                { 
                    httpResponse = await _publicHttp.Client.GetAsync(workingLink);
                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        continue;
                    }
                }
                catch (Exception)
                {
                    continue;
                }

                var siteHtml = await httpResponse.Content.ReadAsStringAsync();
                //Проверка на корректный html, методом парсинга
                var filmNamesCollection = await new LordfilmParser().GetPopularFilmsName(siteHtml, 5);

                if (filmNamesCollection.Count() == 5)
                    yield return workingLink;
            }

            yield return System.Configuration.ConfigurationManager.
                ConnectionStrings["BackUpLink"].ConnectionString;
        }
    }
}
