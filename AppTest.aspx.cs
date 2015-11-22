using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class AppTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region asdasd

        //lbTest.Items.Add("Company inserting..");
        ////lbTest.Items.Add(cmp.Insert().ToString());

        //lbTest.Items.Add("Company initializing..");
        //lbTest.Items.Add(cmp.Authenticate().ToString());

        //lbTest.Items.Add("Company after initialize..");

        //lbTest.Items.Add("ID: " + cmp.CompanyID);
        //lbTest.Items.Add("Name: " + cmp.CompanyName);
        //lbTest.Items.Add("Email: " + cmp.Email);
        //lbTest.Items.Add("Password: " + cmp.Password);
        //lbTest.Items.Add("Address: " + cmp.Address);
        //lbTest.Items.Add("Phone#: " + cmp.Phone);
        //lbTest.Items.Add("Location: " + cmp.Location);
        //lbTest.Items.Add("WifiName: " + cmp.WirelessName);
        //lbTest.Items.Add("WifiPassword: " + cmp.WirelessPassword);
        //lbTest.Items.Add("CityID: " + cmp.CityID);
        //lbTest.Items.Add("AvailabilityID: " + cmp.AvailabilityID);
        //lbTest.Items.Add("PermissionID: " + cmp.PermissionID);

        //lbTest.Items.Add("Company being updated..");

        //cmp.Location = "Temp";
        //cmp.WirelessPassword = "Password";
        //cmp.WirelessName = "WName";

        ////lbTest.Items.Add(cmp.Update().ToString());

        //lbTest.Items.Add("Company after update..");

        //lbTest.Items.Add("ID: " + cmp.CompanyID);
        //lbTest.Items.Add("Name: " + cmp.CompanyName);
        //lbTest.Items.Add("Email: " + cmp.Email);
        //lbTest.Items.Add("Password: " + cmp.Password);
        //lbTest.Items.Add("Address: " + cmp.Address);
        //lbTest.Items.Add("Phone#: " + cmp.Phone);
        //lbTest.Items.Add("Location: " + cmp.Location);
        //lbTest.Items.Add("WifiName: " + cmp.WirelessName);
        //lbTest.Items.Add("WifiPassword: " + cmp.WirelessPassword);
        //lbTest.Items.Add("CityID: " + cmp.CityID);
        //lbTest.Items.Add("AvailabilityID: " + cmp.AvailabilityID);
        //lbTest.Items.Add("PermissionID: " + cmp.PermissionID);

        //lbTest.Items.Add("Company freezing..");
        //lbTest.Items.Add(cmp.Freeze().ToString());

        //lbTest.Items.Add("AvailabilityID" + cmp.AvailabilityID);

        //lbTest.Items.Add("Company resuming..");
        //lbTest.Items.Add(cmp.Resume().ToString());

        //lbTest.Items.Add("AvailabilityID" + cmp.AvailabilityID);

        //cmp.Authenticate();

        //lbTest.Items.Add("Tables...");
        //List<Table> list = cmp.GetTableList();

        //foreach (Table tbl in list)
        //{
        //    lbTest.Items.Add(tbl.TableName);
        //}

        //Table table = cmp.GetTableWithId(39);

        //table.TableName = "mvbönvçnvm";

        //cmp.UpdateTable(table);

        //lbTest.Items.Add("tables after update..");

        //list = cmp.GetTableList();

        //foreach (Table tbl in list)
        //{
        //    lbTest.Items.Add(tbl.TableName);
        //}

        //lbTest.Items.Add("Category adding..");

        //Category c = new Category();

        //c.CategoryName = "dsfsdfsd";

        //cmp.AddCategory(c);

        //c = new Category();

        //c.CategoryName = "fhfghfgh";

        //lbTest.Items.Add("Categories");

        //foreach (Category ctgry in cmp.GetCategoryList())
        //    lbTest.Items.Add(ctgry.CategoryName);

        //lbTest.Items.Add("Categories after update..");
        //Category c;

        //c = cmp.GetCategoryWithId(3);
        //c.CategoryName = "ğpoyıouoıyoy";


        //cmp.UpdateCategory(c);


        //foreach (Category ctgry in cmp.GetCategoryList())
        //    lbTest.Items.Add(ctgry.CategoryName);
        #endregion

        Company cmp = new Company();

        cmp.Email = "sohos@sohos.com";
        cmp.Password = "ituolmazsaodtu";

        cmp.Authenticate();

        foreach (Table tbl in cmp.GetTableList())
        {
            lbTest.Items.Add(tbl.TableName);
        }
    }
}