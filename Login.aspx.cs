using System;

public partial class Login : System.Web.UI.Page
{
    Validation validator = new Validation();
    bool confirm = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        tbEmail.Attributes["onfocus"] = "focusTextBox(this)";
        tbPassword.Attributes["onfocus"] = "focusPassword(this)";
        tbEmail.Attributes["onblur"] = "blurTextBox(this)";
        tbPassword.Attributes["onblur"] = "blurTextBox(this)";
    }
    protected void cmpLogin(object sender, EventArgs e)
    {
        if (!validator.checkCompanyEmail(tbEmail))
        {
            lblError.Text = "There is no company with this email address.";
            tbEmail.Text = "";
            tbPassword.Text = "";
            return;
        }
        validateLogin();
        Company cmp = new Company();

        cmp.Email = tbEmail.Text;
        cmp.Password = tbPassword.Text;
        bool authentication = cmp.Authenticate();
        if (authentication && confirm)
        {
            Session["Company"] = cmp;

            Response.Redirect("Home.aspx");
        }
    }
    private void validateLogin()
    {
        validator.validateEmail(tbEmail);
        validator.validatePassword(tbPassword);
        confirm = confirm && validator.Confirm;
    }
}