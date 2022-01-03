using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Threading;
using Films.MVVMLogic.Models.ImagesScroll;

namespace Films.MVVMLogic.Models
{
    class FilmTimer
    {
        public delegate void CurrentFilmData(Film filmVM);
        public event CurrentFilmData OnData;

        private FilmParser _filmParser= new FilmParser();
        private List<Film> films = new List<Film>();
        private int numberTick = 0;

        public void StartTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();

            _filmParser.OnFilm += film => films.Add(film);
            _ = Task.Run(async () => await _filmParser.CreatingFilms());
            // Подписываемся на событие, когда скачивается картинка, и добавляем в список Объектов Image(В нем хранится название, и ссылка на файл картинки)
            // Запускаем парсинг фильмов, он парсит с сайта название фильма и ищет ему картинку, при успешном нахождении, скачивает, и уведомляет о новом, созданном Image
            timer.Tick += async (object obj, EventArgs e) => await OnTimerTick();

            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();
        }

        private async Task OnTimerTick()
        {
            while (films.Count < numberTick + 1)
                await Task.Delay(100);

            var film = films[numberTick];

            numberTick = numberTick == 4 ? 0 : numberTick + 1;
            //Оповещение MainViewModel о тике таймера, с новыми данными, которые он должен обновить во View
            OnData?.Invoke(film);
        }
    }
}
