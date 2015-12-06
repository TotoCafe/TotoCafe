<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/membership.css" rel="stylesheet" type="text/css" />
    <link href="css/register.css" rel="stylesheet" type="text/css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
    <script src="js/MasterPage.js"></script>
    <script src="js/Validate.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="wrapper">
            <div class="header">
                <a href="home.aspx">TotoCafe</a>
                <a href="login.aspx" style="position: absolute; right: 0px;">Login</a>
            </div>
            <main>
                <div id="container">
                    <div id="register">
                        <asp:Label ID="lblError" runat="server"></asp:Label>
                        <asp:TextBox ID="tbName" CssClass="inputText" runat="server" Text="Name"></asp:TextBox>
                        <asp:TextBox ID="tbEmail" CssClass="inputText" runat="server" Text="Email"></asp:TextBox>
                        <asp:TextBox ID="tbPassword" CssClass="inputText" runat="server" Text="Password"></asp:TextBox>
                        <asp:TextBox ID="tbRePassword" CssClass="inputText" runat="server" Text="Reenter password"></asp:TextBox>
                        <asp:TextBox ID="tbAddress" CssClass="inputText" runat="server" Text="Address" TextMode="MultiLine" EnableTheming="False"></asp:TextBox>
                        <asp:TextBox ID="tbPhone" CssClass="inputText" runat="server" Text="Phone"></asp:TextBox>
                        <asp:DropDownList ID="ddlCity" CssClass="inputText" runat="server" DataSourceID="SqlDataSourceCity" DataTextField="CityName" DataValueField="CityID"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSourceCity" runat="server" ConnectionString="<%$ ConnectionStrings:TotoCafeDB %>" SelectCommand="SELECT * FROM [City]"></asp:SqlDataSource>
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
