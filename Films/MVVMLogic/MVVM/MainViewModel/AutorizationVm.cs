using Films.Classes.MVVM.Buttons;
using System.Windows.Controls;
using System.Windows.Input;
using Films.MVVMLogic.Models;

namespace Films.MVVMLogic.MVVM
{
    internal class AutorizationVm
    {
        private string login;
        public string Login
        {
            get => login;
            set
            {
                login = value;
            }
        }

        public ICommand SignUp => new DelegateCommand(obj =>
                {
                    // напсиать юнит тест
                    var loginnerDataStore = new Loginner().VerifyLogin(Login).VerifyPassword(GetPassword(obj)).GetVerifyResult();
                    if (loginnerDataStore.IsVerify)
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
                   // Залетаем в новое окно
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
