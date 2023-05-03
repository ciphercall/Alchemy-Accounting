<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="create-user.aspx.cs"
    Inherits="AlchemyAccounting.cr_user.ui.create_user" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Single Transaction</title>
<%--<script src="../../Scripts/custom.js" type="text/javascript"></script>--%>
    <link href="../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtTransDate,#txtChequeDate").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+0" });
            // document.getElementById("edit").style.visibility = 'hidden';

            var dlg = $("#msg").dialog({
                autoOpen: false,
                modal: false,
                title: "",
                position: ["center", "center"],
                resizable: false
            });
            dlg.parent().appendTo(jQuery("form:first"));
        });

        function shai_dailog() {
            $("#msg").dialog("open");
        }

        document.onkeypress = function (e) {
            if (e.keyCode == 13) {
                shai_dailog();
            }
        }

        //------------- show iframe dialog --------------------
        function showInFrameDialog(ptr, page, newTitle) {
            var width = $(window).width();
            var height = $(window).height();
            var frame = $('<iframe/>');
            frame = $(frame).attr('width', (width - 60));
            frame = $(frame).attr('height', (height - 110));
            frame = $(frame).attr('src', page);

            var container = $('<div id="frameDialog"></div>');
            container = container.append(frame);
            container.dialog({
                autoOpen: true,
                modal: true,
                title: newTitle,
                width: (width - 100),
                height: (height - 50),
                onEscapeClose: false,
                resizable: true,
                dragable: true
            });


            return false;
        }


        //------------- show iframe dialog --------------------
        function showPageInIFrame(page) {
            //    var width = $(window).width();
            //    var height = $(window).height();
            var frame = $('<iframe/>');
            frame = $(frame).attr('width', 200);
            frame = $(frame).attr('height', 200);
            frame = $(frame).attr('src', page);

            var container = $('<div id="frameDialog"></div>');
            container = container.append(frame);

            container.dialog({
                autoOpen: true,
                modal: false,
                title: newTitle,
                width: (width - 100),
                height: (height - 50),
                onEscapeClose: false,
                resizable: true,
                dragable: true
            });
            return false;
        }



    </script>
    <style type="text/css">
        #header
        {
            float: left;
            width: 100%;
            background-color: transparent;
            height: 50px;
        }
        #header h1
        {
            font-family: Century Gothic;
            font-weight: bold;
        }
        #entry
        {
            float: left;
            width: 100%;
            background-color: transparent;
            border: 1px solid #000;
            border-radius: 10px;
            margin-top: 10px;
            margin-bottom: 30px;
        }
        #toolbar
        {
            float: left;
            width: 100%;
            background-color: #cccccc;
            border-radius: 10px 10px 0px 0px;
        }
        
        .style1
        {
            text-align: center;
            font-weight: bold;
            font-family: Century Gothic;
            width: 2px;
        }
        .style2
        {
            font-weight: bold;
            text-align: right;
            width: 320px;
            font-family: Century Gothic;
        }
        .style4
        {
            width: 92px;
        }
        .style6
        {
            width: 320px;
        }
        
        .style8
        {
            width: 7px;
        }
        .style9
        {
            width: 28px;
        }
        .style10
        {
            width: 430px;
        }
        #msg
        {
            float: left;
            width: 40%;
            height: 100px;
        }
        
        #show
        {
            float: left;
            width: 100%;
        }
        #edit
        {
            float: left;
            width: 100%;
            position: inherit;
        }
        #btnEdit
        {
            float: right;
            width: 40px;
            height: 19px;
            padding-bottom: 2px;
            background: #f2f2f2;
            border: 1px solid;
            border-radius: 5px;
        }
        .style19
        {
            text-align: center;
        }
        .txtColor:focus
        {
            border: solid 4px green !important;
        }
        .txtColor
        {
            margin-left: 0px;
            text-align: left;
        }
        .txtaligncenter
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center">
            Create User</h1>
    </div>
    <div id="entry">
        <div id="toolbar">
            <table style="width: 100%;">
                <tr>
                    <td class="style10">
                        &nbsp;
                    </td>
                    <td class="style8">
                        &nbsp;
                    </td>
                    <td class="style9">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <div id="btnEdit" class="style19">
                            <a onclick="return showInFrameDialog(this,'edit-cr-user.aspx','Edit User');" href=""
                                style="font-family: Helvetica Neue,Lucida Grande,Segoe UI,Arial,Helvetica,Verdana,sans-serif;
                                font-size: 1.2em; font-weight: bold; font-style: italic; text-decoration: none;
                                color: #000000;">Edit</a></div>
                    </td>
                    <td class="style8">
                        <asp:Button ID="btnRefresh" runat="server" Font-Bold="True" Font-Italic="True" Text="Refresh" />
                    </td>
                    <td class="style9">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style10">
                        &nbsp;
                    </td>
                    <td class="style8">
                        &nbsp;
                    </td>
                    <td class="style9">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <table id="show" style="width: 100%; margin: 10px 0px 10px 0px;">
            <tr>
                <td class="style6" style="text-align: right">
                    <strong>User Name</strong>
                </td>
                <td class="style1">
                    <strong>:</strong>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" TabIndex="1" Width="40%" CssClass="txtColor"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="style4">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Email
                </td>
                <td class="style1">
                    :
                </td>
                <td style="margin-left: 40px">
                    <asp:TextBox ID="txtEmail" runat="server" TabIndex="2" Width="40%" 
                        CssClass="txtColor" AutoPostBack="True" ontextchanged="txtEmail_TextChanged"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email ID."
                        ControlToValidate="txtEmail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email ID</asp:RegularExpressionValidator>
                    <asp:Label ID="lblchkUser" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="style4">
                    <asp:TextBox ID="txtUsid" runat="server" TabIndex="2" Width="100%" 
                        CssClass="txtColor" Visible="False"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Branch
                </td>
                <td class="style1">
                    :</td>
                <td style="margin-left: 40px">
                    <asp:DropDownList ID="ddlBranch" runat="server" Width="40%" CssClass="txtColor" 
                        TabIndex="3" AutoPostBack="True" 
                        onselectedindexchanged="ddlBranch_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Label ID="lblBrachCD" runat="server" Visible="False"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td class="style4">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    User Type
                </td>
                <td class="style1">
                    :</td>
                <td style="margin-left: 40px">
                    <asp:DropDownList ID="ddlUserTP" runat="server" Width="40%" CssClass="txtColor" 
                        TabIndex="4" AutoPostBack="True" 
                        onselectedindexchanged="ddlUserTP_SelectedIndexChanged">
                        <asp:ListItem>USER</asp:ListItem>
                        <asp:ListItem>ADMIN</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
                <td class="style4">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    Password
                </td>
                <td class="style1">
                    <strong>:</strong>
                </td>
                <td style="margin-left: 40px">
                    <asp:TextBox ID="txtPass" runat="server" TabIndex="5" TextMode="Password" Width="40%"
                        CssClass="txtColor"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="style4">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Confirm Password
                </td>
                <td class="style1">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtConpas" runat="server" TabIndex="6" TextMode="Password" Width="40%"
                        CssClass="txtColor" 
                        ontextchanged="txtConpas_TextChanged"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="style4">
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style6">
                    &nbsp;
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Font-Bold="True" OnClick="btnSave_Click"
                        TabIndex="7" Text="Save" Width="100px" CssClass="txtColor txtaligncenter" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblMsg" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="style4">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <%--<asp:ListItem>Select</asp:ListItem>--%>
</asp:Content>
