using Films.Models.Loginners;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTest.MVVMLogic_Test.Models_Test
{
    [TestClass]
    public class Loginner_Test
    {
        private LogginerBuilder _logginer = new LogginerBuilder();
        public Loginner_Test()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        
        [TestMethod]
        public void GetVerifyResult_true_verify_login_password()
        {
            var loginnerDataStore = _logginer.VerifyLogin("Krutoi").VerifyPassword("123456").GetVerifyResult();

            Assert.IsTrue(loginnerDataStore.IsVerify);
            Assert.AreEqual("", loginnerDataStore.AcscessString);
        }

        [TestMethod]
        [DataRow("NeKrutoi","336")]
        [DataRow("Krutoi","336228")]
        public void GetVerifyResult_false_verify_login_password(string login, string password)
        {
            var logginerDataStore = _logginer.VerifyLogin(login).VerifyPassword(password).GetVerifyResult();

            Assert.IsFalse(logginerDataStore.IsVerify);
            Assert.IsTrue(logginerDataStore.AcscessString.Contains("Неверный"));
        }
    }
}