<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptBalanceSheet.aspx.cs" Inherits="AlchemyAccounting.Accounts.Report.Report.rptBalanceSheet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Balance Sheet</title>
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

        .ShowHeader thead
         {
            display: table-header-group;
            border: 1px solid #000;
         }

    </style>

    <style type="text/css">
        
        #btnPrint
        {
            font-weight: 700;
        }
        .GrandGrandTotalRowStyle
        {
            border: solid 1px Gray;
            color: #000000;
            font-weight: bold;
            font-size: 16px;
            font-family:Calibri;
            text-align: right;
            height: 30px;
        }
        .style1
        {
            text-align: center;
            width: 1085px;
            font-family: Calibri;
        }
        .style2
        {
            font-size: 16px;
        }
        .style13
        {
            text-align: center;
            width: 1085px;
            font-family: Calibri;
            font-size: 16px;
        }
        #print
        {
            font-family: Calibri;
            font-size: 15px;
        }
        .style14
        {
            font-family: Calibri;
            font-size: 16px;
        }
        .GroupHeaderStyle
        {
            height:30px;
        }
        .rowstyle
        {
            padding-left:5%;
        }
        .SubTotalRowStyle
        {
            font-weight:bold;
        }
        .style15
        {
            width: 336px;
        }
        .style16
        {
            width: 337px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table style="width: 100%;">
            <tr>
                <td class="style16">
                    &nbsp;</td>
                <td class="style1">
                    <asp:Label ID="lblCompNM" runat="server" 
                        style="font-size: 20px; font-weight: 700"></asp:Label>
                </td>
                <td style="text-align: right" class="style15">
                    <input id="print" tabindex="1" type="button" value="Print" onclick = "ClosePrint()"/></td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style16">
                    &nbsp;</td>
                <td class="style13">
                    <asp:Label ID="lblAddress" runat="server" 
                        style="font-family: Calibri; font-size: 10px"></asp:Label>
                </td>
                <td style="text-align: right" class="style15">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style16">
                    &nbsp;</td>
                <td class="style13">
                    <strong>BALANCE SHEET</strong></td>
                <td style="text-align: right" class="style15">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style16">
                    &nbsp;</td>
                <td class="style1">
                    <span class="style2">AS on Date : </span>
                    <asp:Label ID="lblDate" runat="server" CssClass="style2"></asp:Label>
                </td>
                <td style="text-align: right" class="style15">
                    <asp:Label ID="lblTime" runat="server" 
                        style="text-align: right; font-family: Calibri; font-size: 14px;"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>

            <div style = "width:96%; margin: 0% 2% 0% 2%; height: 1px; background: #000000;">
            </div>

            <table style = "width:96%; margin: 1% 2% 0% 2%;">
                <tr>
                    <td class="style14">
                        ASSET</td>
                </tr>
            </table>

                        <div class = "MyCssClass" style = "width:96%; margin: 1% 2% 0% 2%;">

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="GridView1_RowDataBound" Width="100%" 
                    ShowHeaderWhenEmpty="True" 
                    ShowFooter="True" onrowcreated="GridView1_RowCreated">
                    <Columns>
                        <asp:BoundField HeaderText="Item Particulars">
                        <HeaderStyle HorizontalAlign="Center" Width="75%" />
                        <ItemStyle HorizontalAlign="Left" Width="75%" CssClass="rowstyle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="25%" />
                        <ItemStyle HorizontalAlign="Right" Width="25%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle Font-Names="Calibri" Font-Size="16px" />
                    <HeaderStyle Font-Bold="True" Font-Names="Calibri" Font-Size="16px" />
                    <RowStyle Font-Size="14px" Font-Names="Calibri" />
                </asp:GridView>

            </div>

            <table style = "width:96%; margin: 1% 2% 0% 2%;">
                <tr>
                    <td class="style14">
                        LIABILITY</td>
                </tr>
            </table>

                        <div class = "MyCssClass" style = "width:96%; margin: 1% 2% 0% 2%;">

                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="GridView2_RowDataBound" Width="100%" 
                    ShowHeaderWhenEmpty="True" 
                    ShowFooter="True" onrowcreated="GridView2_RowCreated">
                    <Columns>
                        <asp:BoundField HeaderText="Item Particulars">
                        <HeaderStyle HorizontalAlign="Center" Width="75%" />
                        <ItemStyle HorizontalAlign="Left" Width="75%" CssClass="rowstyle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="25%" />
                        <ItemStyle HorizontalAlign="Right" Width="25%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle Font-Names="Calibri" Font-Size="16px" />
                    <HeaderStyle Font-Bold="True" Font-Names="Calibri" Font-Size="16px" />
                    <RowStyle Font-Size="14px" Font-Names="Calibri" />
                </asp:GridView>

            </div>

        </div>
    </div>
    </form>
</body>
</html>
