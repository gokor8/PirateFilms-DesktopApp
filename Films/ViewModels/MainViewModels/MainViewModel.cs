using Films.Models.ScrollFilms;
using Films.ViewModels.MainViewModel;

namespace Films.ViewModels.MainViewModels
{
    class MainViewModel : INPC
    {
        private FilmTimer timer;
        private IWindowChanger _windowChanger = new FilmWindowChanger();
        
        public MainViewModel()
        {
            timer = new FilmTimer();

            timer.OnData += (filmModel) =>
            {
                if (filmModel != null)
                    FilmViewModel = new FilmViewModel() {Name = filmModel.Name, Picture = filmModel.Picture};
            };
            timer.StartTimer();

            AutorizationVM = new AutorizationVm(_windowChanger);
            FilmViewModel = new FilmViewModel() {Name = "....."};
        }

        private AutorizationVm _autorizationVM;
        public AutorizationVm AutorizationVM
        {
            get => _autorizationVM;
            set
            {
                _autorizationVM = value;
                //OnPropertyChanged();
            }
        }

        private FilmViewModel filmViewModel;

        public FilmViewModel FilmViewModel
        {
            get => filmViewModel;
            set
            {
                filmViewModel = value;
                OnPropertyChanged();
            }
        }
    }
}
