<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Administration.aspx.cs" Inherits="CustomerFeed.Administration
" %>
<%@ Register TagPrefix="vs" Namespace="Vladsm.Web.UI.WebControls" 
                                          Assembly="GroupRadioButton" %>
                                          
<%@ Import Namespace="CustomerFeed.Models"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div>  
        <table>
            <tr>
                <td>
                    <asp:LinkButton ID="CustomerListLinkButton" runat="server" 
                        CommandArgument="Customers" oncommand="SwitchPane" >Customers</asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="ReferrerListLinkButton" runat="server" 
                        CommandArgument="Referrers" oncommand="SwitchPane" >Page Referrers</asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="UsersLinkButton" runat="server" 
                        CommandArgument="Users" oncommand="SwitchPane" >Users</asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="ReplacersLinkButton" runat="server" 
                        CommandArgument="Replacers" oncommand="SwitchPane" >Text Replacers</asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="PageContentLinkButton" runat="server"
                        CommandArgument="PageContent" OnCommand="SwitchPane">Page Content</asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="LogsLinkButton" runat="server"
                        CommandArgument="Logs" OnCommand="SwitchPane">Logs</asp:LinkButton>
                </td>
            </tr>
        </table>
        <table>
            <asp:Repeater ID="CustomerList" runat="server">               
                <HeaderTemplate>
                    <tr class="TableHeader">
                        <td>
                            Id
                        </td>
                        <td>
                            User Name
                        </td>
                        <td>
                            First Name
                        </td>
                        <td>
                            Last Name
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "Id")%>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "WebOfficeUserName")%>
                        </td>
                        <td>                           
                           <%# DataBinder.Eval(Container.DataItem, "FirstName")%>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "LastName")%>
                        </td>
                        <td>
                            <asp:HyperLink ID="HyperLink1" class="text" NavigateUrl='<%# "Customer.aspx?Id=" + DataBinder.Eval(Container.DataItem, "Id") %>' Text='Edit' runat="server" /> 
                        </td>
                    </tr>              
                </ItemTemplate>
                 <AlternatingItemTemplate>
                     <tr class="AlternatingTableRow">
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "Id")%>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "WebOfficeUserName")%>
                        </td>
                        <td>                           
                           <%# DataBinder.Eval(Container.DataItem, "FirstName")%>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "LastName")%>
                        </td>
                        <td>
                            <asp:HyperLink ID="HyperLink1" class="text" NavigateUrl='<%# "Customer.aspx?Id=" + DataBinder.Eval(Container.DataItem, "Id") %>' Text='Edit' runat="server" /> 
                        </td>
                    </tr>         
                </AlternatingItemTemplate>                
            </asp:Repeater>
        </table>
       
        <table>
            <asp:Repeater ID="ReferrerList" runat="server" 
                onitemcommand="ReferrerList_ItemCommand" Visible="false">
                <HeaderTemplate>
                     <tr class="TableHeader">
                        <td>
                            Id
                        </td>
                        <td>
                            Referrer Url
                        </td>                        
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label ID="ReferrerIdLabel" Text='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" />
                        </td>
                        <td>                           
                           <asp:Label ID="ReferrerUrlLabel" Text='<%# DataBinder.Eval(Container.DataItem, "ReferrerUrl") %>' runat="server" />
                           <asp:TextBox ID="ReferrerUrlTextBox" Text='<%# DataBinder.Eval(Container.DataItem, "ReferrerUrl") %>' Visible="false" runat="server" />
                        </td>
                        <td>
                            <asp:LinkButton class="text" ID="EditLinkbutton" Text="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Edit" runat="server" /> 
                            <asp:LinkButton ID="SaveLinkButton" class="text" Text="Save" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Save" Visible="false" runat="server" /> 
                        </td>
                        <td>
                            <asp:LinkButton ID="DeleteLinkButton" class="text" Text="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Delete" runat="server" /> 
                        </td>
                    </tr>              
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="AlternatingTableRow">
                        <td>
                           <asp:Label ID="ReferrerIdLabel" Text='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" />
                        </td>
                        <td>                           
                           <asp:Label ID="ReferrerUrlLabel" Text='<%# DataBinder.Eval(Container.DataItem, "ReferrerUrl") %>' runat="server" />
                           <asp:TextBox ID="ReferrerUrlTextBox" Text='<%# DataBinder.Eval(Container.DataItem, "ReferrerUrl") %>' Visible="false" runat="server" />
                        </td>
                        <td>
                            <asp:LinkButton class="text" ID="EditLinkbutton" Text="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Edit" runat="server" /> 
                            <asp:LinkButton ID="SaveLinkButton" class="text" Text="Save" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Save" Visible="false" runat="server" /> 
                        </td>
                        <td>
                            <asp:LinkButton ID="DeleteLinkButton" class="text" Text="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Delete" runat="server" /> 
                        </td>
                    </tr>  
                </AlternatingItemTemplate>
                <FooterTemplate>
                     <tr>
                            <td>                           
                             
                            </td>
                             <td>                           
                               <asp:TextBox id="AddReferrerUrlTextBox" runat="server" />
                            </td>
                            <td colspan="2">                          
                                <asp:LinkButton ID="AddLinkButton" class="text" Text="Add" CommandName="Add" runat="server" /> 
                            </td>
                        </tr> 
                </FooterTemplate>
            </asp:Repeater>
            </table>      
        <table>
            <asp:Repeater Visible="false" ID="UsersList" runat="server" 
                onitemcommand="UsersList_ItemCommand">
                <HeaderTemplate>
                     <tr class="TableHeader">
                        <td>
                            Id
                        </td>
                        <td>
                            User Name
                        </td>        
                        <td>
                            Email
                        </td>    
                        <td>
                            Customer Id
                        </td>          
                        <td>
                            Administrator
                        </td>  
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "Id") %>
                        </td>
                        <td>                           
                           <%# DataBinder.Eval(Container.DataItem, "UserName") %>
                        </td>
                         <td>                           
                           <%# DataBinder.Eval(Container.DataItem, "Email") %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "CustomerId") %>
                        </td>
                        <td>
                            <asp:CheckBox ID="AdministratorCheckbox" Checked='<%# (bool)DataBinder.Eval(Container.DataItem, "Administrator") %>' runat="server" />
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton1" class="text" Text="Save" CommandName="Save"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" /> 
                        </td>
                    </tr>              
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="AlternatingTableRow">
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "Id") %>
                        </td>
                        <td>                           
                           <%# DataBinder.Eval(Container.DataItem, "UserName") %>
                        </td>
                         <td>                           
                           <%# DataBinder.Eval(Container.DataItem, "Email") %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "CustomerId") %>
                        </td>
                        <td>
                            <asp:CheckBox ID="AdministratorCheckbox" Checked='<%# (bool)DataBinder.Eval(Container.DataItem, "Administrator") %>' runat="server" />
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton2" class="text" Text="Save" CommandName="Save" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" /> 
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
        </table>
        
        
        <asp:Panel ID="ReplacerPanel" Visible="false" runat="server">
        <table>
            <asp:Repeater ID="ReplacerList" runat="server" 
                onitemcommand="ReplacerList_ItemCommand">
                <HeaderTemplate>
                     <tr class="TableHeader">
                        <td>
                            Id
                        </td>
                        <td>
                            Keyword
                        </td>        
                        <td>
                            Data Field
                        </td>                           
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label ID="IdLabel" Text='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" />
                        </td>
                        <td>                           
                           <asp:Label ID="KeywordLabel" Text='<%# DataBinder.Eval(Container.DataItem, "Keyword") %>' runat="server" />
                           <asp:TextBox id="KeywordTextbox" Text='<%# DataBinder.Eval(Container.DataItem, "Keyword") %>' visible="false" runat="server" />
                        </td>
                         <td>                           
                           <asp:Label ID="DataFieldLabel" Text='<%# DataBinder.Eval(Container.DataItem, "DataField") %>' runat="server" />
                           <asp:TextBox id="DataFieldTextBox" Text='<%# DataBinder.Eval(Container.DataItem, "DataField") %>' visible="false" runat="server" />
                        </td>                       
                        <td>
                            <asp:LinkButton ID="EditLinkButton" class="text" Text="Edit" CommandName="Edit"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" /> 
                            <asp:LinkButton ID="SaveLinkButton" class="text" Text="Save" CommandName="Save" Visible="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" /> 
                        </td>
                         <td>
                            <asp:LinkButton ID="DeleteLinkButton" class="text" Text="Delete" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" /> 
                        </td>
                    </tr>              
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="AlternatingTableRow">
                        <td>
                            <asp:Label ID="IdLabel" Text='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" />
                        </td>
                        <td>                           
                           <asp:Label ID="KeywordLabel" Text='<%# DataBinder.Eval(Container.DataItem, "Keyword") %>' runat="server" />
                           <asp:TextBox id="KeywordTextbox" Text='<%# DataBinder.Eval(Container.DataItem, "Keyword") %>' visible="false" runat="server" />
                        </td>
                         <td>                           
                           <asp:Label ID="DataFieldLabel" Text='<%# DataBinder.Eval(Container.DataItem, "DataField") %>' runat="server" />
                           <asp:TextBox id="DataFieldTextBox" Text='<%# DataBinder.Eval(Container.DataItem, "DataField") %>' visible="false" runat="server" />
                        </td>                        
                        <td>
                            <asp:LinkButton ID="EditLinkButton" class="text" Text="Edit" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" /> 
                            <asp:LinkButton ID="SaveLinkButton" class="text" Text="Save" CommandName="Save" Visible="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" /> 
                        </td>                        
                        <td>
                            <asp:LinkButton ID="DeleteLinkButton" class="text" Text="Delete" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" /> 
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>                 
                        <tr>
                            <td>                           
                               
                            </td>
                            <td>                           
                               <asp:TextBox id="AddKeywordTextbox" runat="server" />
                            </td>
                             <td>                           
                               <asp:TextBox id="AddDataFieldTextBox" runat="server" />
                            </td>
                            <td colspan="2">                          
                                <asp:LinkButton ID="AddLinkButton" class="text" Text="Add" CommandName="Add" runat="server" /> 
                            </td>
                        </tr>                    
                </FooterTemplate>                
            </asp:Repeater>
             </table>
            <table>
                <tr>
                    <asp:Label ID="PossibleFieldsLabel" Text="Fields available for mapping on Customer: " runat="server" />
                    <asp:Label ID="PossibleFields" runat="server" />
                </tr>
                <tr>
                
                </tr>
            </table>
        </asp:Panel>
        <table>
            <asp:Repeater ID="PageContentList" runat="server" 
                onitemcommand="PageContentList_ItemCommand" Visible="false">
                <HeaderTemplate>
                     <tr class="TableHeader">
                        <td>
                            Id
                        </td>
                        <td>
                            Page Name
                        </td>
                        <td>
                            Start Page
                        </td>                                   
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label ID="IdLabel" Text='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" />
                        </td>
                        <td>                           
                           <asp:Label ID="PageNameLabel" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' runat="server" />
                         </td>
                         <td>
                            <vs:GroupRadioButton ID="StartPageRadioButton" Checked='<%# (bool)DataBinder.Eval(Container.DataItem, "StartPage") %>' GroupName="StartPage" runat="server" />
                         </td>                         
                         <td>
                            <asp:LinkButton ID="EditLinkButton" class="text" Text="Edit" CommandName="Edit"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" /> 
                         </td>
                         <td>
                            <asp:LinkButton ID="DeleteLinkButton" class="text" Text="Delete" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" /> 
                        </td>
                    </tr>              
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="AlternatingTableRow">
                        <td>
                            <asp:Label ID="IdLabel" Text='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" />
                        </td>
                        <td>                           
                           <asp:Label ID="PageNameLabel" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' runat="server" />
                         </td>
                         <td>
                            <vs:GroupRadioButton ID="StartPageRadioButton" Checked='<%# (bool)DataBinder.Eval(Container.DataItem, "StartPage") %>' GroupName="StartPage" runat="server" />                      
                         </td>                         
                         <td>
                            <asp:LinkButton ID="EditLinkButton" class="text" Text="Edit" CommandName="Edit"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" /> 
                         </td>
                         <td>
                            <asp:LinkButton ID="DeleteLinkButton" class="text" Text="Delete" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" /> 
                        </td>
                    </tr>              
                </AlternatingItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td>
                            <asp:LinkButton ID="SaveLinkButton" class="text" Text="Save" CommandName="Save"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" /> 
                        </td>
                        <td colspan="5">
                            <asp:LinkButton ID="DeleteLinkButton" class="text" Text="Add" CommandName="Add" runat="server" /> 
                        </td>
                    </tr>
                </FooterTemplate>
            </asp:Repeater>
        </table>
        <asp:Panel ID="LogPanel" Visible="false" runat="server" >
        <table>
            <asp:Repeater ID="LogList" runat="server" onitemcommand="LogList_ItemCommand">
                <HeaderTemplate>
                     <tr class="TableHeader">
                        <td>
                            Id
                        </td>
                        <td>
                            Message
                        </td>      
                        <td>
                            Log Type
                        </td>       
                        <td>
                            Referrer Url
                        </td>
                        <td>
                            Timestamp
                        </td>        
                        <td>
                            Customer Id
                        </td>              
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label ID="IdLabel" Text='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" />
                        </td>
                        <td>                           
                           <asp:Label ID="PageNameLabel" Text='<%# DataBinder.Eval(Container.DataItem, "Message") %>' runat="server" />
                         </td>
                         <td>                           
                           <asp:Label ID="LogTypeLabel" Text='<%# DataBinder.Eval(Container.DataItem, "LogType") %>' runat="server" />
                         </td>
                         <td>                           
                           <asp:Label ID="ReferrerUrlLabel" Text='<%# DataBinder.Eval(Container.DataItem, "ReferrerUrl") %>' runat="server" />
                         </td>
                         <td>                           
                           <asp:Label ID="LogTimeStampLabel" Text='<%# DataBinder.Eval(Container.DataItem, "LogTimeStamp") %>' runat="server" />
                         </td>                      
                         <td>                           
                           <asp:Label ID="CustomerIdLabel" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerId") %>' runat="server" />
                         </td>
                                          
                         <td>
                            <asp:LinkButton ID="DeleteLinkButton" class="text" Text="Delete" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" /> 
                        </td>
                    </tr>              
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="AlternatingTableRow">
                        <td>
                            <asp:Label ID="IdLabel" Text='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" />
                        </td>
                        <td>                           
                           <asp:Label ID="PageNameLabel" Text='<%# DataBinder.Eval(Container.DataItem, "Message") %>' runat="server" />
                         </td>
                         <td>                           
                           <asp:Label ID="LogTypeLabel" Text='<%# DataBinder.Eval(Container.DataItem, "LogType") %>' runat="server" />
                         </td>
                         <td>                           
                           <asp:Label ID="ReferrerUrlLabel" Text='<%# DataBinder.Eval(Container.DataItem, "ReferrerUrl") %>' runat="server" />
                         </td>
                         <td>                           
                           <asp:Label ID="LogTimeStampLabel" Text='<%# DataBinder.Eval(Container.DataItem, "LogTimeStamp") %>' runat="server" />
                         </td>                      
                         <td>                           
                           <asp:Label ID="CustomerIdLabel" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerId") %>' runat="server" />
                         </td>
                                          
                         <td>
                            <asp:LinkButton ID="DeleteLinkButton" Text="Delete" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" /> 
                        </td>
                    </tr>                            
                </AlternatingItemTemplate>              
            </asp:Repeater>
        </table>
        <table>
            <tr>
                  <td>  <asp:label id="CurrentPageLabel" runat="server"></asp:label>&nbsp;&nbsp;<asp:LinkButton id="CmdLogRefresh" CommandArgument="Refresh" Text="Refresh" runat="server" OnCommand="LogRefresh" /></td>
            </tr>
            <tr>
                  <td>
                    <asp:LinkButton id="CmdPrev" CommandArgument="Previous" runat="server" OnCommand="LogListNavigation" >Previous</asp:LinkButton>
                    <asp:LinkButton id="CmdNext" CommandArgument="Next" runat="server" OnCommand="LogListNavigation">Next</asp:LinkButton>
                  </td>
           </tr>
           <tr>
                <td>
                    <asp:LinkButton ID="DeleteAllLogs" CommandArgument="DeleteAll" runat="server" OnCommand="LogDelete">Delete All Logs. This Cannot be undone</asp:LinkButton>
                </td>
           </tr>
        </table>
        </asp:Panel>
    </div>
</asp:Content>
