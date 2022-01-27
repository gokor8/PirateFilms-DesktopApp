using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Films.Web.HttpClients;

namespace Films.Models.Web.BingSearch
{
    public sealed class Bing
    {
        private readonly PublicHttp _publicHttp = PublicHttp.GetInstance();

        private IBingParser bingElement;

        public Bing()
        {
            _publicHttp.SetDefaultHeaders();
        }

        public async Task<IEnumerable<string>> GetLinksAsync(string textRequest, IBingParser bingElement, bool areAllLinks = false)
        {
            this.bingElement = bingElement;

            string contentResponse = null;
            int numberIteration = 0;

            //3 цикла == 3 попытки на получение нормального ответа с данными из бинга
            while (contentResponse == null && numberIteration != 3)
            {
                numberIteration++;

                await InitializeBingAsync();

                contentResponse = await GetBingResponse(textRequest);
            }

            var asyncLinkCollection = areAllLinks
                ? bingElement.GetWorkingLinksAsync(contentResponse)
                : bingElement.GetWorkingLinksAsync(contentResponse).Take(1);

            return await asyncLinkCollection.ToListAsync();
        }

        private async Task<string> GetBingResponse(string textRequest)
        {
            textRequest = $"https://www.bing.com/" +
                          $"{bingElement.GetObjectType()}" +
                          $"search?q={HttpUtility.UrlEncode(textRequest.Trim().Replace(" ", "+"))}" +
                          "&rdr=1" +
                          $"{bingElement.SearchParametrs}";
            var response = await _publicHttp.Client?.GetAsync(textRequest)!;

            string html = await response.Content.ReadAsStringAsync();

            int htmlByteCount = System.Text.Encoding.Unicode.GetByteCount(html);
            //74438 true with Accept-Encoding || Without Accept-Encoding 318910 true
            return await response.Content.ReadAsStringAsync();
        }

        public async Task InitializeBingAsync()
        {
            var response = await _publicHttp.Client.GetAsync("https://www.bing.com/");

            var responseCookies = response.Headers.FirstOrDefault(c => c.Key == "Set-Cookie").Value;

            if (responseCookies.Count() <= 1)
                return;

            foreach (var cookie in responseCookies)
            {
                string name = cookie.Substring(0, cookie.IndexOf("="));
                string value = cookie.Substring(cookie.IndexOf("=") + 1, cookie.IndexOf(";") - (cookie.IndexOf("=") + 1));
                _publicHttp.CookieContainer.Add(new Uri("https://www.bing.com/"), new Cookie(name, value));
            }
        }

        ~Bing()
        {
            _publicHttp.ClearAllHeaders();
        }
    }
}

