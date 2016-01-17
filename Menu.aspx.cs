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
    Dictionary<int, Category> categories;

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
        this.categories = cmp.GetCategoryCollection();
        if (this.categories.Count == 0)
            Response.Redirect("Settings.aspx");
        else
            initializeCategories();
    }
    private void initializeCategories()
    {
        foreach (Category c in this.categories.Values)
        {
            CategoryPanel category = new CategoryPanel(c);
            pMenu.Controls.Add(category);
        }
    }
}