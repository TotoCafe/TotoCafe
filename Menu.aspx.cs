using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Menu : System.Web.UI.Page
{
    Company cmp;
    HashSet<Category> categoryList;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.cmp = (Company)Session["Company"];
        if (this.cmp == null)
            Response.Redirect("index.aspx");
        else
            getCompanyCategories();
    }
    private void getCompanyCategories()
    {
        this.categoryList = new HashSet<Category>(cmp.GetCategoryList());
        if (this.categoryList.Count == 0)
            Response.Redirect("Settings.aspx");
        else
            initializeCategories();
    }
    private void initializeCategories()
    {
        foreach (Category c in this.categoryList)
        {
            var container = new HtmlGenericControl("div");
            container.ID = c.CategoryName;
            container.Attributes["class"] = "category";

            var categoryName = new HtmlGenericControl("p");
            categoryName.InnerText = c.CategoryName;
            container.Controls.Add(categoryName);

            foreach (Product p in c.GetProducts)
            {
                var productPanel = new HtmlGenericControl("div");
                productPanel.ID = p.CategoryID + "_" + p.ProductID;
                productPanel.Attributes["class"] = "product";

                TextBox tbProductName = new TextBox();
                tbProductName.Text = p.ProductName;
                productPanel.Controls.Add(tbProductName);

                TextBox tbProductDetail = new TextBox();
                tbProductDetail.Text = p.Detail;
                productPanel.Controls.Add(tbProductDetail);

                TextBox tbProductPrice = new TextBox();
                tbProductPrice.Text = p.Price.ToString();
                productPanel.Controls.Add(tbProductPrice);

                Button btnProductUpdate = new Button();
                btnProductUpdate.Text = "Update";
                btnProductUpdate.Click += BtnProductUpdate_Click;
                productPanel.Controls.Add(btnProductUpdate);
                

                container.Controls.Add(productPanel);
            }
            pMenu.Controls.Add(container);
        }
    }

    private void BtnProductUpdate_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}