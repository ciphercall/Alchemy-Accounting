<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptTrialBalance.aspx.cs" Inherits="AlchemyAccounting.Accounts.Report.Report.rptTrialBalance" %>

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
            width: 922px;
            font-family: Calibri;
        }
        .style2
        {
            font-size: 16px;
        }
        .style12
        {
            width: 109px;
        }
        .style14
        {
            text-align: center;
            width: 922px;
            font-family: Calibri;
            font-size: 20px;
            font-weight: 700;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table style="width: 100%;">
            <tr>
                <td class="style12">
                    &nbsp;</td>
                <td class="style14">
                    <asp:Label ID="lblCompNM" runat="server"></asp:Label>
                </td>
                <td style="text-align: right">
                    <input id="print" tabindex="1" type="button" value="Print" onclick = "ClosePrint()"/></td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style12">
                    &nbsp;</td>
                <td class="style1">
                    <asp:Label ID="lblAddress" runat="server" 
                        style="font-family: Calibri; font-size: 9px"></asp:Label>
                </td>
                <td style="text-align: right">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style12">
                    &nbsp;</td>
                <td class="style1">
                    <span class="style2"><strong>TRIAL BALANCE AS AT </strong> </span><strong>
                    <asp:Label ID="lblDate" runat="server" CssClass="style2"></asp:Label>
                    </strong>
                </td>
                <td style="text-align: right">
                    <asp:Label ID="lblTime" runat="server" 
                        style="text-align: right; font-family: Calibri; font-size: 14px;"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>



                        <div class = "MyCssClass" style = "width:96%; margin: 1% 2% 0% 2%;">

                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                Font-Size="12px" onrowdatabound="GridView1_RowDataBound" ShowFooter="True" 
                                ShowHeaderWhenEmpty="True" Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="ACCOUNT CODE">
                                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="PARTICULARS">
                                    <HeaderStyle HorizontalAlign="Center" Width="45%" />
                                    <ItemStyle HorizontalAlign="Left" Width="45%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="DEBIT AMOUNT">
                                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                    <ItemStyle HorizontalAlign="Right" Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="CREDIT AMOUNT">
                                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                    <ItemStyle HorizontalAlign="Right" Width="20%" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle CssClass="GrandGrandTotalRowStyle" Font-Names="Calibri" 
                                    Font-Size="16px" />
                                <HeaderStyle Font-Names="Calibri" Font-Size="16px" />
                                <RowStyle Font-Names="Calibri" Font-Size="14px" />
                            </asp:GridView>

            </div>

        </div>
    </div>
    </form>
</body>
</html>
