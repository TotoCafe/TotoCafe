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
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
        if (authCookie == null) Response.Redirect("Index.aspx");
        else
        {
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            string encEmail = FormsAuthentication.HashPasswordForStoringInConfigFile(ticket.Name, "SHA1");
            lblStatus.Text = getCompanyID().ToString();
            lblNoTable.Visible = false;

           
        }
    }

    #region Triggers
    protected void btnCloseCategory_Click(object sender, EventArgs e)
    {
        panelAddCategory.Visible = false;
    }
    protected void btnCloseCategoryRemove_Click(object sender, EventArgs e)
    {
        panelRemoveCategory.Visible = false;
    }
    protected void btnCancelRemove_Click(object sender, EventArgs e)
    {
        panelRemoveTable.Visible = false;
        panelAddTable.Visible = false;

        btnTriggerRemoveTable.Visible = true;
        btnTriggerAddTable.Visible = true;
    }
    protected void btnTableSettings_Click(object sender, EventArgs e)
    {
        panelTableSettings.Visible = true;
        panelMenuSettings.Visible = false;
    }
    protected void btnMenuSettings_Click(object sender, EventArgs e)
    {
        panelTableSettings.Visible = false;
        panelMenuSettings.Visible = true;
    }
    protected void btnTriggerAddTable_Click(object sender, EventArgs e)
    {

        btnTriggerAddTable.Visible = false;
        btnTriggerRemoveTable.Visible = false;


        panelAddTable.Visible = true;
        panelRemoveTable.Visible = false;

        labelGizle();
        
    }
    protected void btnTriggerRemoveTable_Click(object sender, EventArgs e)
    {
        btnTriggerAddTable.Visible = false;
        btnTriggerRemoveTable.Visible = false;

        panelAddTable.Visible = false;
        panelRemoveTable.Visible = true;
        labelGizle();

    }
    protected void btnTriggerAddCategory_Click(object sender, EventArgs e)
    {
        panelAddCategory.Visible = true;
        panelRemoveCategory.Visible = false;
        panelAddProduct.Visible = false;
        panelRemoveProduct.Visible = false;
        labelGizle();

    }
    protected void btnTriggerRemoveCategory_Click(object sender, EventArgs e)
    {
        panelAddCategory.Visible = false;
        panelRemoveCategory.Visible = true;
        panelAddProduct.Visible = false;
        panelRemoveProduct.Visible = false;
        labelGizle();

    }
    protected void btnTriggerAddProduct_Click(object sender, EventArgs e)
    {
        panelAddCategory.Visible = false;
        panelRemoveCategory.Visible = false;
        panelAddProduct.Visible = true;
        panelRemoveProduct.Visible = false;
        labelGizle();

    }
    protected void btnTriggerRemoveProduct_Click(object sender, EventArgs e)
    {
        panelAddCategory.Visible = false;
        panelRemoveCategory.Visible = false;
        panelAddProduct.Visible = false;
        panelRemoveProduct.Visible = true;
        labelGizle();

    }
    protected void btnCancelAdd_Click(object sender, EventArgs e)
    {/*Cancels addTable.*/
        btnTriggerAddTable.Visible = true; ;
        btnTriggerRemoveTable.Visible = true;


        panelAddTable.Visible = false;
        panelRemoveTable.Visible = false;


    }
    protected void btnCloseRemoveProduct_Click(object sender, EventArgs e)
    {
        panelRemoveProduct.Visible = false;

    }
    protected void btnCloseAddProduct_Click(object sender, EventArgs e)
    {
        panelAddProduct.Visible = false;
    }
    #endregion

    #region Table Settings
    protected void btnSaveTable_Click(object sender, EventArgs e)
    {
        if (textBoxTableName.Text.Length != 0)
        {
            lblSaveTable.Visible = true;
            string insertQuery = "INSERT INTO [Table] (TableName, QrCode, IsReserved, CompanyID)" +
                                           "VALUES (@TableName, @QrCode, @IsReserved, @CompanyID)";
            SqlConnection conn = new SqlConnection(getConnectionString());
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@TableName", textBoxTableName.Text);
            cmd.Parameters.AddWithValue("@QrCode", generateQrString(getCompanyID().ToString(), textBoxTableName.Text));
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
                refreshDropdownLists();
                settingUpdatePanel.DataBind();
            }
        }

    }
    protected void btnRemoveTable_Click(object sender, EventArgs e)
    {
        if (dropDownListTables.SelectedIndex != 0)
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
                refreshDropdownLists();
                settingUpdatePanel.DataBind();
            }
        }
    }
    #endregion

    #region Category Settings
    protected void btnSaveCategory_Click(object sender, EventArgs e)
    {

        if (textBoxCategoryName.Text.Length != 0)
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
                refreshDropdownLists();
                settingUpdatePanel.DataBind();
            }
        }

    }
    protected void btnRemoveCategory_Click(object sender, EventArgs e)
    {
        if (dropDownListShowCategory.SelectedIndex != 0)
        {
            string query = "delete from [Category] where [Category].CompanyID = @CompanyID and [Category].CategoryID = @CategoryID";
            string query2 = "DELETE FROM Product WHERE Product.CategoryID = @CategoryID";
            SqlConnection con = new SqlConnection(getConnectionString());
            SqlCommand cmd = new SqlCommand(query, con);
            SqlCommand cmd2 = new SqlCommand(query2, con);

            cmd.Parameters.AddWithValue("@CompanyID", getCompanyID());
            cmd.Parameters.AddWithValue("@CategoryID", dropDownListShowCategory.SelectedValue);

            cmd2.Parameters.AddWithValue("@CategoryID", dropDownListShowCategory.SelectedValue);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();//Deletes Category

                cmd2.ExecuteNonQuery();//Removes all products in the category which is deleted.

                lblRemoveCategory.Visible = true;
            }
            catch (Exception ex)
            {
                lblRemoveCategory.Text = ex.ToString();
            }
            finally
            {
                con.Close();
                refreshDropdownLists();
                settingUpdatePanel.DataBind();
            }
        }
    }
    #endregion

    #region Product Settings
    protected void btnAddProduct_Click(object sender, EventArgs e)
    {
        if(
            txtProductCredit.Text != "" &&
            txtProductDetails.Text != "" &&
            txtProductName.Text != "" &&
            txtProductPrice.Text != "" &&
            ddlCategorySelect.SelectedIndex != 0
        )
        {
            string query = "INSERT INTO Product (ProductName, Detail, Price, Credit, CategoryID) VALUES (@ProductName, @Detail, @Price, @Credit, @CategoryID)";
            SqlConnection con = new SqlConnection(getConnectionString());
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
            cmd.Parameters.AddWithValue("@Detail", txtProductDetails.Text);
            cmd.Parameters.AddWithValue("@Price", Convert.ToDouble(txtProductPrice.Text));
            cmd.Parameters.AddWithValue("@Credit", Convert.ToDouble(txtProductCredit.Text));
            cmd.Parameters.AddWithValue("@CategoryID", ddlCategorySelect.SelectedValue);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { }
            finally
            {
                lblSaveProduct.Visible = true;
                con.Close();
                refreshDropdownLists();
                settingUpdatePanel.DataBind();
                txtProductCredit.Text = "";
                txtProductDetails.Text = "";
                txtProductName.Text = "";
                txtProductPrice.Text = "";
            }
        }
        
    }
    protected void btnRemoveProduct_Click(object sender, EventArgs e)
    {
        if (ddlRemoveProduct.SelectedIndex != 0)
        {
            string query = "DELETE FROM Product WHERE Product.ProductID = @ProductID";
            SqlConnection con = new SqlConnection(getConnectionString());
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ProductID", ddlRemoveProduct.SelectedValue);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { }
            finally
            {
                lblRemoveProduct.Visible = true;
                con.Close();
                refreshDropdownLists();
                settingUpdatePanel.DataBind();
                
            }
        }
    }
    #endregion

    #region Methods
    public void labelGizle()
    {
        /* Label visibilities 
            Ekleme ve çıkarma buttonları arasında gezinirken
            Önceki paneldeki ürün eklendi ya da silindi 
            yazıları göze hoş gelmediği için her butonla
            geçiş yapıldığında label ları gizledim.
        */
        lblSaveTable.Visible = false;
        lblRemoveTable.Visible = false;

        lblSaveCategory.Visible = false;
        lblRemoveCategory.Visible = false;

        lblSaveProduct.Visible = false;
        lblRemoveProduct.Visible = false;
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
    public string generateQrString(string CompanyID, string TableName)
    {
        return "(" + CompanyID + ")-QR-(" + TableName + ")";
    }
    public void refreshDropdownLists()
    {
        var tempItem = ddlCategorySelect.Items[0];
        ddlCategorySelect.Items.Clear();
        ddlCategorySelect.Items.Add(tempItem);

        tempItem = ddlRemoveProduct.Items[0];
        ddlRemoveProduct.Items.Clear();
        ddlRemoveProduct.Items.Add(tempItem);

        tempItem = dropDownListShowCategory.Items[0];
        dropDownListShowCategory.Items.Clear();
        dropDownListShowCategory.Items.Add(tempItem);

        tempItem = dropDownListTables.Items[0];
        dropDownListTables.Items.Clear();
        dropDownListTables.Items.Add(tempItem);
    }
    #endregion



}