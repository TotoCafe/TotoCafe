using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : Page
{
    Validation validator = new Validation();
    private List<TextBox> textBoxes = new List<TextBox>();
    private bool confirm = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        initializeTextBoxes();

        tbPassword.Attributes["onfocus"] = "focusPassword(this)";
        tbRePassword.Attributes["onfocus"] = "focusPassword(this)";
        tbRePassword.Attributes["onkeyup"] = "checkPassword(this)";
    }
    protected void cmpRegister(object sender, EventArgs e)
    {
        Company cmp = new Company();
        bool signed = false;
        if (validator.checkCompanyEmail(tbEmail))
        {
            lblError.Text = "This mail address is in use.";
            return;
        }
        validateFunction();
        if (confirm)
        {
            cmp.CompanyName = tbName.Text;
            cmp.Email = tbEmail.Text;
            cmp.Password = tbPassword.Text;
            cmp.Address = tbAddress.Text;
            cmp.Phone = tbPhone.Text;
            cmp.CityID = int.Parse(ddlCity.SelectedValue);
            signed = cmp.SignUp();
        }

        if (signed)
        {
            Session["Company"] = cmp;

            Response.Redirect("home2.aspx");
        }
    }

    private void validateFunction()
    {

        foreach (TextBox tb in textBoxes)
        {
            if (tb.ID.Replace("tb", "") == "Email")
                validator.validateEmail(tb);
            else if (tb.ID.Replace("tb", "") == "Password" || tb.ID.Replace("tb", "") == "RePassword")
                validator.validatePassword(tb);
            else
                validator.validateTextBox(tb);
            confirm = confirm && validator.Confirm;
        }
    }
    private void initializeTextBoxes()
    {
        TextBox tb = new TextBox();
        foreach (Control control in form1.Controls)
        {
            if (control.GetType() == typeof(TextBox))
            {
                tb = (TextBox)control;
                tb.Attributes["onfocus"] = "focusTextBox(this)";
                tb.Attributes["onblur"] = "blurTextBox(this)";
                textBoxes.Add(tb);
            }
        }
    }
}