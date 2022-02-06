namespace Films.Models.Web.BingSearch.SettingsSearch
{
    public sealed class SearchBingSettings : IBingSettings
    {
        public string SearchType => string.Empty;
        public SearchBingSettings()
        { }

        public SearchBingSettings(string searchParametrs)
        {
            SearchParametrs = searchParametrs;
        }

        public string SearchParametrs { get; } = string.Empty;
    }
}