using System;
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
            var filmNamesCollection = await new LordfilmParser().GetPopularFilmsName(html, 5);

            for (int countFilms = 0; countFilms < filmNamesCollection.Count(); countFilms++) 
            {
                int copyCount = countFilms;

                //Вывожу в отдельную задачу, так быстрее
                _ = Task.Run(async () =>
                {
                    string nameFilm = filmNamesCollection.ElementAt(copyCount);

                    if (nameFilm == null)
                        throw new NullReferenceException("Ало, аче имени у фильма нету, сайт не найден судя по всему");


                    var linkCollection = await bing.GetLinksAsync(nameFilm,
                        new BingImageParser(
                            "&qft=+filterui%3aimagesize-custom_1000_1000&first=1&tsc=ImageBasicHover"));

                    string linkImage = linkCollection.First();

                    var filmBuilder = new FilmBuilder().SetName(nameFilm).DownloadPicture(linkImage, copyCount)
                        .ValidateLink();

                    OnFilm?.Invoke(filmBuilder.Film);
                });
            }
        }
    }
}
