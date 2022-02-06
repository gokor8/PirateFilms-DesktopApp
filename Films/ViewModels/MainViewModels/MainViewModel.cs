using Films.Models.ScrollFilms;
using Films.ViewModels.MainViewModel;

namespace Films.ViewModels.MainViewModels
{
    class MainViewModel : INPC
    {
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

            AutorizationVm = new AuthorizationViewModel(_windowChanger);
            FilmViewModel = new FilmViewModel() {Name = "....."};
        }

        public AuthorizationViewModel AutorizationVm { get; }
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
