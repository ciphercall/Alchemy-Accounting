<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt-Periodic-Detailed-Performace-Bill-Date-Wise.aspx.cs" Inherits="AlchemyAccounting.CNF.report.vis_rep.rpt_Periodic_Detailed_Performace_Bill_Date_Wise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title></title>
    <link rel="shortcut icon" href="../../../Images/favicon.ico" />
    <link href="../../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>

    <script type="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            //            print.display = false;

            window.print();
        }
    </script>

    <style media="print">
        .MyCssClass thead {
            display: table-header-group;
            border: 1px solid #000;
        }
    </style>

    <style type="text/css">
        #btnPrint {
            font-weight: 700;
        }

        .style1 {
            font-size: small;
            text-align: center;
            width: 908px;
        }

        .style2 {
            font-size: medium;
            font-family: Calibri;
        }

        .SubTotalRowStyle {
            border: solid 1px Black;
            font-weight: bold;
            text-align: right;
            font-family: Calibri;
            font-size: 14px;
            height: 25px;
        }

        .GrandTotalRowStyle {
            border: solid 1px Gray;
            color: #000000;
            font-weight: bold;
            font-size: 16px;
            text-align: right;
            height: 28px;
        }

        .GroupHeaderStyle {
            border: solid 1px Black;
            text-align: left;
            color: #000000;
            height: 28px;
            text-decoration: underline;
            font-size: 15px;
            font-weight: bold;
        }

        .GridRowStyle {
            padding-left: 10%;
        }

        .style8 {
            text-align: center;
            width: 908px;
        }

        .style10 {
            width: 142px;
        }

        .style11 {
            font-size: medium;
            text-align: center;
            width: 908px;
            font-family: Calibri;
        }

        .GrandGrandTotalRowStyle {
            border: solid 1px Gray;
            color: #000000;
            font-weight: bold;
            font-size: 16px;
            text-align: right;
            height: 30px;
            font-family: Calibri;
        }

        .style12 {
            font-family: Calibri;
        }

        .style13 {
            font-size: medium;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table style="width: 100%; border-bottom: 1px solid">
                <tr>
                    <td class="style10">&nbsp;</td>
                    <td class="style1">
                        <asp:Label ID="lblCompNM" runat="server"
                            Style="font-family: Calibri; font-size: 20px; font-weight: 700"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()" /></td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>
                <tr>
                    <td class="style10">&nbsp;</td>
                    <td class="style11">
                        <asp:Label ID="lblAddress" runat="server"
                            Style="font-family: Calibri; font-size: 9px"></asp:Label>
                    </td>
                    <td style="text-align: right">&nbsp;</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>
                <tr>
                    <td class="style10">&nbsp;</td>
                    <td class="style11">
                        <strong>Receipts Periodic Bill Date Wise Detailed Performace Report of C&F (Sea+Air+Local) Job</strong></td>
                    <td style="text-align: right">&nbsp;</td>
                    <td style="text-align: right">&nbsp;</td>
                </tr>
                <tr>
                    <td class="style10">&nbsp;</td>
                    <td class="style8">
                        <span class="style2">Peroid: FROM </span><strong><span class="style2">:&nbsp; 
                        </span></strong>
                        <asp:Label ID="lblFDate" runat="server" CssClass="style2"></asp:Label>
                        <span class="style12"><span class="style13">&nbsp;&nbsp;&nbsp; TO </span><strong>
                            <span class="style13">:&nbsp; </span></strong>
                        </span>
                        <asp:Label ID="lblTDate" runat="server" CssClass="style2"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblTime" runat="server"
                            Style="text-align: right; font-family: Calibri; font-size: 14px;"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
               
            </table>
            <table width="100%">
                 <tr>
                    <td style="width: 5%">Station</td>
                    <td style="width: 1%">:</td>
                    <td style="width: 30%;">
                        <asp:Label runat="server" ID="lblSationName"></asp:Label>
                    </td>
                      <td style="width: 5%">Job Type</td>
                    <td style="width: 1%">:</td>
                    <td style="width: 30%">
                        <asp:Label runat="server" ID="lblJobType"></asp:Label>
                    </td>
                     <td style="width: 28%"></td>
                </tr>
            </table>
                 <div class = "MyCssClass" style = "width:96%; margin: 1% 2% 0% 2%;">

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    onrowcreated="GridView1_RowCreated" 
                    onrowdatabound="GridView1_RowDataBound" Width="100%" 
                    ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:BoundField HeaderText="SL" >
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemStyle Width="5%" HorizontalAlign="Center"/>
                        </asp:BoundField>
                         <asp:BoundField HeaderText="JOB NO" >
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle Width="10%" HorizontalAlign="Center"/>
                        </asp:BoundField>
                         <asp:BoundField HeaderText="REG. ID" >
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle Width="10%" HorizontalAlign="Center"/>
                        </asp:BoundField>
                         <asp:BoundField HeaderText="JOB DATE" >
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle Width="10%" HorizontalAlign="Center"/>
                        </asp:BoundField>
                         <asp:BoundField HeaderText="BILL BO" >
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemStyle Width="5%" HorizontalAlign="Center"/>
                        </asp:BoundField>
                         <asp:BoundField HeaderText="BILL DATE" >
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle Width="10%" HorizontalAlign="Center"/>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="BILL AMOUNT">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                        </asp:BoundField>
                         <asp:BoundField HeaderText="EXP AMOUNT">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                        </asp:BoundField>
                         <asp:BoundField HeaderText="P/L AMOUNT">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                        </asp:BoundField>
                         <asp:BoundField HeaderText="RCV AMOUNT">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="O/S DUE">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle Font-Names="Calibri" Font-Size="16px" />
                    <RowStyle Font-Size="14px" Font-Names="Calibri" />
                </asp:GridView>

            </div>
        </div>
    </form>
</body>
</html>
