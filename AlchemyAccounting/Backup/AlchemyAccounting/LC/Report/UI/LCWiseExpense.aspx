<%@ Page Title = "L/C Wise Expenses" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LCWiseExpense.aspx.cs" Inherits="AlchemyAccounting.LC.Report.UI.LCWiseExpense" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <%--<script src="../../../Scripts/custom.js" type="text/javascript"></script>--%>
<link href="../../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
<script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
<script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>

<script type = "text/javascript">
    $(document).ready(function () {
//        $("#txtFrom,#txtTo").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+0" });
    });
</script>

<style type="text/css">
        #header
        {
            float: left;
            width:100%;
            background-color: transparent;
            height: 50px;
        }
        .style1
        {
            width: 2px;
        }
        .style2
        {
            width: 204px;
        }
        .style3
        {
        width: 231px;
        text-align: right;
        font-weight: 700;
    }

        #s_top
        {
            float:left;
            width:60%;
            margin: 2% 20% 0% 20%;
            border: 1px solid #cccccc;
            border-radius: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center" style="font-weight:bold;">L/c wise expenses</h1>
    </div>
    <div>
        <table id="s_top">
            <tr>
                <td class="style3">
                    <asp:Label ID="lblLCCD" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="style1">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    L/C ID</td>
                <td class="style1">
                    <strong>:</strong></td>
                <td>
                    <asp:TextBox ID="txtLCID" runat="server" AutoPostBack="True" 
                        ClientIDMode="Static" ontextchanged="txtLCID_TextChanged" TabIndex="2" 
                        Width="300px"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtLCID_AutoCompleteExtender" runat="server" 
                        CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" 
                        Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListLC" 
                        ServicePath="" UseContextKey="True" TargetControlID="txtLCID">
                    </asp:AutoCompleteExtender>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style1">
                    &nbsp;</td>
                <td style="text-align: right">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Font-Bold="True" 
                        Font-Italic="True" Width="150px" onclick="btnSearch_Click" TabIndex="4" />
                </td>
                <td>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style1">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
    
    </div>
</asp:Content>
