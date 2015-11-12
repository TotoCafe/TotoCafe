﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Category
/// </summary>
public class Category
{
    public int CategoryID { get; set; }
    public string CategoryName { get; set; }
    public int CompanyID { get; set; }
    public List<Product> ProductList { get; set; }

    public void InitProductList()
    {
        List<Product> ProductList = new List<Product>();

        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
            );
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;
        cmd.CommandText = "SELECT ProductID, ProductName, Detail, Price, Credit FROM Product WHERE (CategoryID = @CategoryID)";

        cmd.Parameters.AddWithValue("@CategoryID", this.CategoryID);

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

                ProductList.Add(p);
            }
        }
        catch (Exception) { }
        finally
        {
            conn.Close();
        }

        this.ProductList = ProductList;
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

        return ExecuteNonQuery(cmd);
    }
    public bool Delete()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "DELETE FROM Category WHERE (CategoryID = @CategoryID)";

        cmd.Parameters.AddWithValue("@CategoryID", this.CategoryID);

        foreach (Product p in this.ProductList) p.Delete();

        return ExecuteNonQuery(cmd);
    }
    public bool Update()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "UPDATE Category SET CategoryName = @CategoryName WHERE (CategoryID = @CategoryID)";

        cmd.Parameters.AddWithValue("@CategoryName", this.CategoryName);
        cmd.Parameters.AddWithValue("@CategoryID", this.CategoryID);

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