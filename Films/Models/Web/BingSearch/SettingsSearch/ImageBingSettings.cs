namespace Films.Models.Web.BingSearch.SettingsSearch
{
    public class ImageBingSettings : IBingSettings
    {
        public ImageBingSettings(string searchParametrs)
        {
            SearchParametrs = searchParametrs;
        }

        public string SearchParametrs { get; } = string.Empty;
        public string SearchType => "images/";
    }
}