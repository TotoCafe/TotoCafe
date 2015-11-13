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
    public int IsReserved { get; set; }
    public int CompanyID { get; set; }
    public TableController Controller { get; set; }

    public Table()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void InitController()
    {
        TableController Controller = null;

        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
            );

        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "SELECT TableController.* FROM TableController WHERE (TableID = @TableID) AND (FinishDateTime IS NULL)";
        cmd.Parameters.AddWithValue("@TableID", this.TableID);

        cmd.Connection = conn;

        try
        {
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();//There must be only 1 record..

                Controller = new TableController();

                Controller.ControllerID = int.Parse(dr["ControllerID"].ToString());
                Controller.CostumerID = int.Parse(dr["CostumerID"].ToString());
                Controller.TableID = int.Parse(dr["TableID"].ToString());
                IFormatProvider culture = new CultureInfo("en-US", true);
                Controller.StartDateTime = DateTime.ParseExact(dr["StartDateTime"].ToString(), "dd/MM/yyyy HH:mm:ss.fff", culture);
                Controller.InitOrderList();
                /**
                 * IN FACT, HERE WE ARE SUPPOSED TO BE READING 'FinishDateTime' ATRIBUTE TOO, BUT
                 * IT DOES NOT NECESSARY FOR THIS STUATION BECAUSE WE ARE USING THIS 'Controller'
                 * OBJECT JUST TO SPECIFY AND HANDLE THE TABLES WHICH ARE ALREADY TAKEN AND
                 * ACTIVE 
                 * */
            }
        }
        catch (Exception) { /*Handle excepsions..*/ }
        finally
        {
            conn.Close();
        }
        this.Controller = Controller;
    }
    public void BreakController()
    {
        /**
         * THIS METHOD BREAKS THE CONNECTION BETWEEN
         * CURRENT TABLE AND ITS CONTROLLER AND SETS
         * CONTROLLER's SETS FinishDateTime CURRENT
         * TIME SO THAT THE TABLE OBJECT WILL BE FREE OR IN OTHER
         * SAYING IT IS READY FOR NEW COSTUMERS AND ORDERS..
         * */
        this.Controller.FinishDateTime = DateTime.Now;
        this.Controller.Update();
        this.Controller = null;
    }
    private void SetTableID()
    {
        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "SELECT TableID FROM [Table] WHERE (TableName = @TableName AND CompanyID = @CompanyID)";
        cmd.Parameters.AddWithValue("@TableName", this.TableName);
        cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
        cmd.Connection = conn;

        int TableID = 0;

        try
        {
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();//There should be only one record with a name in database..

            TableID = int.Parse(dr["TableID"].ToString());
        }
        catch (Exception) { }
        finally
        {
            conn.Close();
            this.TableID = TableID;
        }
    }

    public bool Insert()
    {
        GenerateQrCode();

        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "INSERT INTO [Table] (TableName, QrCode, IsReserved, CompanyID) VALUES (@TableName, @QrCode, @IsReserved, @CompanyID)";

        cmd.Parameters.AddWithValue("@TableName", this.TableName);
        cmd.Parameters.AddWithValue("@QrCode", this.QrCode);
        cmd.Parameters.AddWithValue("@IsReserved", this.IsReserved);
        cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

        bool isDone = ExecuteNonQuery(cmd);//If it fails ID will be set '0'!! Handle it..

        SetTableID();

        return isDone;
    }
    public bool Delete()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "DELETE FROM [Table] WHERE (TableID = @TableID)";
        cmd.Parameters.AddWithValue("@TableID", this.TableID);

        return ExecuteNonQuery(cmd);
    }
    public bool Update()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "UPDATE       [Table]" +
                          "SET          TableName = @TableName, " +
                                        "QrCode = @QrCode, " +
                                        "IsReserved = @IsReserved " +
                          "WHERE        (TableID = @TableID)";

        cmd.Parameters.AddWithValue("@TableName", this.TableName);
        cmd.Parameters.AddWithValue("@QrCode", this.QrCode);
        cmd.Parameters.AddWithValue("@IsReserved", this.IsReserved);
        cmd.Parameters.AddWithValue("@TableID", this.TableID);

        return ExecuteNonQuery(cmd);
    }

    private void GenerateQrCode()
    {
        this.QrCode = "TotoCafe:" + this.CompanyID + ":" + this.TableName;
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