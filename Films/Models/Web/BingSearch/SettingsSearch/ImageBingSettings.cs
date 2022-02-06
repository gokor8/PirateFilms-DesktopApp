namespace Films.Models.Web.BingSearch.SettingsSearch
{
    public sealed class ImageBingSettings : IBingSettings
    {
        public string SearchType => "images/";
        public ImageBingSettings(string searchParametrs)
        {
            SearchParametrs = searchParametrs;
        }

        public string SearchParametrs { get; } = string.Empty;
    }
}