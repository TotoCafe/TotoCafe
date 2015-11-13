<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AppTest.aspx.cs" Inherits="AppTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div class="container">
        <asp:ListBox runat="server"
        ID="lbTest" Height="650px" Width="800px" CssClass="lst">
    </asp:ListBox>
    </div>
</asp:Content>

