<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptReceive_VR.aspx.cs"
    Inherits="AlchemyAccounting.CNF.report.vis_rep.RptReceive_VR" %>

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
        #main
        {
            float: left;
            border: 1px solid #000000;
            width: 100%;
            padding-bottom: 40px;
        }
        #btnPrint
        {
            font-weight: 700;
            font-style: italic;
        }
        .style1
        {
            font-size: smaller;
        }
        .style3
        {
            font-size: smaller;
            width: 837px;
        }
        .style12
        {
            width: 2px;
            font-size: medium;
            font-weight: bold;
        }
        .style13
        {
            width: 592px;
        }
        .style14
        {
            width: 506px;
        }
        .style15
        {
            width: 1051px;
        }
        .style16
        {
            font-size: 16pt;
            width: 182px;
        }
        .style18
        {
            width: 182px;
        }
        .style19
        {
            width: 224px;
            font-size: medium;
        }
        .style22
        {
            width: 5px;
            font-size: medium;
        }
        .style23
        {
            width: 5px;
        }
        .style24
        {
            width: 236px;
        }
        .style25
        {
            width: 134px;
        }
        .style26
        {
            width: 3px;
            font-size: medium;
            font-weight: bold;
        }
        .style27
        {
            width: 346px;
        }
        .style29
        {
            width: 3px;
        }
        .style30
        {
            width: 3px;
            font-size: medium;
        }
        .style31
        {
            width: 180px;
            text-align: right;
        }
        .style32
        {
            height: 12px;
        }
        .style33
        {
            width: 88px;
        }
        .style34
        {
            width: 1px;
        }
        .style38
        {
            width: 54px;
        }
        .style39
        {
            width: 277px;
            text-align: center;
            font-size: medium;
        }
        .style42
        {
            width: 283px;
            text-align: center;
            font-size: medium;
        }
        .style43
        {
            width: 296px;
            font-size: medium;
            text-align: center;
        }
        .style44
        {
            width: 297px;
            font-size: medium;
            text-align: center;
        }
        .style45
        {
            font-family: Calibri;
            font-size: 11px;
        }
        .style47
        {
            width: 194px;
            font-size: 10pt;
            font-family: Calibri;
        }
        .style48
        {
            width: 224px;
            font-size: 10pt;
            font-family: Calibri;
        }
        .style49
        {
            width: 60px;
            font-family: Calibri;
        }
        .style50
        {
            width: 60px;
        }
        .style51
        {
            font-weight: normal;
        }
        .style52
        {
            font-family: Calibri;
            font-size: 11px;
            font-weight: normal;
        }
        .style53
        {
            font-family: Calibri;
            font-size: 10pt;
        }
        .style56
        {
            font-size: 10pt;
        }
        .style59
        {
            width: 300px;
        }
        .style60
        {
            width: 2px;
        }
        .style62
        {
            width: 4px;
            font-size: medium;
        }
        .style63
        {
            width: 4px;
        }
        .style65
        {
            width: 837px;
        }
        .style71
        {
            width: 472px;
        }
        .style72
        {
            width: 198px;
        }
        .style73
        {
            width: 198px;
            font-size: 10pt;
            font-family: Calibri;
        }
        .style74
        {
            width: 142px;
        }
    </style>
</head>
<body style="font-size: small">
    <form id="form1" runat="server">
    <div id="main">
        <div>
            <table style="width: 100%; border-bottom: 1px solid #000000;">
                <tr>
                    <td style="text-align: center; font-size: xx-large; font-weight: 700" class="style74"
                        rowspan="3">
                        <div style="width: 140px; height: 80px;">
                            <img src="../../../Images/logo.png" width="100%" height="100%;" alt="logo" />
                        </div>
                    </td>
                    <td style="text-align: center; font-size: xx-large; font-weight: 700" class="style65">
                        <asp:Label ID="lblCompNM" runat="server" Style="font-family: Calibri; font-size: 23px"></asp:Label>
                    </td>
                    <td style="text-align: center; font-size: xx-large; font-weight: 700">
                        <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" class="style65">
                        <asp:Label ID="lblAddress" runat="server" Style="font-family: Calibri; font-size: 10px"></asp:Label>
                    </td>
                    <td style="text-align: center">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style3" style="text-align: center; font-weight: 700; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                        <span class="style52">TELEPHONE</span><span class="style51"> :
                            <asp:Label ID="lblContact" runat="server" Style="font-family: Calibri; font-size: 11px"></asp:Label>
                        </span>
                    </td>
                    <td class="style1" style="text-align: center; font-weight: 700; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table style="width: 100%;">
                <tr>
                    <td class="style71">
                        &nbsp;
                    </td>
                    <td class="style59" rowspan="3">
                        <div style="width: 100%; height: 60px; border: 1px solid #000000; border-radius: 10px;">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style32">
                                    </td>
                                    <td class="style32">
                                    </td>
                                    <td class="style32">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="font-weight: 700; font-size: large; text-align: center;">
                                        <asp:Label ID="lblVtype" runat="server" Style="font-family: Calibri"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td class="style60">
                        &nbsp;
                    </td>
                    <td class="style73" style="text-align: right">
                        Voucher No
                    </td>
                    <td class="style62" style="font-weight: 700">
                        :
                    </td>
                    <td>
                        <asp:Label ID="lblVNo" runat="server" Style="font-family: Calibri;" CssClass="style56"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style71">
                        &nbsp;
                    </td>
                    <td class="style60">
                        &nbsp;
                    </td>
                    <td class="style73" style="text-align: right">
                        Voucher&nbsp; Date
                    </td>
                    <td class="style62" style="font-weight: 700">
                        :
                    </td>
                    <td>
                        <asp:Label ID="lblTime" runat="server" Style="font-family: Calibri;" CssClass="style56"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style71">
                        &nbsp;
                    </td>
                    <td class="style60">
                        &nbsp;
                    </td>
                    <td class="style72">
                        &nbsp;
                    </td>
                    <td class="style63">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <br />
            <table style="width: 100%;">
                <tr>
                    <td class="style47">
                        Received By
                    </td>
                    <td class="style12">
                        :
                    </td>
                    <td class="style13">
                        <asp:Label ID="lblReceivedBy" runat="server" Style="font-size: 10pt" CssClass="style45"></asp:Label>
                    </td>
                    <td class="style14">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style47">
                        Received From
                    </td>
                    <td class="style12">
                        :
                    </td>
                    <td class="style13">
                        <asp:Label ID="lblReceivedFrom" runat="server" Style="font-size: 10pt" CssClass="style45"></asp:Label>
                    </td>
                    <td class="style14">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div>
            <table style="width: 100%;">
                <tr>
                    <td style="border: 1px solid #000000; text-align: center; font-size: 18px; font-weight: 700;
                        font-family: Calibri;" class="style15">
                        Particulars
                    </td>
                    <td style="border: 1px solid #000000; text-align: center; font-weight: 700; font-family: Calibri;"
                        class="style16">
                        Amount (QR)
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid #cccccc; height: 40px;" class="style15">
                        <asp:Label ID="lblParticulars" runat="server" Style="font-size: 16px; font-family: Calibri;"></asp:Label>
                    </td>
                    <td style="border: 1px solid #cccccc; text-align: right; height: 40px;" class="style18">
                        <asp:Label ID="lblAmount" runat="server" Style="font-size: 16px; font-family: Calibri;"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div>
            <table style="width: 100%;">
                <tr>
                    <td class="style48">
                        Job Type
                    </td>
                    <td class="style22">
                        <strong>:</strong>
                    </td>
                    <td class="style24">
                        <asp:Label ID="lblRMode" runat="server" CssClass="style53"></asp:Label>
                    </td>
                    <td class="style25">
                        Job No &nbsp;
                    </td>
                    <td class="style26">
                        <strong>:</strong>
                    </td>
                    <td class="style27">
                        <asp:Label runat="server" ID="lblJobNo" ></asp:Label>
                        &nbsp;
                    </td>
                    <td class="style50">
                        &nbsp;
                    </td>
                    <td class="style29">
                        &nbsp;
                    </td>
                    <td class="style31">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style48">
                        Received For
                    </td>
                    <td class="style22">
                        <strong>:</strong>
                    </td>
                    <td class="style24">
                        <asp:Label ID="lblReceivedFor" runat="server" CssClass="style53"></asp:Label>
                    </td>
                    <td class="style25" style="font-size: 10pt; font-family: Calibri;">
                    </td>
                    <td class="style26">
                    </td>
                    <td class="style27">
                    </td>
                    <td class="style49" style="font-size: medium; font-weight: 700; text-align: right">
                        Total
                    </td>
                    <td class="style30">
                        <strong>:</strong>
                    </td>
                    <td class="style31">
                        <asp:Label ID="lblTotAmount" runat="server" Style="font-size: medium; text-align: right;
                            font-weight: 700; font-family: Calibri;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style19">
                        &nbsp;
                    </td>
                    <td class="style23">
                        &nbsp;
                    </td>
                    <td class="style24">
                        &nbsp;
                    </td>
                    <td class="style25">
                        &nbsp;
                    </td>
                    <td class="style26">
                        &nbsp;
                    </td>
                    <td class="style27">
                        &nbsp;
                    </td>
                    <td class="style50">
                        &nbsp;
                    </td>
                    <td class="style29">
                        &nbsp;
                    </td>
                    <td class="style31">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table style="width: 100%;">
                <tr>
                    <td class="style33" style="font-size: 10pt; font-family: Calibri;">
                        In Words
                    </td>
                    <td class="style34" style="font-size: medium">
                        <strong>:</strong>
                    </td>
                    <td>
                        <div style="border-bottom: 1px solid #000000; width: 60%;">
                            <asp:Label ID="lblInWords" runat="server" Style="font-size: 12pt; font-family: Calibri;"></asp:Label>
                            <div style="border-bottom: 1px solid #000000; width: 100%; margin-bottom: 10px;">
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="style33" style="font-size: medium">
                        &nbsp;
                    </td>
                    <td class="style34" style="font-size: medium">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table style="width: 100%;">
                <tr>
                    <td class="style38">
                        &nbsp;
                    </td>
                    <td class="style39">
                        &nbsp;
                    </td>
                    <td class="style42">
                        &nbsp;
                    </td>
                    <td class="style44">
                        &nbsp;
                    </td>
                    <td class="style43">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style38">
                        &nbsp;
                    </td>
                    <td class="style39">
                        Received By
                    </td>
                    <td class="style42">
                        Prepared By
                    </td>
                    <td class="style44">
                        Checked By
                    </td>
                    <td class="style43">
                        Authorised By
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
