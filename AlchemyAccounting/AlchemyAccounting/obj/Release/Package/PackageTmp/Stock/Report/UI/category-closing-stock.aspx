<%@ Page Title="Category Wise Closing Stock" Language="C#" AutoEventWireup="true"
    MasterPageFile="~/Site.Master" CodeBehind="category-closing-stock.aspx.cs" Inherits="AlchemyAccounting.Stock.Report.UI.category_closing_stock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtDate").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+0" });
        });
    </script>
    <style type="text/css">
        #header
        {
            float: left;
            width: 100%;
            background-color: transparent;
            height: 50px;
        }
        #s_top
        {
            float: left;
            width: 60%;
            margin: 2% 20% 0% 20%;
            border: 1px solid #cccccc;
            border-radius: 10px;
        }
        .style1
        {
            width: 147px;
            text-align: right;
        }
        .style2
        {
            width: 2px;
            font-weight: bold;
        }
        .style3
        {
            width: 242px;
            text-align: left;
        }
        .autocomplete_completionListElement_grid
        {
            width: 250px !important;
            background-color: inherit;
            color: windowtext;
            border: buttonshadow;
            height: 200px;
            text-align: left;
            overflow: scroll;
            background: #fff;
            border: 1px solid #ccc;
            list-style-type: none;
        }
        
        .autocomplete_listItem_grid
        {
        }
        .autocomplete_highlightedListItem_grid
        {
            background: #000;
            color: Orange;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center" style="font-weight: bold; font-family: Century Gothic;">
            Category Wise Closing Stock & Value</h1>
    </div>
    <div>
        <table id="s_top">
            <tr>
                <td class="style1" style="text-align: right; font-weight: 700">
                    <asp:Label ID="lblCatID" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" style="text-align: right; font-weight: 700">
                    Category
                </td>
                <td class="style2">
                    :
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtCategoryNM" runat="server" TabIndex="1" Width="250px" 
                        OnTextChanged="txtCategoryNM_TextChanged" AutoPostBack="True"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtCategoryNM_AutoCompleteExtender" runat="server"
                        DelimiterCharacters="" Enabled="True" ServicePath="" TargetControlID="txtCategoryNM"
                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                        UseContextKey="True" ServiceMethod="GetCompletionList" CompletionListCssClass="autocomplete_completionListElement_grid">
                    </asp:AutoCompleteExtender>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <strong>Date</strong>
                </td>
                <td class="style2">
                    :
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtDate" runat="server" AutoPostBack="True" ClientIDMode="Static"
                        OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;
                </td>
                <td class="style2">
                    &nbsp;
                </td>
                <td style="text-align: right">
                    <asp:Button ID="btnSearch" runat="server" Font-Bold="True" Text="Search" Width="130px"
                        OnClick="btnSearch_Click" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;
                </td>
                <td class="style2">
                    &nbsp;
                </td>
                <td style="text-align: right">
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="lblErrMsg" runat="server" Font-Bold="False" ForeColor="#990000" 
                        Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
