using System;
using log4net;
using System.Web;
using System.Reflection;

namespace Rsff.Web
{
    /// <summary>
    /// Global initialization.  Sets up logging and a last chance exception handler.
    /// </summary>
    public partial class Global : System.Web.HttpApplication
    {

        #region Application_Start
        protected void Application_Start(Object sender, EventArgs e)
        {
            //warning:  configuring logging must be the 1st statement in the app.  No exceptions.
            log4net.Config.XmlConfigurator.Configure();
            ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.InfoFormat("Application Started, version is {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            logger.InfoFormat("All rights reserved.  This log is for internal use ONLY.");
            logger.DebugFormat("Config file: {0}", AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            const string RELATIVE_PATH_TO_WEB_BIN_FOLDER = @"~/Bin";
            string pathToBinFolder = System.Web.HttpContext.Current.Server.MapPath(RELATIVE_PATH_TO_WEB_BIN_FOLDER);
            logger.DebugFormat("bin folder path: {0}", pathToBinFolder);
        } 
        #endregion

        #region Application_End
        protected void Application_End(Object sender, EventArgs e)
        {
            ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.Info("Application ended");
        } 
        #endregion

        #region Application_Error
        //last chance exception handler
        protected void Application_Error(Object sender, EventArgs e)
        {
            ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            try
            {
                //log the error right away so it doesn't get lost
                Exception exc = Server.GetLastError();
                Server.ClearError();
                logger.Error("[Global] Application_Error : Unhandled application error. ", exc);

                logger.DebugFormat("Application_Error.   Request.Path is  :{0}, CurrentExecutionFilePath is : {1}", HttpContext.Current.Request.Path, HttpContext.Current.Request.CurrentExecutionFilePath);

                if (HttpContext.Current.Request.Path.IndexOf("ErrorPage.aspx") < 0)
                {

                    const string URL_ERROR_PAGE = "~/ErrorPage.aspx";
                    if (HttpContext.Current.Session != null)
                    {
                        logger.DebugFormat("Session not null, SessionID is :{0}.  Redirecting to :{1}", HttpContext.Current.Session.SessionID, URL_ERROR_PAGE);
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.Redirect(URL_ERROR_PAGE, true);
                        HttpContext.Current.Response.End();
                    }
                    else
                    {
                        logger.DebugFormat("Session null, redirecting to :{0}", URL_ERROR_PAGE);
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Server.Transfer(URL_ERROR_PAGE);
                        HttpContext.Current.Response.End();
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error("Exception in Application_Error.", ex);
            }
        }
        
        #endregion

        #region Session_Start
        protected void Session_Start(Object sender, EventArgs e)
        {
            ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.DebugFormat("[Global] Session_Start fired.  Session ID is {0}", HttpContext.Current.Session.SessionID);
        } 
        #endregion

        #region Session_End
        //this only fires if session is inproc.
        protected void Session_End(Object sender, EventArgs e)
        {
            ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.DebugFormat("[Global] Session_End fired.");
        } 
        #endregion

        #region Application_BeginRequest (not used)
        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        } 
        #endregion


    }
}
