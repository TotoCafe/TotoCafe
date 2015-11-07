<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<%@ Register Src="~/Login.ascx" TagPrefix="uc1" TagName="Login" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!-- Css -->
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.min.css" rel="stylesheet" />
    <!-- Scripts -->
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/jquery-2.1.4.min.js"></script>

</head>
<body>
    <form id="form" runat="server">

        <div>
            <div class="container">
                <nav class="navbar navbar-default" id="defaultNavigation">
                    <div class="container-fluid">
                        <!-- Brand and toggle get grouped for better mobile display -->
                        <div class="navbar-header">
                            <button type="button"
                                class="navbar-toggle collapsed"
                                data-toggle="collapse"
                                data-target="#bs-example-navbar-collapse-1"
                                aria-expanded="false">
                            </button>
                            <a class="navbar-brand" href="#">
                                <asp:Image ID="imgBrand"
                                    ImageUrl="~/images/myLogo.svg"
                                    Width="150"
                                    AlternateText="TotoCafe"
                                    runat="server" /></a>
                        </div>
                        <uc1:Login runat="server" ID="Login" />
                        <!-- /.navbar-collapse -->
                    </div>
                    <!-- /.container-fluid -->
                </nav>
                <!-- end Navigasyon part -->

                <!-- Content -->
                <div class="row">
                    <div class="col-md-7">
                        <div class="jumbotron">
                        </div>
                    </div>
                    <!-- end-left-side -->

                    <!-- right-side-->
                    <div class="col-md-5">

                        <h2>Sign up</h2>
                        <h5>It's free and always will be.</h5>
                        <hr class="colorgraph" />
                        <!-- Company Name -->
                        <div class="form-group has-feedback">
                            <asp:TextBox ID="txtCompanyName"
                                CssClass="form-control"
                                placeholder="Company Name"
                                MaxLength="50"
                                runat="server"
                                TabIndex="4" />

                        </div>
                        <!-- Email -->
                        <div class="form-group has-feedback">
                            <asp:TextBox ID="txtCompanyEmail"
                                CssClass="form-control"
                                placeholder="E-mail"
                                runat="server"
                                TextMode="Email"
                                MaxLength="50"
                                TabIndex="5" />

                            <span class="form-control-feedback" id="feedback"></span>
                            <span id="inUse"></span>
                        </div>
                        <!-- Password -->
                        <div class="row">
                            <div class="col-xs-12 col-sm-6 col-md-6">
                                <div class="form-group has-feedback">
                                    <asp:TextBox ID="txtCompanyPassword"
                                        CssClass="form-control"
                                        placeholder="Password"
                                        runat="server"
                                        MaxLength="50"
                                        TabIndex="6"
                                        TextMode="Password" />

                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-md-6">
                                <div id="confirm" class="form-group has-feedback">
                                    <asp:TextBox ID="txtPasswordConfirm"
                                        CssClass="form-control"
                                        placeholder="Confirm Password"
                                        runat="server"
                                        TabIndex="7"
                                        TextMode="Password" />

                                </div>
                            </div>
                        </div>
                        <!-- Address -->
                        <div class="form-group has-feedback">
                            <asp:TextBox ID="txtCompanyAddress"
                                CssClass="form-control"
                                placeholder="Address"
                                runat="server"
                                TabIndex="8"
                                TextMode="MultiLine"
                                MaxLength="50" />

                        </div>
                        <div class="row">
                            <!-- City -->
                            <div class="col-xs-12 col-sm-6 col-md-6">
                                <div class="form-group has-feedback">
                                    <asp:DropDownList ID="ddlCity"
                                        CssClass="form-control"
                                        runat="server"
                                        DataSourceID="sdsCity"
                                        DataTextField="CityName"
                                        DataValueField="CityID"
                                        AppendDataBoundItems="True"
                                        TabIndex="9">
                                        <asp:ListItem Text="Select City"
                                            Value="0" />
                                    </asp:DropDownList>

                                    <asp:SqlDataSource ID="sdsCity"
                                        runat="server"
                                        ConnectionString="<%$ ConnectionStrings:TotoCafeDB %>"
                                        SelectCommand="SELECT * FROM [City]"></asp:SqlDataSource>
                                </div>
                            </div>
                            <!-- Phone -->
                            <div class="col-xs-12 col-sm-6 col-md-6">
                                <div class="form-group has-feedback">
                                    <asp:TextBox ID="txtCompanyPhone"
                                        CssClass="form-control"
                                        placeholder="Phone Number"
                                        MaxLength="50" runat="server"
                                        TabIndex="10"
                                        TextMode="Phone" />

                                </div>
                            </div>
                        </div>
                        <hr class="colorgraph" />
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Button ID="btnSignUp"
                                    Text="Sign Up"
                                    CssClass="btn btn-success"
                                    Style="width: 120%"
                                    TabIndex="11"
                                    runat="server"
                                    OnClick="btnSignUp_Click" />
                            </div>
                            <div class="col-md-9">
                                <p style="font-size: 11px;">
                                    By clicking Sign Up, 
                                    you agree to our Terms and 
                                    that you have read our 
                                    Data Policy, including our Cookie Use.  
                                </p>
                            </div>
                        </div>
                    </div>
                    <!-- right-side-register-part -->
                </div>
                <!-- end row -->
            </div>
            <!-- end div -->
        </div>

        <script>
            $(document).ready(function () {
                $('#txtCompanyEmail').change(function () {
                    var CompanyEmail = $(this);
                    var inUse = $('#inUse');
                    var feedback = $('#feedback');
                    if (CompanyEmail.val().length > 0) {
                        $.ajax({
                            type: "POST",
                            url: "Index.aspx/CheckCompanyEmail",
                            data: "{'CompanyEmail':'" + CompanyEmail.val() + "'}",
                            contentType: "application/json;charset=utf-8",
                            datatype: "json",
                            success: function (msg) {
                                if (msg.d == "<font color='#cc0000'><span class='glyphicon glyphicon-remove' aria-hidden='true'></span></font>") {
                                    inUse.html("<font color='#cc0000' style=\"font-size:11px\">Sorry, it looks like <b>"
                                    + CompanyEmail.val() + "</b> belongs to an existing account.</font>")
                                    feedback.html(msg.d);
                                }
                                else {
                                    feedback.html(msg.d)
                                    inUse.html("");
                                }
                            }
                        })
                    } else {
                        inUse.html("");
                        feedback.html("");
                    }
                });
            })
        </script>

    </form>
</body>
</html>
