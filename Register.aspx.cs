using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : Page
{
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
        validateFunction();
        Company cmp = new Company();

        cmp.CompanyName = tbName.Text;
        cmp.Email = tbEmail.Text;
        cmp.Password = tbPassword.Text;
        cmp.Address = tbAddress.Text;
        cmp.Phone = tbPhone.Text;
        cmp.CityID = int.Parse(ddlCity.SelectedValue);

        if (cmp.SignUp() && confirm)
        {
            Session["Company"] = cmp;

            Response.Redirect("home2.aspx");
        }
    }

    private void validateFunction()
    {
        Validation validator = new Validation();
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