using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MasterPage2 : System.Web.UI.MasterPage
{
    Random rnd = new Random();
    protected void Page_Load(object sender, EventArgs e)
    {
        for (int i = 0; i < 5; i++)
        {
            Table table = new Table();
            table.TableID = rnd.Next(100);
            TableDiv request = new TableDiv(table);
            upRequest.ContentTemplateContainer.Controls.Add(request.ToRequest());
        }
    }


    protected void addDiv(object sender, EventArgs e)
    {
        Table table = new Table();
        table.TableID = rnd.Next(100);
        TableDiv request = new TableDiv(table);
        upRequest.ContentTemplateContainer.Controls.Add(request.ToRequest());
    }

    protected void removeDiv(object sender, EventArgs e)
    {
        
    }
}

