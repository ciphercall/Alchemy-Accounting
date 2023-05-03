<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptCreditVoucherEdit.aspx.cs" Inherits="AlchemyAccounting.Accounts.Report.Report.RptCreditVoucherEdit" %>

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

    <style type="text/css">
        #main {
            float: left;
            border: 1px solid #000000;
            width: 100%;
            padding-bottom: 40px;
        }

        #btnPrint {
            font-weight: 700;
            font-style: italic;
        }

        .style1 {
            font-size: smaller;
        }

        .style2 {
            width: 862px;
        }

        .style3 {
            font-size: 11px;
            width: 862px;
        }

        .style12 {
            width: 2px;
            font-size: medium;
            font-weight: bold;
        }

        .style14 {
            width: 529px;
        }

        .style15 {
            width: 1051px;
        }

        .style16 {
            font-size: 18px;
            width: 182px;
            font-family: Calibri;
        }

        .style18 {
            width: 182px;
        }

        .style19 {
            width: 221px;
            font-size: 10pt;
            font-family: Calibri;
        }

        .style22 {
            width: 5px;
            font-size: medium;
        }

        .style23 {
            width: 5px;
        }

        .style24 {
            width: 275px;
        }

        .style25 {
            width: 144px;
        }

        .style26 {
            width: 3px;
            font-size: medium;
            font-weight: bold;
        }

        .style27 {
            width: 381px;
        }

        .style28 {
            width: 191px;
        }

        .style29 {
            width: 3px;
        }

        .style30 {
            width: 3px;
            font-size: medium;
        }

        .style31 {
            width: 180px;
            text-align: right;
        }

        .style32 {
            height: 12px;
        }

        .style33 {
            width: 88px;
        }

        .style34 {
            width: 1px;
        }

        .style38 {
            width: 54px;
        }

        .style39 {
            width: 277px;
            text-align: center;
            font-size: medium;
        }

        .style42 {
            width: 283px;
            text-align: center;
            font-size: medium;
        }

        .style43 {
            width: 296px;
            font-size: medium;
            text-align: center;
        }

        .style44 {
            width: 297px;
            font-size: medium;
            text-align: center;
        }

        .style46 {
            font-family: Calibri;
        }

        .style47 {
            width: 1051px;
            font-family: Calibri;
            font-size: 18px;
        }

        .style48 {
            font-family: Calibri;
            font-size: 16px;
        }

        .style50 {
            width: 211px;
            font-size: 10pt;
            font-family: Calibri;
        }

        .style52 {
            font-family: Calibri;
            font-size: 10pt;
        }

        .style53 {
            width: 572px;
        }

        .style55 {
            width: 300px;
        }

        .style56 {
            width: 480px;
            height: 19px;
        }

        .style58 {
            height: 19px;
        }

        .style59 {
            height: 19px;
            text-align: right;
            font-family: Calibri;
            width: 186px;
        }

        .style60 {
            text-align: right;
            font-family: Calibri;
            width: 186px;
        }

        .style61 {
            font-family: Calibri;
            font-size: small;
        }

        .style62 {
            width: 186px;
        }

        .style63 {
            text-align: right;
            font-family: Calibri;
            width: 2px;
        }

        .style64 {
            height: 19px;
            text-align: right;
            font-family: Calibri;
            width: 2px;
        }

        .style65 {
            width: 2px;
        }

        .style66 {
            width: 480px;
        }

        .auto-style1 {
            width: 211px;
            font-size: 10pt;
            font-family: Calibri;
            height: 26px;
        }

        .auto-style2 {
            width: 2px;
            font-size: medium;
            font-weight: bold;
            height: 26px;
        }

        .auto-style3 {
            width: 572px;
            height: 26px;
        }

        .auto-style4 {
            width: 529px;
            height: 26px;
        }

        .auto-style5 {
            height: 26px;
        }
    </style>
</head>
<body style="font-size: small">
    <form id="form1" runat="server">
        <div id="main">
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 10%">
                            <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()" /></td>
                        <td style="width: 80%; text-align:center">
                            <asp:Label ID="lblCompNM" runat="server" Visible="false" Style="font-family: Calibri; font-size: 23px"></asp:Label>
                         
                            <asp:Label ID="lblAddress" runat="server" Visible="false" 
                                Style="font-family: Calibri; font-size: 10px"></asp:Label>
                         
                            <span class="style52"></span><span class="style51"> 
                    <asp:Label ID="lblContact" runat="server" Visible="false" 
                        Style="font-family: Calibri; font-size: 11px"></asp:Label></span>


                             
                               <%-- <img src="../../../Images/logo.png" width="100%" height="100%;" alt="logo" />--%>
                                <span style="font-family:Cambria(Heading); font-size:18px">TRADE ABROAD</span>
                          

                        </td>
                        <td style="width: 10%">
                           
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 33%"></td>
                        <td style="width: 34%; text-align: center; border: 1px solid #000; border-radius: 5px;">
                            <asp:Label ID="lblVtype" runat="server" Style="font-family: Calibri"></asp:Label></td>
                        <td style="width: 33%">
                            <table>
                                <tr>
                                    <td>Voucher No</td>
                                    <td><strong>:</strong></td>
                                    <td>
                                        <asp:Label ID="lblVNo" runat="server" CssClass="style61"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Voucher Date</td>
                                    <td><strong>:</strong></td>
                                    <td>
                                        <asp:Label ID="lblTime" runat="server" CssClass="style61"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td><asp:Label ID="lblReceiveCrBy" runat="server"></asp:Label></td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="lblReceivedBy" runat="server" CssClass="style52"></asp:Label>
                            <asp:Label ID="lblMidDate" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblReceiveCrFrom" runat="server"></asp:Label></td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="lblReceivedFrom" runat="server" CssClass="style52"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblAmount" runat="server" Style="font-size: large; font-family: Calibri;"
                    Visible="False"></asp:Label><asp:Label ID="lblVoDT" runat="server" Visible="False"></asp:Label>

            </div>

            <div>
                <table style="width: 100%;">
                    <tr>
                        <td style="border: 1px solid #000000; text-align: center; font-weight: 700;font-size: 11px;"
                            class="style47">Particulars</td>
                        <td style="border: 1px solid #000000; text-align: center; font-weight: 700;font-size: 11px;"
                            class="style16">Amount (TK.)</td>
                    </tr>
                    <tr>
                        <td style="border: 1px solid #cccccc; height: 40px;font-size: 11px"  class="style15">
                            <asp:Label ID="lblParticulars" runat="server"  Style="font-size: 11px;" CssClass="style48"></asp:Label>
                        </td>
                        <td style="border: 1px solid #cccccc; text-align: right; height: 40px;font-size: 11px" class="style18">
                            <asp:Label ID="lblAmountComma" runat="server" CssClass="style48" Style="font-size: 11px;"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td class="style19">Mode of Received </td>
                        <td class="style22">
                            <strong>:</strong></td>
                        <td class="style24">
                            <asp:Label ID="lblRMode" runat="server"
                                CssClass="style52"></asp:Label>
                        </td>
                        <td class="style25">&nbsp;</td>
                        <td class="style26">&nbsp;</td>
                        <td class="style27">&nbsp;</td>
                        <td class="style28">&nbsp;</td>
                        <td class="style29">&nbsp;</td>
                        <td class="style31">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style19">Cheque No</td>
                        <td class="style22">
                            <strong>:</strong></td>
                        <td class="style24">
                            <asp:Label ID="lblChequeNo" runat="server"
                                CssClass="style52"></asp:Label>
                        </td>
                        <td class="style25" style="font-size: 10pt; font-family: Calibri;">Cheque Date</td>
                        <td class="style26">:</td>
                        <td class="style27">
                            <asp:Label ID="lblChequeDT" runat="server" Style="font-size: 10pt"
                                CssClass="style46"></asp:Label>
                        </td>
                        <td class="style28"
                            style="font-size: medium; font-weight: 700; text-align: right; font-family: Calibri;">Total</td>
                        <td class="style30">
                            <strong>:</strong></td>
                        <td class="style31">
                            <asp:Label ID="lblTotAmount" runat="server"
                                Style="font-size: medium; text-align: right; font-weight: 700;"
                                CssClass="style46"></asp:Label>
                        </td>
                    </tr>
                    
                </table>
            </div>
            <div>

                <table style="width: 100%; font-family: Calibri;">
                    <tr>
                        <td class="style33" style="font-size: 10pt">In Words</td>
                        <td class="style34" style="font-size: medium">
                            <strong>:</strong></td>
                        <td>
                            <div style="border-bottom: 1px solid #000000; width: 60%;">
                                <asp:Label ID="lblInWords" runat="server" Style="font-size: 10pt;"></asp:Label>
                                <div style="border-bottom: 1px solid #000000; width: 100%; margin-bottom: 10px;"></div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="style33" style="font-size: medium">&nbsp;</td>
                        <td class="style34" style="font-size: medium">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>

            </div>
            <div>

                <table style="width: 100%; font-family: Calibri;">
                    <tr>
                        <td class="style38">&nbsp;</td>
                        <td class="style39">&nbsp;</td>
                        <td class="style42">&nbsp;</td>
                        <td class="style44">&nbsp;</td>
                        <td class="style43">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style38">&nbsp;</td>
                        <td class="style39">Received By</td>
                        <td class="style42">Prepared By</td>
                        <td class="style44">Checked By</td>
                        <td class="style43">Approved By</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>

            </div>
        </div>
    </form>
</body>
</html>
