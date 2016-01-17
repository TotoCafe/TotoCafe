using System;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for MenuHtmlElement
/// </summary>
public class CategoryPanel : Panel
{
    public CategoryPanel(Category c)
    {
        TextBox tbCategoryName = new TextBox();
        tbCategoryName.Text = c.CategoryName;
        this.Controls.Add(tbCategoryName);
        foreach (Product p in c.GetProducts.Values)
        {
            this.Controls.Add(ProductHtmlElement(p));
        }
    }

    private Panel ProductHtmlElement(Product p)
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
        product.Controls.Add(tbProductDetail);

        TextBox tbProductPrice = new TextBox();
        tbProductPrice.Text = p.Price.ToString();
        tbProductPrice.ReadOnly = true;
        product.Controls.Add(tbProductPrice);

        Button btnProductEdit = new Button();
        btnProductEdit.Text = "✎";
        btnProductEdit.CssClass = "product_button product_button_edit";
        btnProductEdit.Click += BtnProductEdit_Click;
        product.Controls.Add(btnProductEdit);

        Button btnProductUpdate = new Button();
        btnProductUpdate.Text = "✔";
        btnProductUpdate.CssClass = "product_button product_button_update";
        btnProductUpdate.Click += BtnProductUpdate_Click;
        product.Controls.Add(btnProductUpdate);

        Button btnProductDelete = new Button();
        btnProductDelete.Text = "✘";
        btnProductDelete.CssClass = "product_button product_button_delete";
        btnProductDelete.Click += BtnProductDelete_Click;
        product.Controls.Add(btnProductDelete);

        return product;
    }

    private void BtnProductDelete_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void BtnProductUpdate_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void BtnProductEdit_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}