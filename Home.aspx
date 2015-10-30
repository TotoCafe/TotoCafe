<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    
    <asp:Panel runat="server" ID="panelFirst" Visible="false">
        you have no table yet. click to add..
        <a href="Settings.aspx">settings</a>
    </asp:Panel>
    <asp:Panel runat="server" ID="panelDefault" Visible="false">
        tables....
    </asp:Panel>
    <asp:SqlDataSource ID="SqldataCheckDatabase" runat="server"></asp:SqlDataSource>

</asp:Content>


