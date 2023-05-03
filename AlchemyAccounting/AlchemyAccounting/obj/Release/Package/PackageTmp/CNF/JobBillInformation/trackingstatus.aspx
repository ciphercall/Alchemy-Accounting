<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
     CodeBehind="trackingstatus.aspx.cs" Inherits="AlchemyAccounting.CNF.JobBillInformation.trackingstatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
     <link rel="shortcut icon" href="../../Images/favicon.ico" />
    
    <link href="../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>

    <script type="text/javascript">
        
        $(document).ready(function () {
            $("#StatusDatefoot").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-10:+20" });
            
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
           Tracking Data Update Information</h1>
    </div>
    <div id="entry">
        <div id="toolbar">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right; width: 50%">
                    <%--<asp:Button runat="server" ID="btnUpdate" CssClass="txtColor" Font-Bold="True" Font-Names="Calibri"
                            Style="text-align: center" Font-Size="15px" TabIndex="9" Text="Edit" Width="80px"
                            OnClick="btnUpdate_Click" Height="30px" />--%>
                        <%--<asp:Button runat="server" ID="btnPrint" CssClass="txtColor" 
                        Font-Bold="True" Font-Names="Calibri"
                            Style="text-align: center" Font-Size="15px" Text="Print" Width="80px" 
                        OnClick="btnPrint_Click" Height="30px" />--%>
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
                        &nbsp;&nbsp;&nbsp; <strong>Date :</strong>
                        <asp:TextBox runat="server" ID="txtReceiveDate"  CssClass="txtColor txtalign readonly"
                            Width="19%"></asp:TextBox>
                        <%--<asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1 well"
                        Format="dd/MM/yyyy" TargetControlID="txtReceiveDate">--%>
                        <%--</asp:CalendarExtender>--%>
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
                        <asp:Label runat="server" ID="lblddladd" Visible="False"></asp:Label>
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
                        <asp:TemplateField HeaderText="Status Date">
                            <ItemTemplate>
                                <asp:Label ID="lblStatusDate" TabIndex="1" runat="server" Style="text-align: center" Text='<%# Eval("STATSDT") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="StatusDateEdit" runat="server" TabIndex="6" ClientIDMode="Static" AutoPostBack="True" Style="text-align: center" Text='<%# Eval("STATSDT") %>'></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2"  runat="server" CssClass="cal_Theme1 well"
                                Format="dd/MM/yyyy" TargetControlID="StatusDateEdit">
                            </asp:CalendarExtender>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="StatusDatefoot"  ClientIDMode="Static" AutoPostBack="True" Text='<%# Eval("STATSDT") %>' CssClass="txtColor"
                                    Width="100%" TabIndex="11"></asp:TextBox>
                                
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BStatus">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblstatusItem" CssClass="txtColor txtalign" TabIndex="2" Width="100%"
                                    Text='<%# Eval("STATUS") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" TabIndex="7" ID="ddlstatusEdit">
                                    <asp:ListItem>Waiting for Custom Processing</asp:ListItem>
                                     <asp:ListItem>Goods On The Way to Factory</asp:ListItem>
                                     <asp:ListItem>Goods Delivered on Date</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList runat="server" TabIndex="12" ID="ddlstatusFoot">
                                    <asp:ListItem>Waiting for Custom Processing</asp:ListItem>
                                     <asp:ListItem>Goods On The Way to Factory</asp:ListItem>
                                     <asp:ListItem>Goods Delivered on Date</asp:ListItem>
                                </asp:DropDownList>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="4%" />
                            <ItemStyle HorizontalAlign="Center" Width="4%" />
                            <FooterStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label runat="server" TabIndex="3" ID="lblREMARKSItem" Text='<%# Eval("REMARKS") %>'> </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" TabIndex="8" ID="txtREMARKSEdit" Text='<%# Eval("REMARKS") %>'> </asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" TabIndex="13" ID="txtREMARKSfoot" CssClass="txtColor" Width="100%" Text='<%# Eval("REMARKS") %>'></asp:TextBox>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="4%" />
                            <ItemStyle HorizontalAlign="Left" Width="4%" />
                        </asp:TemplateField>
                        
                        
                        <asp:TemplateField HeaderText="serial" Visible="False">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="srialitem" Text='<%# Eval("SERIAL") %>'> </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label runat="server" ID="serialedit" Text='<%# Eval("SERIAL") %>'> </asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label runat="server" ID="serailfoot" CssClass="txtColor" Width="100%" Text='<%# Eval("SERIAL") %>'></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="4%" />
                            <ItemStyle HorizontalAlign="Left" Width="4%" />
                        </asp:TemplateField>
                        
                        

                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" Height="20px"
                                    ImageUrl="~/Images/update.jpg" TabIndex="9" ToolTip="Update" Width="20px" />
                                <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" Height="20px"
                                    ImageUrl="~/Images/Cancel.jpg" TabIndex="10" ToolTip="Cancel" Width="20px" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="imgbtnAdd" runat="server" CommandName="SaveCon" CssClass="txtColor"
                                    Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="14" ToolTip="Save &amp; Continue"
                                    ValidationGroup="validaiton" Width="15px" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" Height="20px"
                                    ImageUrl="~/Images/Edit.jpg" TabIndex="4" ToolTip="Edit" Width="20px" />
                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" Height="20px"
                                    ImageUrl="~/Images/delete.jpg" OnClientClick="return confMSG()" TabIndex="5"
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
