using log4net;
using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace web_mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            #region log4net Initialization - Must be 1st thing in App
            //warning:  configuring logging must be the 1st statement in the app.  No exceptions.
            log4net.Config.XmlConfigurator.Configure();
            ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.InfoFormat("Application Started, version is {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            logger.Info("Copyright 2015.  All rights reserved.  This log is for internal use ONLY.");
            logger.DebugFormat("Config file: {0}", AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            const string RELATIVE_PATH_TO_WEB_BIN_FOLDER = @"~/Bin";
            string pathToBinFolder = System.Web.HttpContext.Current.Server.MapPath(RELATIVE_PATH_TO_WEB_BIN_FOLDER);
            logger.DebugFormat("bin folder path: {0}", pathToBinFolder); 
            #endregion

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //Scripts registering ok, Styles not taking, even though it is configured correctly.  TODO, figure out why.
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_End()
        {
            ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.InfoFormat("Application Ended, version is {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }
    }
}
