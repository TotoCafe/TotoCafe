using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Collections;
using System.Collections.Concurrent;

/// <summary>
/// Summary description for Category
/// </summary>
public class Category
{
    //public int CategoryID { get; set; }
    //public string CategoryName { get; set; }
    //public int CompanyID { get; set; }
    //public List<Product> ProductList { get; set; }

    //private void SetCategoryID()
    //{
    //    SqlConnection conn = new SqlConnection(
    //        ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
    //                                          );
    //    SqlCommand cmd = new SqlCommand();

    //    cmd.CommandText = "SELECT CategoryID FROM Category WHERE (CategoryName = @CategoryName AND CompanyID = @CompanyID)";
    //    cmd.Parameters.AddWithValue("@CategoryName", this.CategoryName);
    //    cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
    //    cmd.Connection = conn;

    //    int CategoryID = 0;

    //    try
    //    {
    //        conn.Open();

    //        SqlDataReader dr = cmd.ExecuteReader();

    //        dr.Read();//There should be only one record with a name in database..

    //        CategoryID = int.Parse(dr["CategoryID"].ToString());
    //    }
    //    catch (Exception) { }
    //    finally
    //    {
    //        conn.Close();
    //        this.CategoryID = CategoryID;
    //    }
    //}
    //public void InitProductList()
    //{
    //    List<Product> ProductList = new List<Product>();

    //    SqlConnection conn = new SqlConnection(
    //        ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
    //        );
    //    SqlCommand cmd = new SqlCommand();

    //    cmd.Connection = conn;
    //    cmd.CommandText = "SELECT ProductID, ProductName, Detail, Price, Credit FROM Product WHERE (CategoryID = @CategoryID)";

    //    cmd.Parameters.AddWithValue("@CategoryID", this.CategoryID);

    //    try
    //    {
    //        conn.Open();

    //        SqlDataReader dr = cmd.ExecuteReader();

    //        while (dr.Read())
    //        {
    //            Product p = new Product();

    //            p.ProductID = int.Parse(dr["ProductID"].ToString());
    //            p.ProductName = dr["ProductName"].ToString();
    //            p.Detail = dr["Detail"].ToString();
    //            p.Price = float.Parse(dr["Price"].ToString());
    //            p.Credit = float.Parse(dr["Credit"].ToString());
    //            p.CategoryID = this.CategoryID;

    //            ProductList.Add(p);
    //        }
    //    }
    //    catch (Exception) { }
    //    finally
    //    {
    //        conn.Close();
    //    }

    //    this.ProductList = ProductList;
    //}
    //public Category()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}

    //public bool Insert()
    //{
    //    SqlCommand cmd = new SqlCommand();

    //    cmd.CommandText = "INSERT INTO Category(CategoryName, CompanyID) VALUES (@CategoryName, @CompanyID)";

    //    cmd.Parameters.AddWithValue("@CategoryName", this.CategoryName);
    //    cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

    //    bool isDone = ExecuteNonQuery(cmd);//If it fails ID will be set '0'!! Handle it..

    //    SetCategoryID();

    //    return isDone;
    //}
    //public bool Delete()
    //{
    //    SqlCommand cmd = new SqlCommand();

    //    cmd.CommandText = "DELETE FROM Category WHERE (CategoryID = @CategoryID)";

    //    cmd.Parameters.AddWithValue("@CategoryID", this.CategoryID);

    //    foreach (Product p in this.ProductList) p.Delete();

    //    return ExecuteNonQuery(cmd);
    //}
    //public bool Update()
    //{
    //    SqlCommand cmd = new SqlCommand();

    //    cmd.CommandText = "UPDATE Category SET CategoryName = @CategoryName WHERE (CategoryID = @CategoryID)";

    //    cmd.Parameters.AddWithValue("@CategoryName", this.CategoryName);
    //    cmd.Parameters.AddWithValue("@CategoryID", this.CategoryID);

    //    return ExecuteNonQuery(cmd);
    //}
    public int CategoryID { get; set; }
    public string CategoryName { get; set; }
    public int AvailabilityID { get; set; }
    public int CompanyID { get; set; }
    private HashSet<Product> Products;
    public HashSet<Product> GetProducts
    {
        get { return this.Products; }
    }

    public Category()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool Insert()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "INSERT INTO Category(CategoryName, CompanyID) VALUES (@CategoryName, @CompanyID)";

        cmd.Parameters.AddWithValue("@CategoryName", this.CategoryName);
        cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
        this.AvailabilityID = 1;//Default value..

        if (ExecuteNonQuery(cmd))
        {
            SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                                  );
            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT CategoryID FROM Category " +
                                               "WHERE (CategoryName = @CategoryName) " +
                                                 "AND (AvailabilityID = @AvailabilityID) " +
                                                 "AND (CompanyID = @CompanyID)";
            cmd.Parameters.AddWithValue("@CategoryName", this.CategoryName);
            cmd.Parameters.AddWithValue("@AvailabilityID", this.AvailabilityID);
            cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

            cmd.Connection = conn;

            bool isSuccess = true;
            try
            {
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                dr.Read();

                this.CategoryID = int.Parse(dr["CategoryID"].ToString());
            }
            catch (Exception) { isSuccess = false; }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        else { return false; }
    }

    public bool Update()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "UPDATE Category SET CategoryName = @CategoryName, AvailabilityID = @AvailabilityID WHERE (CategoryID = @CategoryID)";

        cmd.Parameters.AddWithValue("@CategoryName", this.CategoryName);
        cmd.Parameters.AddWithValue("@AvailabilityID", this.AvailabilityID);
        cmd.Parameters.AddWithValue("@CategoryID", this.CategoryID);

        return ExecuteNonQuery(cmd);
    }

    public bool Freeze()
    {
        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "SELECT AvailabilityID FROM Availability WHERE (Availability = @Availability)";

        cmd.Parameters.AddWithValue("@Availability", "FROZEN");

        cmd.Connection = conn;

        try
        {
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();//Only one record.
            this.AvailabilityID = int.Parse(dr["AvailabilityID"].ToString());
        }
        catch (Exception) { }
        finally
        {
            conn.Close();
        }

        return this.Update();
    }

    public bool Resume()
    {
        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "SELECT AvailabilityID FROM Availability WHERE (Availability = @Availability)";

        cmd.Parameters.AddWithValue("@Availability", "AVAILABLE");

        cmd.Connection = conn;

        try
        {
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();//Only one record.
            this.AvailabilityID = int.Parse(dr["AvailabilityID"].ToString());
        }
        catch (Exception) { }
        finally
        {
            conn.Close();
        }

        return this.Update();
    }

    public bool isExist()
    {
        SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                                  );
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT COUNT(*)   FROM Category " +
                                           "WHERE (CategoryName = @CategoryName) " +
                                             "AND (AvailabilityID = @AvailabilityID) " +
                                             "AND (CompanyID = @CompanyID)";
        cmd.Parameters.AddWithValue("@CategoryName", this.CategoryName);
        cmd.Parameters.AddWithValue("@AvailabilityID", this.AvailabilityID);
        cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

        cmd.Connection = conn;

        bool isSuccess = true;
        try
        {
            conn.Open();

            if (int.Parse(cmd.ExecuteScalar().ToString()) == 0) isSuccess = false;
        }
        catch (Exception) { isSuccess = false; }
        finally
        {
            conn.Close();
        }
        return isSuccess;
    }

    public void InitProductList()
    {
        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "SELECT Product.* FROM Product " +
                                     "INNER JOIN Availability ON Product.AvailabilityID = Availability.AvailabilityID " +
                                     "WHERE (Availability.Availability = @Availability) AND (Product.CategoryID = @CategoryID)";
        cmd.Parameters.AddWithValue("@Availability", "AVAILABLE");
        cmd.Parameters.AddWithValue("@CategoryID", this.CategoryID);

        cmd.Connection = conn;

        this.Products = new HashSet<Product>();
        try
        {
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Product p = new Product();

                p.ProductID = int.Parse(dr["ProductID"].ToString());
                p.ProductName = dr["ProductName"].ToString();
                p.Detail = dr["Detail"].ToString();
                p.Price = float.Parse(dr["Price"].ToString());
                p.Credit = float.Parse(dr["Credit"].ToString());
                p.AvailabilityID = int.Parse(dr["AvailabilityID"].ToString());
                p.CategoryID = this.CategoryID;
                Products.Add(p);
            }
        }
        catch (Exception) { }
        finally { conn.Close(); }
    }

    private bool ExecuteNonQuery(SqlCommand cmd)
    {
        bool isSuccess = true;

        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        cmd.Connection = conn;

        try
        {
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception) { isSuccess = false; }
        finally
        {
            conn.Close();
        }
        return isSuccess;
    }
}