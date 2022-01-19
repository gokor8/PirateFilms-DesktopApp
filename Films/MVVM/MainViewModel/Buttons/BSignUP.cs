using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films.Classes.MVVM.Buttons
{
    class BSignUp
    {
        private string userLogin;
        private string userPassword;
        public BSignUp(string login, string password)
        {
            userLogin = login;
            userPassword = password;
        }

        public bool TrySignUp()
        {
            return false;
        }
    }
}
