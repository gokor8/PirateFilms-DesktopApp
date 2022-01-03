using Films.Classes.BingSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Films.Classes.MVVM
{
    class ScrollTimer
    {
        public delegate void CurrentFilmData(FilmModel viewmodel);
        public event CurrentFilmData GetData;

        private Images images;
        private List<Classes.Image> listImages;
        private int numberTick = 0;

        public VMFilms()
        {
            images = new Images();
            listImages = new List<Classes.Image>();
        }

        protected void StartScrollTimer()
        {
            images.TakeImage += (Classes.Image img) => listImages.Add(img);
            // Подписываемся на событие, когда скачивается картинка, и добавляем в массив Объектов Image(В нем хранится название, и ссылка на файл картинки)
            // Запускаем парсинг фильмов, он парсит с сайта название фильма и ищет ему картинку, при успешном нахождении, скачивает, и уведомляет о новом, созданном Image
            _ = Task.Run(async () => await images.ParsFilmsAndDownload());


            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(async (object obj, EventArgs e) =>
            {

                while (listImages.Count < numberTick + 1)
                    await Task.Delay(25);

                string currentfilmName = "";
                string currentfilmPicture;
                try
                {
                    currentfilmName = listImages[numberTick].FilmPicture;
                    currentfilmPicture = listImages[numberTick].FilmName;
                }
                catch (Exception)
                {
                    currentfilmName = listImages[numberTick].FilmName;
                    currentfilmPicture = listImages[numberTick = numberTick == 4 ? 0 : numberTick + 1].FilmPicture;
                }

                // Вызов event с именем и фильмом
                GetData?.Invoke(new FilmModel(currentfilmName, currentfilmPicture));

                numberTick = numberTick == 4 ? 0 : numberTick + 1;
            });

            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();
        }
    }
}
