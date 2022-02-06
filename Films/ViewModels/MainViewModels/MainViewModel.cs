using Films.Models.ScrollFilms;

namespace Films.ViewModels.MainViewModels
{
    class MainViewModel : INPC
    {
        // Оставляю полем, если нужно будет отписаться от ивента
        private readonly FilmTimer _timer;
        private readonly IWindowChanger _windowChanger = new FilmWindowChanger();
        
        private FilmViewModel _filmViewModel;
        
        public MainViewModel()
        {
            _timer = new FilmTimer();

            _timer.OnData += (filmModel) =>
            {
                if (filmModel != null)
                    FilmViewModel = new FilmViewModel() {Name = filmModel.Name, Picture = filmModel.Picture};
            };
            _timer.StartTimer();

            AutorizationViewModel = new AuthorizationViewModel(_windowChanger);
            FilmViewModel = new FilmViewModel() {Name = "....."};
        }

        public AuthorizationViewModel AutorizationViewModel { get; }
        public FilmViewModel FilmViewModel
        {
            get => _filmViewModel;
            private set
            {
                _filmViewModel = value;
                OnPropertyChanged();
            }
        }
    }
}
