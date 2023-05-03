<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt-bill-details.aspx.cs"
    Inherits="AlchemyAccounting.CNF.report.vis_rep.rpt_bill_details" %>

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
        .ShowHeader thead {
            display: table-header-group;
            border: 1px solid #000;
        }
    </style>
    <style type="text/css">
        .style2 {
            font-weight: normal;
        }

        .style6 {
            font-size: 16px;
        }

        body {
            margin: 0;
            padding: 0;
            background-color: #FAFAFA;
            font: 14px "Calibri";
        }

        * {
            box-sizing: border-box;
            -moz-box-sizing: border-box;
        }

        .page {
            width: 21cm;
            min-height: 29.7cm;
            padding-top: 1.3cm;
            padding-bottom: 1cm;
            padding-right: 1cm;
            padding-left: .6cm;
            margin: 1cm auto;
            border: 1px #D3D3D3 solid;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
        }

        .subpage {
            padding: 1cm;
            border: 5px red solid;
            height: 237mm;
            outline: 2cm #FFEAEA solid;
        }

        @page {
            size: A4;
            margin: 0;
        }

        @media print {
            .page {
                margin: 0;
                border: initial;
                border-radius: initial;
                width: initial;
                min-height: initial;
                box-shadow: initial;
                background: initial;
                page-break-after: avoid; /* here always for subpage */
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page">
            <div style="border: 1px; border-color: red;">
                <%--<div style="float: left; height: 842px; width: 100%">--%>
                <div style="float: left; width: 10%; min-height: 850px; margin-right: 2px;">
                    <img alt="pad" height="850px" width="100%" src="../../../Images/pad.png" />
                </div>
                <div style="float: right; width: 87%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 10%">
                                <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()"
                                    style="font-family: Calibri; font-size: 15px; font-weight: bold; font-style: inherit; text-align: right" />

                            </td>
                            <td style="width: 40%; text-align: center">&nbsp;
                        <asp:Label runat="server" ID="lblCompanyNM" Font-Size="20" Visible="false"></asp:Label>
                                <br />
                                <asp:Label ID="lblAddress" runat="server" Style="font-family: Calibri; font-size: 9px"
                                    Visible="false"></asp:Label>
                            </td>
                            <td style="width: 50%;text-align:right">
                                <div style="width: 86%; height: 50px;  margin-left: 47px;">
                                    <img alt="logo" height="68%" src="../../../Images/logo.png" width="100%" />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tr>

                            <td style="width: 10%">
                                <asp:Label ID="lblBill" runat="server" Visible="False" Style="font-size: 12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%"></td>
                            <td style="width: 80%; text-align: center; font-family: Calibri; font-size: 18px; font-weight: bold;">
                                <span class="style2">C&amp;F BILL</span> -
                        <asp:Label ID="lblJobTP" runat="server"></asp:Label>
                            </td>
                            <td style="width: 10%">&nbsp;
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 15%; font-family: Calibri; font-size: 15px; text-align: left">Job No
                            </td>
                            <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">:
                            </td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left" colspan="2">
                                <asp:Label ID="lblJobNo" runat="server"></asp:Label>
                            </td>
                            <td style="width: 39%; font-family: Calibri; font-size: 12px; text-align: right">
                                <strong>Print Date :</strong>
                                <asp:Label ID="lblPrintDate" runat="server" Font-Names="Calibri" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%; font-family: Calibri; font-size: 15px; text-align: left">Bill No
                            </td>
                            <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">:
                            </td>
                            <td colspan="2" style="font-family: Calibri; font-size: 15px; text-align: left">
                                <asp:Label ID="lblBillNo" runat="server"></asp:Label>
                            </td>
                            <td style="width: 39%; font-family: Calibri; font-size: 15px; text-align: left">Date :
                        <asp:Label ID="lblJobDate" runat="server"></asp:Label>
                        <asp:Label ID="lblReg" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%; font-family: Calibri; font-size: 15px; text-align: left">Account
                            </td>
                            <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">:
                            </td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left" colspan="3">
                                <asp:Label ID="lblAccount" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%; font-family: Calibri; font-size: 15px; text-align: left"></td>
                            <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center"></td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left" colspan="3">
                                <asp:Label ID="lblAccAdd" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%; font-family: Calibri; font-size: 15px; text-align: left">B/E No
                            </td>
                            <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">:
                            </td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left">
                                <asp:Label ID="lblBENo" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left">&nbsp;
                            </td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left">
                        <asp:Label ID="lblPermitNo" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%; font-family: Calibri; font-size: 15px; text-align: left">Invoice No
                            </td>
                            <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">:
                            </td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left">
                                <asp:Label ID="lblInvoiceNo" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left">Date :
                        <asp:Label ID="lblInvoiceDt" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left">B/L No :
                        <asp:Label ID="lblBLNO" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%; font-family: Calibri; font-size: 15px; text-align: left">L/C No
                            </td>
                            <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">:
                            </td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left">
                                <asp:Label ID="lblLc" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left">Date :
                        <asp:Label ID="lblLCDate" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left">AWB No :
                        <asp:Label ID="lblAwbNo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%; font-family: Calibri; font-size: 15px; text-align: left">HAWB No
                            </td>
                            <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">:
                            </td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left" colspan="2">
                                <asp:Label ID="lblHBLNO" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblHAwbNo" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left">Packages :
                        <asp:Label ID="lblPackages" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%; font-family: Calibri; font-size: 15px; text-align: left">Goods
                            </td>
                            <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">:
                            </td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left" colspan="2">
                                <asp:Label ID="lblGoodsDesc" runat="server"></asp:Label>
                                &nbsp;
                            </td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left">
                                Assessable  Value: <asp:Label ID="lblAssessableValue" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%; font-family: Calibri; font-size: 15px; text-align: left">Value (USD)
                            </td>
                            <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">:
                            </td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left">
                                <asp:Label ID="lblValUSD" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left">Exc. Rate :
                        <asp:Label ID="lblExRt" runat="server"></asp:Label>
                            </td>
                            <td style="font-family: Calibri; font-size: 15px; text-align: left">Value Tk. :
                        <asp:Label ID="lblValTk" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div style="width: 100%; height: 480px">
                        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" Font-Names="Calibri"
                            OnRowDataBound="gvReport_RowDataBound" Width="100%" Style="font-size: 14px">
                            <Columns>
                                <asp:BoundField HeaderText="Serial">
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Particulars">
                                    <HeaderStyle HorizontalAlign="Center" Width="45%" />
                                    <ItemStyle HorizontalAlign="Left" Width="45%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Remarks" Visible="False">
                                    <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                    <ItemStyle HorizontalAlign="Left" Width="25%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Date">
                                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Amount">
                                    <HeaderStyle HorizontalAlign="Center" Width="18%" />
                                    <ItemStyle HorizontalAlign="Right" Width="18%" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle Font-Size="14px" />
                            <HeaderStyle Font-Size="14px" />
                            <RowStyle Font-Size="12px" />
                        </asp:GridView>
                        <table style="width: 100%; font-family: Calibri; font-size: 14px">
                            <tr>
                                <td style="width: 5%">&nbsp;
                                </td>
                                <td colspan="3" style="text-align: right">C&amp;F Commission 
                        <asp:Label ID="lblCRem" runat="server" Visible="False"></asp:Label>
                                    &nbsp;(Tk.) :
                                </td>
                                <td style="width: 18%; text-align: right">
                                    <asp:Label ID="lblAgencyCom" runat="server" Font-Names="Calibri" Font-Size="15px"
                                        Style="font-size: 14px">0.00</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">&nbsp;
                                </td>
                                <td style="width: 45%">&nbsp;
                                </td>
                                <td style="width: 5%">&nbsp;
                                </td>
                                <td style="width: 27%; text-align: right">Total (Tk.) :
                                </td>
                                <td style="width: 18%; text-align: right">
                                    <asp:Label ID="lblTotal" runat="server" Font-Names="Calibri" Font-Size="15px" Style="font-size: 14px">0.00</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">&nbsp;
                                </td>
                                <td style="width: 45%">&nbsp;
                                </td>
                                <td style="width: 5%">&nbsp;
                                </td>
                                <td style="width: 27%; text-align: right">&nbsp;Advance Amount (Tk.) :
                                </td>
                                <td style="width: 18%; text-align: right">
                                    <asp:Label ID="lblAdvAmt" runat="server" Font-Names="Calibri" Font-Size="15px" Style="font-size: 14px">0.00</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">&nbsp;
                                </td>
                                <td style="width: 45%">&nbsp;
                                </td>
                                <td style="width: 5%">&nbsp;
                                </td>
                                <td style="width: 27%; text-align: right">Balance Amount (Tk.) :
                                </td>
                                <td style="width: 18%; text-align: right">
                                    <asp:Label ID="lblBalanceAmt" runat="server" Font-Names="Calibri" Font-Size="15px"
                                        Style="font-size: 14px">0.00</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">In Words :
                            <asp:Label ID="lblInWords" runat="server" Font-Names="Calibri" Font-Size="15px" Style="font-size: 14px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" style="text-align: right" class="style6">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" style="text-align: right" class="style6">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" style="text-align: right" class="style6">
                                    TRADE ABROAD
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5"></td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 100%; height: 80px">
                        <div style="text-align: left; color: #cccccc; font-size: 14px; height: 18px; width: 100%">
                            Developed By: Alchemy Software, 01712021091, 01816910849
                        </div>
                        <div style="width: 100%; height: 30px">
                        </div>
                    </div>
                </div>

                <div style="position: relative; padding-left: 6px">
                    <div style="float: left; margin-left: 2px; font-size: 12px">
                        <table style="width: 100%">
                            <tr>
                                <td style=" color: #015AAA">Corporate Office :</td>
                            </tr>
                            <tr>
                                <td style="width: 100%;">Adept S.T. Complex (3rd Floor), KA-7/1-2 Jagannathpur, Basundhara Link Road, Dhaka-1229, Bangladesh
                                <br />Tel:(+8802)8415473, Fax:(+8802)8415475, E-mail:tradeabroad96dhk@gmail.com, www.tradeabroadbd.com
                                </td>
                            </tr>
                        </table>
                        <span style="color: #015AAA"></span>
                    </div>
                    <div style="float: left; margin-left: 2px; font-size: 12px">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 18%; color: #015AAA">Chittagong Office :</td>
                            </tr>
                            <tr>
                                <td style="width: 100%;">Gawsia Bhaban(3rd Floar), 156, Sheikh Mujib Road, Agrabad C/A, Chittagong, Bangladesh
                                    <br />
                                Tel: (+88031) 2520108, Fax: Tel: (+88031) 713356</td>
                            </tr>
                        </table>
                        <span style="color: #015AAA"></span>
                    </div>
                    <div style="float: left; margin-left: 2px; font-size: 12px">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 100%"> <span style="color: #015AAA">Benapole Office :</span> Benapole Bazar, Benapole, Jessore, Cell # 01711 533290</td>
                            </tr>
                        </table>
                    </div>
                </div>

            </div>
        </div>

    </form>
    <p>
        &nbsp;
    </p>
</body>
</html>
