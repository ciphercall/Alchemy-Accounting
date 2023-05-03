<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportCashBook.aspx.cs" Inherits="AlchemyAccounting.Accounts.Report.Report.ReportCashBook" %>

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
            width: 950px;
        }
        .style9
        {
            text-align: left;
            width: 950px;
        }
        .style16
        {
            width: 711px;
            font-weight: bold;
        }
        .style19
        {
            width: 186px;
        }
        .style13
        {
            width: 2px;
            font-weight: 700;
        }
        .style22
        {
            font-family: Calibri;
        }
        .style23
        {
            width: 204px;
            text-align: right;
            font-family: Calibri;
            font-size: 16px;
        }
        .style24
        {
            width: 204px;
            text-align: right;
            font-family: Calibri;
            font-size: 15px;
        }
        .style25
        {
            width: 8px;
        }
        .style26
        {
            font-weight: bold;
            width: 166px;
        }
        .style27
        {
            font-weight: bold;
            width: 157px;
        }
        .style1
        {
            font-family: Calibri;
            font-size: 20px;
            width: 950px;
        }
        .style28
        {
            width: 418px;
        }
        .style29
        {
            width: 2px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table style="width: 100%;">
            <tr>
                <td class="style29">
                    &nbsp;</td>
                <td class="style1">
                    <asp:Label ID="lblCompNM" runat="server" style="font-weight: 700"></asp:Label>
                </td>
                <td style="text-align: right">
                    &nbsp;</td>
                <td style="text-align: right" class="style28">
                    <input id="print" tabindex="1" type="button" value="Print" onclick = "ClosePrint()"/></td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style29">
                    &nbsp;</td>
                <td class="style9">
                    <asp:Label ID="lblAddress" runat="server" 
                        style="font-family: Calibri; font-size: 9px"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td style="text-align: right" class="style28">
                    <strong style="text-align: center; font-family: Calibri; font-size: 16px;">CASH BOOK</strong></td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style29">
                    &nbsp;</td>
                <td class="style9">
                    <asp:Label ID="lblHeadNM" runat="server" 
                        style="font-weight: 700; font-size: 16px; font-family: Calibri;"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td style="text-align: right" class="style28">
                    <asp:Label ID="lblTime" runat="server" 
                        style="font-family: Calibri; font-size: 16px"></asp:Label>
                </td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style29">
                    &nbsp;</td>
                <td class="style8">
                    PERIOD <strong>:&nbsp; </strong>
                    <asp:Label ID="lblFrom" runat="server"></asp:Label>
                    &nbsp;TO&nbsp;&nbsp;
                    <asp:Label ID="lblTo" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td style="text-align: right" class="style28">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style29">
                    &nbsp;</td>
                <td class="style8">
                    <strong><span class="style22">
                    <asp:Label ID="lblOpenBal" 
                        runat="server" CssClass="style22" Visible="False"></asp:Label>
                    </span></strong></td>
                <td>
                    &nbsp;</td>
                <td style="text-align: right" class="style28">
                    <strong><span class="style22">Opening Balance (Tk.):&nbsp;&nbsp;<asp:Label ID="lblOpenBalComma" 
                        runat="server" CssClass="style22"></asp:Label>
                    </span></strong></td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
        
            <div style= "margin-top:0px;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                Width="100%" ShowHeaderWhenEmpty="True" 
                onrowdatabound="GridView1_RowDataBound">
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
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
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
                    <td class="style23">
                        <strong style="text-align: right">Periodic Total</strong></td>
                    <td class="style13">
                        :</td>
                    <td class="style27" style="text-align: right; border-top: 1px solid;">
                        <asp:Label ID="lblPeriodicDB" runat="server" 
                            style="font-weight: 700; font-family: Calibri; font-size: 16px;"></asp:Label>
                    </td>
                    <td style="text-align: right;" class="style25">
                        &nbsp;</td>
                    <td style="text-align: right;border-top: 1px solid;">
                        <asp:Label ID="lblPeriodicCR" runat="server" 
                            style="font-weight: 700; font-family: Calibri; font-size: 16px;"></asp:Label>
                    &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style24">
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
                    
                    <asp:Label ID="lblLastCumBalance" runat="server" style="font-weight: 700; font-family: Calibri; font-size: 16px;" 
                        Visible="False"></asp:Label>
                    
                    Total :</td>
                <td style="border: 1px solid #cccccc; text-align: right;" class="style26">
                    
                    <asp:Label ID="lblTotBalance" runat="server" 
                        style="font-family: Calibri; font-size: 16px"></asp:Label>
                    
                </td>
                <td style="border: 1px solid #cccccc; text-align: right;" class="style19">
                    
                    <asp:Label ID="lblTotCR" runat="server" 
                        style="font-weight: 700; font-size: 16px;" CssClass="style22"></asp:Label>
                    
                </td>
                <td style="border: 1px solid #cccccc; text-align: right;">
                    
                    <asp:Label ID="lblLastCumBalC" runat="server" 
                        style="font-weight: 700; font-size: 16px;" CssClass="style22"></asp:Label>
                    
                </td>
            </tr>
        </table>
        </div>
    </div>
    </form>
</body>
</html>
