using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Films.Models.Web.BingSearch;
using Films.Models.Web.BingSearch.BingObjects;
using Films.Models.Web.HttpClients;
using Films.Models.Web.Parsers;
using Films.MVVMLogic.Models;

namespace Films.Models.ScrollFilms
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
            var filmNamesCollection = await new SiteParser().GetPopularFilmsName(html, 5);

            // Парсю первые 5 фильмов
            for (int countFilms = 0; countFilms < filmNamesCollection.Count(); countFilms++) 
            {
                int copyCount = countFilms;

                //Вывожу в отдельную задачу, так быстрее
                _ = Task.Run(async () =>
                {
                    string nameFilm = filmNamesCollection.ElementAt(copyCount);

                    if (nameFilm == null)
                        throw new NullReferenceException("Ало, аче имени у фильма нету, сайт не найден судя по всему");

                    string linkImage = "";
                    int linkImageIterations = 0;

                    while (linkImage == "" && linkImageIterations != 3)
                    {
                        var linkCollection = await bing.GetLinksAsync(nameFilm,
                            new ImageElement(
                                "&qft=+filterui%3aimagesize-custom_1000_1000&first=1&tsc=ImageBasicHover"));

                        linkImage = linkCollection.First();

                        if (linkImage == "")
                            await Task.Delay(1000);
                        linkImageIterations++;
                    }

                    var filmBuilder = new FilmBuilder().SetName(nameFilm).DownloadPicture(linkImage, copyCount)
                        .ValidateLink();

                    OnFilm?.Invoke(filmBuilder.Film);
                });
            }
        }
    }
}
