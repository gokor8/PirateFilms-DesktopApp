using Films.Models.Web.BingSearch.Parsers;
using Films.Models.Web.BingSearch.SettingsSearch;

namespace Films.Models.Web.BingSearch.Factories
{
    public class SearchBingFactory : IBingFactory
    {
        public IBingSettings CreateBingSettings(string searchParams = "")
        {
            return new SearchBingSettings(searchParams);
        }

        public IBingParser CreateBingParser()
        {
            return new SearchBingParser();
        }
    }
}