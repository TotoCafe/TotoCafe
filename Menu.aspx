<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <!-- Nested GridViews  products inside categories -->
    <style>
        .boundHeading {
            text-align: center;
            font-size: 19px;
            font-weight: 700;
            padding-top: 20px;
          
            border-bottom: 1px solid transparent;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
            color: #a94442;
            background-color: #f2dede;
            border-color: #ebccd1;
         
        }

        #dataList {
            margin: auto;
        }

        .dataListLabel {
            font-weight: bold;
            font-size: 15px;
        }
    </style>
    <h2 style="text-align: center">MENU</h2>
    <div class="container">
        <div class="panel panel-danger" style="width: 100%; margin: auto">
            <!-- haydi başlayalım -->
            <asp:Label Text="text" Visible="false" ID="lblStatus" runat="server" />
            <asp:SqlDataSource ID="SqlDataSourceShowCategories" runat="server" ConnectionString="<%$ ConnectionStrings:TotoCafeDB %>" SelectCommand="SELECT Category.CategoryID, Category.CategoryName, Category.CompanyID FROM Category INNER JOIN Company ON Category.CompanyID = Company.CompanyID WHERE (Company.CompanyID = @CompanyID)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblStatus" Name="CompanyID" PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>

            <asp:GridView ID="GridView1" GridLines="None" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="panel-heading boundHeading">
                                <div class="panel-title" style="font-size: inherit;">
                                    <%# Eval("CategoryName") %>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CategoryID" HeaderText="" ItemStyle-Font-Size="1px" ItemStyle-CssClass="boundHeading" />
                    <asp:TemplateField>
                        <ItemTemplate>
                               <tr>
                                    <td>
                            <div class="panel-body">
                             
                                        <br />
                                        <div id='<%# Eval("CategoryID") %>' style="display: inline" />

                                        <asp:DataList ID="datalist" AutoGenerateColumns="false" runat="server">
                                          
                                                <itemtemplate>
                                                    <div class="row" id="dataList">
                                                        <div class="col-md-3" style="width: 300px;">
                                                            <asp:Label Text="Product Name:" CssClass="dataListLabel" runat="server" />
                                                            <%# Eval("ProductName") %>
                                                        </div>
                                                        <div class="col-md-3" style="width: 300px;">
                                                            <asp:Label Text="Price: " CssClass="dataListLabel" runat="server" />
                                                            <%# Eval("Price") %> TL

                                                        </div>
                                                        <div class="col-md-3" style="width: 300px;">
                                                            <asp:Label CssClass="dataListLabel" Text="Details: " runat="server" />
                                                            <%# Eval("Detail") %>
                                                        </div>
                                                        <div class="col-md-3" style="width: 100px;">
                                                            <asp:Label CssClass="dataListLabel" Text="Credit: " runat="server" />
                                                            <%# Eval("Credit") %>
                                                        </div>
                                                    </div>

                                                </itemtemplate>
                                                <separatortemplate>
                                                    <hr />
                                                </separatortemplate>
                                           
                                        </asp:DataList>
                            </div>
                            <br />
                            </td>
                            </tr>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
        <!-- end panel danger -->
    </div>
</asp:Content>

