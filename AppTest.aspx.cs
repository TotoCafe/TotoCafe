using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AppTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Company Insert Update Freeze Resume test
        Company cmp = new Company();

        cmp.Email = "sohos@sohos.com";
        cmp.Password = "ituolmazsaodtu";
        
        lbTest.Items.Add("Company inserting..");
        //lbTest.Items.Add(cmp.Insert().ToString());

        lbTest.Items.Add("Company initializing..");
        lbTest.Items.Add(cmp.Authenticate().ToString());

        lbTest.Items.Add("Company after initialize..");

        lbTest.Items.Add("ID: " + cmp.CompanyID);
        lbTest.Items.Add("Name: " + cmp.CompanyName);
        lbTest.Items.Add("Email: " + cmp.Email);
        lbTest.Items.Add("Password: " + cmp.Password);
        lbTest.Items.Add("Address: " + cmp.Address);
        lbTest.Items.Add("Phone#: " + cmp.Phone);
        lbTest.Items.Add("Location: " + cmp.Location);
        lbTest.Items.Add("WifiName: " + cmp.WirelessName);
        lbTest.Items.Add("WifiPassword: " + cmp.WirelessPassword);
        lbTest.Items.Add("CityID: " + cmp.CityID);
        lbTest.Items.Add("AvailabilityID: " + cmp.AvailabilityID);
        lbTest.Items.Add("PermissionID: " + cmp.PermissionID);

        lbTest.Items.Add("Company being updated..");

        cmp.Location = "Temp";
        cmp.WirelessPassword = "Password";
        cmp.WirelessName = "WName";

        //lbTest.Items.Add(cmp.Update().ToString());

        lbTest.Items.Add("Company after update..");

        lbTest.Items.Add("ID: " + cmp.CompanyID);
        lbTest.Items.Add("Name: " + cmp.CompanyName);
        lbTest.Items.Add("Email: " + cmp.Email);
        lbTest.Items.Add("Password: " + cmp.Password);
        lbTest.Items.Add("Address: " + cmp.Address);
        lbTest.Items.Add("Phone#: " + cmp.Phone);
        lbTest.Items.Add("Location: " + cmp.Location);
        lbTest.Items.Add("WifiName: " + cmp.WirelessName);
        lbTest.Items.Add("WifiPassword: " + cmp.WirelessPassword);
        lbTest.Items.Add("CityID: " + cmp.CityID);
        lbTest.Items.Add("AvailabilityID: " + cmp.AvailabilityID);
        lbTest.Items.Add("PermissionID: " + cmp.PermissionID);

        lbTest.Items.Add("Company freezing..");
        lbTest.Items.Add(cmp.Freeze().ToString());

        lbTest.Items.Add("AvailabilityID" + cmp.AvailabilityID);

        lbTest.Items.Add("Company resuming..");
        lbTest.Items.Add(cmp.Resume().ToString());

        lbTest.Items.Add("AvailabilityID" + cmp.AvailabilityID);

        Category c = (Category)cmp.Categories[1];

        lbTest.Items.Add(c.CategoryName);

        Product p = (Product)c.Products[1];

        lbTest.Items.Add(p.ProductName);

        lbTest.Items.Add("Tables in database..");

        List<Table> list = cmp.Tables.Values.Cast<Table>().ToList<Table>();

        foreach (Table tbl in list)
        {
            lbTest.Items.Add(tbl.TableName);
        }
        #endregion
    }
}