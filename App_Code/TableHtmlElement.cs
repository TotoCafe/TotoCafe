using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class TableHtmlElement : HtmlGenericControl
{
    int availability;
    bool active = false;
    Label lblTableName;
    Button btnAccept;
    Button btnDecline;
    public EventHandler ButtonAccept
    {
        set { btnAccept.Click += value; }
    }
    public EventHandler ButtonDecline
    {
        set { btnDecline.Click += value; }
    }
    public TableHtmlElement(Table table)
    {
        this.ID = table.TableID.ToString();
        this.Attributes["class"] = "table";
        this.availability = table.AvailabilityID;
        this.lblTableName = new Label();
        this.lblTableName.Text = table.TableName;
        this.lblTableName.Attributes["style"] = "color: #72B1D4";
        this.Controls.Add(lblTableName);
        this.active = table.ActiveController != null;
    }

    public TableHtmlElement ToRequest()
    {
        this.btnAccept = new Button();
        this.btnAccept.ID = this.ID + "_Accept";
        this.btnAccept.CssClass = "btnAccept";
        this.btnAccept.Text = "✓";
        this.Controls.Add(btnAccept);

        this.btnDecline = new Button();
        this.btnDecline.ID = this.ID + "_Decline";
        this.btnDecline.CssClass = "btnDecline";
        this.btnDecline.Text = "✗";
        this.Controls.Add(btnDecline);

        return this;
    }

    public TableHtmlElement ToContent()
    {
        if (this.active)
            this.Attributes["style"] = "border-color: green";
        else
            this.Attributes["style"] = "border-color: red";

        return this;
    }

}