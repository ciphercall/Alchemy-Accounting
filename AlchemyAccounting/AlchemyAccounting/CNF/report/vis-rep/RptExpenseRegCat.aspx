<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptExpenseRegCat.aspx.cs"
    Inherits="AlchemyAccounting.CNF.report.vis_rep.RptExpenseRegCat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <style media="print" type="text/css">
        .ShowHeader thead {
            display: table-header-group;
            border: 1px solid #000;
        }

        .line-separator {
            height: 1px;
            background: #717171;
            border-bottom: 1px solid #313030;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%">
                <tr>
                    <td style="width: 10%" rowspan="3"></td>
                    <td style="width: 80%; text-align: center; font-family: Calibri; font-size: 25px">
                        <asp:Label runat="server" ID="lblCompanyNM"></asp:Label>
                        &nbsp;
                    </td>
                    <td style="width: 10%">
                        <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()"
                            style="font-family: Calibri; font-size: 15px; font-weight: bold; font-style: inherit" />
                    </td>
                </tr>
            </table>
            <table style="width: 100%; margin: 1% 2% 0% 2%;">
                <tr>
                    <td colspan="2" style="width: 10%; text-align: left;font-size: 19PX; font-family: Calibri">
                        EXPENSE REGISTER
                    </td>

                    <td style="width: 10%; text-align: left;"></td>
                    <td style="width: 20%; text-align: left;">
                    </td>

                    <td style="width: 10%; text-align: left;">Print date :</td>
                    <td style="width: 20%; text-align: left;">
                        <asp:Label ID="lblPrintDate" runat="server" Font-Names="Calibri"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%; text-align: left;">Job NO :</td>
                    <td style="width: 30%; text-align: left;">
                        <asp:Label runat="server" ID="lblJobNo"
                            Style="font-family: Calibri; font-size: 14px"></asp:Label>
                    </td>

                    <td style="width: 10%; text-align: left;">Job Type :</td>
                    <td style="width: 20%; text-align: left;">
                        <asp:Label runat="server" ID="lblJobType"></asp:Label>
                    </td>

                    <td style="width: 10%; text-align: left;">Job Year :</td>
                    <td style="width: 20%; text-align: left;">
                        <asp:Label runat="server" ID="lblJobyr"></asp:Label>
                        <asp:Label runat="server" ID="lblInvoice" Font-Names="Calibri; font-size:14px;" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%; text-align: left;">Party Name :</td>
                    <td style="width: 30%; text-align: left;">
                        <asp:Label runat="server" ID="lblPartyNM" Font-Names="Calibri; font-size:14px;"></asp:Label>
                    </td>
                    <td style="width: 10%; text-align: left;">Qty :</td>
                    <td style="width: 20%; text-align: left;">
                        <asp:Label runat="server" ID="lblQty" Font-Names="Calibri; font-size:14px;"></asp:Label>
                    </td>

                    <td style="width: 10%; text-align: left;">Branch :</td>
                    <td style="width: 20%; text-align: left;">
                          <asp:Label runat="server" ID="lblBranch" Font-Names="Calibri;font-size:14px;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%; text-align: left;">Remarks :</td>
                    <td style="width: 30%; text-align: left;">
                         <asp:Label runat="server" ID="lblRemarks" Font-Names="Calibri; font-size:14px;"></asp:Label>
                    </td>

                    <td style="width: 10%; text-align: left;">L/C No :</td>
                    <td style="width: 20%; text-align: left;">
                         <asp:Label runat="server" ID="lblLcNo" Font-Names="Calibri; font-size:14px;"></asp:Label>
                    </td>

                    <td style="width: 10%; text-align: left;">Date :</td>
                    <td style="width: 20%; text-align: left;">
                         <asp:Label runat="server" ID="lblJobDate"
                            Style="font-family: Calibri; font-size: 14px"></asp:Label>
                    </td>
                </tr>
            </table>
            <div style="width: 96%; margin: 1% 2% 0% 2%;">
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" Font-Names="Calibri"
                    OnRowDataBound="gvReport_RowDataBound" ShowFooter="True" Width="100%" OnRowCreated="gvReport_RowCreated">
                    <Columns>
                        <asp:BoundField HeaderText="Expense Name" DataField="EXPNM">
                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Entry Qty" DataField="Qty">
                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Amount" DataField="EXPAMT">
                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                            <ItemStyle HorizontalAlign="Right" Width="15%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle Font-Size="14px" />
                    <HeaderStyle Font-Size="14px" />
                    <RowStyle Font-Size="12px" />
                </asp:GridView>
            </div>
            <table style="width: 100%">
                <tr>
                    <td style="width: 2%; text-align: left"></td>
                    <td style="width: 5%"></td>
                    <td style="width: 1%"></td>
                    <td style="width: 84%"></td>
                </tr>
                <tr>
                    <td style="width: 2%; text-align: left"></td>
                    <td style="width: 5%; text-align: right; font-family: Calibri; font-size: 14px; font-weight: bold">&nbsp;
                    </td>
                    <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px; font-weight: bold">&nbsp;
                    </td>
                    <td style="width: 84%">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 2%; text-align: left"></td>
                    <td style="width: 5%; text-align: left; font-family: Calibri; font-size: 14px; font-weight: bold"></td>
                    <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px; font-weight: bold"></td>
                    <td style="width: 84%">
                        <asp:Label ID="lblInWords" runat="server" Font-Names="Calibri" Font-Size="15px" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
