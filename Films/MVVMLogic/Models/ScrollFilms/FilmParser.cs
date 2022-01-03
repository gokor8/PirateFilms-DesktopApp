using System;
using System.Threading.Tasks;
using System.Windows;
using AngleSharp;
using Films.Classes.MVVM;
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

                    string linkImage = await bing.GetLink(nameFilm, new ImageElement("&qft=+filterui:imagesize-custom_1000_1000&first=1&tsc=ImageBasicHover"));

                    try
                    {
                        var filmBuilder = new FilmBuilder().SetName(nameFilm).DownloadPicture(linkImage, copyCount)
                            .ValidateLink();

                        OnFilm.Invoke(filmBuilder.Film);
                    }
                    catch(Exception exc)
                    {
                        MessageBox.Show($"Плохая ссылка в |Images 49| {linkImage}" + exc.StackTrace);
                    }
                });
            }
            /*int time = Convert.ToInt32(3 + "000"); 
                     * Thread.Sleep(Convert.ToInt32( 3 + "000"));
                     * Чтобы проверить парралельность, расскоментировать строки выше
                     * Т.к картинка встает 3 секунды, а те картинки скачиваются быстро,
                     может показаться, что параллельности нету, но она есть */

            // ЕСЛИ НАЗВАНИЕ ДЛИННОЕ, ВЗЯТЬ ДРУГОЙ ФИЛЬМ, ПОКА ОНО НЕ БУДЕТ КОРОТКИМ, foreach НЕ ПОДОЙДЕТ, Т.К continue СДЕЛАЕТ ДАЛЬШЕ, И БУДЕТ ФИЛЬМОВ НЕ 5, А 4
        }
    }
}
