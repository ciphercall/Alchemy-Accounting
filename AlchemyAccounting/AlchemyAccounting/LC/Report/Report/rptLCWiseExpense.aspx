<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptLCWiseExpense.aspx.cs" Inherits="AlchemyAccounting.LC.Report.Report.rptLCWiseExpense" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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

        .MyCssClass thead
         {
            display: table-header-group;
            border: 1px solid #000;
         }

    </style>

    <style type ="text/css">
    #btnPrint
        {
            font-weight: 700;
        }
            .style1
            {
                font-size: small;
            text-align: center;
            width: 908px;
        }
                        
        .SubTotalRowStyle
        {
            border: solid 1px Black;
            font-weight: bold;
            text-align: right;
            font-family:Calibri;
            font-size: 14px;
            height:25px;
        }
        .GrandTotalRowStyle
        {
            border: solid 1px Gray;
            color: #000000;
            font-weight: bold;
            font-size: 16px;
            text-align: right;
            height: 28px;
        }
        .GroupHeaderStyle
        {
            float: left;
            border: none;
            text-align: left;
            color: #000000;
            height: 20px;
            font-size:15px;
            font-weight: bold;
            margin-left: 15px;
            margin-top: 5px;
        }
        .GridRowStyle
        {
            padding-left: 10%;
        }
        .style10
        {
            width: 142px;
        }
        .style11
        {
            font-size: medium;
            text-align: center;
            width: 908px;
            font-family: Calibri;
        }
        
        .GrandGrandTotalRowStyle
        {
            border: solid 1px Gray;
            color: #000000;
            font-weight: bold;
            font-size: 16px;
            text-align: right;
            height: 30px;
            font-family:Calibri;
        }
        .style13
        {
            font-size: medium;
        }
        </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table style="width: 100%;">
            <tr>
                <td class="style10">
                    &nbsp;</td>
                <td class="style1">
                    <asp:Label ID="lblCompNM" runat="server" 
                        style="font-family: Calibri; font-size: 20px; font-weight: 700"></asp:Label>
                </td>
                <td style="text-align: right">
                    <input id="print" tabindex="1" type="button" value="Print" onclick = "ClosePrint()"/></td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style10">
                    &nbsp;</td>
                <td class="style11">
                    <asp:Label ID="lblAddress" runat="server" 
                        style="font-family: Calibri; font-size: 9px"></asp:Label>
                </td>
                <td style="text-align: right">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style10">
                    &nbsp;</td>
                <td class="style11">
                    <strong>L/C WISE EXPENSES</strong></td>
                <td style="text-align: right">
                    <asp:Label ID="lblTime" runat="server" 
                        style="text-align: right; font-family: Calibri; font-size: 14px;"></asp:Label>
                </td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            </table>
            <table style="width:96%; margin: 0% 0% 0% 2%; font-family: Calibri; font-size: 18px;">
                <tr>
                    <td style="font-size: medium">
                        <asp:Label ID="lblLCID" runat="server" 
                            style="font-family: Calibri; font-weight: 700;" CssClass="style13"></asp:Label>
                    </td>
                </tr>
            </table>
            <div class = "MyCssClass" style = "width:96%; margin: 1% 2% 0% 2%;">

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    onrowcreated="GridView1_RowCreated" 
                    onrowdatabound="GridView1_RowDataBound" Width="100%" 
                    ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:BoundField HeaderText="Date" >
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Charge Name">
                        <HeaderStyle HorizontalAlign="Center" Width="30%" />
                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Cash/Bank Name">
                        <HeaderStyle HorizontalAlign="Center" Width="40%" />
                        <ItemStyle HorizontalAlign="Left" Width="40%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                        <ItemStyle HorizontalAlign="Right" Width="20%" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle Font-Names="Calibri" Font-Size="16px" />
                    <RowStyle Font-Size="14px" Font-Names="Calibri" />
                </asp:GridView>

            </div>

        </div>
    </div>
    </form>
</body>
</html>
