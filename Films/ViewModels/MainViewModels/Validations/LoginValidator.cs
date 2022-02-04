using System.Collections;

namespace Films.ViewModels.MainViewModel.Validations
{
    public class LoginValidator : INPC
    {
        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
            }
        }

        public override IEnumerable GetErrors(string propertyName = null)
        {
            if (Login?.Length < 4)
                yield return "Логин не может быть меньше 4 символов";
        }
    }
}