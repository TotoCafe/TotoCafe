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
        for (int i = 0; i < 100; i++)
        {
            createDiv(i+1);
        }
    }
    public void createDiv(int id)
    {
        HtmlGenericControl div = new HtmlGenericControl("div");
        div.ID = id.ToString();
        div.Attributes["class"] = "table";
        Label lblText = new Label();
        lblText.Text = "Table " + div.ID;
        Button btnAccept = new Button();
        Button btnDecline = new Button();
        btnAccept.CssClass = "btnAccept";
        btnDecline.CssClass = "btnDecline";
        btnAccept.Text = "✔";
        btnDecline.Text = "✘";
        div.Controls.Add(lblText);
        div.Controls.Add(btnAccept);
        div.Controls.Add(btnDecline);
        upHome.ContentTemplateContainer.Controls.Add(div);
    }
}