<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ExpenseInformation.aspx.cs" Inherits="AlchemyAccounting.CNF.ExpenseInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="shortcut icon" href="../Images/favicon.ico" />
    <link href="../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/jquery.blockUI.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#txtColDt").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+100" });

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

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }

            return true;
        }


        function BlockUI(elementID) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function () {
                $("#" + elementID).block({ message: '<table align = "center"><tr><td>' +
     '<img src="images/loadingAnim.gif"/></td></tr></table>',
                    css: {},
                    overlayCSS: { backgroundColor: '#000000', opacity: 0.6, border: '3px solid #63B2EB'
                    }
                });
            });
            prm.add_endRequest(function () {
                $("#" + elementID).unblock();
            });
        }
        $(document).ready(function () {

            BlockUI("dvGrid");
            $.blockUI.defaults.css = {};
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center">
            Expense Information Entry</h1>
    </div>
    <div id="entry">
        <div id="toolbar">
            <table style="width: 100%; border: 1px solid #000;">
                <tr>
                    <td style="text-align: right; width: 38%">
                    </td>
                    <td style="width: 50%">
                        <asp:Button ID="btnRefresh" runat="server" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="15px" Text="Refresh" Width="80px" OnClick="btnRefresh_Click" />
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
                    Category ID
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="width: 60%; text-align: left; font-weight: bold">
                    <asp:TextBox ID="txtcat" runat="server" AutoPostBack="True" TabIndex="1" Width="90%"
                        OnTextChanged="txtcat_TextChanged" CssClass="txtColor"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="" TargetControlID="txtcat" MinimumPrefixLength="1"
                        CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                        ServiceMethod="GetCompletionListCatID">
                    </asp:AutoCompleteExtender>
                </td>
                <td style="width: 20%; text-align: left; font-weight: bold">
                    <asp:TextBox ID="txtcatID" runat="server" AutoPostBack="True" TabIndex="2" Width="30%"
                        OnTextChanged="txtcatID_TextChanged" CssClass="txtColor" Style="text-align: center"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td style="width: 28%; text-align: left; font-weight: bold">
                    <asp:Label ID="lblErrMsg" runat="server" ForeColor="#990000" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; font-weight: bold">
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                </td>
                <td style="width: 60%; text-align: left; font-weight: bold">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtcat"
                        runat="server" ErrorMessage="Item Name Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label runat="server" ID="lblcid" Visible="false"></asp:Label>
        <div style="float: left; width: 100%; margin-bottom: 2%">
            <asp:GridView ID="gvDetails" runat="server" BackColor="White" BorderColor="White"
                BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None"
                Width="100%" AutoGenerateColumns="False" ShowFooter="True" Style="text-align: left"
                OnRowDataBound="gvDetails_RowDataBound" OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                OnRowCommand="gvDetails_RowCommand" OnRowDeleting="gvDetails_RowDeleting" OnRowEditing="gvDetails_RowEditing"
                OnRowUpdating="gvDetails_RowUpdating">
                <Columns>
                    <asp:TemplateField HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Style="text-align: center" Text='<%# Eval("EXPCID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblIDEdit" runat="server" Style="text-align: center" Text='<%# Eval("EXPCID") %>'></asp:Label>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <%--<asp:TextBox ID="txtID" runat="server" Style="text-align: center" Text='<%# Eval("EXPCID") %>' ReadOnly="true" > </asp:TextBox>--%>
                        </FooterTemplate>
                        <FooterStyle CssClass="txtalign" HorizontalAlign="Center"  />
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Expense ID">
                        <ItemTemplate>
                            <asp:Label ID="lblExpense" runat="server" Style="text-align: center" Text='<%# Eval("EXPID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label runat="server" ID="lblExpenseEdit" Style="text-align: center" Text='<%# Eval("EXPID") %>'></asp:Label>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <%--    <asp:TextBox ID="txtExpense" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                Font-Size="12px" TabIndex="32" Width="98%" Style="text-align: center" Text='<%# Eval("EXPID") %>'
                                ReadOnly="true"></asp:TextBox>--%>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="7%" />
                        <ItemStyle HorizontalAlign="Left" Width="7%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Particulars">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtParticularsEdit" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                Font-Size="12px" TabIndex="62" Text='<%# Eval("EXPNM") %>' Width="98%" Style="text-align: left"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtParticulars" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                Font-Size="12px" TabIndex="32" Width="98%"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblParticulars" runat="server" Text='<%# Eval("EXPNM") %>' Style="text-align: center"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="55%" />
                        <ItemStyle HorizontalAlign="Center" Width="55%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRemarksEdit" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                Font-Size="12px" TabIndex="63" Text='<%# Eval("REMARKS") %>' Width="98%" Style="text-align: left"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                Font-Size="12px" TabIndex="33" Width="98%" Style="text-align: left"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARKS") %>' Style="text-align: center"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" Height="20px"
                                ImageUrl="~/Images/update.jpg" TabIndex="64" ToolTip="Update" Width="20px" />
                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" Height="20px"
                                ImageUrl="~/Images/Cancel.jpg" TabIndex="65" ToolTip="Cancel" Width="20px" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="imgbtnAdd" runat="server" CommandName="SaveCon" CssClass="txtColor"
                                Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="34" ToolTip="Save &amp; Continue"
                                ValidationGroup="validaiton" Width="15px" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" Height="20px"
                                ImageUrl="~/Images/Edit.jpg" TabIndex="100" ToolTip="Edit" Width="20px" />
                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" Height="20px"
                                ImageUrl="~/Images/delete.jpg" OnClientClick="return confMSG()" TabIndex="101"
                                ToolTip="Delete" Width="21px" />
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
</asp:Content>
<%-- <div id="dvGrid" style="padding: 10px; width: 100%">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" Width="98%" AutoGenerateColumns="false"
                        Font-Names="Arial" Font-Size="11pt" AlternatingRowStyle-BackColor="#C2D69B" HeaderStyle-BackColor="CornflowerBlue"
                        AllowPaging="true" ShowFooter="True" OnPageIndexChanging="OnPaging" OnRowEditing="EditCustomer"
                        OnRowUpdating="UpdateCustomer" OnRowCancelingEdit="CancelEdit" PageSize="10">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("EXPCID")%>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtID" Width="100%" MaxLength="5" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Expense ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblExpenseID"  Width="100%" runat="server" Text='<%# Eval("EXPID")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtExpenseID" runat="server" Text='<%# Eval("EXPID")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtExpenseID" Width="100%" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="35%" HeaderText="Particulars">
                                <ItemTemplate>
                                    <asp:Label ID="lblParticulars" runat="server" Text='<%# Eval("EXPNM")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtParticulars"  Width="100%" runat="server" Text='<%# Eval("EXPNM")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtParticulars"  Width="100%" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="35%" HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRemarks"  Width="100%" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtRemarks"  Width="100%" runat="server" ></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument='<%# Eval("EXPCID")%>'
                                        OnClientClick="return confirm('Do you want to delete?')" Text="Delete" OnClick="DeleteCustomer"></asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="AddNewCustomer" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" />
                        </Columns>
                        <AlternatingRowStyle BackColor="#C2D69B" />
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridView1" />
                </Triggers>
            </asp:UpdatePanel>
        </div>--%>