<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="CustomerFeed.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
.;<table>
        <tr>
            <td>
            Error recieved.  Below is the contents of the error.
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="ErrorLabel" Text="Unknown error. Please contact your administrator" runat="server" />
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
