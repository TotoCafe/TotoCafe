<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="container-fluid">

        <asp:UpdatePanel runat="server" ID="upTables" ChildrenAsTriggers="true">
            <ContentTemplate>
                <asp:Panel runat="server" ID="panelNoTable" Visible="true">
                    <p id="noTable">You have NO Table yet click <a href="Settings.aspx">settings</a> to add..</p>
                </asp:Panel>
                <asp:Panel runat="server" ID="panelTables" Visible="false"></asp:Panel>
                <asp:Panel runat="server" ID="panelTableSummary" Visible="false">
                    <div id="orderSummary" class="col-lg-push-6">
                        <asp:Panel runat="server" ID="panelOrderList">
                            <asp:Button Text="back" OnClick="back" runat="server" />
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>

    </div>
</asp:Content>
