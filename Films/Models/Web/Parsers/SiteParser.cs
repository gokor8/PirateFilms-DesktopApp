using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp;
using Films.Web.HttpClients;

namespace Films.Models.Web.Parsers
{
    public class SiteParser
    {
        private readonly PublicHttp _publicHttp = PublicHttp.GetInstance();

        public async Task<IEnumerable<string>> GetPopularFilmsName(string siteHtml, int countFilms)
        {
            List<string> filmNamesCollection = new List<string>();
            var htmlDocument = await _publicHttp.Context.OpenAsync(req => req.Content(siteHtml));

            var filmElements = htmlDocument.QuerySelectorAll("div.sect-cont.sect-items.clearfix div.th-title");

            if (filmElements.Length <= 0)
                return filmNamesCollection;


            for (int countFilm = 0; countFilm < countFilms; countFilm++) // Парсю первые 5 фильмов
            {
                int copyCount = countFilm;

                string nameFilm = _publicHttp.ClearWhiteSpaces(filmElements[copyCount]?.TextContent);

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