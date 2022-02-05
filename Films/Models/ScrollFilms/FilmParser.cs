using System;
using System.Linq;
using System.Threading.Tasks;
using Films.Models.Web.BingSearch;
using Films.Models.Web.BingSearch.Factories;
using Films.Models.Web.HttpClients;
using Films.Models.Web.Parsers;
using Films.MVVMLogic.Models;

namespace Films.Models.ScrollFilms
{
    public class FilmParser
    {
        private SiteFilmsHttp _siteHttp;

        public event CorrectFilmBuilder OnFilm;
        public delegate void CorrectFilmBuilder(Film film);

        public async Task CreateFilmsAsync()
        {
            _siteHttp = await SiteFilmsHttp.GetInstanceAsync();
            Bing bing = new Bing();
            
            string html = await _siteHttp.Client.GetStringAsync(_siteHttp.Client.BaseAddress);
            var filmNamesCollection = await new NamesPreviewLordfilmParser().GetFilms(html).Take(5).ToListAsync();

            for (int countFilms = 0; countFilms < filmNamesCollection.Count; countFilms++) 
            {
                int copyCount = countFilms;

                //Вывожу в отдельную задачу, так быстрее
                _ = Task.Run(async () =>
                {
                    string nameFilm = filmNamesCollection.ElementAt(copyCount);

                    if (nameFilm == null)
                        throw new NullReferenceException("Ало, аче имени у фильма нету, сайт не найден судя по всему");

                    IBingFactory factory = new ImageBingFactory();

                    string bingSearchHtml = await bing.GetSearchResultAsync(nameFilm.Contains(" фильм")? nameFilm : nameFilm+" фильм",
                        factory.CreateBingSettings("&qft=+filterui%3aimagesize-custom_1000_1000&first=1&tsc=ImageBasicHover"));

                    string linkImage = await factory.CreateBingParser().GetWorkingLinksAsync(bingSearchHtml).FirstAsync();

                    var filmBuilder = new FilmBuilder().SetName(nameFilm)?.DownloadPicture(linkImage, copyCount)
                        .ValidateLink();

                    OnFilm?.Invoke(filmBuilder.Film);
                });
            }
        }
    }
}
