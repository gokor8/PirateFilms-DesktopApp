using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Films.Classes.MVVM;
using Films.Classes.MVVM.Buttons;
using Films.MVVMLogic.Models;
using Films.ViewModels.MainViewModel.Validations;

namespace Films.ViewModels.MainViewModel
{
    internal class AutorizationVm : INPC
    {
        private CancellationTokenSource _singUpToken = new CancellationTokenSource();

        public AutorizationVm()
        {
            LoginValidator = new LoginValidator();
            PasswordValidator = new PasswordValidator();
        }

        public LoginValidator LoginValidator { get; set; }

        public PasswordValidator PasswordValidator { get; set; }

        public ICommand SignUp => new DelegateCommand(async obj =>
        {
            _singUpToken = new CancellationTokenSource();

            var loginnerTask = await Task.Run(() =>
                    new LoginnerBuilder().VerifyLogin(LoginValidator.Login)
                        .VerifyPassword(PasswordValidator.Password).GetVerifyResult()
                , _singUpToken.Token);

            if (loginnerTask.IsVerify)
            {
                //В основное окно идем епте
                System.Windows.MessageBox.Show("Logged");
            }
            else
            {
                //Вылетает штука вверху, как в Fiddler, не верный лоджин или пароль
                System.Windows.MessageBox.Show("Dont Logged");
            }
        }, obg => (!LoginValidator.HasErrors && !PasswordValidator.HasErrors) && 
                  (LoginValidator.Login?.Length != null && PasswordValidator.Password?.Length != null));

        
        public ICommand SignByGhost => new DelegateCommand((obj) =>
        {
            _singUpToken.Cancel();

            System.Windows.MessageBox.Show("CALAM2");
        });
    }
}
