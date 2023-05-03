<%@ Page Title="Process" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Process.aspx.cs" Inherits="AlchemyAccounting.Accounts.UI.Process" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <title></title>
    <link rel="shortcut icon" href="../../Images/favicon.ico" />
    <link href="../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>

    <script type ="text/javascript">
        $(document).ready(function () {
            $("#txtDate").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+0" });
        });
    </script>

    <style type="text/css">
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
        #main_cont
        {
            float:left;
            width: 50%;
            height: 200px;
            background: #f2f2f2;
            border: 2px solid #cccccc;
            border-radius: 10px;
            margin: 0% 25% 0% 25%;
        }
        
        .style1
        {
            width: 127px;
            text-align: right;
        }
        .style2
        {
            width: 166px;
        }
        
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="header">
        <h1 align="center">Process</h1>
    </div>


        <div id="main_cont">
            <table style="width:100%;">
                <tr>
                    <td class="style2">
                        <asp:Label ID="lblSerial_Bill" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td class="style1">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:Label ID="lblSerial_Mrec" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblSerial_Jour" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSerial_BUY" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:Label ID="lblSerial_Mpay" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblSerial_Cont" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSerial_SALE" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2" style="text-align: right">
                        <strong>Date:</strong></td>
                    <td class="style1">
                        <asp:TextBox ID="txtDate" runat="server" Width="150px" AutoPostBack="True" 
                            ClientIDMode="Static" ontextchanged="txtDate_TextChanged" TabIndex="1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblSlSale_Dis" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2" style="text-align: right">
                        <asp:Label ID="lblSerial_JobRec" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: right">
                    </td>
                    <td>
                        <asp:Label ID="lblSerial_LC" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2" style="text-align: right">
                        <asp:Label ID="lblSerial_JourJobExp" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Button ID="btnProcess" runat="server" CssClass="ui-widget-shadow" 
                            Font-Bold="True" Font-Italic="True" Text="Process" 
                            onclick="btnProcess_Click" TabIndex="2" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                </table>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
            <asp:GridView ID="GridView2" runat="server">
            </asp:GridView>
            <asp:GridView ID="GridView3" runat="server">
            </asp:GridView>
            <asp:GridView ID="GridView4" runat="server">
            </asp:GridView>
            <asp:GridView ID="gridLC" runat="server">
            </asp:GridView>
            <asp:GridView ID="gridSale_Ret" runat="server">
            </asp:GridView>
            <asp:GridView ID="gridPurchase_Ret" runat="server">
            </asp:GridView>
            <asp:GridView ID="gridMultiple" runat="server">
            </asp:GridView>
            <asp:GridView ID="gvMicCollection" runat="server">
            </asp:GridView>
            <asp:GridView ID="gvMicCollectionMember" runat="server">
            </asp:GridView>
            <asp:GridView ID="gvJobExpense" runat="server">
            </asp:GridView>
            <asp:GridView ID="gvJobReceive" runat="server">
            </asp:GridView>
            <asp:GridView ID="gvBill" runat="server">
            </asp:GridView>
        </div>    


</asp:Content>
