<%@ Page Title="Costpool Entry" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"
    CodeBehind="CostPool.aspx.cs" Inherits="AlchemyAccounting.Accounts.UI.CostPool" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="shortcut icon" href="../../Images/favicon.ico" />
    <link href="../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
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
        #entry
        {
            float: left;
            width: 100%;
            background-color: transparent;
            border: 1px solid #000;
            border-radius: 10px;
            margin-top: 10px;
        }
        #grid
        {
            float: left;
            width: 100%;
        }
        .style3
        {
            width: 124px;
        }
        .style4
        {
            width: 17px;
        }
        .style5
        {
            text-align: left;
        }
        .style6
        {
            width: 95px;
        }
        .style8
        {
            text-align: center;
            width: 1px;
        }
        .style9
        {
            width: 1px;
        }
        .style10
        {
            width: 238px;
        }
        .style11
        {
            text-align: left;
            width: 238px;
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
            border: solid 4px green !important;
        }
        .txtColor
        {
            margin-left: 0px;
            text-align: left;
        }
        .txtaligncenter
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center" style="font-weight: bold;">
            Branch/Sales Center Entry</h1>
    </div>
    <div id="entry">
        <table style="width: 100%;">
            <tr>
                <td class="style3">
                    &nbsp;
                </td>
                <td class="style4">
                    &nbsp;
                </td>
                <td class="style6">
                    &nbsp;
                </td>
                <td class="style9">
                    &nbsp;
                </td>
                <td class="style10">
                    <asp:Label ID="lblCatID" runat="server" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMaxCatID" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;
                </td>
                <td class="style4">
                    &nbsp;
                </td>
                <td class="style6" style="text-align: right">
                    <strong>Category</strong>
                </td>
                <td class="style8">
                    <strong>:</strong>
                </td>
                <td class="style11">
                    <asp:TextBox ID="txtCategoryNM" runat="server" TabIndex="1" Width="250px" 
                        OnTextChanged="txtCategoryNM_TextChanged" CssClass="txtColor" 
                        AutoPostBack="True"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtCategoryNM_AutoCompleteExtender" runat="server"
                        DelimiterCharacters="" Enabled="True" ServicePath="" TargetControlID="txtCategoryNM"
                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                        UseContextKey="True" ServiceMethod="GetCompletionList">
                    </asp:AutoCompleteExtender>
                </td>
                <td class="style5">
                    <asp:Button ID="Search" runat="server" Font-Bold="True" Font-Italic="True" TabIndex="2" CssClass="txtColor txtaligncenter"
                        Text="Search" OnClick="Search_Click" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Label ID="lblChkItemID" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="style4">
                    &nbsp;
                </td>
                <td class="style6">
                    &nbsp;
                </td>
                <td class="style9">
                    &nbsp;
                </td>
                <td class="style10">
                    <asp:Label ID="lblIMaxItemID" runat="server" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </td>
            </tr>
        </table>
        <div id="grid">
            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" CssClass="Gridview"
                HeaderStyle-BackColor="#61A6F8" ShowFooter="True" HeaderStyle-Font-Bold="true"
                HeaderStyle-ForeColor="White" OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                OnRowDeleting="gvDetails_RowDeleting" OnRowEditing="gvDetails_RowEditing" OnRowUpdating="gvDetails_RowUpdating"
                OnRowCommand="gvDetails_RowCommand" OnRowDataBound="gvDetails_RowDataBound" Width="100%"
                Font-Names="Calibri">
                <Columns>
                    <asp:TemplateField HeaderText="Cat ID">
                        <ItemTemplate>
                            <asp:Label ID="lblCatGID" runat="server" Text='<%# Eval("CATID") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblCatGIDEdit" runat="server" Text='<%#Eval("CATID") %>' />
                        </EditItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="3%" />
                        <ItemStyle HorizontalAlign="Center" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cost Pool ID">
                        <ItemTemplate>
                            <asp:Label ID="lblCOSTPID" runat="server" Text='<%# Eval("COSTPID") %>' Style="text-align: center" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblCOSTPIDEdit" runat="server" Text='<%#Eval("COSTPID") %>' Style="text-align: center" />
                        </EditItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cost Pool Name">
                        <ItemTemplate>
                            <asp:Label ID="lblCOSTPNM" runat="server" Text='<%# Eval("COSTPNM") %>' Style="text-align: left" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCOSTPNMEdit" runat="server" Text='<%#Eval("COSTPNM") %>' Width="98%"
                                TabIndex="10" Font-Names="Calibri" CssClass="txtColor"/>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCOSTPNM" runat="server" Width="98%" TabIndex="3" Font-Names="Calibri" CssClass="txtColor" />
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="29%" />
                        <ItemStyle HorizontalAlign="Left" Width="29%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:Label ID="lblREMARKS" runat="server" Text='<%#Eval("REMARKS") %>' 
                                Width="90px" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtREMARKSEdit" runat="server" Text='<%#Eval("REMARKS") %>' TabIndex="15"
                                Width="98%" Font-Names="Calibri" CssClass="txtColor"/>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtREMARKS" runat="server" TabIndex="8"
                                Width="98%" Font-Names="Calibri" CssClass="txtColor"/>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                        <ItemStyle Width="20%" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" 
                                Height="20px" ImageUrl="~/Images/Edit.jpg" TabIndex="30" ToolTip="Edit" 
                                Width="20px" />
                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" 
                                Height="20px" ImageUrl="~/Images/delete.jpg" OnClientClick="return confMSG()" 
                                TabIndex="31" Text="Edit" ToolTip="Delete" Width="20px" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" 
                                Height="20px" ImageUrl="~/Images/update.jpg" TabIndex="16" ToolTip="Update" 
                                Width="20px" />
                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" 
                                Height="20px" ImageUrl="~/Images/Cancel.jpg" TabIndex="17" ToolTip="Cancel" 
                                Width="20px" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="imgbtnAdd" runat="server" CommandName="AddNew" 
                                Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="9" 
                                ToolTip="Add new Record" ValidationGroup="validaiton" Width="30px" />
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999966" />
                <HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White"></HeaderStyle>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
