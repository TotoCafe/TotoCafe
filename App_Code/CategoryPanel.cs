using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
public class CategoryPanel : Panel
{
    public Dictionary<int, Category> Categories { get; set; }

    public CategoryPanel()
    {
        foreach (Category c in Categories.Values)
        {
            this.Controls.Add(CreateCategoryPanel(c));
        }
    }
    private Panel CreateCategoryPanel(Category c)
    {
        Panel category = new Panel();
        category.ID = c.CategoryID.ToString();
        category.CssClass = "category";
        TextBox tbCategoryName = new TextBox();
        tbCategoryName.Text = c.CategoryName;
        tbCategoryName.CssClass = "product_textbox";
        category.Controls.Add(tbCategoryName);
        foreach (Product p in c.Products.Values)
        {
            category.Controls.Add(CreateProductPanel(p));
        }
        return category;

    }
    private Panel CreateProductPanel(Product p)
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
        btnProductUpdate.Click += UpdateClick ;
        product.Controls.Add(btnProductUpdate);

        Button btnProductDelete = new Button();
        btnProductDelete.Text = "✘";
        btnProductDelete.CssClass = "product_button product_button_delete";
        btnProductDelete.Click += DeleteClick;
        product.Controls.Add(btnProductDelete);

        return product;
    }

    private void EditClick(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void DeleteClick(object sender, EventArgs e)
    {
        
    }

    private void UpdateClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        MatchCollection mc = Regex.Matches(btn.Parent.ID, @"\d+");
        Product p = Categories[int.Parse(mc[0].Value)].Products[int.Parse(mc[1].Value)];
        p.Detail = "Test";
        p.Update();
    }
}