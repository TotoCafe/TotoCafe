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
            Response.Redirect("index.aspx");
        else
            getCompanyTables();
    }
    private void getCompanyTables()
    {
        this.tableList = new List<Table>(cmp.GetTableList());
        if (this.tableList.Count == 0)
            Response.Redirect("Settings.aspx");
        else
            initializeContent();
    }
    private void initializeContent()
    {
        foreach (Table t in this.tableList)
        {
            TableHtmlElement content = new TableHtmlElement(t).ToContent();
            content.ButtonInformation = btnInfoClicked;
            pHome.Controls.Add(content);
        }
    }
    protected void btnInfoClicked (object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", )

    }
}