<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptInStatement.aspx.cs" Inherits="AlchemyAccounting.Accounts.Report.Report.rptInStatement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Income Statement</title>
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
                font-size: x-large;
            text-align: center;
            width: 908px;
            font-weight: 700;
            font-family: Calibri;
        }
            .style2
            {
            font-size: medium;
            font-family: Calibri;
        }
            
        .SubTotalRowStyle
        {
            border: solid 2px Black;
            
            text-align: right;
        }
        .GrandTotalRowStyle
        {
            border: solid 1px Gray;
            color: #000000;
            font-weight: bold;
            font-size: 18px;
            text-align: right;
            height: 35px;
        }
        .GroupHeaderStyle
        {
            border: solid 1px Black;
            text-align: left;
            color: #000000;
            height: 30px;
            text-decoration: underline;
            font-size:11pt;
            font-weight: bold;
        }
        .GridRowStyle
        {
            padding-left: 3%;
        }
        .style8
        {
            text-align: center;
            width: 908px;
        }
        .style10
        {
            width: 142px;
        }
        .style11
        {
            font-size: large;
            text-align: center;
            width: 908px;
            font-family: Calibri;
        }
        
        .GrandGrandTotalRowStyle
        {
            border: solid 1px Gray;
            color: #000000;
            font-weight: bold;
            font-size: 18px;
            text-align: right;
            height: 30px;
            font-family:Calibri;
        }
        .style12
        {
            font-family: Calibri;
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
                    <asp:Label ID="lblCompNM" runat="server"></asp:Label>
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
                    <strong>INCOME STATEMENT</strong></td>
                <td style="text-align: right">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style10">
                    &nbsp;</td>
                <td class="style8">
                    <span class="style2">FROM </span> <strong><span class="style2">:&nbsp; 
                    </span> </strong>
                    <asp:Label ID="lblFDate" runat="server" CssClass="style2"></asp:Label>
                    <span class="style12"><span class="style13">&nbsp;&nbsp;&nbsp; TO </span> <strong>
                    <span class="style13">:&nbsp; </span> </strong>
                    </span>
                    <asp:Label ID="lblTDate" runat="server" CssClass="style2"></asp:Label>
                </td>
                <td style="text-align: right">
                    <asp:Label ID="lblTime" runat="server" 
                        style="text-align: right; font-family: Calibri; font-size: medium;"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>

            <div class = "MyCssClass" style = "width:96%; margin: 1% 2% 0% 2%;">

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    onrowcreated="GridView1_RowCreated" 
                    onrowdatabound="GridView1_RowDataBound" Width="100%" 
                    ShowHeaderWhenEmpty="True" ShowFooter="True">
                    <Columns>
                        <asp:BoundField HeaderText="Head Particulars" >
                        <HeaderStyle HorizontalAlign="Center" Width="80%" />
                        <ItemStyle Width="80%" HorizontalAlign="Left" CssClass="GridRowStyle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                        <ItemStyle HorizontalAlign="Right" Width="20%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle CssClass="GrandGrandTotalRowStyle" />
                    <HeaderStyle Font-Bold="True" Font-Names="Calibri" Font-Size="18px" />
                    <RowStyle Font-Size="15px" Font-Names="Calibri" />
                </asp:GridView>

            </div>

        </div>
    </div>
    </form>
</body>
</html>
