<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptTransactionList.aspx.cs" Inherits="AlchemyAccounting.Accounts.Report.Report.rptTransactionList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

        .showHeader thead
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
            }
            .style2
            {
                font-size: medium;
            }
            
        .SubTotalRowStyle
        {
            border: solid 1px Black;
            
            font-weight: bold;
            text-align: right;
        }
        .GrandTotalRowStyle
        {
            border: solid 1px Gray;
            color: #000000;
            font-weight: bold;
            font-size: 18px;
            text-align: right;
            height: 35px;
        }
        .GroupHeaderStyle
        {
            border: solid 1px Black;
            text-align: left;
            color: #000000;
            font-weight: bold;
            height: 30px;
        }
        .GridRowStyle
        {
        }
        .style3
        {
            font-family: Calibri;
        }
        .style8
        {
            font-family: Calibri;
            font-size: medium;
        }
        #print
        {
            font-family: Calibri;
        }
      </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table style="width: 100%;">
            <tr>
                <td class="style6">
                    &nbsp;</td>
                <td class="style1">
                    <asp:Label ID="lblCompNM" runat="server" 
                        style="font-family: Calibri; font-size: 20px; font-weight: 700"></asp:Label>
                </td>
                <td class="style5" style="text-align: right">
                    &nbsp;</td>
                <td style="text-align: right">
                    <input id="print" tabindex="1" type="button" value="Print" onclick = "ClosePrint()"/></td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style9">
                    <asp:Label ID="lblAddress" runat="server" 
                        style="font-family: Calibri; font-size: 9px"></asp:Label>
                </td>
                <td class="style5">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style3">
                    <strong style="font-size: medium">TRANSACTION LISTING</strong></td>
                <td class="style5">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style8">
                    <span class="style2">FROM </span> <strong><span class="style2">:&nbsp; 
                    </span> </strong>
                    <asp:Label ID="lblFDate" runat="server" CssClass="style2"></asp:Label>
                &nbsp;&nbsp;&nbsp; TO <strong>:&nbsp; </strong>
                    <asp:Label ID="lblTDate" runat="server" CssClass="style2"></asp:Label>
                </td>
                <td class="style5">
                    &nbsp;</td>
                <td style="text-align: right">
                    <asp:Label ID="lblTime" runat="server" 
                        style="text-align: right; font-family: Calibri; font-size: 14px;"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
            <div style = "width:96%; margin: 0% 2% 0% 2%; height: 1px; background: #000000;">
            </div>
            <table style="width:96%; margin: 1% 2% 0% 2%;">
                <tr>
                    <td>
                        <strong><asp:Label ID="lblCreditHD" runat="server" 
                            style="font-family: Calibri; font-size: 15px"></asp:Label>
                        </strong>
                    </td>
                </tr>
            </table>
            <div class="showHeader" style = "width:96%; margin: 0% 2% 0% 2%;">

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="GridView1_RowDataBound" Width="100%" ShowFooter="True">
                    <Columns>
                        <asp:BoundField HeaderText="Date" >
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle Width="10%" CssClass="GridRowStyle" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="V. No">
                        <HeaderStyle HorizontalAlign="Center" Width="9%" />
                        <ItemStyle HorizontalAlign="Center" Width="9%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Debit Head">
                        <HeaderStyle HorizontalAlign="Center" Width="33%" />
                        <ItemStyle HorizontalAlign="Left" Width="33%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Credit Head" >
                        <HeaderStyle HorizontalAlign="Center" Width="33%" />
                        <ItemStyle HorizontalAlign="Left" Width="33%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Amount" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle Font-Names="Calibri" Font-Size="16px" />
                    <HeaderStyle Font-Names="Calibri" Font-Size="16px" />
                    <RowStyle Font-Size="14px" Font-Names="Calibri" />
                </asp:GridView>

            </div>

            <table style="width:96%; margin: 1% 2% 0% 2%;">
                <tr>
                    <td>
                        <strong><asp:Label ID="lblDebitHD" runat="server" 
                            style="font-family: Calibri; font-size: 15px"></asp:Label>
                        </strong>
                    </td>
                </tr>
            </table>
            <div class="showHeader" style = "width:96%; margin: 0% 2% 0% 2%;">

                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="GridView2_RowDataBound" Width="100%" ShowFooter="True">
                    <Columns>
                        <asp:BoundField HeaderText="Date" >
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle Width="10%" CssClass="GridRowStyle" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="V. No">
                        <HeaderStyle HorizontalAlign="Center" Width="9%" />
                        <ItemStyle HorizontalAlign="Center" Width="9%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Debit Head">
                        <HeaderStyle HorizontalAlign="Center" Width="33%" />
                        <ItemStyle HorizontalAlign="Left" Width="33%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Credit Head" >
                        <HeaderStyle HorizontalAlign="Center" Width="33%" />
                        <ItemStyle HorizontalAlign="Left" Width="33%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Amount" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle Font-Names="Calibri" Font-Size="16px" />
                    <HeaderStyle Font-Names="Calibri" Font-Size="16px" />
                    <RowStyle Font-Size="14px" Font-Names="Calibri" />
                </asp:GridView>

            </div>

            <table style="width:96%; margin: 1% 2% 0% 2%;">
                <tr>
                    <td>
                        <strong><asp:Label ID="lblJournalHD" runat="server" 
                            style="font-family: Calibri; font-size: 15px"></asp:Label>
                        </strong>
                    </td>
                </tr>
            </table>
            <div class="showHeader" style = "width:96%; margin: 0% 2% 0% 2%;">

                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="GridView3_RowDataBound" Width="100%" ShowFooter="True">
                    <Columns>
                        <asp:BoundField HeaderText="Date" >
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle Width="10%" CssClass="GridRowStyle" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="V. No">
                        <HeaderStyle HorizontalAlign="Center" Width="9%" />
                        <ItemStyle HorizontalAlign="Center" Width="9%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Debit Head">
                        <HeaderStyle HorizontalAlign="Center" Width="33%" />
                        <ItemStyle HorizontalAlign="Left" Width="33%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Credit Head" >
                        <HeaderStyle HorizontalAlign="Center" Width="33%" />
                        <ItemStyle HorizontalAlign="Left" Width="33%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Amount" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle Font-Names="Calibri" Font-Size="16px" />
                    <HeaderStyle Font-Names="Calibri" Font-Size="16px" />
                    <RowStyle Font-Size="14px" Font-Names="Calibri" />
                </asp:GridView>

            </div>

            <table style="width:96%; margin: 1% 2% 0% 2%;">
                <tr>
                    <td>
                        <strong><asp:Label ID="lblContraHD" runat="server" 
                            style="font-family: Calibri; font-size: 15px"></asp:Label>
                        </strong>
                    </td>
                </tr>
            </table>
            <div class="showHeader" style = "width:96%; margin: 0% 2% 0% 2%;">

                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="GridView4_RowDataBound" Width="100%" ShowFooter="True">
                    <Columns>
                        <asp:BoundField HeaderText="Date" >
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle Width="10%" CssClass="GridRowStyle" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="V. No">
                        <HeaderStyle HorizontalAlign="Center" Width="9%" />
                        <ItemStyle HorizontalAlign="Center" Width="9%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Debit Head">
                        <HeaderStyle HorizontalAlign="Center" Width="33%" />
                        <ItemStyle HorizontalAlign="Left" Width="33%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Credit Head" >
                        <HeaderStyle HorizontalAlign="Center" Width="33%" />
                        <ItemStyle HorizontalAlign="Left" Width="33%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Amount" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle Font-Names="Calibri" Font-Size="16px" />
                    <HeaderStyle Font-Names="Calibri" Font-Size="16px" />
                    <RowStyle Font-Size="14px" Font-Names="Calibri" />
                </asp:GridView>

            </div>
        </div>
    </div>
    </form>
</body>
</html>