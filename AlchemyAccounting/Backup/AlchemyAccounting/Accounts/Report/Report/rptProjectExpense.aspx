<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptProjectExpense.aspx.cs" Inherits="AlchemyAccounting.Accounts.Report.Report.rptProjectExpense" %>

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
        .GrandGrandTotalRowStyle
        {
            border: solid 1px Gray;
            color: #000000;
            font-weight: bold;
            font-size: 16px;
            font-family:Calibri;
            text-align: right;
            height: 30px;
        }
        .style1
        {
            text-align: center;
            width: 765px;
            font-family: Calibri;
        }
        .style12
        {
            width: 273px;
        }
        .style14
        {
            text-align: center;
            width: 765px;
            font-family: Calibri;
            font-size: 20px;
            font-weight: 700;
        }
        .style15
        {
            font-size: 15px;
        }
        .style16
        {
            width: 279px;
        }
        .style17
        {
            width: 10px;
        }
        .style18
        {
            font-family: Calibri;
            font-size: 15px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table style="width: 100%;">
            <tr>
                <td class="style12">
                    &nbsp;</td>
                <td class="style14">
                    <asp:Label ID="lblCompNM" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" class="style16">
                    <input id="print" tabindex="1" type="button" value="Print" onclick = "ClosePrint()"/></td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style12">
                    &nbsp;</td>
                <td class="style1">
                    <asp:Label ID="lblAddress" runat="server" 
                        style="font-family: Calibri; font-size: 9px"></asp:Label>
                </td>
                <td style="text-align: right" class="style16">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style12">
                    &nbsp;</td>
                <td class="style1">
                    <strong>BRANCH WISE EXPENSE STATEMENT</strong></td>
                <td style="text-align: right" class="style16">
                    <asp:Label ID="lblTime" runat="server" 
                        style="text-align: right; font-family: Calibri; font-size: 14px;"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style12">
                    &nbsp;</td>
                <td class="style1">
                    <span class="style15">From : </span>
                    <asp:Label ID="lblFromDT" runat="server" CssClass="style15"></asp:Label>
                    <span class="style15">&nbsp; To : </span>
                    <asp:Label ID="lblToDT" runat="server" CssClass="style15"></asp:Label>
                </td>
                <td style="text-align: right" class="style16">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>



                        <div class = "MyCssClass" style = "width:96%; margin: 1% 2% 0% 2%;">

                            <table style="width:100%;">
                                <tr>
                                    <td class="style17">
                                        &nbsp;</td>
                                    <td class="style18">
                                        <strong>Branch ID :&nbsp;
                                        <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                </table>

                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                Font-Size="12px" onrowdatabound="GridView1_RowDataBound" ShowFooter="True" 
                                ShowHeaderWhenEmpty="True" Width="100%">
                                <Columns>
                                    <asp:BoundField>
                                    <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                    <ItemStyle HorizontalAlign="Right" Width="12%" />
                                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Account Head Particulars">
                                    <HeaderStyle HorizontalAlign="Center" Width="60%" />
                                    <ItemStyle HorizontalAlign="Left" Width="60%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Amount (Tk.)">
                                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                    <ItemStyle HorizontalAlign="Right" Width="20%" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle Font-Names="Calibri" 
                                    Font-Size="14px" Font-Bold="True" />
                                <HeaderStyle Font-Names="Calibri" Font-Size="16px" />
                                <RowStyle Font-Names="Calibri" Font-Size="12px" />
                            </asp:GridView>

            </div>

        </div>
    </div>
    </form>
</body>
</html>
