using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
        string encEmail = FormsAuthentication.HashPasswordForStoringInConfigFile(ticket.Name, "SHA1");

        lblStatus.Text = encEmail;
        if (authCookie == null) 
            Response.Redirect("Index.aspx");
        else
        {
            if (Authentication(encEmail))
            {
                panelDefault.Visible = true;
            }
            else
            {
                panelFirst.Visible = true;
            }


        }

        

    }

    public bool Authentication(string Email)
    {
       // string query = "SELECT [Table].TableID FROM [Table] INNER JOIN Company ON [Table].CompanyID = Company.CompanyID WHERE (Company.Email = @Email)";
        string query = "Select COUNT(*) from [Table] , Company Where [Table].CompanyID = Company.CompanyID and Company.Email = '42F55A2B142D722DC45D7A879437D1C76C9F7D97'";
        
       
        SqlConnection conn = new SqlConnection(getConnectionString());
        SqlCommand cmd = new SqlCommand(query, conn);

      //  cmd.Parameters.AddWithValue("@Email", Email);
      

        try
        {
            conn.Open();

            if (Convert.ToInt32(cmd.ExecuteScalar().ToString()) == 0)
                return false;
            lblStatus.Text = cmd.ExecuteScalar().ToString();
        }
        catch (Exception e) {
            lblStatus.Text = e.ToString(); 
        }
        finally
        {
            conn.Close();
        }
        return true;
    }

    #region getConnectionString()
    public static string getConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString;
    }
    #endregion
}