using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    Company cmp;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.cmp = (Company)Session["Company"];
        /*
            Method needed for requestList
            initializeRequest(cmp.GetRequestList);
        */

        //Test code
        List<Table> temp = new List<Table>(20);
        for (int i = 1; i <= temp.Capacity; i++)
        {
            Table table = new Table();
            table.TableID = i;
            table.TableName = "Table-" + i;
            temp.Add(table);
        }
        initializeRequest(temp);
        //End of Test Code
    }
    private void initializeRequest(List<Table> tableList)
    {
        foreach (Table t in tableList)
        {
            TablePanel requestTableDiv = new TablePanel(t).ToRequest();
            requestTableDiv.AcceptClick = btnAcceptClick;
            requestTableDiv.DeclineClick = btnDeclineClick;
            pRequest.Controls.Add(requestTableDiv);
        }
    }
    protected void btnAcceptClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int id = int.Parse(btn.Parent.ID);
        TableController controller = new TableController();
        Table table = cmp.GetTableWithId(id);
        table.ActiveController = controller;
    }
    protected void btnDeclineClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int id = int.Parse(btn.Parent.ID);
        Table table = cmp.GetTableWithId(id);
        table.ActiveController = null;
    }
    protected void btnLogOutClick(object sender, EventArgs e)
    {
        this.cmp = null;
        Session.Abandon();
        Response.Redirect(Request.RawUrl);
    }

}

