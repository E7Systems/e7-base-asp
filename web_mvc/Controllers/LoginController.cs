using log4net;
using System.Web.Mvc;
using System.Web.Security;
using web_mvc.ViewModels;

namespace web_mvc.Controllers
{
    public class LoginController : Controller
    {
        private static readonly ILog m_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

                m_logger.DebugFormat("Membership.ValidateUser for userName: {0}", userName);

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