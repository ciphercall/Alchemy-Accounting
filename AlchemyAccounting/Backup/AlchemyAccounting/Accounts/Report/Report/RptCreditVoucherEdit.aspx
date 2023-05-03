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

    <script type ="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            //            print.display = false;

            window.print();
        }
    </script>

    <style type ="text/css">
        #main
        {
            float:left;
            border: 1px solid #000000;
            width: 100%;
            padding-bottom:40px;
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
        .style2
        {
            width: 862px;
        }
        .style3
        {
            font-size: 11px;
            width: 862px;
        }
        .style12
        {
            width: 2px;
            font-size: medium;
            font-weight: bold;
        }
        .style14
        {
            width: 529px;
        }
        .style15
        {
            width: 1051px;
        }
        .style16
        {
            font-size: 18px;
            width: 182px;
            font-family: Calibri;
        }
        .style18
        {
            width: 182px;
        }
        .style19
        {
            width: 221px;
            font-size: 10pt;
            font-family: Calibri;
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
            width: 275px;
        }
        .style25
        {
            width: 144px;
        }
        .style26
        {
            width: 3px;
            font-size: medium;
            font-weight: bold;
        }
        .style27
        {
            width: 381px;
        }
        .style28
        {
            width: 191px;
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
        .style46
        {
            font-family: Calibri;
        }
        .style47
        {
            width: 1051px;
            font-family: Calibri;
            font-size: 18px;
        }
        .style48
        {
            font-family: Calibri;
            font-size: 16px;
        }
        .style50
        {
            width: 211px;
            font-size: 10pt;
            font-family: Calibri;
        }
        .style52
        {
            font-family: Calibri;
            font-size: 10pt;
        }
        .style53
        {
            width: 572px;
        }
        .style55
        {
            width: 300px;
        }
        .style56
        {
            width: 480px;
            height: 19px;
        }
        .style58
        {
            height: 19px;
        }
        .style59
        {
            height: 19px;
            text-align: right;
            font-family: Calibri;
            width: 186px;
        }
        .style60
        {
            text-align: right;
            font-family: Calibri;
            width: 186px;
        }
        .style61
        {
            font-family: Calibri;
            font-size: small;
        }
        .style62
        {
            width: 186px;
        }
        .style63
        {
            text-align: right;
            font-family: Calibri;
            width: 2px;
        }
        .style64
        {
            height: 19px;
            text-align: right;
            font-family: Calibri;
            width: 2px;
        }
        .style65
        {
            width: 2px;
        }
        .style66
        {
            width: 480px;
        }
    </style>
</head>
<body style="font-size: small">
    <form id="form1" runat="server">
        <div id="main">
            <div>
                <table style="width:100%; border-bottom: 1px solid #000000;">
                    <tr>
                        <td style="text-align: center; font-size: xx-large; font-weight: 700" 
                            class="style64" rowspan="3">
                                <div style="width: 140px; height: 80px;">
                                    <img src="../../../Images/logo.png" width="100%" height="100%;" alt="logo" />
                                </div></td>
                        <td style="text-align: center; font-size: xx-large; font-weight: 700" 
                            class="style2">
                    <asp:Label ID="lblCompNM" runat="server" style="font-family: Calibri; font-size: 23px"></asp:Label>
                        </td>
                        <td style="text-align: center; font-size: xx-large; font-weight: 700">
                        <input id="print" tabindex="1" type="button" value="Print" onclick = "ClosePrint()"/></td>
                    </tr>
                    <tr>
                        <td style="text-align: center" class="style2">
                    <asp:Label ID="lblAddress" runat="server" 
                        style="font-family: Calibri; font-size: 10px"></asp:Label>
                        </td>
                        <td style="text-align: center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style3" 
                        
                            style="text-align: center; font-weight: 700; font-family: Calibri">
                            TELEPHONE :
                    <asp:Label ID="lblContact" runat="server" 
                        style="font-family: Calibri; font-size: 11px"></asp:Label>
                        </td>
                        <td class="style1" 
                            style="text-align: center; font-weight: 700; font-family: 'Helvetica Neue', Arial, Helvetica, sans-serif">
                            &nbsp;</td>
                    </tr>
                </table>
              </div>  
            
            <div>
                <table style="width:100%;">
                    <tr>
                        <td class="style66">
                            &nbsp;</td>
                        <td class="style55" rowspan="3">
                            <div style="width:100%; height:60px; border: 1px solid #000000; border-radius: 10px;">
                                <table style="width:100%;">
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
                                            <asp:Label ID="lblVtype" runat="server" style="font-family: Calibri"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td class="style63">
                                            &nbsp;</td>
                        <td class="style60">
                                            Voucher No</td>
                                        <td class="style29" style="text-align: left">
                                            <strong style="font-size: medium">:</strong></td>
                        <td>
                                            <asp:Label ID="lblVNo" runat="server" 
                                                CssClass="style61"></asp:Label>
                                        </td>
                    </tr>
                    <tr>
                        <td class="style56">
                        </td>
                        <td class="style64">
                                            &nbsp;</td>
                        <td class="style59">
                                            Voucher&nbsp; Date</td>
                                        <td class="style30" style="text-align: right">
                                            <strong>:</strong></td>
                        <td class="style58">
                        <asp:Label ID="lblTime" runat="server" CssClass="style61"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style66">
                            &nbsp;</td>
                        <td class="style65">
                            &nbsp;</td>
                        <td class="style62">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <table style="width:100%;">
                    <%--tr>
                        <td class="style11">
                            &nbsp;</td>
                        <td class="style12">
                            &nbsp;</td>
                        <td class="style53">
                            &nbsp;</td>
                        <td style="text-align: center" class="style14">
                            </td>
                        <td style="width: 30%;">
                            <div style="width:100%;">
                            
                                <table style="width:100%;">
                                    <tr>
                                        <td class="style37" style="text-align: right; font-size: medium">
                                            &nbsp;</td>
                                        <td style="text-align: left">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style37" style="text-align: right; font-size: medium">
                                            &nbsp;</td>
                                        <td style="text-align: left">
                                            &nbsp;</td>
                                    </tr>
                                    </table>
                            
                            </div>
                            </td>
                    </tr>--%>
                    <tr>
                        <td class="style50">
                            Received By</td>
                        <td class="style12">
                            :</td>
                        <td class="style53">
                            <asp:Label ID="lblReceivedBy" runat="server" 
                                CssClass="style52"></asp:Label>
                        </td>
                        <td class="style14">
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="lblMidDate" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style50">
                            Received From</td>
                        <td class="style12">
                            :</td>
                        <td class="style53">
                            <asp:Label ID="lblReceivedFrom" runat="server" 
                                CssClass="style52"></asp:Label>
                        </td>
                        <td class="style14">
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="lblAmount" runat="server" style="font-size: large; font-family: Calibri;" 
                                Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
               </div> 
            <br />
            <div>
                <table style="width:100%;">
                    <tr>
                        <td style="border: 1px solid #000000; text-align: center; font-weight: 700;" 
                            class="style47">
                            Particulars</td>
                        <td style="border: 1px solid #000000; text-align: center; font-weight: 700;" 
                            class="style16">
                            Amount (QR)</td>
                    </tr>
                    <tr>
                        <td style="border: 1px solid #cccccc; height: 40px;" class="style15">
                            <asp:Label ID="lblParticulars" runat="server" CssClass="style48"></asp:Label>
                        </td>
                        <td style="border: 1px solid #cccccc; text-align: right; height: 40px;" class="style18">
                            <asp:Label ID="lblAmountComma" runat="server" CssClass="style48"></asp:Label>
                        </td>
                    </tr>
                    </table>
                </div>
                <br />
                <div>
                    <table style="width:100%;">
                        <tr>
                            <td class="style19">
                                Mode of Received </td>
                            <td class="style22">
                                <strong>:</strong></td>
                            <td class="style24">
                                <asp:Label ID="lblRMode" runat="server" 
                                    CssClass="style52"></asp:Label>
                            </td>
                            <td class="style25">
                                &nbsp;</td>
                            <td class="style26">
                                &nbsp;</td>
                            <td class="style27">
                                &nbsp;</td>
                            <td class="style28">
                                &nbsp;</td>
                            <td class="style29">
                                &nbsp;</td>
                            <td class="style31">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style19">
                                Cheque No</td>
                            <td class="style22">
                                <strong>:</strong></td>
                            <td class="style24">
                                <asp:Label ID="lblChequeNo" runat="server" 
                                    CssClass="style52"></asp:Label>
                            </td>
                            <td class="style25" style="font-size: 10pt; font-family: Calibri;">
                                Cheque Date</td>
                            <td class="style26">
                                :</td>
                            <td class="style27">
                                <asp:Label ID="lblChequeDT" runat="server" style="font-size: 10pt" 
                                    CssClass="style46"></asp:Label>
                            </td>
                            <td class="style28" 
                                
                                style="font-size: medium; font-weight: 700; text-align: right; font-family: Calibri;">
                                Total</td>
                            <td class="style30">
                                <strong>:</strong></td>
                            <td class="style31">
                                <asp:Label ID="lblTotAmount" runat="server" 
                                    style="font-size: medium; text-align: right; font-weight: 700;" 
                                    CssClass="style46"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style19">
                                &nbsp;</td>
                            <td class="style23">
                                &nbsp;</td>
                            <td class="style24">
                                &nbsp;</td>
                            <td class="style25">
                                &nbsp;</td>
                            <td class="style26">
                                &nbsp;</td>
                            <td class="style27">
                                &nbsp;</td>
                            <td class="style28">
                                &nbsp;</td>
                            <td class="style29">
                                &nbsp;</td>
                            <td class="style31">
                                &nbsp;</td>
                        </tr>
                    </table>
            </div>
            <div>
                
                <table style="width:100%; font-family: Calibri;">
                    <tr>
                        <td class="style33" style="font-size: 10pt">
                            In Words</td>
                        <td class="style34" style="font-size: medium">
                            <strong>:</strong></td>
                        <td>
                            <div style=" border-bottom: 1px solid #000000; width: 60%;">
                                <asp:Label ID="lblInWords" runat="server" style="font-size: 10pt;"></asp:Label>
                                <div style=" border-bottom: 1px solid #000000; width: 100%; margin-bottom:10px;"></div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="style33" style="font-size: medium">
                            &nbsp;</td>
                        <td class="style34" style="font-size: medium">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                
            </div>
            <div>
                
                <table style="width:100%; font-family: Calibri;">
                    <tr>
                        <td class="style38">
                            &nbsp;</td>
                        <td class="style39">
                            &nbsp;</td>
                        <td class="style42">
                            &nbsp;</td>
                        <td class="style44">
                            &nbsp;</td>
                        <td class="style43">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style38">
                            &nbsp;</td>
                        <td class="style39">
                            Received By</td>
                        <td class="style42">
                            Prepared By</td>
                        <td class="style44">
                            Checked By</td>
                        <td class="style43">
                            Authorised By</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                
            </div>
        </div>
    </form>
</body>
</html>
