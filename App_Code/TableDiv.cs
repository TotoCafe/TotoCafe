using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class TableDiv : HtmlGenericControl
{
    int availability;
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
    public TableDiv(Table table)
    {
        this.ID = table.TableID.ToString();
        this.Attributes["class"] = "table";
        this.availability = table.AvailabilityID;
        this.lblTableName = new Label();
        this.lblTableName.Text = table.TableName;
        this.lblTableName.Attributes["style"] = "color: #72B1D4";
        this.Controls.Add(lblTableName);
    }

    public TableDiv ToRequest()
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

    public TableDiv ToContent()
    {
        if (this.availability == 1)
            this.Attributes["style"] = "background-color: white";
        else
            this.Attributes["style"] = "background-color: green";

        return this;
    }

}