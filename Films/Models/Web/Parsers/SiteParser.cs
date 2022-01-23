using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp;
using Films.Web.HttpClients;

namespace Films.MVVMLogic.Models.Web.Parsers
{
    public class SiteParser
    {
        private SiteFilmsHttp _siteFilmsHttp = SiteFilmsHttp.GetInstance();

        public async Task<IEnumerable<string>> GetPopularFilmsName(string siteHtml, int countFilms)
        {
            List<string> filmNamesCollection = new List<string>();
            var htmlDocument = await _siteFilmsHttp.Context.OpenAsync(req => req.Content(siteHtml));

            for (int countFilm = 0; countFilm < countFilms; countFilm++) // Парсю первые 5 фильмов
            {
                int copyCount = countFilm;

                string nameFilm = _siteFilmsHttp.ClearWhiteSpaces(
                    htmlDocument.QuerySelectorAll("div.sect-cont.sect-items.clearfix div.th-title")[copyCount]
                        .TextContent);

                filmNamesCollection.Add(nameFilm != null ? nameFilm : string.Empty);
            }

            return filmNamesCollection;
        }

        public async Task<IEnumerable<string>> GetFilms()
        {
            return null;
        }
    }
}