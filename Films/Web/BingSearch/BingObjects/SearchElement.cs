using System;
using System.Threading.Tasks;
using Films.Web.BingSearch;

namespace Films.Classes.BingSearch
{
    class SearchElement : IBingElement
    {
        private const string _search = "search";
        public string SearchParametrs { get; private set; }

        public Task<string> GetWorkingLink(string htmlContent)
        {
            throw new NotImplementedException();
        }

        public string GetObjectType()
        {
            return _search;
        }
    }
}
