using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

public partial class _Default : System.Web.UI.Page
{
    private static readonly ILog m_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        m_logger.Debug("Default.aspx loading");
        //TEMPORARY remove this
        Response.Redirect("Projects.aspx");
    }
}