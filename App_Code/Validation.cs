using System.Drawing;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Validation
/// </summary>
public class Validation
{
    private Color defaultColor = ColorTranslator.FromHtml("#72B0D4");
    private bool confirm;
    public bool Confirm
    {
        get { return confirm; }
    }
    public void validateTextBox(TextBox tbTextBox)
    {
        string defaultValue = tbTextBox.ID.Replace("tb", "");

        if (string.IsNullOrEmpty(tbTextBox.Text) || tbTextBox.Text == defaultValue || tbTextBox.Text == defaultValue + " is required.")
        {
            tbTextBox.ForeColor = Color.Red;
            tbTextBox.Text = defaultValue + " is required.";
            confirm = false;
        }
        else
        {
            tbTextBox.ForeColor = defaultColor;
            confirm = true;
        }
    }
    public void validatePassword(TextBox tbPassword)
    {
        string regPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,16}$";
        if (!Regex.IsMatch(tbPassword.Text, regPassword))
        {
            tbPassword.Text = "Please enter a valid password.";
            tbPassword.ForeColor = Color.Red;
            confirm = false;
            tbPassword.Attributes.Add("type", "text");
        }
        else
        {
            tbPassword.Attributes.Add("type", "password");
            tbPassword.ForeColor = defaultColor;
            confirm = true;
        }
    }
    public void validateEmail(TextBox tbEmail)
    {
        string regEmail = @"[a-zA-Z0-9]+(?:(\.|_)[A-Za-z0-9!#$%&'*+/=?^`{|}~-]+)*@(?!([a-zA-Z0-9]*\.[a-zA-Z0-9]*\.[a-zA-Z0-9]*\.))(?:[A-Za-z0-9](?:[a-zA-Z0-9-]*[A-Za-z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?";
        if (!Regex.IsMatch(tbEmail.Text, regEmail))
        {
            tbEmail.Text = "Please enter a valid email address.";
            tbEmail.ForeColor = Color.Red;
            confirm = false;
        }
        else
        {
            tbEmail.ForeColor = defaultColor;
            confirm = true;
        }
    }
}