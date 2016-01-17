using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Validation
/// </summary>
public class Validator
{
    private bool confirm;
    public bool Confirm
    {
        get { return confirm; }
    }
    public void validateTextBox(TextBox tbTextBox)
    {
        string defaultValue = tbTextBox.Attributes["placeholder"];

        if (string.IsNullOrEmpty(tbTextBox.Text) || tbTextBox.Text == defaultValue || tbTextBox.Text == defaultValue + " is required." || tbTextBox.Text == "Please enter a valid " + defaultValue + ".") 
        {
            tbTextBox.Attributes["placeholder"] = defaultValue + " is required.";
            confirm = false;
        }
        else
        {
            confirm = true;
        }
    }
    public void validatePassword(TextBox tbPassword)
    {
        string regPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,16}$";
        if (!Regex.IsMatch(tbPassword.Text, regPassword))
        {
            tbPassword.Attributes["placeholder"] = "Please enter a valid password.";
            confirm = false;
        }
        else
        {
            confirm = true;
        }
    }
    public void validateEmail(TextBox tbEmail)
    {
        string regEmail = @"[a-zA-Z0-9]+(?:(\.|_)[A-Za-z0-9!#$%&'*+/=?^`{|}~-]+)*@(?!([a-zA-Z0-9]*\.[a-zA-Z0-9]*\.[a-zA-Z0-9]*\.))(?:[A-Za-z0-9](?:[a-zA-Z0-9-]*[A-Za-z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?";
        if (!Regex.IsMatch(tbEmail.Text, regEmail))
        {
            tbEmail.Attributes["placeholder"] = "Invalid email address";
            confirm = false;
        }
        else
        {
            confirm = true;
        }
    }
    public bool checkCompanyEmail(TextBox tbEmail)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TotoCafeDB"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "SELECT COUNT(Email) FROM Company WHERE (Email = @Email)";
        cmd.Parameters.AddWithValue("@Email", tbEmail.Text);
        cmd.Connection = conn;

        try
        {
            conn.Open();
            int userCount = (int)cmd.ExecuteScalar();
            if (userCount > 0)
                return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            conn.Close();
        }
        return false; 
    }
}