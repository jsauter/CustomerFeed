<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="RetrievePassword.aspx.cs" Inherits="CustomerFeed.RetrievePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    
    <asp:Panel ID="StartRetrievePanel" DefaultButton="StartRetrieveSubmitButton" runat="server">
    Enter the following information to retrieve your password.  
    It will be emailed to the address you specified at account creation.<br />
    <asp:Label ID="ResponseInformationLabel" runat="server" Visible="false" /><asp:HyperLink ID="ReturnToLoginHyperLink" Text="Return To Login" NavigateUrl="~/Default.aspx" runat="server" />
            
    <table>
        <tr>                       
            <td>
                <asp:Label id="UserNameLabel" Text="Username:" runat="server" />            
            </td>
            <td>
                <asp:TextBox id="UserNameTextBox" runat="server" />
                <asp:RequiredFieldValidator ID="UserNameRequiredField" runat="server" 
                    ControlToValidate="UserNameTextBox" ValidationGroup="SubmitStart" >User Name Required</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label id="UserEmailLabel" Text="Email:" runat="server" />            
            </td>
            <td>
                <asp:TextBox id="UserEmailTextBox" runat="server" />
                <asp:RequiredFieldValidator ID="UserEmailRequiredField" runat="server" 
                    ControlToValidate="UserEmailTextBox" ValidationGroup="SubmitStart" >Email Required</asp:RequiredFieldValidator>
            </td>
        </tr>     
    </table>
    <asp:Button ID="StartRetrieveSubmitButton" Text="Submit" runat="server" 
        onclick="StartRetrieveSubmitButton_Click" ValidationGroup="SubmitStart" />
    </asp:Panel>

    <asp:Label ID="PasswordResetLabel" runat="server" />
    <asp:Panel ID="ResetPasswordPanel" DefaultButton="ResetPasswordSubmitButton" visible="false" runat="server">
    Enter your new password and click 'Submit'.<br />        
    <table>
        <tr>
            <td>
                <asp:Label id="PasswordLabel" Text="Password:" runat="server" />            
            </td>
            <td>
                <asp:TextBox id="PasswordTextBox" runat="server" TextMode="Password" />            
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label id="ReEnterPasswordLabel" Text="Re-enter Password:" runat="server" />            
            </td>
            <td>
                <asp:TextBox id="ReEnterPasswordTextBox" runat="server" TextMode="Password" />
                <asp:CompareValidator ID="PasswordCompareValidator" runat="server" 
                    ControlToCompare="PasswordTextBox" ControlToValidate="ReEnterPasswordTextBox" ValidationGroup="SubmitFinish" >Passwords 
                Must Match</asp:CompareValidator>
            </td>
        </tr>     
    </table>
        <asp:Button ID="ResetPasswordSubmitButton" Text="Submit" runat="server" 
        onclick="ResetPasswordSubmitButton_Click" ValidationGroup="SubmitFinish" />
    </asp:Panel>
   
</asp:Content>
