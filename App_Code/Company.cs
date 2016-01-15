using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.Security;
using System.Web.Services;

/// <summary>
/// Summary description for Company
/// In this class, all informations about the
/// company are stored and the database operations of company
/// are working.
/// 
/// --Initialize()--
/// Initialize method returns void.
/// It sets informations that we need to the object.
/// It is invoked when the object created.
/// 
/// </summary>
public class Company
{
    #region Properties
    public int CompanyID { get; set; }
    public string CompanyName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Location { get; set; }
    public string WirelessName { get; set; }
    public string WirelessPassword { get; set; }
    public int CityID { get; set; }
    public int AvailabilityID { get; set; }
    public int PermissionID { get; set; }
    private Dictionary<int, Table> Tables { get; set; }
    private Dictionary<int, Category> Categories { get; set; }
    #endregion

    #region Constructor
    public Company()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region Functions For Company
    /// <summary>
    /// Initializes Company object to use.
    /// After this method called the object is
    /// ready for all operations.
    /// </summary>
    /// <returns></returns>
    private bool Initialize()
    {
        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "SELECT Company.* FROM Company WHERE (Email = @Email)";
        cmd.Parameters.AddWithValue("@Email", this.Email);
        cmd.Connection = conn;
        bool result = true;
        try
        {
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();//Only one record.

            this.CompanyID = int.Parse(dr["CompanyID"].ToString());
            this.CompanyName = dr["CompanyName"].ToString();
            this.Email = dr["Email"].ToString();
            this.Password = dr["Password"].ToString();
            this.Address = dr["Address"].ToString();
            this.Phone = dr["Phone#"].ToString();
            this.Location = dr["Location"].ToString();
            this.WirelessName = dr["WirelessName"].ToString();
            this.WirelessPassword = dr["WirelessPassword"].ToString();
            this.CityID = int.Parse(dr["CityID"].ToString());
            this.AvailabilityID = int.Parse(dr["AvailabilityID"].ToString());
            this.PermissionID = int.Parse(dr["PermissionID"].ToString());

            InitCategoryList();
            InitTableList();
        }
        catch (Exception) { result = false; }
        finally
        {
            conn.Close();
        }
        return result;
    }

    /// <summary>
    /// Inserts Company to database. These informations are just the required ones 
    /// that we want user to enter while signing up. Other attributes have default values each.
    /// </summary>
    /// <returns></returns>
    private bool Insert()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "INSERT INTO Company(CompanyName, Email, Password, Address, Phone#, CityID) " +
                                      "VALUES (@CompanyName, @Email, @Password, @Address, @Phone#, @CityID)";
        cmd.Parameters.AddWithValue("@CompanyName", this.CompanyName);
        cmd.Parameters.AddWithValue("@Email", this.Email);
        cmd.Parameters.AddWithValue("@Password", FormsAuthentication.HashPasswordForStoringInConfigFile(this.Password, "SHA1"));
        cmd.Parameters.AddWithValue("@Address", this.Address);
        cmd.Parameters.AddWithValue("@Phone#", this.Phone);
        cmd.Parameters.AddWithValue("@CityID", this.CityID);

        return ExecuteNonQuery(cmd);
    }

    /// <summary>
    /// Updates Company in database with all informations.
    /// </summary>
    /// <returns></returns>
    public bool Update()
    {
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "UPDATE Company " +
                             "SET CompanyName = @CompanyName, " +
                                 "Password = @Password, " +
                                 "Address = @Address, " +
                                 "Phone# = @Phone#, " +
                                 "Location = @Location, " +
                                 "WirelessName = @WirelessName, " +
                                 "WirelessPassword = @WirelessPassword, " +
                                 "CityID = @CityID, " +
                                 "AvailabilityID = @AvailabilityID, " +
                                 "PermissionID = @PermissionID " +
                           "WHERE (CompanyID = @CompanyID)";

        cmd.Parameters.AddWithValue("@CompanyName", this.CompanyName);
        cmd.Parameters.AddWithValue("@Password", this.Password);
        cmd.Parameters.AddWithValue("@Address", this.Address);
        cmd.Parameters.AddWithValue("@Phone#", this.Phone);
        cmd.Parameters.AddWithValue("@Location", this.Location);
        cmd.Parameters.AddWithValue("@WirelessName", this.WirelessName);
        cmd.Parameters.AddWithValue("@WirelessPassword", this.WirelessPassword);
        cmd.Parameters.AddWithValue("@CityID", this.CityID);
        cmd.Parameters.AddWithValue("@AvailabilityID", this.AvailabilityID);
        cmd.Parameters.AddWithValue("@PermissionID", this.PermissionID);
        cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

        return ExecuteNonQuery(cmd);
    }

    /// <summary>
    /// Freezes the Company if user requests. The record will not be deleted
    /// it will be only hidden from the user until user requests to reopen his/her account.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// This function works opposite of Freeze function.
    /// It resumes Company if user requests to reopen the account.
    /// </summary>
    /// <returns></returns>
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
        catch (Exception) { return false; }
        finally
        {
            conn.Close();
        }

        return this.Update();
    }
    
    /// <summary>
    /// Authenticates the company with Email and password and Initializes it.
    /// It is needed to be set Email and Password to Company object.
    /// </summary>
    /// <returns></returns>
    public bool Authenticate()
    {
        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;
        cmd.CommandText = "SELECT COUNT(*) FROM Company " +
                                    "INNER JOIN Availability ON Company.AvailabilityID = Availability.AvailabilityID " +
                                    "WHERE (Company.Email = @Email) AND (Company.Password = @Password) AND (Availability.Availability = 'AVAILABLE')";
        cmd.Parameters.AddWithValue("@Email", this.Email);
        cmd.Parameters.AddWithValue("@Password", FormsAuthentication.HashPasswordForStoringInConfigFile(this.Password, "SHA1"));

        bool result = true;
        try
        {
            conn.Open();

            if (int.Parse(cmd.ExecuteScalar().ToString()) == 0) result = false;
            else this.Initialize();
        }
        catch (Exception) { result = false; }
        finally { conn.Close(); }
        return result;
    }

    /// <summary>
    /// Executes Signup operation for a company object.
    /// Inserts the object to database and initializes it.
    /// It is needed to be set CompanyName, Email, Password, Address, City and Phone to the Company object.
    /// </summary>
    /// <returns></returns>
    public bool SignUp()
    {
        bool result = this.Insert();

        if (result) this.Initialize();

        return result;
    }
    #endregion

    #region Functions For Tables
    /// <summary>
    /// Returns the table according to given id.
    /// </summary>
    /// <param name="TableID"></param>
    /// <returns></returns>
    public Table GetTableWithId(int TableID)
    {
        return (Table)this.Tables[TableID];
    }

    /// <summary>
    /// Adds the table to the Company's Hashtable.
    /// </summary>
    /// <param name="t"></param>
    [WebMethod]
    public void AddTable(Table t)
    {
        t.CompanyID = this.CompanyID;
        t.Insert();
        this.Tables.Add(t.TableID, t);
    }

    /// <summary>
    /// Updates Table in database and hashtable.
    /// </summary>
    /// <param name="t"></param>
    [WebMethod]
    public void UpdateTable(Table t)
    {
        t.Update();
        this.Tables[t.TableID] = t;
    }

    /// <summary>
    /// Freezes the table.
    /// </summary>
    /// <param name="t"></param>
    [WebMethod]
    public void FreezeTable(Table t)
    {
        t.Freeze();
        this.Tables.Remove(t.TableID);
    }

    /// <summary>
    /// Transefers a tablo to another table.
    /// </summary>
    /// <param name="FromTableID"></param>
    /// <param name="ToTableID"></param>
    [WebMethod]
    public void TransferTable(int FromTableID, int ToTableID)
    {
        Table from = this.GetTableWithId(FromTableID);
        Table to = this.GetTableWithId(ToTableID);

        from.TransferTo(to);
        this.Tables[to.TableID] = this.Tables[from.TableID];
    }

    /// <summary>
    /// Returns a list containing tables of company.
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, Table> GetTableDictionary()
    {
        Dictionary<int, Table> tableDictionary = new Dictionary<int, Table>();

        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "SELECT [Table].TableID, [Table].TableName, [Table].AvailabilityID FROM [Table] " +
                                "INNER JOIN Availability ON [Table].AvailabilityID = Availability.AvailabilityID " +
                                "WHERE (Availability.Availability = @Availability) AND ([Table].CompanyID = @CompanyID) " +
                                "ORDER BY [Table].TableName";

        cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
        cmd.Parameters.AddWithValue("@Availability", "AVAILABLE");

        cmd.Connection = conn;

        try
        {
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Table t = new Table();

                t.TableID = int.Parse(dr["TableID"].ToString());
                t.TableName = dr["TableName"].ToString();
                t.AvailabilityID = int.Parse(dr["AvailabilityID"].ToString());
                t.CompanyID = this.CompanyID;
                t.QrCode = "TotoCafe-" + this.CompanyID.ToString() + "-" + t.TableID;
                t.InitActiveController();//Current open controller..

                tableDictionary.Add(t.TableID,t);
            }
        }
        catch (Exception) { }
        finally { conn.Close(); }
        return tableDictionary;
    }

    /// <summary>
    /// Initializes Tables of the Company and add them to TableList property.
    /// </summary>
    private void InitTableList()
    {
        Dictionary<int, Table> dt = new Dictionary<int, Table>();

        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "SELECT [Table].TableID, [Table].TableName, [Table].AvailabilityID FROM [Table] " +
                                "INNER JOIN Availability ON [Table].AvailabilityID = Availability.AvailabilityID " +
                                "WHERE (Availability.Availability = @Availability) AND ([Table].CompanyID = @CompanyID) " +
                                "ORDER BY [Table].TableName";

        cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
        cmd.Parameters.AddWithValue("@Availability", "AVAILABLE");

        cmd.Connection = conn;

        try
        {
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Table t = new Table();

                t.TableID = int.Parse(dr["TableID"].ToString());
                t.TableName = dr["TableName"].ToString();
                t.AvailabilityID = int.Parse(dr["AvailabilityID"].ToString());
                t.CompanyID = this.CompanyID;
                t.QrCode = "TotoCafe-" + this.CompanyID.ToString() + "-" + t.TableID;
                t.InitActiveController();//Current open controller..

                dt.Add(t.TableID, t);
            }
        }
        catch (Exception) { }
        finally { conn.Close(); }
        this.Tables = dt;
    }
    #endregion

    #region Functions For Categories
    /// <summary>
    /// Returns category according to given id.
    /// </summary>
    /// <param name="CategoryID"></param>
    /// <returns></returns>
    public Category GetCategoryWithId(int CategoryID)
    {
        return (Category)this.Categories[CategoryID];
    }

    /// <summary>
    /// Adds category to database and 
    /// </summary>
    /// <param name="c"></param>
    [WebMethod]
    public void AddCategory(Category c)
    {
        c.CompanyID = this.CompanyID;
        c.Insert();
        this.Categories.Add(c.CategoryID, c);
    }

    /// <summary>
    /// Updates Category in database and hashtable.
    /// </summary>
    /// <param name="c"></param>
    [WebMethod]
    public void UpdateCategory(Category c)
    {
        c.Update();
        this.Categories[c.CategoryID] = c;
    }

    /// <summary>
    /// Freezes the category and removes from hashtable.
    /// -->PAY ATTENTION
    /// WHEN THE CATEGORY FREEZED THEN THE PRODUCTS HAS THE CategoryID AS THIS CATEGORY WILL BE INVISIBLE.
    /// CREATE A DIOLOG WITH USER TO MAKE HIM/HER AWARE OF THIS.
    /// </summary>
    /// <param name="c"></param>
    [WebMethod]
    public void FreezeCategory(Category c)
    {
        c.Freeze();
        this.Categories.Remove(c.CategoryID);
    }

    /// <summary>
    /// Returns a list of categories.
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, Category> GetCategoryDictionary()
    {
        Dictionary<int, Category> categoryDictionary = new Dictionary<int, Category>();

        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "SELECT Category.* FROM Category " +
                                      "INNER JOIN Availability ON Category.AvailabilityID = Availability.AvailabilityID " +
                                      "WHERE (Category.CompanyID = @CompanyID) AND (Availability.Availability = @Availability) " +
                                      "ORDER BY Category.CategoryName";
        cmd.Parameters.AddWithValue("@Availability", "AVAILABLE");
        cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

        cmd.Connection = conn;

        try
        {
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Category c = new Category();

                c.CategoryID = int.Parse(dr["CategoryID"].ToString());
                c.CategoryName = dr["CategoryName"].ToString();
                c.AvailabilityID = int.Parse(dr["AvailabilityID"].ToString());
                c.InitProductList();

                categoryDictionary.Add(c.CategoryID,c);
            }
        }
        catch (Exception) { }
        finally { conn.Close(); }
        return categoryDictionary;
    }

    /// <summary>
    /// Initializes Categories which belong to current Company and Available.
    /// </summary>
    private void InitCategoryList()
    {
        Dictionary<int, Category> dc = new Dictionary<int, Category>();

        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "SELECT Category.* FROM Category " +
                                      "INNER JOIN Availability ON Category.AvailabilityID = Availability.AvailabilityID " +
                                      "WHERE (Category.CompanyID = @CompanyID) AND (Availability.Availability = @Availability)";
        cmd.Parameters.AddWithValue("@Availability", "AVAILABLE");
        cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

        cmd.Connection = conn;

        try
        {
            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Category c = new Category();

                c.CategoryID = int.Parse(dr["CategoryID"].ToString());
                c.CategoryName = dr["CategoryName"].ToString();
                c.AvailabilityID = int.Parse(dr["AvailabilityID"].ToString());
                c.InitProductList();

                dc.Add(c.CategoryID, c);
            }
        }
        catch (Exception) { }
        finally { conn.Close(); }
        this.Categories = dc;
    }
    #endregion

    #region QrCode
    /// <summary>
    /// Returns a list of qrCodes.
    /// </summary>
    /// <returns></returns>
    private List<QrCode> GetQrCodeList()
    {
        List<QrCode> QrCodeList = new List<QrCode>();

        Dictionary<int, Table> tableDictionary = this.GetTableDictionary();

        foreach (Table t in tableDictionary.Values)
        {
            QrCode qr = new QrCode();

            qr.CompanyName = this.CompanyName;
            qr.TableName = t.TableName;
            qr.QrString = t.QrCode;
            qr.GenerateQrImage();

            QrCodeList.Add(qr);
        }

        return QrCodeList;
    }

    /// <summary>
    /// Creates a collection of qrCodes and writes them into a pdf file.
    /// Saves the pdf file given path.
    /// </summary>
    /// <param name="path"></param>
    public void SaveQrPdf(string path)
    {
        Document document = new Document(PageSize.A4, 0, 0, 25, 0);

        Bitmap bitMap = new Bitmap(path + "/Empty Qr Image/emptyQr.png");
        MemoryStream ms = new MemoryStream();
        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

        List<QrCode> tmp = this.GetQrCodeList();

        for (int i = 3; i >= tmp.Count % 3; i--)
        {
            QrCode qr = new QrCode();
            qr.QrImage = System.Drawing.Image.FromStream(ms);
            qr.TableName = "";
            qr.CompanyName = "";
            qr.QrString = "";

            tmp.Add(qr);
        }

        try
        {
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path + "/" + this.GetQrPdfName(), FileMode.Create));
            document.Open();

            //Create a master table with 3 columns
            PdfPTable masterTable = new PdfPTable(3);

            masterTable.PaddingTop = 3f;
            masterTable.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPTable table;
            PdfPCell cell;

            foreach (QrCode q in tmp)
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

    /// <summary>
    /// Generates and return a pdf name that belongs to current company.
    /// </summary>
    /// <returns></returns>
    public string GetQrPdfName()
    {
        return "QrCodes-" + this.CompanyID;
    }

    /// <summary>
    /// Deletes QrCode pdf file.
    /// Must be invoked while user loging out.
    /// </summary>
    /// <param name="path"></param>
    public void DeleteQrPdf(string path)
    {
        if (File.Exists(path + "/" + this.GetQrPdfName()))
        {
            File.Delete(path + "/" + this.GetQrPdfName());
        }
    }
    #endregion

    #region Common DB Function
    /// <summary>
    /// Executes NonQueries. Insert - Delete - Update
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
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
    #endregion
}