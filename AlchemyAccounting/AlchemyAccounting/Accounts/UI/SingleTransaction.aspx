<%@ Page Title="Single Transaction Entry" Language="C#" MasterPageFile="~/Site.Master"
    CodeBehind="SingleTransaction.aspx.cs" Inherits="AlchemyAccounting.Accounts.UI.SingleTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Single Transaction</title>
<%--<script src="../../Scripts/custom.js" type="text/javascript"></script>--%>
    <link rel="shortcut icon" href="../../Images/favicon.ico" />
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
        .style5
        {
            width: 92px;
            text-align: right;
            font-family: Century Gothic;
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
        
        .style11
        {
            text-align: center;
            font-family: Century Gothic;
            width: 2px;
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
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center">
            Single Transaction Entry</h1>
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
                            <a onclick="return showInFrameDialog(this,'EditSingleVoucher.aspx','Edit Single Voucher');"
                                href="" style="font-family: Helvetica Neue,Lucida Grande,Segoe UI,Arial,Helvetica,Verdana,sans-serif;
                                font-size: 1.2em; font-weight: bold; font-style: italic; text-decoration: none;
                                color: #000000;">Edit</a></div>
                    </td>
                    <td class="style8">
                        <asp:Button ID="btnRefresh" runat="server" Font-Bold="True" Font-Italic="True" Text="Refresh"
                            OnClick="btnRefresh_Click" CssClass="txtColor" />
                    </td>
                    <td class="style9">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;<strong><asp:Label ID="lblErrMSg" runat="server" ForeColor="#CC0000" 
                            Text="lblErrMSg" Visible="False"></asp:Label>
                    </strong>
                    </td>
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
                    <strong>Transaction Type</strong>
                </td>
                <td class="style1">
                    <strong>:</strong>
                </td>
                <td>
                    <strong>
                        <asp:DropDownList ID="ddlTransType" runat="server" Width="150px" OnSelectedIndexChanged="ddlTransType_SelectedIndexChanged"
                            TabIndex="1" AutoPostBack="True" CssClass="txtColor">
                            <%--<asp:ListItem>Select</asp:ListItem>--%>
                            <asp:ListItem Value="MPAY">PAYMENT</asp:ListItem>
                            <asp:ListItem Value="MREC">RECEIPT</asp:ListItem>
                            <asp:ListItem Value="JOUR">JOURNAL</asp:ListItem>
                            <asp:ListItem Value="CONT">CONTRA</asp:ListItem>
                        </asp:DropDownList>
                    </strong>
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
                    <strong>Transaction Date</strong>
                </td>
                <td class="style1">
                    <strong>:</strong>
                </td>
                <td style="margin-left: 40px">
                    <asp:TextBox ID="txtTransDate" runat="server" TabIndex="2" ClientIDMode="Static"
                        OnTextChanged="txtTransDate_TextChanged" AutoPostBack="True" CssClass="txtColor"></asp:TextBox>
                    &nbsp;<asp:TextBox ID="txtTransYear" runat="server" TabIndex="3" ReadOnly="True"
                        CssClass="txtColor"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="style4">
                    <asp:Label ID="lblVCount" runat="server" Text="Label"></asp:Label>
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
                    Voucher No
                </td>
                <td class="style1">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtVouchNo" runat="server" TabIndex="4" ReadOnly="True" CssClass="txtColor"></asp:TextBox>
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
                <td class="style2">
                    Transaction For&nbsp;
                </td>
                <td class="style1">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlTransFor" runat="server" Width="150px" TabIndex="5" OnSelectedIndexChanged="ddlTransFor_SelectedIndexChanged"
                        CssClass="txtColor" AutoPostBack="True" Visible="False">
                        <%--<asp:ListItem>Select</asp:ListItem>--%>
                        <asp:ListItem>OTHERS</asp:ListItem>
                        <asp:ListItem>OFFICIAL</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtCostPool" runat="server" TabIndex="6" Width="180px" OnTextChanged="txtCostPool_TextChanged"
                        CssClass="txtColor" AutoPostBack="True"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtCostPool_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="" TargetControlID="txtCostPool" MinimumPrefixLength="1"
                        CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                        ServiceMethod="GetCompletionListCostPool">
                    </asp:AutoCompleteExtender>
                    <asp:Label ID="lblCostpoolID" runat="server" Visible="False"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="style4">
                    <%--<asp:ListItem>Select</asp:ListItem>--%>
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
                    Transaction Mode
                </td>
                <td class="style1">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlTransMode" runat="server" Width="150px" TabIndex="7" OnSelectedIndexChanged="ddlTransMode_SelectedIndexChanged"
                        CssClass="txtColor">
                        <%--<asp:ListItem>Select</asp:ListItem>--%>
                        <asp:ListItem>CASH</asp:ListItem>
                        <asp:ListItem>CASH CHEQUE</asp:ListItem>
                        <asp:ListItem>A/C PAYEE CHEQUE</asp:ListItem>
                        <asp:ListItem>ONLINE TRANSFER</asp:ListItem>
                        <asp:ListItem>PAY ORDER</asp:ListItem>
                        <asp:ListItem>ATM</asp:ListItem>
                        <asp:ListItem>D.D.</asp:ListItem>
                        <asp:ListItem>T.T.</asp:ListItem>
                        <asp:ListItem>OTHERS</asp:ListItem>
                    </asp:DropDownList>
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
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="style1">
                    :
                </td>
                <td>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" TargetControlID="txtCNDebitNm"
                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                        UseContextKey="True" ServiceMethod="GetCompletionListConD">
                    </asp:AutoCompleteExtender>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" TargetControlID="txtJRDebitNm"
                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                        UseContextKey="true" ServiceMethod="GetCompletionListJourD">
                    </asp:AutoCompleteExtender>
                    <asp:TextBox ID="txtCNDebitNm" runat="server" AutoPostBack="True" ClientIDMode="Static"
                        OnTextChanged="txtCNDebitNm_TextChanged" TabIndex="11" Width="300px" CssClass="txtColor"></asp:TextBox>
                    <asp:TextBox ID="txtJRDebitNm" runat="server" AutoPostBack="True" ClientIDMode="Static"
                        OnTextChanged="txtJRDebitNm_TextChanged" TabIndex="10" Width="300px" CssClass="txtColor"></asp:TextBox>
                    <asp:TextBox ID="txtMPDebitNM" runat="server" AutoPostBack="True" ClientIDMode="Static"
                        OnTextChanged="txtMPDebitNM_TextChanged" TabIndex="9" Width="300px" CssClass="txtColor"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtDebitNm"
                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                        UseContextKey="True" ServiceMethod="GetCompletionListMrecD">
                    </asp:AutoCompleteExtender>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtMPDebitNM"
                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                        UseContextKey="True" ServiceMethod="GetCompletionListMpayD">
                    </asp:AutoCompleteExtender>
                    <asp:TextBox ID="txtDebitNm" runat="server" AutoPostBack="True" ClientIDMode="Static"
                        OnTextChanged="txtDebitNm_TextChanged" TabIndex="8" Width="300px" CssClass="txtColor"></asp:TextBox>
                    <asp:TextBox ID="txtDebited" runat="server" Width="150px" TabIndex="12" ReadOnly="True"
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
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="style1">
                    :
                </td>
                <td>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" TargetControlID="txtMpCreditNm"
                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                        UseContextKey="True" ServiceMethod="GetCompletionListMpayC">
                    </asp:AutoCompleteExtender>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" TargetControlID="txtJRCreditNm"
                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                        UseContextKey="True" ServiceMethod="GetCompletionListJourC">
                    </asp:AutoCompleteExtender>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" TargetControlID="txtCNCreditNm"
                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                        UseContextKey="True" ServiceMethod="GetCompletionListConC">
                    </asp:AutoCompleteExtender>
                    <asp:TextBox ID="txtCNCreditNm" runat="server" AutoPostBack="True" ClientIDMode="Static"
                        OnTextChanged="txtCNCreditNm_TextChanged" TabIndex="16" Width="300px" CssClass="txtColor"></asp:TextBox>
                    <asp:TextBox ID="txtJRCreditNm" runat="server" AutoPostBack="True" ClientIDMode="Static"
                        OnTextChanged="txtJRCreditNm_TextChanged" TabIndex="15" Width="300px" CssClass="txtColor"></asp:TextBox>
                    <asp:TextBox ID="txtMpCreditNm" runat="server" AutoPostBack="True" ClientIDMode="Static"
                        OnTextChanged="txtMpCreditNm_TextChanged" TabIndex="14" Width="300px" CssClass="txtColor"></asp:TextBox>
                    <asp:TextBox ID="txtCreditNm" runat="server" AutoPostBack="True" ClientIDMode="Static"
                        OnTextChanged="txtCreditNm_TextChanged" TabIndex="13" Width="300px" CssClass="txtColor"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtCreditNm"
                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                        UseContextKey="True" ServiceMethod="GetCompletionListMrecC">
                    </asp:AutoCompleteExtender>
                    <asp:TextBox ID="txtCredited" runat="server" TabIndex="17" Width="150px" ReadOnly="True"
                        CssClass="txtColor"></asp:TextBox>
                    &nbsp;
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
                    Cheque No
                </td>
                <td class="style1">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtCheque" runat="server" TabIndex="18" OnTextChanged="txtCheque_TextChanged"
                        CssClass="txtColor"></asp:TextBox>
                    &nbsp; <strong>&nbsp;
                        <asp:Label ID="lblCheque" runat="server" ForeColor="#FF3300" Text="Label" Visible="False"></asp:Label>
                    </strong>
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="style5">
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
                <td style="text-align: right" class="style6">
                    <strong>Cheque Date</strong>
                </td>
                <td class="style11">
                    :
                </td>
                <td>
                    <strong>
                        <asp:TextBox ID="txtChequeDate" runat="server" TabIndex="19" ClientIDMode="Static"
                            OnTextChanged="txtChequeDate_TextChanged" CssClass="txtColor"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblChequeDT" runat="server" ForeColor="#FF3300" Text="Label" Visible="False"></asp:Label>
                    </strong>
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
                    Remarks
                </td>
                <td class="style1">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="386px" TabIndex="20"
                        OnTextChanged="txtRemarks_TextChanged" CssClass="txtColor"></asp:TextBox>
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
                    Amount
                </td>
                <td class="style1">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server" TabIndex="21" OnTextChanged="txtAmount_TextChanged"
                        ClientIDMode="Static" AutoPostBack="True" CssClass="txtColor"></asp:TextBox>
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
                    In Words
                </td>
                <td class="style1">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtInwords" runat="server" TextMode="MultiLine" Width="386px" onclick="shai_dailog()"
                        TabIndex="22" ReadOnly="True" OnTextChanged="txtInwords_TextChanged" CssClass="txtColor"></asp:TextBox>
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
                <td class="style6">
                    &nbsp;
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
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
        <div id="msg">
            <table style="width: 100%;">
                <tr>
                    <td colspan="3" style="text-align: center">
                        <strong style="text-align: center">Are you sure to </strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Button ID="btnSave" runat="server" Font-Bold="True" Font-Italic="True" Text="Save"
                            Width="117px" Style="text-align: center" TabIndex="23" OnClick="btnSave_Click"
                            CssClass="txtColor" />
                    </td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Font-Bold="True" Font-Italic="True" TabIndex="24"
                            Text="Save &amp; Print" Width="139px" OnClick="Button1_Click" OnClientClick=""
                            CssClass="txtColor" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
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
            </table>
        </div>
    </div>
    <%--<asp:ListItem>Select</asp:ListItem>--%>
</asp:Content>
