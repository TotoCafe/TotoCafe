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
    Company cmp = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        cmp = (Company)Session["Company"];
        if (cmp != null)
            Response.Redirect("Home.aspx");
    }

    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        cmp = new Company();

        if (
            cmp.Insert(
            txtCompanyName.Text,
            txtCompanyEmail.Text,
            txtCompanyPassword.Text,
            txtCompanyAddress.Text,
            txtCompanyPhone.Text,
            0,
            "",
            int.Parse(ddlCity.SelectedValue))
            )
        {
            cmp.Initialize(txtCompanyEmail.Text);

            Session["Company"] = cmp;
            Response.Redirect("Home.aspx");
        }
    }

    [WebMethod]
    public static string CheckCompanyEmail(string CompanyEmail)
    {
        int result = 0;
        string encEmail = FormsAuthentication.HashPasswordForStoringInConfigFile(CompanyEmail, "SHA1");
        string query = "SELECT COUNT(Email) FROM Company WHERE Email = '" + encEmail + "'";

        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
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
}