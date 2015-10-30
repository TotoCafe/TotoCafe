using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

        if (authCookie == null) 
            Response.Redirect("Index.aspx");
        else
        {
            DataView dv = (DataView)SqldataCheckDatabase.Select(DataSourceSelectArguments.Empty);
            DataRowView drv = dv[0];
       
            int dID = Convert.ToInt32(drv["dID"]);

            if (dID == 0)
            {
                panelFirst.Visible = true;
            }
            else
            {
                panelDefault.Visible = true;
            }

        }

        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

        string companyEmail = ticket.Name;

    }
    
}