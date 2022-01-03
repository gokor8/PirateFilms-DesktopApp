using Films.Classes.MVVM.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Films.MVVMLogic.MVVM
{
    class AutorizationViewModel
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
                    bool isSigned = new BSignUp(Login, GetPassword(obj)).TrySignUp();
                    if (isSigned)
                    {
                        //В основное окно идем епте
                    }
                    else
                    {
                        //Вылетает штука вверху, как в Fiddler, не верный лоджин или пароль
                    }
                    System.Windows.MessageBox.Show("CALAM");
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
