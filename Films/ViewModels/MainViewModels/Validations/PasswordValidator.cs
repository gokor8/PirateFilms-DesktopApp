using System.Collections;

namespace Films.ViewModels.MainViewModels.Validations
{
    public class PasswordValidator : INPC
    {
        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
            }
        }

        public override IEnumerable GetErrors(string propertyName = null)
        {
            if (Password?.Length < 6)
                yield return "Пароль не может быть меньше 6 символов";
        }
    }
}