<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="PageManager.aspx.cs" Inherits="CustomerFeed.PageManager" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <asp:Panel ID="DeletePanel" runat="server" Visible="false">
        <table>
            <tr>
                <td>
                    <asp:Label ID="DeleteConfirmationLabel" runat="server" />
                </td>
            </tr>
            <tr>    
                <td>
                    <asp:LinkButton ID="NoLinkButton" runat="server" onclick="NoLinkButton_Click">No</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="YesLinkButton" runat="server" onclick="YesLinkButton_Click">Yes</asp:LinkButton>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="EditPanel" runat="server" Visible="true">
        <table>
            <tr>
                <td>
                    <asp:Label ID="PageIdLabel" runat="server" Visible="false" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="PageNameLabel" Text="Page Name:" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="PageNameTextBox" runat="server" /><asp:Label ID="PageNameInUseLabel" runat="server" visible="false" />
                </td>
            </tr>
                <tr>
                    <td>
                        <asp:Label ID="PageActiveLabel" Text="Active" runat="server" />                        
                    </td>
                    <td>
                        <asp:CheckBox ID="PageActiveCheckBox" runat="server" />
                    </td>
                </tr>
            </table>
            <table>                
            <tr>
                <td>
                    <ed:Editor PathPrefix="Editor_data/" id="HTMLEditor" Submit="false" 
                        runat="server" InsideOboutWindow="false"
                        ShowQuickFormat="false" Width="800" Height="500" ImageBrowse="myImageBrowse.aspx" AutoFocus="False">                                        
                       <Buttons>
                         <ed:HorizontalSeparator/>
                         <ed:Custom OnClientClick="myImageUpload" ImageName="ed_upload_image_a.gif" ToolTip="Upload image" />
                       </Buttons>                 
                    </ed:Editor>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="SaveButton" OnClick="SaveButton_Click" Text="Save" runat="server" />
                    <asp:Button ID="CancelButton" OnClick="CancelButton_Click" Text="Cancel" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    To add a photo, create the following HTML:  &lt;img src=&quot;ProfileImage.asxp?ImageId=PLACEHOLDER&quot;&lt;/a&gt;
                </td>
            </tr>
        </table>
        
        <script type="text/JavaScript">

        function myImageUpload(editor) {
            var imageFileName = null;
            var imageFileTitle = null;

            function postback(doc, popup_iframe) {
                if (doc != null) // Upload clicked
                {
                    if (doc.getElementById("path").value != "") {
                        var frm = doc.getElementById("fraExecute");

                        function onLoad() {
                            setTimeout(function() {
                                if (frm.detachEvent)
                                    frm.detachEvent("onload", onLoad);
                                else
                                    if (frm.removeEventListener)
                                    frm.removeEventListener("load", onLoad, true);

                                imageFileName = frm.contentWindow.imageFileName;
                                imageFileTitle = frm.contentWindow.imageFileTitle;

                                if (imageFileName == null)
                                    alert(frm.contentWindow.imageSaved);

                                popup_iframe.contentWindow.document.getElementById("cancel").onclick();  // emulate Cancel pressed
                            }, 0);
                        }

                        if (frm.attachEvent)
                            frm.attachEvent("onload", onLoad);
                        else
                            if (frm.addEventListener)
                            frm.addEventListener("load", onLoad, true);

                        try { doc.getElementById("frmFile").submit(); }
                        catch (e) {
                            alert(e.message);

                            if (frm.detachEvent)
                                frm.detachEvent("onload", onLoad);
                            else
                                if (frm.removeEventListener)
                                frm.removeEventListener("load", onLoad, true);

                            popup_iframe.contentWindow.document.getElementById("cancel").onclick();
                        }
                        return false;
                    }
                }
                else  // Cancel clicked or emulated
                {
                    if (imageFileName != null) {
                        editor.focusEditor();
                        editor.InsertHTML("<img src=\"" + imageFileName + "\" alt=\"" + imageFileTitle + "\" title=\"" + imageFileTitle + "\" />");
                    }
                }
            }

            function init(doc, popup_iframe) {
                if (doc != null) {
                    doc.getElementById("title").value = "";
                    if (document.all) {
                        popup_iframe.style.display = "none";
                        doc.getElementById("path").click();
                        popup_iframe.style.display = "";
                        if (doc.getElementById("path").value == "")
                            popup_iframe.contentWindow.document.getElementById("cancel").onclick();
                    }
                }
            }

            editor.customPopup("popup_image_upload_progress", "image-upload", "__cs_myImageUpload_progress.aspx", postback, init, false, false);
        }
    </script>
    </asp:Panel>
</asp:Content>
