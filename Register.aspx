<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/membership.css" rel="stylesheet" type="text/css" />
    <link href="css/register.css" rel="stylesheet" type="text/css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
    <script src="js/Script.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="wrapper">
            <div class="header">
                <a href="home2.aspx">TotoCafe</a>
                <a href="login.aspx" style="position: absolute; right: 0px;">Login</a>
            </div>
            <main>
                <div id="container">
                    <div id="register">
                        <asp:TextBox ID="tbCompanyName" CssClass="inputText" runat="server" Text="Name"></asp:TextBox>
                        <asp:TextBox ID="tbCompanySurname" CssClass="inputText" runat="server" Text="Surname"></asp:TextBox>
                        <asp:TextBox ID="tbCompanyMail" CssClass="inputText" runat="server" Text="Email"></asp:TextBox>
                        <asp:TextBox ID="tbCompanyPassword" CssClass="inputText" runat="server" Text="Password" ToolTip="Password"></asp:TextBox>
                        <asp:Button ID="btnRegister" CssClass="inputButton" runat="server" Text="Sign Up" OnClick="btnLogin_Click" />
                    </div>
                </div>
            </main>
            <footer>
            </footer>
        </div>
    </form>
</body>
</html>
