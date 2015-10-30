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
    string selectQuery = "SELECT COUNT(*) FROM Company WHERE Email = @Email AND Password = @Password";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region Login
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Authentication(txtLoginMail.Text, txtLoginPassword.Text))
        {

            FormsAuthentication.SetAuthCookie(txtLoginMail.Text, false);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                                                                            1,
                                                                            txtLoginMail.Text,
                                                                            DateTime.Now,
                                                                            DateTime.Now.AddDays(7),
                                                                            false,
                                                                            "HR"
                                                                            );

            HttpCookie cookie = new HttpCookie(
                                               FormsAuthentication.FormsCookieName,
                                               FormsAuthentication.Encrypt(ticket)
                                               );
            Response.Cookies.Add(cookie);
            Response.Redirect("Home.aspx");

        }
        else
        {
            //IF USER AUTHAENTICATION RETURNS FALSE SHOW USER A MESSAGE THAT SAYS E-MAIL OR PASSWORD NOT CORRECT..
        }
    }
    #endregion

    public bool Authentication(string Email, string Password)
    {
        bool result = true;
        string encPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "SHA1");
        string encEmail = FormsAuthentication.HashPasswordForStoringInConfigFile(Email, "SHA1");

        SqlConnection conn = new SqlConnection(getConnectionString());
        SqlCommand cmd = new SqlCommand(selectQuery, conn);

        cmd.Parameters.AddWithValue("@Email", encEmail);
        cmd.Parameters.AddWithValue("@Password", encPassword);

        try
        {
            conn.Open();

            if (Convert.ToInt32(cmd.ExecuteScalar().ToString()) == 0)
                result = false;
        }
        catch (Exception) { }
        finally
        {
            conn.Close();
        }
        return result;
    }


    #region getConnectionString()
    public static string getConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString;
    }
    #endregion

}