using System;
using System.IO;
using System.Threading.Tasks;
using Films.Models.Web.HttpClients;
using Films.MVVMLogic.Models;

namespace Films.Models.ScrollFilms
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

            string downloadedPicturePath = string.Empty;
            //Скачиваю картинку
            Task.Run(async () =>
            {
                downloadedPicturePath = await _publicHttp.Download(link, appData + "/" + numberFilm + ".jpg");
            }).Wait();
            Film.Picture = downloadedPicturePath;

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
