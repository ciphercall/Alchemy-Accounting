<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportBankBook.aspx.cs" Inherits="AlchemyAccounting.Accounts.Report.Report.ReportBankBook" %>

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
            border: 1px solid #cccccc;
            width: 100%;
            padding-bottom:40px;
        }
                #btnPrint
        {
            font-weight: 700;
            font-style: italic;
        }
        .style8
        {
            text-align: left;
            font-family: Calibri;
            font-size: 16px;
            width: 836px;
        }
        .style9
        {
            text-align: left;
            width: 836px;
        }
        .style13
        {
            width: 2px;
            font-weight: 700;
        }
        .style16
        {
            width: 709px;
            font-weight: bold;
        }
        .style18
        {
            width: 188px;
            font-weight: bold;
        }
        .style19
        {
            width: 183px;
        }
        .style23
        {
            font-size: 20px;
            font-weight: 700;
            font-family: Calibri;
            width: 836px;
        }
        .style24
        {
            font-family: Calibri;
            font-size: 16px;
        }
        .style25
        {
            width: 19px;
        }
        .style26
        {
            width: 384px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table style="width: 100%;">
            <tr>
                <td class="style6">
                    &nbsp;</td>
                <td class="style23">
                    <asp:Label ID="lblCompNM" runat="server"></asp:Label>
                </td>
                <td style="text-align: right">
                    &nbsp;</td>
                <td style="text-align: right" class="style26">
                    <input id="print" tabindex="1" type="button" value="Print" onclick = "ClosePrint()"/></td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style9">
                    <asp:Label ID="lblAddress" runat="server" 
                        style="font-family: Calibri; font-size: 9px"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td style="text-align: right" class="style26">
                    <strong style="text-align: center; font-family: Calibri;">BANK BOOK</strong></td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style9">
                    <asp:Label ID="lblHeadNM" runat="server" 
                        style="font-weight: 700; font-size: 16px; font-family: Calibri;"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td style="text-align: right" class="style26">
                    <asp:Label ID="lblTime" runat="server" style="font-family: Calibri"></asp:Label>
                </td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style8">
                    <span class="style24">PERIOD</span> <strong>:&nbsp; </strong>
                    <asp:Label ID="lblFrom" runat="server" 
                        style="font-family: Calibri; font-size: 16px"></asp:Label>
                    <strong>&nbsp;&nbsp;&nbsp;</strong>TO&nbsp;
                    <asp:Label ID="lblTo" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td style="text-align: right" class="style26">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style8">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td style="text-align: right" class="style26">
                    <strong><span class="style24">Opening Balance (Tk.):&nbsp;&nbsp; </span>
                    <asp:Label ID="lblOpenBal" runat="server" CssClass="style24"></asp:Label>
                    </strong>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
        
            <div style="margin-top: 0px;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                Width="100%" onrowdatabound="GridView1_RowDataBound" 
                ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:BoundField HeaderText="Date" >
                    <HeaderStyle HorizontalAlign="Center" Width="8%" />
                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Doc. No" >
                    <HeaderStyle HorizontalAlign="Center" Width="8%" />
                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Particulars" >
                    <HeaderStyle HorizontalAlign="Center" Width="39%" />
                    <ItemStyle HorizontalAlign="Left" Width="39%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Debit (Tk.)" >
                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
                    <ItemStyle HorizontalAlign="Right" Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Credit (Tk.)" >
                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
                    <ItemStyle HorizontalAlign="Right" Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Balance (Tk.)">
                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
                    <ItemStyle HorizontalAlign="Right" Width="15%" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle Font-Names="Calibri" Font-Size="16px" />
                <RowStyle Font-Names="Calibri" Font-Size="14px" />
            </asp:GridView>
            </div>
        
                    <div style="width:45%; margin: 0% 15% 0% 40%;">
                        <br />
                        <table style="width:99%;">
                            <tr>
                                <td style="text-align: right">
                                    <strong style="text-align: right; font-size: 16px; font-family: Calibri;">Periodic Total</strong></td>
                                <td class="style13">
                                    :</td>
                                <td class="style27" style="text-align: right; border-top: 1px solid;">
                                    <asp:Label ID="lblPeriodicDB" runat="server" 
                                        style="font-weight: 700; font-family: Calibri; font-size: 16px;"></asp:Label>
                                </td>
                                <td class="style25" style="text-align: right;">
                                    &nbsp;</td>
                                <td style="text-align: right;border-top: 1px solid;">
                                    <asp:Label ID="lblPeriodicCR" runat="server" 
                                        style="font-weight: 700; font-family: Calibri; font-size: 16px;"></asp:Label>
                                    &nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <strong style="font-family: Calibri; font-size: 16px;">Periodic Balance</strong></td>
                                <td>
                                    <strong>:</strong></td>
                                <td class="style27" style="text-align: right;border-top: 1px solid;">
                                    <asp:Label ID="lblPeriodicBalance" runat="server" 
                                        style="font-weight: 700; text-align: right; font-family: Calibri; font-size: 16px;"></asp:Label>
                                </td>
                                <td class="style25">
                                    &nbsp;</td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <br />
            </div>
        <table style="width:100%; border: 1px solid #cccccc;">
            <tr style="border: 1px solid #cccccc;">
                <td style="border: 1px solid #cccccc; text-align: right; font-family: Calibri; font-size: 16px;" 
                    class="style16">
                    
                    Total :</td>
                <td style="border: 1px solid #cccccc; text-align: right;" class="style18">
                    
                    <asp:Label ID="lblTotBalance" runat="server" 
                        style="font-family: Calibri; font-size: 16px"></asp:Label>
                    
                </td>
                <td style="border: 1px solid #cccccc; text-align: right;" class="style19">
                    
                    <asp:Label ID="lblTotCR" runat="server" 
                        style="font-weight: 700; font-family: Calibri; font-size: 16px;"></asp:Label>
                    
                </td>
                <td style="border: 1px solid #cccccc; text-align: right;">
                    
                    <asp:Label ID="lblLastCumBalance" runat="server" 
                        style="font-weight: 700; font-family: Calibri; font-size: 16px;"></asp:Label>
                    
                </td>
            </tr>
        </table>
        </div>
    </div>
    </form>
</body>
</html>
