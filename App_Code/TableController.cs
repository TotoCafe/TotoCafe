﻿using System;
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

    public TableController()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool Insert()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "INSERT INTO TableController(CostumerID, TableID, StartDateTime) VALUES (@CostumerID, @TableID, @StartDateTime)";

        cmd.Parameters.AddWithValue("@CostumerID", this.CostumerID);
        cmd.Parameters.AddWithValue("@TableID", this.TableID);
        cmd.Parameters.AddWithValue("@StartDateTime", this.StartDateTime);

        bool result = ExecuteNonQuery(cmd);

        if (result)
        {
            SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                                  );
            cmd.Parameters.Clear();

            cmd.CommandText = "SELECT ControllerID FROM TableController " + 
                               "WHERE (CostumerID = @CostumerID) AND (TableID = @TableID) AND (StartDateTime = @StartDateTime) AND (FinishDateTime IS NULL)";
            cmd.Parameters.AddWithValue("@CostumerID", this.CostumerID);
            cmd.Parameters.AddWithValue("@TableID", this.TableID);
            cmd.Parameters.AddWithValue("@StartDateTime", this.StartDateTime);

            cmd.Connection = conn;

            try
            {
                conn.Open();

                SqlDataReader dr = new SqlDataReader();

                dr.Read();

                this.ControllerID = int.Parse(dr["ControllerID"].ToString());
            }
            catch (Exception) { }
            finally { conn.Close(); }
        }
        return result;
    }

    public bool Update()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "UPDATE TableController SET TableID = @TableID, FinishDateTime = @FinishDateTime WHERE (ControllerID = @ControllerID)";

        cmd.Parameters.AddWithValue("@TableID", this.TableID);
        cmd.Parameters.AddWithValue("@FinishDateTime", this.FinishDateTime);
        cmd.Parameters.AddWithValue("@ControllerID", this.ControllerID);

        return ExecuteNonQuery(cmd);
    }

    public List<Order> getOrderList()
    {
        List<Order> orderList = new List<Order>();

        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "SELECT [Order].* FROM [Order] WHERE (ControllerID = @ControllerID)";

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
                o.Amount = int.Parse(dr["Amount#"].ToString()); 
                IFormatProvider culture = new CultureInfo("en-US", true);
                o.OrderTime = DateTime.ParseExact(dr["OrderTime"].ToString(), "dd/MM/yyyy HH:mm:ss.fff", culture);
                o.ControllerID = int.Parse(dr["ControllerID"].ToString());
                o.ProductID = int.Parse(dr["ProductID"].ToString());
                o.OrderDetails = dr["OrderDetails"].ToString();

                orderList.Add(o);
            }
        }
        catch (Exception) { }
        finally { conn.Close(); }
        return orderList;
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