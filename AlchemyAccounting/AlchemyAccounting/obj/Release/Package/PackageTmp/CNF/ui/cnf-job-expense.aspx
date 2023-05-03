<%@ Page Title="CNF Job Expenses Information" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="cnf-job-expense.aspx.cs" Inherits="AlchemyAccounting.CNF.ui.cnf_job_expense" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="shortcut icon" href="../../Images/favicon.ico" />
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
                return false;
            }
            return true;
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
            height: 20px;
        }

        .txtaligncenter {
            text-align: center;
        }

        .txtalignright {
            text-align: right;
        }

        .txtalignleft {
            text-align: left;
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

        .completionList {
            width: 300px !important;
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
    <div id="header">
        <h1 align="center">Job Expense</h1>
    </div>
    <div id="entry">
        <div id="toolbar">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: right" class="style10">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <% if (Session["UserTp"].ToString() == "ADMIN")
                            { %>
                        <asp:Button ID="btnEdit" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"
                            Text="Edit" Width="80px" TabIndex="50" CssClass="txtColor txtaligncenter"
                            OnClick="btnEdit_Click" Height="30px" />
                        <% } %>
                    </td>
                    <td class="style8">&nbsp;
                    </td>
                    <td class="style9">
                        <asp:Button ID="btnPrint" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"
                            Text="Print" Width="80px" TabIndex="50" CssClass="txtColor txtaligncenter"
                            OnClick="btnPrint_Click" Visible="False" Height="30px" />
                        <asp:Button ID="btnPrintmini" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"
                            Text="Print Mini" Width="100px" TabIndex="50" CssClass="txtColor txtaligncenter"
                            OnClick="btnPrint_ClickMini" Height="30px" />
                        <asp:Button ID="btnRefresh" runat="server" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="15px" Text="Refresh" Width="80px" TabIndex="51" CssClass="txtColor txtaligncenter"
                            OnClick="btnRefresh_Click" Height="30px" />
                        <% if (Session["UserTp"].ToString() == "ADMIN")
                            { %>
                        <asp:Button ID="btnDelete" runat="server" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="15px" Text="Delete" Width="80px" TabIndex="51" CssClass="txtColor txtaligncenter"
                            Height="30px" OnClick="btnDelete_Click" />
                        <% } %>
                        <asp:Label ID="lblMY" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblInvoiceNo" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblExsl" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div class="cont">
            <table style="width: 100%; font-family: Calibri; font-size: 14px; font-weight: bold">
                <tr>
                    <td style="text-align: right; width: 25%">Expense Date
                    </td>
                    <td style="width: 1%">:
                    </td>
                    <td style="width: 76%">
                        <asp:TextBox ID="txtExDT" runat="server" AutoPostBack="True" CssClass="txtColor"
                            OnTextChanged="txtExDT_TextChanged" TabIndex="1" Width="13%" Font-Names="Calibri"
                            Font-Size="14px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtExDT"
                            PopupButtonID="txtExDT" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                        &nbsp;&nbsp;&nbsp; Invoice No :
                        <asp:TextBox ID="txtNo" runat="server" ReadOnly="True" CssClass="txtaligncenter"
                            Width="10%" TabIndex="100" Font-Names="Calibri" Font-Size="14px"></asp:TextBox>
                        <asp:DropDownList ID="ddlInvoice" runat="server" AutoPostBack="True" TabIndex="1"
                            Visible="False" CssClass="txtColor"
                            OnSelectedIndexChanged="ddlInvoice_SelectedIndexChanged" Height="25px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 25%">Job No &amp; Year
                    </td>
                    <td style="width: 1%">:
                    </td>
                    <td style="width: 76%">
                        <asp:TextBox ID="txtJobNo" runat="server" AutoPostBack="True" CssClass="txtColor txtaligncenter"
                            TabIndex="2" Width="15%" Font-Names="Calibri" Font-Size="14px" OnTextChanged="txtJobNo_TextChanged"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txtJobNo_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                            Enabled="True" ServicePath="" TargetControlID="txtJobNo" MinimumPrefixLength="1"
                            CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                            ServiceMethod="GetCompletionListJob_No_Year_Type" CompletionListCssClass="completionList"
                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                        </asp:AutoCompleteExtender>
                        <asp:TextBox ID="txtJobYear" runat="server" ReadOnly="True" Width="10%" Font-Names="Calibri"
                            Font-Size="14px" TabIndex="200" CssClass="txtaligncenter"></asp:TextBox>
                        <asp:TextBox ID="txtJobTP" runat="server" ReadOnly="True" Width="10%" Font-Names="Calibri"
                            Font-Size="14px" TabIndex="2000" CssClass="txtaligncenter"></asp:TextBox>
                        <asp:TextBox ID="txtCompID" runat="server" ReadOnly="True" Width="10%" Font-Names="Calibri"
                            Font-Size="14px" TabIndex="20001" Visible="False"></asp:TextBox>
                        <strong>Assessable Value:</strong> <asp:TextBox runat="server" ID="txtAssValue" CssClass="txtColor txtalign readonly"
                            Width="15%" ReadOnly="true"></asp:TextBox> 
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 25%">Expensed By
                    </td>
                    <td style="width: 1%">:
                    </td>
                    <td style="width: 76%">
                        <asp:TextBox ID="txtExpenseNM" runat="server" AutoPostBack="True" CssClass="txtColor"
                            OnTextChanged="txtExpenseNM_TextChanged" TabIndex="3" Width="40%" Font-Names="Calibri"
                            Font-Size="14px"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txtExpenseNM_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                            Enabled="True" ServicePath="" TargetControlID="txtExpenseNM" MinimumPrefixLength="1"
                            CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                            ServiceMethod="GetCompletionListExpense" CompletionListCssClass="completionList"
                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                        </asp:AutoCompleteExtender>
                        <asp:TextBox ID="txtExCD" runat="server" ReadOnly="True" Width="20%" Font-Names="Calibri"
                            Font-Size="14px" TabIndex="300"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 25%">Remarks
                    </td>
                    <td style="width: 1%">:
                    </td>
                    <td style="width: 76%">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtColor" TabIndex="4" Width="61%"
                            Font-Names="Calibri" Font-Size="14px" AutoPostBack="True" OnTextChanged="txtRemarks_TextChanged"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txtRemarks_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                            Enabled="True" ServicePath="" TargetControlID="txtRemarks" MinimumPrefixLength="1"
                            CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                            ServiceMethod="GetCompletionListCompanyName" CompletionListCssClass="completionList"
                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 25%">&nbsp;
                    </td>
                    <td style="width: 1%">&nbsp;
                    </td>
                    <td style="width: 76%">
                        <asp:Label ID="lblError" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
            <div id="grid">
                <asp:GridView ID="gvDetails" runat="server" BackColor="White" BorderColor="White"
                    BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None"
                    Width="100%" AutoGenerateColumns="False" ShowFooter="True" Style="text-align: left"
                    OnRowDataBound="gvDetails_RowDataBound" OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                    OnRowCommand="gvDetails_RowCommand" OnRowDeleting="gvDetails_RowDeleting" OnRowEditing="gvDetails_RowEditing"
                    OnRowUpdating="gvDetails_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Serial">
                            <ItemTemplate>
                                <asp:Label ID="lblSL" runat="server" Style="text-align: center" Text='<%# Eval("SLNO") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblSlEdit" runat="server" Style="text-align: center" Text='<%# Eval("SLNO") %>'></asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Particulars">
                            <ItemTemplate>
                                <asp:Label ID="lblParticulars" runat="server" Text='<%# Eval("EXPNM") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtParticularsEdit" runat="server" Font-Names="Calibri" CssClass="txtColor"
                                    Font-Size="12px" TabIndex="61" Width="98%" Text='<%# Eval("EXPNM") %>' AutoPostBack="True"
                                    OnTextChanged="txtParticularsEdit_TextChanged"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServicePath="" TargetControlID="txtParticularsEdit" MinimumPrefixLength="1"
                                    CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                                    CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                    CompletionListHighlightedItemCssClass="itemHighlighted" ServiceMethod="GetCompletionListParticulars">
                                </asp:AutoCompleteExtender>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtParticulars" runat="server" Font-Names="Calibri" Font-Size="12px"
                                    TabIndex="31" Width="98%" CssClass="txtColor" AutoPostBack="True" OnTextChanged="txtParticulars_TextChanged"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServicePath="" TargetControlID="txtParticulars" MinimumPrefixLength="1"
                                    CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                                    CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                    CompletionListHighlightedItemCssClass="itemHighlighted" ServiceMethod="GetCompletionListParticulars">
                                </asp:AutoCompleteExtender>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="45%" />
                            <ItemStyle HorizontalAlign="Left" Width="45%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCodeEdit" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                    Font-Size="12px" TabIndex="620" Text='<%# Eval("EXPID") %>' Width="98%" Style="text-align: center"
                                    ReadOnly="True"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtCode" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                    Font-Size="12px" TabIndex="320" Width="98%" Style="text-align: center" ReadOnly="True"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCode" runat="server" Text='<%# Eval("EXPID") %>' Style="text-align: center"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="8%" />
                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Expense Amount">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAmountEdit" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                    Font-Size="12px" TabIndex="63" Text='<%# Eval("EXPAMT") %>' Width="98%" onkeypress="return isNumberKey(event)" Style="text-align: right"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                    Font-Size="12px" TabIndex="33" Width="98%" onkeypress="return isNumberKey(event)" Style="text-align: right">.00</asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("EXPAMT") %>' Style="text-align: right"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="12%" />
                            <ItemStyle HorizontalAlign="Right" Width="12%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemarksGEdit" runat="server" CssClass="txtColor txtalignleft" Font-Names="Calibri"
                                    Font-Size="12px" TabIndex="64" Text='<%# Eval("REMARKS_BOT") %>' Width="98%"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtRemarksG" runat="server" CssClass="txtColor txtalignleft" Font-Names="Calibri"
                                    Font-Size="12px" TabIndex="34" Width="98%"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARKS_BOT") %>' Style="text-align: left;"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" Height="20px" CssClass="txtColor"
                                    ImageUrl="~/Images/update.jpg" TabIndex="65" ToolTip="Update" Width="20px" />
                                <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" Height="20px" CssClass="txtColor"
                                    ImageUrl="~/Images/Cancel.jpg" TabIndex="66" ToolTip="Cancel" Width="20px" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="imgbtnAdd" runat="server" CommandName="SaveCon" CssClass="txtColor"
                                    Height="20px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="35" ToolTip="Save &amp; Continue"
                                    ValidationGroup="validaiton" Width="20px" />
                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Complete" CssClass="txtColor"
                                    Height="20px" ImageUrl="~/Images/checkmark.jpg" TabIndex="36" ToolTip="Complete"
                                    ValidationGroup="validaiton" Width="20px" />
                                <asp:ImageButton ID="ImagebtnPPrint" runat="server" CommandName="SavePrint" CssClass="txtColor"
                                    Height="20px" ImageUrl="~/Images/print.gif" TabIndex="37" ToolTip="Save &amp; Print"
                                    ValidationGroup="validaiton" Width="20px" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" Height="20px" CssClass="txtColor"
                                    ImageUrl="~/Images/Edit.jpg" TabIndex="100" ToolTip="Edit" Width="20px" />
                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" Height="20px" CssClass="txtColor"
                                    ImageUrl="~/Images/delete.jpg" OnClientClick="return confMSG()" TabIndex="101"
                                    ToolTip="Delete" Width="20px" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" Font-Names="Calibri"
                        Font-Size="14px" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" Font-Names="Calibri" Font-Size="12px" />
                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#594B9C" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#33276A" />
                </asp:GridView>
                <table style="width: 100%; font-family: Calibri; font-size: 14px; font-weight: bold">
                    <tr>
                        <td style="width: 5%"></td>
                        <td style="width: 45%"></td>
                        <td style="width: 8%; text-align: right">Total :
                        </td>
                        <td style="width: 12%; text-align: right">
                            <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="txtalignright" Font-Bold="True"
                                Font-Names="Calibri" Font-Size="14px" ReadOnly="True" Width="100%">.00</asp:TextBox>
                        </td>
                        <td style="width: 20%"></td>
                        <td style="width: 10%"></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
