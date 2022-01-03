using System;
using System.IO;
using System.Threading.Tasks;

namespace Films.Web.HttpClients
{
    public class SiteFimlsHttp : BaseHttp
    {
        private static SiteFimlsHttp _instance = new SiteFimlsHttp();
        public SiteFimlsHttp()
        {
            SetSiteHeaders();
        }
        private void SetSiteHeaders()
        {
            Client.DefaultRequestHeaders.Host = "mr.lordfilm.fans";
            Client.DefaultRequestHeaders.Add("Referer", "https://yandex.ru/");
            Client.DefaultRequestHeaders.Add("User-Agent", "Fiddler Everywhere");
            Client.BaseAddress = new Uri("https://w.lordfilm.cfd/film/");
        }

        public override Task<Stream> GetStreamClient(string link)
        {
            return Client.GetStreamAsync(link);
        }

        public static SiteFimlsHttp GetInstance()
        {
            return _instance;
        }
    }
}
