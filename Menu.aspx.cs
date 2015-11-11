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

public partial class Menu : System.Web.UI.Page
{
    Company cmp = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        cmp = (Company)Session["Company"];
        if (cmp == null) Response.Redirect("Index.aspx");
    }


    private void bindgridview()
    {
        DataView dv = (DataView)SqlDataSourceShowCategories.Select(DataSourceSelectArguments.Empty);
        GridView1.DataSource = dv;
        GridView1.DataBind();


    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            try
            {

                //dolaba insert işlemi...
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString);
                string query = "SELECT Product.* FROM Category INNER JOIN Company ON Category.CompanyID = Company.CompanyID INNER JOIN Product ON Category.CategoryID = Product.CategoryID WHERE (Category.CategoryID = @CategoryID) AND (Company.CompanyID = @CompanyID)";

                conn.Open();

                DataList dl = (DataList)e.Row.FindControl("datalist");
                SqlCommand com = new SqlCommand(query, conn);
                com.Parameters.AddWithValue("@CategoryID", Convert.ToInt32(e.Row.Cells[1].Text));
                com.Parameters.AddWithValue("@CompanyID", lblStatus.Text);

                SqlDataAdapter da = new SqlDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();

                // dl.RepeatColumns = 4;
                // dl.RepeatDirection = RepeatDirection.Vertical;
                dl.DataSource = ds;
                dl.DataBind();

            }
            catch (Exception ee)
            {
                Response.Write(ee.ToString());
            }




            //updatePanel3.Update();
        }
    }
    public int getCompanyID()
    {
        string query = "Select * from Company where Company.Email = @Email";
        SqlConnection conn = new SqlConnection(getConnectionString());
        SqlCommand com = new SqlCommand(query, conn);
        com.Parameters.AddWithValue("@Email", getEMail());
        int companyID = 0;
        try
        {
            conn.Open();
            SqlDataReader reader = com.ExecuteReader();

            reader.Read();
            companyID = (int)reader["CompanyID"];

            reader.Close();
        }
        catch (Exception) { }
        finally
        {
            conn.Close();
        }
        return companyID;
    }
    protected string getEMail()
    {
        HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
        return FormsAuthentication.HashPasswordForStoringInConfigFile(ticket.Name, "SHA1");
    }
    public static string getConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString;
    }
}