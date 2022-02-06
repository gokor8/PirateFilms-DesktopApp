using System.Collections.Generic;
using System.Linq;
using System.Text;
using Films.Models.DataBaseLogic.UserDataBse;
using Films.MVVMLogic.Models;

namespace Films.Models.Loginners
{
    public class LogginerBuilder
    {
        private readonly List<Logginer> _logginers = new List<Logginer>();
        protected void AddLogginer(string acscessString, bool isFound)
        {
            StringBuilder builder = new StringBuilder(acscessString);
            builder.Append("; ");

            if (isFound)
                builder.Clear();

            _logginers.Add(new Logginer() { AcscessString = builder.ToString(), IsVerify = isFound });
        }

        public LogginerBuilder VerifyLogin(string login)
        {
            using (var context = new UserContext())
            {
                bool isFound = context.Users.FirstOrDefault(l=>l.Login == login) != null;
                AddLogginer("Неверный логин", isFound);
            }

            return this;
        }

        public LogginerBuilder VerifyPassword(string password)
        {
            using (var context = new UserContext())
            {
                bool isFound = context.Users.FirstOrDefault(l => l.Password == password) != null;
                AddLogginer("Неверный пароль", isFound);
            }
            return this;
        }

        public Logginer GetVerifyResult()
        {
            Logginer currentDataStore = new Logginer();

            foreach (var logginer in _logginers)
            {
                currentDataStore.AcscessString += logginer.AcscessString;

                if (currentDataStore.IsVerify)
                    currentDataStore.IsVerify = logginer.IsVerify;
            }

            return currentDataStore;
        }
    }
}