using System;
using System.Collections.Generic;
using AngleSharp;

namespace Films.Models.Web.Parsers
{
    public class NamesPreviewLordfilmParser : LordfilmParser<string>
    {
        public override async IAsyncEnumerable<string> GetFilms(string siteHtml)
        {
            List<string> filmNames = new List<string>();
            var htmlDocument = await Parser.Context.OpenAsync(req => req.Content(siteHtml));

            var filmElements = htmlDocument.QuerySelectorAll("div.sect-cont.sect-items.clearfix div.th-title");

            if (filmElements.Length <= 0)
                yield return String.Empty;


            foreach (var filmElement in filmElements)
            {
                string nameFilm = Parser.ClearWhiteSpaces(filmElement?.TextContent);

                yield return nameFilm ?? String.Empty;
            }
        }
    }
}