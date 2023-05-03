<%@ Page Title="L/C Wise Item Information" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="LCItemInfo.aspx.cs" Inherits="AlchemyAccounting.LC.UI.LCItemInfo" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<link href="../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>

    <script type ="text/javascript">
        $(document).ready(function () {
            $("#txtDate").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+0" });
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

    <style type ="text/css">
        #header
        {
            float: left;
            width:100%;
            background-color: transparent;
            height: 50px;
        }
        #header h1
        {
            font-family:Century Gothic;
            font-weight: bold;
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
            float:left;
            width:100%;
            height: 30px;
            background-color: #cccccc;
            border-radius: 10px 10px 0px 0px;
        }
        #head_row
        {
            float:left;
            width:100%;
        }
        #grid
        {
            float:left;
            width:100%;
        }
        .style1
        {
            width: 40px;
        }
        .style2
        {
            width: 45px;
        }
        .style3
        {
            width: 341px;
            text-align: right;
        }
        .Gridview
        {
        font-family:Verdana;
        font-size:10pt;
        font-weight:normal;
        color:black;

        }
        .style4
        {
            width: 230px;
        }
        .style5
        {
            width: 3px;
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
        .style6
        {
            width: 394px;
            text-align: left;
        }
        .style7
        {
            width: 506px;
            text-align: right;
        }
        .style8
        {
            width: 4px;
            text-align: right;
            font-weight: bold;
        }
        .style9
        {
            width: 49px;
        }
      </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div id="header">
        <h1 align="center">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>L/C Wise Item Information</h1>
     </div>

     <div id="entry">
        <div id="toolbar">
            <table style="width:100%;">
                <tr>
                    <td class="style3">
                        &nbsp;</td>
                    <td class="style1">
                        &nbsp;</td>
                    <td class="style2">
                        &nbsp;</td>
                    <td class="style12">
                        &nbsp;</td>
                    <td class="style13">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <div id="head_row">
            
            <table style="width:100%;">
                <tr>
                    <td class="style4">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style4">
                        &nbsp;</td>
                    <td style="text-align: right">
                        <strong>L/C ID</strong></td>
                    <td class="style5">
                        <strong>:</strong></td>
                    <td>
                        <asp:TextBox ID="txtLCName" runat="server" AutoPostBack="True" TabIndex="1" 
                            Width="350px" ontextchanged="txtLCName_TextChanged" CssClass="txtColor"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txtLCName_AutoCompleteExtender" runat="server" 
                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                            UseContextKey="True" ServiceMethod="GetLCListame" TargetControlID="txtLCName">
                        </asp:AutoCompleteExtender>
                        <asp:TextBox ID="txtLCCD" runat="server" TabIndex="10" Width="150px" 
                            ReadOnly="True"></asp:TextBox>
&nbsp;
                        <asp:Button ID="btnSubmit" runat="server" Font-Bold="True" Text="SUBMIT" 
                            TabIndex="2" onclick="btnSubmit_Click" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style4">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            
        </div>
        <div id="grid">
        
<asp:GridView ID="gvDetails" runat="server" 
        AutoGenerateColumns="False" CssClass="Gridview" HeaderStyle-BackColor="#61A6F8" 
ShowFooter="True" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" 
        onrowcancelingedit="gvDetails_RowCancelingEdit" 
        onrowdeleting="gvDetails_RowDeleting" onrowediting="gvDetails_RowEditing" 
        onrowupdating="gvDetails_RowUpdating" 
        onrowcommand="gvDetails_RowCommand"
        OnRowDataBound="gvDetails_RowDataBound" Width="100%">
     
<Columns>

<asp:TemplateField HeaderText="Item Name">
    <ItemTemplate>
        <asp:Label ID="lblItemNM" runat="server" Text='<%# Eval("ITEMNM") %>' 
            Width="98%"/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtItemNMEdit" runat="server" CssClass="txtColor" Text='<%#Eval("ITEMNM") %>' 
            Width="98%" TabIndex="9" ontextchanged="txtItemNMEdit_TextChanged" 
            AutoPostBack="True"/>
        <asp:AutoCompleteExtender ID="txtItemNMEdit_AutoCompleteExtender" runat="server" 
             MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" 
             UseContextKey="True" ServiceMethod="GetItemNameEdit" TargetControlID="txtItemNMEdit">
        </asp:AutoCompleteExtender>
    </EditItemTemplate>
    
    <FooterTemplate>
        <asp:TextBox ID="txtItemNM" runat="server" Width="98%" TabIndex="3" 
            CssClass="txtColor" AutoPostBack="True" 
            ontextchanged="txtItemNM_TextChanged"/>
        <asp:AutoCompleteExtender ID="txtItemNM_AutoCompleteExtender" runat="server" 
            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
            UseContextKey="True" ServiceMethod="GetItemName" TargetControlID="txtItemNM">
        </asp:AutoCompleteExtender>
    </FooterTemplate> 
    <FooterStyle HorizontalAlign="Left" Width="30%" />
    <HeaderStyle HorizontalAlign="Center" Width="30%" />
</asp:TemplateField>

 <asp:TemplateField HeaderText="Item ID">
 <%--<FooterTemplate>
 <asp:TextBox ID="txtftrDesignation" runat="server"/>
  <asp:RequiredFieldValidator ID="rfvdesignation" runat="server" ControlToValidate="txtftrDesignation" Text="*" ValidationGroup="validaiton"/>
 </FooterTemplate>--%>
     
    <ItemTemplate>
        <asp:Label ID="lblItemID" runat="server" Text='<%# Eval("ITEMID") %>' 
            Width="95%"/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtItemIDEdit" runat="server" CssClass="txtColor" 
            Text='<%#Eval("ITEMID") %>' Width="95%"
         ReadOnly="True" TabIndex="101"/>
    </EditItemTemplate>
     
    <FooterTemplate>

        <asp:TextBox ID="txtItemID" runat="server" Width="95%" ReadOnly="True" CssClass="txtColor"
            TabIndex="100"></asp:TextBox>

    </FooterTemplate>

     <FooterStyle HorizontalAlign="Center" Width="10%" />
     <HeaderStyle HorizontalAlign="Center" Width="10%" />
     <ItemStyle HorizontalAlign="Center" />
 </asp:TemplateField>

 <asp:TemplateField HeaderText="Qty">
     <ItemTemplate>
        <asp:Label ID="lblQty" runat="server" Text='<%# Eval("QTY") %>' 
             Width="95%" style="text-align: right"></asp:Label>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtQtyEdit" runat="server" CssClass="txtColor" 
            Text='<%# Eval("QTY") %>' Width="95%" TabIndex="10" style="text-align: right"></asp:TextBox>
    </EditItemTemplate>

     <FooterTemplate>

         <asp:TextBox ID="txtQty" runat="server" CssClass="txtColor" Width="95%" style="text-align: right" 
             TabIndex="4"></asp:TextBox>

     </FooterTemplate>

     <FooterStyle HorizontalAlign="Right" Width="10%" />

     <HeaderStyle HorizontalAlign="Center" Width="10%" />

 </asp:TemplateField>

    <asp:TemplateField HeaderText="Rate">
        <FooterTemplate>
            <asp:TextBox ID="txtRate" runat="server" CssClass="txtColor" Width="95%" style="text-align: right" 
                TabIndex="5" AutoPostBack="True" ontextchanged="txtRate_TextChanged">.00</asp:TextBox>
        </FooterTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="txtRateEdit" runat="server" CssClass="txtColor" Width="95%" 
                Text='<%#Eval("RATE") %>' style="text-align: right"
                TabIndex="11" AutoPostBack="True" ontextchanged="txtRateEdit_TextChanged"></asp:TextBox>
        </EditItemTemplate>
        <ItemTemplate>
            <asp:Label ID="lblRate" runat="server" Width="95%" Text='<%#Eval("RATE") %>' 
                style="text-align: right"></asp:Label>
        </ItemTemplate>
        <FooterStyle HorizontalAlign="Right" Width="10%" />
        <HeaderStyle HorizontalAlign="Center" Width="10%" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Amount">
        <FooterTemplate>
            <asp:TextBox ID="txtAmount" runat="server" CssClass="txtColor" style="text-align: right" 
                Width="95%" TabIndex="6" ReadOnly="True">.00</asp:TextBox>
        </FooterTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="txtAmountEdit" runat="server" CssClass="txtColor" Text='<%#Eval("AMOUNT") %>'
                style="text-align: right" 
                Width="95%" TabIndex="12" ReadOnly="True"></asp:TextBox>
        </EditItemTemplate>
        <ItemTemplate>
            <asp:Label ID="lblAmount" runat="server" Width="95%" 
                Text='<%#Eval("AMOUNT") %>' style="text-align: right"></asp:Label>
        </ItemTemplate>
        <FooterStyle HorizontalAlign="Right" Width="15%" />
        <HeaderStyle HorizontalAlign="Center" Width="15%" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Remarks">
        <FooterTemplate>
            <asp:TextBox ID="txtRemarks" CssClass="txtColor" runat="server" Width="95%" TabIndex="7"></asp:TextBox>
        </FooterTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="txtRemarksEdit" CssClass="txtColor" runat="server" Width="95%" 
                Text='<%#Eval("REMARKS") %>' TabIndex="13"></asp:TextBox>
        </EditItemTemplate>
        <ItemTemplate>
            <asp:Label ID="lblRemarks" runat="server" Width="95%" Text='<%#Eval("REMARKS") %>'></asp:Label>
        </ItemTemplate>
        <FooterStyle HorizontalAlign="Left" Width="20%" />
        <HeaderStyle HorizontalAlign="Center" Width="20%" />
    </asp:TemplateField>
    <asp:TemplateField Visible="False">
        <EditItemTemplate>
            <asp:Label ID="lblItemSLEdit" runat="server" Text='<%#Eval("SL") %>'></asp:Label>
        </EditItemTemplate>
        <ItemTemplate>
            <asp:Label ID="lblItemSl" runat="server" Text='<%#Eval("SL") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>

 <asp:TemplateField>
<EditItemTemplate>
<asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" 
        ImageUrl="~/Images/update.jpg" ToolTip="Update" Height="20px" Width="20px" 
        TabIndex="14" />
<asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" 
        ImageUrl="~/Images/Cancel.jpg" ToolTip="Cancel" Height="20px" Width="20px" 
        TabIndex="15" />

</EditItemTemplate>
<ItemTemplate>
<asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" 
        ImageUrl="~/Images/Edit.jpg" ToolTip="Edit" Height="20px" Width="20px" 
        TabIndex="16" />
<asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server" 
        ImageUrl="~/Images/delete.jpg" ToolTip="Delete" Height="20px" Width="20px" OnClientClick="return confMSG()"
        TabIndex="17" />
</ItemTemplate>
<FooterTemplate>
<asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/AddNewitem.jpg" 
        CommandName="AddNew" Width="30px" Height="30px" ToolTip="Add new Record" 
        ValidationGroup="validaiton" TabIndex="8" />

</FooterTemplate>
     <FooterStyle HorizontalAlign="Left" Width="10%" />
     <HeaderStyle HorizontalAlign="Center" Width="10%" />
 </asp:TemplateField>

 </Columns>

    <EditRowStyle BackColor="#999966" />

<HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White"></HeaderStyle>
</asp:GridView>



         </div>
         <%--<table style="width:100%;">
             <tr>
                 <td class="style9">
                     &nbsp;</td>
                 <td class="style7">
                     <strong>Total</strong></td>
                 <td class="style8">
                     :</td>
                 <td class="style6">
                     <asp:TextBox ID="txtTotal" runat="server"></asp:TextBox>
                 </td>
                 <td>
                     &nbsp;</td>
             </tr>
             <tr>
                 <td class="style9">
                     &nbsp;</td>
                 <td class="style7">
                     &nbsp;</td>
                 <td class="style8">
                     &nbsp;</td>
                 <td class="style6">
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
             </tr>
             <tr>
                 <td class="style9">
                     &nbsp;</td>
                 <td class="style7">
                     &nbsp;</td>
                 <td class="style8">
                     &nbsp;</td>
                 <td class="style6">
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
             </tr>
         </table>--%>
     </div>
</asp:Content>
