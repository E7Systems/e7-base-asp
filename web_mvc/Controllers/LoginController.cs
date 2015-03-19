using System.Web.Mvc;
using web_mvc.ViewModels;
using System.Web.Security;

namespace web_mvc.Controllers
{
    public class LoginController : Controller
    {
        // GET: /login
        public ActionResult Index()
        {
            return View(new LoginIndex());
        }

        [HttpPost]
        public ActionResult Index(LoginIndex form)
        {
            string userName = form.UserName;
            string password = form.Password;
            
            bool isValidUser = Membership.ValidateUser(userName, password);

            if (isValidUser)
            {
                return Content("passed top secret login process");
            }
            else
            {
                return Content("failed login");
            }
        }
    }
}