using System.Security;
using System.Threading.Tasks;
using Films.Classes.MVVM.Buttons;
using System.Windows.Controls;
using System.Windows.Input;
using Films.MVVMLogic.Models;
using System.Threading;
using Films.Classes.MVVM;

namespace Films.MVVMLogic.MVVM
{
    internal class AutorizationVm : INPC
    {
        private CancellationTokenSource _singUpToken = new CancellationTokenSource();

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                if (value.Length <= 15)
                    _login = value;
                OnPropertyChanged();
            }
        }

        private SecureString _securePassword;

        public SecureString SecurePassword
        {
            private get => _securePassword;
            set
            {
                //if(value.Length <= 10)
                    _securePassword = value;
                //OnPropertyChanged();
            }
        }

        public ICommand SignUp => new DelegateCommand(async obj =>
                { 
                    var loginnerTask = await Task.Run(() =>
                        new LoginnerBuilder().VerifyLogin(Login).VerifyPassword(SecurePassword.ToString()).GetVerifyResult()
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
                    //System.Windows.MessageBox.Show("CALAM");
                });


        public ICommand SignByGhost => new DelegateCommand((obj) =>
               {
                   _singUpToken.Cancel();

                   System.Windows.MessageBox.Show("CALAM2");
               });

       /* private string GetPassword(object passwordObject)
        {
            var passwordBox = passwordObject as PasswordBox;
            if (passwordBox == null)
                return "";

            return passwordBox.Password;
        }*/
    }
}
