using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    Validator validator = new Validator();
    bool confirmLogin = true;
    bool confirmRegister = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["Company"] != null)
        {
            Response.Redirect("home.aspx");
        }
    }
    protected void cmpLogin(object sender, EventArgs e)
    {
        if (!validator.checkCompanyEmail(loginEmail))
        {
            loginEmail.Text = null;
            loginEmail.Attributes["placeholder"] = "Invalid Email Address";
            loginEmail.Focus();
            return;
        }
        validateLogin();
        Company cmp = new Company();

        cmp.Email = loginEmail.Text;
        cmp.Password = loginPassword.Text;
        bool authentication = cmp.Authenticate();
        if (authentication && confirmLogin)
        {
            Session["Company"] = cmp;

            Response.Redirect("Home.aspx");
        }
    }
    protected void cmpRegister(object sender, EventArgs e)
    {
        Company cmp = new Company();
        bool signed = false;
        if (validator.checkCompanyEmail(registerEmail))
        {
            registerEmail.Attributes["placeholder"] = "This mail address is in use.";
            return;
        }
        validateRegister();
        if (confirmRegister)
        {
            cmp.CompanyName = registerCompanyName.Text;
            cmp.Email = registerEmail.Text;
            cmp.Password = registerPassword.Text;
            cmp.Address = registerAddress.Text;
            cmp.Phone = registerPhone.Text;
            cmp.CityID = int.Parse(registerCity.SelectedValue);
            signed = cmp.SignUp();
        }

        if (signed)
        {
            Session["Company"] = cmp;

            Response.Redirect("Home.aspx");
        }
    }
    private void validateLogin()
    {
        validator.validateEmail(loginEmail);
        validator.validatePassword(loginPassword);
        confirmLogin = confirmLogin && validator.permission;
    }
    private void validateRegister()
    {
        foreach (TextBox tb in register.Controls.OfType<TextBox>())
        {
            if (tb.ID.Replace("register", "") == "Email")
                validator.validateEmail(tb);
            else if (tb.ID.Replace("register", "") == "Password")
                validator.validatePassword(tb);
            else
                validator.validateTextBox(tb);
            confirmRegister = confirmRegister && validator.permission;
        }
    }
}