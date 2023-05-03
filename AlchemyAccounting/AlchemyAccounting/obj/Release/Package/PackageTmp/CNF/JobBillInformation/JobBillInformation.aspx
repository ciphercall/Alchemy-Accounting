<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="JobBillInformation.aspx.cs" Inherits="AlchemyAccounting.CNF.JobBillInformation.JobBillInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="shortcut icon" href="../../Images/favicon.ico" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script type="text/javascript">
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
            border: solid 3px green !important;
        }
        .txtColor
        {
            margin-left: 0px;
            text-align: left;
            height: 22px;
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
        .readonly
        {
            background-color: #CCCCCC;
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
            job (Bill) Information</h1>
    </div>
    <div id="entry">
        <div id="toolbar">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right; width: 50%">
                    <%--<asp:Button runat="server" ID="btnUpdate" CssClass="txtColor" Font-Bold="True" Font-Names="Calibri"
                            Style="text-align: center" Font-Size="15px" TabIndex="9" Text="Edit" Width="80px"
                            OnClick="btnUpdate_Click" Height="30px" />--%>
                        <asp:Button runat="server" ID="btnPrint" CssClass="txtColor" 
                        Font-Bold="True" Font-Names="Calibri"
                            Style="text-align: center" Font-Size="15px" Text="Print" Width="80px" 
                        OnClick="btnPrint_Click" Height="30px" />
                </td>
                <td style="width: 50%">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </td>
            </tr>
        </table>
        </div>
        <div class="def">
            <table style="width: 100%">
                <tr>
                    <td style="width: 15%; text-align: right; font-weight: bold">
                        Job No & Year
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 84%">
                        <asp:TextBox runat="server" ID="txtJobID" CssClass="txtColor txtalign" Width="11%"
                            TabIndex="3" OnTextChanged="txtJobID_TextChanged" AutoPostBack="True"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" Enabled="True" ServicePath=""
                            TargetControlID="txtJobID" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true"
                            CompletionSetCount="3" UseContextKey="True" ServiceMethod="GetCompletionListJob_No_Year_Type"
                            runat="server" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                            CompletionListHighlightedItemCssClass="itemHighlighted">
                        </asp:AutoCompleteExtender>
                        <asp:TextBox runat="server" ID="txtJobYear" CssClass="txtColor txtalign readonly"
                            Width="12%" ReadOnly="true"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtJobType" CssClass="txtColor txtalign readonly"
                            Width="15%" ReadOnly="true"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;  <strong>Assessable Value:</strong> <asp:TextBox runat="server" ID="txtAssValue" CssClass="txtColor txtalign readonly"
                            Width="15%" ReadOnly="true"></asp:TextBox>  &nbsp;&nbsp;&nbsp; <strong>Date :</strong>
                        <asp:TextBox runat="server" ID="txtReceiveDate" CssClass="txtColor txtalign readonly"
                            Width="19%"></asp:TextBox>
                        <%--<asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1 well"
                        Format="dd/MM/yyyy" TargetControlID="txtReceiveDate">
                        </asp:CalendarExtender>--%>
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
                        <asp:TextBox runat="server" ID="txtCompanyNM" CssClass="txtColor readonly" Width="67%"
                            ReadOnly="true"></asp:TextBox>
                        &nbsp;
                        <asp:TextBox runat="server" ID="txtCompanyID" CssClass="txtColor" Width="20%" ReadOnly="true"
                            Visible="False"></asp:TextBox>
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
                        <asp:TextBox runat="server" ID="txtPartyNM" CssClass="txtColor readonly" Width="67%"
                            ReadOnly="true"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtPartyID" CssClass="txtColor" Width="20%" ReadOnly="true"
                            Visible="False"></asp:TextBox>
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
                        <asp:Label ID="lblErrmsg" runat="server" Visible="False" ForeColor="Red" Style="font-weight: 700"></asp:Label>
                    </td>
                </tr>
            </table>
            <div style="float: left; width: 100%; margin-bottom: 2%">
                <asp:GridView ID="gvDetails" runat="server" BackColor="White" BorderColor="White"
                    BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None"
                    Width="100%" AutoGenerateColumns="False" ShowFooter="True" Style="text-align: left"
                    OnRowDataBound="gvDetails_RowDataBound" OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                    OnRowCommand="gvDetails_RowCommand" OnRowDeleting="gvDetails_RowDeleting" OnRowEditing="gvDetails_RowEditing"
                    OnRowUpdating="gvDetails_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Bill Date">
                            <ItemTemplate>
                                <asp:Label ID="lblBillDate" runat="server" Style="text-align: center" Text='<%# Eval("BILLD") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblBillDateEdit" runat="server" Style="text-align: center" Text='<%# Eval("BILLD") %>'></asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="txtBillDate" Text='<%# Eval("BILLD") %>' CssClass="txtColor txtalign readonly"
                                    Width="100%" TabIndex="11" ReadOnly="true"></asp:TextBox>
                                <%--<asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1 well"
                                Format="dd/MM/yyyy" TargetControlID="txtBillDate">
                            </asp:CalendarExtender>--%>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill No">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblBillNo" CssClass="txtColor txtalign" Width="100%"
                                    Text='<%# Eval("BILLNO") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBillNoEdit" runat="server" CssClass="txtColor readonly txtalign"
                                    Font-Names="Calibri" Text='<%# Eval("BILLNO") %>' Style="text-align: center"
                                    Font-Size="12px" TabIndex="121" Width="98%" ReadOnly="true"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtBillNo" runat="server" CssClass="txtColor readonly txtalign"
                                    Font-Names="Calibri" Text='<%# Eval("BILLNO") %>' Style="text-align: center"
                                    Font-Size="12px" TabIndex="120" Width="98%" ReadOnly="true"></asp:TextBox>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="4%" />
                            <ItemStyle HorizontalAlign="Center" Width="4%" />
                            <FooterStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SL" Visible="False">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSl" Text='<%# Eval("EXPSL") %>'> </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label runat="server" ID="lblSlEdit" Text='<%# Eval("EXPSL") %>'> </asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label runat="server" ID="lblSlItem" CssClass="txtColor" Width="100%" Text='<%# Eval("EXPSL") %>'></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="4%" />
                            <ItemStyle HorizontalAlign="Center" Width="4%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Expense">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtExpenseEdit" runat="server" CssClass="txtColor readonly" Font-Names="Calibri"
                                    Text='<%# Eval("EXPNM") %>' Font-Size="12px" TabIndex="12" Width="98%" ReadOnly="true"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtExpense" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                    Font-Size="12px" TabIndex="13" Width="98%" AutoPostBack="True" Text='<%# Eval("EXPNM") %>'
                                    OnTextChanged="txtExpense_TextChanged"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender2" Enabled="True" ServicePath=""
                                    TargetControlID="txtExpense" MinimumPrefixLength="1" CompletionInterval="10"
                                    CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                    EnableCaching="true" CompletionSetCount="3" UseContextKey="True" ServiceMethod="GetCompletionListExpenseID_Name"
                                    runat="server">
                                </asp:AutoCompleteExtender>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblExpense" runat="server" Style="text-align: center" Text='<%# Eval("EXPNM") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="13%" />
                            <ItemStyle HorizontalAlign="Left" Width="13%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Expense ID">
                            <EditItemTemplate>
                                <%--<asp:Label ID="txtExpenseIDEdit" runat="server" Text='<%# Eval("PARTYID") %>' Style="text-align: center"></asp:Label>--%>
                                <asp:TextBox ID="txtExpenseIDEdit" runat="server" CssClass="txtColor readonly txtalign"
                                    Font-Names="Calibri" ReadOnly="true" Text='<%# Eval("EXPID") %>' Style="text-align: center"
                                    Font-Size="12px" TabIndex="55" Width="98%"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <%--<asp:Label ID="txtExpenseID" runat="server" Text='<%# Eval("PARTYID") %>' Style="text-align: center"></asp:Label>--%>
                                <asp:TextBox ID="txtExpenseID" runat="server" CssClass="txtColor readonly txtalign"
                                    Font-Names="Calibri" ReadOnly="true" Text='<%# Eval("EXPID") %>' Style="text-align: center"
                                    Font-Size="12px" TabIndex="55" Width="98%"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblExpenseID" runat="server" Text='<%# Eval("EXPID") %>' Width="98%"
                                    CssClass="txtalign"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="4%" />
                            <ItemStyle HorizontalAlign="Right" Width="4%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Expense Amount">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtExpenseAmountEdit" runat="server" CssClass="txtColor readonly"
                                    Font-Names="Calibri" Text='<%# Eval("EXPAMT") %>' Style="text-align: right" Font-Size="12px"
                                    TabIndex="55" Width="98%" ReadOnly="true"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtExpenseAmount" runat="server" CssClass="txtColor" Font-Names="Calibri" Enabled="false"
                                    Text='0.00' Style="text-align: right" Font-Size="12px" TabIndex="14"
                                    Width="98%"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblExpenseAmount" runat="server" Text='<%# Eval("EXPAMT") %>' Width="98%"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="7%" />
                            <ItemStyle HorizontalAlign="Right" Width="7%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill Amount">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBillAmountEdit" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                    Text='<%# Eval("BILLAMT") %>' Style="text-align: right" Font-Size="12px" TabIndex="30"
                                    Width="98%"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtBillAmount" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                    Text='<%# Eval("BILLAMT") %>' Style="text-align: center" Font-Size="12px" TabIndex="15"
                                    Width="98%"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblBillAmount" runat="server" Text='<%# Eval("BILLAMT") %>' Width="98%"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="7%" />
                            <ItemStyle HorizontalAlign="Right" Width="7%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemarksEdit" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                    Text='<%# Eval("REMARKS") %>' Style="text-align: center" Font-Size="12px" TabIndex="31"
                                    Width="98%"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                    Text='<%# Eval("REMARKS") %>' Style="text-align: center" Font-Size="12px" TabIndex="16"
                                    Width="98%"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="12%" />
                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtFwdDateEdit" CssClass="txtColor" Width="100%"
                                    Text='<%# Eval("EXPPD") %>' TabIndex="32" 
                                    ontextchanged="txtFwdDateEdit_TextChanged" AutoPostBack="True"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1 well"
                                    Format="dd/MM/yyyy" TargetControlID="txtFwdDateEdit">
                                </asp:CalendarExtender>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="txtFwdDate" CssClass="txtColor" Width="100%" TabIndex="17"
                                    Text='<%# Eval("EXPPD") %>' AutoPostBack="True" 
                                    ontextchanged="txtFwdDate_TextChanged"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1 well"
                                    Format="dd/MM/yyyy" TargetControlID="txtFwdDate">
                                </asp:CalendarExtender>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFwdDate" runat="server" Text='<%# Eval("EXPPD") %>' Style="text-align: center"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill SL">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBillSlEdit" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                    Text='<%# Eval("BILLSL") %>' Style="text-align: center" Font-Size="12px" 
                                    TabIndex="33" AutoPostBack="true"
                                    Width="98%" ontextchanged="txtBillSlEdit_TextChanged"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtBillSl" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                    Text='<%# Eval("BILLSL") %>' Style="text-align: center" Font-Size="12px" 
                                    TabIndex="18" AutoPostBack="true"
                                    Width="98%" ontextchanged="txtBillSl_TextChanged"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblBillSl" runat="server" Text='<%# Eval("BILLSL") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="4%" />
                            <ItemStyle HorizontalAlign="Center" Width="4%" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" Height="20px"
                                    ImageUrl="~/Images/update.jpg" TabIndex="34" ToolTip="Update" Width="20px" />
                                <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" Height="20px"
                                    ImageUrl="~/Images/Cancel.jpg" TabIndex="35" ToolTip="Cancel" Width="20px" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="imgbtnAdd" runat="server" CommandName="SaveCon" CssClass="txtColor"
                                    Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="19" ToolTip="Save &amp; Continue"
                                    ValidationGroup="validaiton" Width="15px" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" Height="20px"
                                    ImageUrl="~/Images/Edit.jpg" TabIndex="100" ToolTip="Edit" Width="20px" />
                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" Height="20px"
                                    ImageUrl="~/Images/delete.jpg" OnClientClick="return confMSG()" TabIndex="101"
                                    ToolTip="Delete" Width="21px" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            <ItemStyle HorizontalAlign="Left" Width="5%" />
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
                <table style="width: 100%">
                    <tr>
                        <td style="width: 5%">
                        </td>
                        <td style="width: 57%">
                            <asp:Label ID="lblErrMsgExist" runat="server" ForeColor="#990000" Visible="False"></asp:Label>
                            <asp:Label ID="lblChkInternalID" runat="server" ForeColor="#990000" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
