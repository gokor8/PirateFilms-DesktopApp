using System.Net;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using Films.Web.HttpClients;
using Newtonsoft.Json;

namespace Films.Web.BingSearch.BingObjects
{
    public class ImageElement : IBingElement
    {
        private const string image = "images";
        public string SearchParametrs { get; private set; }
        private readonly PublicHttp httpClient = PublicHttp.GetInstance();

        public ImageElement(string searchParametrs)
        {
            SearchParametrs = searchParametrs;
        }

        public async Task<string> GetWorkingLink(string htmlСontent)
        {
            IDocument htmlDocument = await httpClient.Context.OpenAsync(html => html.Content(htmlСontent));
            string linkPicture = "";

            foreach (var filmImage in htmlDocument.QuerySelectorAll("div.imgpt a.iusc"))
            {
                string mAttribute = filmImage.GetAttribute("m");
                //Получаем ссылку на картинку, и проверяем ссылку на валидность(и блокировку)
                linkPicture = JsonConvert.DeserializeObject<JsonImageBing>(mAttribute)?.Murl;

                var linkCode = HttpStatusCode.BadRequest;

                if (linkPicture != null)
                {
                    var codeRequest = await httpClient.Client.GetAsync(linkPicture);
                    linkCode = codeRequest.StatusCode;
                }

                if(linkCode == HttpStatusCode.OK)
                    return linkPicture;
            }

            return linkPicture;
        }

        public string GetObjectType()
        {
            return image;
        }

        private class JsonImageBing
        {
            public string Murl { get; set; }
        }
    }
}
