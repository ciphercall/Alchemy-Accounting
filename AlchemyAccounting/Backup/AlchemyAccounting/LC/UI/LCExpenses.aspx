<%@ Page Title="L/C Expenses Information" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="LCExpenses.aspx.cs" Inherits="AlchemyAccounting.LC.UI.LCExpenses" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtDate").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+0" });
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
            border-radius: 0px 0px 10px 10px;
            margin-top: 10px;
        }
        #grid
        {
            float:left;
            width:100%;
        }
        .Gridview
         {
            font-family:Verdana;
            font-size:10pt;
            font-weight:normal;
            color:black;
            margin-right: 0px;
            text-align: left;
         }
        .txtColor:focus
        {
            border:solid 1px green !important;
        }
        .txtColor
        {
            margin-left: 0px;
            text-align: left;
        }
        .style2
        {
            width: 298px;
            text-align: right;
        }
        .style3
        {
            width: 3px;
            text-align: center;
            font-weight: bold;
        }
        #toolbar
        {
            float:left;
            width:100%;
            background-color: #cccccc;
            border-radius: 10px 10px 0px 0px;
        }
        .style4
        {
            width: 464px;
        }
        </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div id="header">
        <h1 align="center" style="font-weight: bold;">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            L/C EXPENSES INFORMATION</h1>
    </div>
    <div id ="entry">

        <div id="toolbar">
            <table style="width:100%;"><tr><td class="style4" style="text-align: right">
                <asp:Button ID="btnExpenseEdit" runat="server" Font-Bold="True" Text="EDIT" 
                    Width="70px" onclick="btnExpenseEdit_Click" /></td><td>
                    &nbsp;</td></tr><tr><td class="style4">
                <asp:Label ID="lblMY" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblMxNo" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblOpenDT" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblCashBank" runat="server" Visible="False"></asp:Label>
                </td><td>&nbsp;</td></tr></table>
        </div>
        <table style="width:100%;">
            <tr>
                <td class="style2" style="text-align: right">
                    <strong>Date</strong></td>
                <td class="style3">
                    :</td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server" Width="85px" TabIndex="1" 
                        CssClass="txtColor" AutoPostBack="True" 
                        ontextchanged="txtDate_TextChanged" ClientIDMode="Static"></asp:TextBox>
                    <asp:TextBox ID="txtNo" runat="server" Width="80px" CssClass="txtColor" 
                        ReadOnly="True"></asp:TextBox>
                    <asp:DropDownList ID="ddlNo" runat="server" Width="100px" CssClass="txtColor" 
                        Visible="False" AutoPostBack="True" 
                        onselectedindexchanged="ddlNo_SelectedIndexChanged">
                    </asp:DropDownList>
                &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <strong>L/C ID</strong></td>
                <td class="style3">
                    :</td>
                <td>
                    <asp:TextBox ID="txtLCName" runat="server" Width="350px" TabIndex="2" 
                        CssClass="txtColor" AutoPostBack="True" 
                        ontextchanged="txtLCName_TextChanged"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtLCName_AutoCompleteExtender" runat="server" 
                        CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" 
                        Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListLC" 
                        ServicePath="" TargetControlID="txtLCName" UseContextKey="True">
                    </asp:AutoCompleteExtender>
                    <asp:TextBox ID="txtLCCD" runat="server" Width="200px" CssClass="txtColor" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <%--<strong>L/C No</strong></td>--%>
                    <strong>Invoice No</strong></td>
                <td class="style3">
                    :</td>
                <td>
                    <%--<asp:TextBox ID="txtLCNo" runat="server" Width="150px" 
                        CssClass="txtColor" ReadOnly="True"></asp:TextBox>
                    <strong>LC Date :
                    <asp:TextBox ID="txtLCDate" runat="server" CssClass="txtColor" 
                        ReadOnly="True" Width="80px"></asp:TextBox>--%>
                    <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="txtColor" TabIndex="3" 
                        Width="350px" AutoPostBack="True" ontextchanged="txtInvoiceNo_TextChanged"></asp:TextBox>
                    <%--</strong>--%>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <strong>Remarks</strong></td>
                <td class="style3">
                    :</td>
                <td>
                    <asp:TextBox ID="txtRemarks" runat="server" Width="563px" TabIndex="4" 
                        CssClass="txtColor" AutoPostBack="True" 
                        ontextchanged="txtRemarks_TextChanged"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    <%--<asp:Button ID="btnUpdate" runat="server" Font-Bold="True" 
                        onclick="btnUpdate_Click" Text="UPDATE" Visible="False" />--%>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" 
                        CssClass="Gridview" HeaderStyle-BackColor="#61A6F8" 
                        HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" 
                        onrowcancelingedit="gvDetails_RowCancelingEdit" 
                        onrowcommand="gvDetails_RowCommand" OnRowDataBound="gvDetails_RowDataBound" 
                        onrowdeleting="gvDetails_RowDeleting" onrowediting="gvDetails_RowEditing" 
                        onrowupdating="gvDetails_RowUpdating" ShowFooter="True" 
            Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Charge Name">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtChargeNM" runat="server" Width="98%" TabIndex="6" 
                                        CssClass="txtColor" ontextchanged="txtChargeNM_TextChanged" 
                                        AutoPostBack="True"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txtChargeNM_AutoCompleteExtender" runat="server" 
                                        CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" 
                                        Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListChargeNM" 
                                        ServicePath="" TargetControlID="txtChargeNM" UseContextKey="True">
                                    </asp:AutoCompleteExtender>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtChargeNMEdit" runat="server" Width="98%" Text='<%# Eval("CHARGENM") %>'
                                        CssClass="txtColor" TabIndex="20" AutoPostBack="True" 
                                        ontextchanged="txtChargeNMEdit_TextChanged"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txtChargeNMEdit_AutoCompleteExtender" 
                                        runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" 
                                        Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListChargeNMEdit" 
                                        ServicePath="" TargetControlID="txtChargeNMEdit" UseContextKey="True">
                                    </asp:AutoCompleteExtender>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblChargeNM" runat="server" style="text-align: left" 
                                        Text='<%# Eval("CHARGENM") %>' Width="98%" />
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Left" Width="25%" />
                                <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                <ItemStyle HorizontalAlign="Left" Width="25%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Charge ID">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtChrgeIDEdit" runat="server" CssClass="txtColor" 
                                        style="text-align: left" Text='<%#Eval("CHARGEID") %>' 
                                        Width="98%" ReadOnly="True"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtChargeID" runat="server" ReadOnly="True" Width="93%" 
                                        CssClass="txtColor"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblChrgeID" runat="server" style="text-align: left" 
                                        Text='<%# Eval("CHARGEID") %>' Width="98%" />
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Left" Width="10%" />
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" style="text-align: right" 
                                        Text='<%# Eval("AMOUNT") %>' Width="98%" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAmountEdit" runat="server" CssClass="txtColor" 
                                        style="text-align: right" TabIndex="21" Text='<%# Eval("AMOUNT") %>' 
                                        Width="95%" />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="txtColor" 
                                        style="text-align: right" TabIndex="7" Width="95%" >.00</asp:TextBox>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Left" Width="15%" />
                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cash/Bank Name">

                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCashBankEdit" runat="server" CssClass="txtColor" Text='<%#Eval("ACCOUNTNM") %>' 
                                        TabIndex="22" Width="95%" AutoPostBack="True" 
                                        ontextchanged="txtCashBankEdit_TextChanged"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txtCashBankEdit_AutoCompleteExtender" 
                                        runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" 
                                        Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListCashBankEdit" 
                                        ServicePath="" TargetControlID="txtCashBankEdit" UseContextKey="True">
                                    </asp:AutoCompleteExtender>
                                </EditItemTemplate>

                                <FooterTemplate>
                                    <asp:TextBox ID="txtCashBankNm" runat="server" TabIndex="8" Width="95%" 
                                        CssClass="txtColor" ontextchanged="txtCashBankNm_TextChanged" 
                                        AutoPostBack="True"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txtCashBankNm_AutoCompleteExtender" 
                                        runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" 
                                        Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListCashBank" 
                                        ServicePath="" TargetControlID="txtCashBankNm" UseContextKey="True">
                                    </asp:AutoCompleteExtender>
                                </FooterTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblCashBankNM" runat="server" Text='<%#Eval("ACCOUNTNM") %>' ></asp:Label>
                                </ItemTemplate>

                            <FooterStyle HorizontalAlign="Left" Width="20%"></FooterStyle>

                            <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRemarksGridEdit" runat="server" CssClass="txtColor" 
                                        TabIndex="23" Text='<%#Eval("REMARKS") %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtRemarksGrid" runat="server" TabIndex="9" Width="95%" 
                                        CssClass="txtColor"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarksGrid" runat="server" Text='<%# Eval("REMARKS") %>' 
                                        style="text-align: left" Width="98%"></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="20%" />
                                <FooterStyle HorizontalAlign="Left" Width="20%" />
                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="False">
                                <EditItemTemplate>
                                    <asp:Label ID="lblChargeSlEdit" runat="server" Text='<%#Eval("CHARGESL") %>'></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblChargeSl" runat="server" Text='<%#Eval("CHARGESL") %>'></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblChargeShow" runat="server" Text='<%#Eval("CHARGESL") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" 
                                        Height="20px" ImageUrl="~/Images/update.jpg" TabIndex="24" ToolTip="Update" 
                                        Width="20px" />
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" 
                                        Height="20px" ImageUrl="~/Images/Cancel.jpg" TabIndex="25" ToolTip="Cancel" 
                                        Width="20px" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" 
                                        Height="20px" ImageUrl="~/Images/Edit.jpg" TabIndex="13" ToolTip="Edit" 
                                        Width="20px" />
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" OnClientClick="return confMSG()" 
                                        Height="20px" ImageUrl="~/Images/delete.jpg" TabIndex="14" Text="Edit" 
                                        ToolTip="Delete" Width="20px" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:ImageButton ID="imgbtnAdd" runat="server" CommandName="AddNew" 
                                        Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="10" 
                                        ToolTip="Add new Record" ValidationGroup="validaiton" Width="30px" />
                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Complete" 
                                         CssClass="txtColor" Height="30px" ImageUrl="~/Images/checkmark.jpg" 
                                         TabIndex="11" ToolTip="Complete" ValidationGroup="validaiton" 
                                         Width="30px" />
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Left" Width="10%" />
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999966" />
                        <HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
    </div>
</asp:Content>