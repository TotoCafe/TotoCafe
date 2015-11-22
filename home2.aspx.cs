using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class home2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        for (int i = 1; i <= 100; i++)
        {
            Table table = new Table();
            table.TableID = i;
            table.AvailabilityID = 1;
            TableDiv request = new TableDiv(table);
            upHome.ContentTemplateContainer.Controls.Add(request.ToContent());
        }
    }
}