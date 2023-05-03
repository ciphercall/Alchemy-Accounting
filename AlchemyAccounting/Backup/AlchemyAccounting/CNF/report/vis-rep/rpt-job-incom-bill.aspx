<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt-job-incom-bill.aspx.cs" Inherits="AlchemyAccounting.CNF.report.vis_rep.rpt_job_incom_bill" %>

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
    <style type="text/css">
        #btnPrint
        {
            font-weight: 700;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table style="width: 100%; font-family: Calibri;">
                <tr>
                    <td style="width: 60%">
                        <asp:Label ID="lblCompNM" runat="server" Font-Size="20px" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="text-align: right; width: 40%">
                        <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60%">
                        <asp:Label ID="lblAddress" runat="server" Style="font-family: Calibri; font-size: 9px"></asp:Label>
                    </td>
                    <td style="text-align: right; width: 40%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 60%">
                        <strong>JOB - INCOMPLETE FOR BILL</strong>
                    </td>
                    <td style="text-align: right; width: 40%">
                        <asp:Label ID="lblTime" runat="server" Style="text-align: right; font-family: Calibri;
                            font-size: 11px;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 60%">
                        YEAR :
                        <asp:Label ID="lblJobYear" runat="server"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        TYPE :
                        <asp:Label ID="lblJobType" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right; width: 40%">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <div style="width: 100%; margin: 1% 0% 0% 0%;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Font-Size="12px"
                    OnRowDataBound="GridView1_RowDataBound" ShowHeaderWhenEmpty="True" 
                    Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="Job No">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Party">
                            <HeaderStyle HorizontalAlign="Center" Width="40%" />
                            <ItemStyle HorizontalAlign="Left" Width="40%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Permit No">
                            <HeaderStyle HorizontalAlign="Center" Width="25%" />
                            <ItemStyle HorizontalAlign="Left" Width="25%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Invoice No">
                        <HeaderStyle HorizontalAlign="Center" Width="25%" />
                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle Font-Names="Calibri" Font-Size="14px" Font-Bold="True" />
                    <HeaderStyle Font-Names="Calibri" Font-Size="16px" />
                    <RowStyle Font-Names="Calibri" Font-Size="12px" />
                </asp:GridView>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
