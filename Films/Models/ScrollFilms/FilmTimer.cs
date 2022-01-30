using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using Films.MVVMLogic.Models;

namespace Films.Models.ScrollFilms
{
    class FilmTimer
    {
        public delegate void CurrentFilmData(Film filmVM);
        public event CurrentFilmData OnData;

        private FilmParser _filmParser = new FilmParser();
        private ConcurrentBag<Film> _films = new ConcurrentBag<Film>();
        private int numberTick = default;

        public void StartTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();

            _filmParser.OnFilm += film => _films.Add(film);
            _ = Task.Run(async () => await _filmParser.CreateFilmsAsync());
            // Подписываемся на событие, когда скачивается картинка, и добавляем в список Объектов Image(В нем хранится название, и ссылка на файл картинки)
            // Запускаем парсинг фильмов, он парсит с сайта название фильма и ищет ему картинку, при успешном нахождении, скачивает, и уведомляет о новом, созданном Image

            timer.Tick += async (obj, e) => await OnTimerTickAsync();

            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();
        }

        private async Task OnTimerTickAsync()
        {
            for (int i = 0; i < 25; i++)
            {
                if ((_films.Count-1) >= numberTick + 1)
                    break;

                await Task.Delay(1);
            }

            var film = _films.ElementAtOrDefault(numberTick);

            numberTick = numberTick + 1 > (_films.Count - 1) ? 0 : numberTick + 1;

            //Оповещение MainViewModel о тике таймера, с новыми данными, которые он должен обновить во View
            OnData?.Invoke(film);
        }
    }
}
