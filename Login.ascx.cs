using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           Company cmp = (Company)Session["Company"];

            if (cmp != null)
                Response.Redirect("Home.aspx");
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Authentication(txtLoginMail.Text, txtLoginPassword.Text))
        {
            Company cmp = new Company();

            cmp.Initialize(txtLoginMail.Text);

            Session["Company"] = cmp;

            Response.Redirect("Home.aspx");
        }
        else
        {
            //IF USER AUTHAENTICATION RETURNS FALSE SHOW USER A MESSAGE THAT SAYS E-MAIL OR PASSWORD NOT CORRECT..
        }
    }

    public bool Authentication(string Email, string Password)
    {
        bool result = true;
        string encPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "SHA1");
        string encEmail = FormsAuthentication.HashPasswordForStoringInConfigFile(Email, "SHA1");

        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;
        cmd.CommandText = "SELECT COUNT(*) FROM Company WHERE Email = @Email AND Password = @Password";
        cmd.Parameters.AddWithValue("@Email", encEmail);
        cmd.Parameters.AddWithValue("@Password", encPassword);

        try
        {
            conn.Open();

            if (int.Parse(cmd.ExecuteScalar().ToString()) == 0)
                result = false;
        }
        catch (Exception) { result = false; }
        finally
        {
            conn.Close();
        }
        return result;
    }
}