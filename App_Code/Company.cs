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
    #region OldCode
    //public int CompanyID { get; set; }
    //public string CompanyName { get; set; }
    //public string Email { get; set; }
    //public string Pasword { get; set; }
    //public string Address { get; set; }
    //public string Phone { get; set; }
    //public string Location { get; set; }
    //public string WirelessName { get; set; }
    //public string WirelessPassword { get; set; }
    //public int CityID { get; set; }
    //public int AvailabilityID { get; set; }
    //public int PermissionID { get; set; }
    //public string QrPdfName { get; set; }

    //public List<Category> CategoryList { get; set; }
    //public List<Table> TableList { get; set; }
    //public List<QrCode> QrCodeList { get; set; }

    //private void SetCategoryList()
    //{
    //    List<Category> CategoryList = new List<Category>();

    //    SqlConnection conn = new SqlConnection(
    //        ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
    //        );
    //    SqlCommand cmd = new SqlCommand();

    //    cmd.Connection = conn;
    //    cmd.CommandText = "SELECT CategoryID, CategoryName FROM Category WHERE (CompanyID = @CompanyID)";

    //    cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

    //    try
    //    {
    //        conn.Open();

    //        SqlDataReader dr = cmd.ExecuteReader();

    //        while (dr.Read())
    //        {
    //            Category ctgry = new Category();

    //            ctgry.CategoryID = int.Parse(dr["CategoryID"].ToString());
    //            ctgry.CategoryName = dr["CategoryName"].ToString();
    //            ctgry.InitProductList();

    //            CategoryList.Add(ctgry);
    //        }
    //    }
    //    catch (Exception) { }
    //    finally
    //    {
    //        conn.Close();
    //        this.CategoryList = CategoryList;
    //    }
    //}
    //private void SetTableList()
    //{
    //    List<Table> TableList = new List<Table>();

    //    SqlConnection conn = new SqlConnection(
    //       ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
    //       );
    //    SqlCommand cmd = new SqlCommand();

    //    cmd.Connection = conn;
    //    cmd.CommandText = "SELECT TableID, TableName, QrCode FROM [Table] WHERE (CompanyID = @CompanyID)";

    //    cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

    //    try
    //    {
    //        conn.Open();

    //        SqlDataReader dr = cmd.ExecuteReader();

    //        while (dr.Read())
    //        {
    //            Table tbl = new Table();

    //            tbl.TableID = int.Parse(dr["TableID"].ToString());
    //            tbl.TableName = dr["TableName"].ToString();
    //            tbl.QrCode = dr["QrCode"].ToString();
    //            tbl.InitController();

    //            TableList.Add(tbl);
    //        }
    //    }
    //    catch (Exception) { }
    //    finally
    //    {
    //        conn.Close();
    //        this.TableList = TableList;
    //    }
    //}
    //public List<TableController> GetTableControllerList()
    //{
    //    /**
    //     * HERE WE CAN GET CONTROLLERS WHICH RELATED TO COMPANY
    //     * SO THAT WHEN A DELETION PROCESSED IN COMPANY TABLE
    //     * WE ARE ALSO SUPPOSED TO DELETE CONTROLS AND ORDERS FROM DATABASE.
    //     * BUT THE MAIN THING HERE THAT TO ENABLE COMPANY
    //     * REACH HISTORY OF ORDERS IF THE RELATED TABLE OR TABLES
    //     * ARE DELETED FROM DATABASE.
    //     * **/

    //    List<TableController> TableControllerList = null;

    //    SqlConnection conn = new SqlConnection(
    //       ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
    //       );
    //    SqlCommand cmd = new SqlCommand();

    //    cmd.Connection = conn;
    //    cmd.CommandText = "SELECT TableController.* FROM TableController WHERE (CompanyID = @CompanyID)";

    //    cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

    //    try
    //    {
    //        TableControllerList = new List<TableController>();

    //        conn.Open();

    //        SqlDataReader dr = cmd.ExecuteReader();

    //        while (dr.Read())
    //        {
    //            TableController tc = new TableController();

    //            tc.ControllerID = int.Parse(dr["ControllerID"].ToString());

    //            tc.InitOrderList();

    //            /**
    //             * -->PAY ATTENTION!!!
    //             * THIS OPERATION DESIGNED FOR ONLY REACH
    //             * ORDERS AND CONTROLLERS BELONG TO COMPANY AND DELETE THEM
    //             * WHILE COMPANY BEING DELETED. THE CONTROLLER OBJECT IN THE LIST
    //             * CONTAINS JUST CONTROLLER ID's AND RELATED PRODUCT's.
    //             * **/

    //            TableControllerList.Add(tc);
    //        }
    //    }
    //    catch (Exception) { }
    //    finally
    //    {
    //        conn.Close();
    //    }
    //    return TableControllerList;
    //}//Maybe...

    //private void SetQrPdfName()
    //{
    //    this.QrPdfName = "QrCodes-" + this.CompanyID;
    //}
    //private void SetQrCodeList()
    //{
    //    List<QrCode> QrCodeList = new List<QrCode>();

    //    foreach (Table t in this.TableList)
    //    {
    //        QrCode qr = new QrCode();

    //        qr.CompanyName = this.CompanyName;
    //        qr.TableName = t.TableName;
    //        qr.QrString = t.QrCode + "\t" + this.WirelessPassword;
    //        qr.GenerateQrImage();

    //        QrCodeList.Add(qr);
    //    }

    //    this.QrCodeList = QrCodeList;
    //}
    //public void SaveQrPdf(string path)
    //{
    //    this.SetQrCodeList();

    //    Document document = new Document(PageSize.A4, 0, 0, 25, 0);

    //    Bitmap bitMap = new Bitmap(path + "/Empty Qr Image/emptyQr.png");
    //    MemoryStream ms = new MemoryStream();
    //    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);


    //    for (int i = 3; i >= this.QrCodeList.Count % 3; i--)
    //    {
    //        QrCode qr = new QrCode();
    //        qr.QrImage = System.Drawing.Image.FromStream(ms);
    //        qr.TableName = "";
    //        qr.CompanyName = "";
    //        qr.QrString = "";

    //        QrCodeList.Add(qr);
    //    }

    //    try
    //    {
    //        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path + "/" + this.QrPdfName, FileMode.Create));
    //        document.Open();

    //        //Create a master table with 3 columns
    //        PdfPTable masterTable = new PdfPTable(3);

    //        masterTable.PaddingTop = 3f;
    //        masterTable.HorizontalAlignment = Element.ALIGN_CENTER;

    //        PdfPTable table;
    //        PdfPCell cell;

    //        foreach (QrCode q in this.QrCodeList)
    //        {
    //            table = new PdfPTable(1);
    //            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
    //            table.HorizontalAlignment = Element.ALIGN_CENTER;

    //            Phrase tableName = new Phrase(q.TableName);
    //            tableName.Font.Size = 16f;

    //            cell = new PdfPCell(tableName);
    //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
    //            cell.BorderColor = BaseColor.WHITE;
    //            table.AddCell(cell);

    //            Phrase companyName = new Phrase(q.CompanyName);
    //            companyName.Font.Size = 10f;

    //            cell = new PdfPCell(companyName);
    //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
    //            cell.BorderColor = BaseColor.WHITE;
    //            table.AddCell(cell);

    //            iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(q.QrImage, System.Drawing.Imaging.ImageFormat.Png);
    //            cell = new PdfPCell(pdfImage);
    //            cell.BorderColor = BaseColor.WHITE;
    //            table.AddCell(cell);

    //            //Add the sub-table to our master table instead of the writer
    //            masterTable.AddCell(table);
    //        }

    //        //Add the master table to our document
    //        document.Add(masterTable);
    //    }

    //    catch (Exception)
    //    {
    //        //handle errors
    //    }
    //    finally
    //    {
    //        document.Close();
    //    }
    //}
    //public void DeleteQrPdf(string path)
    //{
    //    if (File.Exists(path + "/" + this.QrPdfName))
    //    {
    //        File.Delete(path + "/" + this.QrPdfName);
    //    }
    //}

    ///// <summary>
    ///// Initializes company.
    ///// </summary>
    ///// <param name="Email"></param>
    //public void Initialize(string Email)
    //{
    //    SqlConnection conn = new SqlConnection(
    //        ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
    //                                          );
    //    SqlCommand cmd = new SqlCommand();

    //    cmd.CommandText = "SELECT Company.* FROM Company WHERE (Email = @Email)";

    //    cmd.Parameters.AddWithValue("@Email", Email);

    //    cmd.Connection = conn;

    //    try
    //    {
    //        conn.Open();

    //        SqlDataReader dr = cmd.ExecuteReader();

    //        dr.Read(); //There must be only 1 record in database with this Email.

    //        this.CompanyID = int.Parse(dr["CompanyID"].ToString());
    //        this.CompanyName = dr["CompanyName"].ToString();
    //        this.Email = dr["Email"].ToString();
    //        this.Pasword = dr["Password"].ToString();
    //        this.Address = dr["Address"].ToString();
    //        this.Phone = dr["Phone#"].ToString();
    //        this.Location = dr["Location"].ToString();
    //        this.WirelessName = dr["WirelessName"].ToString();
    //        this.WirelessPassword = dr["WirelessPassword"].ToString();
    //        this.CityID = int.Parse(dr["CityID"].ToString());
    //        this.AvailabilityID = int.Parse(dr["AvailabilityID"].ToString());
    //        this.PermissionID = int.Parse(dr["PermissionID"].ToString());
    //        this.SetCategoryList();
    //        this.SetTableList();
    //        this.SetQrCodeList();
    //        this.SetQrPdfName();
    //    }
    //    catch (Exception) { }
    //    finally
    //    {
    //        conn.Close();
    //    }
    //}

    ///// <summary>
    ///// Binds data of Company object.
    ///// </summary>
    //public void BindData()
    //{
    //    this.Initialize(this.Email);
    //}

    ///// <summary>
    ///// Inserts current Company object into database.
    ///// </summary>
    ///// <param name="CompanyName"></param>
    ///// <param name="Email"></param>
    ///// <param name="Password"></param>
    ///// <param name="Address"></param>
    ///// <param name="Phone"></param>
    ///// <param name="CityID"></param>
    ///// <returns></returns>
    //public bool Insert(string CompanyName, string Email, string Password, string Address, string Phone, int CityID)
    //{
    //    SqlCommand cmd = new SqlCommand();

    //    cmd.CommandText = "INSERT INTO Company(CompanyName, Email, Password, Address, Phone#, CityID) " +
    //                             "VALUES (@CompanyName, @Email, @Password, @Address, @Phone#, @CityID)";

    //    cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
    //    cmd.Parameters.AddWithValue("@Email", FormsAuthentication.HashPasswordForStoringInConfigFile(Email, "SHA1"));
    //    cmd.Parameters.AddWithValue("@Password", FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "SHA1"));
    //    cmd.Parameters.AddWithValue("@Address", Address);
    //    cmd.Parameters.AddWithValue("@Phone#", FormsAuthentication.HashPasswordForStoringInConfigFile(Phone, "SHA1"));
    //    cmd.Parameters.AddWithValue("@CityID", CityID);

    //    return ExecuteNonQuery(cmd);
    //}

    ///// <summary>
    ///// Deletes current Company object from database.
    ///// Instead of this Freeze() operation should be used.
    ///// </summary>
    ///// <returns></returns>
    //public bool Delete()
    //{
    //    SqlCommand cmd = new SqlCommand();

    //    cmd.CommandText = "DELETE FROM Company WHERE (CompanyID = @CompanyID)";
    //    cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

    //    try
    //    {
    //        foreach (Category c in this.CategoryList) c.Delete();
    //        foreach (Table t in this.TableList) t.Delete();
    //        foreach (TableController tc in this.GetTableControllerList()) tc.Delete();
    //    }
    //    catch (Exception) { return false; }

    //    return ExecuteNonQuery(cmd);
    //}

    ///// <summary>
    ///// Updates informations of current Company object.
    ///// </summary>
    ///// <returns></returns>
    //public bool Update()
    //{
    //    SqlCommand cmd = new SqlCommand();

    //    cmd.CommandText = "UPDATE Company " +
    //                         "SET CompanyName = @CompanyName, " +
    //                             "Email = @Email, " +
    //                             "Password = @Password, " +
    //                             "Address = @Address, " +
    //                             "Phone# = @Phone#, " +
    //                             "Location = @Location, " +
    //                             "WirelessName = @WirelessName, " +
    //                             "WirelessPassword = @WirelessPassword, " +
    //                             "CityID = @CityID, " +
    //                             "AvailabilityID = @AvailabilityID, " +
    //                             "PermissionID = @PermissionID " +
    //                       "WHERE (CompanyID = @CompanyID)";

    //    cmd.Parameters.AddWithValue("@CompanyName", this.CompanyName);
    //    cmd.Parameters.AddWithValue("@Email", this.Email);
    //    cmd.Parameters.AddWithValue("@Password", this.Pasword);
    //    cmd.Parameters.AddWithValue("@Address", this.Address);
    //    cmd.Parameters.AddWithValue("@Phone#", this.Phone);
    //    cmd.Parameters.AddWithValue("@Location", this.Location);
    //    cmd.Parameters.AddWithValue("@WirelessName", this.WirelessName);
    //    cmd.Parameters.AddWithValue("@WirelessPassword", this.WirelessPassword);
    //    cmd.Parameters.AddWithValue("@CityID", this.CityID);
    //    cmd.Parameters.AddWithValue("@AvailabilityID", this.AvailabilityID);
    //    cmd.Parameters.AddWithValue("@PermissionID", this.PermissionID);
    //    cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

    //    return ExecuteNonQuery(cmd);
    //}

    ///// <summary>
    ///// Freezes current Company objec and makes it unusable until the Company object becomes available again.
    ///// Our keywords = AVALIABLE/FROZEN
    ///// </summary>
    ///// <returns></returns>
    //public bool Freeze()
    //{
    //    SqlCommand cmd = new SqlCommand();

    //    cmd.CommandText = "UPDATE Company " +
    //                         "SET AvailabilityID = Availability.AvailabilityID " +
    //                        "FROM Company " +
    //                       "INNER JOIN Availability ON Company.AvailabilityID = Availability.AvailabilityID " +
    //                       "WHERE (Company.CompanyID = @CompanyID) AND (Availability.Availability = 'FROZEN')";
    //    cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

    //    return ExecuteNonQuery(cmd);
    //}

    ///// <summary>
    ///// Makes Company object usable.
    ///// Our keywords = AVALIABLE/FROZEN
    ///// </summary>
    ///// <returns></returns>
    //public bool Resume()
    //{
    //    SqlCommand cmd = new SqlCommand();

    //    cmd.CommandText = "UPDATE Company " +
    //                         "SET AvailabilityID = Availability.AvailabilityID " +
    //                        "FROM Company " +
    //                       "INNER JOIN Availability ON Company.AvailabilityID = Availability.AvailabilityID " +
    //                       "WHERE (Company.CompanyID = @CompanyID) AND (Availability.Availability = 'AVAILABLE')";
    //    cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);

    //    return ExecuteNonQuery(cmd);
    //}
    #endregion

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
    public List<Category> CategoryList { get; set; }
    public List<Table> TableList { get; set; }

    public Company()
    {
        //
        // TODO: Add constructor logic here
        //
    }

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
    /// Initializes Categories which belong to current Company and Available.
    /// </summary>
    private void InitCategoryList()
    {
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

                this.CategoryList.Add(c);
            }
        }
        catch (Exception) { }
        finally { conn.Close(); }
    }

    /// <summary>
    /// Initializes Tables of the Company and add them to TableList property.
    /// </summary>
    private void InitTableList()
    {
        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "SELECT [Table].TableID, [Table].TableName, [Table].AvailabilityID " +
                            "FROM [Table] " +
                            "INNER JOIN Availability ON [Table].AvailabilityID = Availability.AvailabilityID " +
                            "WHERE ([Table].CompanyID = @CompanyID) AND (Availability.Availability = @Availability)";
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

                this.TableList.Add(t);
            }
        }
        catch (Exception) { }
        finally { conn.Close(); }

    }

    /// <summary>
    /// Authenticates the company with Email and password and Initializes it.
    /// </summary>
    /// <returns></returns>
    public bool Authenticate()
    {
        SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString
                                              );
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;
        cmd.CommandText = "SELECT COUNT(*) FROM Company WHERE (Email = @Email) AND (Password = @Password)";
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
    /// </summary>
    /// <returns></returns>
    public bool SignUp()
    {
        bool result = this.Insert();

        if (result) this.Initialize();

        return result;
    }

    /// <summary>
    /// Executes NonQueries.
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
}