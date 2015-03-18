using System;
using Rsff.BusinessLayer;
using System.Collections.Generic;
using log4net;


public partial class Projects : System.Web.UI.Page
{
    private static readonly ILog m_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        m_logger.Debug("Projects.aspx loading");

    }
}