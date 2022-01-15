using Films.MVVMLogic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTest.MVVMLogic_Test.Models_Test
{
    [TestClass]
    public class Loginner_Test
    {
        private LoginnerBuilder _loginner = new LoginnerBuilder();

        [TestMethod]
        public void GetVerifyResult_true_verify_login_password()
        {
            var loginnerDataStore = _loginner.VerifyLogin("Krutoi").VerifyPassword("228").GetVerifyResult();

            Assert.IsTrue(loginnerDataStore.IsVerify);
            Assert.Equals("", loginnerDataStore);
        }

        [TestMethod]
        public void GetVerifyResult_false_verify_login_password()
        {
            var loginnerDataStore = _loginner.VerifyLogin("NeKrutoi").VerifyPassword("336").GetVerifyResult();

            Assert.IsFalse(loginnerDataStore.IsVerify);
            Assert.Equals("Неверный логин; Неверный пароль;", loginnerDataStore);
        }
    }
}