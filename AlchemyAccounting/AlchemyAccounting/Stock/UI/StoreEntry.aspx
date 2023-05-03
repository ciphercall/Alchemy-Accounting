<%@ Page Title="Store Entry" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"
    CodeBehind="StoreEntry.aspx.cs" Inherits="AlchemyAccounting.Stock.UI.StoreEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
            margin-bottom: 30px;
        }
        #toolbar
        {
            float: left;
            width: 100%;
            background-color: #cccccc;
            border-radius: 10px 10px 0px 0px;
        }
        #grid
        {
            float: left;
            padding-bottom: 20px;
            margin-left: 20px;
        }
        .Gridview
        {
            font-family: Verdana;
            font-size: 10pt;
            font-weight: normal;
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center" style="font-weight: bold;">
            STORE ENTRY</h1>
    </div>
    <div id="entry">
        <div id="toolbar">
            <asp:Label ID="lblMaxStID" runat="server"></asp:Label>
            <asp:Label ID="lblSTID" runat="server"></asp:Label>
        </div>
        <div id="grid">
            <br />
            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" CssClass="Gridview"
                HeaderStyle-BackColor="#61A6F8" ShowFooter="True" HeaderStyle-Font-Bold="true"
                HeaderStyle-ForeColor="White" OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                OnRowDeleting="gvDetails_RowDeleting" OnRowEditing="gvDetails_RowEditing" OnRowUpdating="gvDetails_RowUpdating"
                OnRowCommand="gvDetails_RowCommand" OnRowDataBound="gvDetails_RowDataBound" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="Store ID">
                        <ItemTemplate>
                            <asp:Label ID="lblSTID" runat="server" Text='<%# Eval("STOREID") %>' Width="80px" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblSTID" runat="server" Text='<%# Eval("STOREID") %>' Width="80px" />
                        </EditItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <ControlStyle Width="80px" />
                        <FooterStyle Width="80px" />
                        <HeaderStyle Width="80px" />
                        <ItemStyle Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Store Name">
                        <ItemTemplate>
                            <asp:Label ID="lblSTNM" runat="server" Text='<%# Eval("STORENM") %>' Width="250px" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSTNMEdit" runat="server" Text='<%#Eval("STORENM") %>' Width="250px"
                                TabIndex="6" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtSTNM" runat="server" Width="250px" TabIndex="1" />
                        </FooterTemplate>
                        <ControlStyle Width="250px" />
                        <FooterStyle Width="250px" />
                        <HeaderStyle Width="250px" />
                        <ItemStyle Width="250px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate>
                            <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("ADDRESS") %>' Width="250px" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAddressEdit" runat="server" Text='<%#Eval("ADDRESS") %>' Width="250px"
                                TabIndex="6" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddress" runat="server" Width="250px" TabIndex="2" />
                        </FooterTemplate>
                        <ControlStyle Width="250px" />
                        <FooterStyle HorizontalAlign="Left" Width="250px" />
                        <HeaderStyle HorizontalAlign="Center" Width="250px" />
                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contact No">
                        <ItemTemplate>
                            <asp:Label ID="lblContact" runat="server" Text='<%#Eval("CONTACTNO") %>' Width="100px"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtContactEdit" runat="server" Text='<%#Eval("CONTACTNO") %>' Width="100px"
                                TabIndex="7" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtContact" runat="server" TabIndex="3" Width="100px" />
                        </FooterTemplate>
                        <ControlStyle Width="100px" />
                        <FooterStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARKS") %>' Width="200px"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRemarksEdit" runat="server" Text='<%#Eval("REMARKS") %>' Width="200px"
                                TabIndex="8" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" Width="200px" TabIndex="4" />
                        </FooterTemplate>
                        <ControlStyle Width="200px" />
                        <FooterStyle HorizontalAlign="Center" Width="200px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/update.jpg"
                                ToolTip="Update" Height="20px" Width="20px" TabIndex="9" />
                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/Cancel.jpg"
                                ToolTip="Cancel" Height="20px" Width="20px" TabIndex="10" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/Edit.jpg"
                                ToolTip="Edit" Height="20px" Width="20px" TabIndex="10" />
                            <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server"
                                ImageUrl="~/Images/delete.jpg" ToolTip="Delete" Height="20px" Width="20px" OnClientClick="return confMSG()"
                                TabIndex="11" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/AddNewitem.jpg"
                                CommandName="AddNew" Width="30px" Height="30px" ToolTip="Add new Record" ValidationGroup="validaiton"
                                TabIndex="5" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White"></HeaderStyle>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
