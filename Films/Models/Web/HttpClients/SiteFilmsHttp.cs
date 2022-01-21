using System;
using System.IO;
using System.Threading.Tasks;

namespace Films.Web.HttpClients
{
    public class SiteFilmsHttp : BaseHttp
    {
        private static SiteFilmsHttp _instance = new SiteFilmsHttp();
        public SiteFilmsHttp()
        {
            SetSiteHeaders();
        }
        private void SetSiteHeaders()
        {
            //Client.DefaultRequestHeaders.Host = "n.lordfilms-film.online";
            Client.DefaultRequestHeaders.Add("Referer", "https://yandex.ru/");
            Client.DefaultRequestHeaders.Add("User-Agent", "Fiddler Everywhere");
            Client.BaseAddress = new Uri("https://lordik.lordfilm.cfd/film/");
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
