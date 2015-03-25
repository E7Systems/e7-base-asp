using log4net;
using System.Web.Mvc;
using System.Web.Security;
using web_mvc.ViewModels;

namespace web_mvc.Controllers
{
    //handles login.  This is the default route, so all unknown/unmatched url requests are redirected here.
    public class LoginController : Controller
    {
        private static readonly ILog m_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Index Action (Get)
        // GET: /login
        public ActionResult Index()
        {
            return View(new LoginIndex());
        } 
        #endregion

        #region Index Action (Post)
        [HttpPost]
        public ActionResult Index(LoginIndex form)
        {
            if (ModelState.IsValid)
            {
                //passed form validation, see if login credentials are valid
                string userName = form.UserName;
                string password = form.Password;


                bool isValidUser = Membership.ValidateUser(userName, password);
                m_logger.DebugFormat("Membership.ValidateUser result for userName: {0} = {1}", userName, isValidUser);

                if (isValidUser)
                {
                    bool isEndUser = Roles.IsUserInRole(userName, "enduser");
                    bool isAdmin = Roles.IsUserInRole(userName, "admin");
                    bool isSuperAdmin = Roles.IsUserInRole(userName, "superadmin");

                    m_logger.InfoFormat("Successful login for User: {0}, isEndUser:{1}, isAdmin: {2}, isSuperAdmin:{3}", userName, isEndUser, isAdmin, isSuperAdmin);
                    return RedirectToRoute("project");
                }
                else
                {
                    m_logger.DebugFormat("Invalid credentials for userName : {0}", userName);
                    ModelState.AddModelError("UserName", "Username or password is not correct.");
                    return View(form);
                }
            }
            else
            {
                //failed form validation, send back for another try
                m_logger.Debug("ModelState.IsValid=false");
                return View(form);
            }
        } 
        #endregion
    }
}