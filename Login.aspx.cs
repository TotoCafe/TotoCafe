using System;

public partial class Login : System.Web.UI.Page
{
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
        validateFunction();
        Company cmp = new Company();

        cmp.Email = tbEmail.Text;
        cmp.Password = tbPassword.Text;
        bool authentication = cmp.Authenticate();
        if (authentication && confirm)
        {
            Session["Company"] = cmp;

            Response.Redirect("home2.aspx");
        }
        if (!authentication)
        {
            lblAuthenticate.Text = "Wrong email or password.";
        }
    }
    private void validateFunction()
    {
        Validation validator = new Validation();
        validator.validateEmail(tbEmail);
        validator.validatePassword(tbPassword);
        confirm = confirm && validator.Confirm;
    }
}