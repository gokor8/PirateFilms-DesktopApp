using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Films.Models.Web.HttpClients
{
    public class SiteFilmsHttp : BaseHttp
    {
        private static readonly SiteFilmsHttp _instance = new SiteFilmsHttp();

        public SiteFilmsHttp()
        {
            SetSiteHeaders().Wait();
        }
        private async Task SetSiteHeaders()
        {
            var searcher = await new SiteFabric().CreateSiteSearcherAsync();
            //Client.DefaultRequestHeaders.Add("Referer", "https://yandex.ru/");
            //Client.DefaultRequestHeaders.Add("User-Agent", "Fiddler Everywhere");
            Client.BaseAddress = new Uri(searcher.WorkingLinks.First());
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
