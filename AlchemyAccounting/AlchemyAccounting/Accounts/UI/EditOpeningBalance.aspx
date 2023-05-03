<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditOpeningBalance.aspx.cs" Inherits="AlchemyAccounting.Accounts.UI.EditOpeningBalance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

      .Gridview
{
font-family:Verdana;
font-size:10pt;
font-weight:normal;
color:black;
            text-align: center;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style = "margin: 2% 10% 0% 10%">
               
<asp:GridView ID="gvDetails" runat="server" 
        AutoGenerateColumns="False" CssClass="Gridview" HeaderStyle-BackColor="#61A6F8" 
ShowFooter="True" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" 
        onrowcancelingedit="gvDetails_RowCancelingEdit" 
        onrowdeleting="gvDetails_RowDeleting" onrowediting="gvDetails_RowEditing" 
        onrowupdating="gvDetails_RowUpdating" 
        onrowcommand="gvDetails_RowCommand" Width="100%">
     
<Columns>
<asp:TemplateField HeaderText="Doc No">
    <ItemTemplate>
        <asp:Label ID="lblDocNo" runat="server" Text='<%#Eval("TRANSNO") %>'/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:Label ID ="lblDocNo" runat="server" Text='<%#Eval("TRANSNO") %>'/>
    </EditItemTemplate>

    <ControlStyle Width="50px" />
    <FooterStyle HorizontalAlign="Center" Width="50px" />
    <HeaderStyle HorizontalAlign="Center" Width="50px" />
    <ItemStyle HorizontalAlign="Center" Width="50px" />

</asp:TemplateField>

<asp:TemplateField HeaderText="Date">
    <ItemTemplate>
        <asp:Label ID="lblOpenDT" runat="server" Text='<%#Eval("TRANSDT") %>'/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:Label ID ="lblOpenDT" runat="server" Text='<%#Eval("TRANSDT") %>'/>
    </EditItemTemplate>

    <ControlStyle Width="100px" />
    <FooterStyle HorizontalAlign="Center" Width="100px" />
    <HeaderStyle HorizontalAlign="Center" Width="100px" />
    <ItemStyle HorizontalAlign="Center" Width="100px" />

</asp:TemplateField>

<asp:TemplateField HeaderText="Account Information">
    <ItemTemplate>
        <asp:Label ID="lblAccNM" runat="server" Text='<%#Eval("ACCOUNTNM") %>'/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtAccNM" runat="server" Text='<%#Eval("ACCOUNTNM") %>'  
            Width="150px" AutoPostBack="True" ontextchanged="txtAccNM_TextChanged"/>

            <asp:AutoCompleteExtender ID="txtAccNM_AutoCompleteExtender" runat="server" 
                                            TargetControlID="txtAccNM" UseContextKey="True" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" 
                                            CompletionSetCount="3" ServiceMethod="GetCompletionList">
                                        </asp:AutoCompleteExtender>
                                        
    </EditItemTemplate>
    
    <ControlStyle Width="500px" />
    <FooterStyle HorizontalAlign="Left" Width="500px" />
    <HeaderStyle HorizontalAlign="Left" Width="500px" />
    <ItemStyle HorizontalAlign="Left" Width="500px" />
    
</asp:TemplateField>

<asp:TemplateField HeaderText="Debit Amount">
    <ItemTemplate>
        <asp:Label ID="lblDebitAmnt" runat="server" Text='<%#Eval("DEBITAMT") %>'/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtDebitAmnt" runat="server" Text='<%#Eval("DEBITAMT") %>' 
            style="text-align: right"/>
    </EditItemTemplate>
    
    <ControlStyle Width="150px" />
    <FooterStyle HorizontalAlign="Right" Width="150px" />
    <HeaderStyle HorizontalAlign="Right" Width="150px" />
    <ItemStyle HorizontalAlign="Right" Width="150px" />
    
</asp:TemplateField>

<asp:TemplateField HeaderText="Credit Amount">
    <ItemTemplate>
        <asp:Label ID="lblCreditAmt" runat="server" Text='<%#Eval("CREDITAMT") %>'/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtCreditAmt" runat="server" Text='<%#Eval("CREDITAMT") %>' 
            style="text-align: right"/>
    </EditItemTemplate>
    
    <ControlStyle Width="150px" />
    <FooterStyle HorizontalAlign="Right" Width="150px" />
    <HeaderStyle HorizontalAlign="Right" Width="150px" />
    <ItemStyle HorizontalAlign="Right" Width="150px" />
    
</asp:TemplateField>

 <asp:TemplateField>
<EditItemTemplate>
<asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/update.jpg" ToolTip="Update" Height="20px" Width="20px" />
<asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/Cancel.jpg" ToolTip="Cancel" Height="20px" Width="20px" />

</EditItemTemplate>
<ItemTemplate>
<asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/Edit.jpg" ToolTip="Edit" Height="20px" Width="20px" />
<asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server" ImageUrl="~/Images/delete.jpg" ToolTip="Delete" Height="20px" Width="20px" />
</ItemTemplate>
 </asp:TemplateField>

 </Columns>

<HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White"></HeaderStyle>
</asp:GridView>



            <asp:Label ID="lblDebitCD" runat="server" Visible="False"></asp:Label>



            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>



        </div>
    </div>
    </form>
</body>
</html>
