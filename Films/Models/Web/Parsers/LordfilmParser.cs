using System.Collections.Generic;
using System.Threading.Tasks;

namespace Films.Models.Web.Parsers
{
    public abstract class LordfilmParser<T>
    {
        protected readonly ParserCore Parser = new ParserCore();

        public abstract Task<IEnumerable<T>> GetPopularFilmsName(string siteHtml, int countFilms);
        public abstract Task<IEnumerable<T>> GetFilms();
    }
}