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
    public int Amount { get; set; }
    public DateTime OrderTime { get; set; }
    public int ControllerID { get; set; }
    public int ProductID { get; set; }
    public string OrderDetails { get; set; }

	public Order()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool Insert()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "INSERT INTO [Order] (Amount#, OrderTime, ControllerID, ProductID, OrderDetails) " + 
                                       "VALUES (@Amount#, @OrderTime, @ControllerID, @ProductID, @OrderDetails)";

        cmd.Parameters.AddWithValue("@Amount#", this.Amount);
        cmd.Parameters.AddWithValue("@OrderTime", this.OrderTime);
        cmd.Parameters.AddWithValue("@ControllerID", this.ControllerID);
        cmd.Parameters.AddWithValue("@ProductID", this.ProductID);
        cmd.Parameters.AddWithValue("@OrderDetails", this.OrderDetails);


        bool result = ExecuteNonQuery(cmd);

        if (result)
        {
            SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                                  );
            cmd.CommandText = "SELECT OrderID FROM [Order] " +
                               "WHERE (Amount# = @Amount#) " +
                                 "AND (OrderTime = @OrderTime) " +
                                 "AND (ControllerID = @ControllerID) " +
                                 "AND (ProductID = @ProductID) " +
                                 "AND (OrderDetails = @OrderDetails)";
            try
            {
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                dr.Read();

                this.OrderID = int.Parse(dr["OrderID"].ToString());
            }
            catch (Exception) { result = false; }
            finally { conn.Close(); }
        }
        return result;
    }
    public bool Update()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "UPDATE [Order] SET Amount# = @Amount#, ControllerID = @ControllerID, ProductID = @ProductID WHERE (OrderID = @OrderID)";

        cmd.Parameters.AddWithValue("@Amount#", this.Amount);
        cmd.Parameters.AddWithValue("@ControllerID", this.ControllerID);
        cmd.Parameters.AddWithValue("@ProductID", this.ProductID);
        cmd.Parameters.AddWithValue("@OrderID", this.OrderID);

        return ExecuteNonQuery(cmd);
    }

    /// <summary>
    /// Transfers the order to another table.
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public bool TransferTo(Table table)
    {
        if (table.ActiveController == null) return false;
        else
        {
            this.ControllerID = table.ActiveController.ControllerID;
            return true;
        }
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