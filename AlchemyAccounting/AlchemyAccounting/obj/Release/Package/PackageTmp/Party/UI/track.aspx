<%@ Page Title="" Language="C#" MasterPageFile="~/Partymst.Master" AutoEventWireup="true" CodeBehind="track.aspx.cs" Inherits="AlchemyAccounting.Party.UI.track" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.7.123, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>

    <style>
        .completionList {
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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="padding-top: 20px;">
        <div class="row">
            <div class="col-md-12">
                <div class="jumbotron" style="box-shadow: 0 5px 5px 5px #888888;">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel runat="server" ID="update1">
                        <ContentTemplate>



                            <div class="text-center">
                                Hi,
                                <asp:Label runat="server" ID="lblPartyNM" CssClass="text-info" ForeColor="#CA5100"></asp:Label>
                            </div>
                            <asp:Label runat="server" ID="lblpartyid" Visible="False"></asp:Label>
                            <div>


                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:DropDownList runat="server" CssClass="dropdown form-control" TabIndex="1" AutoPostBack="True" ID="ddltrackselect" OnSelectedIndexChanged="ddltrackselect_SelectedIndexChanged">
                                            <asp:ListItem>Invoice Number</asp:ListItem>
                                            <asp:ListItem>Permit Number</asp:ListItem>
                                            <asp:ListItem>House B/L</asp:ListItem>
                                            <asp:ListItem>House AWB Number</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="txtjob" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtjob_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" Enabled="True" ServicePath="" CompletionListCssClass="completionList"
                                            TargetControlID="txtjob" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true"
                                            CompletionSetCount="3" UseContextKey="True" ServiceMethod="GetCompletionListJob_No_Year_Type"
                                            runat="server" CompletionListItemCssClass="listItem">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-4">
                                        <table class="table">
                                            <tr>
                                                <td>Type</td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtjobtp" CssClass="form-control" ReadOnly="True"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-md-4">
                                        <table class="table">
                                            <tr>
                                                <td>Year</td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtjobyear" CssClass="form-control" ReadOnly="True"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-md-4">
                                        <table class="table">
                                            <tr>
                                                <td>Job NO</td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtjobno" CssClass="form-control" ReadOnly="True"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-md-6">
                                        <table class="table">
                                            <tr>
                                                <td>B/E No</td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtDate" CssClass="form-control" ReadOnly="True" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtbill" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>House B/L No</td>
                                                <td>
                                                    <asp:TextBox ID="txthousebill" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Packages</td>
                                                <td>
                                                    <asp:TextBox ID="txtCount" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>L/C No</td>
                                                <td>
                                                    <asp:TextBox ID="txtLcNo" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-md-6">
                                        <table class="table">
                                            <tr>
                                                <td>Invoice No</td>
                                                <td>
                                                    <asp:TextBox ID="txtInvNo" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>House AWB No</td>
                                                <td>
                                                    <asp:TextBox ID="txtAWBHbill" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Permit No</td>
                                                <td>
                                                    <asp:TextBox ID="txtPermit" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>

                                        </table>
                                    </div>
                                </div>

                                <div>
                                </div>
                            </div>

                            <div>
                                <asp:GridView ID="gvDetails" runat="server" BackColor="White" BorderColor="White"
                                    BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None"
                                    Width="100%" AutoGenerateColumns="False" Style="text-align: left" CssClass="table table-bordered table-hover table-responsive"
                                    OnRowDataBound="gvDetails_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Status Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatusDate" runat="server" Style="text-align: center" Text='<%# Eval("STATSDT") %>'></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblstatusItem" CssClass="txtColor txtalign" Width="100%"
                                                    Text='<%# Eval("STATUS") %>'></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblREMARKSItem" Text='<%# Eval("REMARKS") %>'> </asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                        </asp:TemplateField>

                                    </Columns>

                                    <HeaderStyle BackColor="#9DC565" Font-Bold="True" ForeColor="#ffffff" Font-Names="Calibri"
                                        Font-Size="14px" />
                                    <PagerStyle BackColor="#C6C3C6" ForeColor="#ffffff" HorizontalAlign="Right" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Names="Calibri" Font-Size="12px" />
                                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#594B9C" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#33276A" />
                                </asp:GridView>
                            </div>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
