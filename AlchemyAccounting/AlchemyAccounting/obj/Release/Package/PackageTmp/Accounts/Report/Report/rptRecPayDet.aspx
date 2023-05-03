<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptRecPayDet.aspx.cs" Inherits="AlchemyAccounting.Accounts.Report.Report.rptRecPayDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="shortcut icon" href="../../../Images/favicon.ico" />
    <link href="../../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>

    <script type ="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            //            print.display = false;

            window.print();
        }
    </script>
    
    <style media="print">

        .MyCssClass thead
         {
            display: table-header-group;
            border: 1px solid #000;
         }

    </style>

    <style type ="text/css">
    #btnPrint
        {
            font-weight: 700;
        }
            .style1
            {
                font-size: small;
            text-align: center;
            width: 908px;
        }
            .style2
            {
            font-size: 14px;
            font-family: Calibri;
        }
            
        .SubTotalRowStyle
        {
            border: solid 1px Black;
            font-weight: bold;
            text-align: right;
            font-family:Calibri;
            font-size: 14px;
            height:25px;
        }
        .GrandTotalRowStyle
        {
            border: solid 1px Gray;
            color: #000000;
            font-weight: bold;
            font-size: 16px;
            text-align: right;
            height: 28px;
        }
        .GroupHeaderStyle
        {
            border: solid 1px Black;
            text-align: left;
            color: #000000;
            height: 28px;
            text-decoration: underline;
            font-size:15px;
            font-weight: bold;
        }
        .GridRowStyle
        {
        }
        .style8
        {
            text-align: center;
            width: 908px;
        }
        .style10
        {
            width: 142px;
        }
        .style11
        {
            font-size: medium;
            text-align: center;
            width: 908px;
            font-family: Calibri;
        }
        
        .GrandGrandTotalRowStyle
        {
            border: solid 1px Gray;
            color: #000000;
            font-weight: bold;
            font-size: 16px;
            text-align: right;
            height: 30px;
            font-family:Calibri;
        }
        .style12
        {
            font-family: Calibri;
        }
        .style14
        {
            font-size: 14px;
            text-align: center;
            width: 908px;
            font-family: Calibri;
        }
        .footer
        {
            font-size: 14px;
            font-family: Calibri;
            font-weight: bold;
        }
        .style15
        {
            font-size: 14px;
        }
        </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table style="width: 100%;">
            <tr>
                <td class="style10">
                    &nbsp;</td>
                <td class="style1">
                    <asp:Label ID="lblCompNM" runat="server" 
                        style="font-family: Calibri; font-size: 18px; font-weight: 700"></asp:Label>
                </td>
                <td style="text-align: right">
                    <input id="print" tabindex="1" type="button" value="Print" onclick = "ClosePrint()"/></td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style10">
                    &nbsp;</td>
                <td class="style11">
                    <asp:Label ID="lblAddress" runat="server" 
                        style="font-family: Calibri; font-size: 9px"></asp:Label>
                </td>
                <td style="text-align: right">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style10">
                    &nbsp;</td>
                <td class="style14">
                    <strong>RECEIPTS &amp; PAYMENT STATEMENT - DETAILS</strong></td>
                <td style="text-align: right">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style10">
                    &nbsp;</td>
                <td class="style8">
                    <span class="style2">FROM </span> <strong><span class="style2">:&nbsp; 
                    </span> </strong>
                    <asp:Label ID="lblFDate" runat="server" CssClass="style2"></asp:Label>
                    <span class="style12"><span class="style15">&nbsp;&nbsp;&nbsp; TO </span> <strong>
                    <span class="style15">:&nbsp; </span> </strong>
                    </span>
                    <asp:Label ID="lblTDate" runat="server" CssClass="style2"></asp:Label>
                </td>
                <td style="text-align: right">
                    <asp:Label ID="lblTime" runat="server" 
                        style="text-align: right; font-family: Calibri; font-size: 14px;"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
            <table style="width:96%; margin: 1% 2% 0% 2%;">
                <tr>
                    <td>
                        <strong>
                        <asp:Label ID="lblOpening" runat="server" 
                            style="font-family: Calibri; " CssClass="style15"></asp:Label>
                        </strong>
                    </td>
                </tr>
            </table>
            <div class = "MyCssClass" style = "width:96%; margin: 1% 2% 0% 2%;">

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="GridView1_RowDataBound" Width="100%" 
                    ShowHeaderWhenEmpty="True" ShowFooter="True">
                    <Columns>
<asp:BoundField HeaderText="Date">
<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
</asp:BoundField>
<asp:BoundField HeaderText="Doc. No">
<HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Width="8%"></ItemStyle>
</asp:BoundField>
                        <asp:BoundField HeaderText="Debit" >
                        <HeaderStyle HorizontalAlign="Center" Width="22%" />
                        <ItemStyle HorizontalAlign="Left" Width="22%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Credit">
                        <HeaderStyle HorizontalAlign="Center" Width="22%" />
                        <ItemStyle HorizontalAlign="Left" Width="22%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Remarks">
                        <HeaderStyle HorizontalAlign="Center" Width="19%" />
                        <ItemStyle HorizontalAlign="Left" Width="19%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Debit Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Credit Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                        <ItemStyle HorizontalAlign="Left" Width="12%" CssClass="GridRowStyle" />
                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle CssClass="footer" />
                    <HeaderStyle Font-Names="Calibri" Font-Size="14px" />
                    <RowStyle Font-Size="12px" Font-Names="Calibri" />
                </asp:GridView>

            </div>

            <table style="width:96%; margin: 2% 2% 0% 2%;">
                <tr>
                    <td>
                        <strong>
                        <asp:Label ID="lblReceive" runat="server" 
                            style="font-family: Calibri; " CssClass="style15"></asp:Label>
                        </strong>
                    </td>
                </tr>
            </table>
            <div class = "MyCssClass" style = "width:96%; margin: 1% 2% 0% 2%;">

                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="GridView2_RowDataBound" Width="100%" 
                    ShowHeaderWhenEmpty="True" ShowFooter="True">
                    <Columns>
<asp:BoundField HeaderText="Date">
<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
</asp:BoundField>
<asp:BoundField HeaderText="Doc. No">
<HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Width="8%"></ItemStyle>
</asp:BoundField>
                        <asp:BoundField HeaderText="Debit" >
                        <HeaderStyle HorizontalAlign="Center" Width="22%" />
                        <ItemStyle HorizontalAlign="Left" Width="22%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Credit">
                        <HeaderStyle HorizontalAlign="Center" Width="22%" />
                        <ItemStyle HorizontalAlign="Left" Width="22%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Remarks">
                        <HeaderStyle HorizontalAlign="Center" Width="19%" />
                        <ItemStyle HorizontalAlign="Left" Width="19%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Debit Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Credit Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                        <ItemStyle HorizontalAlign="Left" Width="12%" CssClass="GridRowStyle" />
                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle CssClass="footer" />
                    <HeaderStyle Font-Names="Calibri" Font-Size="14px" />
                    <RowStyle Font-Size="12px" Font-Names="Calibri" />
                </asp:GridView>

            </div>

            <table style="width:96%; margin: 2% 2% 0% 2%;">
                <tr>
                    <td>
                        <strong>
                        <asp:Label ID="lblPayment" runat="server" 
                            style="font-family: Calibri; " CssClass="style15"></asp:Label>
                        </strong>
                    </td>
                </tr>
            </table>
            <div class = "MyCssClass" style = "width:96%; margin: 1% 2% 0% 2%;">

                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="GridView3_RowDataBound" Width="100%" 
                    ShowHeaderWhenEmpty="True" ShowFooter="True">
                    <Columns>
<asp:BoundField HeaderText="Date">
<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
</asp:BoundField>
<asp:BoundField HeaderText="Doc. No">
<HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Width="8%"></ItemStyle>
</asp:BoundField>
                        <asp:BoundField HeaderText="Debit" >
                        <HeaderStyle HorizontalAlign="Center" Width="22%" />
                        <ItemStyle HorizontalAlign="Left" Width="22%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Credit">
                        <HeaderStyle HorizontalAlign="Center" Width="22%" />
                        <ItemStyle HorizontalAlign="Left" Width="22%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Remarks">
                        <HeaderStyle HorizontalAlign="Center" Width="19%" />
                        <ItemStyle HorizontalAlign="Left" Width="19%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Debit Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Credit Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                        <ItemStyle HorizontalAlign="Left" Width="12%" CssClass="GridRowStyle" />
                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle CssClass="footer" />
                    <HeaderStyle Font-Names="Calibri" Font-Size="14px" />
                    <RowStyle Font-Size="12px" Font-Names="Calibri" />
                </asp:GridView>

            </div>

            <table style="width:96%; margin: 2% 2% 0% 2%;">
                <tr>
                    <td>
                        <strong>
                        <asp:Label ID="lblTransfer" runat="server" 
                            style="font-family: Calibri; " CssClass="style15"></asp:Label>
                        </strong>
                    </td>
                </tr>
            </table>
            <div class = "MyCssClass" style = "width:96%; margin: 1% 2% 0% 2%;">

                <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="GridView6_RowDataBound" Width="100%" 
                    ShowHeaderWhenEmpty="True" ShowFooter="True">
                    <Columns>
<asp:BoundField HeaderText="Date">
<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
</asp:BoundField>
<asp:BoundField HeaderText="Doc. No">
<HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Width="8%"></ItemStyle>
</asp:BoundField>
                        <asp:BoundField HeaderText="Debit" >
                        <HeaderStyle HorizontalAlign="Center" Width="22%" />
                        <ItemStyle HorizontalAlign="Left" Width="22%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Credit">
                        <HeaderStyle HorizontalAlign="Center" Width="22%" />
                        <ItemStyle HorizontalAlign="Left" Width="22%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Remarks">
                        <HeaderStyle HorizontalAlign="Center" Width="19%" />
                        <ItemStyle HorizontalAlign="Left" Width="19%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Debit Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Credit Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                        <ItemStyle HorizontalAlign="Left" Width="12%" CssClass="GridRowStyle" />
                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle CssClass="footer" />
                    <HeaderStyle Font-Names="Calibri" Font-Size="14px" />
                    <RowStyle Font-Size="12px" Font-Names="Calibri" />
                </asp:GridView>

            </div>

            <table style="width:96%; margin: 2% 2% 0% 2%;">
                <tr>
                    <td>
                        <strong>
                        <asp:Label ID="lblClosing" runat="server" 
                            style="font-family: Calibri; " CssClass="style15"></asp:Label>
                        </strong>
                    </td>
                </tr>
            </table>
            <div class = "MyCssClass" style = "width:96%; margin: 1% 2% 0% 2%;">

                <div style="width:100%; margin-top: 5px;">
                    
                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="GridView4_RowDataBound" Width="100%" 
                    ShowHeaderWhenEmpty="True" ShowFooter="True">
                    <Columns>
<asp:BoundField HeaderText="Date">
<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
</asp:BoundField>
<asp:BoundField HeaderText="Doc. No">
<HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Width="8%"></ItemStyle>
</asp:BoundField>
                        <asp:BoundField HeaderText="Debit" >
                        <HeaderStyle HorizontalAlign="Center" Width="22%" />
                        <ItemStyle HorizontalAlign="Left" Width="22%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Credit">
                        <HeaderStyle HorizontalAlign="Center" Width="22%" />
                        <ItemStyle HorizontalAlign="Left" Width="22%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Remarks">
                        <HeaderStyle HorizontalAlign="Center" Width="19%" />
                        <ItemStyle HorizontalAlign="Left" Width="19%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Debit Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Credit Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                        <ItemStyle HorizontalAlign="Left" Width="12%" CssClass="GridRowStyle" />
                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle CssClass="footer" />
                    <HeaderStyle Font-Names="Calibri" Font-Size="14px" />
                    <RowStyle Font-Size="12px" Font-Names="Calibri" />
                </asp:GridView>

                    <table style="width:100%; border: 1px solid #000; margin-top: 5px;">
                        <tr>
                            <td style="width:5%">
                                &nbsp;</td>
                            <td style="width:8%">
                                &nbsp;</td>
                            <td style="width:33%">
                                &nbsp;</td>
                            <td style="width:30%; text-align: right;" class="style16">
                                <strong>Gand Total :</strong></td>
                            <td style="width:12%; text-align: right;">
                                <asp:Label ID="lblGrDrAmt" runat="server" 
                                    style="font-weight: 700; font-family: Calibri; font-size: 16px" Text="0.00"></asp:Label>
                            </td>
                            <td style="width:12%; text-align: right;">
                                <asp:Label ID="lblGrCrAmt" runat="server" 
                                    style="font-weight: 700; font-family: Calibri; font-size: 16px" Text="0.00"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    
                </div>

            </div>

            <table style="width:96%; margin: 2% 2% 0% 2%;">
                <tr>
                    <td>
                        <strong>
                        <asp:Label ID="lblNonCash" runat="server" 
                            style="font-family: Calibri; " CssClass="style15"></asp:Label>
                        </strong>
                    </td>
                </tr>
            </table>

            <div class = "MyCssClass" style = "width:96%; margin: 1% 2% 0% 2%;">

                <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="GridView5_RowDataBound" Width="100%" 
                    ShowHeaderWhenEmpty="True" ShowFooter="True">
                    <Columns>
<asp:BoundField HeaderText="Date">
<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
</asp:BoundField>
<asp:BoundField HeaderText="JV No">
<HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
</asp:BoundField>
                        <asp:BoundField HeaderText="Debited To" >
                        <HeaderStyle HorizontalAlign="Center" Width="25%" />
                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Credited To">
                        <HeaderStyle HorizontalAlign="Center" Width="25%" />
                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Remarks">
                        <HeaderStyle HorizontalAlign="Center" Width="25%" />
                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle CssClass="footer" />
                    <HeaderStyle Font-Names="Calibri" Font-Size="14px" />
                    <RowStyle Font-Size="12px" Font-Names="Calibri" />
                </asp:GridView>

            </div>

        </div>
    </div>
    </form>
</body>
</html>
