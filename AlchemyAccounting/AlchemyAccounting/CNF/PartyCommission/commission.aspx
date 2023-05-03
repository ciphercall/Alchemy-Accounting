<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="commission.aspx.cs" Inherits="AlchemyAccounting.CNF.PartyCommission.commission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="shortcut icon" href="../../Images/favicon.ico" />
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
        .style1
        {
            width: 60%;
        }
        .rightalign
        {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center">
            Party Commission Entry</h1>
    </div>
    <div id="entry">
        <div id="toolbar">
            <table style="width: 100%; border: 1px solid #000;">
                <tr>
                    <td style="text-align: right; width: 50%">
                        <asp:Label ID="lblValCommPer" runat="server" ForeColor="#990000" 
                            Visible="False"></asp:Label>
                        <asp:Label ID="lblValTP" runat="server" ForeColor="#990000" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 50%">
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
                <td style="width: 10%; text-align: right; font-weight: bold">
                    Party Name
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="width: 50%; text-align: left; font-weight: bold">
                    <asp:TextBox ID="txtParty" runat="server" AutoPostBack="True" TabIndex="1" Width="90%"
                        OnTextChanged="txtParty_TextChanged" CssClass="txtColor"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="" TargetControlID="txtParty" MinimumPrefixLength="1"
                        CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                        ServiceMethod="GetCompletionListParty">
                    </asp:AutoCompleteExtender>
                      <asp:TextBox ID="txtPartyID" runat="server" AutoPostBack="True" TabIndex="2" Width="100%"
                        CssClass="txtColor" Style="text-align: center" ReadOnly="true" Visible="false"></asp:TextBox>
                </td>
                <td style="width: 10%; text-align: left; font-weight: bold">
                   
                </td>
                <td style="width: 29%; text-align: left; font-weight: bold">
                   
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; font-weight: bold">
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                </td>
                <td style="width: 60%; text-align: left; font-weight: bold">
                    <asp:Label ID="lblErrMsg" runat="server" ForeColor="#990000" Visible="False"></asp:Label>
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
                    <asp:TemplateField HeaderText="Serial">
                        <ItemTemplate>
                            <asp:Label ID="lblSerial" runat="server" Style="text-align: center" Text='<%# Eval("COMMSL") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblSerialEdit" runat="server" Style="text-align: center" Text='<%# Eval("COMMSL") %>'></asp:Label>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <%--<asp:TextBox ID="txtID" runat="server" Style="text-align: center" Text='<%# Eval("EXPCID") %>' ReadOnly="true" > </asp:TextBox>--%>
                        </FooterTemplate>
                        <FooterStyle CssClass="txtalign" />
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblExctype" Text='<%# Eval("EXCTP") %>' Width="98%"
                                CssClass="txtalign"> </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlExctypeEdit" CssClass="txtColor txtalign"
                                Width="98%" TabIndex="20" AutoPostBack="True" OnSelectedIndexChanged="ddlExctypeEdit_SelectedIndexChanged">
                                <asp:ListItem Value="BDT">BDT</asp:ListItem>
                                <asp:ListItem Value="USD">USD</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList runat="server" ID="ddlExctype" CssClass="txtColor txtalign" Width="98%"
                                TabIndex="10" AutoPostBack="True" OnSelectedIndexChanged="ddlExctype_SelectedIndexChanged">
                                <asp:ListItem Value="BDT">BDT</asp:ListItem>
                                <asp:ListItem Value="USD">USD</asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="7%" />
                        <ItemStyle HorizontalAlign="Left" Width="7%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FROM">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtfromEdit" runat="server" CssClass="txtColor rightalign" Font-Names="Calibri"
                                Font-Size="12px" TabIndex="21" Text='<%# Eval("VALUEFR") %>' Width="98%"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtfrom" runat="server" CssClass="txtColor rightalign" Font-Names="Calibri"
                                Font-Size="12px" TabIndex="11" Width="98%"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblfrom" runat="server" Text='<%# Eval("VALUEFR") %>' CssClass="rightalign"
                                Width="98%"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TO">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtToEdit" runat="server" CssClass="txtColor rightalign" Font-Names="Calibri"
                                Font-Size="12px" TabIndex="22" Text='<%# Eval("VALUETO") %>' Width="98%"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtTO" runat="server" CssClass="txtColor rightalign" Font-Names="Calibri"
                                Font-Size="12px" TabIndex="12" Width="98%"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTo" runat="server" Text='<%# Eval("VALUETO") %>' CssClass="txtColor rightalign"
                                Width="98%"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlvalueTpEdit" Width="98%" CssClass="txtColor txtalign"
                                TabIndex="23" AutoPostBack="True" OnSelectedIndexChanged="ddlvalueTpEdit_SelectedIndexChanged">
                                <asp:ListItem Value="PCNT">Percent</asp:ListItem>
                                <asp:ListItem Value="AMT">Amount</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList runat="server" ID="ddlvalueTp" Width="98%" CssClass="txtColor txtalign"
                                TabIndex="13" AutoPostBack="True" OnSelectedIndexChanged="ddlvalueTp_SelectedIndexChanged">
                                <asp:ListItem Value="PCNT">Percent</asp:ListItem>
                                <asp:ListItem Value="AMT">Amount</asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblvalueTp" runat="server" Text='<%# Eval("VALUETP") %>' Width="98%"
                                CssClass="txtColor txtalign"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAmountEdit" runat="server" Font-Names="Calibri" Font-Size="12px"
                                TabIndex="24" Text='<%# Eval("COMMAMT") %>' Width="98%" CssClass="txtColor rightalign"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" Font-Names="Calibri" TabIndex="14" Font-Size="12px"
                                Width="98%" CssClass="txtColor rightalign"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("COMMAMT") %>' Width="98%"
                                CssClass="txtColor rightalign"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Job Type">
                        <EditItemTemplate>
                           <asp:DropDownList runat="server" ID="ddlJobTypeEdit" TabIndex="15" Width="90%" CssClass="txtColor">
                        <asp:ListItem Value="EXPORT">EXPORT</asp:ListItem>
                        <asp:ListItem Value="IMPORT">IMPORT</asp:ListItem>
                    </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList runat="server" ID="ddlJobType" TabIndex="15" Width="90%" CssClass="txtColor">
                        <asp:ListItem Value="EXPORT">EXPORT</asp:ListItem>
                        <asp:ListItem Value="IMPORT">IMPORT</asp:ListItem>
                    </asp:DropDownList>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblJobType" runat="server" Text='<%# Eval("JOBTP") %>' Width="98%"
                                CssClass="txtColor txtalign"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" Height="20px"
                                ImageUrl="~/Images/update.jpg" TabIndex="25" ToolTip="Update" Width="20px" />
                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" Height="20px"
                                ImageUrl="~/Images/Cancel.jpg" TabIndex="26" ToolTip="Cancel" Width="20px" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="imgbtnAdd" runat="server" CommandName="SaveCon" CssClass="txtColor"
                                Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="16" ToolTip="Save &amp; Continue"
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
</asp:Content>
