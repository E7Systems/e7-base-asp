using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace web_mvc.Tests.Controller
{
    [TestClass]
    public class TestLoginController
    {
        [TestMethod]
        public void TestLoginControllerIndex()
        {
            web_mvc.Controllers.LoginController loginController = new Controllers.LoginController();
            ActionResult result = loginController.Index();
            Assert.IsNotNull(result);

        }
    }
}
