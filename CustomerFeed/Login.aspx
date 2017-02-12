<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CustomerFeed.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sideContent" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="UserNameLabel" Text="User Name:" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="UserNameTextBox" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="PasswordLabel" Text="Password:" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="SubmitButton" runat="server" OnClick="SubmitButtonClick" 
                        Text="Submit" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="RegisterHyperlink" Text="Click Here to Register" NavigateUrl="~/Register.aspx" runat="server" />                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="ResetPasswordHyperlink" Text="Reset Password" NavigateUrl="~/RetrievePassword.aspx" runat="server" />
                </td>
            </tr>
        </table>
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="server">

</asp:Content>
