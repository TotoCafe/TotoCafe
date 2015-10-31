<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script>

    <div class="container">
        <h1>Add your Tables</h1>

        <asp:Panel ID="panel" runat="server">
            <div id="container">
                <p id="add_field"><a href="#"><span>Click To Add Table</span></a></p>
            </div>
        </asp:Panel>

        <asp:Button Text="Save" ID="btnSaveTables" runat="server" OnClick="btnSaveTables_Click" />
        <asp:Label Text="text" ID="lblstatus" runat="server" />
    </div>





    <script type="text/javascript">
        var counter = 0;
        $(function () {
            $('p#add_field').click(function () {
                counter += 1;
                $('#container').append(
                '<strong>Table ' + counter + '</strong><br />'
                + '<input id="tableName' + counter + '" name="dynfields[]' + '" type="text" /><br />');

            });
        });
    </script>

</asp:Content>

