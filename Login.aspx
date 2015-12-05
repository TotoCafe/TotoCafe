﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/membership.css" rel="stylesheet" type="text/css" />
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
    <script src="js/Validate.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="wrapper">
            <div class="header">
                <a href="home2.aspx">TotoCafe</a>
                <a href="register.aspx" style="position: absolute; right: 0px;">Register</a>
            </div>
            <main>
                <div id="container">
                    <div id="login">
                        <asp:TextBox ID="tbEmail" CssClass="inputText" runat="server" Text="Email"></asp:TextBox>
                        <asp:CustomValidator ID="cvEmail" runat="server" OnServerValidate="validateEmail" ControlToValidate="tbEmail"></asp:CustomValidator>
                        <asp:TextBox ID="tbPassword" CssClass="inputText" runat="server" Text="Password"></asp:TextBox>
                        <asp:CustomValidator ID="cvPassword" runat="server" OnServerValidate="validatePassword" ControlToValidate="tbPassword"></asp:CustomValidator>
                        <asp:Button ID="btnLogin" CssClass="inputButton" runat="server" Text="Login" OnClick="cmpLogin"/>
                        <div class="remember">
                            <asp:CheckBox ID="cbRemember" runat="server" />
                            <asp:Label ID="lblRemember" runat="server" Text="Remember Me"></asp:Label>
                            <a id="lnkForgot" href="">Forgot Password?</a>
                        </div>
                    </div>
                </div>
            </main>
            <footer>
            </footer>
        </div>
    </form>
</body>
</html>
