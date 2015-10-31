using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Menu : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
        string encEmail = FormsAuthentication.HashPasswordForStoringInConfigFile(ticket.Name, "SHA1");

    }

    #region Triggers

    protected void btnTriggerAddTable_Click(object sender, EventArgs e)
    {
        panelAddTable.Visible = true;
        panelRemoveTable.Visible = false;
        panelAddCategory.Visible = false;
        panelRemoveCategory.Visible = false;
        panelAddProduct.Visible = false;
        panelRemoveProduct.Visible = false;
    }

    protected void btnTriggerRemoveTable_Click(object sender, EventArgs e)
    {
        panelAddTable.Visible = false;
        panelRemoveTable.Visible = true;
        panelAddCategory.Visible = false;
        panelRemoveCategory.Visible = false;
        panelAddProduct.Visible = false;
        panelRemoveProduct.Visible = false;
        getCompanyID();
    }

    protected void btnTriggerAddCategory_Click(object sender, EventArgs e)
    {
        panelAddTable.Visible = false;
        panelRemoveTable.Visible = false;
        panelAddCategory.Visible = true;
        panelRemoveCategory.Visible = false;
        panelAddProduct.Visible = false;
        panelRemoveProduct.Visible = false;
    }

    protected void btnTriggerRemoveCategory_Click(object sender, EventArgs e)
    {
        panelAddTable.Visible = false;
        panelRemoveTable.Visible = false;
        panelAddCategory.Visible = false;
        panelRemoveCategory.Visible = true;
        panelAddProduct.Visible = false;
        panelRemoveProduct.Visible = false;
        getCompanyID();

    }

    protected void btnTriggerAddProduct_Click(object sender, EventArgs e)
    {
        panelAddTable.Visible = false;
        panelRemoveTable.Visible = false;
        panelAddCategory.Visible = false;
        panelRemoveCategory.Visible = false;
        panelAddProduct.Visible = true;
        panelRemoveProduct.Visible = false;
    }

    protected void btnTriggerRemoveProduct_Click(object sender, EventArgs e)
    {
        panelAddTable.Visible = false;
        panelRemoveTable.Visible = false;
        panelAddCategory.Visible = false;
        panelRemoveCategory.Visible = false;
        panelAddProduct.Visible = false;
        panelRemoveProduct.Visible = true;
    }

    #endregion



    #region getCompanyID
    protected int getCompanyID()
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

            while (reader.Read())
            {
                companyID = (int)reader["CompanyID"];
            }

            reader.Close();

        }
        catch (Exception) { }
        finally
        {

            conn.Close();
        }
        lblStatus.Text = companyID.ToString();
        return companyID;
    }
    #endregion

    #region getEmailFromCookie
    protected string getEMail()
    {
        HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
        return FormsAuthentication.HashPasswordForStoringInConfigFile(ticket.Name, "SHA1");
    }
    #endregion

    #region getConnectionString()
    public static string getConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString;
    }
    #endregion

    #region Table Settings 
    protected void btnSaveTable_Click(object sender, EventArgs e)
    {
        lblSaveTable.Visible = true;
        string insertQuery = "INSERT INTO [Table] (TableName, QrCode, IsReserved, CompanyID)" +
                                       "VALUES (@TableName, @QrCode, @IsReserved, @CompanyID)";
        SqlConnection conn = new SqlConnection(getConnectionString());
        SqlCommand cmd = new SqlCommand(insertQuery, conn);
        cmd.Parameters.AddWithValue("@TableName", textBoxTableName.Text);
        cmd.Parameters.AddWithValue("@QrCode", "safaffafaffaf");
        cmd.Parameters.AddWithValue("@IsReserved", 0);
        cmd.Parameters.AddWithValue("@CompanyID", getCompanyID());

        try
        {
            conn.Open();
            cmd.ExecuteNonQuery();


            lblSaveTable.CssClass = "alert alert-success ";

        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.ToString();
            lblSaveTable.CssClass = "alert alert-warning";
            lblSaveTable.Text = "Warning :/ Try again..";
        }
        finally
        {
            textBoxTableName.Text = "";
            conn.Close();
            updatePanelSettings.DataBind();
        }

    }

    protected void btnRemoveTable_Click(object sender, EventArgs e)
    {
        string query = "delete from [Table] where [Table].CompanyID = @CompanyID and [Table].TableID = @TableID";
        SqlConnection con = new SqlConnection(getConnectionString());
        SqlCommand cmd = new SqlCommand(query, con);

        cmd.Parameters.AddWithValue("@CompanyID", getCompanyID());
        cmd.Parameters.AddWithValue("@TableID", dropDownListTables.SelectedValue);

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            lblRemoveTable.Visible = true;
        }
        catch (Exception ex)
        {
            lblRemoveTable.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            updatePanelSettings.DataBind();
        }


    }
    #endregion

    #region Category Settings
    protected void btnSaveCategory_Click(object sender, EventArgs e)
    {

        lblSaveCategory.Visible = true;
        string insertQuery = "INSERT INTO Category (CategoryName, CompanyID)" +
                                       "VALUES (@CategoryName, @CompanyID)";
        SqlConnection conn = new SqlConnection(getConnectionString());
        SqlCommand cmd = new SqlCommand(insertQuery, conn);
        cmd.Parameters.AddWithValue("@CategoryName", textBoxCategoryName.Text);
        cmd.Parameters.AddWithValue("@CompanyID", getCompanyID());

        try
        {
            conn.Open();
            cmd.ExecuteNonQuery();


            lblSaveCategory.CssClass = "alert alert-success ";

        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.ToString();
            lblSaveCategory.CssClass = "alert alert-warning";
            lblSaveCategory.Text = "Warning :/ Try again..";
        }
        finally
        {
            textBoxCategoryName.Text = "";
            conn.Close();
            updatePanelSettings.DataBind();
        }

    }

    protected void btnRemoveCategory_Click(object sender, EventArgs e)
    {
        string query = "delete from [Category] where [Category].CompanyID = @CompanyID and [Category].CategoryID = @CategoryID";
        SqlConnection con = new SqlConnection(getConnectionString());
        SqlCommand cmd = new SqlCommand(query, con);

        cmd.Parameters.AddWithValue("@CompanyID", getCompanyID());
        cmd.Parameters.AddWithValue("@CategoryID", dropDownListShowCategory.SelectedValue);

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            lblRemoveCategory.Visible = true;
        }
        catch (Exception ex)
        {
            lblRemoveCategory.Text = ex.ToString();
        }
        finally
        {
            con.Close();
            updatePanelSettings.DataBind();
        }
    }
    #endregion


}