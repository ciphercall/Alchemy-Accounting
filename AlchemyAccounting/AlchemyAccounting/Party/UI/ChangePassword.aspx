<%@ Page Title="" Language="C#" MasterPageFile="~/Partymst.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="AlchemyAccounting.Party.UI.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="padding-top: 20px;">
        <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-6">
                <div class="jumbotron" style="box-shadow: 0 5px 5px 5px #888888;">
                    <div class="text-center"><h2>Change your Password</h2></div>
                    <div>
                        <table class="table">
                            <tr>
                                <td>Old Password</td>
                                <td><asp:TextBox runat="server" ID="txtoldpass" TextMode="Password" CssClass="form-control" placeholder="Old password"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>New Password</td>
                                <td><asp:TextBox runat="server" ID="txtnewpass" TextMode="Password" CssClass="form-control" placeholder="New password"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Confirm Password</td>
                                <td><asp:TextBox runat="server" ID="txtconfirmpass"  TextMode="Password"  CssClass="form-control" placeholder="Confirm password"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td class="text-center"><asp:Button runat="server" ID="btnsubmit" Text="Submit" OnClick="btnsubmit_Click"/></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
