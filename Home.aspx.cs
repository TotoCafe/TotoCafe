using System;
using System.Collections.Generic;

public partial class Home : System.Web.UI.Page
{
    Company cmp;
    HashSet<Table> tableList;

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
        this.tableList = new HashSet<Table>(cmp.GetTableList());
        if (this.tableList.Count == 0)
            Response.Redirect("Settings.aspx");
        else
            initializeContent();
    }
    private void initializeContent()
    {
        foreach (Table t in this.tableList)
        {
            TablePanel contentTableDiv = new TablePanel(t).ToContent();
            contentTableDiv.InformationClick = btnInformationClicked;
            pHome.Controls.Add(contentTableDiv);
        }
    }
    protected void btnInformationClicked (object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", )

    }
}