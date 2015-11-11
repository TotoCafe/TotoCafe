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

    public bool Insert() { return true; }
    public bool Delete() { return true; }
    public bool Update() { return true; }
}