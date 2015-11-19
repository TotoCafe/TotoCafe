using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Product
/// </summary>
public class Product
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public string Detail { get; set; }
    public float Price { get; set; }
    public float Credit { get; set; }
    public int AvailabilityID { get; set; }
    public int CategoryID { get; set; }

	public Product()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool Insert()
    {
        SqlCommand cmd = new SqlCommand();
        
        cmd.CommandText = "INSERT INTO Product(ProductName, Detail, Price, Credit, CategoryID) VALUES (@ProductName, @Detail, @Price, @Credit, @CategoryID)";

        cmd.Parameters.AddWithValue("@ProductName", this.ProductName);
        cmd.Parameters.AddWithValue("@Detail", this.Detail);
        cmd.Parameters.AddWithValue("@Price", this.Price);
        cmd.Parameters.AddWithValue("@Credit", this.Credit);
        cmd.Parameters.AddWithValue("@CategoryID", this.CategoryID);
        this.AvailabilityID = 1;//Default value..

        return ExecuteNonQuery(cmd);
    }

    public bool Update()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "UPDATE Product SET ProductName = @ProductName, " +
                                             "Detail = @Detail, " +
                                             "Price = @Price, " +
                                             "Credit = @Credit, " +
                                             "CategoryID = @CategoryID, " +
                                             "AvailabilityID = @AvailabilityID " +
                                       "WHERE (ProductID = @ProductID)";

        cmd.Parameters.AddWithValue("@ProductName", this.ProductName);
        cmd.Parameters.AddWithValue("@Detail", this.Detail);
        cmd.Parameters.AddWithValue("@Price", this.Price);
        cmd.Parameters.AddWithValue("@Credit", this.Credit);
        cmd.Parameters.AddWithValue("@CategoryID", this.CategoryID);
        cmd.Parameters.AddWithValue("@AvailabilityID", this.AvailabilityID);
        cmd.Parameters.AddWithValue("@ProductID", this.ProductID);

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
        catch (Exception) {  }
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
        catch (Exception) {  }
        finally
        {
            conn.Close();
        }

        return this.Update();
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