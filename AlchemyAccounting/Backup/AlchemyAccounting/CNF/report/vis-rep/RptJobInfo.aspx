﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptJobInfo.aspx.cs" Inherits="AlchemyAccounting.CNF.report.vis_rep.RptJobInfo" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%">
            <tr>
                <td style="width: 10%" rowspan="3">
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
        </table>
        <table style="width: 100%">
            <tr>
                <td style="width: 2%; text-align: left">
                </td>
                <td style="width: 48%; text-align: left; font-size: 19PX; font-family: Calibri">
                    Job Register
                </td>
                <td style="width: 40%; text-align: right; font-size: 14px; font-family: Calibri">
                    Print date :
                    <asp:Label ID="lblPrintDate" runat="server" Font-Names="Calibri"></asp:Label>
                    &nbsp;
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td style="width: 2%; text-align: left">
                </td>
                <td style="width: 13%; text-align: left; font-family: Calibri; font-size: 14px;">
                    Job Year
                </td>
                <td style="width: 1%; text-align: center">
                    :
                </td>
                <td style="width: 18%;">
                    <asp:Label runat="server" ID="lblJobyr"></asp:Label>
                </td>
                <td style="width: 33%; text-align: left; font-size: 14px; font-family: Calibri;">
                    Job Type : &nbsp;
                    <asp:Label runat="server" ID="lblJobType"></asp:Label>
                </td>
                <td style="width: 33%; text-align: left; font-size: 14px; font-family: Calibri;">
                    &nbsp; &nbsp;
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 2%; text-align: left">
                </td>
                <td style="width: 13%; text-align: left; font-family: Calibri; font-size: 14px;">
                    &nbsp;Branch&nbsp;&nbsp;&nbsp;
                </td>
                <td style="width: 1%; text-align: center">
                    :
                </td>
                <td style="width: 43%">
                    <asp:Label runat="server" ID="lblBranch" Font-Names="Calibri;font-size:14px;"></asp:Label>
                </td>
                <td style="width: 15%; text-align: right; font-family: Calibri; font-size: 14px;">
                    &nbsp;
                </td>
                <td style="width: 27%; font-size: 14px; font-family: Calibri">
                    &nbsp;
                </td>
            </tr>
        </table>
        <div style="width: 96%; margin: 1% 2% 0% 2%;">
            <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" Font-Names="Calibri"
                OnRowDataBound="gvReport_RowDataBound" Width="100%">
                <Columns>
                    <asp:BoundField HeaderText="Job No" DataField="JOBNO">
                        <HeaderStyle HorizontalAlign="Center" Width="6%" />
                        <ItemStyle HorizontalAlign="Center" Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="REGID" DataField="REGID">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Party" DataField="ACCOUNTNM">
                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Goods" DataField="GOODS">
                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Packages" DataField="PKGS">
                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PERMITNO" HeaderText="Permit No">
                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DOCINVNO" HeaderText="Invoice No">
                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
                    <ItemStyle HorizontalAlign="Center" Width="15%" />
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
                <td style="width: 5%">
                </td>
                <td style="width: 1%">
                </td>
                <td style="width: 84%">
                </td>
            </tr>
            <tr>
                <td style="width: 2%; text-align: left">
                </td>
                <td style="width: 5%; text-align: right; font-family: Calibri; font-size: 14px; font-weight: bold">
                    &nbsp;
                </td>
                <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                    font-weight: bold">
                    &nbsp;
                </td>
                <td style="width: 84%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 2%; text-align: left">
                </td>
                <td style="width: 5%; text-align: left; font-family: Calibri; font-size: 14px; font-weight: bold">
                </td>
                <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                    font-weight: bold">
                </td>
                <td style="width: 84%">
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
