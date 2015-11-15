using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Test
/// </summary>
public class Test
{
    public Test()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public List<string> startTest()
    {
        List<Company> testObjectList = null;

        TestPackage tp = InitTestObjects();

        List<string> testLog = tp.Log;

        testObjectList = tp.Companies;

        int score = tp.TestScore;

        testLog.Add("Query test begin.. " + DateTime.Now.ToString());

        foreach (Company c in testObjectList)
            c.Initialize(c.Email);

        try
        {
            foreach (Company c in testObjectList)
            {

                testLog.Add(c.CompanyID.ToString() + " " + c.CompanyName);

                foreach (Category c1 in c.CategoryList)
                {
                    testLog.Add(c.CompanyID.ToString() + " " + c.CompanyName + "  " + c1.CategoryID.ToString() + " - " + c1.CategoryName);

                    foreach (Product p in c1.ProductList)
                    {
                        testLog.Add(c.CompanyID.ToString() + " " + c.CompanyName + "  " + c1.CategoryID.ToString() + " - " + c1.CategoryName + " " + p.ProductID.ToString() + " - " + p.ProductName);
                    }
                }
                foreach (Table t in c.TableList)
                {
                    testLog.Add(c.CompanyID.ToString() + " " + c.CompanyName + "  " + t.TableID.ToString() + " - " + t.TableName);


                }
            }
        }
        catch (Exception) { score--; }

        testLog.Add("Query test end.. " + DateTime.Now.ToString());
        testLog.Add("-----------------------------------------------------------------------------------------------");

        testLog.Add("Update test begin..");
        foreach (Company c in testObjectList)
        {


            testLog.Add(c.CompanyName + " being updated.. " + DateTime.Now.ToString());

            c.CompanyName += "-Updated";
            c.Address += "-Updated";
            c.Phone += "-Updated";
            c.Location += "-Updated";
            c.Pasword += "-Updated";

            if (c.Update())
            {
                testLog.Add("Successful.. " + DateTime.Now.ToString());

                foreach (Table t in c.TableList)
                {
                    t.TableName += "-Updated";
                    testLog.Add(c.CompanyName + " - TableID=" + t.TableID.ToString() + " being updated.. " + DateTime.Now.ToString());
                    if (t.Update())
                    {
                        testLog.Add("Successful.. " + DateTime.Now.ToString());


                    }
                    else { testLog.Add("Failed.. " + DateTime.Now.ToString()); score--; }
                }

                foreach (Category ct in c.CategoryList)
                {
                    ct.CategoryName += "Updated";
                    testLog.Add(c.CompanyName + " - CategoryID=" + ct.CategoryID.ToString() + " being updated.. " + DateTime.Now.ToString());
                    if (ct.Update())
                    {
                        testLog.Add("Successful.. " + DateTime.Now.ToString());

                        foreach (Product p in ct.ProductList)
                        {
                            testLog.Add(c.CompanyName + " - CategoryID=" + ct.CategoryID.ToString() + " ProductID=" + p.ProductID + " being updated.. " + DateTime.Now.ToString());

                            p.ProductName += "Updated";
                            p.Detail += "Updated";
                            p.Price = float.Parse("20,25");

                            if (p.Update()) testLog.Add("Successful.. " + DateTime.Now.ToString());
                            else { testLog.Add("Failed.. " + DateTime.Now.ToString()); score--; }

                        }
                    }
                    else { testLog.Add("Failed.. " + DateTime.Now.ToString()); score--; }
                }
            }
            else { testLog.Add("Failed.. " + DateTime.Now.ToString()); score--; }
            testLog.Add("Update test end..");
            testLog.Add("-----------------------------------------------------------------------------------------------");



        }
        testLog.Add("Query test AFTER UPDATES begin.. " + DateTime.Now.ToString());

        foreach (Company c in testObjectList)
        {
            try
            {

                testLog.Add(c.CompanyID.ToString() + " " + c.CompanyName);

                foreach (Category c1 in c.CategoryList)
                {
                    testLog.Add(c.CompanyID.ToString() + " " + c.CompanyName + "  " + c1.CategoryID.ToString() + " - " + c1.CategoryName);

                    foreach (Product p in c1.ProductList)
                    {
                        testLog.Add(c.CompanyID.ToString() + " " + c.CompanyName + "  " + c1.CategoryID.ToString() + " - " + c1.CategoryName + " " + p.ProductID.ToString() + " - " + p.ProductName);
                    }
                }
                foreach (Table t in c.TableList)
                {
                    testLog.Add(c.CompanyID.ToString() + " " + c.CompanyName + "  " + t.TableID.ToString() + " - " + t.TableName);


                }

            }
            catch (Exception) { score--; }
        }

        testLog.Add("Query test AFTER UPDATES end.. " + DateTime.Now.ToString());
        testLog.Add("-----------------------------------------------------------------------------------------------");

        /*foreach (Company c in testObjectList)
        {
            testLog.Add(c.CompanyName + " is deleting..");

            if (c.Delete()) testLog.Add("Successful.. " + DateTime.Now.ToString()); //Company deletion starts all deletion operation on related tables in the DB.
            else { testLog.Add("Failed.. " + DateTime.Now.ToString()); score--; }   //If any exception occurs while deleting any data from database it won't complete the deletion of current company object.

        }*/
        testLog.Add("Deletion test end..");
        testLog.Add("-----------------------------------------------------------------------------------------------");

        if (!(score < 0)) testLog.Add("Test Completed! Result: Passed!!");
        else testLog.Add("Test Completed! Result: Failed!!");
        testLog.Add("-----------------------------------------------------------------------------------------------");

        return testLog;
    }
    public TestPackage InitTestObjects()
    {
        List<string> initLog = new List<string>();

        initLog.Add("Init test log begin.." + DateTime.Now.ToString());

        int score = 0;

        List<Company> cList = new List<Company>();

        for (int i = 1; i <= 2; i++)
        {
            initLog.Add("Company-" + i.ToString() + " is inserting.. " + DateTime.Now.ToString());
            //Add 10 company to database..
            Company c = new Company();
            if (c.Insert("Company-" + i.ToString(),
                        "Email-" + i.ToString(),
                        "Password-" + i.ToString(),
                        "Address-" + i.ToString(),
                        "Phone-" + i.ToString(), i))
            {
                cList.Add(c);
                initLog.Add("Successful.. " + DateTime.Now.ToString());

                initLog.Add("Company-" + i.ToString() + " is initializing.. " + DateTime.Now.ToString());
                c.Initialize(FormsAuthentication.HashPasswordForStoringInConfigFile("Email-" + i.ToString(), "SHA1"));//Company inserted database we must initialize it with Email before starting to use..
                //Add categories and their products..
                for (int j = 1; j <= 2; j++)
                {
                    Category ctgry = new Category();
                    ctgry.CategoryName = "Category-" + j.ToString();
                    ctgry.CompanyID = c.CompanyID;
                    initLog.Add("Inserting Category-" + j.ToString() + " for Company-" + i.ToString() + " " + DateTime.Now.ToString());
                    if (ctgry.Insert())
                    {
                        initLog.Add("Successful.. " + DateTime.Now.ToString());
                        for (int k = 1; k <= 2; k++)
                        {
                            initLog.Add("Inserting Product-" + k.ToString() + " for Category-" + j.ToString() + " " + DateTime.Now.ToString());
                            Product p = new Product();
                            p.CategoryID = ctgry.CategoryID;
                            p.Detail = "Detail-" + k.ToString();
                            p.Price = float.Parse(k.ToString());
                            p.Credit = float.Parse(k.ToString());
                            p.ProductName = "ProductName-" + k.ToString();

                            if (p.Insert()) initLog.Add("Successful.. " + DateTime.Now.ToString());
                            else { initLog.Add("Failed.. " + DateTime.Now.ToString()); score -= 1; }
                        }
                    }
                    else { initLog.Add("Failed.. " + DateTime.Now.ToString()); score -= 1; }

                    initLog.Add("Inserting Table-" + j.ToString() + " for Company-" + i.ToString() + " " + DateTime.Now.ToString());
                    //Add tables..
                    Table t = new Table();
                    t.TableName = "Table-" + j.ToString();
                    t.CompanyID = c.CompanyID;

                    if (t.Insert())
                    {
                        initLog.Add("Successful.. " + DateTime.Now.ToString());

                    }
                    else { initLog.Add("Failed.. " + DateTime.Now.ToString()); score -= 1; }
                }
            }
            else { initLog.Add("Failed.. " + DateTime.Now.ToString()); score -= 1; }
        }
        initLog.Add("Init test log end.." + DateTime.Now.ToString());
        initLog.Add("-----------------------------------------------------------------------------------------------");

        TestPackage tp = new TestPackage();

        tp.Companies = cList;
        tp.Log = initLog;
        tp.TestScore = score;

        return tp;
    }
}
public class TestPackage
{
    public List<Company> Companies { get; set; }
    public List<string> Log { get; set; }
    public int TestScore { get; set; }
}