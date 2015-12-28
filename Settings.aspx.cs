using System;
using System.Collections.Generic;

public partial class Settings : System.Web.UI.Page
{
    Company cmp = new Company();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.cmp = (Company)Session["Company"];
        if (this.cmp == null)
            Response.Redirect("login.aspx");
    }

    protected void addTable(object sender, EventArgs e)
    {
        Table table = new Table();
        if (!string.IsNullOrEmpty(tbTableName.Text))
        {
            try
            {
                table.TableName = tbTableName.Text;
                this.cmp.AddTable(table);
                lblNotification.Text = "Table added.";
            }
            catch (Exception)
            {
                lblNotification.Text = "Table could not added.";
            }
        }
        else
        {
            lblNotification.Text = "Please enter table name.";
        }

    }
    protected void removeTable(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(tbTableName.Text))
        {
            List<Table> tableList = new List<Table>(cmp.GetTableList());
            Table table = tableList.Find(p => p.TableName == tbTableName.Text && p.CompanyID == cmp.CompanyID);
            try
            {
                cmp.FreezeTable(table);
                lblNotification.Text = "Table removed.";
            }
            catch (Exception)
            {
                lblNotification.Text = "Table could not removed.";
            }
        }
        else
        {
            lblNotification.Text = "Please enter table name.";
        }
    }
}