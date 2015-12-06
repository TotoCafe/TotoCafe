using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class home2 : System.Web.UI.Page
{
    Company cmp;
    List<Table> tableList;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.cmp = (Company)Session["Company"];
        if (this.cmp == null)
            Response.Redirect("login.aspx");
        else
            getCompanyTables();
    }
    private void getCompanyTables()
    {
        this.tableList = new List<Table>(cmp.GetTableList());
        //tableRequests = new List<Table>(cmp.GetTableRequest())
        //if (this.tableList.Count == 0)
        //    Response.Redirect("AccSettings.aspx");
        //else
            initializeContent();
    }
    private void initializeContent()
    {
        foreach (Table t in this.tableList)
        {
            TableDiv content = new TableDiv(t).ToContent();
            pHome.Controls.Add(content);
        }
    }
}