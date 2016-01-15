using System;
using System.Collections.Generic;

public partial class Home : System.Web.UI.Page
{
    Company cmp;
    Dictionary<int, Table> tableDictionary;

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
        this.tableDictionary = new Dictionary<int, Table>(cmp.GetTableDictionary());
        if (this.tableDictionary.Count == 0)
            Response.Redirect("Settings.aspx");
        else
            initializeContent();
    }
    private void initializeContent()
    {
        foreach (Table t in this.tableDictionary.Values)
        {
            TablePanel contentTableDiv = new TablePanel(t).ToContent();
            contentTableDiv.InformationClick = btnInformationClicked;
            pHome.Controls.Add(contentTableDiv);
        }
    }
    protected void btnInformationClicked(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", )

    }
}