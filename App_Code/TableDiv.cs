using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class TableDiv : HtmlGenericControl
{
    int state;
    Label lblTableName;
    Button btnAccept;
    Button btnDecline;
    public TableDiv(Table table)
    {
        state = table.AvailabilityID;
        this.ID = table.TableID.ToString();
        this.Attributes["class"] = "table";

        this.lblTableName = new Label();
        this.Controls.Add(lblTableName);
    }

    public TableDiv ToRequest()
    {
        this.lblTableName.Text = "Table " + this.ID;

        this.btnAccept = new Button();
        this.btnAccept.ID = this.ID + "_Accept";
        this.btnAccept.CssClass = "btnAccept";
        this.btnAccept.Text = "✓";
        this.btnAccept.OnClientClick = "changeTableState(" + this.ID + ", true)";
        this.Controls.Add(btnAccept);

        this.btnDecline = new Button();
        this.btnDecline.ID = this.ID + "_Decline";
        this.btnDecline.CssClass = "btnDecline";
        this.btnDecline.Text = "✗";
        this.btnAccept.OnClientClick = "changeTableState(" + this.ID + ", true)";
        this.Controls.Add(btnDecline);

        return this;
    }

    public TableDiv ToContent()
    {
        this.lblTableName.Text = this.ID;
        if (state == 1)
            this.Attributes["style"] = "background-color: red";
        else
            this.Attributes["style"] = "background-color: green";

        return this;
    }

}