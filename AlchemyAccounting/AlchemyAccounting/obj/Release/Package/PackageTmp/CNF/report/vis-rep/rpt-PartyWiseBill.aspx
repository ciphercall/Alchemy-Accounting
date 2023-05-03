<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt-PartyWiseBill.aspx.cs" Inherits="AlchemyAccounting.CNF.report.vis_rep.rpt_PartyWiseBill" %>

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
            <div class="MyCssClass" style="width: 96%; margin: 1% 2% 0% 2%;">
                <table style="width: 100%;text-transform: uppercase;">
                    <tr>
                        <td>DATE:  <asp:Label ID="lblTime" runat="server"></asp:Label><span style="float: right">
                            <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()" /></span>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>THE MANAGING DIRECTOR</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblPartyName"></asp:Label>.
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="width: 400px">
                                <asp:Label runat="server" ID="lblPartyaddress" ></asp:Label>.
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >SUBJECT: SUBMISSION OF <%=Session["JOBTP"].ToString() %> BILLS
                            <asp:Label ID="lblFDate" runat="server"></asp:Label> ---
                            <asp:Label ID="lblTDate" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <asp:Label ID="lblAddress" runat="server" Visible="False"></asp:Label>
                <asp:Label runat="server" ID="lblJobType" Visible="False"></asp:Label>
                <asp:Label runat="server" ID="lblSationName" Visible="False"></asp:Label>
            </div>

            <div class="MyCssClass" style="width: 96%; margin: 1% 2% 0% 2%;">

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Font-Size="12px"
                    OnRowDataBound="GridView1_RowDataBound" ShowHeaderWhenEmpty="True"
                    Width="100%" ShowFooter="True">
                    <Columns>
                        <asp:BoundField HeaderText="SL">
                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="BILL DATE">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="R/ID">
                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="JOB NO">
                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="BILL NO">
                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="BILL AMOUNT">
                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                            <ItemStyle HorizontalAlign="Right" Width="15%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle Font-Names="Calibri" Font-Size="14px" Font-Bold="True" />
                    <HeaderStyle Font-Names="Calibri" Font-Size="16px" />
                    <RowStyle Font-Names="Calibri" Font-Size="12px" />
                </asp:GridView>

            </div>

            <div class="MyCssClass" style="width: 96%; margin: 1% 2% 0% 2%;">
                <table style="width: 100%">
                    <tr>
                        <td style="text-transform: uppercase; font-weight: 800">IN WORD TAKA: 
                            <asp:Label ID="lblinword" runat="server"></asp:Label>.</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>THANKING YOU</td>
                    </tr>
                    <tr>
                        <td>FOR  
                            <asp:Label ID="lblCompNM" runat="server"></asp:Label>.</td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
