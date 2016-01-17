using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class home2 : System.Web.UI.Page
{
    Company cmp;
    HashSet<Table> tableList;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.cmp = (Company)Session["Company"];
        if (this.cmp == null)
            Response.Redirect("index.aspx");
        else
            getCompanyTables();
    }
    private void getCompanyTables()
    {
        this.tableList = new HashSet<Table>(cmp.GetTableList());
        if (this.tableList.Count == 0)
            Response.Redirect("Settings.aspx");
        else
            initializeContent();
    }
    private void initializeContent()
    {
        foreach (Table t in this.tableList)
        {
            TablePanel content = new TablePanel(t).ToContent();
            content.ButtonInformation = btnInfoClicked;
            pHome.Controls.Add(content);
        }
    }
    protected void btnInfoClicked (object sender, EventArgs e)
    {
        Button btnSender = (Button)sender;
        int id = int.Parse(Regex.Match(btnSender.ID, @"\d+").Value);
        pHome.Controls.Clear();
        pHome.Controls.Add(createInformationPanel(id));
    }
    private Panel createInformationPanel(int TableID)
    {
        Panel pnlInfo = new Panel();
        Dictionary<int, Product> products = new Dictionary<int, Product>();
        foreach (Category c in cmp.GetCategoryCollection().Values)
        {
            foreach (var p in c.GetProducts)
            {
                products.Add(p.Key, p.Value);
            }
        }

        Table t = cmp.GetTableWithId(TableID);
        foreach (Order o in t.ActiveController.getOrders().Values)
        {
            Label lbl = new Label();
            lbl.ForeColor = System.Drawing.Color.Black;
            Product product = products[o.ProductID - 1];
            lbl.Text = product.ProductID + " - " + product.ProductName + " - " + product.Price * o.Amount;
            pnlInfo.Controls.Add(lbl);
        }
        return pnlInfo;        
    }
}