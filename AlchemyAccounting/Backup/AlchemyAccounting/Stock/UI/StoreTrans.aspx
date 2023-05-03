<%@ Page Title="Store Transaction" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"
    CodeBehind="StoreTrans.aspx.cs" Inherits="AlchemyAccounting.Stock.UI.StoreTrans" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad() {
            $("#txtInDT,#txtLCDT,#txtPInDT,#txtPLCDT,#txtInDT_Trans,#txtTInDt_Ret").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+0" });
        }

        function confMSG() {
            if (confirm("Are you Sure to Delete?")) {
                //                alert("Clicked Yes");
            }
            else {
                //                alert("Clicked No");
                return false;
            }

        }

    </script>
    <script language="javascript" type="text/javascript">
        Sys.Application.add_load
        (
            function () {
                window.setTimeout(focus, 1);
            }
        )
        function focus() {
            document.getElementById('<%=txtSLMNo.ClientID %>').focus();
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
            margin-bottom: 5px;
        }
        #toolbar
        {
            float: left;
            width: 100%;
            background-color: #cccccc;
            border-radius: 10px 10px 0px 0px;
        }
        
        #header_purchase
        {
            float: left;
            width: 100%;
            background-color: transparent;
            height: 50px;
        }
        #header_purchase h1
        {
            font-family: Century Gothic;
            font-weight: bold;
        }
        #entry_purchase
        {
            float: left;
            width: 100%;
            background-color: transparent;
            border: 1px solid #000;
            border-radius: 10px;
            margin-top: 10px;
            margin-bottom: 5px;
        }
        #toolbar_purchase
        {
            float: left;
            width: 100%;
            background-color: #cccccc;
            border-radius: 10px 10px 0px 0px;
        }
        
        #header_transfer
        {
            float: left;
            width: 100%;
            background-color: transparent;
            height: 50px;
        }
        #header_transfer h1
        {
            font-family: Century Gothic;
            font-weight: bold;
        }
        #entry_transfer
        {
            float: left;
            width: 100%;
            background-color: transparent;
            border: 1px solid #000;
            border-radius: 10px;
            margin-top: 10px;
            margin-bottom: 5px;
        }
        #toolbar_ret
        {
            float: left;
            width: 100%;
            background-color: #cccccc;
            border-radius: 10px 10px 0px 0px;
        }
        
        #header_transfer
        {
            float: left;
            width: 100%;
            background-color: transparent;
            height: 50px;
        }
        #header_ret h1
        {
            font-family: Century Gothic;
            font-weight: bold;
        }
        #entry_ret
        {
            float: left;
            width: 100%;
            background-color: transparent;
            border: 1px solid #000;
            border-radius: 10px;
            margin-top: 10px;
            margin-bottom: 5px;
        }
        #toolbar_ret
        {
            float: left;
            width: 100%;
            background-color: #cccccc;
            border-radius: 10px 10px 0px 0px;
        }
        
        #dialog
        {
            float: left;
            width: 30%;
        }
        .Gridview
        {
            font-family: Verdana;
            font-size: 10pt;
            font-weight: normal;
            color: black;
        }
        .txtColor:focus
        {
            border: solid 1px green !important;
        }
        .txtColor
        {
            margin-left: 0px;
        }
        .style13
        {
            width: 145px;
            text-align: right;
        }
        .style2
        {
            width: 1px;
        }
        .style27
        {
            width: 63px;
        }
        .style5
        {
            width: 120px;
            text-align: right;
        }
        .style6
        {
            width: 2px;
        }
        .style17
        {
            width: 290px;
        }
        .style12
        {
            width: 51px;
        }
        .style15
        {
            width: 4px;
        }
        .style28
        {
            width: 411px;
            text-align: left;
        }
        .style30
        {
            width: 69px;
            text-align: right;
        }
        .style31
        {
            width: 3px;
        }
        .style32
        {
            width: 76px;
        }
        .style33
        {
            width: 28px;
        }
        #grid_sale
        {
            float: left;
            width: 100%;
        }
        .style34
        {
            width: 478px;
        }
        .style35
        {
            width: 142px;
            text-align: right;
        }
        .style36
        {
            width: 133px;
        }
        .style37
        {
            width: 85px;
        }
        .style38
        {
            width: 6px;
            text-align: right;
        }
        .style44
        {
            width: 92px;
        }
        .style45
        {
            width: 29px;
        }
        .style48
        {
            width: 5px;
        }
        .style49
        {
            width: 139px;
            text-align: right;
        }
        .style52
        {
            width: 158px;
        }
        .style53
        {
            width: 468px;
        }
        .style56
        {
            width: 144px;
            text-align: right;
        }
        .style57
        {
            width: 143px;
            text-align: right;
        }
        .style61
        {
            width: 165px;
        }
        .style64
        {
            width: 141px;
            text-align: right;
        }
        .style65
        {
            width: 64px;
            text-align: left;
        }
        .style66
        {
            text-align: left;
        }
        .style67
        {
            width: 32px;
        }
        .style68
        {
            width: 106px;
        }
        .style70
        {
            width: 147px;
            text-align: right;
        }
        .style71
        {
            width: 193px;
            text-align: right;
        }
        .style72
        {
            width: 176px;
        }
        .style73
        {
            width: 75px;
        }
        .style74
        {
            width: 142px;
        }
        .style76
        {
            width: 160px;
        }
        .style77
        {
            width: 141px;
        }
        .style78
        {
            width: 476px;
        }
        .style79
        {
            width: 108px;
            text-align: right;
        }
        .style86
        {
            width: 165px;
            text-align: right;
        }
        .style88
        {
            width: 50px;
        }
        .style89
        {
            width: 68px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="Up_TabContaner" runat="server">
            <ContentTemplate>
                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnDemand="true"
                    AutoPostBack="false" TabStripPlacement="Top" Font-Bold="True" Font-Italic="False"
                    Font-Size="12pt" Font-Underline="False" ForeColor="Black" TabIndex="100" Width="100%">
                    <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="SALE INFORMATION" Enabled="true"
                        OnDemandMode="Once">
                        <HeaderTemplate>
                            SALE INFORMATION</HeaderTemplate>
                        <ContentTemplate>
                            <div id="header">
                                <h1 align="center" style="font-style: normal">
                                    SALE INFORMATION</h1>
                            </div>
                            <div id="entry">
                                <div id="toolbar">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="style34" style="text-align: right">
                                                <asp:Button ID="btnSaleEdit" runat="server" Font-Bold="True" OnClick="btnSaleEdit_Click"
                                                    Text="EDIT" Width="70px" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style34">
                                                <asp:Label ID="lblItemID" runat="server"></asp:Label><asp:Label ID="lblSMY" runat="server"
                                                    Visible="False"></asp:Label><asp:Label ID="lblSMxNo" runat="server" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSmsgComTrans" runat="server" ForeColor="#990000" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table style="width: 100%; font-style: normal;">
                                    <tr>
                                        <td class="style13" style="font-style: normal">
                                            Invoice Date &amp; No
                                        </td>
                                        <td class="style2" style="font-style: normal">
                                            :
                                        </td>
                                        <td class="style73">
                                            <asp:TextBox ID="txtInDT" runat="server" AutoPostBack="True" ClientIDMode="Static"
                                                CssClass="txtColor" OnTextChanged="txtInDT_TextChanged" TabIndex="1" Width="84px"></asp:TextBox>
                                        </td>
                                        <td class="style76">
                                            <asp:TextBox ID="txtInNo" runat="server" CssClass="txtColor" ReadOnly="True" TabIndex="2"
                                                Width="80px"></asp:TextBox><asp:DropDownList ID="ddlSalesEditInNo" runat="server"
                                                    OnSelectedIndexChanged="ddlSalesEditInNo_SelectedIndexChanged" TabIndex="2" AutoPostBack="True"
                                                    CssClass="txtColor" Visible="False" Width="80px">
                                                </asp:DropDownList>
                                        </td>
                                        <td class="style5">
                                            Sales Memo No
                                        </td>
                                        <td class="style6">
                                            :
                                        </td>
                                        <td class="style45">
                                            <asp:TextBox ID="txtSLMNo" runat="server" CssClass="txtColor" TabIndex="3" Width="135px"></asp:TextBox>
                                        </td>
                                        <td class="style79">
                                            Total Amount
                                        </td>
                                        <td class="style48">
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTotAmt" runat="server" ReadOnly="True" TabIndex="35">0.00</asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%; font-style: normal;">
                                    <tr>
                                        <td class="style35">
                                            Party ID
                                        </td>
                                        <td class="style6">
                                            :
                                        </td>
                                        <td class="style12">
                                            <asp:TextBox ID="txtPNM" runat="server" AutoPostBack="True" CssClass="txtColor" OnTextChanged="txtPNM_TextChanged"
                                                TabIndex="9" Width="300px"></asp:TextBox><asp:AutoCompleteExtender ID="txtPNM_AutoCompleteExtender"
                                                    runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters=""
                                                    Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListParty"
                                                    ServicePath="" TargetControlID="txtPNM" UseContextKey="True">
                                                </asp:AutoCompleteExtender>
                                        </td>
                                        <td class="style44">
                                            <asp:TextBox ID="txtPID" runat="server" ReadOnly="True" TabIndex="10"></asp:TextBox>
                                        </td>
                                        <td class="style71">
                                            Discount Amount
                                        </td>
                                        <td class="style2">
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtGrossDisAmt" runat="server" AutoPostBack="True" OnTextChanged="txtGrossDisAmt_TextChanged"
                                                Style="text-align: left" TabIndex="36">0.00</asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%; font-style: normal;">
                                    <tr>
                                        <td class="style74" style="text-align: right">
                                            Remarks
                                        </td>
                                        <td class="style15">
                                            :
                                        </td>
                                        <td class="style78">
                                            <asp:TextBox ID="txtRemarks" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                OnTextChanged="txtRemarks_TextChanged" TabIndex="11" Width="445px"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right">
                                            L.t. Cost
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLtCost" runat="server" AutoPostBack="True" OnTextChanged="txtLtCost_TextChanged"
                                                TabIndex="37">0.00</asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style74">
                                            &nbsp;
                                        </td>
                                        <td class="style15">
                                            &nbsp;
                                        </td>
                                        <td class="style78">
                                            &nbsp;
                                        </td>
                                        <td class="style86">
                                            Net Amount
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtNetAmt" runat="server" ReadOnly="True" TabIndex="38">0.00</asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style74">
                                            <asp:Label ID="lblCatID" runat="server" Visible="False"></asp:Label>
                                        </td>
                                        <td class="style15">
                                        </td>
                                        <td class="style78">
                                            <asp:Label ID="lblTransSL" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblSaleFrom" runat="server" Font-Bold="False" Font-Italic="True" ForeColor="#CC3300"
                                                Visible="False"></asp:Label>
                                            &nbsp;&nbsp;
                                            <asp:Label ID="lblPartyID" runat="server" Font-Bold="False" Font-Italic="True" ForeColor="#CC3300"
                                                Visible="False"></asp:Label>
                                        </td>
                                        <td class="style86">
                                            <asp:Button ID="btnComplete" runat="server" Font-Bold="True" OnClick="btnComplete_Click"
                                                Style="text-align: left" TabIndex="39" Text="Complete" />
                                        </td>
                                        <td>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Button ID="btnPrint" runat="server" Font-Bold="True" OnClick="btnPrint_Click"
                                                TabIndex="40" Text="PRINT" Width="70px" />
                                            &nbsp;<asp:Button ID="btnDoPrint" runat="server" Font-Bold="True" OnClick="btnDoPrint_Click"
                                                TabIndex="41" Text="PRINT DO" Width="80px" />
                                        </td>
                                    </tr>
                                </table>
                                <div id="grid_sale">
                                    <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="Gridview"
                                        Font-Italic="False" OnRowCancelingEdit="gvDetail_RowCancelingEdit" OnRowCommand="gvDetail_RowCommand"
                                        OnRowDataBound="gvDetail_RowDataBound" OnRowDeleting="gvDetail_RowDeleting" OnRowEditing="gvDetail_RowEditing"
                                        OnRowUpdating="gvDetail_RowUpdating" ShowFooter="True" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sale Form">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtStoreNMEdit" runat="server" AutoPostBack="True" Font-Names="Calibri"
                                                        OnTextChanged="txtStoreNMEdit_TextChanged" TabIndex="24" Text='<%#Eval("STORENM") %>'
                                                        Width="98%" />
                                                    <asp:AutoCompleteExtender ID="txtStoreNMEdit_AutoCompleteExtender" runat="server"
                                                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListStore"
                                                        ServicePath="" TargetControlID="txtStoreNMEdit" UseContextKey="True">
                                                    </asp:AutoCompleteExtender>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtStoreNM" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                        Font-Names="Calibri" OnTextChanged="txtStoreNM_TextChanged" TabIndex="12" Width="98%" />
                                                    <asp:AutoCompleteExtender ID="txtStoreNM_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                        Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListStore"
                                                        ServicePath="" TargetControlID="txtStoreNM" UseContextKey="True">
                                                    </asp:AutoCompleteExtender>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStoreNM" runat="server" Style="text-align: left" Text='<%# Eval("STORENM") %>'
                                                        Width="98%" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Store ID" Visible="False">
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblStoreIDEdit" runat="server" Style="text-align: left" Text='<%# Eval("STOREFR") %>' />
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtStoreID" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                        Font-Names="Calibri" OnTextChanged="txtStoreNM_TextChanged" TabIndex="12" Width="98%" />
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStoreID" runat="server" Style="text-align: left" Text='<%# Eval("STOREFR") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Particulars">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtItemNMEdit" runat="server" OnTextChanged="txtItemNMEdit_TextChanged"
                                                        TabIndex="24" Text='<%#Eval("ITEMNM") %>' Width="98%" AutoPostBack="True" Font-Names="Calibri" /><asp:AutoCompleteExtender
                                                            ID="txtItemNMEdit_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                            Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListEdit"
                                                            ServicePath="" TargetControlID="txtItemNMEdit" UseContextKey="True">
                                                        </asp:AutoCompleteExtender>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtItemNM" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                        OnTextChanged="txtItemNM_TextChanged" TabIndex="12" Width="98%" Font-Names="Calibri" /><asp:AutoCompleteExtender
                                                            ID="txtItemNM_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtItemNM"
                                                            UseContextKey="True">
                                                        </asp:AutoCompleteExtender>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemNM" runat="server" Style="text-align: left" Text='<%# Eval("ITEMNM") %>'
                                                        Width="98%" /></ItemTemplate>
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item ID" Visible="False">
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblItemIDEdit" runat="server" Style="text-align: center" Text='<%#Eval("ITEMID") %>'
                                                        Width="50px" /></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtItID" runat="server" CssClass="txtColor" TabIndex="13" Width="50px"
                                                        Font-Names="Calibri" Font-Size="12px"></asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemID" runat="server" Style="text-align: center" Text='<%# Eval("ITEMID") %>'
                                                        Width="50px" /></ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type">
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddltypeEdit" runat="server" Width="98%" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddltypeEdit_SelectedIndexChanged" TabIndex="25" Font-Names="Calibri">
                                                        <asp:ListItem Value="CARTON">CARTON</asp:ListItem>
                                                        <asp:ListItem>PIECES</asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                        OnSelectedIndexChanged="ddlType_SelectedIndexChanged" TabIndex="14" Width="98%"
                                                        Font-Names="Calibri">
                                                        <asp:ListItem Value="CARTON">CARTON</asp:ListItem>
                                                        <asp:ListItem>PIECES</asp:ListItem>
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType" runat="server" Style="text-align: center" Width="98%" Text='<%# Eval("UNITTP") %>'></asp:Label></ItemTemplate>
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Set">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCPQTYEdit" runat="server" ReadOnly="True" Style="text-align: right"
                                                        TabIndex="201" Text='<%#Eval("CPQTY") %>' Width="98%" Font-Names="Calibri"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtCPQTY" runat="server" CssClass="txtColor" ReadOnly="True" Style="text-align: right"
                                                        TabIndex="200" Width="98%" Font-Names="Calibri">.00</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCPQTY" runat="server" Style="text-align: right" Text='<%# Eval("CPQTY") %>'
                                                        Width="98%"></asp:Label></ItemTemplate>
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Carton">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCQtyEdit" runat="server" Style="text-align: right" TabIndex="27"
                                                        Text='<%#Eval("CQTY") %>' Width="98%" AutoPostBack="True" OnTextChanged="txtCQtyEdit_TextChanged"
                                                        Font-Names="Calibri"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtCQty" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                        OnTextChanged="txtCQty_TextChanged" Style="text-align: right" TabIndex="16" Width="98%"
                                                        Font-Names="Calibri">.00</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCQty" runat="server" Style="text-align: center" Text='<%# Eval("CQTY") %>'
                                                        Width="98%"></asp:Label></ItemTemplate>
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pieces">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPQtyEdit" runat="server" Style="text-align: right" TabIndex="28"
                                                        Text='<%#Eval("PQTY") %>' Width="98%" AutoPostBack="True" OnTextChanged="txtPQtyEdit_TextChanged"
                                                        Font-Names="Calibri"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtPQty" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                        OnTextChanged="txtPQty_TextChanged" Style="text-align: right" TabIndex="17" Width="98%"
                                                        Font-Names="Calibri">.00</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPQty" runat="server" Style="text-align: right" Text='<%# Eval("PQTY") %>'
                                                        Width="98%"></asp:Label></ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                                <ItemStyle Width="7%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtQtyEdit" runat="server" Style="text-align: right" TabIndex="29"
                                                        Text='<%#Eval("QTY") %>' Width="98%" Font-Names="Calibri"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtQty" runat="server" CssClass="txtColor" Style="text-align: right"
                                                        TabIndex="18" Width="98%" Font-Names="Calibri"></asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" runat="server" Style="text-align: right" Text='<%# Eval("QTY") %>'
                                                        Width="98%"></asp:Label></ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                                <ItemStyle HorizontalAlign="Right" Width="8%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtRateEdit" runat="server" Style="text-align: right" TabIndex="30"
                                                        Text='<%#Eval("RATE") %>' Width="98%" AutoPostBack="True" OnTextChanged="txtRateEdit_TextChanged"
                                                        Font-Names="Calibri"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtRate" runat="server" CssClass="txtColor" Style="text-align: right"
                                                        TabIndex="19" Width="98%" AutoPostBack="True" OnTextChanged="txtRate_TextChanged"
                                                        Font-Names="Calibri">.00</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRate" runat="server" Style="text-align: right" Text='<%# Eval("RATE") %>'
                                                        Width="98%"></asp:Label></ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtAmountEdit" runat="server" Style="text-align: right" Text='<%#Eval("AMOUNT") %>'
                                                        Width="98%" TabIndex="202" Font-Names="Calibri"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="txtColor" Style="text-align: right"
                                                        TabIndex="103" Width="98%" Font-Names="Calibri">.00</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Style="text-align: right" Text='<%# Eval("AMOUNT") %>'
                                                        Width="98%"></asp:Label></ItemTemplate>
                                                <HeaderStyle Width="12%" />
                                                <ItemStyle Width="12%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discount Rate" Visible="False">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDisRtEdit" runat="server" Style="text-align: right" TabIndex="31"
                                                        Text='<%#Eval("DISCRT") %>' Width="60px" AutoPostBack="True" OnTextChanged="txtDisRtEdit_TextChanged"
                                                        Font-Names="Calibri"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtDisRt" runat="server" CssClass="txtColor" Style="text-align: right"
                                                        TabIndex="20" Width="60px" AutoPostBack="True" OnTextChanged="txtDisRt_TextChanged"
                                                        Font-Names="Calibri">0</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDisRt" runat="server" Style="text-align: right" Text='<%#Eval("DISCRT") %>'
                                                        Width="60px"></asp:Label></ItemTemplate>
                                                <ControlStyle Width="60px" />
                                                <FooterStyle HorizontalAlign="Right" Width="60px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discount Amount" Visible="False">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDisAmtEdit" runat="server" Style="text-align: right" TabIndex="32"
                                                        Text='<%#Eval("DISCAMT") %>' Width="60px" AutoPostBack="True" OnTextChanged="txtDisAmtEdit_TextChanged"
                                                        Font-Names="Calibri"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtDisAmt" runat="server" CssClass="txtColor" Style="text-align: right"
                                                        TabIndex="21" Width="60px" AutoPostBack="True" OnTextChanged="txtDisAmt_TextChanged"
                                                        Font-Names="Calibri">.00</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDisAmt" runat="server" Style="text-align: right" Text='<%#Eval("DISCAMT") %>'
                                                        Width="60px"></asp:Label></ItemTemplate>
                                                <ControlStyle Width="60px" />
                                                <FooterStyle HorizontalAlign="Right" Width="60px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net Amount" Visible="False">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtNetAmtEdit" runat="server" Style="text-align: right" TabIndex="203"
                                                        Text='<%#Eval("NETAMT") %>' Width="80px" AutoPostBack="True" Font-Names="Calibri"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtNetAmt" runat="server" CssClass="txtColor" Style="text-align: right"
                                                        TabIndex="104" Width="100px" AutoPostBack="True" Font-Names="Calibri">.00</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNetAmt" runat="server" Style="text-align: right" Text='<%#Eval("NETAMT") %>'
                                                        Width="100px"></asp:Label></ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                <FooterStyle HorizontalAlign="Right" Width="100px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SL" Visible="False">
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblTransSLEdit" runat="server" Text='<%#Eval("TRANSSL") %>'></asp:Label></EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTransSL" runat="server" Text='<%#Eval("TRANSSL") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" Height="20px"
                                                        ImageUrl="~/Images/update.jpg" TabIndex="33" ToolTip="Update" Width="20px" /><asp:ImageButton
                                                            ID="imgbtnCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cancel.jpg"
                                                            TabIndex="34" ToolTip="Cancel" Width="20px" /></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="imgbtnAdd" runat="server" CommandName="SaveCon" CssClass="txtColor"
                                                        Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="22" ToolTip="Save &amp; Continue"
                                                        ValidationGroup="validaiton" Width="20px" /><asp:ImageButton ID="ImageButton1" runat="server"
                                                            CommandName="Complete" CssClass="txtColor" Height="30px" ImageUrl="~/Images/checkmark.jpg"
                                                            TabIndex="23" ToolTip="Complete" ValidationGroup="validaiton" Width="20px" /></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" Height="20px"
                                                        ImageUrl="~/Images/Edit.jpg" TabIndex="10" ToolTip="Edit" Width="20px" /><asp:ImageButton
                                                            ID="imgbtnDelete" runat="server" CommandName="Delete" Height="20px" ImageUrl="~/Images/delete.jpg"
                                                            TabIndex="11" OnClientClick="return confMSG()" ToolTip="Delete" Width="21px" /></ItemTemplate>
                                                <HeaderStyle Width="8%" />
                                                <ItemStyle Width="8%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999966" />
                                        <HeaderStyle BackColor="#61A6F8" Font-Bold="True" Font-Names="Calibri" Font-Size="14px"
                                            ForeColor="White" />
                                        <RowStyle Font-Names="Calibri" />
                                    </asp:GridView>
                                    <table style="width: 100%; font-style: normal;">
                                        <tr>
                                            <td class="style28">
                                                <asp:Label ID="lblGridMsg" runat="server" Font-Bold="False" ForeColor="#CC3300" Visible="False"></asp:Label>
                                                <asp:TextBox ID="txtTotal" runat="server" ReadOnly="True" Style="text-align: right"
                                                    TabIndex="106" Visible="False" Width="100px">.00</asp:TextBox>
                                                <asp:TextBox ID="txtTDisAmount" runat="server" ReadOnly="True" Style="text-align: right"
                                                    Visible="False" Width="60px">.00</asp:TextBox>
                                            </td>
                                            <td class="style37" style="text-align: right">
                                                Total
                                            </td>
                                            <td style="width: 1%">
                                                :
                                            </td>
                                            <td class="style89">
                                                <asp:TextBox ID="txtTCarton" runat="server" ReadOnly="True" Style="text-align: right"
                                                    TabIndex="106" Width="50px">.00</asp:TextBox>
                                            </td>
                                            <td style="text-align: right" class="style88">
                                                &nbsp;</td>
                                            <td class="style72">
                                                <asp:TextBox ID="txtTQuantity" runat="server" ReadOnly="True" 
                                                    Style="text-align: right" TabIndex="106" Width="70px">.00</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTAmount" runat="server" ReadOnly="True" Style="text-align: right"
                                                    TabIndex="106" Width="120px">.00</asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="float: left; width: 100%;">
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="PURCHASE INFORMATION" Enabled="true"
                        OnDemandMode="Once">
                        <HeaderTemplate>
                            PURCHASE INFORMATION</HeaderTemplate>
                        <ContentTemplate>
                            <div id="header_purchase">
                                <h1 align="center">
                                    PURCHASE INFORMATION</h1>
                            </div>
                            <div id="entry_purchase">
                                <div id="toolbar_purchase">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="style34" style="text-align: right">
                                                <asp:Button ID="btnPurchaseEdit" runat="server" Font-Bold="True" OnClick="btnPurchaseEdit_Click"
                                                    Text="EDIT" Width="70px" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnPurchasePrint" runat="server" Font-Bold="True" Text="PRINT" Width="70px"
                                                    OnClick="btnPurchasePrint_Click" Visible="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style34">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table style="width: 100%; font-style: normal;">
                                    <tr>
                                        <td class="style13" style="font-style: normal">
                                            Invoice Date &amp; No
                                        </td>
                                        <td class="style2" style="font-style: normal">
                                            :
                                        </td>
                                        <td class="style27">
                                            <asp:TextBox ID="txtPInDT" runat="server" AutoPostBack="True" ClientIDMode="Static"
                                                CssClass="txtColor" OnTextChanged="txtPInDT_TextChanged" TabIndex="35" Width="84px"></asp:TextBox>
                                        </td>
                                        <td class="style61">
                                            <asp:TextBox ID="txtPInNo" runat="server" CssClass="txtColor" ReadOnly="True" TabIndex="36"
                                                Width="80px"></asp:TextBox>
                                            <asp:DropDownList ID="ddlPurchaseEditInNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPurchaseEditInNo_TextChanged"
                                                TabIndex="36" Visible="False" Width="80px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style5">
                                            Memo No
                                        </td>
                                        <td class="style6">
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPMemoNo" runat="server" CssClass="txtColor" TabIndex="37" Width="135px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%; font-style: normal;">
                                    <tr>
                                        <td class="style57">
                                            Purchase To
                                        </td>
                                        <td class="style6">
                                            :
                                        </td>
                                        <td class="style17">
                                            <asp:TextBox ID="txtPTo" runat="server" AutoPostBack="True" CssClass="txtColor" Font-Bold="False"
                                                OnTextChanged="txtPTo_TextChanged" TabIndex="38" Width="300px"></asp:TextBox><asp:AutoCompleteExtender
                                                    ID="txtPTo_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="3"
                                                    DelimiterCharacters="" Enabled="True" ServicePath="" MinimumPrefixLength="1"
                                                    ServiceMethod="GetCompletionListPStore" TargetControlID="txtPTo" UseContextKey="True">
                                                </asp:AutoCompleteExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPToID" runat="server" CssClass="txtColor" OnTextChanged="txtSlFr_TextChanged"
                                                ReadOnly="True" TabIndex="39"></asp:TextBox>&#160;&#160;&#160;&#160;&#160;
                                            <asp:Label ID="lblPValidMsg" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#CC3300"
                                                Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%; font-style: normal;">
                                    <tr>
                                        <td class="style35">
                                            LC Type
                                        </td>
                                        <td class="style2">
                                            :
                                        </td>
                                        <td class="style36">
                                            <asp:DropDownList ID="ddlPLCType" runat="server" Width="80px" AutoPostBack="True"
                                                CssClass="txtColor" OnSelectedIndexChanged="ddlPLCType_SelectedIndexChanged"
                                                TabIndex="40">
                                                <asp:ListItem>IMPORT</asp:ListItem>
                                                <asp:ListItem>LOCAL</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style37">
                                            <asp:TextBox ID="txtPLCID" runat="server" Width="218px" CssClass="txtColor" TabIndex="41"
                                                AutoPostBack="True" OnTextChanged="txtPLCID_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtPLCID_AutoCompleteExtender" runat="server" CompletionInterval="10"
                                                CompletionSetCount="3" DelimiterCharacters="" Enabled="True" ServicePath="" MinimumPrefixLength="1"
                                                ServiceMethod="GetCompletionListPLcID" TargetControlID="txtPLCID" UseContextKey="True">
                                            </asp:AutoCompleteExtender>
                                        </td>
                                        <td class="style38">
                                            LC Date
                                        </td>
                                        <td class="style2">
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPLCDT" runat="server" AutoPostBack="True" ClientIDMode="Static"
                                                CssClass="txtColor" OnTextChanged="txtPLCDT_TextChanged" TabIndex="42" Width="84px"></asp:TextBox>
                                            <asp:Label ID="lblPLcCD" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%; font-style: normal;">
                                    <tr>
                                        <td class="style35">
                                            Supplier ID
                                        </td>
                                        <td class="style6">
                                            :
                                        </td>
                                        <td class="style12">
                                            <asp:TextBox ID="txtPSupplier" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                OnTextChanged="txtPSupplier_TextChanged" TabIndex="43" Width="300px"></asp:TextBox><asp:AutoCompleteExtender
                                                    ID="txtPSupplier_AutoCompleteExtender" runat="server" CompletionInterval="10"
                                                    CompletionSetCount="3" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1"
                                                    ServiceMethod="GetCompletionListSupplier" ServicePath="" TargetControlID="txtPSupplier"
                                                    UseContextKey="True">
                                                </asp:AutoCompleteExtender>
                                        </td>
                                        <td class="style32">
                                            <asp:TextBox ID="txtPSupID" runat="server" ReadOnly="True" TabIndex="44"></asp:TextBox>
                                        </td>
                                        <td class="style33">
                                            <asp:Label ID="lblValidMSG" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#CC3300"
                                                Visible="False"></asp:Label>
                                        </td>
                                        <td class="style15">
                                            &#160;
                                        </td>
                                        <td>
                                            &#160;
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%; font-style: normal;">
                                    <tr>
                                        <td class="style77" style="text-align: right">
                                            Remarks
                                        </td>
                                        <td class="style15">
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPRemarks" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                OnTextChanged="txtPRemarks_TextChanged" TabIndex="46" Width="452px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style77">
                                            &nbsp;
                                        </td>
                                        <td class="style15">
                                            &#160;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnPUpdate" runat="server" Font-Bold="True" OnClick="btnPUpdate_Click"
                                                TabIndex="47" Text="Update" Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style77">
                                            <asp:Label ID="lblPTransSL" runat="server" Visible="False"></asp:Label>
                                        </td>
                                        <td class="style15">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPItemID" runat="server"></asp:Label>
                                            <asp:Label ID="lblPLCTP" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblPMy" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblPMxNo" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblPCatID" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div style="width: 100%; margin-bottom: 10px;">
                                    <asp:GridView ID="gvPurchase" runat="server" AutoGenerateColumns="False" CssClass="Gridview"
                                        Font-Italic="False" OnRowCancelingEdit="gvPurchase_RowCancelingEdit" OnRowCommand="gvPurchase_RowCommand"
                                        OnRowDataBound="gvPurchase_RowDataBound" OnRowDeleting="gvPurchase_RowDeleting"
                                        OnRowEditing="gvPurchase_RowEditing" OnRowUpdating="gvPurchase_RowUpdating" ShowFooter="True"
                                        Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Item Particulars">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtItemNMEdit_P" runat="server" TabIndex="58" Text='<%#Eval("ITEMNM") %>'
                                                        Width="230px" AutoPostBack="True" OnTextChanged="txtItemNMEdit_P_TextChanged" /><asp:AutoCompleteExtender
                                                            ID="txtItemNMEdit_P_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                            Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListPEdit"
                                                            ServicePath="" TargetControlID="txtItemNMEdit_P" UseContextKey="True">
                                                        </asp:AutoCompleteExtender>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtItemNM_P" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                        OnTextChanged="txtItemNM_P_TextChanged" TabIndex="47" Width="230px" /><asp:AutoCompleteExtender
                                                            ID="txtItemNM_P_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionPList" ServicePath="" TargetControlID="txtItemNM_P"
                                                            UseContextKey="True">
                                                        </asp:AutoCompleteExtender>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemNM_P" runat="server" Style="text-align: left" Text='<%# Eval("ITEMNM") %>'
                                                        Width="250px" /></ItemTemplate>
                                                <ControlStyle Width="230px" />
                                                <FooterStyle HorizontalAlign="Left" Width="230px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="230px" />
                                                <ItemStyle HorizontalAlign="Left" Width="230px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item ID">
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblItemIDEdit_P" runat="server" Style="text-align: center" Text='<%# Eval("ITEMID") %>'
                                                        Width="50px" /></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtItemID_P" runat="server" CssClass="txtColor" TabIndex="48" Width="60px"></asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemID_P" runat="server" Style="text-align: center" Text='<%# Eval("ITEMID") %>'
                                                        Width="50px" /></ItemTemplate>
                                                <ControlStyle Width="60px" />
                                                <FooterStyle HorizontalAlign="Center" Width="60px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type">
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddltypeEdit_P" runat="server" Width="80px" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddltypeEdit_P_SelectedIndexChanged" TabIndex="60">
                                                        <asp:ListItem Value="CARTON">CARTON</asp:ListItem>
                                                        <asp:ListItem>PIECES</asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlType_P" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                        OnSelectedIndexChanged="ddlType_P_SelectedIndexChanged" TabIndex="49" Width="80px">
                                                        <asp:ListItem Value="CARTON">CARTON</asp:ListItem>
                                                        <asp:ListItem>PIECES</asp:ListItem>
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType_P" runat="server" Style="text-align: center" Text='<%# Eval("UNITTP") %>'></asp:Label></ItemTemplate>
                                                <ControlStyle Width="80px" />
                                                <FooterStyle HorizontalAlign="Left" Width="80px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Set">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCPQTYEdit_P" runat="server" ReadOnly="True" Style="text-align: right"
                                                        TabIndex="110" Text='<%#Eval("CPQTY") %>' Width="50px"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtCPQTY_P" runat="server" CssClass="txtColor" ReadOnly="True" Style="text-align: right"
                                                        TabIndex="105" Width="50px"></asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCPQTY_P" runat="server" Style="text-align: right" Text='<%# Eval("CPQTY") %>'></asp:Label></ItemTemplate>
                                                <ControlStyle Width="50px" />
                                                <FooterStyle HorizontalAlign="Left" Width="50px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Carton">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCQtyEdit_P" runat="server" Style="text-align: right" TabIndex="62"
                                                        Text='<%#Eval("CQTY") %>' Width="50px" AutoPostBack="True" OnTextChanged="txtCQtyEdit_P_TextChanged"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtCQty_P" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                        OnTextChanged="txtCQty_P_TextChanged" Style="text-align: right" TabIndex="51"
                                                        Width="50px"></asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCQty_P" runat="server" Style="text-align: center" Text='<%# Eval("CQTY") %>'></asp:Label></ItemTemplate>
                                                <ControlStyle Width="50px" />
                                                <FooterStyle HorizontalAlign="Left" Width="50px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pieces">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPQtyEdit_P" runat="server" Style="text-align: right" TabIndex="63"
                                                        Text='<%#Eval("PQTY") %>' Width="70px" AutoPostBack="True" OnTextChanged="txtPQtyEdit_P_TextChanged"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtPQty_P" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                        OnTextChanged="txtPQty_P_TextChanged" Style="text-align: right" TabIndex="52"
                                                        Width="70px"></asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPQty_P" runat="server" Style="text-align: right" Text='<%# Eval("PQTY") %>'
                                                        Width="90px"></asp:Label></ItemTemplate>
                                                <ControlStyle Width="70px" />
                                                <FooterStyle HorizontalAlign="Right" Width="70px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                <ItemStyle Width="70px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtQtyEdit_P" runat="server" Style="text-align: right" TabIndex="112"
                                                        Text='<%#Eval("QTY") %>' Width="90px" OnTextChanged="txtQtyEdit_P_TextChanged"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtQty_P" runat="server" CssClass="txtColor" Style="text-align: right"
                                                        TabIndex="106" Width="90px" OnTextChanged="txtQty_P_TextChanged"></asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty_P" runat="server" Style="text-align: right" Text='<%# Eval("QTY") %>'
                                                        Width="90px"></asp:Label></ItemTemplate>
                                                <ControlStyle Width="90px" />
                                                <FooterStyle HorizontalAlign="Right" Width="90px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtRateEdit_P" runat="server" Style="text-align: right" TabIndex="65"
                                                        Text='<%#Eval("RATE") %>' Width="80px" AutoPostBack="True" OnTextChanged="txtRateEdit_P_TextChanged"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtRate_P" runat="server" CssClass="txtColor" Style="text-align: right"
                                                        TabIndex="54" Width="100px" AutoPostBack="True" OnTextChanged="txtRate_P_TextChanged">.00</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRate_P" runat="server" Style="text-align: right" Text='<%# Eval("RATE") %>'
                                                        Width="60px"></asp:Label></ItemTemplate>
                                                <ControlStyle Width="60px" />
                                                <FooterStyle HorizontalAlign="Right" Width="60px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtAmountEdit_P" runat="server" Style="text-align: right" Text='<%#Eval("RATE") %>'
                                                        Width="110px" TabIndex="113"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtAmount_P" runat="server" CssClass="txtColor" Style="text-align: right"
                                                        TabIndex="107" Width="110px">.00</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount_P" runat="server" Style="text-align: right" Text='<%# Eval("AMOUNT") %>'></asp:Label></ItemTemplate>
                                                <ControlStyle Width="110px" />
                                                <FooterStyle Width="110px" />
                                                <HeaderStyle Width="110px" />
                                                <ItemStyle Width="110px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SL" Visible="False">
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblTransSLEdit_P" runat="server" Text='<%# Eval("TRANSSL") %>'></asp:Label></EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTransSL_P" runat="server" Text='<%# Eval("TRANSSL") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="imgbtnPUpdate" runat="server" CommandName="Update" Height="20px"
                                                        ImageUrl="~/Images/update.jpg" TabIndex="67" ToolTip="Update" Width="20px" /><asp:ImageButton
                                                            ID="imgbtnPCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cancel.jpg"
                                                            TabIndex="68" ToolTip="Cancel" Width="20px" /></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="imgbtnPAdd" runat="server" CommandName="SaveCon" CssClass="txtColor"
                                                        Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="56" ToolTip="Save &amp; Continue"
                                                        ValidationGroup="validaiton" Width="30px" /><asp:ImageButton ID="ImagebtnPComp" runat="server"
                                                            CommandName="Complete" CssClass="txtColor" Height="30px" ImageUrl="~/Images/checkmark.jpg"
                                                            TabIndex="57" ToolTip="Complete" ValidationGroup="validaiton" Width="30px" /><asp:ImageButton
                                                                ID="ImagebtnPPrint" runat="server" CommandName="SavePrint" CssClass="txtColor"
                                                                Height="30px" ImageUrl="~/Images/print.gif" TabIndex="58" ToolTip="Save &amp; Print"
                                                                ValidationGroup="validaiton" Width="30px" />
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px"
                                                        ImageUrl="~/Images/Edit.jpg" TabIndex="10" ToolTip="Edit" Width="20px" />
                                                    <asp:ImageButton ID="imgbtnPDelete" runat="server" CommandName="Delete" OnClientClick="return confMSG()"
                                                        Height="20px" ImageUrl="~/Images/delete.jpg" TabIndex="11" ToolTip="Delete" Width="20px" /></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999966" />
                                        <HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                    <table style="width: 100%; font-style: normal;">
                                        <tr>
                                            <td class="style28">
                                                <asp:Label ID="lblPGridMSG" runat="server" Font-Bold="True" ForeColor="#CC3300" Visible="False"
                                                    Font-Italic="True"></asp:Label>
                                            </td>
                                            <td class="style30">
                                                &nbsp;
                                            </td>
                                            <td class="style30">
                                                &nbsp;
                                            </td>
                                            <td class="style70">
                                                &nbsp;
                                            </td>
                                            <td class="style30">
                                                Total
                                            </td>
                                            <td class="style6">
                                                :
                                            </td>
                                            <td class="style31">
                                                <asp:TextBox ID="txtPTotal" runat="server" ReadOnly="True" Style="text-align: right"
                                                    TabIndex="106" Width="110px">.00</asp:TextBox>
                                            </td>
                                            <td>
                                                &#160;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="TRANSFER INFORMATION" Enabled="true"
                        OnDemandMode="Once">
                        <HeaderTemplate>
                            TRANSFER INFORMATION</HeaderTemplate>
                        <ContentTemplate>
                            <div id="header_transfer">
                                <h1 align="center">
                                    TRANSFER INFORMATION</h1>
                            </div>
                            <div id="entry_transfer">
                                <div id="toolbar_transfer">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="style34" style="text-align: right">
                                                <asp:Button ID="btnTransferEdit" runat="server" Font-Bold="True" OnClick="btnTransferEdit_Click"
                                                    Text="EDIT" Width="70px" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnTransferPrint" runat="server" Font-Bold="True" Text="PRINT" Width="70px"
                                                    OnClick="btnTransferPrint_Click" Visible="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style34">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table style="width: 100%; font-style: normal;">
                                    <tr>
                                        <td class="style13" style="font-style: normal">
                                            Invoice Date &amp; No
                                        </td>
                                        <td class="style2" style="font-style: normal">
                                            :
                                        </td>
                                        <td class="style27">
                                            <asp:TextBox ID="txtInDT_Trans" runat="server" AutoPostBack="True" ClientIDMode="Static"
                                                CssClass="txtColor" TabIndex="70" Width="84px" OnTextChanged="txtInDT_Trans_TextChanged"></asp:TextBox>
                                        </td>
                                        <td class="style76">
                                            <asp:TextBox ID="txtInNo_Trans" runat="server" CssClass="txtColor" ReadOnly="True"
                                                TabIndex="71" Width="80px" OnTextChanged="txtInNo_Trans_TextChanged"></asp:TextBox>
                                            <asp:DropDownList ID="ddlInNoEdit_Trans" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInNoEdit_Trans_TextChanged"
                                                TabIndex="71" Visible="False" Width="80px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style5">
                                            Memo No
                                        </td>
                                        <td class="style6">
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMemo_Trans" runat="server" CssClass="txtColor" TabIndex="73"
                                                Width="135px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%; font-style: normal;">
                                    <tr>
                                        <td class="style56">
                                            Transfer From
                                        </td>
                                        <td class="style6">
                                            :
                                        </td>
                                        <td class="style17">
                                            <asp:TextBox ID="txtTFr_Trans" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                Font-Bold="False" TabIndex="74" Width="300px" OnTextChanged="txtTFr_Trans_TextChanged"></asp:TextBox><asp:AutoCompleteExtender
                                                    ID="txtTFr_Trans_AutoCompleteExtender" runat="server" CompletionInterval="10"
                                                    CompletionSetCount="3" DelimiterCharacters="" Enabled="True" ServicePath="" MinimumPrefixLength="1"
                                                    ServiceMethod="GetCompletionListStore_TFrom" TargetControlID="txtTFr_Trans" UseContextKey="True">
                                                </asp:AutoCompleteExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTFrID_Trans" runat="server" CssClass="txtColor" ReadOnly="True"
                                                TabIndex="75"></asp:TextBox>&#160;&#160;&#160;&#160;&#160;
                                            <asp:Label ID="lblFromMsg_Trans" runat="server" Font-Bold="True" Font-Italic="True"
                                                ForeColor="#CC3300" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%; font-style: normal;">
                                    <tr>
                                        <td class="style56">
                                            Transfer To
                                        </td>
                                        <td class="style6">
                                            :
                                        </td>
                                        <td class="style12">
                                            <asp:TextBox ID="txtTTo_Trans" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                TabIndex="76" Width="300px" OnTextChanged="txtTTo_Trans_TextChanged"></asp:TextBox><asp:AutoCompleteExtender
                                                    ID="txtTTo_Trans_AutoCompleteExtender" runat="server" CompletionInterval="10"
                                                    CompletionSetCount="3" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1"
                                                    ServiceMethod="GetCompletionListStore_TFrom" ServicePath="" TargetControlID="txtTTo_Trans"
                                                    UseContextKey="True">
                                                </asp:AutoCompleteExtender>
                                        </td>
                                        <td class="style32">
                                            <asp:TextBox ID="txtTToID_Trans" runat="server" ReadOnly="True" TabIndex="77"></asp:TextBox>
                                        </td>
                                        <td class="style33">
                                            <asp:Label ID="lblMsgTo_Trans" runat="server" Font-Bold="True" Font-Italic="True"
                                                ForeColor="#CC3300" Visible="False"></asp:Label>
                                        </td>
                                        <td class="style15">
                                            &#160;
                                        </td>
                                        <td>
                                            &#160;
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%; font-style: normal;">
                                    <tr>
                                        <td class="style44" style="text-align: right">
                                            Remarks
                                        </td>
                                        <td class="style15">
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRemarks_Trans" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                OnTextChanged="txtRemarks_Trans_TextChanged" TabIndex="78" Width="452px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style44">
                                            <asp:Label ID="lblTTransSl" runat="server" Visible="False"></asp:Label>
                                        </td>
                                        <td class="style15">
                                            &#160;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTItemID" runat="server"></asp:Label><asp:Label ID="lblTMy" runat="server"
                                                Visible="False"></asp:Label><asp:Label ID="lblTMxNo" runat="server" Visible="False"></asp:Label><asp:Label
                                                    ID="lblTCatID" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div style="width: 100%; margin-bottom: 10px;">
                                    <asp:GridView ID="gv_Transfer" runat="server" AutoGenerateColumns="False" CssClass="Gridview"
                                        Font-Italic="False" OnRowCancelingEdit="gv_Transfer_RowCancelingEdit" OnRowCommand="gv_Transfer_RowCommand"
                                        OnRowDataBound="gv_Transfer_RowDataBound" OnRowDeleting="gv_Transfer_RowDeleting"
                                        OnRowEditing="gv_Transfer_RowEditing" OnRowUpdating="gv_Transfer_RowUpdating"
                                        ShowFooter="True" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Item Particulars">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtItemNMEdit_T" runat="server" TabIndex="88" Text='<%#Eval("ITEMNM") %>'
                                                        Width="230px" AutoPostBack="True" OnTextChanged="txtItemNMEdit_T_TextChanged" /><asp:AutoCompleteExtender
                                                            ID="txtItemNMEdit_T_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                            Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListTEdit"
                                                            ServicePath="" TargetControlID="txtItemNMEdit_T" UseContextKey="True">
                                                        </asp:AutoCompleteExtender>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtItemNM_T" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                        TabIndex="79" Width="230px" OnTextChanged="txtItemNM_T_TextChanged" /><asp:AutoCompleteExtender
                                                            ID="txtItemNM_T_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionTList" ServicePath="" TargetControlID="txtItemNM_T"
                                                            UseContextKey="True">
                                                        </asp:AutoCompleteExtender>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemNM_T" runat="server" Style="text-align: left" Text='<%# Eval("ITEMNM") %>'
                                                        Width="250px" /></ItemTemplate>
                                                <ControlStyle Width="230px" />
                                                <FooterStyle HorizontalAlign="Left" Width="230px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="230px" />
                                                <ItemStyle HorizontalAlign="Left" Width="230px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item ID">
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblItemIDEdit_T" runat="server" Style="text-align: center" Text='<%# Eval("ITEMID") %>'
                                                        Width="50px" /></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtItemID_T" runat="server" CssClass="txtColor" TabIndex="80" Width="60px"></asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemID_T" runat="server" Style="text-align: center" Text='<%# Eval("ITEMID") %>'
                                                        Width="50px" /></ItemTemplate>
                                                <ControlStyle Width="60px" />
                                                <FooterStyle HorizontalAlign="Center" Width="60px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type">
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddltypeEdit_T" runat="server" Width="80px" AutoPostBack="True"
                                                        TabIndex="89" OnSelectedIndexChanged="ddltypeEdit_T_SelectedIndexChanged">
                                                        <asp:ListItem Value="CARTON">CARTON</asp:ListItem>
                                                        <asp:ListItem>PIECES</asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlType_T" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                        TabIndex="81" Width="80px" OnSelectedIndexChanged="ddlType_T_SelectedIndexChanged">
                                                        <asp:ListItem Value="CARTON">CARTON</asp:ListItem>
                                                        <asp:ListItem>PIECES</asp:ListItem>
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType_T" runat="server" Style="text-align: center" Text='<%# Eval("UNITTP") %>'></asp:Label></ItemTemplate>
                                                <ControlStyle Width="80px" />
                                                <FooterStyle HorizontalAlign="Left" Width="80px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Set">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCPQTYEdit_T" runat="server" ReadOnly="True" Style="text-align: right"
                                                        TabIndex="210" Text='<%#Eval("CPQTY") %>' Width="50px"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtCPQTY_T" runat="server" CssClass="txtColor" ReadOnly="True" Style="text-align: right"
                                                        TabIndex="205" Width="50px"></asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCPQTY_T" runat="server" Style="text-align: right" Text='<%# Eval("CPQTY") %>'></asp:Label></ItemTemplate>
                                                <ControlStyle Width="50px" />
                                                <FooterStyle HorizontalAlign="Left" Width="50px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Carton">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCQtyEdit_T" runat="server" Style="text-align: right" TabIndex="90"
                                                        Text='<%#Eval("CQTY") %>' Width="50px" AutoPostBack="True" OnTextChanged="txtCQtyEdit_T_TextChanged"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtCQty_T" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                        Style="text-align: right" TabIndex="82" Width="50px" OnTextChanged="txtCQty_T_TextChanged"></asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCQty_T" runat="server" Style="text-align: center" Text='<%# Eval("CQTY") %>'></asp:Label></ItemTemplate>
                                                <ControlStyle Width="50px" />
                                                <FooterStyle HorizontalAlign="Left" Width="50px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pieces">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPQtyEdit_T" runat="server" Style="text-align: right" TabIndex="91"
                                                        Text='<%#Eval("PQTY") %>' Width="70px" AutoPostBack="True" OnTextChanged="txtPQtyEdit_T_TextChanged"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtPQty_T" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                        Style="text-align: right" TabIndex="83" Width="70px" OnTextChanged="txtPQty_T_TextChanged"></asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPQty_T" runat="server" Style="text-align: right" Text='<%# Eval("PQTY") %>'
                                                        Width="90px"></asp:Label></ItemTemplate>
                                                <ControlStyle Width="70px" />
                                                <FooterStyle HorizontalAlign="Right" Width="70px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                <ItemStyle Width="70px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtQtyEdit_T" runat="server" Style="text-align: right" TabIndex="212"
                                                        Text='<%#Eval("QTY") %>' Width="90px"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtQty_T" runat="server" CssClass="txtColor" Style="text-align: right"
                                                        TabIndex="206" Width="90px"></asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty_T" runat="server" Style="text-align: right" Text='<%# Eval("QTY") %>'
                                                        Width="90px"></asp:Label></ItemTemplate>
                                                <ControlStyle Width="90px" />
                                                <FooterStyle HorizontalAlign="Right" Width="90px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtRateEdit_T" runat="server" Style="text-align: right" TabIndex="92"
                                                        Text='<%#Eval("RATE") %>' Width="80px" AutoPostBack="True" OnTextChanged="txtRateEdit_T_TextChanged"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtRate_T" runat="server" CssClass="txtColor" Style="text-align: right"
                                                        TabIndex="84" Width="100px" AutoPostBack="True" OnTextChanged="txtRate_T_TextChanged"></asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRate_T" runat="server" Style="text-align: right" Text='<%# Eval("RATE") %>'
                                                        Width="60px"></asp:Label></ItemTemplate>
                                                <ControlStyle Width="60px" />
                                                <FooterStyle HorizontalAlign="Right" Width="60px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtAmountEdit_T" runat="server" Style="text-align: right" Text='<%#Eval("RATE") %>'
                                                        Width="110px" TabIndex="113"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtAmount_T" runat="server" CssClass="txtColor" Style="text-align: right"
                                                        TabIndex="207" Width="110px"></asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount_T" runat="server" Style="text-align: right" Text='<%# Eval("AMOUNT") %>'></asp:Label></ItemTemplate>
                                                <ControlStyle Width="110px" />
                                                <FooterStyle Width="110px" />
                                                <HeaderStyle Width="110px" />
                                                <ItemStyle Width="110px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SL" Visible="False">
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblTransSLEdit_T" runat="server" Text='<%# Eval("TRANSSL") %>'></asp:Label></EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTransSL_T" runat="server" Text='<%# Eval("TRANSSL") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="imgbtnTUpdate" runat="server" CommandName="Update" Height="20px"
                                                        ImageUrl="~/Images/update.jpg" TabIndex="93" ToolTip="Update" Width="20px" /><asp:ImageButton
                                                            ID="imgbtnTCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cancel.jpg"
                                                            TabIndex="93" ToolTip="Cancel" Width="20px" /></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="imgbtnTAdd" runat="server" CommandName="SaveCon" CssClass="txtColor"
                                                        Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="85" ToolTip="Save &amp; Continue"
                                                        ValidationGroup="validaiton" Width="30px" /><asp:ImageButton ID="ImagebtnTComp" runat="server"
                                                            CommandName="Complete" CssClass="txtColor" Height="30px" ImageUrl="~/Images/checkmark.jpg"
                                                            TabIndex="86" ToolTip="Complete" ValidationGroup="validaiton" Width="30px" /><asp:ImageButton
                                                                ID="ImagebtnTPrint" runat="server" CommandName="SavePrint" CssClass="txtColor"
                                                                Height="30px" ImageUrl="~/Images/print.gif" TabIndex="87" ToolTip="Save &amp; Print"
                                                                ValidationGroup="validaiton" Width="30px" /></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px"
                                                        ImageUrl="~/Images/Edit.jpg" TabIndex="10" ToolTip="Edit" Width="20px" /><asp:ImageButton
                                                            ID="imgbtnPDelete" runat="server" CommandName="Delete" OnClientClick="return confMSG()"
                                                            Height="20px" ImageUrl="~/Images/delete.jpg" TabIndex="11" ToolTip="Delete" Width="20px" /></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999966" />
                                        <HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                    <table style="width: 100%; font-style: normal;">
                                        <tr>
                                            <td class="style28">
                                                <asp:Label ID="lblTGridMSG" runat="server" Font-Bold="True" ForeColor="#CC3300" Visible="False"
                                                    Font-Italic="True"></asp:Label>
                                            </td>
                                            <td class="style30">
                                                &nbsp;
                                            </td>
                                            <td class="style30">
                                                &nbsp;
                                            </td>
                                            <td class="style71">
                                                &nbsp;
                                            </td>
                                            <td class="style30">
                                                Total
                                            </td>
                                            <td class="style6">
                                                :
                                            </td>
                                            <td class="style31">
                                                <asp:TextBox ID="txtTTotalAmount" runat="server" ReadOnly="True" Style="text-align: right"
                                                    TabIndex="106" Width="110px">.00</asp:TextBox>
                                            </td>
                                            <td>
                                                &#160;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="RETURN INFORMATION" Enabled="true"
                        OnDemandMode="Once">
                        <HeaderTemplate>
                            RETURN INFORMATION</HeaderTemplate>
                        <ContentTemplate>
                            <div id="header_ret">
                                <h1 align="center" style="font-style: normal">
                                    SALE/PURCHASE RETURN</h1>
                            </div>
                            <div id="entry_ret">
                                <div id="toolbar_ret">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="style34" style="text-align: right">
                                                <asp:Button ID="btnEdit_Ret" runat="server" Font-Bold="True" OnClick="btnEdit_Ret_Click"
                                                    Text="EDIT" Width="70px" />
                                            </td>
                                            <td>
                                                &#160;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style34">
                                                <asp:Label ID="lblItemId_Ret" runat="server"></asp:Label>
                                                <asp:Label ID="lblMy_Ret" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lblMxNo_Ret" runat="server" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSmsgComTrans_Ret" runat="server" ForeColor="#990000" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table style="width: 100%; font-style: normal;">
                                    <tr>
                                        <td class="style13" style="font-style: normal">
                                            Return Type
                                        </td>
                                        <td class="style2" style="font-style: normal">
                                            :
                                        </td>
                                        <td class="style27" colspan="2">
                                            <asp:DropDownList ID="ddlRetType" runat="server" Width="180px" CssClass="txtColor"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlRetType_SelectedIndexChanged">
                                                <asp:ListItem Value="IRTS">SALE RETURN</asp:ListItem>
                                                <asp:ListItem Value="IRTB">PURCHASE RETURN</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style56">
                                            &nbsp;
                                        </td>
                                        <td class="style6">
                                            &nbsp;
                                        </td>
                                        <td class="style45">
                                            &nbsp;
                                        </td>
                                        <td class="style49">
                                            &#160;
                                        </td>
                                        <td class="style48">
                                            &#160;
                                        </td>
                                        <td>
                                            &#160;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style13" style="font-style: normal">
                                            Invoice Date &amp; No
                                        </td>
                                        <td class="style2" style="font-style: normal">
                                            :
                                        </td>
                                        <td class="style73">
                                            <asp:TextBox ID="txtTInDt_Ret" runat="server" AutoPostBack="True" ClientIDMode="Static"
                                                CssClass="txtColor" TabIndex="1" Width="84px" OnTextChanged="txtTInDt_Ret_TextChanged"
                                                Height="22px"></asp:TextBox>
                                        </td>
                                        <td class="style72">
                                            <asp:TextBox ID="txtInNo_Ret" runat="server" CssClass="txtColor" ReadOnly="True"
                                                TabIndex="2" Width="80px"></asp:TextBox>
                                            <asp:DropDownList ID="ddlSalesEditInNo_Ret" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                TabIndex="2" Visible="False" Width="80px" OnSelectedIndexChanged="ddlSalesEditInNo_Ret_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style56">
                                            <asp:Label ID="T_Ret" runat="server"></asp:Label>
                                            &nbsp;Memo No
                                        </td>
                                        <td class="style6">
                                            :
                                        </td>
                                        <td class="style45">
                                            <asp:TextBox ID="txtSLMNo_Ret" runat="server" CssClass="txtColor" TabIndex="3" Width="135px"></asp:TextBox>
                                        </td>
                                        <td class="style49">
                                            &nbsp;
                                        </td>
                                        <td class="style48">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%; font-style: normal;">
                                    <tr>
                                        <td class="style35">
                                            <asp:Label ID="lblMode" runat="server"></asp:Label>
                                        </td>
                                        <td class="style6">
                                            :
                                        </td>
                                        <td class="style17">
                                            <asp:TextBox ID="txtSaleFrom_Ret" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                Font-Bold="False" TabIndex="4" Width="300px" OnTextChanged="txtSaleFrom_Ret_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtSaleFrom_Ret_AutoCompleteExtender" runat="server"
                                                CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListStore_Ret" ServicePath=""
                                                TargetControlID="txtSaleFrom_Ret" UseContextKey="True">
                                            </asp:AutoCompleteExtender>
                                        </td>
                                        <td class="style52">
                                            <asp:TextBox ID="txtSlFr_Ret" runat="server" CssClass="txtColor" ReadOnly="True"
                                                TabIndex="5"></asp:TextBox>&#160;&#160;
                                        </td>
                                        <td class="style49">
                                            Total Amount
                                        </td>
                                        <td class="style6">
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTotAmt_Ret" runat="server" ReadOnly="True" TabIndex="35">0.00</asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%; font-style: normal;">
                                    <tr>
                                        <td class="style35">
                                            <asp:Label ID="lblPSID_Ret" runat="server"></asp:Label>
                                        </td>
                                        <td class="style6">
                                            :
                                        </td>
                                        <td class="style12">
                                            <asp:TextBox ID="txtPSNM_Ret" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                TabIndex="9" Width="300px" OnTextChanged="txtPSNM_Ret_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtPSNM_Ret_AutoCompleteExtender" runat="server" CompletionInterval="10"
                                                CompletionSetCount="3" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1"
                                                ServiceMethod="GetCompletionListParty_Ret" ServicePath="" TargetControlID="txtPSNM_Ret"
                                                UseContextKey="True">
                                            </asp:AutoCompleteExtender>
                                        </td>
                                        <td class="style52">
                                            <asp:TextBox ID="txtPID_Ret" runat="server" ReadOnly="True" TabIndex="10"></asp:TextBox>
                                        </td>
                                        <td class="style49">
                                            Discount Amount
                                        </td>
                                        <td class="style2">
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtGrossDisAmt_Ret" runat="server" AutoPostBack="True" Style="text-align: left"
                                                TabIndex="36" OnTextChanged="txtGrossDisAmt_Ret_TextChanged">0.00</asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%; font-style: normal;">
                                    <tr>
                                        <td class="style74" style="text-align: right">
                                            Remarks
                                        </td>
                                        <td class="style15">
                                            :
                                        </td>
                                        <td class="style53">
                                            <asp:TextBox ID="txtRemarks_Ret" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                TabIndex="11" Width="452px" OnTextChanged="txtRemarks_Ret_TextChanged"></asp:TextBox>
                                        </td>
                                        <td class="style64">
                                            Net Amount
                                        </td>
                                        <td class="style15">
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNetAmt_Ret" runat="server" ReadOnly="True" TabIndex="37">0.00</asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style74">
                                            <asp:Label ID="lblCatID_Ret" runat="server" Visible="False"></asp:Label>
                                        </td>
                                        <td class="style15">
                                            &#160;
                                        </td>
                                        <td class="style53">
                                            <asp:Label ID="lblTransSL_Ret" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblSaleFrom_Ret" runat="server" Font-Bold="False" Font-Italic="True"
                                                ForeColor="#CC3300" Visible="False"></asp:Label>&#160;&#160;
                                            <asp:Label ID="lblPartyID_Ret" runat="server" Font-Bold="False" Font-Italic="True"
                                                ForeColor="#CC3300" Visible="False"></asp:Label>
                                        </td>
                                        <td class="style64">
                                            <asp:Button ID="btnComplete_Ret" runat="server" Font-Bold="True" Style="text-align: left"
                                                TabIndex="38" Text="Complete" OnClick="btnComplete_Ret_Click" />
                                        </td>
                                        <td class="style15">
                                            &#160;
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Button ID="btnPrint_Ret" runat="server" Font-Bold="True" TabIndex="39" Text="PRINT"
                                                Width="70px" OnClick="btnPrint_Ret_Click" />&#160;
                                        </td>
                                    </tr>
                                </table>
                                <div id="Div4">
                                    <asp:GridView ID="gvDetail_Ret" runat="server" AutoGenerateColumns="False" CssClass="Gridview"
                                        Font-Italic="False" ShowFooter="True" Width="100%" OnRowCancelingEdit="gvDetail_Ret_RowCancelingEdit"
                                        OnRowCommand="gvDetail_Ret_RowCommand" OnRowDataBound="gvDetail_Ret_RowDataBound"
                                        OnRowDeleting="gvDetail_Ret_RowDeleting" OnRowEditing="gvDetail_Ret_RowEditing"
                                        OnRowUpdating="gvDetail_Ret_RowUpdating">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Item Particulars">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtItemNMEdit_Ret" runat="server" TabIndex="24" Text='<%#Eval("ITEMNM") %>'
                                                        Width="180px" AutoPostBack="True" Font-Names="Calibri" OnTextChanged="txtItemNMEdit_Ret_TextChanged" />
                                                    <asp:AutoCompleteExtender ID="txtItemNMEdit_Ret_AutoCompleteExtender" runat="server"
                                                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListEdit"
                                                        ServicePath="" TargetControlID="txtItemNMEdit_Ret" UseContextKey="True">
                                                    </asp:AutoCompleteExtender>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtItemNM_Ret" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                        OnTextChanged="txtItemNM_Ret_TextChanged" TabIndex="12" Width="180px" Font-Names="Calibri" />
                                                    <asp:AutoCompleteExtender ID="txtItemNM_Ret_AutoCompleteExtender" runat="server"
                                                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionList"
                                                        ServicePath="" TargetControlID="txtItemNM_Ret" UseContextKey="True">
                                                    </asp:AutoCompleteExtender>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemNM_Ret" runat="server" Style="text-align: left" Text='<%# Eval("ITEMNM") %>'
                                                        Width="180px" /></ItemTemplate>
                                                <ControlStyle Width="180px" />
                                                <FooterStyle HorizontalAlign="Left" Width="180px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item ID">
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblItemIDEdit_Ret" runat="server" Style="text-align: center" Text='<%# Eval("ITEMID") %>'
                                                        Width="50px" /></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtItID_Ret" runat="server" CssClass="txtColor" TabIndex="13" Width="50px"
                                                        Font-Names="Calibri" Font-Size="12px"></asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemID_Ret" runat="server" Style="text-align: center" Text='<%# Eval("ITEMID") %>'
                                                        Width="50px" /></ItemTemplate>
                                                <ControlStyle Width="50px" />
                                                <FooterStyle HorizontalAlign="Center" Width="50px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type">
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddltypeEdit_Ret" runat="server" Width="67px" AutoPostBack="True"
                                                        TabIndex="25" Font-Names="Calibri" OnSelectedIndexChanged="ddltypeEdit_Ret_SelectedIndexChanged">
                                                        <asp:ListItem Value="CARTON">CARTON</asp:ListItem>
                                                        <asp:ListItem>PIECES</asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlType_Ret" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                        TabIndex="14" Width="67px" Font-Names="Calibri" OnSelectedIndexChanged="ddlType_Ret_SelectedIndexChanged">
                                                        <asp:ListItem Value="CARTON">CARTON</asp:ListItem>
                                                        <asp:ListItem>PIECES</asp:ListItem>
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType_Ret" runat="server" Style="text-align: center" Width="67px"
                                                        Text='<%# Eval("UNITTP") %>'></asp:Label></ItemTemplate>
                                                <ControlStyle Width="67px" />
                                                <FooterStyle HorizontalAlign="Left" Width="67px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="67px" />
                                                <ItemStyle HorizontalAlign="Left" Width="67px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Set">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCPQTYEdit_Ret" runat="server" ReadOnly="True" Style="text-align: right"
                                                        TabIndex="201" Text='<%#Eval("CPQTY") %>' Width="35px" Font-Names="Calibri"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtCPQTY_Ret" runat="server" CssClass="txtColor" ReadOnly="True"
                                                        Style="text-align: right" TabIndex="200" Width="35px" Font-Names="Calibri">.00</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCPQTY_Ret" runat="server" Style="text-align: right" Text='<%# Eval("CPQTY") %>'></asp:Label></ItemTemplate>
                                                <ControlStyle Width="35px" />
                                                <FooterStyle HorizontalAlign="Left" Width="35px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                                <ItemStyle HorizontalAlign="Left" Width="35px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Carton">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCQtyEdit_Ret" runat="server" Style="text-align: right" TabIndex="27"
                                                        Text='<%#Eval("CQTY") %>' Width="40px" AutoPostBack="True" OnTextChanged="txtCQtyEdit_Ret_TextChanged"
                                                        Font-Names="Calibri"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtCQty_Ret" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                        Style="text-align: right" TabIndex="16" Width="40px" Font-Names="Calibri" OnTextChanged="txtCQty_Ret_TextChanged">.00</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCQty_Ret" runat="server" Style="text-align: center" Text='<%# Eval("CQTY") %>'></asp:Label></ItemTemplate>
                                                <ControlStyle Width="40px" />
                                                <FooterStyle HorizontalAlign="Left" Width="40px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                                <ItemStyle HorizontalAlign="Left" Width="40px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pieces">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPQtyEdit_Ret" runat="server" Style="text-align: right" TabIndex="28"
                                                        Text='<%#Eval("PQTY") %>' Width="50px" AutoPostBack="True" OnTextChanged="txtPQtyEdit_Ret_TextChanged"
                                                        Font-Names="Calibri"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtPQty_Ret" runat="server" AutoPostBack="True" CssClass="txtColor"
                                                        Style="text-align: right" TabIndex="17" Width="50px" Font-Names="Calibri" OnTextChanged="txtPQty_Ret_TextChanged">.00</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPQty" runat="server" Style="text-align: right" Text='<%#Eval("PQTY") %>'
                                                        Width="50px"></asp:Label></ItemTemplate>
                                                <ControlStyle Width="50px" />
                                                <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtQtyEdit_Ret" runat="server" Style="text-align: right" TabIndex="29"
                                                        Text='<%#Eval("QTY") %>' Width="70px" Font-Names="Calibri"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtQty_Ret" runat="server" CssClass="txtColor" Style="text-align: right"
                                                        TabIndex="18" Width="70px" Font-Names="Calibri">.00</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty_Ret" runat="server" Style="text-align: right" Text='<%# Eval("QTY") %>'
                                                        Width="70px"></asp:Label></ItemTemplate>
                                                <ControlStyle Width="70px" />
                                                <FooterStyle HorizontalAlign="Right" Width="70px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtRateEdit_Ret" runat="server" Style="text-align: right" TabIndex="30"
                                                        Text='<%#Eval("RATE") %>' Width="60px" AutoPostBack="True" Font-Names="Calibri"
                                                        OnTextChanged="txtRateEdit_Ret_TextChanged"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtRate_Ret" runat="server" CssClass="txtColor" Style="text-align: right"
                                                        TabIndex="19" Width="60px" AutoPostBack="True" Font-Names="Calibri" OnTextChanged="txtRate_Ret_TextChanged">.00</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRate_Ret" runat="server" Style="text-align: right" Text='<%# Eval("RATE") %>'
                                                        Width="60px"></asp:Label></ItemTemplate>
                                                <ControlStyle Width="60px" />
                                                <FooterStyle HorizontalAlign="Right" Width="60px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtAmountEdit_Ret" runat="server" Style="text-align: right" Text='<%#Eval("AMOUNT") %>'
                                                        Width="90px" TabIndex="202" Font-Names="Calibri"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtAmount_Ret" runat="server" CssClass="txtColor" Style="text-align: right"
                                                        TabIndex="103" Width="90px" Font-Names="Calibri">.00</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount_Ret" runat="server" Style="text-align: right" Text='<%# Eval("AMOUNT") %>'></asp:Label></ItemTemplate>
                                                <ControlStyle Width="90px" />
                                                <FooterStyle Width="90px" />
                                                <HeaderStyle Width="90px" />
                                                <ItemStyle Width="90px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discount Rate">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDisRtEdit_Ret" runat="server" Style="text-align: right" TabIndex="31"
                                                        Text='<%#Eval("DISCRT") %>' Width="60px" AutoPostBack="True" Font-Names="Calibri"
                                                        OnTextChanged="txtDisRtEdit_Ret_TextChanged"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtDisRt_Ret" runat="server" CssClass="txtColor" Style="text-align: right"
                                                        TabIndex="20" Width="60px" AutoPostBack="True" Font-Names="Calibri" OnTextChanged="txtDisRt_Ret_TextChanged">0</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDisRt_Ret" runat="server" Style="text-align: right" Text='<%# Eval("DISCRT") %>'
                                                        Width="60px"></asp:Label></ItemTemplate>
                                                <ControlStyle Width="60px" />
                                                <FooterStyle HorizontalAlign="Right" Width="60px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discount Amount">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDisAmtEdit_Ret" runat="server" Style="text-align: right" TabIndex="32"
                                                        Text='<%#Eval("DISCAMT") %>' Width="60px" AutoPostBack="True" Font-Names="Calibri"
                                                        OnTextChanged="txtDisAmtEdit_Ret_TextChanged"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtDisAmt_Ret" runat="server" CssClass="txtColor" Style="text-align: right"
                                                        TabIndex="21" Width="60px" AutoPostBack="True" Font-Names="Calibri" OnTextChanged="txtDisAmt_Ret_TextChanged">.00</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDisAmt_Ret" runat="server" Style="text-align: right" Text='<%# Eval("DISCAMT") %>'
                                                        Width="60px"></asp:Label></ItemTemplate>
                                                <ControlStyle Width="60px" />
                                                <FooterStyle HorizontalAlign="Right" Width="60px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net Amount">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtNetAmtEdit_Ret" runat="server" Style="text-align: right" TabIndex="203"
                                                        Text='<%#Eval("NETAMT") %>' Width="80px" AutoPostBack="True" Font-Names="Calibri"></asp:TextBox></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtNetAmt_Ret" runat="server" CssClass="txtColor" Style="text-align: right"
                                                        TabIndex="104" Width="100px" AutoPostBack="True" Font-Names="Calibri">.00</asp:TextBox></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNetAmt_Ret" runat="server" Style="text-align: right" Text='<%# Eval("NETAMT") %>'
                                                        Width="100px"></asp:Label></ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                <FooterStyle HorizontalAlign="Right" Width="100px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SL" Visible="False">
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblTransSLEdit_Ret" runat="server" Text='<%# Eval("TRANSSL") %>'></asp:Label></EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTransSL_Ret" runat="server" Text='<%# Eval("TRANSSL") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="imgbtnUpdate_Ret" runat="server" CommandName="Update" Height="20px"
                                                        ImageUrl="~/Images/update.jpg" TabIndex="33" ToolTip="Update" Width="20px" /><asp:ImageButton
                                                            ID="imgbtnCancel_Ret" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cancel.jpg"
                                                            TabIndex="34" ToolTip="Cancel" Width="20px" /></EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="imgbtnAdd_Ret" runat="server" CommandName="SaveCon" CssClass="txtColor"
                                                        Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="22" ToolTip="Save &amp; Continue"
                                                        ValidationGroup="validaiton" Width="20px" /><asp:ImageButton ID="imgBtnComp_Ret"
                                                            runat="server" CommandName="Complete" CssClass="txtColor" Height="30px" ImageUrl="~/Images/checkmark.jpg"
                                                            TabIndex="23" ToolTip="Complete" ValidationGroup="validaiton" Width="20px" /></FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" Height="20px"
                                                        ImageUrl="~/Images/Edit.jpg" TabIndex="10" ToolTip="Edit" Width="20px" /><asp:ImageButton
                                                            ID="imgbtnDelete_Ret" runat="server" CommandName="Delete" Height="20px" ImageUrl="~/Images/delete.jpg"
                                                            TabIndex="11" OnClientClick="return confMSG()" ToolTip="Delete" Width="20px" /></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999966" />
                                        <HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White" Font-Names="Calibri"
                                            Font-Size="14px" />
                                        <RowStyle Font-Names="Calibri" />
                                    </asp:GridView>
                                    <table style="width: 100%; font-style: normal;">
                                        <tr>
                                            <td class="style28">
                                                <asp:Label ID="lblGridMsg_Ret" runat="server" Font-Bold="False" ForeColor="#CC3300"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td class="style68" style="text-align: right">
                                                Total
                                            </td>
                                            <td class="style31">
                                                :
                                            </td>
                                            <td class="style66">
                                                <asp:TextBox ID="txtTAmount_Ret" runat="server" ReadOnly="True" Style="text-align: right"
                                                    TabIndex="106" Width="90px">.00</asp:TextBox>
                                            </td>
                                            <td class="style65">
                                                &#160;
                                            </td>
                                            <td class="style38">
                                                <asp:TextBox ID="txtTDisAmount_Ret" runat="server" Width="60px" ReadOnly="True" Style="text-align: right">.00</asp:TextBox>
                                            </td>
                                            <td class="style67">
                                                <asp:TextBox ID="txtTotal_Ret" runat="server" ReadOnly="True" Style="text-align: right"
                                                    TabIndex="106" Width="100px">.00</asp:TextBox>
                                            </td>
                                            <td>
                                                &#160;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
