using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Films.ViewModels
{
    public class INPC : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public ObservableCollection<string> ErrorsCollection
        {
            get => new ObservableCollection<string>(GetErrors(null).OfType<string>());
        }

        public bool HasErrors
        {
            get => GetErrors(null).OfType<object>().Any();
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public virtual IEnumerable GetErrors([CallerMemberName] string propertyName = null)
        {
            return Enumerable.Empty<object>();
        }

        protected virtual void OnErrorsChanged([CallerMemberName] string propertyName = null)
        {
            //HasErrors = GetErrors(propertyName).OfType<object>().Any();
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

    }
}
