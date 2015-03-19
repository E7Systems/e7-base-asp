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
            if (ModelState.IsValid)
            {
                //passed validation, see if login credentials are valid
                string userName = form.UserName;
                string password = form.Password;

                bool isValidUser = Membership.ValidateUser(userName, password);

                if (isValidUser)
                {
                    return RedirectToRoute("projects");
                }
                else
                {
                    ModelState.AddModelError("UserName", "Username or password is not correct.");
                    return View(form);
                }
            }
            else
            {
                //failed validation, send back for another try
                return View(form);
            }
        }
    }
}