<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptLcBasicInfo.aspx.cs" Inherits="AlchemyAccounting.LC.Report.Report.rptLcBasicInfo" %>

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
        .style12
    {
        width: 120px;
        font-size: 13px;
    }
    .style16
    {
        width: 2px;
    }
    .style17
    {
    }
    .style18
    {
        width: 1px;
        font-size: 13px;
    }
        .style38
    {
        width: 161px;
        font-size: 13px;
    }
    .style39
    {
        width: 115px;
    }
    .style40
    {
    }
    .style41
    {
        width: 317px;
    }
        .style42
    {
        width: 791px;
    }
    .style44
    {
        width: 1px;
    }
    .style45
    {
        width: 2px;
        font-size: 13px;
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
                    <strong>L/C BASIC INFORMATION</strong></td>
                <td style="text-align: right">
                    <asp:Label ID="lblTime" runat="server" 
                        style="text-align: right; font-family: Calibri; font-size: 14px;"></asp:Label>
                </td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            </table>
            <div style="width:96%; margin: 1% 0% 0% 2%;">
                <table style="width:100%; font-family: Calibri; font-size: 18px;">
                <tr>
                    <td style="font-size: medium;" class="style42">
                        <strong>L/C No. :</strong>&nbsp;
                        <asp:Label ID="lblLCNM" runat="server" style="font-weight: 700"></asp:Label>
                    </td>
                    <td style="font-size: medium; text-align: right;">
                        <strong>L/C Date : </strong>&nbsp;
                    </td>
                    <td style="font-size: medium;">
                        <asp:Label ID="lblLCDt" runat="server" style="font-weight: 700"></asp:Label>
                    </td>
                </tr>
                </table>
            </div>
            <div style="width:96%; margin: 1% 0% 0% 2%; border-radius: 5px; border: 1px solid #CCCCCC;">
                <table style="width:100%; font-family: Calibri; font-size: 13px;">
                <tr>
                    <td style="text-align: left;" class="style12">
                        <strong>Importer Name</strong></td>
                    <td style="text-align: center;" class="style18">
                        <strong style="text-align: center">:</strong></td>
                    <td class="style40">
                        <asp:Label ID="lblImporNM" runat="server" 
                            style="font-weight: 700; "></asp:Label>
                        </td>
                    <td class="style38">
                        <strong>Beneficiary</strong></td>
                    <td class="style45">
                        <strong>:</strong></td>
                    <td class="style17" colspan="4">
                        <asp:Label ID="lblBeneficiary" runat="server" 
                            style="font-weight: 700; "></asp:Label>
                        </td>
                </tr>
                <tr>
                    <td style="text-align: left;" class="style12">
                        <strong>MC Name</strong></td>
                    <td style="text-align: center;" class="style18">
                        <strong>:</strong></td>
                    <td class="style40">
                        <asp:Label ID="lblMCNM" runat="server" 
                            style="font-weight: 700; "></asp:Label>
                        </td>
                    <td class="style38">
                        <strong>MC No.</strong></td>
                    <td class="style45">
                        <strong>:</strong></td>
                    <td class="style41">
                        <asp:Label ID="lblMCNO" runat="server" 
                            style="font-weight: 700; "></asp:Label>
                        </td>
                    <td class="style39">
                        <strong>MC Date</strong></td>
                    <td class="style45">
                        <strong>:</strong></td>
                    <td style="font-size: medium;" class="style17">
                        <asp:Label ID="lblMcDT" runat="server" 
                            style="font-weight: 700; font-size: 15px"></asp:Label>
                        </td>
                </tr>
                <tr>
                    <td style="text-align: left;" class="style12">
                        <strong>SCPI No.</strong></td>
                    <td style="text-align: center;" class="style44">
                        :</td>
                    <td class="style40">
                        <asp:Label ID="lblScpiNo" runat="server" 
                            style="font-weight: 700; "></asp:Label>
                        </td>
                    <td class="style38">
                        <strong>SCPI Date</strong></td>
                    <td class="style45">
                        <strong>:</strong></td>
                    <td class="style41">
                        <asp:Label ID="lblScpiDT" runat="server" 
                            style="font-weight: 700; "></asp:Label>
                        </td>
                    <td class="style39">
                        &nbsp;</td>
                    <td class="style16">
                        &nbsp;</td>
                    <td style="font-size: medium;" class="style17">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left;" class="style12">
                        <strong>MPI No.</strong></td>
                    <td style="text-align: center;" class="style18">
                        <strong>:</strong></td>
                    <td class="style40">
                        <asp:Label ID="lblMpiNo" runat="server" 
                            style="font-weight: 700; "></asp:Label>
                        </td>
                    <td class="style38">
                        <strong>MPI Date</strong></td>
                    <td class="style45">
                        <strong>:</strong></td>
                    <td class="style41">
                        <asp:Label ID="lblMpiDT" runat="server" 
                            style="font-weight: 700; "></asp:Label>
                        </td>
                    <td class="style39">
                        &nbsp;</td>
                    <td class="style16">
                        &nbsp;</td>
                    <td style="font-size: medium;" class="style17">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left;" class="style12">
                        <strong>L/C Value (USD)</strong></td>
                    <td style="text-align: center;" class="style18">
                        <strong>:</strong></td>
                    <td class="style40">
                        <asp:Label ID="lblLcUSD" runat="server" 
                            style="font-weight: 700; "></asp:Label>
                        </td>
                    <td class="style38">
                        <strong>L/C Value Exchange Rate</strong></td>
                    <td class="style45">
                        <strong>:</strong></td>
                    <td class="style41">
                        <asp:Label ID="lblLcRT" runat="server" 
                            style="font-weight: 700; "></asp:Label>
                        </td>
                    <td class="style39">
                        <strong>L/C Value (BDT)</strong></td>
                    <td class="style45">
                        <strong>:</strong></td>
                    <td style="font-size: medium;" class="style17">
                        <asp:Label ID="lblBDT" runat="server" 
                            style="font-weight: 700; font-size: 13px"></asp:Label>
                        </td>
                </tr>
                <tr>
                    <td style="text-align: left;" class="style12">
                        <strong>Remarks</strong></td>
                    <td style="text-align: center;" class="style18">
                        <strong>:</strong></td>
                    <td class="style40" colspan="7">
                        <asp:Label ID="lblRemarks" runat="server" 
                            style="font-weight: 700; "></asp:Label>
                        </td>
                </tr>
                </table>
            </div>
            <div class = "MyCssClass" style = "width:96%; margin: 1% 2% 0% 2%;">

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" 
                    ShowHeaderWhenEmpty="True" ShowFooter="True" 
                    onrowdatabound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="SL" >
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Item Name">
                        <HeaderStyle HorizontalAlign="Center" Width="50%" />
                        <ItemStyle HorizontalAlign="Left" Width="50%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Qty">
                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="25%" />
                        <ItemStyle HorizontalAlign="Right" Width="25%" />
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
