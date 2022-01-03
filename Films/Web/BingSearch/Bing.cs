using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Films.Web.HttpClients;

namespace Films.Web.BingSearch
{
    public class Bing
    {
        private readonly PublicHttp publicHttp = PublicHttp.GetInstance();

        private IBingElement typeObject;

        public Bing()
        {
            InitializeBing().Wait();
        }

        public async Task<string> GetLink(string textRequest, IBingElement typeObject)
        {
            this.typeObject = typeObject;
            //Проверка на работоспособность поиска
            //Если не срабатывает, то делаем повторный запрос(инициализацию)
            string contentRequest = await GetSearchResult(textRequest);

            if (contentRequest != null)
            {
                return await typeObject.GetWorkingLink(contentRequest);
            }
            else
            {   // Получаем кукисы
                await InitializeBing();
                contentRequest = await GetSearchResult(textRequest);

                return await typeObject.GetWorkingLink(contentRequest);
            }
        }
        private async Task<string> GetSearchResult(string textRequest)
        {
            textRequest = $"https://www.bing.com/{typeObject.GetObjectType()}/search?q={HttpUtility.UrlEncode(textRequest.Trim().Replace(" ", "+"))}{typeObject.SearchParametrs}";
            var response = await publicHttp.Client.GetAsync(textRequest);

            //Если нет кукисов, то все отлично, поисковик работает
            //Возвращаем html

            var headersCookies = response.Headers.Where(i => i.Key == "Set-Cookie");

            return headersCookies.Count() <= 1 ? await response.Content.ReadAsStringAsync() : null;
        }

        public async Task InitializeBing()
        {
            var response = await publicHttp.Client.GetAsync("https://www.bing.com/");
            foreach (var cookie in response.Headers.FirstOrDefault(i => i.Key == "Set-Cookie").Value)
            {
                string name = cookie.Substring(0, cookie.IndexOf("="));
                string value = cookie.Substring(cookie.IndexOf("=") + 1, cookie.IndexOf(";") - (cookie.IndexOf("=") + 1));
                publicHttp.CookieContainer.Add(new Uri("https://www.bing.com/"), new Cookie(name, value));
            }
        }

    }
}

