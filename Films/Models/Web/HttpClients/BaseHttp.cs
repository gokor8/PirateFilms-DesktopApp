using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp;

namespace Films.Models.Web.HttpClients
{
    public abstract class BaseHttp
    {
        public CookieContainer CookieContainer;
        private HttpClientHandler _handler;
        public HttpClient Client;
        public IBrowsingContext Context;

        protected BaseHttp()
        {
            CookieContainer = new CookieContainer();
            _handler = new HttpClientHandler() { 
                CookieContainer = CookieContainer,
                AllowAutoRedirect = true,
                UseCookies = true,
                PreAuthenticate = true,
                UseDefaultCredentials = true
            };
            Client = new HttpClient(_handler);
            Context = BrowsingContext.New(Configuration.Default);
            
            SetDefaultHeaders();
        }

        public async Task<string> Download(string link, string path)
        {
            var fileStream = await GetStreamClient(link);
            using (var targetFile = File.Create(path))
            {
                await fileStream.CopyToAsync(targetFile);
            }

            return path;
        }

        public abstract Task<Stream> GetStreamClient(string link);

        public string ClearWhiteSpaces(string value)
        {
            value = Regex.Replace(value, @"\s+", " ");
            if (value.Length > 0)
            {
                if (value[0] == ' ')
                    value = value.Substring(1);

                value = value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower();
            }
            return value.Trim();
        }

        public void SetDefaultHeaders()
        {
            Client.DefaultRequestHeaders.Add("User-Agent", "AmPirate");
            Client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
            Client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
        }

        public void ClearAllHeaders()
        {
            Client.DefaultRequestHeaders.Clear();
            SetDefaultHeaders();
        }
    }
}
