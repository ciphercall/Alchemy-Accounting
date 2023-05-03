<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PartyWiseBillSummary.aspx.cs" Inherits="AlchemyAccounting.CNF.report.ui.PartyWiseBillSummary" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.7.123, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="shortcut icon" href="../../../Images/favicon.ico" />
    <link href="../../../css2/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        #header {
            float: left;
            width: 100%;
            background-color: transparent;
            height: 50px;
        }

        .style1 {
            width: 2px;
        }

        .style2 {
            width: 262px;
        }

        .style3 {
            width: 170px;
            text-align: right;
            font-weight: 700;
        }

        #s_top {
            float: left;
            width: 60%;
            margin: 2% 20% 0% 20%;
            border: 1px solid #cccccc;
            border-radius: 10px;
        }

        .txtColor:focus {
            border: solid 4px green !important;
        }

        .txtColor {
            margin-left: 0px;
            text-align: left;
        }

        .center {
            text-align: center;
        }

        .completionList {
            width: 400px !important;
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 200px;
            overflow: auto;
            background-color: #FFFFFF;
        }

        .listItem {
            color: #1C1C1C;
        }

        .itemHighlighted {
            background-color: orange;
        }

        .well {
            background-color: white;
        }

        /*Calendar Control CSS*/
        .cal_Theme1 .ajax__calendar_other .ajax__calendar_day, .cal_Theme1 .ajax__calendar_other .ajax__calendar_year {
            color: White; /*Your background color of calender control*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h3 align="center" style="font-weight: bold;">Party wise Bill Summary</h3>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-6" style="border: 1px solid;">
               <br/>
                <table style="width: 100%">
                    <tr>
                        <td>Office Name</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtBranchIDBill" CssClass="txtColor" Width="100%" TabIndex="1"
                                OnTextChanged="txtBranchIDBill_TextChanged" AutoPostBack="True"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" Enabled="True" ServicePath=""
                                CompletionListCssClass="completionList" TargetControlID="txtBranchIDBill" MinimumPrefixLength="1"
                                CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                                ServiceMethod="GetCompletionListBranchID" runat="server">
                            </asp:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>Job Type</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlJobTypeBill" CssClass="txtColor" Width="100%"
                                TabIndex="3" ReadOnly="true" AutoPostBack="True" OnSelectedIndexChanged="ddlJobTypeBill_SelectedIndexChanged">
                                <asp:ListItem Value="EXPORT">EXPORT</asp:ListItem>
                                <asp:ListItem Value="IMPORT">IMPORT</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Partye Name</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtparty" CssClass="txtColor" Width="100%"  TabIndex="4" OnTextChanged="txtparty_TextChanged"
                                AutoPostBack="True">                       
                            </asp:TextBox>
                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" Enabled="True" ServicePath=""
                                TargetControlID="txtparty" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true"
                                CompletionSetCount="3" UseContextKey="True" ServiceMethod="GetCompletionListParty"
                                runat="server">
                            </asp:AutoCompleteExtender>
                            &nbsp;&nbsp;&nbsp;
                    <asp:TextBox runat="server" ID="txtpatyID" CssClass="txtColor center" Width="30%"
                        Height="22px" TabIndex="99" Enabled="False" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Date From</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDateFromBill" CssClass="txtColor center" Width="100%"
                                TabIndex="5"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" CssClass="cal_Theme1 well"
                                Format="dd/MM/yyyy" TargetControlID="txtDateFromBill">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>Date To</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDateToBill" CssClass="txtColor center" Width="100%"
                                TabIndex="6"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" CssClass="cal_Theme1 well"
                                Format="dd/MM/yyyy" TargetControlID="txtDateToBill">
                            </asp:CalendarExtender>


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblbranchtagBill" runat="server" Text="Branch Name" Visible="false"></asp:Label>
                            <asp:Label ID="lblCompanyIDBill" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="Label1Bill" runat="server" Text=":" Visible="false" Style="font-weight: 700"></asp:Label>

                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblbranchBill" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnViewBill" runat="server" Text="View Details" Font-Bold="True" Font-Italic="False"
                                Width="100%" TabIndex="7" CssClass="txtColor center" OnClick="btnViewBill_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="text-center">
        <asp:Label ID="lblErrmsg" runat="server" Visible="False" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>
