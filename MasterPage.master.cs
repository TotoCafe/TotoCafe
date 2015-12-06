﻿using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MasterPage2 : System.Web.UI.MasterPage
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
        List<Table> temp = new List<Table>(5);
        for (int i = 0; i < temp.Capacity; i++)
        {
            Table table = new Table();
            table.TableID = i;
            table.TableName = "Table-" + i+1;
            temp.Add(table);
        }
        initializeRequest(temp);
        //End of Test Code
    }
    private void initializeRequest(List<Table> requestTables)
    {
        foreach (Table requestTable in requestTables)
        {
            TableDiv requestDiv = new TableDiv(requestTable).ToRequest();
            requestDiv.ButtonAccept = btnAcceptClick;
            requestDiv.ButtonAccept = btnDeclineClick;
            //btnAccept.Click += new EventHandler(btnAcceptClick);
            pRequest.Controls.Add(requestDiv);
        }
    }
    protected void btnAcceptClick(object sender, EventArgs e)
    {

    }
    protected void btnDeclineClick(object sender, EventArgs e)
    {

    }

}

