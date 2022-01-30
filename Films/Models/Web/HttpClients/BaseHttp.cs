using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Films.Models.Web.HttpClients
{
    public abstract class BaseHttp
    {
        public CookieContainer CookieContainer;
        private HttpClientHandler _handler;
        public HttpClient Client;

        protected BaseHttp()
        {
            CookieContainer = new CookieContainer();
            _handler = new HttpClientHandler()
            {
                CookieContainer = CookieContainer,
                AllowAutoRedirect = true,
            };

            Client = new HttpClient(_handler);
            Client.Timeout = TimeSpan.FromMinutes(1);

            SetBaseHeaders();
        }

        public async Task<string> Download(string link, string path)
        {
            var fileStream = await Client.GetStreamAsync(link);
            using (var targetFile = File.Create(path))
            {
                await fileStream.CopyToAsync(targetFile);
            }

            return path;
        }

        public abstract void SetOptionalHeaders();

        private void SetBaseHeaders()
        {
            Client.DefaultRequestHeaders.Add("User-Agent", "AmPirate");
            Client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
            Client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
        }

        public void ClearAllHeaders()
        {
            Client.DefaultRequestHeaders.Clear();
            SetBaseHeaders();
            SetOptionalHeaders();
        }
    }
}
