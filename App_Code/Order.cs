using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Order
/// </summary>
public class Order
{
    public int OrderID { get; set; }
    public string ProductName { get; set; }
    public float ProductPrice { get; set; }
    public int Amount { get; set; }
    public DateTime OrderTime { get; set; }
    public int ControllerID { get; set; }

	public Order()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool Insert()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "INSERT INTO [Order] (ProductName, ProductPrice, Amount#, OrderTime, ControllerID) " + 
                                       "VALUES (@ProductName, @ProductPrice, @Amount#, @OrderTime, @ControlleID)";
        cmd.Parameters.AddWithValue("@ProductName", this.ProductName);
        cmd.Parameters.AddWithValue("@ProductPrice", this.ProductPrice);
        cmd.Parameters.AddWithValue("@Amount#", this.Amount);
        cmd.Parameters.AddWithValue("@OrderTime", this.OrderTime);
        cmd.Parameters.AddWithValue("@ControllerID", this.ControllerID);

        return ExecuteNonQuery(cmd);
    }
    public bool Delete()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "DELETE FROM [Order] WHERE (OrderID = @OrderID)";
        cmd.Parameters.AddWithValue("@OrderID", this.OrderID);

        return ExecuteNonQuery(cmd);
    }
    public bool Update()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "UPDATE [Order] SET ProductName = @ProductName, " +
                                             "ProductPrice = @ProductPrice, " +
                                             "Amount# = @Amount#, " +
                                             "OrderTime = @OrderTime, " +
                                             "ControllerID = @ControllerID" +
                          "WHERE             (OrderID = @OrderID)";

        cmd.Parameters.AddWithValue("@ProductName", this.ProductName);
        cmd.Parameters.AddWithValue("@ProductPrice", this.ProductPrice);
        cmd.Parameters.AddWithValue("@Amount#", this.Amount);
        cmd.Parameters.AddWithValue("@OrderTime", this.OrderTime);
        cmd.Parameters.AddWithValue("@ControllerID", this.ControllerID);
        cmd.Parameters.AddWithValue("@OrderID", this.OrderID);

        return ExecuteNonQuery(cmd);
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