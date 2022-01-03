using Films.Classes.BingSearch;
using Films.Classes.MVVM.Buttons;
using System.Windows.Input;
using System.Windows.Controls;
using Films.MVVMLogic.Models;
using Films.MVVMLogic.MVVM;

namespace Films.Classes.MVVM
{
    class MainViewModel : INPC
    {
        public MainViewModel()
        {
            //InitializeTimer по правилу mvvm должен быть тут, но т.к MainViewModel инициализируется 2 раза, делаем костыль, запускаем во View
        }

        public void InitializeTimer()
        {
            FilmTimer timer = new FilmTimer();

            timer.OnData += (filmModel) => FilmViewModel = filmModel;
            timer.StartTimer();

            AutorizationVM = new AutorizationViewModel();
        }

        private AutorizationViewModel _autorizationVM { get; set; }
        public AutorizationViewModel AutorizationVM
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
