<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
    <script src="js/Script.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="wrapper">
            <div class="header">
                <a href="home2.aspx">TotoCafe</a>
            </div>
            <main>
                <div id="container">
                    <div id="login">
                        <asp:TextBox ID="tbCompanyMail" CssClass="inputText" runat="server" Text="Username"></asp:TextBox>
                        <asp:TextBox ID="tbCompanyPassword" CssClass="inputText" runat="server" Text="Password" ToolTip="Password"></asp:TextBox>
                        <asp:Button ID="btnLogin" CssClass="inputButton" runat="server" Text="Login" OnClick="btnLogin_Click" />
                    </div>
                </div>
            </main>
            <footer>
            </footer>
        </div>
    </form>
</body>
</html>
