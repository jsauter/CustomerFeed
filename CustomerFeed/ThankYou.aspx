<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ThankYou.aspx.cs" Inherits="CustomerFeed.ThankYou" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <table>
        <tr>
            <td>
                Congratulations <asp:Label ID="UserNameLabel" runat="server" />!  
                <br /><br />
                <br /><br />
                Your member profile is now set up and an email has been sent to <asp:Label ID="EmailLabel" runat="server" /> with details about your registrations.  
                <br /><br />
                You can now log in to join the community discussions! If you're interested in subscribing to our web services, simply log in and click on My Account.
                <br /><br />
                Thank you for joining PlasticChatter!
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="ReturnHomeLinkButton" Text="Return Home" runat="server" 
                    onclick="ReturnHomeLinkButton_Click" />
            </td>
        </tr>
    </table>
    
</asp:Content>
