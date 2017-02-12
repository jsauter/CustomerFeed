<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="CustomerFeed.Register" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="navigation" runat="server">
    <div id="navigation">
        <img src="images/nav_welcome.png" class="nav" id="nav_welcome" usemap="#m_nav" alt="Navigation Menu" />
        <map name="m_nav" id="m_nav">
        <area shape="poly" coords="474,5,610,5,616,7,618,13,618,40,616,46,610,48,474,48,468,46,466,40,466,13,468,7,474,5,474,5" href="Questions.aspx" title="Questions?" alt="Questions?" />
        <area shape="poly" coords="12,5,148,5,154,7,156,13,156,40,154,46,148,48,12,48,6,46,4,40,4,13,6,7,12,5,12,5" href="Default.aspx" title="Welcome" alt="Welcome" />
        <area shape="rect" coords="639,22,723,42" href="mailto:support@plasticchatter.ca" alt="" />
        <area shape="poly" coords="320,5,456,5,462,7,464,13,464,40,462,46,456,48,320,48,314,46,312,40,312,13,314,7,320,5,320,5" href="Community.aspx" title="Community" alt="Community" />
        <area shape="poly" coords="166,5,302,5,308,7,310,13,310,40,308,46,302,48,166,48,160,46,158,40,158,13,160,7,166,5,166,5" href="Services.aspx" title="web services" alt="web services" />
        </map>
    </div>
</asp:Content>    

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
<div>
    <asp:Panel ID="RegisterPanel" DefaultButton="SubmitButton" runat="server" />
    <table>
            <tr>
                <td colspan="2">
                    <asp:Label ID="UserNameNotAvailableLabel" Text="Username not available, please choose another." ForeColor="Red" Visible="false" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="UserNameLabel" Text="User Name:" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="UserNameTextBox" runat="server" />
                    <asp:RequiredFieldValidator ID="UsernameRequiredFieldValidator" runat="server" 
                        ControlToValidate="UserNameTextBox" ErrorMessage="Username Required" ValidationGroup="RegisterGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="EmailLabel" Text="Email:" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="EmailTextBox" runat="server" />
                    <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" 
                        ControlToValidate="EmailTextBox" ErrorMessage="Email Required" ValidationGroup="RegisterGroup"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="EmailValidation" runat="server" 
                        ControlToValidate="EmailTextBox" ErrorMessage="Invalid Email" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        ValidationGroup="RegisterGroup"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="PasswordLabel" Text="Password:" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="PasswordRequiredFieldValidator" runat="server" 
                        ControlToValidate="PasswordTextBox" ErrorMessage="Password Required" ValidationGroup="RegisterGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="PasswordReenterLabel" Text="Re-enter Password:" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="PasswordReenterTextBox" runat="server" TextMode="Password" />
                    <asp:CompareValidator ID="PasswordComparer" runat="server" 
                        ControlToCompare="PasswordTextBox" ControlToValidate="PasswordReenterTextBox" 
                        ErrorMessage="Passwords Do Not Match" ValidationGroup="RegisterGroup"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                      <recaptcha:RecaptchaControl
                      ID="recaptcha"
                      runat="server"
                      PublicKey="6LdWVwYAAAAAAF9w9Z2M1YbZvSOa-oNruHzod_sN"            
                      PrivateKey="6LdWVwYAAAAAADZ0-s5OLSA3us1NT5yG934KEnUs"
                      />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="SubmitButton" runat="server" OnClick="SubmitButtonClick" 
                        Text="Submit" ValidationGroup="RegisterGroup" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
