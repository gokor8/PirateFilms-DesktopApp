using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using AngleSharp;
using AngleSharp.Dom;
using Films.Web.HttpClients;
using Newtonsoft.Json;

namespace Films.Models.Web.BingSearch.BingObjects
{
    public sealed class BingImageParser : IBingParser
    {
        private const string IMAGE = "images/";

        private readonly PublicHttp _publicHttp = PublicHttp.GetInstance();

        public BingImageParser(string searchParametrs)
        {
            SearchParametrs = searchParametrs;
        }

        public string SearchParametrs { get; }


        public async IAsyncEnumerable<string> GetWorkingLinksAsync(string htmlСontent)
        {
            IDocument htmlDocument = await _publicHttp.Context.OpenAsync(html => html.Content(htmlСontent));
            string linkPicture = string.Empty;

            foreach (var filmImage in htmlDocument.QuerySelectorAll("div.imgpt a.iusc"))
            {
                string mAttribute = filmImage.GetAttribute("m");
                //Получаем ссылку на картинку, и проверяем ссылку на валидность(и блокировку)
                linkPicture = JsonConvert.DeserializeObject<JsonImageBing>(mAttribute)?.Murl;

                HttpResponseMessage siteResponseMessage = null;

                if (linkPicture != null)
                {
                    try
                    {
                        siteResponseMessage = await _publicHttp.Client.GetAsync(linkPicture);
                    } catch (Exception)
                    { continue; }
                }

                string endLinkPicture = linkPicture
                    .Substring(linkPicture.Length - 6, linkPicture.Length - (linkPicture.Length - 6));

                if (siteResponseMessage.StatusCode == HttpStatusCode.OK &&
                   !siteResponseMessage.RequestMessage.RequestUri.Host.Contains("warning.rt") &&
                   (!endLinkPicture.Contains("?") && endLinkPicture.Contains(".")))
                     yield return linkPicture;
            }

            yield return linkPicture;
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
