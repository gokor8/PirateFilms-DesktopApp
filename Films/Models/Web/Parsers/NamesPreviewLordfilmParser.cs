using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp;

namespace Films.Models.Web.Parsers
{
    public class NamesPreviewLordfilmParser : LordfilmParser<string>
    {
        protected readonly ParserCore _parser = new ParserCore();

        public override async Task<IEnumerable<string>> GetPopularFilmsName(string siteHtml, int countFilms)
        {
            List<string> filmNames = new List<string>();
            var htmlDocument = await _parser.Context.OpenAsync(req => req.Content(siteHtml));

            var filmElements = htmlDocument.QuerySelectorAll("div.sect-cont.sect-items.clearfix div.th-title");

            if (filmElements.Length <= 0)
                return filmNames;


            for (int countFilm = 0; countFilm < countFilms; countFilm++)
            {
                string nameFilm = _parser.ClearWhiteSpaces(filmElements[countFilm]?.TextContent);

                filmNames.Add(nameFilm ?? String.Empty);
            }

            return filmNames;
        }

        public override async Task<IEnumerable<string>> GetFilms()
        {
            return null;
        }
    }
}