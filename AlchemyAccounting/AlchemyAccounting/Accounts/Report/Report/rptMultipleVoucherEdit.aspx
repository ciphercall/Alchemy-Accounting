<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptMultipleVoucherEdit.aspx.cs" Inherits="AlchemyAccounting.Accounts.Report.Report.rptMultipleVoucherEdit" %>

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
        .style5
        {
            font-size: smaller;
            width: 190px;
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
        .style56
        {
            width: 473px;
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
            font-family: Calibri;
            text-align: right;
        }
        .style63
        {
            text-align: center;
            font-family: Calibri;
            width: 315px;
            font-size: medium;
        }
        .style64
        {
            height: 19px;
            text-align: right;
            font-family: Calibri;
            width: 315px;
        }
        .style66
        {
            width: 473px;
        }
        .style67
        {
            width: 315px;
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
                            <span style="font-family:Cambria(Heading); font-size:18px">TRADE ABROAD</span>
                        </td>
                        <td style="width: 10%">
                           
                                 <%-- <img src="../../../Images/logo.png" width="100%" height="100%;" alt="logo" />--%>
                               
                           
                        </td>
                    </tr>
                </table>
              </div> 
            <div>
                  <table style="width: 100%">
                    <tr>
                        <td style="width: 33%"></td>
                        <td style="width: 34%; text-align: center; border: 1px solid #000; border-radius: 5px;">
                           MULTIPLE VOUCHER</td>
                        <td style="width: 33%">
                            <table>
                                <tr>
                                    <td>Voucher No</td>
                                    <td><strong>:</strong></td>
                                    <td>
                                         <asp:Label ID="lblVNo" runat="server" 
                                                CssClass="style61"></asp:Label></td>
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
                
               </div> 
            <br />
            <div>
                
                <asp:GridView ID="gvdetails" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="gvdetails_RowDataBound" ShowFooter="True" 
                    style="font-family: Calibri" Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="SL">
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Debited To" >
                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Credited To" >
                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Reamarks" >
                        <HeaderStyle HorizontalAlign="Center" Width="35%" />
                        <ItemStyle HorizontalAlign="Left" Width="35%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Amount" >
                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                        <ItemStyle HorizontalAlign="Right" Width="20%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle Font-Names="Calibri" Font-Size="14px" />
                    <HeaderStyle Font-Names="Calibri" Font-Size="14px" />
                    <RowStyle Font-Names="Calibri" Font-Size="12px" />
                </asp:GridView>
                
            </div>

            <div style="margin-top: 1%;">
                
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
                
                <table style="width:100%; font-family: Calibri; padding-top:25px">
                    <tr>
                        <td style="width:33%; text-align:center;"></td>
                        <td style="width:34%; text-align:center;"></td>
                         <td style="width:33%; text-align:center;"></td>
                    </tr>
                    <tr>
                        <td style="width:33%; text-align:center;">Prepared By</td>
                        <td style="width:34%; text-align:center;"> Checked By</td>
                         <td style="width:33%; text-align:center;">Approved By</td>
                    </tr>
                   
                </table>
                
            </div>
        </div>
    </form>
</body>
</html>
