using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    Boolean email = false, password = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        tbEmail.Attributes["onfocus"] = "focusEmail(this)";
        tbPassword.Attributes["onfocus"] = "focusPassword(this)";
    }
    protected void cmpLogin(object sender, EventArgs e)
    {
        Company cmp = new Company();

        cmp.Email = tbEmail.Text;
        cmp.Password = tbPassword.Text;

        if (cmp.Authenticate() && email && password)
        {
            Session["Company"] = cmp;

            Response.Redirect("home2.aspx");
        }
    }

    protected void validateEmail(object source, ServerValidateEventArgs args)
    {        
        if (tbEmail.Text == "" || tbEmail.Text == "Email")
        {
            args.IsValid = false;
            tbEmail.Text = "Email is required";
            tbEmail.ForeColor = Color.Red;
        }
        else
        {
            args.IsValid = true;
            email= true;
        }
    }
    protected void validatePassword(object source, ServerValidateEventArgs args)
    {
        if (tbPassword.Text == "" || tbPassword.Text == "Password")
        {
            args.IsValid = false;
            tbPassword.Text = "Password is required";
            tbPassword.ForeColor = Color.Red;
        }
        else
        {
            tbPassword.Attributes.Add("type", "password");
            args.IsValid = true;
            password = true;
        }
    }
}