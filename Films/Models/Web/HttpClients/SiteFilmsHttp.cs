using System;
using System.IO;
using System.Threading.Tasks;

namespace Films.Models.Web.HttpClients
{
    public class SiteFilmsHttp : BaseHttp
    {
        private static readonly SiteFilmsHttp _instance = new SiteFilmsHttp();
        public SiteFilmsHttp()
        {
            SetSiteHeaders();
        }
        private void SetSiteHeaders()
        {
            //Client.DefaultRequestHeaders.Add("Referer", "https://yandex.ru/");
            //Client.DefaultRequestHeaders.Add("User-Agent", "Fiddler Everywhere");
            Client.BaseAddress = new Uri(new SiteFabric().CreateSiteLink());
            Client.DefaultRequestHeaders.Host = Client.BaseAddress.Host;
        }

        public override Task<Stream> GetStreamClient(string link)
        {
            return Client.GetStreamAsync(link);
        }

        public static SiteFilmsHttp GetInstance()
        {
            return _instance;
        }
    }
}
