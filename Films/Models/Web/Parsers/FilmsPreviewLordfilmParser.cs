using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp;
using Films.MVVMLogic.Models;

namespace Films.Models.Web.Parsers
{
    public class FilmsPreviewLordfilmParser : LordfilmParser<Film>
    {
        public override async Task<IEnumerable<Film>> GetPopularFilmsName(string siteHtml, int countFilms)
        {
            List<Film> filmNames = new List<Film>();
            var htmlDocument = await _parser.Context.OpenAsync(req => req.Content(siteHtml));

            var filmElements = htmlDocument.QuerySelectorAll("div.sect-cont.sect-items.clearfix div.th-item");

            if (filmElements.Length <= 0)
                return filmNames;


            for (int countFilm = 0; countFilm < countFilms; countFilm++)
            {
                string nameFilm = _parser.ClearWhiteSpaces(filmElements[countFilm]?.QuerySelector("div.th-title")?.TextContent);
                string pictureLink = filmElements[countFilm]?.QuerySelector("img")?.GetAttribute("srcset");

                if (nameFilm == null || pictureLink == null)
                    continue;
                
                filmNames.Add(new Film(nameFilm, "https://nu2.lordfilm3.cam" + pictureLink));
            }

            return filmNames;
        }

        public override async Task<IEnumerable<Film>> GetFilms()
        {
            throw new System.NotImplementedException();
        }
    }
}