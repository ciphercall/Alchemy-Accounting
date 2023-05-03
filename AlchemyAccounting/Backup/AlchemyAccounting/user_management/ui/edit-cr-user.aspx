<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit-cr-user.aspx.cs" Inherits="AlchemyAccounting.cr_user.ui.edit_cr_user" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit Create User</title>
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
        .style1
        {
            font-family: Calibri;
            width: 512px;
        }
        .style2
        {
            width: 5px;
        }
        .style3
        {
            font-family: Calibri;
            width: 5px;
        }
        .style4
        {
            width: 512px;
        }
        .Gridview
        {
            font-family: Calibri;
        }
        .txtColor:focus
        {
            border: solid 3px green !important;
        }
        .txtColor
        {
            margin-left: 0px;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td class="style4">
                    &nbsp;
                </td>
                <td class="style2">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" style="text-align: left">
                    <strong>Search :
                        <asp:TextBox ID="txtSearch" runat="server" Width="300px" AutoPostBack="True" OnTextChanged="txtSearch_TextChanged"
                            CssClass="txtColor"></asp:TextBox>
                    </strong>
                </td>
                <td class="style3">
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="lblBranchCD" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblBranchNM" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblUserTp" runat="server" Visible="False"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;
                </td>
                <td class="style2">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <div>
            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1"
                GridLines="None" Style="font-family: Calibri" Width="60%" OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                OnRowDeleting="gvDetails_RowDeleting" OnRowEditing="gvDetails_RowEditing" OnRowUpdating="gvDetails_RowUpdating">
                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtnameEdit" runat="server" Width="98%" Text='<%#Eval("Name") %>'
                                Font-Names="Calibri" Font-Size="10px" TabIndex="1" CssClass="txtColor"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Style="font-family: Calibri" Text='<%#Eval("Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="E-Mail">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEmailEdit" runat="server" Width="98%" Text='<%#Eval("Email") %>'
                                CssClass="txtColor" Font-Names="Calibri" Font-Size="10px" TabIndex="2" 
                                AutoPostBack="True" ontextchanged="txtEmailEdit_TextChanged"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User ID">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtuseridEdit" runat="server" Text='<%#Eval("UserID") %>' Font-Names="Calibri"
                                ReadOnly="true" Font-Size="10px" TabIndex="3" Width="98%" CssClass="txtColor"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbluserid" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Branch">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlBranchEdit" runat="server" AutoPostBack="True" Font-Names="Calibri" Font-Size="10px"
                                onselectedindexchanged="ddlBranchEdit_SelectedIndexChanged" Width="98%" TabIndex="4" CssClass="txtColor">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("COSTPNM") %>' Font-Names="Calibri" Font-Size="10px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Type">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlUserTypeEdit" runat="server" AutoPostBack="True" Font-Names="Calibri" Font-Size="10px"
                                onselectedindexchanged="ddlUserTypeEdit_SelectedIndexChanged" Width="98%" TabIndex="5" CssClass="txtColor">
                                <asp:ListItem>USER</asp:ListItem>
                                <asp:ListItem>ADMIN</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblUserType" runat="server" Text='<%#Eval("USERTP") %>' Font-Names="Calibri" Font-Size="10px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Password">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPassEdit" runat="server" Width="98%" Text='<%#Eval("Password") %>'
                                CssClass="txtColor" Font-Names="Calibri" Font-Size="10px" TabIndex="6"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPassword" runat="server" Text='<%#Eval("Password") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="7%" />
                        <ItemStyle HorizontalAlign="Left" Width="7%" />
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="lblLoginIDEdit" runat="server" Text='<%#Eval("LoginID") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblLoginID" runat="server" Text='<%#Eval("LoginID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/Edit.jpg"
                                ToolTip="Edit" Height="20px" Width="20px" CssClass="txtColor"/>
                            <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Images/delete.jpg"
                                ToolTip="Delete" Height="20px" Width="20px" CssClass="txtColor" OnClientClick="return confMSG()" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/update.jpg"
                                ToolTip="Update" Height="20px" Width="20px" CssClass="txtColor" TabIndex="7"/>
                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/Cancel.jpg"
                                ToolTip="Cancel" Height="20px" Width="20px" CssClass="txtColor" TabIndex="8"/>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999966" Font-Names="Calibri" Font-Size="10px" />
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#DEDFDE" Font-Names="Calibri" Font-Size="10px" ForeColor="Black" />
                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#594B9C" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#33276A" />
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
