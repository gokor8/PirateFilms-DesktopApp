using System.Collections;
using System.Security;

namespace Films.ViewModels.MainViewModel.Validations
{
    public class PasswordValidator : INPC
    {
        public string Password { get; set; }

        public override IEnumerable GetErrors(string propertyName = null)
        {
            if (Password?.Length < 6)
                yield return "Пароль не может быть меньше 6 символов";
        }
    }
}