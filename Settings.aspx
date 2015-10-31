<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <link href="Content/settings.css" rel="stylesheet" />


    <div class="container">
        <div class="buttons">
            <div class="triggerbuttons">
                <asp:Button Text="Add Table" CssClass="btn btn-default"  OnClick="btnTriggerAddTable_Click" ID="btnTriggerAddTable" runat="server" style="background-color:rebeccapurple; color:white;"/>
            </div>
            <div class="triggerbuttons">
                <asp:Button CssClass="btn btn-default" Text="Remove Table" OnClick="btnTriggerRemoveTable_Click" ID="btnTriggerRemoveTable" runat="server" style="background-color:mediumpurple; color:white;"/>

            </div>
            <div class="triggerbuttons">
                <asp:Button CssClass="btn btn-default" Text="Add Category" OnClick="btnTriggerAddCategory_Click" ID="btnTriggerAddCategory" runat="server" style="background-color:purple; color:white;" />

            </div>
            <div class="triggerbuttons">
                <asp:Button CssClass="btn btn-default" Text="Remove Category" OnClick="btnTriggerRemoveCategory_Click" ID="btnTriggerRemoveCategory" runat="server" style="background-color:lightpink; color:white;" />

            </div>
            <div class="triggerbuttons">
                <asp:Button CssClass="btn btn-default" Text="Add Product" OnClick="btnTriggerAddProduct_Click" ID="btnTriggerAddProduct" runat="server" style="background-color:plum; color:white;" />

            </div>
            <div class="triggerbuttons">
                <asp:Button CssClass="btn btn-default" Text="Remove Product" OnClick="btnTriggerRemoveProduct_Click" ID="btnTriggerRemoveProduct" runat="server" style="background-color:hotpink; color:white;" />

            </div>
        </div>

        <div class="clearfix">
        </div>




        <asp:UpdatePanel ID="updatePanelSettings" runat="server" ChildrenAsTriggers="True">

            <ContentTemplate>
                <asp:Label Text="text" ID="lblStatus" Visible="false" runat="server" />
                <!-- Table -->
                <asp:Panel ID="panelAddTable" runat="server" CssClass="panel panel-primary" Visible="false">
                    <div class="panel-heading">Add table:</div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="col-md-4 control-label" for="textBoxTableName">Table Name: </label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="textBoxTableName" class="form-control input-md" />
                            </div>
                        </div>
                        <div class="form-group" style="padding-top: 40px;">
                            <div class="col-md-3">
                                <asp:Button Text="Save" ID="btnSaveTable" OnClick="btnSaveTable_Click" CssClass="btn btn-success" Style="width: 130px;" runat="server" />
                            </div>
                            <div class="col-md-7">
                                <asp:Label ID="lblSaveTable" Text="You successfully add new table!" Visible="false" runat="server" />

                            </div>
                        </div>

                    </div>

                </asp:Panel>
                <asp:Panel ID="panelRemoveTable" runat="server" CssClass="panel panel-danger" Visible="false">
                    <div class="panel-heading">Remove table:</div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:DropDownList ID="dropDownListTables" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceGetTableS" DataTextField="TableName" DataValueField="TableID"></asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSourceGetTableS" runat="server" ConnectionString="<%$ ConnectionStrings:TotoCafeDB %>" SelectCommand="SELECT [Table].TableID, [Table].TableName, Company.CompanyID FROM Company INNER JOIN [Table] ON Company.CompanyID = [Table].CompanyID WHERE (Company.CompanyID = @CompanyID)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="lblStatus" Name="CompanyID" PropertyName="Text" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                            
                            <div class="row" style="padding-top:20px;">
                                <div class="col-md-3">
                                    <asp:Button Text="Delete" ID="btnRemoveTable" OnClick="btnRemoveTable_Click" CssClass="btn btn-danger" Style="width: 130px;" runat="server" />
                                </div>
                                <div class="col-md-7">
                                    <asp:Label ID="lblRemoveTable" Text="You remove this table!" CssClass="alert alert-danger" Visible="false" runat="server" />

                                </div>
                            </div>
                        </div>
                        <!-- end form-group -->

                    </div>
                    <!-- end panel-body -->
                </asp:Panel>
                <!-- Category -->
                <asp:Panel ID="panelAddCategory" runat="server" CssClass="panel panel-primary" Visible="false">
                    <div class="panel-heading">Add Category:</div>
                   <div class="panel-body">
                        <div class="form-group">
                            <label class="col-md-4 control-label" for="textBoxCategoryName">Category Name: </label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="textBoxCategoryName" class="form-control input-md" />
                            </div>
                        </div>
                        <div class="form-group" style="padding-top: 40px;">
                            <div class="col-md-3">
                                <asp:Button Text="Save" ID="btnSaveCategory" OnClick="btnSaveCategory_Click" CssClass="btn btn-success" Style="width: 130px;" runat="server" />
                            </div>
                            <div class="col-md-7">
                                <asp:Label ID="lblSaveCategory" Text="You successfully add new category!" Visible="false" runat="server" />

                            </div>
                        </div>

                    </div>

                </asp:Panel>
                <asp:Panel ID="panelRemoveCategory" runat="server" CssClass="panel panel-danger" Visible="false">
                    <div class="panel-heading">Remove Category:</div>
                   <div class="panel-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:DropDownList ID="dropDownListShowCategory" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceGetCategories" DataTextField="CategoryName" DataValueField="CategoryID"></asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSourceGetCategories" runat="server" ConnectionString="<%$ ConnectionStrings:TotoCafeDB %>" SelectCommand="SELECT Category.*, Company.CompanyID FROM Category INNER JOIN Company ON Category.CompanyID = Company.CompanyID WHERE (Company.CompanyID = @CompanyID)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="lblStatus" Name="CompanyID" PropertyName="Text" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                            
                            <div class="row" style="padding-top:20px;">
                                <div class="col-md-3">
                                    <asp:Button Text="Delete" ID="btnRemoveCategory" OnClick="btnRemoveCategory_Click" CssClass="btn btn-danger" Style="width: 130px;" runat="server" />
                                </div>
                                <div class="col-md-7">
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
                    <div class="panel-heading">Add table:</div>
                    <div class="panel-body">
                    </div>

                </asp:Panel>
                <asp:Panel ID="panelRemoveProduct" runat="server" CssClass="panel panel-danger" Visible="false">
                    <div class="panel-heading">Remove table:</div>
                    <div class="panel-body">
                    </div>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnTriggerAddTable" />
                <asp:AsyncPostBackTrigger ControlID="btnTriggerRemoveTable" />
                <asp:AsyncPostBackTrigger ControlID="btnTriggerAddProduct" />
                <asp:AsyncPostBackTrigger ControlID="btnTriggerRemoveProduct" />
                <asp:AsyncPostBackTrigger ControlID="btnTriggerAddCategory" />
                <asp:AsyncPostBackTrigger ControlID="btnTriggerRemoveCategory" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

