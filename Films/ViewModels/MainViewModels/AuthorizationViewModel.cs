using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Films.Models.Loginners;
using Films.ViewModels.MainViewModel.Validations;

namespace Films.ViewModels.MainViewModels
{
    internal class AuthorizationViewModel : INPC
    {
        private CancellationTokenSource _singUpToken = new CancellationTokenSource();
        private readonly IWindowChanger _windowChanger;

        private string _badSignIn;
        
        public AuthorizationViewModel(IWindowChanger windowChanger)
        {
            _windowChanger = windowChanger;
            
            LoginValidator = new LoginValidator();
            PasswordValidator = new PasswordValidator();
        }

        public LoginValidator LoginValidator { get; }

        public PasswordValidator PasswordValidator { get; }

        
        public string BadSignIn
        {
            get => _badSignIn; 
            set
            {
                _badSignIn = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand SignUp => new DelegateCommand(async obj =>
        {
            _singUpToken = new CancellationTokenSource();

            var loginnerTask = await Task.Run(() =>
                    new LoginnerBuilder().VerifyLogin(LoginValidator.Login)
                        .VerifyPassword(PasswordValidator.Password).GetVerifyResult()
                , _singUpToken.Token);

            if (loginnerTask.IsVerify)
            {
                _windowChanger.SetTransferAttribute(LoginValidator.Login);
                _windowChanger.CloseAndOpen();
            }
            else
            {
                BadSignIn = loginnerTask.AcscessString;
                //Вылетает штука вверху, как в Fiddler, не верный лоджин или пароль
                System.Windows.MessageBox.Show("Dont Logged");
            }
        }, obg => (!LoginValidator.HasErrors && !PasswordValidator.HasErrors) && 
                  (LoginValidator.Login?.Length != null && PasswordValidator.Password?.Length != null));

        
        public ICommand SignByGhost => new DelegateCommand((obj) =>
        {
            _singUpToken.Cancel();
            
            _windowChanger.SetTransferAttribute("Гость");
            _windowChanger.CloseAndOpen();
        });
    }
}
