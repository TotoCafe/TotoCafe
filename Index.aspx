<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login or Signup</title>
    <%--Roboto Font--%>
    <link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet" />
    <%--Index CSS--%>
    <link href="css/Index.css" rel="stylesheet" />
    <%--Validate JavaScript--%>
    <script src="js/Validate.js"></script>
</head>
<body>
    <form id="frmIndex" runat="server">
        <div id="header">
            <a id="logo" href="Home.aspx">TotoCafe</a>
            <table id="login">
                <tr>
                    <td>
                        <label for="tbEmail">Email</label>
                    </td>
                    <td>
                        <label for="tbPassword">Password</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="loginEmail" CssClass="login-input" runat="server" TextMode="Email"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="loginPassword" CssClass="login-input" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnLogin" CssClass="inputButton" runat="server" Text="Login" OnClick="cmpLogin"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="cbRemember" runat="server" />
                        <label for="cbRemember">Remember Me</label>
                    </td>
                    <td>
                        <asp:HyperLink ID="hlForgot" runat="server">Forgot your password?</asp:HyperLink>
                    </td>
                </tr>
            </table>
            <div id="register" runat="server">
                <asp:Label ID="lblRegister" CssClass="register-input" runat="server" Text="Register"></asp:Label>
                <asp:Label ID="lblError" CssClass="register-input" runat="server"></asp:Label>
                <asp:TextBox ID="registerName" CssClass="register-input" runat="server" placeholder="Name"></asp:TextBox>
                <asp:TextBox ID="registerSurname" CssClass="register-input" runat="server" placeholder="Surname"></asp:TextBox>
                <asp:TextBox ID="registerEmail" CssClass="register-input" runat="server" placeholder="Email"></asp:TextBox>
                <asp:TextBox ID="registerPassword" CssClass="register-input" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                <asp:TextBox ID="registerAddress" CssClass="register-input" runat="server" placeholder="Address" TextMode="MultiLine" Style="resize: none;" Height="75px"></asp:TextBox>
                <asp:TextBox ID="registerPhone" CssClass="register-input" runat="server" placeholder="Phone"></asp:TextBox>
                <asp:DropDownList ID="registerCity" CssClass="register-input" runat="server" DataSourceID="SqlDataSourceCity" DataTextField="CityName" DataValueField="CityID"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceCity" runat="server" ConnectionString="<%$ ConnectionStrings:TotoCafeDB %>" SelectCommand="SELECT * FROM [City]"></asp:SqlDataSource>
                <asp:Button ID="btnRegister" CssClass="inputButton" runat="server" Text="Sign Up" OnClick="cmpRegister"/>
            </div>
        </div>
    </form>
</body>
</html>
