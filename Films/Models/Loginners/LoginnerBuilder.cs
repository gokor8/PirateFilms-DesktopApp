using System.Collections.Generic;
using System.Linq;
using System.Text;
using Films.MVVMLogic.Models.DataBaseLogic;

namespace Films.MVVMLogic.Models
{
    public class LoginnerBuilder
    {
        private readonly List<Loginner> _loginners = new List<Loginner>();
        protected void AddLogginer(string acscessString, bool isFound)
        {
            StringBuilder builder = new StringBuilder(acscessString);
            builder.Append("; ");

            if (isFound)
                builder.Clear();

            _loginners.Add(new Loginner() { AcscessString = builder.ToString(), IsVerify = isFound });
        }

        public LoginnerBuilder VerifyLogin(string login)
        {
            using (var context = new UserContext())
            {
                bool isFound = context.Users.FirstOrDefault(l=>l.Login == login) != null;
                AddLogginer("Неверный логин", isFound);
            }

            return this;
        }

        public LoginnerBuilder VerifyPassword(string password)
        {
            using (var context = new UserContext())
            {
                bool isFound = context.Users.FirstOrDefault(l => l.Password == password) != null;
                AddLogginer("Неверный пароль", isFound);
            }
            return this;
        }

        public Loginner GetVerifyResult()
        {
            Loginner currentDataStore = new Loginner();

            foreach (var logginer in _loginners)
            {
                currentDataStore.AcscessString += logginer.AcscessString;

                if (!currentDataStore.IsVerify)
                    currentDataStore.IsVerify = logginer.IsVerify;
            }

            return currentDataStore;
        }
    }
}