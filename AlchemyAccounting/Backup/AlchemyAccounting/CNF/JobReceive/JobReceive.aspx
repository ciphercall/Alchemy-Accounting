<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="JobReceive.aspx.cs" Inherits="AlchemyAccounting.CNF.JobReceive.JobReceive" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="shortcut icon" href="../../Images/favicon.ico" />
    <%--<script type="text/javascript" src="../../Scripts/jquery.blockUI.js"></script>--%>
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
            margin-top: 10px;
            margin-bottom: 30px;
            border-radius: 10px;
            text-align: left;
        }
        .Gridview
        {
            font-family: Verdana;
            font-size: 10pt;
            font-weight: normal;
            color: black;
            margin-right: 0px;
            text-align: left;
        }
        .txtColor:focus
        {
            border: solid 2px green !important;
        }
        .txtColor
        {
            margin-left: 0px;
            text-align: left;
            height: 22px;
        }
        .right
        {
            text-align: right;
        }
        .center
        {
            text-align: center;
        }
        .def
        {
            float: left;
            width: 100%;
        }
        #toolbar
        {
            float: left;
            width: 100%;
            background-color: #cccccc;
            border-radius: 10px 10px 0px 0px;
        }
        .ui-accordion
        {
            text-align: left;
        }
        .txtalign
        {
            text-align: center;
        }
        .passport
        {
            float: left;
            width: 100%;
            height: 250px;
        }
        .sign
        {
            float: left;
            width: 100%;
            height: 150px;
            margin-top: 10%;
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
        .completionList
        {
            width: 400px !important;
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 200px;
            overflow: auto;
            background-color: #FFFFFF;
        }
        
        .listItem
        {
            color: #1C1C1C;
        }
        
        .itemHighlighted
        {
            background-color: orange;
        }
        
        .well
        {
            background-color: white;
        }
        
        /*Calendar Control CSS*/
        .cal_Theme1 .ajax__calendar_other .ajax__calendar_day, .cal_Theme1 .ajax__calendar_other .ajax__calendar_year
        {
            color: White; /*Your background color of calender control*/
        }
    </style>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }

            return true;
        }
               
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center">
            Receive Against job (Only Bill) Information</h1>
    </div>
    <div id="entry">
        <div id="toolbar">
            <table style="width: 100%; border: 1px solid #000;">
                <tr>
                    <td style="text-align: right; width: 50%">
                        <asp:Button runat="server" ID="btnUpdate" CssClass="txtColor" Font-Bold="True" Font-Names="Calibri"
                            Style="text-align: center" Font-Size="15px" TabIndex="9" Text="Edit" Width="80px"
                            OnClick="btnUpdate_Click" Height="30px" />
                        <asp:Button runat="server" ID="btnCancel" CssClass="txtColor" Font-Bold="True" Font-Names="Calibri"
                            Style="text-align: center" Font-Size="15px" Text="Cancel" Width="80px" OnClick="btnCancel_Click"
                            Visible="false" Height="30px" />
                        <asp:Button runat="server" ID="btnDelete" CssClass="txtColor" Font-Bold="True" Font-Names="Calibri"
                            Style="text-align: center" Font-Size="15px" Text="Delete" Width="80px" OnClick="btnDelete_Click"
                            Visible="false" Height="30px" />
                    </td>
                    <td style="width: 50%">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="def">
        <table style="width: 100%">
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    Receive Date
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="width: 84%">
                    <asp:TextBox runat="server" ID="txtReceiveDate" CssClass="txtColor" Width="20%" TabIndex="1"
                        AutoPostBack="True" OnTextChanged="txtReceiveDate_TextChanged"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1 well"
                        Format="dd/MM/yyyy" TargetControlID="txtReceiveDate">
                    </asp:CalendarExtender>
                    <asp:TextBox runat="server" ID="txtTransMY" CssClass="txtColor center" Width="9%"
                        ReadOnly="true"></asp:TextBox>
                    <asp:DropDownList runat="server" ID="ddlTransMy" Visible="false" OnSelectedIndexChanged="ddlTransMy_SelectedIndexChanged"
                        AutoPostBack="True" CssClass="txtColor">
                    </asp:DropDownList>
                </td>
                <asp:Label runat="server" ID="lblMY" Visible="false"></asp:Label>
                <asp:Label runat="server" ID="lblSL" Visible="false"></asp:Label>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    voucher No
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="width: 84%">
                    <asp:TextBox runat="server" ID="txtVoucher" CssClass="txtColor center" Width="20%"
                        ReadOnly="true"></asp:TextBox>
                    <asp:DropDownList runat="server" ID="ddlVouchNo" Visible="false" OnSelectedIndexChanged="ddlVouchNo_SelectedIndexChanged"
                        AutoPostBack="True" CssClass="txtColor">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    Receive Type
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="width: 84%">
                    <asp:DropDownList runat="server" ID="ddlRcvType" CssClass="txtColor" Width="20%"
                        TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="ddlRcvType_SelectedIndexChanged">
                        <asp:ListItem Value="Advance">Advance</asp:ListItem>
                        <asp:ListItem Value="Normal">Normal</asp:ListItem>
                        <asp:ListItem>Discount</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lbltransfor" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    Company ID
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="width: 84%">
                    <asp:TextBox runat="server" ID="txtCompanyNM" CssClass="txtColor" Width="40%" ReadOnly="true"></asp:TextBox>
                    <asp:TextBox runat="server" ID="txtCompanyID" CssClass="txtColor" Width="20%" ReadOnly="true"
                        Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    Job No & Year
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="width: 84%">
                    <asp:TextBox runat="server" ID="txtJobID" CssClass="txtColor center" Width="11%"
                        TabIndex="3" OnTextChanged="txtJobID_TextChanged" AutoPostBack="True"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" Enabled="True" ServicePath=""
                        TargetControlID="txtJobID" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true"
                        CompletionSetCount="3" UseContextKey="True" ServiceMethod="GetCompletionListJob_No_Year_Type"
                        runat="server">
                    </asp:AutoCompleteExtender>
                    <asp:TextBox runat="server" ID="txtJobYear" CssClass="txtColor center" Width="12%"
                        ReadOnly="true"></asp:TextBox>
                    <asp:TextBox runat="server" ID="txtJobType" CssClass="txtColor center" Width="15%"
                        ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    Party ID
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="width: 84%">
                    <asp:TextBox runat="server" ID="txtPartyNM" CssClass="txtColor" Width="40%" ReadOnly="true"></asp:TextBox>
                    <asp:TextBox runat="server" ID="txtPartyID" CssClass="txtColor" Width="20%" ReadOnly="true"
                        Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    Cash/Bank ID
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="width: 84%">
                    <asp:TextBox runat="server" ID="txtCashBankNM" CssClass="txtColor" Width="40%" TabIndex="4"
                        OnTextChanged="txtCashBankNM_TextChanged" AutoPostBack="True"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" Enabled="True" ServicePath=""
                        TargetControlID="txtCashBankNM" MinimumPrefixLength="1" CompletionInterval="10"
                        EnableCaching="true" CompletionSetCount="3" UseContextKey="True" ServiceMethod="GetCompletionListCash_Bank"
                        runat="server">
                    </asp:AutoCompleteExtender>
                    <asp:TextBox runat="server" ID="txtCashBankID" CssClass="txtColor" Width="20%" ReadOnly="true"
                        Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    Remarks
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="width: 84%">
                    <asp:TextBox runat="server" ID="txtRemarks" CssClass="txtColor" Width="40%" TabIndex="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    Amount
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="width: 84%">
                    <asp:TextBox runat="server" ID="txtAmount"
                        CssClass="txtColor" Width="40%" TabIndex="6" AutoPostBack="True" OnTextChanged="txtAmount_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    In Words
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="width: 84%">
                    <asp:TextBox runat="server" ID="txtInwords" onkeypress="return isNumberKey(event)"
                        CssClass="txtColor" Width="40%" TabIndex="7"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    &nbsp;
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    &nbsp;
                </td>
                <td style="width: 84%">
                    <asp:Button runat="server" ID="btnSave" CssClass="txtColor right" Font-Bold="True"
                        Font-Names="Calibri" Style="text-align: center" Font-Size="15px" TabIndex="8"
                        Text="Save" Width="80px" OnClick="btnSave_Click" Height="30px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnSave_Print" CssClass="txtColor right" Font-Bold="True"
                        Font-Names="Calibri" Style="text-align: center" Font-Size="15px" TabIndex="9"
                        Text="Print" Width="80px" Height="30px" OnClick="btnSave_Print_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" ID="lblErrmsg" Visible="False" ForeColor="#990000" Style="font-weight: 700"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    &nbsp;
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    &nbsp;
                </td>
                <td style="width: 84%">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
