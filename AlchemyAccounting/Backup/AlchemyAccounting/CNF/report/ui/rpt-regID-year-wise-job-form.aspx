<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rpt-regID-year-wise-job-form.aspx.cs" Inherits="AlchemyAccounting.CNF.report.ui.rpt_regID_year_wise_job_form" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="shortcut icon" href="../../../Images/favicon.ico" />
    <script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <style type="text/css">
        #header
        {
            float: left;
            width: 100%;
            background-color: transparent;
            height: 50px;
        }
        #header h1
        {
            font-family: Century Gothic;
            font-weight: bold;
        }
        #entry
        {
            float: left;
            width: 100%;
            background-color: transparent;
            border: 1px solid #000;
            margin-top: 10px;
            margin-bottom: 30px;
            border-radius: 10px;
            text-align: left;
        }
        .Gridview
        {
            font-family: Verdana;
            font-size: 10pt;
            font-weight: normal;
            color: black;
            margin-right: 0px;
            text-align: left;
        }
        .txtColor:focus
        {
            border: solid 3px green !important;
        }
        .txtColor
        {
            margin-left: 0px;
            text-align: left;
        }
        .def
        {
            float: left;
            width: 100%;
        }
        #toolbar
        {
            float: left;
            width: 100%;
            background-color: #cccccc;
            border-radius: 10px 10px 0px 0px;
        }
        .ui-accordion
        {
            text-align: left;
        }
        .txtalign
        {
            text-align: center;
        }
        .passport
        {
            float: left;
            width: 100%;
            height: 250px;
        }
        .sign
        {
            float: left;
            width: 100%;
            height: 150px;
            margin-top: 10%;
        }
        .autocomplete_completionListElement_grid
        {
            width: 250px !important;
            background-color: inherit;
            color: windowtext;
            border: buttonshadow;
            height: 200px;
            text-align: left;
            overflow: scroll;
            background: #fff;
            border: 1px solid #ccc;
            list-style-type: none;
        }
        
        .autocomplete_listItem_grid
        {
        }
        .autocomplete_highlightedListItem_grid
        {
            background: #000;
            color: Orange;
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
        
        .well
        {
            background-color: white;
        }
        
        /*Calendar Control CSS*/
        .cal_Theme1 .ajax__calendar_other .ajax__calendar_day, .cal_Theme1 .ajax__calendar_other .ajax__calendar_year
        {
            color: White; /*Your background color of calender control*/
        }
    </style>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }

            return true;
        }
               
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center">
            Job Register - Station Wise</h1>
    </div>
    <div id="entry">
        <div id="toolbar">
            <table style="width: 100%; border: 1px solid #000;">
                <tr>
                    <td style="text-align: right; width: 50%">
                        <%--<asp:Button runat="server" ID="btnUpdate" CssClass="txtColor" Font-Bold="True" Font-Names="Calibri"
                            Style="text-align: center" Font-Size="15px" TabIndex="9" Text="Edit" Width="80px"
                            OnClick="btnUpdate_Click" Height="30px" />
                        <asp:Button runat="server" ID="btnCancel" CssClass="txtColor" Font-Bold="True" Font-Names="Calibri"
                            Style="text-align: center" Font-Size="15px" Text="Cancel" Width="80px" OnClick="btnCancel_Click"
                            Visible="false" Height="30px" />--%>
                    </td>
                    <td style="width: 50%">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="def">
        <table width="100%">
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    Reg ID
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="width: 84%; margin-left: 120px;">
                    <asp:TextBox runat="server" ID="txtRegID" CssClass="txtColor center" 
                        Width="30%" Height="22px"
                        TabIndex="1" OnTextChanged="txtRegID_TextChanged" AutoPostBack="True"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" Enabled="True" ServicePath=""
                        CompletionListCssClass="completionList" TargetControlID="txtRegID" MinimumPrefixLength="1"
                        CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                        ServiceMethod="GetCompletionListRegID" runat="server">
                    </asp:AutoCompleteExtender>
                    &nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    &nbsp;</td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    &nbsp;</td>
                &nbsp;
                <td style="width: 84%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    Job Year
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="width: 84%">
                    <asp:TextBox runat="server" ID="txtJobYY" CssClass="txtColor center" 
                        Width="30%" TabIndex="3"
                        AutoPostBack="True"  Height="22px" ontextchanged="txtJobYY_TextChanged"></asp:TextBox>
                   <asp:AutoCompleteExtender ID="AutoCompleteExtender3" Enabled="True" ServicePath=""
                        TargetControlID="txtJobYY" MinimumPrefixLength="1" CompletionInterval="10"
                        EnableCaching="true" CompletionSetCount="3" UseContextKey="True" ServiceMethod="GetCompletionListYear"
                        runat="server">
                    </asp:AutoCompleteExtender>
                    &nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    &nbsp;
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    &nbsp;
                </td>
                <td style="width: 84%">
                    <asp:Button ID="btnSubmit" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px" TabIndex="5"
                        Text="View Report" Width="110px" OnClick="btnSubmit_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblErrmsg" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>