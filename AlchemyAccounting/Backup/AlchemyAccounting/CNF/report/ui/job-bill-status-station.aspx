<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="job-bill-status-station.aspx.cs" Inherits="AlchemyAccounting.CNF.report.ui.job_bill_status_station" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="shortcut icon" href="../../../Images/favicon.ico" />
    <style type="text/css">
        #header
        {
            float: left;
            width: 100%;
            background-color: transparent;
            height: 50px;
        }
        .style1
        {
            width: 2px;
        }
        .style2
        {
            width: 262px;
        }
        .style3
        {
            width: 170px;
            text-align: right;
            font-weight: 700;
        }
        
        #s_top
        {
            float: left;
            width: 60%;
            margin: 2% 20% 0% 20%;
            border: 1px solid #cccccc;
            border-radius: 10px;
        }
        .txtColor:focus
        {
            border: solid 4px green !important;
        }
        .txtColor
        {
            margin-left: 0px;
            text-align: left;
        }
        .center
        {
            text-align: center;
        }
        .completionList
        {
            width: 400px !important;
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 200px;
            overflow: auto;
            background-color: #FFFFFF;
        }
        
        .listItem
        {
            color: #1C1C1C;
        }
        
        .itemHighlighted
        {
            background-color: orange;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center" style="font-weight: bold;">
            Station Wise Bill Status</h1>
    </div>
    <div>
        <table id="s_top">
            <tr>
                <td class="style3">
                    &nbsp;
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td class="style2">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Office Name
                </td>
                <td class="style1">
                    <strong>:</strong>
                </td>
                <td class="style2">
                    <asp:TextBox runat="server" ID="txtBranchID" CssClass="txtColor" Width="100%" TabIndex="1"
                        OnTextChanged="txtBranchID_TextChanged" AutoPostBack="True"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" Enabled="True" ServicePath=""
                        CompletionListCssClass="completionList" TargetControlID="txtBranchID" MinimumPrefixLength="1"
                        CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                        ServiceMethod="GetCompletionListBranchID" runat="server">
                    </asp:AutoCompleteExtender>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style3">
                        Register ID
                    </td>
                <td class="style1">
                    :
                </td>
                <td class="style2">
                        <asp:DropDownList ID="ddlRegID" runat="server" AutoPostBack="True" CssClass="txtColor"
                            OnSelectedIndexChanged="ddlRegID_SelectedIndexChanged" TabIndex="2" 
                        Width="100%">
                            <asp:ListItem>CHITTAGONG</asp:ListItem>
                            <asp:ListItem>COMILLA</asp:ListItem>
                            <asp:ListItem>BENAPOLE</asp:ListItem>
                            <asp:ListItem>DEPZ</asp:ListItem>
                            <asp:ListItem>ICD</asp:ListItem>
                            <asp:ListItem>AEPZ</asp:ListItem>
                            <asp:ListItem>AIRPORT</asp:ListItem>
                        </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    <b>Job Type</b>
                </td>
                <td class="style1">
                    <strong>:</strong>
                </td>
                <td class="style2">
                    <asp:DropDownList runat="server" ID="ddlJobType" CssClass="txtColor" Width="50%"
                        TabIndex="3" ReadOnly="true" AutoPostBack="True" OnSelectedIndexChanged="ddlJobType_SelectedIndexChanged">
                        <asp:ListItem Value="EXPORT">EXPORT</asp:ListItem>
                        <asp:ListItem Value="IMPORT">IMPORT</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <b>Job Year</b>
                </td>
                <td class="style1">
                    <strong>:</strong>
                </td>
                <td class="style2">
                    <asp:TextBox runat="server" ID="txtJobYear" CssClass="txtColor center" Width="50%"
                        TabIndex="4" AutoPostBack="True" OnTextChanged="txtJobYear_TextChanged"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" Enabled="True" ServicePath=""
                        TargetControlID="txtJobYear" MinimumPrefixLength="1" CompletionInterval="10"
                        EnableCaching="true" CompletionSetCount="3" UseContextKey="True" ServiceMethod="GetCompletionListYear"
                        runat="server">
                    </asp:AutoCompleteExtender>
                </td>
                <td>
                    <asp:Label runat="server" ID="lblCompanyID" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Label ID="lblbranchtag" runat="server" Text="Branch Name" Visible="false"></asp:Label>
                </td>
                <td class="style1">
                    <asp:Label ID="Label1" runat="server" Text=":" Visible="false" Style="font-weight: 700"></asp:Label>
                </td>
                <td class="style2">
                    <asp:Label runat="server" ID="lblbranch" Visible="false"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td style="text-align: right" class="style2">
                    <asp:Button ID="btnView" runat="server" Text="View Report" Font-Bold="True" Font-Italic="False"
                        Width="150px" TabIndex="5" CssClass="txtColor center" OnClick="btnView_Click" />
                </td>
                <td>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td style="text-align: right" class="style2">
                    <asp:Label ID="lblErrmsg" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
