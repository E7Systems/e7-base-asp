using System;
using System.Web.Security;
using System.Web.UI;
using log4net;

namespace Rsff.Web
{
    public partial class Login : Page
    {
        private static readonly ILog m_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        protected void Page_Load(object sender, EventArgs e)
        {

            mtksLogin.Focus();
            if (!IsPostBack)
            {
                //Initialize Session Handling here?  Or after login?
                UpdateData();
            }            
        }


        protected void mtksLogin_LoggedIn( object sender, EventArgs e )
        {
            string userName = mtksLogin.UserName;
            bool isEndUser = Roles.IsUserInRole(userName,"enduser");
            bool isAdmin = Roles.IsUserInRole(userName, "admin");
            bool isSuperAdmin = Roles.IsUserInRole(userName, "superadmin");
            
            m_logger.InfoFormat("Successful login for User: {0}, isEndUser:{1}, isAdmin: {2}, isSuperAdmin:{3}", userName, isEndUser, isAdmin, isSuperAdmin);

            #region Not Used Right Now
            // const string URL_ADMIN_DEFAULT = "~/admin/admindefault.aspx";

            //if (userRole == UserRolesEnum.Admin)
            // {
            //     Response.Redirect(URL_ADMIN_DEFAULT);
            // }
            // else
            // {
            //     bool allowEndUserLogin = true;  
            //     if (!allowEndUserLogin) 
            //     {
            //         m_logger.DebugFormat("User '{0}' successfully authenticated, but end user login functionality prohibited.",userName);
            //         FormsAuthentication.SignOut();
            //         FormsAuthentication.RedirectToLoginPage();
            //     }
            // }    
            #endregion

        }

        public void UpdateData()
        {

            mtksLogin.UserNameLabelText = "Username";
            mtksLogin.UserNameRequiredErrorMessage = "User name is required";
            mtksLogin.PasswordLabelText = "Password";
            mtksLogin.PasswordRequiredErrorMessage = "Password is required";

        }
         
    }
}