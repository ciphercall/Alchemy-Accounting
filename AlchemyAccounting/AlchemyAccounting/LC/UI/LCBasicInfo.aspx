<%@ Page Title="L/C Basic Information" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="LCBasicInfo.aspx.cs" Inherits="AlchemyAccounting.LC.UI.LCBasicInfo" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>

    <script type ="text/javascript">
        $(document).ready(function () {
            $("#txtLCDT,#txtSCPIDT,#txtMcDT,#txtMPIDT").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+0" });
            var dlg = $("#dialog").dialog({
                autoOpen: false,
                modal: false,
                title: "",
                position: ["center", "center"],
                resizable: false
            });
            dlg.parent().appendTo(jQuery("form:first"));
        });
        function shai_dailog() {
            $("#dialog").dialog("open");
        }

        document.onkeypress = function (e) {
            if (e.keyCode == 13) {
                shai_dailog();
            }
        }

        //------------- show iframe dialog --------------------
        function showInFrameDialog(ptr, page, newTitle) {
            var width = $(window).width();
            var height = $(window).height();
            var frame = $('<iframe/>');
            frame = $(frame).attr('width', (width - 60));
            frame = $(frame).attr('height', (height - 110));
            frame = $(frame).attr('src', page);

            var container = $('<div id="frameDialog"></div>');
            container = container.append(frame);
            container.dialog({
                autoOpen: true,
                modal: true,
                title: newTitle,
                width: (width - 100),
                height: (height - 50),
                onEscapeClose: false,
                resizable: true,
                dragable: true
            });


            return false;
        }


        //------------- show iframe dialog --------------------
        function showPageInIFrame(page) {
            //    var width = $(window).width();
            //    var height = $(window).height();
            var frame = $('<iframe/>');
            frame = $(frame).attr('width', 300);
            frame = $(frame).attr('height', 300);
            frame = $(frame).attr('src', page);

            var container = $('<div id="frameDialog"></div>');
            container = container.append(frame);

            container.dialog({
                autoOpen: true,
                modal: false,
                title: newTitle,
                width: (width - 100),
                height: (height - 50),
                onEscapeClose: false,
                resizable: true,
                dragable: true
            });
            return false;
        }

    </script>

    <style type ="text/css">
        #header
        {
            float: left;
            width:100%;
            background-color: transparent;
            height: 50px;
        }
        #entry
        {
            float: left;
            width: 100%;
            background-color: transparent;
            border: 1px solid #000;
            border-radius: 0px 0px 10px 10px;
            margin-bottom: 30px;
        }
        #btnEdit
        {
            border-style: solid;
            border-color: inherit;
            border-width: 1px;
            float:right;
            width:40px;
            height:19px;
            padding-bottom: 2px;
            background: #f2f2f2;
            border-radius: 5px;
            text-align: left;
        }
        #toolbar
        {
            float:left;
            width:100%;
            background-color: #cccccc;
            border-radius: 10px 10px 0px 0px;
            border: 1px solid #000;
            margin-top:10px;
        }
        #grid
        {
            float:left;
        }
        #dialog
        {
            float:left;
            width:30%;
        }
        .style4
        {
            text-align: center;
        }
        .style5
        {
            width: 134px;
        }
        .style7
        {
            width: 420px;
        }
        .style8
        {
            width: 276px;
        }
        .style9
        {
            width: 270px;
        }
        .style10
    {
        width: 3px;
    }
    .style11
    {
        width: 85px;
    }
    .style12
    {
        width: 82px;
    }
    .style13
    {
            width: 185px;
        }
    .style14
    {
        width: 185px;
        text-align: right;
    }
    .style15
    {
        width: 1px;
    }
    .style16
    {
        width: 401px;
    }
    .style17
    {
        width: 185px;
        text-align: right;
            font-weight: 700;
        }
    .style18
    {
        width: 1px;
        font-weight: bold;
    }
    
            .txtColor:focus
        {
            border:solid 1px green !important;
        }
        .txtColor
        {
            margin-left: 0px;
            text-align: left;
        }
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
   </asp:ScriptManager>
       
        <div id="header">
        <h1 align="center" style="font-weight: bold;">L/C Basic INformation</h1>
    </div>

    <div id="toolbar">
        
        <table style="width:100%;">
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style8">
                    &nbsp;</td>
                <td class="style9">
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <%--<div id= "btnEdit" class="style19">
                    
                <a onclick="return showInFrameDialog(this,'EditLCBasicInfo.aspx','Edit L/C Basic Info');" 
                        href="" 
                        style= "font-family:Helvetica Neue,Lucida Grande,Segoe UI,Arial,Helvetica,Verdana,sans-serif; font-size:1.2em; font-weight:bold; font-style:italic; text-decoration: none; color:#000000; text-align: center;">Edit</a></div>--%>
                    <asp:Button ID="btnLCEdit" runat="server" onclick="btnLCEdit_Click" 
                        style="font-weight: 700" Text="Edit" Width="80px" />
                </td>
                <td class="style8">
                    <asp:Button ID="btnRefresh" runat="server" Font-Bold="True" Font-Italic="False" 
                        Text="Refresh" onclick="btnRefresh_Click" />
                </td>
                <td class="style9">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                        <asp:Label ID="lblLcID" runat="server" Visible="False"></asp:Label>
                    </td>
                <td class="style8">
                    &nbsp;</td>
                <td class="style9">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        
    </div>
    <div id = "entry">
        
        <div>
            
            <table style="width:100%;">
                <tr>
                    <td class="style13" style="text-align: right">
                        <strong>L/C Type</strong></td>
                    <td class="style10">
                        <strong>:</strong></td>
                    <td class="style11">
                        <asp:DropDownList ID="ddlLCType" runat="server" Width="100px" TabIndex="1" 
                            AutoPostBack="True" 
                            onselectedindexchanged="ddlLCType_SelectedIndexChanged" CssClass="txtColor">
                            <asp:ListItem>IMPORT</asp:ListItem>
                            <asp:ListItem>LOCAL</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style12" style="text-align: right">
                        <b>L/C ID</b></td>
                    <td class="style10">
                        <b>:</b></td>
                    <td>
                        <asp:TextBox ID="txtLCID" runat="server" Width="100px" ReadOnly="True" 
                            TabIndex="100" CssClass="txtColor"></asp:TextBox>
                        <asp:DropDownList ID="ddlLcID" runat="server" AutoPostBack="True" 
                            Visible="False" Width="105px" 
                            onselectedindexchanged="ddlLcID_SelectedIndexChanged" TabIndex="2" 
                            CssClass="txtColor">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style13" style="text-align: right">
                        <strong>L/C No</strong></td>
                    <td class="style10">
                        <strong>:</strong></td>
                    <td class="style11">
                        <asp:TextBox ID="txtLCNo" runat="server" Width="200px" TabIndex="2" 
                            CssClass="txtColor"></asp:TextBox>
                        <br />
                        <asp:TextBox ID="txtLcEdit" runat="server" CssClass="txtColor" TabIndex="2" 
                            Width="200px" AutoPostBack="True" ontextchanged="txtLcEdit_TextChanged" 
                            Visible="False"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txtLcEdit_AutoCompleteExtender" runat="server" 
                        CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" 
                        Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListLcName" 
                        ServicePath="" TargetControlID="txtLcEdit" UseContextKey="True">
                    </asp:AutoCompleteExtender>
                    </td>
                    <td class="style12" style="text-align: right">
                        <strong>L/C Date</strong></td>
                    <td class="style10">
                        <strong>:</strong></td>
                    <td>
                        <asp:TextBox ID="txtLCDT" runat="server" Width="100px" TabIndex="3" 
                            AutoPostBack="True" ClientIDMode="Static" CssClass="txtColor" 
                            ontextchanged="txtLCDT_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                </table>
            <table style="width:100%;">
                <tr>
                    <td class="style14">
                        <strong>Bank Name</strong></td>
                    <td class="style15">
                        <strong>:</strong></td>
                    <td class="style16">
                        <asp:TextBox ID="txtBankNM" runat="server" Width="540px" TabIndex="5" 
                            CssClass="txtColor" AutoPostBack="True" 
                            ontextchanged="txtBankNM_TextChanged"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txtBankNM_AutoCompleteExtender" runat="server" 
                            DelimiterCharacters="" Enabled="True" ServicePath="" TargetControlID="txtBankNM"
                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                            UseContextKey="True" ServiceMethod="GetCompletionList">
                        </asp:AutoCompleteExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBankCD" runat="server" Width="150px" ReadOnly="True" 
                            TabIndex="101" CssClass="txtColor"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width:100%;">
                <tr>
                    <td class="style17">
                        <strong>Importer Name</strong></td>
                    <td class="style18">
                        :</td>
                    <td>
                        <asp:TextBox ID="txtImportrNM" runat="server" Width="700px" TabIndex="6" 
                            CssClass="txtColor" AutoPostBack="True" 
                            ontextchanged="txtImportrNM_TextChanged"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txtImportrNM_AutoCompleteExtender" runat="server" 
                            DelimiterCharacters="" Enabled="True" ServicePath="" TargetControlID="txtImportrNM"
                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                            UseContextKey="True" ServiceMethod="GetCompletionListImporter">
                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style17">
                        <strong>Beneficiary</strong></td>
                    <td class="style18">
                        :</td>
                    <td>
                        <asp:TextBox ID="txtBeneficiary" runat="server" Width="700px" TabIndex="7" 
                            CssClass="txtColor" AutoPostBack="True" 
                            ontextchanged="txtBeneficiary_TextChanged"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txtBeneficiary_AutoCompleteExtender" runat="server"
                         DelimiterCharacters="" Enabled="True" ServicePath="" TargetControlID="txtBeneficiary"
                         MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                         UseContextKey="True" ServiceMethod="GetCompletionListBeneficiary">
                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style17">
                        SCPI No</td>
                    <td class="style18">
                        :</td>
                    <td>
                        <asp:TextBox ID="txtSCPINo" runat="server" Width="200px" TabIndex="8" 
                            CssClass="txtColor"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style17">
                        SCPI Date</td>
                    <td class="style18">
                        :</td>
                    <td>
                        <asp:TextBox ID="txtSCPIDT" runat="server" Width="100px" TabIndex="9" 
                            AutoPostBack="True" ClientIDMode="Static" CssClass="txtColor" 
                            ontextchanged="txtSCPIDT_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style17">
                        <strong>MC Name</strong></td>
                    <td class="style18">
                        :</td>
                    <td>
                        <asp:TextBox ID="txtMcNM" runat="server" Width="700px" TabIndex="10" 
                            CssClass="txtColor"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style17">
                        MC No</td>
                    <td class="style18">
                        :</td>
                    <td>
                        <asp:TextBox ID="txtMcNo" runat="server" Width="200px" TabIndex="11" 
                            CssClass="txtColor"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style17">
                        MC Date</td>
                    <td class="style18">
                        :</td>
                    <td>
                        <asp:TextBox ID="txtMcDT" runat="server" Width="100px" TabIndex="12" 
                            AutoPostBack="True" ClientIDMode="Static" CssClass="txtColor" 
                            ontextchanged="txtMcDT_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style17">
                        MPI No</td>
                    <td class="style18">
                        :</td>
                    <td>
                        <asp:TextBox ID="txtMPINo" runat="server" Width="200px" TabIndex="13" 
                            CssClass="txtColor"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style17">
                        MPI Date</td>
                    <td class="style18">
                        :</td>
                    <td>
                        <asp:TextBox ID="txtMPIDT" runat="server" Width="100px" TabIndex="14" 
                            AutoPostBack="True" ClientIDMode="Static" CssClass="txtColor" 
                            ontextchanged="txtMPIDT_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style17">
                        L/C USD</td>
                    <td class="style18">
                        :</td>
                    <td>
                        <asp:TextBox ID="txtLcUSD" runat="server" Width="200px" 
                            style="text-align: right" TabIndex="15" CssClass="txtColor" 
                            AutoPostBack="True" ontextchanged="txtLcUSD_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style17">
                        L/C Exchange Rate</td>
                    <td class="style18">
                        :</td>
                    <td>
                        <asp:TextBox ID="txtLCExRT" runat="server" Width="200px" 
                            style="text-align: right" TabIndex="16" CssClass="txtColor" 
                            AutoPostBack="True" ontextchanged="txtLCExRT_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style17">
                        L/C BDT</td>
                    <td class="style18">
                        :</td>
                    <td>
                        <asp:TextBox ID="txtLCBDT" runat="server" Width="200px" 
                            style="text-align: right" TabIndex="17" CssClass="txtColor" 
                            ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style17">
                        <strong>Remarks</strong></td>
                    <td class="style18">
                        :</td>
                    <td>
                        <asp:TextBox ID="txtRemarks" runat="server" Width="700px" TabIndex="18" 
                            CssClass="txtColor"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style17">
                        Status</td>
                    <td class="style18">
                        :</td>
                    <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="19" Width="200px">
                        <asp:ListItem Value="A">ACTIVE</asp:ListItem>
                        <asp:ListItem Value="I">INACTIVE</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style17">
                        &nbsp;</td>
                    <td class="style18">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            
        </div>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="dialog">
            
            <table style="width:100%;">
                <tr>
                    <td class="style4" colspan="2">
                        <strong>Are you Sure to</strong></td>
                </tr>
                <tr>
                    <td style="text-align: right" class="style5">
                        <asp:Button ID="btnSave" runat="server" Font-Bold="True" Font-Italic="True" 
                            TabIndex="19" Text="Save" Width="100px" onclick="btnSave_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnSavePrint" runat="server" Font-Bold="True" 
                            Font-Italic="True" Text="Save &amp; Print" TabIndex="20" />
                    </td>
                </tr>
                <tr>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            
        </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

    
</asp:Content>
