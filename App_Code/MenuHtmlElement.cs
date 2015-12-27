using System.Collections;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for MenuHtmlElement
/// </summary>
public class MenuHtmlElement : HtmlGenericControl
{
    Label categoryName;
    Panel productPanel;

    public MenuHtmlElement(Category category)
    {
        this.categoryName.Text = category.CategoryName;
        foreach (Product p in category.Products)
        {
            productPanel = new Panel();
            Label tbProductName = new Label();
            tbProductName.Text = p.ProductName;
            TextBox tbProductPrice = new TextBox();
            tbProductPrice.Text = p.Price.ToString();
            this.Controls.Add(tbProductName);
            this.Controls.Add(tbProductPrice);
        }
    }
}