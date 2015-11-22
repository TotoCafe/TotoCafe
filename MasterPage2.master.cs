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
            createDiv();
        }
    }

    public void createDiv()
    {
        HtmlGenericControl div = new HtmlGenericControl("div");
        div.ID = rnd.Next(100).ToString();
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
        upRequest.ContentTemplateContainer.Controls.Add(div);
    }
}
