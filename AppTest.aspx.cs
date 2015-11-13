using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AppTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Test t = new Test();

        List<string> log = t.startTest();

        foreach (string s in log)
            lbTest.Items.Add(s);

        lbTest.DataBind();
    }
}