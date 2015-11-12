using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TableController
/// </summary>
public class TableController
{
    public int ControllerID { get; set; }
    public int CostumerID { get; set; }
    public int TableID { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime FinishDateTime { get; set; }
    public List<Order> OrderList { get; set; }
    public int CompanyID { get; set; }

    public void InitOrderList()
    {
        List<Order> OrderList = new List<Order>();

        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
            );

        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "SELECT * FROM [Order] WHERE ([ControllerID] = @ControllerID)";
        cmd.Parameters.AddWithValue("@ControllerID", this.ControllerID);

        cmd.Connection = conn;

        try
        {
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Order o = new Order();

                o.OrderID = int.Parse(dr["OrderID"].ToString());
                o.ProductName = dr["ProductName"].ToString();
                o.ProductPrice = float.Parse(dr["ProductPrice"].ToString());
                o.Amount = int.Parse(dr["Amount#"].ToString());
                IFormatProvider culture = new CultureInfo("en-US", true);
                o.OrderTime = DateTime.ParseExact(dr["StartDateTime"].ToString(), "dd/MM/yyyy HH:mm:ss.fff", culture);
                o.ControllerID = this.ControllerID;

                OrderList.Add(o);
            }
        }
        catch (Exception) { /*Handle excepsions..*/ }
        finally
        {
            conn.Close();
        }

        this.OrderList = OrderList;
    }

    public TableController()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool Insert()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "INSERT INTO TableController(CostumerID, TableID, StartDateTime, FinishDateTime, CompanyID) " +
                                 "VALUES              (@CostumerID, @TableID, @StartDateTime, @FinishDateTime, @CompanyID)";

        cmd.Parameters.AddWithValue("@CostumerID", this.CostumerID);
        cmd.Parameters.AddWithValue("@TableID", this.TableID);
        cmd.Parameters.AddWithValue("@StartDateTime", this.StartDateTime);
        cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
        // this.FinishDateTime will be set 'null' since the table is opened.

        return ExecuteNonQuery(cmd);
    }
    public bool Delete()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "DELETE FROM TableController WHERE (ControllerID = @ControllerID)";

        cmd.Parameters.AddWithValue("@ControllerID", this.ControllerID);

        foreach (Order o in this.OrderList) o.Delete();

        return ExecuteNonQuery(cmd);
    }
    public bool Update()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "UPDATE TableController SET CostumerID = @CostumerID, " +
                                                     "TableID = @TableID, " +
                                                     "StartDateTime = @StartDateTime, " +
                                                     "FinishDateTime = @FinishDateTime" +
                          "WHERE                     (ControllerID = @ControllerID)";

        cmd.Parameters.AddWithValue("@CostumerID", this.CostumerID);
        cmd.Parameters.AddWithValue("@TableID", this.TableID);
        cmd.Parameters.AddWithValue("@StartDateTime", this.StartDateTime);
        cmd.Parameters.AddWithValue("@FinishDateTime", this.FinishDateTime);
        cmd.Parameters.AddWithValue("@ControllerID", this.ControllerID);

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