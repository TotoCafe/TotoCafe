<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Login.ascx.cs" Inherits="Login" %>

<div id="signin" class="navbar-form navbar-right" role="form">
    <div class="input-group">
        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
        <asp:TextBox ID="txtLoginMail"
            runat="server"
            CssClass="form-control"
            placeholder="E-mail"
            TabIndex="1" />
    </div>
    <div class="input-group">
        <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
        <asp:TextBox ID="txtLoginPassword"
            runat="server"
            placeholder="Password"
            TabIndex="2"
            CssClass="form-control"
            TextMode="Password" />
    </div>
    <asp:Button ID="btnLogin"
        Text="Login"
        CssClass="form-control btn-primary"
        runat="server"
        TabIndex="3"
        OnClick="btnLogin_Click" />
</div>
