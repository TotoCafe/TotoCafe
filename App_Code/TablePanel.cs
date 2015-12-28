using System;
using System.Web.UI.WebControls;

public class TablePanel : Panel
{
    int availability;
    bool active = false;
    public EventHandler DeclineClick { private get; set; }
    public EventHandler AcceptClick { private get; set; }
    public EventHandler InformationClick { private get; set; }
    public TablePanel(Table t)
    {
        ID = t.TableID.ToString();
        CssClass = "table";
        availability = t.AvailabilityID;
        Label lblTableName = new Label();
        lblTableName.Text = t.TableName;
        lblTableName.ForeColor = System.Drawing.Color.FromArgb(114, 177, 212);
        Controls.Add(lblTableName);
        active = (t.ActiveController != null);
    }
    public TablePanel ToRequest()
    {
        Button btnAccept = new Button();
        btnAccept.CssClass = "btnAccept";
        btnAccept.Text = "✓";
        btnAccept.Click += AcceptClick;
        Controls.Add(btnAccept);


        Button btnDecline = new Button();
        btnDecline.CssClass = "btnDecline";
        btnDecline.Text = "✗";
        btnDecline.Click += DeclineClick;
        Controls.Add(btnDecline);

        return this;
    }
    public TablePanel ToContent()
    {
        Button btnInformation = new Button();
        btnInformation.ID = ID + "_Info";
        btnInformation.Text = "Information";
        btnInformation.Click += InformationClick;
        Controls.Add(btnInformation);

        if (active)
            Attributes["style"] = "border-color: green";
        else
            Attributes["style"] = "border-color: red";

        return this;
    }
}