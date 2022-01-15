using System.Threading.Tasks;
using Films.Classes.MVVM.Buttons;
using System.Windows.Controls;
using System.Windows.Input;
using Films.MVVMLogic.Models;
using System.Threading;
using Films.Classes.MVVM;

namespace Films.MVVMLogic.MVVM
{
    internal class AutorizationVm
    {
        private CancellationTokenSource _singUpToken = new CancellationTokenSource();

        private string login;
        public string Login
        {
            get => login;
            set
            {
                login = value; 
            }
        }

        public ICommand SignUp => new DelegateCommand(async obj =>
                { 
                    var loginnerTask = await Task.Run(() =>
                        new LoginnerBuilder().VerifyLogin(Login).VerifyPassword(GetPassword(obj)).GetVerifyResult()
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

        private string GetPassword(object passwordObject)
        {
            var passwordBox = passwordObject as PasswordBox;
            if (passwordBox == null)
                return "";

            return passwordBox.Password;
        }
    }
}
