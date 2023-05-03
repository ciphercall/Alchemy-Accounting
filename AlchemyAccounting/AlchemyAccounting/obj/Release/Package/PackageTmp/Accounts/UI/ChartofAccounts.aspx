<%@ Page Title="Chart of Accounts Entry" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ChartofAccounts.aspx.cs" Inherits="AlchemyAccounting.Accounts.UI.ChartofAccounts" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Chart of Accounts</title>
    <link rel="shortcut icon" href="../../Images/favicon.ico" />
    <link href="../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtDate").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+100" });

            var dlg = $("").dialog({
                autoOpen: false,
                modal: false,
                title: "",
                position: ["center", "center"],
                resizable: false
            });
            dlg.parent().appendTo(jQuery("form:first"));
        });

        function shai_dailog() {
            $("").dialog("open");
        }
        function ConfirmationBox(username) {
            var result = confirm('Are you sure you want to delete ' + username + ' Details?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }

        function confMSG() {
            if (confirm("Are you Sure to Delete?")) {
                //                alert("Clicked Yes");
            }
            else {
                //                alert("Clicked No");
                return false;
            }

        }

    </script>
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
            border-radius: 10px;
            margin-top: 10px;
            margin-bottom: 30px;
        }
        #toolbar
        {
            float: left;
            width: 100%;
            height: 30px;
            background-color: #cccccc;
            border-radius: 10px 10px 0px 0px;
        }
        #head_row
        {
            float: left;
            width: 100%;
            margin-top: 10px;
        }
        #grid
        {
            float: left;
            width: 90%;
            margin: 0% 5% 0% 5%;
        }
        .style1
        {
            width: 40px;
        }
        .style2
        {
            width: 45px;
        }
        .style3
        {
            width: 341px;
            text-align: right;
        }
        .Gridview
        {
            font-family: Verdana;
            font-size: 10pt;
            font-weight: normal;
            color: black;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center">
            Chart of Accounts Entry</h1>
    </div>
    <div id="entry">
        <div id="toolbar">
            <table style="width: 100%;">
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
                    <td class="style12">
                        &nbsp;
                    </td>
                    <td class="style13">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div id="head_row">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 20%; text-align: right">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        &nbsp;<asp:Label ID="lblIncrLevel" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblSelLvlCD" runat="server"></asp:Label>
                    </td>
                    <td style="width: 10%; text-align: right">
                        <asp:DropDownList ID="ddlLevelID" runat="server" OnSelectedIndexChanged="ddlLevelID_SelectedIndexChanged"
                            AutoPostBack="True" TabIndex="1" CssClass="txtColor">
                            <asp:ListItem>SELECT</asp:ListItem>
                            <asp:ListItem Value="1">ASSET</asp:ListItem>
                            <asp:ListItem Value="2">LIABILITY</asp:ListItem>
                            <asp:ListItem Value="3">INCOME</asp:ListItem>
                            <asp:ListItem Value="4">EXPENDETURE</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtHdName" runat="server" Width="60%" OnTextChanged="txtHdName_TextChanged"
                            AutoPostBack="True" ClientIDMode="Static" TabIndex="2" CssClass="txtColor"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtHdName"
                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                            UseContextKey="True" ServiceMethod="GetCompletionList">
                        </asp:AutoCompleteExtender>
                        <asp:TextBox ID="txtLiabilty" runat="server" AutoPostBack="True" ClientIDMode="Static"
                            Width="60%" OnTextChanged="txtLiabilty_TextChanged" TabIndex="3" CssClass="txtColor"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtLiabilty"
                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                            UseContextKey="True" ServiceMethod="GetCompletionListL">
                        </asp:AutoCompleteExtender>
                        <asp:TextBox ID="txtIncome" runat="server" AutoPostBack="True" ClientIDMode="Static"
                            Width="60%" OnTextChanged="txtIncome_TextChanged" TabIndex="4" CssClass="txtColor"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtIncome"
                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                            UseContextKey="True" ServiceMethod="GetCompletionListI">
                        </asp:AutoCompleteExtender>
                        <asp:TextBox ID="txtExpen" runat="server" AutoPostBack="True" ClientIDMode="Static"
                            Width="60%" OnTextChanged="txtExpen_TextChanged" TabIndex="5" CssClass="txtColor"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" TargetControlID="txtExpen"
                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                            UseContextKey="True" ServiceMethod="GetCompletionListE">
                        </asp:AutoCompleteExtender>
                        <asp:TextBox ID="txtCode" runat="server" Width="30%" ReadOnly="True" TabIndex="6"
                            Visible="False"></asp:TextBox>
                        <strong>Level Code:</strong>&nbsp;
                        <asp:Label ID="lblLvlID" runat="server" Text="LevelCode"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:Button ID="btnSubmit" runat="server" Font-Bold="True" Font-Italic="True" Text="SUBMIT"
                            OnClick="btnSubmit_Click" TabIndex="7" CssClass="txtColor" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: center">
                        <strong>Sub Level Code :</strong>
                        <asp:Label ID="lblBotCode" runat="server"></asp:Label>
                    </td>
                    <td style="width: 10%; text-align: right">
                        <asp:Label ID="lblMxAccCode" runat="server" Text="MaxAccCode"></asp:Label>
                    </td>
                    <td style="width: 50%">
                        <asp:Label ID="lblNewLvlCD" runat="server"></asp:Label>
                        <asp:Label ID="lblAccTP" runat="server"></asp:Label>
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        <asp:Label ID="lblBRCD" runat="server"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div id="grid">
            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" CssClass="Gridview"
                HeaderStyle-BackColor="#61A6F8" ShowFooter="True" HeaderStyle-Font-Bold="true"
                HeaderStyle-ForeColor="White" OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                OnRowDeleting="gvDetails_RowDeleting" OnRowEditing="gvDetails_RowEditing" OnRowUpdating="gvDetails_RowUpdating"
                OnRowCommand="gvDetails_RowCommand" OnRowDataBound="gvDetails_RowDataBound" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="Account Head">
                        <ItemTemplate>
                            <asp:Label ID="lblAccHdNM" runat="server" Text='<%# Eval("ACCOUNTNM") %>' Width="98%" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAccHead" runat="server" Text='<%#Eval("ACCOUNTNM") %>' TabIndex="20"
                                CssClass="txtColor" Width="98%"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAccHead" runat="server" Width="98%" TabIndex="8" CssClass="txtColor" />
                            <asp:RequiredFieldValidator ID="accHd" runat="server" ControlToValidate="txtAccHead"
                                Text="Account Head" ValidationGroup="validaiton" Font-Bold="True" Font-Italic="True"
                                ForeColor="Red" />
                        </FooterTemplate>
                        <HeaderStyle Width="55%" />
                        <ItemStyle Width="55%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Account Code">
                        <ItemTemplate>
                            <asp:Label ID="lblAcountCode" runat="server" Text='<%#Eval("ACCOUNTCD") %>' Width="100%" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAccCode" runat="server" Text='<%#Eval("ACCOUNTCD") %>' ReadOnly="True"
                                Width="100%" />
                        </EditItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Control Code">
                        <ItemTemplate>
                            <asp:Label ID="lblControlCode" runat="server" Text='<%#Eval("CONTROLCD") %>' Width="100%"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtContolCode" runat="server" Text='<%#Eval("CONTROLCD") %>' ReadOnly="True"
                                Width="100%" />
                        </EditItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Access">
                        <ItemTemplate>
                            <asp:Label ID="lblAccess" runat="server" Text='<%#Eval("CATNM") %>' Width="100%" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlAccessEdit" runat="server" Width="98%" CssClass="txtColor" TabIndex="21"
                                AutoPostBack="True" onselectedindexchanged="ddlAccessEdit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlAccess" runat="server" Width="98%" AutoPostBack="True" CssClass="txtColor" TabIndex="9"
                                onselectedindexchanged="ddlAccess_SelectedIndexChanged">
                            </asp:DropDownList>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AccessCD" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAccessCD" runat="server" Text='<%#Eval("BRANCHCD") %>' Width="100%" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblAccessCDEdit" runat="server" Width="98%">
                            </asp:Label>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblAccessCDFot" runat="server" Width="98%">
                            </asp:Label>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" CssClass="txtColor"
                                ImageUrl="~/Images/update.jpg" ToolTip="Update" Height="20px" Width="20px" TabIndex="22" />
                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" CssClass="txtColor"
                                ImageUrl="~/Images/Cancel.jpg" ToolTip="Cancel" Height="20px" Width="20px" TabIndex="23" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" CssClass="txtColor"
                                ImageUrl="~/Images/Edit.jpg" ToolTip="Edit" Height="20px" Width="20px" TabIndex="30" />
                            <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server"
                                CssClass="txtColor" ImageUrl="~/Images/delete.jpg" ToolTip="Delete" Height="20px"
                                Width="20px" OnClientClick="return confMSG()" TabIndex="31" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="imgbtnAdd" runat="server" CssClass="txtColor" ImageUrl="~/Images/AddNewitem.jpg"
                                CommandName="AddNew" Width="20px" Height="20px" ToolTip="Add new Record" ValidationGroup="validaiton"
                                TabIndex="10" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White"></HeaderStyle>
            </asp:GridView>
        </div>
        <asp:Label ID="lblresult" runat="server"></asp:Label>
    </div>
</asp:Content>
