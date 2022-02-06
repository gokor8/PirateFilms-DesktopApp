using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using AngleSharp;
using AngleSharp.Dom;
using Films.Models.Web.HttpClients;
using Newtonsoft.Json;

namespace Films.Models.Web.BingSearch.Parsers
{
    public sealed class ImageBingParser : IBingParser
    {
        private readonly PublicHttp _publicHttp = PublicHttp.GetInstance();

        public async IAsyncEnumerable<string> GetWorkingLinksAsync(string htmlСontent)
        {
            IDocument htmlDocument = await new ParserCore().Context.OpenAsync(html => html.Content(htmlСontent));
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

        private class JsonImageBing
        {
            public string Murl { get; set; }
        }
    }
}
