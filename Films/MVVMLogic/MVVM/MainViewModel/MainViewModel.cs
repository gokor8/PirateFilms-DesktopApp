using Films.Classes.MVVM;
using Films.MVVMLogic.Models;

namespace Films.MVVMLogic.MVVM
{
    class MainViewModel : INPC
    {
        public MainViewModel()
        {
            FilmTimer timer = new FilmTimer();

            timer.OnData += (filmModel) => FilmViewModel = new FilmViewModel(){ Name=filmModel.Name, Picture = filmModel.Picture};
            timer.StartTimer();

            AutorizationVM = new AutorizationVm();
        }

        private AutorizationVm _autorizationVM { get; set; }
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
