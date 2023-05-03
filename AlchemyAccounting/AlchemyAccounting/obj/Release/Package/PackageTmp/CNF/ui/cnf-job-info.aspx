<%@ Page Title="CNF Job Informaiton" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="cnf-job-info.aspx.cs" Inherits="AlchemyAccounting.CNF.ui.cnf_job_info" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="shortcut icon" href="../../Images/favicon.ico" />
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
    <style type="text/css">
        #header {
            float: left;
            width: 100%;
            background-color: transparent;
            height: 50px;
        }

            #header h1 {
                font-family: Century Gothic;
                font-weight: bold;
            }

        #entry {
            float: left;
            width: 100%;
            background-color: transparent;
            border: 1px solid #000;
            margin-top: 10px;
            margin-bottom: 30px;
            border-radius: 10px;
            font-family: Calibri;
            font-size: 14px;
        }

        #grid {
            float: left;
            width: 100%;
        }

        #toolbar {
            float: left;
            width: 100%;
            background-color: #cccccc;
            border-radius: 10px 10px 0px 0px;
        }

        .style8 {
            width: 4px;
        }

        .txtColor:focus {
            border: solid 4px green !important;
        }

        .txtColor {
            margin-left: 0px;
            text-align: left;
            height: 22px;
        }

        .txtaligncenter {
            text-align: center;
        }

        .txtalignright {
            text-align: right;
        }

        .style9 {
            width: 667px;
        }

        .style10 {
            width: 635px;
        }

        .ui-accordion {
            text-align: left;
        }

        .fontfiltering {
            font-family: Calibri;
            font-size: 10px;
            list-style: none;
            margin-left: -40px;
        }

        .cont {
            float: left;
            width: 100%;
        }

        .style11 {
            width: 31%;
            height: 26px;
        }

        .style12 {
            width: 1%;
            height: 26px;
        }

        .style13 {
            width: 69%;
            height: 26px;
        }

        .style14 {
            width: 31%;
        }

        .style15 {
        }

        .completionList {
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 150px;
            overflow: auto;
            background-color: #FFFFFF;
        }

        .listItem {
            color: #1C1C1C;
            list-style: none;
        }

        .itemHighlighted {
            background-color: orange;
            list-style: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="upd1">
        <ContentTemplate>

            <div id="header">
                <h1 align="center">Job Information</h1>
            </div>
            <div id="entry">
                <div id="toolbar">
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: right" class="style10">
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <asp:Label ID="lblCompADD" runat="server" Visible="False"></asp:Label>
                                <asp:Button ID="btnEdit" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"
                                    Text="Edit" Width="80px" TabIndex="50" CssClass="txtColor txtaligncenter"
                                    OnClick="btnEdit_Click" Height="30px" />
                            </td>
                            <td class="style8">&nbsp;
                            </td>
                            <td class="style9">
                                <asp:Button ID="btnRefresh" runat="server" Font-Bold="True" Font-Names="Calibri"
                                    Font-Size="15px" Text="Refresh" Width="80px" TabIndex="51" CssClass="txtColor txtaligncenter"
                                    OnClick="btnRefresh_Click" Height="30px" />
                                <asp:Label ID="lblJobNo" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblJobTP" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblJobReg" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblError" runat="server" ForeColor="#CC0000" Visible="False"
                                    Style="font-weight: 700"></asp:Label>
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="cont">
                    <table style="width: 100%; font-family: Calibri; font-size: 14px; font-weight: bold">
                        <tr>
                            <td style="text-align: right" class="style14">Company
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td style="width: 69%" colspan="3">
                                <asp:TextBox ID="txtCompNM" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    OnTextChanged="txtCompNM_TextChanged" TabIndex="1" Width="91%"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServicePath="" TargetControlID="txtCompNM" MinimumPrefixLength="1"
                                    CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                                    ServiceMethod="GetCompletionListCompanyName" CompletionListCssClass="completionList"
                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                </asp:AutoCompleteExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">Creation Date &amp; No
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td style="width: 69%" colspan="3">
                                <asp:DropDownList ID="ddlJobTp" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    OnSelectedIndexChanged="ddlJobTp_SelectedIndexChanged" TabIndex="2"
                                    Width="15%" Height="28px">
                                    <asp:ListItem>EXPORT</asp:ListItem>
                                    <asp:ListItem>IMPORT</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtCrDt" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    OnTextChanged="txtCrDt_TextChanged" TabIndex="3" Width="13%"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp; Job Year :
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCrDt"
                            PopupButtonID="txtCrDt" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                                <asp:TextBox ID="txtJobYear" runat="server" Width="10%" CssClass="txtColor txtaligncenter"
                                    TabIndex="4" AutoPostBack="True" OnTextChanged="txtJobYear_TextChanged"></asp:TextBox>
                                <asp:TextBox ID="txtNo" runat="server" ReadOnly="True" CssClass="txtaligncenter"
                                    Width="9%" Height="23px"></asp:TextBox>
                                <asp:DropDownList ID="ddlJobNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged"
                                    TabIndex="4" Visible="False" Width="15%" CssClass="txtColor" Height="28px">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtCompID" runat="server" ReadOnly="True" Width="10%" Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style11">Register ID
                            </td>
                            <td class="style12">:
                            </td>
                            <td class="style13" colspan="3">
                                <asp:DropDownList ID="ddlRegID" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    OnSelectedIndexChanged="ddlRegID_SelectedIndexChanged" TabIndex="5" Width="42%">
                                    <asp:ListItem>CHITTAGONG</asp:ListItem>
                                    <asp:ListItem>COMILLA</asp:ListItem>
                                    <asp:ListItem>BENAPOLE</asp:ListItem>
                                    <asp:ListItem>DEPZ</asp:ListItem>
                                    <asp:ListItem>ICD</asp:ListItem>
                                    <asp:ListItem>AEPZ</asp:ListItem>
                                    <asp:ListItem>AIRPORT</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtPartyID" runat="server" ReadOnly="True" Width="19%" Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">Party Name<span style="color: red; font-size: 14px; font-weight: 800;">*</span>
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td style="width: 69%" colspan="3">
                                <asp:TextBox ID="txtPartyNM" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    OnTextChanged="txtPartyNM_TextChanged" TabIndex="6" Width="91%"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServicePath="" TargetControlID="txtPartyNM" MinimumPrefixLength="1"
                                    CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                                    ServiceMethod="GetCompletionListParty" CompletionListCssClass="completionList"
                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                </asp:AutoCompleteExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">Consignee Name &amp; Address<span style="color: red; font-size: 14px; font-weight: 800;">*</span>
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td style="width: 69%" colspan="3">
                                <asp:TextBox ID="txtConsigneeNM" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    OnTextChanged="txtConsigneeNM_TextChanged" TabIndex="7" Width="45%"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServicePath="" TargetControlID="txtConsigneeNM" MinimumPrefixLength="1"
                                    CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                                    ServiceMethod="GetCompletionListConsigName" CompletionListCssClass="completionList"
                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                </asp:AutoCompleteExtender>
                                <asp:TextBox ID="txtConsigneeAdd" runat="server" CssClass="txtColor" TabIndex="8"
                                    Width="45%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">Supplier Name
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td class="style15" colspan="3">
                                <asp:TextBox ID="txtSuppNM" runat="server" CssClass="txtColor" TabIndex="9" Width="91%"
                                    AutoPostBack="True" OnTextChanged="txtSuppNM_TextChanged"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServicePath="" TargetControlID="txtSuppNM" MinimumPrefixLength="1"
                                    CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                                    ServiceMethod="GetCompletionListSupplier" CompletionListCssClass="completionList"
                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                </asp:AutoCompleteExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">Goods Desc<span style="color: red; font-size: 14px; font-weight: 800;">*</span>
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td class="style15">
                                <asp:TextBox ID="txtGoodsDesc" runat="server" CssClass="txtColor" TabIndex="10" Width="80%"></asp:TextBox>
                            </td>
                            <td style="width: 35%" colspan="2">Packages Details<span style="color: red; font-size: 14px; font-weight: 800;">*</span> :
                        <asp:TextBox ID="txtPkgDet" runat="server" CssClass="txtColor" TabIndex="11" Width="52%"></asp:TextBox>
                            </td>
                        </tr>
                        <%--                <tr>
                    <td style="text-align: right" class="style14">
                        Container No
                    </td>
                    <td style="width: 1%">
                        :
                    </td>
                    <td class="style15" colspan="3">
                        
                    </td>
                </tr>--%>
                        <tr>
                            <td style="text-align: right" class="style14">C&amp;F Value (USD/EUR)<span style="color: red; font-size: 14px; font-weight: 800;">*</span>
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtCNFVUSD" runat="server" CssClass="txtColor txtalignright" TabIndex="13"
                                    Width="50%" AutoPostBack="True" OnTextChanged="txtCNFVUSD_TextChanged"></asp:TextBox>
                            </td>
                            <td style="width: 15%; text-align: right">CRF Value (USD) :
                            </td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtCRFVUSD" runat="server" CssClass="txtColor txtalignright" TabIndex="14"
                                    Width="50%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">Exchange Rate &amp; Type&nbsp;<span style="color: red; font-size: 14px; font-weight: 800;">*</span>
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtChangeRT" runat="server" CssClass="txtColor txtalignright" TabIndex="15"
                                    Width="50%" AutoPostBack="True" OnTextChanged="txtChangeRT_TextChanged"></asp:TextBox>
                                <asp:TextBox ID="txtExTP" runat="server" CssClass="txtColor" TabIndex="16" Width="20%"
                                    AutoPostBack="True" OnTextChanged="txtExTP_TextChanged"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txtExTP_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServicePath="" TargetControlID="txtExTP" MinimumPrefixLength="1"
                                    CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                                    ServiceMethod="GetCompletionListExchangeTp" CompletionListCssClass="completionList"
                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td style="width: 15%; text-align: right">C&amp;F Value (BDT) :
                            </td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtCNFVBDT" runat="server" CssClass="txtColor txtalignright" TabIndex="17"
                                    Width="50%" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">Rot No
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtRotno" runat="server" CssClass="txtColor txtalignright"
                                    TabIndex="17" Width="50%"></asp:TextBox>
                                &nbsp;
                            </td>
                            <td style="width: 15%; text-align: right">Vessel :
                            </td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtVessel" runat="server" CssClass="txtColor txtalignright" TabIndex="17"
                                    Width="50%"></asp:TextBox>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">Gross Weight<span style="color: red; font-size: 14px; font-weight: 800;">*</span>
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtGrossWeight" runat="server" CssClass="txtColor txtalignright" TabIndex="17"
                                    Width="50%"></asp:TextBox>&nbsp;
                            </td>
                            <td style="width: 15%; text-align: right">Net Weight<span style="color: red; font-size: 14px; font-weight: 800;">*</span> :
                            </td>
                            <td style="width: 29%">

                                <asp:TextBox ID="txtNetWeight" runat="server" CssClass="txtColor txtalignright" TabIndex="17"
                                    Width="50%"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">
                                <asp:GridView ID="GridView1" runat="server">
                                </asp:GridView>
                                Assessable Value
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtAssessableVal" runat="server" CssClass="txtColor txtalignright"
                                    TabIndex="18" Width="50%" AutoPostBack="True"
                                    OnTextChanged="txtAssessableVal_TextChanged"></asp:TextBox>
                                &nbsp;<%--Comm(%) :<asp:TextBox ID="txtComPer" runat="server" Width="15%" CssClass="txtColor txtaligncenter"
                            TabIndex="25">0</asp:TextBox>--%>
                            </td>
                            <td style="width: 15%; text-align: right">Commission :
                            </td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtCommission" runat="server" CssClass="txtColor txtalignright"
                                    TabIndex="19" Width="50%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">CRF No
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtCRFNO" runat="server" CssClass="txtColor" TabIndex="20" Width="50%"></asp:TextBox>
                            </td>
                            <td style="width: 15%; text-align: right">CRF Date :
                            </td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtCRFDT" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    OnTextChanged="txtCRFDT_TextChanged" TabIndex="21" Width="30%"></asp:TextBox>
                                <asp:CalendarExtender ID="txtCRFDT_CalendarExtender" runat="server" TargetControlID="txtCRFDT"
                                    PopupButtonID="txtCRFDT" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">Invoice No<span style="color: red; font-size: 14px; font-weight: 800;">*</span>
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="txtColor" TabIndex="22"
                                    Width="50%" AutoPostBack="True" OnTextChanged="txtInvoiceNo_TextChanged"></asp:TextBox>
                            </td>
                            <td style="width: 15%; text-align: right">Invoice Date<span style="color: red; font-size: 14px; font-weight: 800;">*</span> :
                            </td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtInvoiceDT" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    OnTextChanged="txtInvoiceDT_TextChanged" TabIndex="23" Width="30%"></asp:TextBox>
                                <asp:CalendarExtender ID="txtInvoiceDT_CalendarExtender" runat="server" TargetControlID="txtInvoiceDT"
                                    PopupButtonID="txtInvoiceDT" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">
                                <asp:TextBox ID="txtWhDT" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    Visible="false" OnTextChanged="txtWhDT_TextChanged" TabIndex="21" Width="35%"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtWhDT"
                                    PopupButtonID="txtWhDT" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                                B/E No<span style="color: red; font-size: 14px; font-weight: 800;">*</span>
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtBENO" runat="server" CssClass="txtColor" TabIndex="24"
                                    Width="50%" AutoPostBack="True" OnTextChanged="txtBENO_TextChanged"></asp:TextBox>
                            </td>
                            <td style="width: 15%; text-align: right">B/E Date<span style="color: red; font-size: 14px; font-weight: 800;">*</span> :
                            </td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtBEDT" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    OnTextChanged="txtBEDT_TextChanged" TabIndex="25" Width="30%"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtBEDT"
                                    PopupButtonID="txtBEDT" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                                <asp:TextBox ID="txtDelDT" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    Visible="false" OnTextChanged="txtDelDT_TextChanged" TabIndex="22" Width="30%"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtDelDT"
                                    PopupButtonID="txtDelDT" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <%--                <tr>
                    <td style="text-align: right" class="style14">
                        Wharfent Date
                    </td>
                    <td style="width: 1%">
                        :
                    </td>
                    <td style="width: 25%">
                        &nbsp;</td>
                    <td style="width: 15%; text-align: right">
                        Delivery Date:
                    </td>
                    <td style="width: 29%">
                        &nbsp;</td>
                </tr>--%>
                        <tr>
                            <td style="text-align: right" class="style14">B/L No
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtBLNO" runat="server" CssClass="txtColor" TabIndex="26" Width="50%"></asp:TextBox>
                            </td>
                            <td style="width: 15%; text-align: right">B/L Date:
                            </td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtBLDT" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    OnTextChanged="txtBLDT_TextChanged" TabIndex="27" Width="30%"></asp:TextBox>
                                <asp:CalendarExtender ID="txtBLDT_CalendarExtender" runat="server" TargetControlID="txtBLDT"
                                    PopupButtonID="txtBLDT" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">L/C No<span style="color: red; font-size: 14px; font-weight: 800;">*</span>
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtLCNO" runat="server" CssClass="txtColor" TabIndex="28" Width="50%"></asp:TextBox>
                            </td>
                            <td style="width: 15%; text-align: right">L/C Date<span style="color: red; font-size: 14px; font-weight: 800;">*</span> :
                            </td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtLCDT" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    OnTextChanged="txtLCDT_TextChanged" TabIndex="29" Width="30%"></asp:TextBox>
                                <asp:CalendarExtender ID="txtLCDT_CalendarExtender" runat="server" TargetControlID="txtLCDT"
                                    PopupButtonID="txtLCDT" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">Permit No
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtPermitNO" runat="server" CssClass="txtColor" TabIndex="30"
                                    Width="50%" AutoPostBack="True" OnTextChanged="txtPermitNO_TextChanged"></asp:TextBox>
                            </td>
                            <td style="width: 15%; text-align: right">Permit Date :
                            </td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtPermitDT" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    OnTextChanged="txtPermitDT_TextChanged" TabIndex="31" Width="30%"></asp:TextBox>
                                <asp:CalendarExtender ID="txtPermitDT_CalendarExtender" runat="server" TargetControlID="txtPermitDT"
                                    PopupButtonID="txtPermitDT" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">AWB No</td>
                            <td style="width: 1%">:</td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtAwbNo" runat="server" CssClass="txtColor" TabIndex="32"
                                    Width="50%"></asp:TextBox>
                            </td>
                            <td style="width: 15%; text-align: right">AWB Date :</td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtAwbDT" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    OnTextChanged="txtAwbDT_TextChanged" TabIndex="33" Width="30%"></asp:TextBox>
                                <asp:CalendarExtender ID="txtAwbDT_CalendarExtender" runat="server" TargetControlID="txtAwbDT"
                                    PopupButtonID="txtAwbDT" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">H/BL No</td>
                            <td style="width: 1%">:</td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtHBlNo" runat="server" CssClass="txtColor" TabIndex="34"
                                    Width="50%"></asp:TextBox>
                            </td>
                            <td style="width: 15%; text-align: right">H/BL Date :
                            </td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtHblDT" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    OnTextChanged="txtHblDT_TextChanged" TabIndex="35" Width="30%"></asp:TextBox>
                                <asp:CalendarExtender ID="txtHblDT_CalendarExtender" runat="server" TargetControlID="txtHblDT"
                                    PopupButtonID="txtHblDT" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">HAWB No</td>
                            <td style="width: 1%">:</td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtHawbNo" runat="server" CssClass="txtColor" TabIndex="36"
                                    Width="50%"></asp:TextBox>
                            </td>
                            <td style="width: 15%; text-align: right">HAWB Date :</td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtHawbDT" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    OnTextChanged="txtHawbDT_TextChanged" TabIndex="37" Width="30%"></asp:TextBox>
                                <asp:CalendarExtender ID="txtHawbDT_CalendarExtender" runat="server" TargetControlID="txtHawbDT"
                                    PopupButtonID="txtHawbDT" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">Undertaking No</td>
                            <td style="width: 1%">:</td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtUnderTakeNo" runat="server" CssClass="txtColor" TabIndex="38"
                                    Width="50%"></asp:TextBox>
                            </td>
                            <td style="width: 15%; text-align: right">Under Taking Date :</td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtUnderTakeDt" runat="server" CssClass="txtColor" TabIndex="39" Width="30%"></asp:TextBox>
                                <asp:CalendarExtender ID="txtUnderTakeDt_CalendarExtender" runat="server" TargetControlID="txtUnderTakeDt"
                                    PopupButtonID="txtUnderTakeDt" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">ETA Date</td>
                            <td style="width: 1%">:</td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtetaDate" runat="server" CssClass="txtColor" TabIndex="40"
                                    Width="50%" MaxLength="100"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtetaDate"
                                    PopupButtonID="txtetaDate" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </td>
                            <td style="width: 15%; text-align: right">ETB Date :</td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtetbDate" runat="server" CssClass="txtColor" TabIndex="41" Width="30%"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtetbDate"
                                    PopupButtonID="txtetbDate" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">UD NO<span style="color: red; font-size: 14px; font-weight: 800;">*</span></td>
                            <td style="width: 1%">:</td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtUdNo" runat="server" CssClass="txtColor" TabIndex="42"
                                    Width="50%" MaxLength="100"></asp:TextBox>
                            </td>
                            <td style="width: 15%; text-align: right">UD Date<span style="color: red; font-size: 14px; font-weight: 800;">*</span> :</td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtUdDate" runat="server" CssClass="txtColor" TabIndex="43" Width="30%"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtUdDate"
                                    PopupButtonID="txtUdDate" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">Exp. Form NO<span style="color: red; font-size: 14px; font-weight: 800;">*</span></td>
                            <td style="width: 1%">:</td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtExpFNo" runat="server" CssClass="txtColor" TabIndex="44"
                                    Width="50%"></asp:TextBox>
                            </td>
                            <td style="width: 15%; text-align: right">Exp. Form Date<span style="color: red; font-size: 14px; font-weight: 800;">*</span> :</td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtExpFDate" runat="server" CssClass="txtColor" TabIndex="45" Width="30%"
                                    AutoPostBack="True" OnTextChanged="txtUnderTakeDt_TextChanged"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtExpFDate"
                                    PopupButtonID="txtExpFDate" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">Container No</td>
                            <td style="width: 1%">:</td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtContainerNo" runat="server" CssClass="txtColor" TabIndex="46"
                                    Width="50%"></asp:TextBox>
                            </td>
                            <td style="width: 15%; text-align: right">&nbsp;</td>
                            <td style="width: 29%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">Remarks<span style="color: red; font-size: 14px; font-weight: 800;">*</span></td>
                            <td style="width: 1%">:</td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtComRemarks" runat="server" CssClass="txtColor" TabIndex="47"
                                    Width="100%"></asp:TextBox>
                            </td>
                            <td style="width: 15%; text-align: right">&nbsp;</td>
                            <td style="width: 29%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">Status
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td style="width: 25%">
                                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" TabIndex="48"
                                    Width="30%">
                                    <asp:ListItem Value="A">ACTIVE</asp:ListItem>
                                    <asp:ListItem Value="I">INACTIVE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 15%; text-align: right">&nbsp;
                            </td>
                            <td style="width: 29%">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">&nbsp;
                            </td>
                            <td style="width: 1%">&nbsp;
                            </td>
                            <td style="width: 25%">
                                <asp:Button ID="btnSave" runat="server" CssClass="txtColor txtaligncenter" Font-Bold="True"
                                    TabIndex="49" Text="Save" Width="50%" OnClick="btnSave_Click"
                                    Height="30px" />
                            </td>
                            <td style="width: 15%; text-align: right">&nbsp;
                            </td>
                            <td style="width: 29%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style14">&nbsp;
                            </td>
                            <td style="width: 1%">&nbsp;
                            </td>
                            <td style="width: 25%">&nbsp;
                            </td>
                            <td style="width: 15%; text-align: right">&nbsp;
                            </td>
                            <td style="width: 29%">&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
