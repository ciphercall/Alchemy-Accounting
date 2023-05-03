<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt-collection.aspx.cs"
    Inherits="AlchemyAccounting.CNF.report.vis_rep.rpt_collection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="shortcut icon" href="../../../Images/favicon.ico" />
    <script type="text/javascript">
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table style="width: 100%">
                <tr>
                    <td style="width: 10%" rowspan="3">
                        <div style="width: 140px; height: 80px;">
                            <img alt="logo" height="100%;" src="../../../Images/logo.png" width="100%" />
                        </div>
                    </td>
                    <td style="width: 80%; text-align: center">
                        <asp:Label ID="lblCompNM" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="23px"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()"
                            style="font-family: Calibri; font-size: 15px; font-weight: bold; font-style: inherit" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%; text-align: center">
                        <asp:Label ID="lblAddress" runat="server" Style="font-family: Calibri; font-size: 9px"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%; text-align: center; font-family: Calibri; font-size: 18px;
                        font-weight: bold;">
                        Collection Information as on
                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%">
                    </td>
                    <td style="text-align: right; font-family: Calibri; font-size: 14px;" colspan="2">
                        <strong>Print Date :</strong>
                        <asp:Label ID="lblPrintDate" runat="server" Font-Names="Calibri" Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                    <td style="text-align: right; font-family: Calibri; font-size: 14px;" colspan="2">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="width: 20%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        Scheme
                    </td>
                    <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                        :
                    </td>
                    <td style="width: 34%; font-family: Calibri; font-size: 15px; text-align: left">
                        <asp:Label ID="lblSchTp" runat="server"></asp:Label>
                    </td>
                    <td style="width: 15%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        Branch
                    </td>
                    <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                        :
                    </td>
                    <td style="width: 29%; font-family: Calibri; font-size: 15px; text-align: left">
                        <asp:Label ID="lblBranch" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        Area
                    </td>
                    <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                        :
                    </td>
                    <td style="width: 34%; font-family: Calibri; font-size: 15px; text-align: left">
                        <asp:Label ID="lblArea" runat="server"></asp:Label>
                    </td>
                    <td style="width: 15%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        Document No
                    </td>
                    <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                        :
                    </td>
                    <td style="width: 29%; font-family: Calibri; font-size: 15px; text-align: left">
                        <asp:Label ID="lblDocNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        Collector
                    </td>
                    <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                        :
                    </td>
                    <td style="width: 34%; font-family: Calibri; font-size: 15px; text-align: left">
                        <asp:Label ID="lblCollector" runat="server"></asp:Label>
                    </td>
                    <td style="width: 15%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        Remarks
                    </td>
                    <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                        :
                    </td>
                    <td style="width: 29%; font-family: Calibri; font-size: 15px; text-align: left">
                        <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <div style="width: 96%; margin: 1% 2% 0% 2%;">
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" Font-Names="Calibri"
                    OnRowDataBound="gvReport_RowDataBound" ShowFooter="True" Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="Serial">
                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Particulars">
                            <HeaderStyle HorizontalAlign="Center" Width="60%" />
                            <ItemStyle HorizontalAlign="Left" Width="60%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Code">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Amount">
                            <HeaderStyle HorizontalAlign="Center" Width="25%" />
                            <ItemStyle HorizontalAlign="Right" Width="25%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle Font-Size="14px" />
                    <HeaderStyle Font-Size="14px" />
                    <RowStyle Font-Size="12px" />
                </asp:GridView>
            </div>
            <table style="width: 100%">
                <tr>
                    <td style="width: 15%">
                    </td>
                    <td style="width: 1%">
                    </td>
                    <td style="width: 84%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%; text-align: right; font-family: Calibri; font-size: 14px;
                        font-weight: bold">
                        &nbsp;
                    </td>
                    <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                        font-weight: bold">
                        &nbsp;
                    </td>
                    <td style="width: 84%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%; text-align: right; font-family: Calibri; font-size: 14px;
                        font-weight: bold">
                        In Words
                    </td>
                    <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                        font-weight: bold">
                        :
                    </td>
                    <td style="width: 84%">
                        <asp:Label ID="lblInWords" runat="server" Font-Names="Calibri" Font-Size="15px"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="width: 12%">
                    </td>
                    <td style="width: 15%">
                    </td>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 15%">
                    </td>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 15%">
                    </td>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 16%">
                    </td>
                    <td style="width: 12%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 12%">
                        &nbsp;
                    </td>
                    <td style="width: 15%; text-align: center; font-family: Calibri; font-size: 14px">
                        &nbsp;
                    </td>
                    <td style="width: 5%">
                        &nbsp;
                    </td>
                    <td style="width: 15%; text-align: center; font-family: Calibri; font-size: 14px">
                        &nbsp;
                    </td>
                    <td style="width: 5%">
                        &nbsp;
                    </td>
                    <td style="width: 15%; text-align: center; font-family: Calibri; font-size: 14px">
                        &nbsp;
                    </td>
                    <td style="width: 5%">
                        &nbsp;
                    </td>
                    <td style="width: 16%; text-align: center; font-family: Calibri; font-size: 14px">
                        &nbsp;
                    </td>
                    <td style="width: 12%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 12%">
                        &nbsp;
                    </td>
                    <td style="width: 15%; text-align: center; font-family: Calibri; font-size: 14px">
                        &nbsp;
                    </td>
                    <td style="width: 5%">
                        &nbsp;
                    </td>
                    <td style="width: 15%; text-align: center; font-family: Calibri; font-size: 14px">
                        &nbsp;
                    </td>
                    <td style="width: 5%">
                        &nbsp;
                    </td>
                    <td style="width: 15%; text-align: center; font-family: Calibri; font-size: 14px">
                        &nbsp;
                    </td>
                    <td style="width: 5%">
                        &nbsp;
                    </td>
                    <td style="width: 16%; text-align: center; font-family: Calibri; font-size: 14px">
                        &nbsp;
                    </td>
                    <td style="width: 12%">
                        &nbsp;
                    </td>
                </tr>
                </table>
        </div>
    </div>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
