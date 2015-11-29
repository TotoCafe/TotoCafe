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
                        <asp:TextBox ID="tbCompanyName" CssClass="inputText" runat="server" Text="Name" ></asp:TextBox>
                        <asp:TextBox ID="tbCompanyEmail" CssClass="inputText" runat="server" Text="Email" TextMode="Email" ></asp:TextBox>
                        <asp:TextBox ID="tbCompanyPassword" CssClass="inputText" runat="server" Text="Password" ToolTip="Password" TextMode="SingleLine"></asp:TextBox>
                        <asp:TextBox ID="tbCompanyAddress" CssClass="inputText" runat="server" Text="Address"  TextMode="MultiLine" EnableTheming="False"></asp:TextBox>
                        <asp:TextBox ID="tbCompanyPhone" CssClass="inputText" runat="server" Text="Phone" TextMode="Phone" ></asp:TextBox>
                        <asp:DropDownList ID="ddlCity" CssClass="inputText" runat="server">
                            <asp:ListItem Value="26">Eskişehir</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnRegister" CssClass="inputButton" runat="server" Text="Sign Up" OnClick="cmpRegister" />
                    </div>
                </div>
            </main>
            <footer>
            </footer>
        </div>
    </form>
</body>
</html>
