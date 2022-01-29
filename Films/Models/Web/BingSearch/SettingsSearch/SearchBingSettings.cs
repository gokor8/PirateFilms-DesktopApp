namespace Films.Models.Web.BingSearch.SettingsSearch
{
    public class SearchBingSettings : IBingSettings
    {
        public SearchBingSettings()
        { }

        public SearchBingSettings(string searchParametrs)
        {
            SearchParametrs = searchParametrs;
        }

        public string SearchParametrs { get; } = string.Empty;
        public string SearchType => string.Empty;
    }
}