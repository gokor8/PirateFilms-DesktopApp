using System.Collections.Generic;
using System.Linq;
using System.Text;
using Films.MVVMLogic.Models.DataBaseLogic;

namespace Films.MVVMLogic.Models
{
    public class LoginnerBuilder
    {
        private readonly List<LoginnerDataStore> _dataStores = new List<LoginnerDataStore>();
        protected void DictonaryAdd(string acscessString, bool isFound)
        {
            StringBuilder builder = new StringBuilder(acscessString);
            builder.Append("; ");

            if (isFound)
                builder.Clear();

            _dataStores.Add(new LoginnerDataStore() { AcscessString = builder.ToString(), IsVerify = isFound });
        }

        public LoginnerBuilder VerifyLogin(string login)
        {
            using (var context = new UserContext())
            {
                bool isFound = context.Users.FirstOrDefault(l=>l.Login == login) != null;
                DictonaryAdd("Неверный логин", isFound);
            }
            return this;
        }

        public LoginnerBuilder VerifyPassword(string password)
        {
            using (var context = new UserContext())
            {
                bool isFound = context.Users.FirstOrDefault(l => l.Password == password) != null;
                DictonaryAdd("Неверный пароль", isFound);
            }
            return this;
        }

        public LoginnerDataStore GetVerifyResult()
        {
            LoginnerDataStore currentDataStore = new LoginnerDataStore();

            foreach (var dataStore in _dataStores)
            {
                currentDataStore.AcscessString += dataStore.AcscessString;

                if (currentDataStore.IsVerify)
                    currentDataStore.IsVerify = dataStore.IsVerify;
            }

            return currentDataStore;
        }
    }
}