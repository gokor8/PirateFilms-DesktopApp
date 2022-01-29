using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp;

namespace Films.Models.Web.Parsers
{
    public class LordfilmParser
    {
        private readonly ParserCore _parser = new ParserCore();

        public async Task<IEnumerable<string>> GetPopularFilmsName(string siteHtml, int countFilms)
        {
            List<string> filmNames = new List<string>();
            var htmlDocument = await _parser.Context.OpenAsync(req => req.Content(siteHtml));

            var filmElements = htmlDocument.QuerySelectorAll("div.sect-cont.sect-items.clearfix div.th-title");

            if (filmElements.Length <= 0)
                return filmNames;


            for (int countFilm = 0; countFilm < countFilms; countFilm++)
            {
                int copyCount = countFilm;

                string nameFilm = _parser.ClearWhiteSpaces(filmElements[copyCount]?.TextContent);

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