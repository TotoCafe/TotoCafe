using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        tbCompanyEmail.Attributes["onfocus"] = "focusTextBox(this)";
        tbCompanyEmail.Attributes["onblur"] = "blurTextBox(this)";
        tbCompanyPassword.Attributes["onfocus"] = "focusTextBox(this)";
        tbCompanyPassword.Attributes["onblur"] = "blurTextBox(this)";
    }
    protected void cmpLogin(object sender, EventArgs e)
    {
        Company cmp = new Company();

        cmp.Email = tbCompanyEmail.Text;
        cmp.Password = tbCompanyPassword.Text;

        if (cmp.Authenticate())
        {
            Session["Company"] = cmp;

            Response.Redirect("home2.aspx");
        }
        else
        {
            lblAuthenticate.Text = "Email ya da şifreyi yanlış girdiniz.";
            tbCompanyEmail.Attributes["style"] = "border-color: red";
            tbCompanyPassword.Attributes["style"] = "border-color: red";
        }
    }
}