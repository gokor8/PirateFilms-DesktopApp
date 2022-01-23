using System;
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
        private const string IMAGE = "images/";

        private readonly PublicHttp _publicHttp = PublicHttp.GetInstance();

        public ImageElement(string searchParametrs)
        {
            SearchParametrs = searchParametrs;
        }

        public string SearchParametrs { get; private set; }

        public async Task<string> GetWorkingLink(string htmlСontent)
        {
            IDocument htmlDocument = await _publicHttp.Context.OpenAsync(html => html.Content(htmlСontent));
            string linkPicture = string.Empty;

            foreach (var filmImage in htmlDocument.QuerySelectorAll("div.imgpt a.iusc"))
            {
                string mAttribute = filmImage.GetAttribute("m");
                //Получаем ссылку на картинку, и проверяем ссылку на валидность(и блокировку)
                linkPicture = JsonConvert.DeserializeObject<JsonImageBing>(mAttribute)?.Murl;

                var linkCode = HttpStatusCode.BadRequest;

                if (linkPicture != null)
                {
                    /*try
                    {*/
                        var codeRequest = await _publicHttp.Client.GetAsync(linkPicture);
                        linkCode = codeRequest.StatusCode;
                    /*}
                    catch (Exception)
                    {
                        continue;
                    }*/
                }

                if(linkCode == HttpStatusCode.OK)
                     return linkPicture;
            }

            return linkPicture;
        }

        public string GetObjectType()
        {
            return IMAGE;
        }

        private class JsonImageBing
        {
            public string Murl { get; set; }
        }
    }
}
