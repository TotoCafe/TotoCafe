using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;

/// <summary>
/// Summary description for Company
/// In this class, all informations about the
/// company are stored and the database operations of company
/// are working.
/// 
/// --Initialize()--
/// Initialize method returns void.
/// It sets informations that we need to the object.
/// It must be invoke when the object created.
/// 
/// </summary>
public class Company
{
    public int CompanyID { get; set; }
    public string CompanyName { get; set; }
    public string Email { get; set; }
    public string Pasword { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public float Grade { get; set; }
    public string Location { get; set; }
    public int CityID { get; set; }
    public List<Category> CategoryList { get; set; }
    public List<Table> TableList { get; set; }
    public List<QrCode> QrCodeList { get; set; }

    public Company()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private void GetCategoryList()
    {
        List<Category> CategoryList = new List<Category>();

        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
            );
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;
        cmd.CommandText = "SELECT CategoryID, CategoryName FROM Category WHERE (CompanyID = @CompanyID)";

        cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

        try
        {
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Category ctgry = new Category();

                ctgry.CategoryID = int.Parse(dr["CategoryID"].ToString());
                ctgry.CategoryName = dr["CategoryName"].ToString();

                CategoryList.Add(ctgry);
            }
        }
        catch (Exception) { }
        finally
        {
            conn.Close();
            this.CategoryList = CategoryList;
        }
    }
    private void GetTableList()
    {
        List<Table> TableList = new List<Table>();

        SqlConnection conn = new SqlConnection(
           ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
           );
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;
        cmd.CommandText = "SELECT TableID, TableName, QrCode, IsReserved FROM [Table] WHERE (CompanyID = @CompanyID)";

        cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

        try
        {
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Table tbl = new Table();

                tbl.TableID = int.Parse(dr["TableID"].ToString());
                tbl.TableName = dr["TableName"].ToString();
                tbl.QrCode = dr["QrCode"].ToString();
                tbl.IsReserved = int.Parse(dr["IsReserved"].ToString());
                tbl.InitController();

                TableList.Add(tbl);
            }
        }
        catch (Exception) { }
        finally
        {
            conn.Close();
            this.TableList = TableList;
        }
    }

    public string GetQrPdfName()
    {
        return this.CompanyID + "-QrCodes";
    }
    private void GetQrCodeList()
    {
        List<QrCode> QrCodeList = new List<QrCode>();

        foreach (Table t in this.TableList)
        {
            QrCode qr = new QrCode();

            qr.CompanyName = this.CompanyName;
            qr.TableName = t.TableName;
            qr.QrString = t.QrCode;
            qr.GenerateQrImage();

            QrCodeList.Add(qr);
        }

        this.QrCodeList = QrCodeList;
    }
    public void SaveQrPdf(string path)
    {
        this.GetQrCodeList();

        Document document = new Document(PageSize.A4, 0, 0, 25, 0);

        Bitmap bitMap = new Bitmap(path + "/Empty Qr Image/emptyQr.png");
        MemoryStream ms = new MemoryStream();
        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);


        for (int i = 3; i >= this.QrCodeList.Count % 3; i--)
        {
            QrCode qr = new QrCode();
            qr.QrImage = System.Drawing.Image.FromStream(ms);
            qr.TableName = "";
            qr.CompanyName = "";
            qr.QrString = "";

            QrCodeList.Add(qr);
        }

        try
        {
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path + "/" + GetQrPdfName(), FileMode.Create));
            document.Open();

            //Create a master table with 3 columns
            PdfPTable masterTable = new PdfPTable(3);

            masterTable.PaddingTop = 3f;
            masterTable.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPTable table;
            PdfPCell cell;

            foreach (QrCode q in this.QrCodeList)
            {
                table = new PdfPTable(1);
                table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                Phrase tableName = new Phrase(q.TableName);
                tableName.Font.Size = 16f;

                cell = new PdfPCell(tableName);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BorderColor = BaseColor.WHITE;
                table.AddCell(cell);

                Phrase companyName = new Phrase(q.CompanyName);
                companyName.Font.Size = 10f;

                cell = new PdfPCell(companyName);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BorderColor = BaseColor.WHITE;
                table.AddCell(cell);

                iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(q.QrImage, System.Drawing.Imaging.ImageFormat.Png);
                cell = new PdfPCell(pdfImage);
                cell.BorderColor = BaseColor.WHITE;
                table.AddCell(cell);

                //Add the sub-table to our master table instead of the writer
                masterTable.AddCell(table);
            }

            //Add the master table to our document
            document.Add(masterTable);
        }

        catch (Exception)
        {
            //handle errors
        }
        finally
        {
            document.Close();
        }
    }
    public void DeleteQrPdf(string path)
    {
        if (File.Exists(path + "/" + GetQrPdfName()))
        {
            File.Delete(path + "/" + GetQrPdfName());
        }
    }

    public void Initialize(string Email)
    {
        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "SELECT Company.* FROM Company WHERE (Email = @Email)";
        cmd.Parameters.AddWithValue("@Email", FormsAuthentication.HashPasswordForStoringInConfigFile(Email, "SHA1"));

        cmd.Connection = conn;

        try
        {
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read(); //There must be only 1 record in database with this Email.

            this.CompanyID = int.Parse(dr["CompanyID"].ToString());
            this.CompanyName = dr["CompanyName"].ToString();
            this.Pasword = dr["Password"].ToString();
            this.Address = dr["Address"].ToString();
            this.Phone = dr["Phone#"].ToString();
            this.Grade = float.Parse(dr["Grade"].ToString());
            this.Location = dr["Location"].ToString();
            this.CityID = int.Parse(dr["CityID"].ToString());
            this.GetCategoryList();
            this.GetTableList();
            this.GetQrCodeList();
        }
        catch (Exception ex) { Console.Write(ex.ToString()); }
        finally
        {
            conn.Close();
        }
    }

    public bool Insert(string CompanyName, string Email, string Password, string Address, string Phone, float grade, string Location, int CityID)
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "INSERT INTO Company (CompanyName, Email, Password, Address, Phone#, Grade, Location, CityID)" +
                                          "VALUES (@CompanyName, @Email, @Password, @Address, @Phone#, @Grade, @Location, @CityID)";

        cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
        cmd.Parameters.AddWithValue("@Email", FormsAuthentication.HashPasswordForStoringInConfigFile(Email, "SHA1"));
        cmd.Parameters.AddWithValue("@Password", FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "SHA1"));
        cmd.Parameters.AddWithValue("@Address", Address);
        cmd.Parameters.AddWithValue("@Phone#", FormsAuthentication.HashPasswordForStoringInConfigFile(Phone, "SHA1"));
        cmd.Parameters.AddWithValue("@Grade", Grade);
        cmd.Parameters.AddWithValue("@Location", Location);
        cmd.Parameters.AddWithValue("@CityID", CityID);

        return ExecuteNonQuery(cmd);
    }
    public bool Delete()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "DELETE FROM Company WHERE (CompanyID = @CompanyID)";
        cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

        foreach (Category c in this.CategoryList) c.Delete();
        foreach (Table t in this.TableList) t.Delete();

        return ExecuteNonQuery(cmd);
    }
    public bool Update()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "UPDATE      Company" +
                          "SET         CompanyName = @CompanyName, " +
                                      "Email = @Email, " +
                                      "Password = @Password, " +
                                      "Address = @Address, " +
                                      "Phone# = @Phone#, " +
                                      "Grade = @Grade, " +
                                      "Location = @Location, " +
                                      "CityID = @CityID" +
                          "WHERE      (CompanyID = @CompanyID)";

        cmd.Parameters.AddWithValue("@CompanyName", this.CompanyName);
        cmd.Parameters.AddWithValue("@Email", this.Email);
        cmd.Parameters.AddWithValue("@Password", this.Pasword);
        cmd.Parameters.AddWithValue("@Address", this.Address);
        cmd.Parameters.AddWithValue("@Phone#", this.Phone);
        cmd.Parameters.AddWithValue("@Grade", this.Grade);
        cmd.Parameters.AddWithValue("@Location", this.Location);
        cmd.Parameters.AddWithValue("@CityID", this.CityID);
        cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

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
    private int ExecuteReader(SqlCommand cmd)
    {
        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );

        cmd.Connection = conn;

        int temp = 0;

        try
        {
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();

            temp = int.Parse(dr["CompanyID"].ToString());
        }
        catch (Exception) { }
        finally
        {
            conn.Close();
        }
        return temp;
    }
}