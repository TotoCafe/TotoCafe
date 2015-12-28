using System;
using System.Web.UI.WebControls;
public class CategoryPanel : Panel
{
    public EventHandler EditClick { private get; set; }
    public EventHandler UpdateClick { private get; set; }
    public EventHandler DeleteClick { private get; set; }
    public CategoryPanel(Category c)
    {
        this.ID = c.CategoryID.ToString();
        this.CssClass = "category";
        TextBox tbCategoryName = new TextBox();
        tbCategoryName.Text = c.CategoryName;
        tbCategoryName.CssClass = "product_textbox";
        this.Controls.Add(tbCategoryName);
        foreach (Product p in c.GetProducts)
        {
            this.Controls.Add(ProductPanel(p));
        }
    }
    private Panel ProductPanel(Product p)
    {
        Panel product = new Panel();
        product.ID = p.CategoryID + "_" + p.ProductID;
        product.CssClass = "product";

        TextBox tbProductName = new TextBox();
        tbProductName.Text = p.ProductName;
        tbProductName.ReadOnly = true;
        tbProductName.CssClass = "product_textbox";
        product.Controls.Add(tbProductName);

        TextBox tbProductDetail = new TextBox();
        tbProductDetail.Text = p.Detail;
        tbProductDetail.ReadOnly = true;
        tbProductDetail.CssClass = "product_textbox";
        product.Controls.Add(tbProductDetail);

        TextBox tbProductPrice = new TextBox();
        tbProductPrice.Text = p.Price.ToString();
        tbProductPrice.ReadOnly = true;
        tbProductPrice.CssClass = "product_textbox"; 
        product.Controls.Add(tbProductPrice);

        Button btnProductEdit = new Button();
        btnProductEdit.Text = "✎";
        btnProductEdit.CssClass = "product_button product_button_edit";
        btnProductEdit.Click += EditClick;
        product.Controls.Add(btnProductEdit);

        Button btnProductUpdate = new Button();
        btnProductUpdate.Text = "✔";
        btnProductUpdate.CssClass = "product_button product_button_update";
        btnProductUpdate.Click += UpdateClick;
        product.Controls.Add(btnProductUpdate);

        Button btnProductDelete = new Button();
        btnProductDelete.Text = "✘";
        btnProductDelete.CssClass = "product_button product_button_delete";
        btnProductDelete.Click += DeleteClick;
        product.Controls.Add(btnProductDelete);

        return product;
    }
}