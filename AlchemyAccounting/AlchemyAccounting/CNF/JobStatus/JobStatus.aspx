<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="JobStatus.aspx.cs" Inherits="AlchemyAccounting.CNF.JobStatus.JobStatus" %>

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
            text-align: left;
        }

        .Gridview {
            font-family: Verdana;
            font-size: 10pt;
            font-weight: normal;
            color: black;
            margin-right: 0px;
            text-align: left;
        }

        .txtColor:focus {
            border: solid 3px green !important;
        }

        .txtColor {
            margin-left: 0px;
            text-align: left;
            height: 22px;
        }

        .def {
            float: left;
            width: 100%;
        }

        #toolbar {
            float: left;
            width: 100%;
            background-color: #cccccc;
            border-radius: 10px 10px 0px 0px;
        }

        .ui-accordion {
            text-align: left;
        }

        .txtalign {
            text-align: center;
        }

        .passport {
            float: left;
            width: 100%;
            height: 250px;
        }

        .sign {
            float: left;
            width: 100%;
            height: 150px;
            margin-top: 10%;
        }

        .autocomplete_completionListElement_grid {
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

        .autocomplete_listItem_grid {
        }

        .autocomplete_highlightedListItem_grid {
            background: #000;
            color: Orange;
        }

        .completionList {
            width: 400px !important;
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 200px;
            overflow: auto;
            background-color: #FFFFFF;
        }

        .listItem {
            color: #1C1C1C;
        }

        .itemHighlighted {
            background-color: orange;
        }

        .well {
            background-color: white;
        }

        /*Calendar Control CSS*/
        .cal_Theme1 .ajax__calendar_other .ajax__calendar_day, .cal_Theme1 .ajax__calendar_other .ajax__calendar_year {
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
      <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="upd1">
        <ContentTemplate>

            <div id="header">
                <h1 align="center">Job Status Information</h1>
            </div>
            <div id="entry">
                <%--<div id="toolbar">--%>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right; width: 50%">
                            <asp:Label ID="lblExsl" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td style="width: 50%">
                          
                        </td>
                    </tr>
                </table>
                <%--</div>--%>
                <div class="def">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 10%; text-align: right; font-weight: bold">Company Name
                            </td>
                            <td style="width: 1%; text-align: center; font-weight: bold">:
                            </td>
                            <td style="width: 84%; text-align: left; font-weight: bold" colspan="7">
                                <asp:TextBox ID="txtCompNM" runat="server" AutoPostBack="True" TabIndex="1" Width="60%"
                                    CssClass="txtColor" OnTextChanged="txtCompNM_TextChanged"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServicePath="" TargetControlID="txtCompNM" MinimumPrefixLength="1"
                                    CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                                    ServiceMethod="GetCompletionListCompanyName" CompletionListCssClass="completionList"
                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                </asp:AutoCompleteExtender>
                                <asp:TextBox ID="txtCompID" runat="server" Width="10%" CssClass="txtColor" Visible="false"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%; text-align: right; font-weight: bold"></td>
                            <td style="width: 1%; text-align: center; font-weight: bold"></td>
                            <td style="width: 84%; text-align: left; font-weight: bold" colspan="7">
                                <asp:Label ID="lblError" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%; text-align: right; font-weight: bold">Job Type
                            </td>
                            <td style="width: 1%; text-align: center; font-weight: bold">:
                            </td>
                            <td style="width: 5%; text-align: left; font-weight: bold">
                                <asp:DropDownList runat="server" ID="ddlJobTP" CssClass="txtColor" TabIndex="2" OnSelectedIndexChanged="ddlJobTP_SelectedIndexChanged"
                                    AutoPostBack="true" Width="100%">
                                    <asp:ListItem Value="EXPORT">EXPORT</asp:ListItem>
                                    <asp:ListItem Value="IMPORT">IMPORT</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 5%; text-align: right; font-weight: bold">Job Year
                            </td>
                            <td style="width: 1%; text-align: center; font-weight: bold">:
                            </td>
                            <td style="width: 5%; text-align: left; font-weight: bold">
                                <asp:TextBox ID="txtJobYR" runat="server" TabIndex="3" Width="60%" CssClass="txtColor"
                                    onkeypress="return isNumberKey(event)" AutoPostBack="true" OnTextChanged="txtJobYR_TextChanged"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                    Enabled="True" ServicePath="" TargetControlID="txtJobYR" MinimumPrefixLength="1"
                                    CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                                    ServiceMethod="GetCompletionListYear" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"
                                    CompletionListCssClass="completionList">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td style="width: 5%; text-align: right; font-weight: bold">Bill Date
                            </td>
                            <td style="width: 1%; text-align: center; font-weight: bold">:
                            </td>
                            <td style="width: 5%; text-align: left; font-weight: bold">
                                <asp:TextBox runat="server" ID="txtBilldt" CssClass="txtColor" Width="50%" AutoPostBack="True"
                                    TabIndex="4" OnTextChanged="txtBilldt_TextChanged"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1 well"
                                    Format="dd/MM/yyyy" TargetControlID="txtBilldt">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%; text-align: right; font-weight: bold">&nbsp;
                            </td>
                            <td style="width: 1%; text-align: center; font-weight: bold">&nbsp;
                            </td>
                            <td style="width: 10%; text-align: left; font-weight: bold">
                                <asp:Button runat="server" ID="btnShow" CssClass="txtColor right" Font-Bold="True"
                                    Font-Names="Calibri" Style="text-align: center" Font-Size="15px" TabIndex="5"
                                    Text="Search" Width="80px" OnClick="btnShow_Click" Height="30px" />
                                &nbsp;
                            </td>
                            <td style="text-align: left; font-weight: bold" colspan="6">
                                <asp:Label runat="server" ID="ErrDt" Visible="False" ForeColor="Red"></asp:Label>
                                <asp:Label runat="server" ID="Erryear" Visible="False" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
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
                                        <asp:Label ID="lblBillDate" runat="server" Style="text-align: center" Text='<%# Eval("TRANSD") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblBillDateEdit" runat="server" Style="text-align: center" Text='<%# Eval("TRANSD") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox runat="server" ID="txtBillDate" CssClass="txtColor txtalign" Width="100%"
                                            TabIndex="11" AutoPostBack="True" OnTextChanged="txtBillDate_TextChanged"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1 well"
                                            Format="dd/MM/yyyy" TargetControlID="txtBillDate">
                                        </asp:CalendarExtender>
                                    </FooterTemplate>
                                    <FooterStyle CssClass="txtalign" />
                                    <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Year" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblyear" Text='<%# Eval("TRANSYY") %>'> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label runat="server" ID="lblyearEdit" Text='<%# Eval("TRANSYY") %>'> </asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="txtyear" CssClass="txtColor" Width="100%" Text='<%# Eval("TRANSYY") %>'></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="4%" />
                                    <ItemStyle HorizontalAlign="Left" Width="4%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill No">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBillNo" Style="text-align: center" Text='<%# Eval("TRANSNO") %>'> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label runat="server" ID="lblBillNoEdit" Style="text-align: center" Text='<%# Eval("TRANSNO") %>'> </asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="4%" />
                                    <ItemStyle HorizontalAlign="Center" Width="4%" />
                                    <FooterStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Job No">
                                    <EditItemTemplate>
                                        <asp:Label ID="txtJobNoEdit" runat="server" Text='<%# Eval("JOBNO") %>' Style="text-align: center"></asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtJobNo" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                            Style="text-align: center" Font-Size="12px" TabIndex="12" Width="98%" OnTextChanged="txtJobNo_TextChanged"
                                            AutoPostBack="True"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender12" Enabled="True" ServicePath=""
                                            TargetControlID="txtJobNo" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true"
                                            CompletionSetCount="3" UseContextKey="True" ServiceMethod="GetCompletionListJob_No_Year_Type"
                                            runat="server" CompletionListItemCssClass="listItem" CompletionListCssClass="completionList">
                                        </asp:AutoCompleteExtender>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblJobNo" runat="server" Text='<%# Eval("JOBNO") %>' Style="text-align: center"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party ID" Visible="false">
                                    <EditItemTemplate>
                                        <asp:Label ID="txtPrtyIDEdit" runat="server" Text='<%# Eval("PARTYID") %>' Style="text-align: center"></asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="txtPrtyID" runat="server" Text='<%# Eval("PARTYID") %>' Style="text-align: center"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrtyID" runat="server" Text='<%# Eval("PARTYID") %>' Style="text-align: center"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="13%" />
                                    <ItemStyle HorizontalAlign="Right" Width="13%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Name">
                                    <EditItemTemplate>
                                        <asp:Label ID="txtPrtyNMEdit" runat="server" Text='<%# Eval("PARTYNM") %>' Style="text-align: center"></asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="txtPrtyNM" runat="server" Text='<%# Eval("PARTYNM") %>' Style="text-align: center"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrtyNM" runat="server" Text='<%# Eval("PARTYNM") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="13%" />
                                    <ItemStyle HorizontalAlign="Left" Width="13%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Forward Date">
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="txtFwdDateEdit" CssClass="txtColor" Width="100%"
                                            TabIndex="22" Text='<%# Eval("BILLFD") %>'></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1 well"
                                            Format="dd/MM/yyyy" TargetControlID="txtFwdDateEdit">
                                        </asp:CalendarExtender>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox runat="server" ID="txtFwdDate" CssClass="txtColor txtalign" Width="100%"
                                            TabIndex="13"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1 well"
                                            Format="dd/MM/yyyy" TargetControlID="txtFwdDate">
                                        </asp:CalendarExtender>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblFwdDate" runat="server" Text='<%# Eval("BILLFD") %>' Style="text-align: center"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <EditItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlStatusEdit" CssClass="txtColor" TabIndex="23"
                                            Width="100%">
                                            <asp:ListItem Value="C">COMPLETED</asp:ListItem>
                                            <asp:ListItem Value="P">PENDING</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="txtColor" TabIndex="14"
                                            Width="100%">
                                            <asp:ListItem Value="C">COMPLETED</asp:ListItem>
                                            <asp:ListItem Value="P">PENDING</asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS") %>' Style="text-align: center"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
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
                                        <%--<asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" Height="20px"
                                    ImageUrl="~/Images/delete.jpg" OnClientClick="return confMSG()" TabIndex="101"
                                    ToolTip="Delete" Width="21px" />--%>
                                &nbsp;
                                <asp:Button runat="server" ID="btnprocess" Text="Process" Height="30px" CssClass="txtColor"
                                    OnClick="btnprocess_Click" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="6%" />
                                    <ItemStyle HorizontalAlign="Left" Width="6%" />
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
                                <td style="width: 5%"></td>
                                <td style="width: 57%">
                                    <asp:Label ID="lblErrMsgExist" runat="server" ForeColor="#990000" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChkInternalID" runat="server" ForeColor="#990000" Visible="False"></asp:Label>
                                    <asp:GridView ID="GridView1" runat="server">
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
