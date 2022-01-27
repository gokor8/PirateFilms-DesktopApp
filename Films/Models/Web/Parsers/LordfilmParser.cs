using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp;
using Films.Web.HttpClients;

namespace Films.Models.Web.Parsers
{
    public class LordfilmParser
    {
        private readonly PublicHttp _publicHttp = PublicHttp.GetInstance();

        public async Task<IEnumerable<string>> GetPopularFilmsName(string siteHtml, int countFilms)
        {
            List<string> filmNames = new List<string>();
            var htmlDocument = await _publicHttp.Context.OpenAsync(req => req.Content(siteHtml));

            var filmElements = htmlDocument.QuerySelectorAll("div.sect-cont.sect-items.clearfix div.th-title");

            if (filmElements.Length <= 0)
                return filmNames;


            for (int countFilm = 0; countFilm < countFilms; countFilm++)
            {
                int copyCount = countFilm;

                string nameFilm = _publicHttp.ClearWhiteSpaces(filmElements[copyCount]?.TextContent);

                filmNames.Add(nameFilm ?? string.Empty);
            }

            return filmNames;
        }

        public async Task<IEnumerable<string>> GetFilms()
        {
            return null;
        }
    }
}