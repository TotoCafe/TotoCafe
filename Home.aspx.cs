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
    Company cmp = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        cmp = (Company)Session["Company"];
        if (cmp == null) Response.Redirect("Index.aspx");
        else
        {
            InitTableButtons();
        }
    }

    #region Init Button
    public void InitTableButtons()
    {
        
    }
    #endregion

    #region Functions
    protected void TableButtonsClick(object sender, EventArgs e)
    {
        Button ClickedButton = (Button)sender;

        //GetTableOrders(ClickedButton.ID);

        panelTables.Visible = false;
        panelTableSummary.Visible = true;
    }

    protected void back(object sender, EventArgs e)
    {
        panelTables.Visible = true;
        panelTableSummary.Visible = false;
    }
    #endregion

    #region getConnectionString()
    public static string getConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString;
    }
    #endregion
}