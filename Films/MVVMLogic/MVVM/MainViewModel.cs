using Films.Classes.MVVM;
using Films.MVVMLogic.Models;
using Films.Web.BingSearch;

namespace Films.MVVMLogic.MVVM
{
    class MainViewModel : INPC
    {
        public MainViewModel()
        {
            FilmTimer timer = new FilmTimer();

            timer.OnData += (filmModel) => FilmViewModel = filmModel;
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
                OnPropertyChanged();
            }
        }

        private Film filmViewModel;
        public Film FilmViewModel
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
