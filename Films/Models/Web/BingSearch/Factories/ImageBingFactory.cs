using Films.Models.Web.BingSearch.Parsers;
using Films.Models.Web.BingSearch.SettingsSearch;

namespace Films.Models.Web.BingSearch.Factories
{
    public class ImageBingFactory : IBingFactory
    {
        public IBingSettings CreateBingSettings(string searchParams = "")
        {
            return new ImageBingSettings(searchParams);
        }

        public IBingParser CreateBingParser()
        {
            return new ImageBingParser();
        }
    }
}