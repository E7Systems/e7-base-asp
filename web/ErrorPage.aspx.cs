using System;
using System.Web.UI;

using log4net;

namespace Rsff.Web
{
    public partial class ErrorPage : Page
    {
        private static readonly ILog m_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Page_Load(object sender, EventArgs e)
        {
            m_logger.Debug("ErrorPage.aspx loading");
            lbFullError.Text = "<pre>\nAn error has occurred.  See logs for details.\n</pre>";
        }
    }
}