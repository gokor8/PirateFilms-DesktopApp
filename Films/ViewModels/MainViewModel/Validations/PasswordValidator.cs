using System.Collections;

namespace Films.ViewModels.MainViewModel.Validations
{
    public class PasswordValidator : INPC
    {
        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                if (value.Length <= 25)
                    _password = value;
                OnPropertyChanged();
            }
        }

        public override IEnumerable GetErrors(string propertyName = null)
        {
            if (Password?.Length < 6)
                yield return "Пароль не может быть меньше 6 символов";
        }
    }
}