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
            Panel category = new CategoryPanel(c);
            pMenu.Controls.Add(category);
        }
    }
}