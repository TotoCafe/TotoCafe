<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AppTest.aspx.cs" Inherits="AppTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div class="container" style="top:50px">
        <asp:ListBox runat="server"
        ID="lbTest" Height="550px" Width="800px" CssClass="lst" Enabled="False" EnableTheming="False">
    </asp:ListBox>
    </div>
    <asp:SqlDataSource runat="server" />
</asp:Content>

