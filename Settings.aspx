<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="AccSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/Settings.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="container">
        <div class="title">
            <h3>Add Table</h3>
        </div>
        <div class="settings" id="table">
            <asp:TextBox ID="tbTableName" runat="server"></asp:TextBox>
            <asp:Button ID="btnAddTable" runat="server" Text="Add" OnClick="addTable" />
            <asp:Button ID="btnRemoveTable" runat="server" Text="Remove" OnClick="removeTable" />
            <asp:Label ID="lblNotification" runat="server"></asp:Label>
        </div>
        <div class="title">
            <h3>Download QR</h3>
        </div>
        <div class="settings" id="QR">
            <asp:Button ID="btnQR" runat="server" Text="DownloadQR" OnClick="downloadQR" />
        </div>
    </div>
</asp:Content>

