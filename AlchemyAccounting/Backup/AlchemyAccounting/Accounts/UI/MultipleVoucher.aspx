<%@ Page Title="Multiple Voucher Entry" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="MultipleVoucher.aspx.cs" Inherits="AlchemyAccounting.Accounts.UI.MultipleVoucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="shortcut icon" href="../../Images/favicon.ico" />
    <link href="../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#txtTransDate").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+0" });

        });

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
        }
        #grid
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
        .style1
        {
            width: 1px;
        }
        .style2
        {
            width: 127px;
        }
        .style3
        {
            width: 30px;
        }
        .style4
        {
            width: 88px;
        }
        .style5
        {
            width: 3px;
            font-weight: 700;
        }
        .style6
        {
            width: 266px;
        }
        .style7
        {
            width: 73px;
        }
        .style8
        {
            width: 4px;
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
        .style9
        {
            width: 667px;
        }
        .style10
        {
            width: 635px;
        }
        .ui-accordion
        {
            text-align: left;
        }
        .style11
        {
            width: 193px;
            text-align: right;
            font-family: Calibri;
        }
        .style12
        {
            width: 193px;
            text-align: right;
            font-family: Calibri;
            font-size: medium;
        }
        .style13
        {
            font-size: small;
        }
        .style14
        {
            width: 749px;
        }
        .fontfiltering
        {
            font-family: Calibri;
            font-size: 10px;
            list-style: none;
            margin-left: -40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center">
            Multiple Transaction Entry</h1>
    </div>
    <div id="entry">
        <div id="toolbar">
            <table style="width: 100%;">
                <tr>
                    <td class="style10">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
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
                    <td style="text-align: right" class="style10">
                        <asp:Button ID="btnEdit" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"
                            Text="EDIT" Width="80px" OnClick="btnEdit_Click" />
                    </td>
                    <td class="style8">
                        &nbsp;
                    </td>
                    <td class="style9">
                        <asp:Button ID="btnPrint" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"
                            Text="PRINT" Width="80px" Visible="False" OnClick="btnPrint_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style10">
                        <asp:Label ID="lblDebitCD" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblCreditCD" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblCostPoolID" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td class="style8">
                        &nbsp;
                    </td>
                    <td class="style9">
                        <asp:Label ID="lblTransforEdit" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblTransmodeEdit" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%; font-family: Calibri; font-size: small;">
            <tr>
                <td style="text-align: right">
                    <strong>Voucher Type</strong>
                </td>
                <td class="style1">
                    <strong>:</strong>
                </td>
                <td class="style2">
                    <strong>
                        <asp:DropDownList ID="ddlTransType" runat="server" Width="80px" OnSelectedIndexChanged="ddlTransType_SelectedIndexChanged"
                            TabIndex="1" AutoPostBack="True" Style="font-family: Calibri" CssClass="txtColor">
                            <asp:ListItem Value="MPAY">PAYMENT</asp:ListItem>
                            <asp:ListItem Value="MREC">RECEIPT</asp:ListItem>
                            <asp:ListItem Value="JOUR">JOURNAL</asp:ListItem>
                            <asp:ListItem Value="CONT">CONTRA</asp:ListItem>
                        </asp:DropDownList>
                    </strong>
                </td>
                <td class="style3">
                    &nbsp;
                </td>
                <td class="style4" style="text-align: right">
                    <strong>Voucher Date</strong>
                </td>
                <td class="style5">
                    :
                </td>
                <td class="style6">
                    <asp:TextBox ID="txtTransDate" runat="server" TabIndex="1" ClientIDMode="Static"
                        OnTextChanged="txtTransDate_TextChanged" AutoPostBack="True" CssClass="txtColor"
                        Width="80px" Font-Names="Calibri" Font-Size="12px"></asp:TextBox>
                    &nbsp;
                    <asp:TextBox ID="txtTransYear" runat="server" TabIndex="200" CssClass="txtColor"
                        ReadOnly="True" Width="80px" Font-Names="Calibri" Font-Size="12px"></asp:TextBox>
                    &nbsp;
                </td>
                <td class="style7">
                    <strong>Voucher No</strong>
                </td>
                <td class="style8">
                    <strong>:</strong>
                </td>
                <td>
                    <asp:TextBox ID="txtVouchNo" runat="server" TabIndex="300" Style="font-family: Calibri"
                        Width="80px" CssClass="txtColor" Font-Names="Calibri" Font-Size="12px" ReadOnly="True"></asp:TextBox>
                    <asp:DropDownList ID="ddlVouch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlVouch_SelectedIndexChanged"
                        TabIndex="3" Visible="False">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblVCount" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td class="style2">
                    <asp:Label ID="lblSLCount" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="style3">
                    &nbsp;
                </td>
                <td class="style4">
                    &nbsp;
                </td>
                <td class="style5">
                    &nbsp;
                </td>
                <td class="style6">
                    <asp:Label ID="lblError" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                </td>
                <td class="style7">
                    &nbsp;
                </td>
                <td class="style8">
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
        <div id="grid">
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
            <asp:GridView ID="gvDetails" runat="server" BackColor="White" BorderColor="White"
                BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None"
                Width="100%" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gvDetails_RowDataBound"
                OnRowCommand="gvDetails_RowCommand" OnRowEditing="gvDetails_RowEditing" OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                OnRowUpdating="gvDetails_RowUpdating" OnRowDeleting="gvDetails_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="SL">
                        <EditItemTemplate>
                            <asp:Label ID="lblSLEdit" runat="server" Text='<%# Eval("SERIALNO") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSL" runat="server" Text='<%# Eval("SERIALNO") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblDebit" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("DEBITNM") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDebitedEdit" runat="server" Font-Names="Calibri" CssClass="txtColor"
                                Font-Size="10px" TabIndex="30" Width="98%" Text='<%# Eval("DEBITNM") %>' AutoPostBack="True"
                                OnTextChanged="txtDebitedEdit_TextChanged"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txtDebitedEdit_AutoCompleteExtender" runat="server"
                                TargetControlID="txtDebitedEdit" UseContextKey="True" MinimumPrefixLength="1"
                                CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" ServiceMethod="GetCompletionListDebit">
                            </asp:AutoCompleteExtender>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtDebited" runat="server" Font-Names="Calibri" Font-Size="10px" CssClass="txtColor"
                                TabIndex="4" Width="98%" AutoPostBack="True" OnTextChanged="txtDebited_TextChanged"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txtDebited_AutoCompleteExtender" runat="server" TargetControlID="txtDebited"
                                UseContextKey="True" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true"
                                CompletionSetCount="3" ServiceMethod="GetCompletionListDebit">
                            </asp:AutoCompleteExtender>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="18%" />
                        <ItemStyle HorizontalAlign="Left" Width="18%" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblCredit" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("CREDITNM") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCreditedEdit" runat="server" Font-Names="Calibri" CssClass="txtColor"
                                Font-Size="10px" TabIndex="31" Width="98%" Text='<%# Eval("CREDITNM") %>' AutoPostBack="True"
                                OnTextChanged="txtCreditedEdit_TextChanged"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txtCreditedEdit_AutoCompleteExtender" runat="server"
                                TargetControlID="txtCreditedEdit" UseContextKey="True" MinimumPrefixLength="1"
                                CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" ServiceMethod="GetCompletionListCredit">
                            </asp:AutoCompleteExtender>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCredited" runat="server" Font-Names="Calibri" Font-Size="10px"
                                TabIndex="5" Width="98%" AutoPostBack="True" CssClass="txtColor" OnTextChanged="txtCredited_TextChanged"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txtCredited_AutoCompleteExtender" runat="server" TargetControlID="txtCredited"
                                UseContextKey="True" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true"
                                CompletionSetCount="3" ServiceMethod="GetCompletionListCredit">
                            </asp:AutoCompleteExtender>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="18%" />
                        <ItemStyle HorizontalAlign="Left" Width="18%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Trans For" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblTransFor" runat="server" Text='<%# Eval("TRANSFOR") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlTransforEdit" runat="server" Font-Names="Calibri" CssClass="txtColor"
                                Font-Size="10px" TabIndex="32" Width="98%" OnSelectedIndexChanged="ddlTransforEdit_SelectedIndexChanged"
                                AutoPostBack="True">
                                <asp:ListItem>PROJECT</asp:ListItem>
                                <asp:ListItem>OFFICIAL</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlTransfor" runat="server" Font-Names="Calibri" Font-Size="10px"
                                TabIndex="6" Width="98%" AutoPostBack="True" CssClass="txtColor" OnSelectedIndexChanged="ddlTransfor_SelectedIndexChanged">
                                <asp:ListItem>PROJECT</asp:ListItem>
                                <asp:ListItem>OFFICIAL</asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Branch Name">
                        <ItemTemplate>
                            <asp:Label ID="lblProject" runat="server" Text='<%# Eval("COSTPNM") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCostPNMEdit" runat="server" Font-Names="Calibri" CssClass="txtColor"
                                Font-Size="10px" TabIndex="33" Width="98%" Text='<%# Eval("COSTPNM") %>' AutoPostBack="True"
                                OnTextChanged="txtCostPNMEdit_TextChanged"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txtCostPNMEdit_AutoCompleteExtender" runat="server"
                                CompletionListCssClass="fontfiltering" TargetControlID="txtCostPNMEdit" UseContextKey="True"
                                MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                                ServiceMethod="GetCompletionListCostPool">
                            </asp:AutoCompleteExtender>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCostPNM" runat="server" Font-Names="Calibri" Font-Size="10px"
                                TabIndex="7" Width="98%" AutoPostBack="True" CssClass="txtColor" 
                                OnTextChanged="txtCostPNM_TextChanged"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txtCostPNM_AutoCompleteExtender" runat="server" CompletionListCssClass="fontfiltering"
                                TargetControlID="txtCostPNM" UseContextKey="True" MinimumPrefixLength="1" CompletionInterval="10"
                                EnableCaching="true" CompletionSetCount="12" ServiceMethod="GetCompletionListCostPool">
                            </asp:AutoCompleteExtender>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transaction Mode">
                        <ItemTemplate>
                            <asp:Label ID="lblTransMode" runat="server" Text='<%# Eval("TRANSMODE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlTransModeEdit" runat="server" Font-Names="Calibri" CssClass="txtColor"
                                Font-Size="10px" OnSelectedIndexChanged="ddlTransModeEdit_SelectedIndexChanged"
                                TabIndex="34" Width="98%" AutoPostBack="True">
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
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlTransMode" runat="server" Font-Names="Calibri" Font-Size="10px"
                                OnSelectedIndexChanged="ddlTransMode_SelectedIndexChanged" TabIndex="8" Width="98%"
                                AutoPostBack="True" CssClass="txtColor">
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
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="6%" />
                        <ItemStyle HorizontalAlign="Left" Width="6%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cheque No">
                        <ItemTemplate>
                            <asp:Label ID="lblChequeNo" runat="server" Text='<%# Eval("CHEQUENO") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtChequeEdit" runat="server" Font-Names="Calibri" CssClass="txtColor"
                                Font-Size="10px" TabIndex="35" Width="98%" Text='<%# Eval("CHEQUENO") %>' Enabled="False"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCheque" runat="server" Font-Names="Calibri" Font-Size="10px"
                                TabIndex="9" Width="98%" CssClass="txtColor" Enabled="False"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cheque Date">
                        <ItemTemplate>
                            <asp:Label ID="lblChequeDT" runat="server" Text='<%# Eval("CHEQUEDT") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <strong>
                                <asp:TextBox ID="txtChequeDateEdit" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                    Font-Size="8px" TabIndex="36" Width="98%" Text='<%# Eval("CHEQUEDT") %>' Enabled="False"
                                    AutoPostBack="True" OnTextChanged="txtChequeDateEdit_TextChanged"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtChequeDateEdit"
                                    PopupButtonID="txtChequeDateEdit" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </strong>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <strong>
                                <asp:TextBox ID="txtChequeDate" runat="server" Font-Names="Calibri" Font-Size="10px"
                                    TabIndex="10" Width="98%" CssClass="txtColor" AutoPostBack="True" ClientIDMode="Static"
                                    OnTextChanged="txtChequeDate_TextChanged" Enabled="False"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtChequeDate"
                                    PopupButtonID="txtChequeDate" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </strong>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="6%" />
                        <ItemStyle HorizontalAlign="Left" Width="6%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRemarksEdit" runat="server" Font-Names="Calibri" CssClass="txtColor"
                                Font-Size="10px" TabIndex="37" Width="98%" Text='<%# Eval("REMARKS") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" Font-Names="Calibri" Font-Size="10px"
                                TabIndex="11" Width="98%" CssClass="txtColor"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAmountEdit" runat="server" Font-Names="Calibri" CssClass="txtColor"
                                Font-Size="10px" TabIndex="38" Width="98%" Text='<%# Eval("AMOUNT") %>' Style="text-align: right"
                                AutoPostBack="True" OnTextChanged="txtAmountEdit_TextChanged"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" Font-Names="Calibri" Font-Size="10px"
                                TabIndex="12" Width="98%" CssClass="txtColor" Style="text-align: right"
                                AutoPostBack="True" OnTextChanged="txtAmount_TextChanged"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" Height="20px"
                                ImageUrl="~/Images/Edit.jpg" TabIndex="20" ToolTip="Edit" Width="20px" />
                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" Height="20px"
                                ImageUrl="~/Images/delete.jpg" OnClientClick="return confMSG()" TabIndex="21"
                                ToolTip="Delete" Width="21px" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" Height="20px"
                                ImageUrl="~/Images/update.jpg" TabIndex="39" ToolTip="Update" Width="20px" />
                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" Height="20px"
                                ImageUrl="~/Images/Cancel.jpg" TabIndex="40" ToolTip="Cancel" Width="20px" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="imgbtnAdd" runat="server" CommandName="SaveCon" CssClass="txtColor"
                                Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="13" ToolTip="Save &amp; Continue"
                                ValidationGroup="validaiton" Width="15px" />
                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Complete" CssClass="txtColor"
                                Height="30px" ImageUrl="~/Images/checkmark.jpg" TabIndex="14" ToolTip="Complete"
                                ValidationGroup="validaiton" Width="15px" />
                            <asp:ImageButton ID="ImagebtnPPrint" runat="server" CommandName="SavePrint" CssClass="txtColor"
                                Height="30px" ImageUrl="~/Images/print.gif" TabIndex="15" ToolTip="Save &amp; Print"
                                ValidationGroup="validaiton" Width="15px" />
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" Font-Names="Calibri"
                    Font-Size="12px" />
                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#DEDFDE" ForeColor="Black" Font-Names="Calibri" Font-Size="10px" />
                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#594B9C" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#33276A" />
            </asp:GridView>
            <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
        </div>
        <table style="width: 100%;">
            <tr>
                <td class="style12">
                    <strong style="font-size: small">In Words :</strong>
                </td>
                <td class="style14">
                    <asp:TextBox ID="txtCumInWords" runat="server" ReadOnly="True" Width="750px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style11">
                    <strong><span class="style13">In Words (Total) :</span></strong>
                </td>
                <td class="style14">
                    <asp:TextBox ID="txtTotInWords" runat="server" ReadOnly="True" Width="596px"></asp:TextBox>
                    <asp:TextBox ID="txtTotAmount" runat="server" ReadOnly="True" Width="145px" Style="text-align: right;"
                        Font-Bold="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style11">
                    &nbsp;
                </td>
                <td class="style14">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
