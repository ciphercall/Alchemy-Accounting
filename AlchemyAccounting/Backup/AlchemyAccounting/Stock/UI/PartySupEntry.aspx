<%@ Page Title="Category & Item Entry" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"  CodeBehind="PartySupEntry.aspx.cs" Inherits="AlchemyAccounting.Stock.UI.PartySupEntry" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>

    <script type ="text/javascript">
        $(document).ready(function () {
            var dlg = $("#dialog").dialog({
                autoOpen: false,
                modal: false,
                title: "",
                position: ["center", "center"],
                resizable: false
            });
            dlg.parent().appendTo(jQuery("form:first"));
        });
        function shai_dailog() {
            $("#dialog").dialog("open");
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
            frame = $(frame).attr('width', 300);
            frame = $(frame).attr('height', 300);
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

    <style type ="text/css">
        #header
        {
            float: left;
            width:100%;
            background-color: transparent;
            height: 50px;
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
        #btnEdit
        {
            border-style: solid;
            border-color: inherit;
            border-width: 1px;
            float:right;
            width:40px;
            height:19px;
            padding-bottom: 2px;
            background: #f2f2f2;
            border-radius: 5px;
            text-align: center;
        }
        #toolbar
        {
            float:left;
            width:100%;
            background-color: #cccccc;
            border-radius: 10px 10px 0px 0px;
        }
        #grid
        {
            float:left;
        }
        .style3
        {
        width: 256px;
    }
        #dialog
        {
            float:left;
            width:30%;
        }
        .style4
        {
            text-align: center;
        }
        .style5
        {
            width: 134px;
        }
        .style7
        {
            width: 420px;
        }
        .style8
        {
            width: 276px;
        }
        .style9
        {
            width: 270px;
        }
        .style10
        {
            width: 203px;
        }
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center" style="font-weight: bold;">PARTY & SUPPLIER ENTRY</h1>
    </div>
    <div id = "entry">
        <div id="toolbar">
        
        <table style="width:100%;">
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style8">
                    &nbsp;</td>
                <td class="style9">
                    <asp:Label ID="lblPS_ID" runat="server" Visible="False"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" class="style7">
                <div id= "btnEdit" class="style19">
                    
                <a onclick="return showInFrameDialog(this,'EditPSEntry.aspx','Edit Party Suppliar');" 
                        href="" 
                        style= "font-family:Helvetica Neue,Lucida Grande,Segoe UI,Arial,Helvetica,Verdana,sans-serif; font-size:1.2em; font-weight:bold; font-style:italic; text-decoration: none; color:#000000; text-align: center;">Edit</a></div>
                </td>
                <td class="style8">
                    <asp:Button ID="btnRefresh" runat="server" Font-Bold="True" Font-Italic="True" 
                        Text="Refresh" onclick="btnRefresh_Click" />
                </td>
                <td class="style9">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style8">
                    &nbsp;</td>
                <td class="style9">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        
    </div>
        <table style="width:100%;">
            <tr>
                <td class="style10">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" class="style10">
                    <b>PS Type</b></td>
                <td class="bold" style="text-align: center">
                    :</td>
                <td class="style3">
                    <asp:DropDownList ID="ddlPSTP" runat="server" TabIndex="1" Width="200px" 
                        AutoPostBack="True" onselectedindexchanged="ddlPSTP_SelectedIndexChanged">
                        <asp:ListItem Value="P">PARTY</asp:ListItem>
                        <asp:ListItem Value="S">SUPPLIER</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" class="style10">
                    <b>PS Name</b></td>
                <td class="bold">
                    :</td>
                <td class="style3">
                    <asp:TextBox ID="txtPNM" runat="server" TabIndex="2" Width="300px" 
                        AutoPostBack="True" ontextchanged="txtPNM_TextChanged"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                        DelimiterCharacters="" Enabled="True" ServicePath="" TargetControlID="txtPNM" 
                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                        UseContextKey="True" ServiceMethod="GetCompletionListParty">
                    </asp:AutoCompleteExtender>
                    <asp:TextBox ID="txtSNM" runat="server" TabIndex="2" Width="300px" 
                        AutoPostBack="True" ontextchanged="txtSNM_TextChanged"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtSNM_AutoCompleteExtender" runat="server" 
                        DelimiterCharacters="" Enabled="True" ServicePath="" TargetControlID="txtSNM"
                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                        UseContextKey="True" ServiceMethod="GetCompletionListSuppliar">
                    </asp:AutoCompleteExtender>
                </td>
                <td>
                    <asp:TextBox ID="txtPSCD" runat="server" ReadOnly="True" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right" class="style10">
                    <b>City</b></td>
                <td class="bold">
                    :</td>
                <td class="style3">
                    <asp:TextBox ID="txtCity" runat="server" TabIndex="3" Width="200px" 
                        ontextchanged="txtCity_TextChanged"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" class="style10">
                    <b>Address</b></td>
                <td class="bold">
                    :</td>
                <td class="style3">
                    <asp:TextBox ID="txtAddress" runat="server" TabIndex="4" TextMode="MultiLine" 
                        Width="300px" ontextchanged="txtAddress_TextChanged"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" class="style10">
                    <b>Contact No</b></td>
                <td class="bold">
                    :</td>
                <td class="style3">
                    <asp:TextBox ID="txtContact" runat="server" TabIndex="5" Width="200px" 
                        ontextchanged="txtContact_TextChanged"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" class="style10">
                    <b>Email</b></td>
                <td class="bold">
                    :</td>
                <td class="style3">
                    <asp:TextBox ID="txtEmail" runat="server" TabIndex="6" Width="200px" 
                        ontextchanged="txtEmail_TextChanged"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" class="style10">
                    <b>Web ID</b></td>
                <td class="bold">
                    :</td>
                <td class="style3">
                    <asp:TextBox ID="txtWebID" runat="server" TabIndex="7" Width="200px" 
                        ontextchanged="txtWebID_TextChanged"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" class="style10">
                    <b>Contact Person Name</b></td>
                <td class="bold">
                    :</td>
                <td class="style3">
                    <asp:TextBox ID="txtCPNM" runat="server" TabIndex="8" Width="200px" 
                        ontextchanged="txtCPNM_TextChanged"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" class="style10">
                    <b>Contact Person No</b></td>
                <td class="bold">
                    :</td>
                <td class="style3">
                    <asp:TextBox ID="txtCPNO" runat="server" TabIndex="9" Width="200px" 
                        ontextchanged="txtCPNO_TextChanged"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" class="style10">
                    <b>Remarks</b></td>
                <td class="bold">
                    :</td>
                <td class="style3">
                    <asp:TextBox ID="txtRemarks" runat="server" TabIndex="10" TextMode="MultiLine" 
                        Width="300px" ontextchanged="txtRemarks_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </td>
            </tr>
            <tr>
                <td style="text-align: right" class="style10">
                    <b>Status</b></td>
                <td class="bold">
                    :</td>
                <td class="style3">
                    <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="11" Width="200px">
                        <asp:ListItem Value="A">ACTIVE</asp:ListItem>
                        <asp:ListItem Value="I">INACTIVE</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" class="style10">
                    &nbsp;</td>
                <td class="bold">
                    &nbsp;</td>
                <td class="style3">
                        <asp:Button ID="btnSave" runat="server" Font-Bold="True" Font-Italic="True" 
                            TabIndex="12" Text="Save" Width="100px" onclick="btnSave_Click" />
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        
<%--        <div id="dialog_close">
            
            <table style="width:100%;">
                <tr>
                    <td class="style4" colspan="2">
                        <strong>Are you Sure to</strong></td>
                </tr>
                <tr>
                    <td style="text-align: right" class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnSavePrint" runat="server" Font-Bold="True" 
                            Font-Italic="True" Text="Save &amp; Print" TabIndex="13" />
                    </td>
                </tr>
                <tr>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            
        </div>--%>

    </div>
</asp:Content>
