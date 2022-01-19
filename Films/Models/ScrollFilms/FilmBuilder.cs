using System;
using System.IO;
using Films.Web.HttpClients;

namespace Films.MVVMLogic.Models.ImagesScroll
{
    public class FilmBuilder
    {
        public Film Film { get; private set; } = new Film();

        private readonly PublicHttp _publicHttp = PublicHttp.GetInstance();

        public FilmBuilder SetName(string name)
        {
            if (name != "" || name != " ")
                Film.Name = name;
            else
                return null;

            return this;
        }
        public FilmBuilder DownloadPicture(string link, int numberFilm)
        {
            // Создаю Папку приложения в %AppData%
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\AMFilms";
            Directory.CreateDirectory(appData);

            //Скачиваю картинку
            var downloadTask = _publicHttp.Download(link, appData + "/" + numberFilm + ".jpg");
            downloadTask.Wait();
            Film.Picture = downloadTask.Result;

            return this;
        }
        public FilmBuilder ValidateLink()
        {
            if (Film.Picture.Length < 3)
                return null;

            return this;
        }
    }
}
