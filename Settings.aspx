<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <link href="Content/settings.css" rel="stylesheet" />
    <style>
        .btn-settingsPrimary {
            color: #00540A;
            background-color: #ffffff;
            border-color: #2D7012;
        }

            .btn-settingsPrimary:hover,
            .btn-settingsPrimary:focus,
            .btn-settingsPrimary:active,
            .btn-settingsPrimary.active,
            .open .dropdown-toggle.btn-settingsPrimary {
                color: white;
                background-color: #286907;
                border-color: #2D7012;
            }

            .btn-settingsPrimary:active,
            .btn-settingsPrimary.active,
            .open .dropdown-toggle.btn-settingsPrimary {
                background-image: none;
            }

            .btn-settingsPrimary.disabled,
            .btn-settingsPrimary[disabled],
            fieldset[disabled] .btn-settingsPrimary,
            .btn-settingsPrimary.disabled:hover,
            .btn-settingsPrimary[disabled]:hover,
            fieldset[disabled] .btn-settingsPrimary:hover,
            .btn-settingsPrimary.disabled:focus,
            .btn-settingsPrimary[disabled]:focus,
            fieldset[disabled] .btn-settingsPrimary:focus,
            .btn-settingsPrimary.disabled:active,
            .btn-settingsPrimary[disabled]:active,
            fieldset[disabled] .btn-settingsPrimary:active,
            .btn-settingsPrimary.disabled.active,
            .btn-settingsPrimary[disabled].active,
            fieldset[disabled] .btn-settingsPrimary.active {
                background-color: #CDEBD6;
                border-color: #2D7012;
            }

            .btn-settingsPrimary .badge {
                color: #CDEBD6;
                background-color: #00540A;
            }

        /* panel Table */
        .panel-settings {
            border-color: #A6CE95;
        }

            .panel-settings > .panel-heading {
                color: #234214;
                background-color: #E0EFDD;
                border-color: #A6CE95;
            }

                .panel-settings > .panel-heading + .panel-collapse > .panel-body {
                    border-top-color: #ebccd1;
                }

                .panel-settings > .panel-heading .badge {
                    color: #f2dede;
                    background-color: #a94442;
                }

            .panel-settings > .panel-footer + .panel-collapse > .panel-body {
                border-bottom-color: #ebccd1;
            }
    </style>

    <div id="divSettings" class="container">
        <div class="row">
            <div class="container-fluid">
                <div class="col-md-6" style="align-items: center">
                    <div class="triggerbuttons">
                        <asp:Button Text="Tables" runat="server" ID="btnTableSettings" CssClass="btn btn-settingsPrimary" OnClick="btnTableSettings_Click" />
                    </div>
                </div>
                <div class="col-md-6" style="align-items: center">
                    <div class="triggerbuttons" style="align-items: center">
                        <asp:Button Text="Menu" runat="server" ID="btnMenuSettings" CssClass="btn btn-settingsPrimary" OnClick="btnMenuSettings_Click" />
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <asp:UpdatePanel runat="server" ID="settingUpdatePanel" ChildrenAsTriggers="true">
                <ContentTemplate>

                    <asp:Panel runat="server" ID="panelTableSettings" Visible="false" CssClass="panel panel-settings">
                        <div class="panel-heading">Table Settings</div>
                        <div class="panel-body">
                            <div class="col-md-3">
                                <asp:GridView runat="server" ID="gridTables" DataSourceID="getTables" AutoGenerateColumns="False" CssClass="form-group">
                                    <Columns>
                                        <asp:BoundField DataField="TableName" HeaderText="TableName" SortExpression="TableName" />
                                        <asp:BoundField DataField="IsReserved" HeaderText="IsReserved" SortExpression="IsReserved" />
                                        <asp:BoundField DataField="QrCode" HeaderText="QrCode" SortExpression="QrCode" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource runat="server"
                                    ID="getTables"
                                    ConnectionString="<%$ ConnectionStrings:TotoCafeDB %>"
                                    SelectCommand="SELECT [TableName], [IsReserved], [QrCode] FROM [Table] WHERE ([CompanyID] = @CompanyID)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="lblStatus" Name="CompanyID" PropertyName="Text" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <asp:Label Text="There is no table yet.." ID="lblNoTable" runat="server" />
                            </div>
                            <div class="col-md-9">
                                <asp:Panel runat="server">
                                    <div class="triggerbuttons">
                                        <asp:Button Text="Add Table"
                                            CssClass="btn btn-default"
                                            OnClick="btnTriggerAddTable_Click"
                                            ID="btnTriggerAddTable"
                                            runat="server"
                                            Style="background-color: rebeccapurple; color: white;" />
                                    </div>
                                    <div class="triggerbuttons">
                                        <asp:Button CssClass="btn btn-default"
                                            Text="Remove Table"
                                            OnClick="btnTriggerRemoveTable_Click"
                                            ID="btnTriggerRemoveTable"
                                            runat="server"
                                            Style="background-color: mediumpurple; color: white;" />
                                    </div>
                                </asp:Panel>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:Label Text="text" ID="lblStatus" Visible="false" runat="server" />
                                        <!-- Table -->
                                        <asp:Panel ID="panelAddTable" runat="server" CssClass="panel panel-primary" Visible="false">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label" for="textBoxTableName">Table Name: </label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox runat="server" ID="textBoxTableName" class="form-control input-md" />
                                                    </div>
                                                </div>
                                                <div class="form-group" style="padding-top: 40px;">
                                                    <div class="col-md-2">
                                                        <asp:Button Text="Save"
                                                            ID="btnSaveTable"
                                                            OnClick="btnSaveTable_Click"
                                                            CssClass="btn btn-success"
                                                            Style="width: 130px;"
                                                            runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Button Text="Close"
                                                            runat="server"
                                                            ID="btnCancelAdd"
                                                            CssClass="btn btn-danger"
                                                            Style="width: 130px"
                                                            OnClick="btnCancelAdd_Click" />
                                                    </div>
                                                    <div class="col-md-7">
                                                        <asp:Label ID="lblSaveTable" Text="You successfully add new table!" Visible="false" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="panelRemoveTable" runat="server" CssClass="panel panel-danger" Visible="false">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="dropDownListTables"
                                                                CssClass="form-control"
                                                                runat="server"
                                                                DataSourceID="SqlDataSourceGetTableS"
                                                                DataTextField="TableName"
                                                                DataValueField="TableID" AppendDataBoundItems="True">
                                                                <asp:ListItem Value="0">&lt;Select Table to Remove&gt;</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="SqlDataSourceGetTableS"
                                                                runat="server" ConnectionString="<%$ ConnectionStrings:TotoCafeDB %>"
                                                                SelectCommand="SELECT [Table].TableID, [Table].TableName, Company.CompanyID FROM Company INNER JOIN [Table] ON Company.CompanyID = [Table].CompanyID WHERE (Company.CompanyID = @CompanyID)">
                                                                <SelectParameters>
                                                                    <asp:ControlParameter ControlID="lblStatus" Name="CompanyID" PropertyName="Text" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Button Text="Delete"
                                                                ID="btnRemoveTable"
                                                                OnClick="btnRemoveTable_Click"
                                                                CssClass="btn btn-success"
                                                                Style="width: 130px;"
                                                                runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Button Text="Close"
                                                                ID="btnCancelRemove"
                                                                CssClass="btn btn-danger"
                                                                Style="width: 130px;"
                                                                runat="server"
                                                                OnClick="btnCancelRemove_Click" />
                                                        </div>
                                                    </div>

                                                    <div class="row" style="padding-top: 20px;">
                                                        <div class="col-md-7">
                                                            <asp:Label ID="lblRemoveTable"
                                                                Text="You remove this table!"
                                                                CssClass="alert alert-danger"
                                                                Visible="false"
                                                                runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- end form-group -->
                                            </div>
                                            <!-- end panel-body -->
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnTriggerRemoveTable" />
                                        <asp:AsyncPostBackTrigger ControlID="btnTriggerAddTable" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="panelQrImages">
                            <asp:Button Text="Save Qr" runat="server" ID="qrButton" OnClick="qrButton_Click" />
                        </asp:Panel>
                    </asp:Panel>
                    <!-- END PanelTableSettings -->

                    <asp:Panel runat="server" ID="panelMenuSettings" Visible="false" CssClass="panel panel-settings">
                        <div class="panel-heading">Menu Settings</div>
                        <div class="panel-body">
                            <div class="col-md-4">
                                <asp:GridView runat="server"
                                    ID="gridCategory" AutoGenerateColumns="False" DataSourceID="dataGetCategories" Width="319px">
                                    <Columns>
                                        <asp:BoundField HeaderText="CategoryName" DataField="CategoryName" />
                                        <asp:TemplateField HeaderText="Products">
                                            <ItemTemplate>
                                                <asp:GridView ID="gridProduct" runat="server" AutoGenerateColumns="False" DataSourceID="dataGetProducts">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="ProductName" DataField="ProductName" SortExpression="ProductName" />
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:SqlDataSource ID="dataGetProducts" runat="server" ConnectionString="<%$ ConnectionStrings:TotoCafeDB %>" SelectCommand="SELECT Product.ProductName FROM Product INNER JOIN Category ON Product.CategoryID = Category.CategoryID WHERE (Category.CompanyID = @CompanyID)">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="lblStatus" Name="CompanyID" PropertyName="Text" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="dataGetCategories" runat="server"
                                    ConnectionString="<%$ ConnectionStrings:TotoCafeDB %>"
                                    SelectCommand="SELECT [CategoryName] FROM [Category] WHERE ([CompanyID] = @CompanyID)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="lblStatus" Name="CompanyID" PropertyName="Text" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                            <div class="col-md-8">
                                <div class="container">
                                    <div class="buttons">

                                        <div class="triggerbuttons">
                                            <asp:Button CssClass="btn btn-default" Text="Add Category" OnClick="btnTriggerAddCategory_Click" ID="btnTriggerAddCategory" runat="server" Style="background-color: purple; color: white;" />

                                        </div>
                                        <div class="triggerbuttons">
                                            <asp:Button CssClass="btn btn-default" Text="Remove Category" OnClick="btnTriggerRemoveCategory_Click" ID="btnTriggerRemoveCategory" runat="server" Style="background-color: purple; color: white;" />

                                        </div>
                                        <div class="triggerbuttons">
                                            <asp:Button CssClass="btn btn-default" Text="Add Product" OnClick="btnTriggerAddProduct_Click" ID="btnTriggerAddProduct" runat="server" Style="background-color: purple; color: white;" />

                                        </div>
                                        <div class="triggerbuttons">
                                            <asp:Button CssClass="btn btn-default" Text="Remove Product" OnClick="btnTriggerRemoveProduct_Click" ID="btnTriggerRemoveProduct" runat="server" Style="background-color: purple; color: white;" />

                                        </div>

                                    </div>
                                </div>
                                <hr />
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <!-- Category -->
                                        <asp:Panel ID="panelAddCategory" runat="server" CssClass="panel panel-primary" Visible="false">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label" for="textBoxCategoryName">Category Name: </label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox runat="server" ID="textBoxCategoryName" class="form-control input-md" />
                                                    </div>
                                                </div>
                                                <div class="row" style="padding-top: 40px;">
                                                    <div class="col-md-2">
                                                        <asp:Button Text="Save" ID="btnSaveCategory" OnClick="btnSaveCategory_Click" CssClass="btn btn-success" Style="width: 130px;" runat="server" />
                                                    </div>

                                                    <div class="col-md-2" style="padding-left: 30px;">
                                                        <asp:Button Text="Close" ID="btnCloseCategory" CssClass="btn btn-danger" Style="width: 130px;" runat="server" OnClick="btnCloseCategory_Click" />
                                                    </div>
                                                    <div class="col-md-8" style="padding-left: 50px;">
                                                        <asp:Label ID="lblSaveCategory" Text="You successfully add new category!" Visible="false" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="panelRemoveCategory" runat="server" CssClass="panel panel-danger" Visible="false">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-md-5">
                                                            <asp:DropDownList ID="dropDownListShowCategory" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceGetCategories" DataTextField="CategoryName" DataValueField="CategoryID" AppendDataBoundItems="True">
                                                                <asp:ListItem Value="0">&lt;Select Category to Remove&gt;</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="SqlDataSourceGetCategories" runat="server" ConnectionString="<%$ ConnectionStrings:TotoCafeDB %>" SelectCommand="SELECT Category.*, Company.CompanyID FROM Category INNER JOIN Company ON Category.CompanyID = Company.CompanyID WHERE (Company.CompanyID = @CompanyID)">
                                                                <SelectParameters>
                                                                    <asp:ControlParameter ControlID="lblStatus" Name="CompanyID" PropertyName="Text" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="padding-top: 20px;">
                                                        <div class="col-md-2">
                                                            <asp:Button Text="Delete" ID="btnRemoveCategory" OnClick="btnRemoveCategory_Click" CssClass="btn btn-success" Style="width: 130px;" runat="server" />
                                                        </div>
                                                        <div class="col-md-2" style="padding-left: 30px;">
                                                            <asp:Button Text="Close" ID="btnCloseCategoryRemove" CssClass="btn btn-danger" Style="width: 130px;" runat="server" OnClick="btnCloseCategoryRemove_Click" />
                                                        </div>
                                                        <div class="col-md-8" style="padding-left: 50px;">
                                                            <asp:Label ID="lblRemoveCategory" Text="You remove this category!" CssClass="alert alert-danger" Visible="false" runat="server" />

                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- end form-group -->

                                            </div>
                                            <!-- end panel-body -->
                                        </asp:Panel>
                                        <!-- Product -->
                                        <asp:Panel ID="panelAddProduct" runat="server" CssClass="panel panel-primary" Visible="false">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label" for="txtProductName">Name: </label>
                                                    <div class="col-md-5 clearfix">
                                                        <asp:TextBox runat="server" ID="txtProductName" class="form-control input-md" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label" for="txtProductPrice">Price: </label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox runat="server" ID="txtProductPrice" class="form-control input-md" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label" for="txtProductDetails">Details: </label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox runat="server" ID="txtProductDetails" class="form-control input-md" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label" for="txtProductCredit">Credit: </label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox runat="server" ID="txtProductCredit" class="form-control input-md" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label" for="ddlCategorySelect">Category: </label>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList ID="ddlCategorySelect" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceGetCategories" DataTextField="CategoryName" DataValueField="CategoryID" AppendDataBoundItems="True">
                                                            <asp:ListItem Value="0">&lt;Select Category&gt;</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group" style="padding-top: 180px;">
                                                    <div class="col-md-2">
                                                        <asp:Button ID="btnAddProduct" Text="Add" CssClass="btn btn-success" Style="width: 130px;" runat="server" OnClick="btnAddProduct_Click" />
                                                    </div>
                                                    <div class="col-md-2" style="padding-left: 30px;">
                                                        <asp:Button ID="btnCloseAddProduct" Text="Close" CssClass="btn btn-danger" Style="width: 130px;" runat="server" OnClick="btnCloseAddProduct_Click" />
                                                    </div>
                                                    <div class="col-md-4" style="padding-left: 50px;">
                                                        <asp:Label ID="lblSaveProduct" Text="You add new product!" CssClass="alert alert-success" Visible="false" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="panelRemoveProduct" runat="server" CssClass="panel panel-danger" Visible="false">
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label" for="ddlCategorySelect">Remove Product: </label>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList ID="ddlRemoveProduct" CssClass="form-control" runat="server" DataSourceID="dataSourceGetProducts" DataTextField="ProductName" DataValueField="ProductID" AppendDataBoundItems="True">
                                                            <asp:ListItem Value="0">&lt;Select Product to Remove&gt;</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="dataSourceGetProducts" runat="server" ConnectionString="<%$ ConnectionStrings:TotoCafeDB %>" SelectCommand="SELECT Product.ProductName, Product.ProductID FROM Product INNER JOIN Category ON Product.CategoryID = Category.CategoryID WHERE (Category.CompanyID = @CompanyID)">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="lblStatus" Name="CompanyID" PropertyName="Text" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <div class="col-md-2">
                                                        <asp:Button ID="btnRemoveProduct" Text="Remove" CssClass="btn btn-success" Style="width: 130px;" runat="server" OnClick="btnRemoveProduct_Click" />
                                                    </div>
                                                    <div class="col-md-2" style="padding-left: 30px">
                                                        <asp:Button ID="btnCloseRemoveProduct" Text="Close" CssClass="btn btn-danger" Style="width: 130px;" runat="server" OnClick="btnCloseRemoveProduct_Click" />
                                                    </div>
                                                    <div class="col-md-8" style="padding-left: 50px;">
                                                        <asp:Label ID="lblRemoveProduct" Text="You remove this product!" CssClass="alert alert-danger" Visible="false" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                    </asp:Panel>
                    <!-- panelMenuSettings -->
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnTableSettings" />
                    <asp:AsyncPostBackTrigger ControlID="btnMenuSettings" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancelAdd" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancelRemove" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:SqlDataSource runat="server" />
</asp:Content>
