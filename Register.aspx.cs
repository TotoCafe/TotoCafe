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
        tbCompanyName.Attributes["onfocus"] = "focusTextBox(this)";
        tbCompanyName.Attributes["onblur"] = "blurTextBox(this)";
        tbCompanyEmail.Attributes["onfocus"] = "focusTextBox(this)";
        tbCompanyEmail.Attributes["onblur"] = "blurTextBox(this)";
        tbCompanyPassword.Attributes["onfocus"] = "focusTextBox(this)";
        tbCompanyPassword.Attributes["onblur"] = "blurTextBox(this)";
        tbCompanyAddress.Attributes["onfocus"] = "focusTextBox(this)";
        tbCompanyAddress.Attributes["onblur"] = "blurTextBox(this)";
        tbCompanyPhone.Attributes["onfocus"] = "focusTextBox(this)";
        tbCompanyPhone.Attributes["onblur"] = "blurTextBox(this)";
    }
    protected void cmpRegister(object sender, EventArgs e)
    {
        Company cmp = new Company();

        cmp.CompanyName = tbCompanyName.Text;
        cmp.Email = tbCompanyEmail.Text;
        cmp.Password = tbCompanyPassword.Text;
        cmp.Address = tbCompanyAddress.Text;
        cmp.Phone = tbCompanyPhone.Text;
        cmp.CityID = int.Parse(ddlCity.SelectedValue);

        if (cmp.SignUp())
        {
            Session["Company"] = cmp;

            Response.Redirect("home2.aspx");
        }
    }
}