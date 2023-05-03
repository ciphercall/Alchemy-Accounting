<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PartyInformation.aspx.cs" Inherits="AlchemyAccounting.CNF.Party.PartyInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="shortcut icon" href="../../Images/favicon.ico" />
    <link href="../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="../../Scripts/jquery.blockUI.js"></script>--%>
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
            height: 22px;
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
        .style1
        {
            width: 60%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center">
            Party Information Entry</h1>
    </div>
    <div id="entry">
        <div id="toolbar">
            <table style="width: 100%; border: 1px solid #000;">
                <tr>
                    <td style="text-align: right; width: 50%">
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
        <table style="width: 100%">
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    Party Name
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="text-align: left; font-weight: bold" class="style1">
                    <asp:TextBox ID="txtPartynm" runat="server" AutoPostBack="True" TabIndex="1" Width="90%"
                        CssClass="txtColor" OnTextChanged="txtParty_TextChanged"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="" TargetControlID="txtPartynm" MinimumPrefixLength="1"
                        CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                        ServiceMethod="GetCompletionListParty">
                    </asp:AutoCompleteExtender>
                    
                    <br />
                   
                     <asp:Label ID="lblerrmsg" runat="server" BackColor="Black" Visible="false"
                        ForeColor="#FF3300" ></asp:Label>
                 
                </td>
         
                <td style="width: 30%; text-align: left; font-weight: bold">
                    <asp:TextBox ID="txtPartyID" runat="server" AutoPostBack="True" Width="100%" ReadOnly="true" Visible="false"
                       CssClass="txtColor" Style="text-align: center"  OnTextChanged="txtPartyID_TextChanged"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    Address
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="text-align: left; font-weight: bold" class="style1">
                    <asp:TextBox ID="txtaddress" runat="server" TabIndex="2" Width="90%" CssClass="txtColor"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    Contact No
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="text-align: left; font-weight: bold" class="style1">
                    <asp:TextBox ID="txtcontact" runat="server" TabIndex="3" Width="90%" CssClass="txtColor"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    Email ID
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="text-align: left; font-weight: bold" class="style1">
                    <asp:TextBox ID="txtEmail" runat="server" TabIndex="4" Width="90%" CssClass="txtColor"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    Web Address
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="text-align: left; font-weight: bold" class="style1">
                    <asp:TextBox ID="txtwebadd" runat="server" TabIndex="5" Width="90%" CssClass="txtColor"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    A.P. Name
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="text-align: left; font-weight: bold" class="style1">
                    <asp:TextBox ID="txtAPname" runat="server" TabIndex="6" Width="90%" CssClass="txtColor"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    A.P. Contact No
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="text-align: left; font-weight: bold" class="style1">
                    <asp:TextBox ID="txtapcontact" runat="server" TabIndex="7" Width="90%" CssClass="txtColor"></asp:TextBox>
                </td>
                <td style="width: 20%; text-align: right; font-weight: bold">
                    <p>
                        A.P. - Authorized Person
                    </p>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    Status
                </td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    :
                </td>
                <td style="text-align: left; font-weight: bold" class="style1">
                    <asp:DropDownList runat="server" ID="ddlstatus" TabIndex="8">
                        <asp:ListItem Value="A">ACTIVE</asp:ListItem>
                        <asp:ListItem Value="L">INACTIVE</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td style="width: 15%; text-align: right; font-weight: bold">
                    &nbsp;</td>
                <td style="width: 1%; text-align: center; font-weight: bold">
                    &nbsp;</td>
                <td style="text-align: left; font-weight: bold" class="style1">
                    <asp:Button ID="btnSave" runat="server" Font-Bold="True" Font-Names="Calibri" 
                        Font-Size="15px" CssClass="txtColor txtalign" TabIndex="9"
                        Text="Save" Width="80px" onclick="btnSave_Click1" Height="30px"  />
                  &nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnEdit" runat="server" Font-Bold="True" Font-Names="Calibri" 
                        Font-Size="15px" CssClass="txtColor txtalign" TabIndex="10"
                        Text="Update" Width="80px" onclick="btnEdit_Click" Height="30px"  />
                </td>
            </tr>
            
        </table>
    </div>
</asp:Content>
