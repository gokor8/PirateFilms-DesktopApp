using Films.MVVMLogic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTest.MVVMLogic_Test.Models_Test
{
    [TestClass]
    public class Loginner_Test
    {
        public Loginner_Test()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        private LoginnerBuilder _loginner = new LoginnerBuilder();

        [TestMethod]
        public void GetVerifyResult_true_verify_login_password()
        {
            var loginnerDataStore = _loginner.VerifyLogin("Krutoi").VerifyPassword("225").GetVerifyResult();

            Assert.IsTrue(loginnerDataStore.IsVerify);
            Assert.AreEqual("", loginnerDataStore.AcscessString);
        }

        [TestMethod]
        public void GetVerifyResult_false_verify_login_password()
        {
            var loginnerDataStore = _loginner.VerifyLogin("NeKrutoi").VerifyPassword("336").GetVerifyResult();

            Assert.IsFalse(loginnerDataStore.IsVerify);
            Assert.AreEqual("Неверный логин; Неверный пароль; ", loginnerDataStore.AcscessString);
        }
    }
}