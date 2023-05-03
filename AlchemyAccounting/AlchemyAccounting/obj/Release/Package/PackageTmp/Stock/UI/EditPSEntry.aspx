<%@ Page Title="Party Suppliar Entry" Language="C#" AutoEventWireup="true" CodeBehind="EditPSEntry.aspx.cs" Inherits="AlchemyAccounting.Stock.UI.EditPSEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

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

      .Gridview
{
font-family:Verdana;
font-size:10pt;
font-weight:normal;
color:black;

}
        .style1
        {
            width: 5px;
        }
        .style2
        {
            width: 228px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
            
    
           
    <div>
        <table style="width:100%;">
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </td>
                <td style="text-align: right">
                    <b>PS Type</b></td>
                <td class="style1">
                    <b>:</b></td>
                <td>
                    <asp:DropDownList ID="ddlPSTP" runat="server" TabIndex="1" Width="150px" 
                        AutoPostBack="True" onselectedindexchanged="ddlPSTP_SelectedIndexChanged">
                        <asp:ListItem Value="">SELECT</asp:ListItem>
                        <asp:ListItem Value="P">PARTY</asp:ListItem>
                        <asp:ListItem Value="S">SUPPLIAR</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td style="text-align: right">
                    <asp:Label ID="lblPSID" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        
        <div style = "margin-right: 5%;">
            <asp:GridView ID="gvDetails" runat="server" 
        AutoGenerateColumns="False" CssClass="Gridview" 
            HeaderStyle-BackColor="#61A6F8" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" 
        onrowcancelingedit="gvDetails_RowCancelingEdit" 
        onrowdeleting="gvDetails_RowDeleting" onrowediting="gvDetails_RowEditing" 
        onrowupdating="gvDetails_RowUpdating" 
        onrowcommand="gvDetails_RowCommand"
        OnRowDataBound="gvDetails_RowDataBound" Width="100%" Font-Size="8pt">
     
<Columns>

<asp:TemplateField HeaderText="Party/Suppliar Name">
    <ItemTemplate>
        <asp:Label ID="lblPSNM" runat="server" Text='<%#Eval("ACCOUNTNM") %>'/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtPSNMEdit" runat="server" Text='<%#Eval("ACCOUNTNM") %>' 
            AutoPostBack="True" ontextchanged="txtPSNMEdit_TextChanged"/>
        <asp:AutoCompleteExtender ID="txtPSNMEdit_AutoCompleteExtender" runat="server" 
             TargetControlID="txtPSNMEdit" UseContextKey="True" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" 
             CompletionSetCount="3" ServiceMethod="GetCompletionList">
         </asp:AutoCompleteExtender>
    </EditItemTemplate>
    
    <ControlStyle Width="180px" />
    <FooterStyle HorizontalAlign="Center" Width="180px" />
    <HeaderStyle HorizontalAlign="Center" Width="5%" />
    <ItemStyle HorizontalAlign="Center" Width="5%" />
    
</asp:TemplateField>

 <asp:TemplateField HeaderText="City">
    <ItemTemplate>
        <asp:Label ID="lblCity" runat="server" Text='<%#Eval("CITY") %>'/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtCityEdit" runat="server" Text='<%#Eval("CITY") %>' 
         />
    </EditItemTemplate>
     
     <ControlStyle Width="80px" />
     <FooterStyle HorizontalAlign="Left" Width="80px" />
     <HeaderStyle HorizontalAlign="Center" Width="5%" />
     <ItemStyle HorizontalAlign="Left" Width="5%" />
     
 </asp:TemplateField>

<asp:TemplateField HeaderText="Address">
    <ItemTemplate>
        <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("ADDRESS") %>'/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtAddressEdit" runat="server" Text='<%#Eval("ADDRESS") %>'/>
    </EditItemTemplate>
    
    <ControlStyle Width="120px" />
    <FooterStyle HorizontalAlign="Left" Width="120px" />
    <HeaderStyle HorizontalAlign="Center" Width="15%" />
    <ItemStyle HorizontalAlign="Left" Width="15%" />
    
</asp:TemplateField>

<asp:TemplateField HeaderText="Contact No">
    <ItemTemplate>
        <asp:Label ID="lblCont" runat="server" Text='<%#Eval("CONTACTNO") %>'/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtContEdit" runat="server" Text='<%#Eval("CONTACTNO") %>'/>
    </EditItemTemplate>
    
    <ControlStyle Width="80px" />
    <FooterStyle HorizontalAlign="Center" Width="80px" />
    <HeaderStyle HorizontalAlign="Center" Width="10%" />
    <ItemStyle HorizontalAlign="Center" Width="10%" />
    
</asp:TemplateField>

<asp:TemplateField HeaderText="Email">
    <ItemTemplate>
        <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("EMAIL") %>'/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtEmailEdit" runat="server" Text='<%#Eval("EMAIL") %>'/>
    </EditItemTemplate>
    
    <ControlStyle Width="120px" />
    <FooterStyle HorizontalAlign="Center" Width="120px" />
    <HeaderStyle HorizontalAlign="Center" Width="8%" />
    <ItemStyle HorizontalAlign="Center" Width="8%" />
    
</asp:TemplateField>

<asp:TemplateField HeaderText="Web">
    <ItemTemplate>
        <asp:Label ID="lblWeb" runat="server" Text='<%#Eval("WEBID") %>'/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtWebEdit" runat="server" Text='<%#Eval("WEBID") %>'/>
    </EditItemTemplate>
    
    <ControlStyle Width="100px" />
    <FooterStyle HorizontalAlign="Center" Width="100px" />
    <HeaderStyle HorizontalAlign="Center" Width="8%" />
    <ItemStyle HorizontalAlign="Center" Width="8%" />
    
</asp:TemplateField>
<asp:TemplateField HeaderText="Contact Person Name">
    <ItemTemplate>
        <asp:Label ID="lblCPNM" runat="server" Text='<%#Eval("CPNM") %>'/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtCPNMEdit" runat="server" Text='<%#Eval("CPNM") %>'/>
    </EditItemTemplate>
    
    <ControlStyle Width="110px" />
    <FooterStyle HorizontalAlign="Center" Width="110px" />
    <HeaderStyle HorizontalAlign="Center" Width="8%" />
    <ItemStyle HorizontalAlign="Center" Width="8%" />
    
</asp:TemplateField>
<asp:TemplateField HeaderText="Contact Person No">
    <ItemTemplate>
        <asp:Label ID="lblCPNO" runat="server" Text='<%#Eval("CPNO") %>'/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtCPNOEdit" runat="server" Text='<%#Eval("CPNO") %>'/>
    </EditItemTemplate>
    
    <ControlStyle Width="80px" />
    <FooterStyle HorizontalAlign="Center" Width="80px" />
    <HeaderStyle HorizontalAlign="Center" Width="6%" Wrap="True" />
    <ItemStyle HorizontalAlign="Center" Width="6%" />
    
</asp:TemplateField>
<asp:TemplateField HeaderText="Remarks">
    <ItemTemplate>
        <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("REMARKS") %>'/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtRemarksEdit" runat="server" Text='<%#Eval("REMARKS") %>'/>
    </EditItemTemplate>
    
    <ControlStyle Width="150px" />
    <FooterStyle HorizontalAlign="Center" Width="150px" />
    <HeaderStyle HorizontalAlign="Center" Width="5%" />
    <ItemStyle HorizontalAlign="Center" Width="5%" />
    
</asp:TemplateField>

<asp:TemplateField HeaderText="Status">
    <ItemTemplate>
        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("STATUS") %>'/>
    </ItemTemplate>

    <EditItemTemplate>

        <asp:DropDownList ID="ddlStatus" runat="server" Width="75px">
            <asp:ListItem Value="A">ACTIVE</asp:ListItem>
            <asp:ListItem Value="I">INACTIVE</asp:ListItem>
        </asp:DropDownList>

    </EditItemTemplate>
    
    <ControlStyle Width="80px" />
    <FooterStyle HorizontalAlign="Center" Width="80px" />
    <HeaderStyle HorizontalAlign="Center" Width="6%" />
    <ItemStyle HorizontalAlign="Center" Width="6%" />
    
</asp:TemplateField>

<asp:TemplateField Visible="false">
    <ItemTemplate>
        <asp:Label ID="lblPS_ID" runat="server" Text='<%#Eval("PS_ID") %>'/>
    </ItemTemplate>

    <EditItemTemplate>

        <asp:Label ID="lblPS_IDEdit" runat="server" Text='<%#Eval("PS_ID") %>'/>
    
    </EditItemTemplate>
    
    <HeaderStyle Width="2%" />
    <ItemStyle Width="2%" />
    
</asp:TemplateField>


 <asp:TemplateField>
     <ItemTemplate>
         <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" 
             Height="20px" ImageUrl="~/Images/Edit.jpg" ToolTip="Edit" Width="20px" />
         <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" 
             Height="20px" ImageUrl="~/Images/delete.jpg" ToolTip="Delete" Width="20px" OnClientClick="return confMSG()" />
    
    </ItemTemplate>

    <EditItemTemplate>
        <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" 
            Height="20px" ImageUrl="~/Images/update.jpg" ToolTip="Update" Width="20px" />
        <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" 
            Height="20px" ImageUrl="~/Images/Cancel.jpg" ToolTip="Cancel" Width="20px" />

    </EditItemTemplate>

     <HeaderStyle Width="10%" />
     <ItemStyle Width="10%" />

 </asp:TemplateField>

 </Columns>

<HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White" Font-Size="10pt" 
            HorizontalAlign="Center"></HeaderStyle>
        <RowStyle Font-Size="10px" Font-Names="Calibri" />
        <SelectedRowStyle BackColor="#006600" Font-Size="8pt" />
</asp:GridView>

        </div>
    </div>
    </form>
</body>
</html>