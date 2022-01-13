using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AngleSharp;
using Films.Web.BingSearch;
using Films.Web.BingSearch.BingObjects;
using Films.Web.HttpClients; 

namespace Films.MVVMLogic.Models.ImagesScroll
{
    public class FilmParser
    {
        public delegate void CorrectFilmBuilder(Film film);
        public event CorrectFilmBuilder OnFilm;
        private readonly SiteFilmsHttp siteHttp = SiteFilmsHttp.GetInstance();
        
        public async Task CreatingFilms()
        {
            Bing bing = new Bing();
            string html = await siteHttp.Client.GetStringAsync(siteHttp.Client.BaseAddress);
            var htmlDocument = await siteHttp.Context.OpenAsync(req => req.Content(html));
            
            for (int countFilms = 0; countFilms < 5; countFilms++) // Парсю первые 5 фильмов
            {
                int copyCount = countFilms;

                //Вывожу в отдельную задачу, так быстрее
                _ = Task.Run(async () =>
                {
                    string nameFilm = siteHttp.ClearWhiteSpaces(
                       htmlDocument.QuerySelectorAll("div.sect-cont.sect-items.clearfix div.th-title")[copyCount].TextContent);

                    string linkImage = "";
                    int linkImageIterations = 0;

                    while (linkImage == "" && linkImageIterations != 3)
                    {
                        linkImage = await bing.GetLink(nameFilm,
                            new ImageElement("&qft=+filterui%3aimagesize-custom_1000_1000&first=1&tsc=ImageBasicHover"));

                        if (linkImage == "")
                            await Task.Delay(1000);
                        linkImageIterations++;
                    }

                    try
                    {
                        var filmBuilder = new FilmBuilder().SetName(nameFilm).DownloadPicture(linkImage, copyCount)
                            .ValidateLink();

                        OnFilm?.Invoke(filmBuilder.Film);
                    }
                    catch(Exception exc)
                    {
                        Debug.WriteLine($"Плохая ссылка в |Images 49| {linkImage}" + exc.StackTrace);
                        //MessageBox.Show($"Плохая ссылка в |Images 49| {linkImage}" + exc.StackTrace);
                    }
                });
            }
        }
    }
}
