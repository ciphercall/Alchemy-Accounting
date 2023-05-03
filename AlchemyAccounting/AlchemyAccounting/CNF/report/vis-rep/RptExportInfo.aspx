<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptExportInfo.aspx.cs" Inherits="AlchemyAccounting.CNF.report.vis_rep.RptExportInfo" %>

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
    <style media="print" type="text/css">
        .ShowHeader thead
        {
            display: table-header-group;
            border: 1px solid #000;
        }
        
        .line-separator
        {
            height: 1px;
            background: #717171;
            border-bottom: 1px solid #313030;
        }
    </style>
    <style type="text/css">
        .style1
        {
            font-size: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%">
            <tr>
                <td style="width: 10%" rowspan="4">
                </td>
                <td style="width: 80%; text-align: center; font-family: Calibri; font-size: 25px">
                    <asp:Label runat="server" ID="lblCompanyNM"></asp:Label>
                    &nbsp;
                </td>
                <td style="width: 10%">
                    <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()"
                        style="font-family: Calibri; font-size: 15px; font-weight: bold; font-style: inherit" />
                </td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: center; font-family: Calibri; font-size: 25px">
                    <asp:Label runat="server" ID="lblADD" style="font-size: 9px"></asp:Label>
                </td>
                <td style="width: 10%">
                    &nbsp;
                </td>
            </tr>
        </table>
    <table style="width: 100%">
             <tr>
                <td style="width: 2%; text-align: left">
                </td>
                <td style="width: 13%; text-align: left; font-family: Calibri; font-size: 14px;">
                    Job Type
                </td>
                <td style="width: 1%; text-align: center">
                    :
                </td>
                <td style="width: 28%; font-family: Calibri; font-size: 14px;">
                    <asp:Label runat="server" ID="lblJobType"></asp:Label>
                </td>
             
                <td style="width: 56%; text-align: right; font-family: Calibri;" class="style1">
                   Print date :
                    <asp:Label ID="lblPrintDate" runat="server" Font-Names="Calibri"></asp:Label>
                </td>
            </tr>

        </table>
        <table style="width: 100%">
            <tr>
                <td style="width: 2%; text-align: left">
                </td>
                <td style="width: 13%; text-align: left; font-family: Calibri; font-size: 14px;">
                    Party Name
                </td>
                <td style="width: 1%; text-align: center">
                    :
                </td>
                <td style="font-family: Calibri; font-size: 14px;">
                    <asp:Label runat="server" ID="lblPartyNM"></asp:Label>
                    &nbsp; &nbsp;
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 2%; text-align: left">
                </td>
                <td style="width: 13%; text-align: left; font-family: Calibri; font-size: 14px;">
                    Addresss</td>
                <td style="width: 1%; text-align: center">
                    :
                </td>
                <td colspan="3">
                    <asp:Label runat="server" ID="lblBranch" Font-Names="Calibri;font-size:14px;"></asp:Label>
                    &nbsp;
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 2%; text-align: left">
                    &nbsp;</td>
                <td style="width: 13%; text-align: left; font-family: Calibri; font-size: 14px;">
                    &nbsp;</td>
                <td style="width: 1%; text-align: center">
                    &nbsp;</td>
                <td style="width: 43%">
                    &nbsp;</td>
                <td style="width: 15%; text-align: right; font-family: Calibri; font-size: 14px;">
                    &nbsp;</td>
                <td style="width: 27%; font-size: 14px; font-family: Calibri">
                    &nbsp;</td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 2%; text-align: left">
                 
                </td>
                <td style="width: 98%; text-align: left; font-family: Calibri; font-size: 14px;">
                

                    SUB : Forwarding Bill  details as follows : <asp:Label runat="server" ID="lblRemarks"></asp:Label>
                </td>
               
            </tr>
        </table>
        <div style="width: 96%; margin: 1% 2% 0% 2%;">
            <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" Font-Names="Calibri"
                OnRowDataBound="gvReport_RowDataBound" ShowFooter="true" Width="100%">
                <Columns>
                    <asp:BoundField HeaderText="SL">
                        <HeaderStyle HorizontalAlign="Center" Width="6%" />
                        <ItemStyle HorizontalAlign="Center" Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Bill No">
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Job No">
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Total Amount">
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemStyle HorizontalAlign="Right" Width="5%" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle Font-Size="14px" />
                <HeaderStyle Font-Size="14px" />
                <RowStyle Font-Size="12px" />
            </asp:GridView>
        </div>
        <table style="width: 100%">
            <tr>
                <td style="width: 2%; text-align: left">
                </td>
                <td style="width: 15%">
                </td>
                <td style="width: 1%">
                </td>
                <td style="width: 74%">
                </td>
            </tr>
            </table>
            <table>
            <tr>
                <td style="width: 2%; text-align: left">
                </td>
                <td style="width: 10%; text-align: left; font-family: Calibri; font-size: 14px;
                    font-weight: bold">
                    In Words&nbsp;
                </td>
                <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                    font-weight: bold">
                    :&nbsp;
                </td>
                <td style="width: 79%">
                    &nbsp;
                    <asp:Label ID="lblInWords" runat="server" Font-Names="Calibri" Font-Size="15px"></asp:Label>
                </td>
            </tr>
         
           
        </table>


        <table>
         <tr>
                <td style="width: 2%; text-align: left">
                </td>
   
                <td style="width: 98%">
                    &nbsp;
                    </td>
            </tr>
         <tr>
                <td style="width: 2%; text-align: left">
                    &nbsp;</td>
   
                <td style="width: 98%">
                    &nbsp;</td>
            </tr>
         <tr>
                <td style="width: 2%; text-align: left; font-family:Calibri;font-size:14px;">
                    &nbsp;</td>
   
                <td style="width: 98%; font-family: Calibri; font-size: 15px;">
                    Your Faithfully</td>
            </tr>
         <tr>
                <td style="width: 2%; text-align: left">
                    &nbsp;</td>
   
                <td style="width: 98% ;  font-family:Calibri;font-size:14px;">
                    For&nbsp;
                    <asp:Label ID="lblcomp" runat="server" Font-Names="Calibri" Font-Size="15px"></asp:Label>
                </td>
            </tr>
         <tr>
                <td style="width: 2%; text-align: left">
                    &nbsp;</td>
   
                <td style="width: 98%">
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>