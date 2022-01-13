using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Films.Classes.MVVM
{
    public class INPC : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
