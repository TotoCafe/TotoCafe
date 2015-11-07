using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.Services;
using System.Configuration;
using System.Web.UI.HtmlControls;

public partial class Index : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
                Response.Redirect("Home.aspx");
        }
    }
    #endregion

    #region Queries
    string insertQuery = "INSERT INTO Company (CompanyName, Email, Password, Address, Phone#, Grade, Location, CityID)" +
                                      "VALUES (@CompanyName, @Email, @Password, @Address, @Phone#, @Grade, @Location, @CityID)";
    #endregion

    #region Sign Up
    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        if (databaseInsertCompany(insertQuery))
        {
            FormsAuthentication.SetAuthCookie(txtCompanyEmail.Text, false);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                                                                            1,
                                                                            txtCompanyEmail.Text,
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
    }
    #endregion

    #region Database Operations
    public bool databaseInsertCompany(string query)
    {
        bool result = false;
        string encPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtCompanyPassword.Text, "SHA1");
        string encEmail = FormsAuthentication.HashPasswordForStoringInConfigFile(txtCompanyEmail.Text, "SHA1");
        string encPhone = FormsAuthentication.HashPasswordForStoringInConfigFile(txtCompanyPhone.Text, "SHA1");

        SqlConnection conn = new SqlConnection(getConnectionString());
        SqlCommand cmd = new SqlCommand(query, conn);

        cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text);
        cmd.Parameters.AddWithValue("@Email", encEmail);
        cmd.Parameters.AddWithValue("@Password", encPassword);
        cmd.Parameters.AddWithValue("@Address", txtCompanyAddress.Text);
        cmd.Parameters.AddWithValue("@Phone#", encPhone);
        cmd.Parameters.AddWithValue("@Grade", 0);
        cmd.Parameters.AddWithValue("@Location", "");
        cmd.Parameters.AddWithValue("@CityID", ddlCity.SelectedValue);

        try
        {
            conn.Open();
            cmd.ExecuteNonQuery();
            result = true;

        }
        catch (Exception) { }
        finally
        {
            conn.Close();
        }
        return result;
    }

    [WebMethod]
    public static string CheckCompanyEmail(string CompanyEmail)
    {
        int result = 0;
        string encEmail = FormsAuthentication.HashPasswordForStoringInConfigFile(CompanyEmail, "SHA1");
        string query = "SELECT COUNT(Email) FROM Company WHERE Email = '" + encEmail + "'";

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString);
        SqlCommand cmd = new SqlCommand(query, conn);

        try
        {
            conn.Open();
            result = int.Parse((cmd.ExecuteScalar().ToString()));
        }
        catch (Exception) { /*Handle errors*/ }
        finally
        {
            conn.Close();
        }
        return result == 0 ? "<font color='#32CD32'><span class='glyphicon glyphicon-ok' aria-hidden='true'></span></font>"
                           : "<font color='#cc0000'><span class='glyphicon glyphicon-remove' aria-hidden='true'></span></font>";
    }
    #endregion

    #region getConnectionString()
    public static string getConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString;
    }
    #endregion
}