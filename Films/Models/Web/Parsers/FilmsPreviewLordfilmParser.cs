using System.Collections.Generic;
using AngleSharp;
using Films.MVVMLogic.Models;

namespace Films.Models.Web.Parsers
{
    public class FilmsPreviewLordfilmParser : LordfilmParser<Film>
    {
        public override async IAsyncEnumerable<Film> GetFilms(string siteHtml)
        {
            var htmlDocument = await Parser.Context.OpenAsync(req => req.Content(siteHtml));

            var filmElements = htmlDocument.QuerySelectorAll("div.sect-cont.sect-items.clearfix div.th-item");

            if (filmElements.Length <= 0)
                yield return null;
            
            foreach (var filmElement in filmElements)
            {
                string nameFilm = Parser.ClearWhiteSpaces(filmElement?.QuerySelector("div.th-title")?.TextContent);
                string pictureLink = filmElement?.QuerySelector("img")?.GetAttribute("srcset");

                if (nameFilm == null || pictureLink == null)
                    continue;
                
                yield return new Film(nameFilm, pictureLink);
            }
        }
    }
}