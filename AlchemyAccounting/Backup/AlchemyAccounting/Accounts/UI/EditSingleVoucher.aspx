<%@ Page Title="Edit Single Voucher" Language="C#" AutoEventWireup="true" CodeBehind="EditSingleVoucher.aspx.cs"
    Inherits="AlchemyAccounting.Accounts.UI.EditSingleVoucher" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Single Voucher</title>
    <link rel="shortcut icon" href="../../Images/favicon.ico" />
    <link href="../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtEdDate").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+0" });
            $("input[id^=gvDetails_txtCqDt]").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+0" });
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
        .style1
        {
            width: 5px;
        }
        .style2
        {
        }
        .style3
        {
            width: 321px;
        }
        .style4
        {
            width: 163px;
        }
        .style5
        {
            width: 1px;
        }
        #cont
        {
            float: left;
            width: 80%;
            margin: 0% 10% 0% 10%;
        }
        .style6
        {
            width: 157px;
        }
        .Gridview
        {
            font-family: Verdana;
            font-size: 10pt;
            font-weight: normal;
            color: black;
        }
        #ddlEditTransType
        {
            width: 206px;
        }
        .style7
        {
            width: 130px;
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
        .fontalloc
        {
            font-family: Calibri;
            font-size: 11px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td class="style3">
                    <asp:Label ID="lblDebitCD" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td class="style7">
                    &nbsp;
                </td>
                <td class="style4">
                    &nbsp;
                </td>
                <td class="style5">
                    &nbsp;
                </td>
                <td class="style6">
                    <asp:Label ID="lblTransTP" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCreditCD" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3" style="text-align: right">
                    <b>Date</b>
                </td>
                <td class="style1">
                    <b>:</b>
                </td>
                <td class="style7">
                    <asp:TextBox ID="txtEdDate" runat="server" ClientIDMode="Static" TabIndex="1" CssClass="txtColor"
                        AutoPostBack="True" OnTextChanged="txtEdDate_TextChanged"></asp:TextBox>
                </td>
                <td class="style4" style="text-align: right">
                    <strong>Transaction Type</strong>
                </td>
                <td class="style5">
                    <strong>:</strong>
                </td>
                <td class="style6">
                    <strong>
                        <asp:DropDownList ID="ddlEditTransType" runat="server" Width="150px" OnSelectedIndexChanged="ddlEditTransType_SelectedIndexChanged"
                            TabIndex="2" AutoPostBack="True" CssClass="txtColor">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem Value="MREC">RECEIPT</asp:ListItem>
                            <asp:ListItem Value="MPAY">PAYMENT</asp:ListItem>
                            <asp:ListItem Value="JOUR">JOURNAL</asp:ListItem>
                            <asp:ListItem Value="CONT">CONTRA</asp:ListItem>
                        </asp:DropDownList>
                    </strong>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Font-Bold="True" Font-Italic="True" OnClick="btnSearch_Click"
                        Text="Search" TabIndex="3" CssClass="txtColor" />
                </td>
            </tr>
            <tr>
                <td class="style3" style="text-align: right">
                    <%--<strong>Account Head</strong>--%>
                    <asp:Label ID="lblTransMode" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblTransFor" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="style1">
                    <%--<strong>:</strong>--%>
                </td>
                <td class="style2" colspan="2">
                    <%--<asp:TextBox ID="txtAcHdNm" runat="server" Width="350px" AutoPostBack="True" 
                        ontextchanged="txtAcHdNm_TextChanged" TabIndex="4" Font-Size="12px" 
                        style="font-family: Calibri"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtAcHdNm_AutoCompleteExtender" runat="server"
                                            TargetControlID="txtAcHdNm" UseContextKey="True" MinimumPrefixLength="3" CompletionInterval="10" EnableCaching="true" 
                                            CompletionSetCount="3" ServiceMethod="GetCompletionListHdSR">
                                        </asp:AutoCompleteExtender>--%>
                    <asp:Label ID="lblCostpoolID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblCatNM" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="style5">
                    &nbsp;
                </td>
                <td class="style6">
                    &nbsp;
                </td>
                <td>
                    <%--<asp:Label ID="lblSearchedCD" runat="server"></asp:Label>--%>
                    <strong>
                        <asp:Label ID="lblErrMsg" runat="server" ForeColor="#FF3300" Visible="False"></asp:Label>
                    </strong>
                </td>
            </tr>
        </table>
        <div style="margin: 0% 5% 0% 0%;">
            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" CssClass="Gridview"
                HeaderStyle-BackColor="#61A6F8" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White"
                OnRowCancelingEdit="gvDetails_RowCancelingEdit" OnRowDeleting="gvDetails_RowDeleting"
                OnRowEditing="gvDetails_RowEditing" OnRowUpdating="gvDetails_RowUpdating" OnRowCommand="gvDetails_RowCommand"
                OnRowDataBound="gvDetails_RowDataBound" Width="100%" Font-Names="Calibri" Font-Size="11px">
                <Columns>
                    <asp:TemplateField HeaderText="Print">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnPrint" runat="server" CommandName="print" Height="26px" ImageUrl="~/Images/print.gif"
                                Width="43px" CssClass="txtColor" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="btnPrint" runat="server" Height="26px" ImageUrl="~/Images/print.gif"
                                Width="43px" CssClass="txtColor" />
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="3%" />
                        <ItemStyle HorizontalAlign="Center" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Trans No">
                        <ItemTemplate>
                            <asp:Label ID="lblTransNo" runat="server" Text='<%#Eval("TRANSNO") %>' Width="98%" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblTransNo" runat="server" Text='<%#Eval("TRANSNO") %>' CssClass="txtColor"
                                Width="98%" />
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="3%" />
                        <ItemStyle HorizontalAlign="Center" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transaction For" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblTransFor" runat="server" Text='<%#Eval("TRANSFOR") %>' Width="98%" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlTransFor" runat="server" Width="98%" TabIndex="5" CssClass="txtColor fontalloc" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlTransFor_SelectedIndexChanged">
                                <asp:ListItem>OFFICIAL</asp:ListItem>
                                <asp:ListItem>OTHERS</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Branch Name">
                        <ItemTemplate>
                            <asp:Label ID="lblCostPoolNM" runat="server" Text='<%#Eval("COSTPNM") %>' Width="98%" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCostPoolNM" runat="server" Text='<%#Eval("COSTPNM") %>' Width="98%"
                                TabIndex="6" AutoPostBack="True" OnTextChanged="txtCostPoolNM_TextChanged" CssClass="txtColor fontalloc" />
                            <asp:AutoCompleteExtender ID="txtCostPoolNM_AutoCompleteExtender" runat="server"
                                TargetControlID="txtCostPoolNM" UseContextKey="True" MinimumPrefixLength="1"
                                CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" ServiceMethod="GetCompletionListCostPool">
                            </asp:AutoCompleteExtender>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCostPoolID" runat="server" Text='<%# Eval("COSTPID") %>' Visible="true"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblCostPoolIDEdit" runat="server" Text='<%# Eval("COSTPID") %>' Visible="true"></asp:Label>
                        </EditItemTemplate>
                        <HeaderStyle Width="10%" />
                        <ItemStyle Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transaction Mode">
                        <ItemTemplate>
                            <asp:Label ID="lblTransMode" runat="server" Text='<%#Eval("TRANSMODE") %>' Width="98%" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlTransMode" runat="server" TabIndex="7" OnSelectedIndexChanged="ddlTransMode_SelectedIndexChanged"
                                AutoPostBack="true" CssClass="txtColor fontalloc" Width="98%">
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
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Debit">
                        <ItemTemplate>
                            <asp:Label ID="lblDBCD" runat="server" Text='<%#Eval("DEBITCD") %>' Width="98%" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDbCd" runat="server" Text='<%#Eval("DEBITCD") %>' Width="98%"
                                TabIndex="8" AutoPostBack="True" OnTextChanged="txtDbCd_TextChanged" CssClass="txtColor fontalloc" />
                            <asp:AutoCompleteExtender ID="txtDbCd_AutoCompleteExtender" runat="server" TargetControlID="txtDbCd"
                                UseContextKey="True" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true"
                                CompletionSetCount="3" ServiceMethod="GetCompletionListDebit">
                            </asp:AutoCompleteExtender>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblDRCD" runat="server" Text='<%# Eval("DRCD") %>' Visible="False"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblDRCD" runat="server" Text='<%# Eval("DRCD") %>' Visible="False"></asp:Label>
                        </EditItemTemplate>
                        <ControlStyle Width="0px" />
                        <FooterStyle Width="0px" />
                        <HeaderStyle Width="0px" />
                        <ItemStyle Width="0px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit">
                        <ItemTemplate>
                            <asp:Label ID="lblCRCD" runat="server" Text='<%#Eval("CREDITCD") %>' Width="98%" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCrCd" runat="server" Text='<%#Eval("CREDITCD") %>' Width="98%"
                                TabIndex="9" AutoPostBack="True" OnTextChanged="txtCrCd_TextChanged" CssClass="txtColor fontalloc" />
                            <asp:AutoCompleteExtender ID="txtCrCd_AutoCompleteExtender" runat="server" TargetControlID="txtCrCd"
                                UseContextKey="True" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true"
                                CompletionSetCount="3" ServiceMethod="GetCompletionListCredit">
                            </asp:AutoCompleteExtender>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblCreditCD" runat="server" Text='<%#Eval("CRCD") %>' Visible="False"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblCreditCD" runat="server" Text='<%# Eval("CRCD") %>' Visible="False"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cheque No">
                        <ItemTemplate>
                            <asp:Label ID="lblCHEQUE" runat="server" Text='<%#Eval("CHEQUENO") %>' Width="98%" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtChq" runat="server" Text='<%#Eval("CHEQUENO") %>' CssClass="txtColor fontalloc"
                                TabIndex="10" Width="98%"/>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cheque Date">
                        <ItemTemplate>
                            <asp:Label ID="lblCQDT" runat="server" Text='<%#Eval("CHEQUEDT_CON") %>' Width="98%" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCqDt" runat="server" Text='<%#Eval("CHEQUEDT_CON") %>' CssClass="txtColor fontalloc"
                                TabIndex="11" Width="98%" />
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("AMOUNT") %>' Width="98%" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" Text='<%#Eval("AMOUNT") %>' Style="text-align: right"
                                TabIndex="12" CssClass="txtColor fontalloc" Width="98%" />
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("REMARKS") %>' Width="98%"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" Text='<%#Eval("REMARKS") %>' CssClass="txtColor fontalloc"
                                Width="98%" TabIndex="13" />
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="14%" />
                        <ItemStyle HorizontalAlign="Left" Width="14%" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/update.jpg"
                                TabIndex="14" ToolTip="Update" Height="20px" Width="20px" CssClass="txtColor" />
                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/Cancel.jpg"
                                TabIndex="15" ToolTip="Cancel" Height="20px" Width="20px" CssClass="txtColor" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/Edit.jpg"
                                ToolTip="Edit" Height="20px" Width="20px" CssClass="txtColor" TabIndex="101" />
                            <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Images/delete.jpg"
                                ToolTip="Delete" Height="20px" Width="20px" CssClass="txtColor" OnClientClick="return confMSG()"
                                TabIndex="102" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle Font-Names="Calibri" Font-Size="11px" />
                <HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White"></HeaderStyle>
                <RowStyle Font-Names="Calibri" Font-Size="11px" />
            </asp:GridView>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>
    </div>
    </form>
</body>
</html>
