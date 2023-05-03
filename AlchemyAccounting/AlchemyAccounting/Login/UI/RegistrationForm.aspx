<%@ Page Title="User Registration Form" Language="C#" MasterPageFile="~/Web_2.Master" AutoEventWireup="true" CodeBehind="RegistrationForm.aspx.cs" Inherits="AlchemyAccounting.Login.UI.RegistrationForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .style19
    {
        width: 591px;
        text-align: left;
    }
    .style28
    {
        text-align: center;
        height: 20px;
    }
    .style31
    {
        width: 129px;
        text-align: right;
    }
    .style32
    {
        width: 129px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p style="height: 33px">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="style28">
        <h1 style=" font-family: Century Gothic; color: Teal; text-align: center;">User Registration Form</h1>    
    </div>
</p>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div align="center" style="width: 100%; margin-top: 40px;"> 
        <asp:Panel ID="Panel1" runat="server" Height="311px" style="margin-left: 6px" 
            Width="520px">
            <table style="width: 400px; height: 251px; border: 1px solid #000; margin-top: 20px; border-radius: 10px;">
                <tr>
                    <td class="style31">
                        User Name :</td>
                    <td class="menu">
                        <asp:TextBox ID="txtusername" runat="server" ViewStateMode="Enabled" 
                            Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserName" runat="server" 
                            ControlToValidate="txtusername" ErrorMessage="Required." 
                            Font-Bold="False" Font-Italic="True" ForeColor="Red">Required.</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style31">
                        Password :</td>
                    <td class="menu">
                        <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" 
                            ViewStateMode="Enabled" Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="Password" runat="server" 
                            ControlToValidate="txtpassword" ErrorMessage="Required." 
                            Font-Italic="True" ForeColor="Red">Required.</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style31">
                        Confirm Password :</td>
                    <td class="menu">
                        <asp:TextBox ID="txtconpassword" runat="server" TextMode="Password" 
                            ViewStateMode="Enabled" Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="conpass" runat="server" 
                            ControlToValidate="txtconpassword" ErrorMessage="Required" Font-Italic="True" 
                            ForeColor="Red">Required</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style31">
                        Name :</td>
                    <td class="menu">
                        <asp:TextBox ID="txtname" runat="server" ViewStateMode="Enabled" Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="Rqname" runat="server" 
                            ControlToValidate="txtname" ErrorMessage="Required" Font-Italic="True" 
                            ForeColor="Red">Required</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style31">
                        E-mail:&nbsp;
                    </td>
                    <td class="menu">
                        <asp:TextBox ID="txtemail" runat="server" Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RqEmail" runat="server" 
                            ControlToValidate="txtemail" ErrorMessage="Required." Font-Italic="True" 
                            ForeColor="Red">Required.</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style31">
                        &nbsp;</td>
                    <td class="menu">
                        <asp:Button ID="btnRegistration" runat="server" onclick="btnRegistration_Click" 
                            style="height: 26px" Text="Registration" />
                        &nbsp;<asp:Label ID="Label1" runat="server" Font-Italic="True" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style32">
                        &nbsp;</td>
                    <td class="menu">
                        <asp:CompareValidator ID="PasswordCompare" runat="server" 
                            ControlToCompare="txtpassword" ControlToValidate="txtconpassword" 
                            Display="Dynamic" 
                            ErrorMessage="The Password and Confirmation Password must match." 
                            ForeColor="Red" ValidationGroup="Panel1"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style32">
                        &nbsp;</td>
                    <td class="menu">
                        <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="style19" colspan="2" style="color: Red;">
                        <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" 
                            CssClass="style16" Height="33px" ValidationGroup="Panel1" Width="500px" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        </div>
<br />
    </ContentTemplate>
</asp:UpdatePanel>
<p>
    <br />
</p>
</asp:Content>
