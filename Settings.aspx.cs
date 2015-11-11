﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Menu : System.Web.UI.Page
{
    Company cmp = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.qrButton);
        cmp = (Company)Session["Company"];

        if (cmp == null) Response.Redirect("Index.aspx");
        else
        {

            cmp.DeleteQrPdf(Server.MapPath("~/Qr Codes"));
        }
    }

    #region Triggers
    protected void btnCloseCategory_Click(object sender, EventArgs e)
    {
        panelAddCategory.Visible = false;
    }
    protected void btnCloseCategoryRemove_Click(object sender, EventArgs e)
    {
        panelRemoveCategory.Visible = false;
    }
    protected void btnCancelRemove_Click(object sender, EventArgs e)
    {
        panelRemoveTable.Visible = false;
        panelAddTable.Visible = false;

        btnTriggerRemoveTable.Visible = true;
        btnTriggerAddTable.Visible = true;
    }
    protected void btnTableSettings_Click(object sender, EventArgs e)
    {
        panelTableSettings.Visible = true;
        panelMenuSettings.Visible = false;
    }
    protected void btnMenuSettings_Click(object sender, EventArgs e)
    {
        panelTableSettings.Visible = false;
        panelMenuSettings.Visible = true;
    }
    protected void btnTriggerAddTable_Click(object sender, EventArgs e)
    {

        btnTriggerAddTable.Visible = false;
        btnTriggerRemoveTable.Visible = false;


        panelAddTable.Visible = true;
        panelRemoveTable.Visible = false;

        labelGizle();

    }
    protected void btnTriggerRemoveTable_Click(object sender, EventArgs e)
    {
        btnTriggerAddTable.Visible = false;
        btnTriggerRemoveTable.Visible = false;

        panelAddTable.Visible = false;
        panelRemoveTable.Visible = true;
        labelGizle();

    }
    protected void btnTriggerAddCategory_Click(object sender, EventArgs e)
    {
        panelAddCategory.Visible = true;
        panelRemoveCategory.Visible = false;
        panelAddProduct.Visible = false;
        panelRemoveProduct.Visible = false;
        labelGizle();

    }
    protected void btnTriggerRemoveCategory_Click(object sender, EventArgs e)
    {
        panelAddCategory.Visible = false;
        panelRemoveCategory.Visible = true;
        panelAddProduct.Visible = false;
        panelRemoveProduct.Visible = false;
        labelGizle();

    }
    protected void btnTriggerAddProduct_Click(object sender, EventArgs e)
    {
        panelAddCategory.Visible = false;
        panelRemoveCategory.Visible = false;
        panelAddProduct.Visible = true;
        panelRemoveProduct.Visible = false;
        labelGizle();

    }
    protected void btnTriggerRemoveProduct_Click(object sender, EventArgs e)
    {
        panelAddCategory.Visible = false;
        panelRemoveCategory.Visible = false;
        panelAddProduct.Visible = false;
        panelRemoveProduct.Visible = true;
        labelGizle();

    }
    protected void btnCancelAdd_Click(object sender, EventArgs e)
    {/*Cancels addTable.*/
        btnTriggerAddTable.Visible = true; ;
        btnTriggerRemoveTable.Visible = true;


        panelAddTable.Visible = false;
        panelRemoveTable.Visible = false;


    }
    protected void btnCloseRemoveProduct_Click(object sender, EventArgs e)
    {
        panelRemoveProduct.Visible = false;

    }
    protected void btnCloseAddProduct_Click(object sender, EventArgs e)
    {
        panelAddProduct.Visible = false;
    }
    #endregion

    #region Table Settings
    protected void btnSaveTable_Click(object sender, EventArgs e)
    {
        if (textBoxTableName.Text.Length != 0)
        {
            Table t = new Table();

            t.TableName = textBoxTableName.Text;
            t.IsReserved = 0;
            t.CompanyID = cmp.CompanyID;


            lblSaveTable.Visible = true;
            lblSaveTable.CssClass = "alert alert-success ";


            textBoxTableName.Text = "";
            refreshDropdownLists();
            settingUpdatePanel.DataBind();
        }
    }
    protected void btnRemoveTable_Click(object sender, EventArgs e)
    {
        //if (dropDownListTables.SelectedIndex != 0)
        //{
        //    Table t = new Table();

        //    t.TableID = int.Parse(dropDownListTables.SelectedValue);

        //    t.Delete();

        //    refreshDropdownLists();
        //    settingUpdatePanel.DataBind();
        //}
    }
    #endregion

    #region Category Settings
    protected void btnSaveCategory_Click(object sender, EventArgs e)
    {

        if (textBoxCategoryName.Text.Length != 0)
        {
            Category ctgry = new Category();

            ctgry.CategoryName = textBoxCategoryName.Text;
            ctgry.CompanyID = cmp.CompanyID;

            ctgry.Insert();

            lblSaveCategory.Visible = true;
            lblSaveCategory.CssClass = "alert alert-success ";


            textBoxCategoryName.Text = "";
            refreshDropdownLists();
            settingUpdatePanel.DataBind();
        }
    }
    protected void btnRemoveCategory_Click(object sender, EventArgs e)
    {
        if (dropDownListShowCategory.SelectedIndex != 0)
        {
            Category ctgry = new Category();

            ctgry.CategoryID = int.Parse(dropDownListShowCategory.SelectedValue);

            ctgry.Delete();

            refreshDropdownLists();
            settingUpdatePanel.DataBind();
            lblRemoveCategory.Visible = true;
        }
    }
    #endregion

    #region Product Settings
    protected void btnAddProduct_Click(object sender, EventArgs e)
    {
        if (
            txtProductCredit.Text != "" &&
            txtProductDetails.Text != "" &&
            txtProductName.Text != "" &&
            txtProductPrice.Text != "" &&
            ddlCategorySelect.SelectedIndex != 0
        )
        {
            Product p = new Product();

            p.ProductName = txtProductName.Text;
            p.Detail = txtProductDetails.Text;
            p.Price = float.Parse(txtProductPrice.Text);
            p.Credit = float.Parse(txtProductCredit.Text);
            p.CategoryID = int.Parse(ddlCategorySelect.SelectedValue);

            p.Insert();

            lblSaveProduct.Visible = true;
            refreshDropdownLists();
            settingUpdatePanel.DataBind();
            txtProductCredit.Text = "";
            txtProductDetails.Text = "";
            txtProductName.Text = "";
            txtProductPrice.Text = "";
        }
    }
    protected void btnRemoveProduct_Click(object sender, EventArgs e)
    {
        Product p = new Product();
        p.ProductID = int.Parse(ddlRemoveProduct.SelectedValue);
        p.Delete();


        lblRemoveProduct.Visible = true;
        refreshDropdownLists();
        settingUpdatePanel.DataBind();
    }
    #endregion

    #region Methods
    public void labelGizle()
    {
        /* Label visibilities 
            Ekleme ve çıkarma buttonları arasında gezinirken
            Önceki paneldeki ürün eklendi ya da silindi 
            yazıları göze hoş gelmediği için her butonla
            geçiş yapıldığında label ları gizledim.
        */
        lblSaveTable.Visible = false;
        lblRemoveTable.Visible = false;

        lblSaveCategory.Visible = false;
        lblRemoveCategory.Visible = false;

        lblSaveProduct.Visible = false;
        lblRemoveProduct.Visible = false;
    }

    public void refreshDropdownLists()
    {
        var tempItem = ddlCategorySelect.Items[0];
        ddlCategorySelect.Items.Clear();
        ddlCategorySelect.Items.Add(tempItem);

        tempItem = ddlRemoveProduct.Items[0];
        ddlRemoveProduct.Items.Clear();
        ddlRemoveProduct.Items.Add(tempItem);

        tempItem = dropDownListShowCategory.Items[0];
        dropDownListShowCategory.Items.Clear();
        dropDownListShowCategory.Items.Add(tempItem);

        //tempItem = dropDownListTables.Items[0];
        //dropDownListTables.Items.Clear();
        //dropDownListTables.Items.Add(tempItem);
    }
    #endregion

    protected void qrButton_Click(object sender, EventArgs e)
    {
        cmp.SaveQrPdf(Server.MapPath("~/Qr Codes"));

        string path = MapPath("~/Qr Codes") + "/" + cmp.GetQrPdfName();

        byte[] bts = System.IO.File.ReadAllBytes(path);

        Response.AddHeader("Content-Type", "Application/octet-stream");
        Response.AddHeader("Content-Length", bts.Length.ToString());
        Response.AddHeader("Content-Disposition", "attachment; filename=QrCodes.pdf");
        Response.BinaryWrite(bts);
        Response.Flush();
        Response.End();
    }
}