namespace Films.Models.Web.BingSearch
{
    public interface IBingFactory
    {
        public IBingSettings CreateBingSettings(string searchParams = "");

        public IBingParser CreateBingParser();
    }
}