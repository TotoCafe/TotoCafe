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
    Table infoTable;

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
    protected void btnInfoClicked(object sender, EventArgs e)
    {
        Button btnSender = (Button)sender;
        int id = int.Parse(Regex.Match(btnSender.ID, @"\d+").Value);
        pHome.Controls.Clear();
        pHome.Controls.Add(createInformationPanel(id));
    }
    private Panel createInformationPanel(int TableID)
    {
        Panel pnlInfo = new Panel();
        infoTable = cmp.GetTableWithId(TableID);
        float totalPrice = 0;
        Dictionary<int, Product> products = new Dictionary<int, Product>();

        foreach (Category c in cmp.GetCategoryCollection().Values)
        {
            foreach (var p in c.GetProducts)
            {
                products.Add(p.Key, p.Value);
            }
        }

        HtmlTable table = new HtmlTable();
        HtmlTableRow row;
        HtmlTableCell cell;
        Button btn;

        row = new HtmlTableRow();
        row.Attributes["class"] = "headRow";

        cell = new HtmlTableCell();
        cell.InnerText = "Product ID";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = "Product Name";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = "Amount";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = "Product Price";
        row.Cells.Add(cell);

        table.Rows.Add(row);

        foreach (Order o in infoTable.ActiveController.getOrders().Values)
        {
            Product product = products[o.ProductID];
            totalPrice += product.Price * o.Amount;

            row = new HtmlTableRow();
            row.Attributes["class"] = "contentRow";

            cell = new HtmlTableCell();
            cell.InnerText = product.ProductID.ToString();
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = product.ProductName;
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = o.Amount.ToString();
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = (product.Price * o.Amount).ToString();
            row.Cells.Add(cell);

            table.Rows.Add(row);
        }
        table.Style["margin"] = "auto";
        pnlInfo.Controls.Add(table);

        
        btn = new Button();
        btn.Text = "Back";
        btn.Click += BtnBack_Click;
        pnlInfo.Controls.Add(btn);

        btn = new Button();
        btn.Text = "Pay";
        btn.Click += BtnPay_Click;
        pnlInfo.Controls.Add(btn);

        Label lblTotalPrice = new Label();
        lblTotalPrice.Text = "Total: " + totalPrice.ToString();
        lblTotalPrice.ForeColor = System.Drawing.Color.Black;
        pnlInfo.Controls.Add(lblTotalPrice);

        pnlInfo.Style["color"] = "black";
        pnlInfo.Style["text-align"] = "center";
        return pnlInfo;
    }

    private void BtnPay_Click(object sender, EventArgs e)
    {
        infoTable.CloseTable();
    }

    private void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}