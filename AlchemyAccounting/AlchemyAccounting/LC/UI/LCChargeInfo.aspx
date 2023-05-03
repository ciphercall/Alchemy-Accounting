<%@ Page Title="L/C Charges Information" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="LCChargeInfo.aspx.cs" Inherits="AlchemyAccounting.LC.UI.LCChargeInfo" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

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
        </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="header">
        <h1 align="center" style="font-weight: bold;">l/c charges information</h1>
    </div>
    <div id="entry">
        
        <div id="grid">
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" 
                        CssClass="Gridview" HeaderStyle-BackColor="#61A6F8" 
                        HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" 
                        onrowcancelingedit="gvDetails_RowCancelingEdit" 
                        onrowcommand="gvDetails_RowCommand" OnRowDataBound="gvDetails_RowDataBound" 
                        onrowdeleting="gvDetails_RowDeleting" onrowediting="gvDetails_RowEditing" 
                        onrowupdating="gvDetails_RowUpdating" ShowFooter="True" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Type">
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" 
                                        CssClass="txtColor" onselectedindexchanged="ddlType_SelectedIndexChanged" 
                                        TabIndex="1" Width="98%">
                                        <asp:ListItem Value="BANK">BANK</asp:ListItem>
                                        <asp:ListItem Value="L/C">L/C</asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblType" runat="server" style="text-align: left" 
                                        Text='<%# Eval("CHARGETP") %>' Width="98%" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlTypeEdit" runat="server" AutoPostBack="True" 
                                        CssClass="txtColor" onselectedindexchanged="ddlTypeEdit_SelectedIndexChanged" 
                                        TabIndex="10" Width="98%">
                                        <asp:ListItem Value="BANK">BANK</asp:ListItem>
                                        <asp:ListItem Value="L/C">L/C</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <FooterStyle HorizontalAlign="Left" Width="10%" />
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Charge ID">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtChrgeIDEdit" runat="server" CssClass="txtColor" 
                                        style="text-align: left" TabIndex="11" Text='<%#Eval("CHARGEID") %>' 
                                        Width="98%" ReadOnly="True"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtChargeID" runat="server" ReadOnly="True" Width="93%"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblChrgeID" runat="server" style="text-align: left" 
                                        Text='<%# Eval("CHARGEID") %>' Width="98%" />
                                </ItemTemplate>
                                <ControlStyle Width="8%" />
                                <FooterStyle HorizontalAlign="Left" Width="8%" />
                                <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                <ItemStyle Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Charge Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblChargeNM" runat="server" style="text-align: left" 
                                        Text='<%# Eval("CHARGENM") %>' Width="98%" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtChargeNMEdit" runat="server" CssClass="txtColor" 
                                        style="text-align: left" TabIndex="12" Text='<%#Eval("CHARGENM") %>' 
                                        Width="98%" />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtChargeNM" runat="server" CssClass="txtColor" 
                                        style="text-align: left" TabIndex="3" Width="98%" />
                                </FooterTemplate>
                                <ControlStyle Width="35%" />
                                <FooterStyle HorizontalAlign="Left" Width="35%" />
                                <HeaderStyle HorizontalAlign="Center" Width="35%" />
                                <ItemStyle HorizontalAlign="Left" Width="35%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRemarksEdit" runat="server" CssClass="txtColor" 
                                        TabIndex="13" Text='<%#Eval("REMARKS") %>' Width="98%"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtColor" TabIndex="4" 
                                        Width="98%"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("REMARKS") %>' 
                                        Width="98%"></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="40%" />
                                <FooterStyle HorizontalAlign="Left" Width="40%" />
                                <HeaderStyle HorizontalAlign="Center" Width="40%" />
                                <ItemStyle HorizontalAlign="Left" Width="40%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" 
                                        Height="20px" ImageUrl="~/Images/update.jpg" TabIndex="14" ToolTip="Update" 
                                        Width="20px" />
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" 
                                        Height="20px" ImageUrl="~/Images/Cancel.jpg" TabIndex="15" ToolTip="Cancel" 
                                        Width="20px" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" 
                                        Height="20px" ImageUrl="~/Images/Edit.jpg" TabIndex="10" ToolTip="Edit" 
                                        Width="20px" />
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" 
                                        Height="20px" ImageUrl="~/Images/delete.jpg" TabIndex="11" Text="Edit" OnClientClick="return confMSG()"
                                        ToolTip="Delete" Width="20px" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:ImageButton ID="imgbtnAdd" runat="server" CommandName="AddNew" 
                                        Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="5" 
                                        ToolTip="Add new Record" ValidationGroup="validaiton" Width="30px" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999966" />
                        <HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>



        </div>
        
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:Label ID="lblMxChargeID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblLCType" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        
    </div>

</asp:Content>
