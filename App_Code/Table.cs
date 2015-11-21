using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Table
/// </summary>
public class Table
{
    public int TableID { get; set; }
    public string TableName { get; set; }
    public string QrCode { get; set; }
    public int CompanyID { get; set; }
    public int AvailabilityID { get; set; }
    public TableController ActiveController { get; set; }

    public Table()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool Insert()
    {
        this.AvailabilityID = 1;//Default..
        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr = null;

        cmd.CommandText = "SELECT [Table].TableID FROM [Table] " +
                                           "INNER JOIN Availability ON [Table].AvailabilityID = Availability.AvailabilityID " +
                                           "WHERE ([Table].TableName = @TableName) AND (Availability.Availability = @Availability) AND ([Table].CompanyID = @CompanyID)";
        cmd.Parameters.AddWithValue("@TableName", this.TableName);
        cmd.Parameters.AddWithValue("@Availability", "FROZEN");
        cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

        cmd.Connection = conn;

        bool result;
        try
        {
            conn.Open();

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();//There should be just one record.
                this.TableID = int.Parse(dr["TableID"].ToString());
                result = this.Resume();//Frozen record will be resume..
            }
            else
            {
                cmd.Parameters.Clear();

                cmd.CommandText = "INSERT INTO [Table] (TableName, CompanyID) VALUES (@TableName, @CompanyID)";

                cmd.Parameters.AddWithValue("@TableName", this.TableName);
                cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

                result = ExecuteNonQuery(cmd);

                if (result)
                {
                    this.TableID = getTableId();

                    this.QrCode = "TotoCafe:-" + this.CompanyID.ToString() + "-" + this.TableID.ToString();
                }
            }
        }
        catch (Exception) { result = false; }
        finally { conn.Close(); }
        return result;
    }

    public bool Update()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "UPDATE       [Table]" +
                          "SET          TableName = @TableName, " +
                                        "AvailabilityID = @AvailabilityID " +
                          "WHERE        (TableID = @TableID)";

        cmd.Parameters.AddWithValue("@TableName", this.TableName);
        cmd.Parameters.AddWithValue("@AvailabilityID", this.AvailabilityID);
        cmd.Parameters.AddWithValue("@TableID", this.TableID);

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
            SqlDataReader dr = cmd.ExecuteReader();//Only one record.
            dr.Read();//Only one record.
            this.AvailabilityID = int.Parse(dr["AvailabilityID"].ToString());
        }
        catch (Exception) { return false; }
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
            SqlDataReader dr = cmd.ExecuteReader();//Only one record.
            dr.Read();//Only one record.
            this.AvailabilityID = int.Parse(dr["AvailabilityID"].ToString());
        }
        catch (Exception) { return false; }
        finally
        {
            conn.Close();
        }

        return this.Update();
    }

    private int getTableId()
    {
        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        SqlCommand cmd = new SqlCommand();

        cmd.Parameters.Clear();

        cmd.CommandText = "SELECT TableID FROM [Table] WHERE (TableName = @TableName) AND (CompanyID = @CompanyID)";

        cmd.Parameters.AddWithValue("@TableName", this.TableName);
        cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
        cmd.Connection = conn;

        int id = 0;
        try
        {
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();

            id = int.Parse(dr["TableID"].ToString());
        }
        catch (Exception) { }
        finally { conn.Close(); }
        return id;
    }

    /// <summary>
    /// Initializes active controller when company first login.
    /// This gives us company can see the current view even it logs out and in again and again.
    /// </summary>
    public void InitActiveController()
    {
        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "SELECT ControllerID, CostumerID, TableID, StartDateTime " +
                            "FROM TableController " +
                           "WHERE (TableID = @TableID) AND (FinishDateTime IS NULL)";
        cmd.Parameters.AddWithValue("@TableID", this.TableID);
        cmd.Connection = conn;

        try
        {
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();//Must be only one record.

            TableController tc = new TableController();

            tc.ControllerID = int.Parse(dr["ControllerID"].ToString());
            tc.CostumerID = int.Parse(dr["CostumerID"].ToString());
            tc.TableID = int.Parse(dr["TableID"].ToString());
            DateTime t;
            DateTime.TryParse(dr["StartDateTime"].ToString(), out t);
            tc.StartDateTime = t;
            this.ActiveController = tc;
        }
        catch (Exception) { }
        finally { conn.Close(); }
    }

    /// <summary>
    /// Inserts a record into TableController table and sets the controller object
    /// to the table's active controller. This means this table is active until the payment
    /// </summary>
    /// <param name="CostumerID"></param>
    public void StartTableForCostumer(int CostumerID)
    {
        TableController tc = new TableController();

        tc.TableID = this.TableID;
        tc.CostumerID = CostumerID;
        tc.StartDateTime = DateTime.Now;

        tc.Insert();

        this.ActiveController = tc;
    }

    public void CloseTable()
    {
        this.ActiveController.FinishDateTime = DateTime.Now;
        this.ActiveController.Update();
        this.ActiveController = null;
    }

    /// <summary>
    /// Transfers the orders belong to a table to another table
    /// </summary>
    /// <param name="table"></param>
    public void TransferTo(Table table)
    {
        this.ActiveController.TableID = table.TableID;
        this.ActiveController.Update();
        table.ActiveController = this.ActiveController;
        this.ActiveController = null;
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