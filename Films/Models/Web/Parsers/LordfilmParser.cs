using System.Collections.Generic;

namespace Films.Models.Web.Parsers
{
    public abstract class LordfilmParser<T>
    {
        protected readonly ParserCore Parser = new ParserCore();
        public abstract IAsyncEnumerable<T> GetFilms(string siteHtml);
    }
}