<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptExpense-VR-mini.aspx.cs" Inherits="AlchemyAccounting.CNF.report.vis_rep.RptExpense_VR_mini" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../css2/bootstrap.css" rel="stylesheet" />
    <script type="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            //            print.display = false;

            window.print();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="height: 5.75in; width: 4in; ">
            <div style="padding: 12px">

                <div style="text-align: center; font-family: cambria; font-size: 12px">
                    <%--<img width="200px" height="40px" src="../../../Images/logo.png" />--%>
                    <strong>Trade Abraod</strong>
                </div>
                <div style="text-align: center; font-size: 9px; font-family: Calibri">
                    <asp:Label ID="lblAddress" runat="server" Visible="False" Style="font-family: Calibri; font-size: 9px"></asp:Label>

                </div>
                <div style="padding-top: 10px">
                    <table style="font-size: 9px; width: 100%">
                        <tr>
                            <td style="width: 20%; text-align: left">Date</td>
                            <td style="width: 5%">&nbsp;:</td>
                            <td style="width: 67%">
                                <asp:Label ID="lbldt" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 20%; text-align: left">Invoice No</td>
                            <td style="width: 5%">&nbsp;:</td>
                            <td style="width: 67%">
                                <asp:Label ID="lblInvoiceNo" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 20%; text-align: left">Job No</td>
                            <td style="width: 5%">&nbsp;:</td>
                            <td style="width: 67%">
                                <asp:Label ID="lblJobNo" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 20%; text-align: left">Party</td>
                            <td style="width: 5%">&nbsp;:</td>
                            <td style="width: 67%">
                                <asp:Label ID="lblparty" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 20%; text-align: left">Expense By</td>
                            <td style="width: 5%">&nbsp;:</td>
                            <td style="width: 67%">
                                <asp:Label ID="lblExpenseBy" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 20%; text-align: left">Goods Desc.</td>
                            <td style="width: 5%">&nbsp;:</td>
                            <td style="width: 67%">
                                <asp:Label ID="lblGoods" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 20%; text-align: left">Packages</td>
                            <td style="width: 5%">&nbsp;:</td>
                            <td style="width: 67%">
                                <asp:Label ID="lblPackages" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 20%; text-align: left">Remarks</td>
                            <td style="width: 5%">&nbsp;:</td>
                            <td style="width: 67%">
                                <asp:Label ID="lblRemarks" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </div>
                <div style="width: 100%">
                    <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" Font-Names="Calibri"
                        OnRowDataBound="gvReport_RowDataBound" ShowFooter="True" Width="100%">
                        <Columns>
                            <asp:BoundField HeaderText="SL">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Particulars">
                                <HeaderStyle HorizontalAlign="Center" Width="45%" />
                                <ItemStyle HorizontalAlign="Left" Width="45%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Remarks">
                                <HeaderStyle HorizontalAlign="Center" Width="40%" />
                                <ItemStyle HorizontalAlign="Left" Width="40%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Amount">
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                            </asp:BoundField>

                            <%-- <asp:BoundField HeaderText="Remarks">
                                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                    <ItemStyle HorizontalAlign="Left" Width="20%" />
                                </asp:BoundField>--%>
                        </Columns>
                        <FooterStyle Font-Size="11px" />
                        <HeaderStyle Font-Size="11px" HorizontalAlign="Center"/>
                        <RowStyle Font-Size="9px" />
                    </asp:GridView>
                </div>
                <table style="width: 100%">

                    <tr>
                        <td style="width: 20%; text-align: left; font-family: Calibri; font-size: 9px; font-weight: bold">In Words
                        </td>
                        <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 9px; font-weight: bold">:
                        </td>
                        <td style="width: 67%">
                            <asp:Label ID="lblInWords" runat="server" Font-Names="Calibri" Font-Size="9px"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; margin-top: 50px">
                    <tr style="text-align: center;">
                        <td style="width: 45%; text-align: center; font-family: Calibri; font-size: 9px; border-top: 1px solid #000;">PREPARED BY
                        </td>
                        <td style="width: 10%"></td>
                        <td style="width: 45%; text-align: center; font-family: Calibri; font-size: 9px; border-top: 1px solid #000;">CHECKED BY
                        </td>

                    </tr>
                </table>
                <table style="width: 100%; margin-top: 5px; font-size: 9px; text-align: center">
                    <tr>
                        <td>
                            <strong>Print Date :</strong>
                            <asp:Label ID="lblPrintDate" runat="server" Font-Names="Calibri" Font-Size="9px"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; margin-top: 5px; font-size: 9px; text-align: center">
                    <tr>
                        <td>
                            <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()"
                                style="font-family: Calibri; font-size: 15px; font-weight: bold; font-style: inherit; text-align: right" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>

    </form>
</body>
</html>
