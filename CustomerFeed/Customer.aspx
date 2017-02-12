<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="CustomerFeed.Customer" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="navigation" runat="server">
    <div id="navigation">
        <img src="images/nav_welcome.png" class="nav" id="nav_welcome" usemap="#m_nav" alt="Navigation Menu" />
        <map name="m_nav" id="m_nav">
        <area shape="poly" coords="12,5,148,5,154,7,156,13,156,40,154,46,148,48,12,48,6,46,4,40,4,13,6,7,12,5,12,5" href="Default.aspx" title="Welcome" alt="Welcome" />
        <area shape="rect" coords="639,22,723,42" href="javascript:;" alt="" />
        <area shape="poly" coords="320,5,456,5,462,7,464,13,464,40,462,46,456,48,320,48,314,46,312,40,312,13,314,7,320,5,320,5" href="Community.aspx" title="Community" alt="Community" />
        <area shape="poly" coords="166,5,302,5,308,7,310,13,310,40,308,46,302,48,166,48,160,46,158,40,158,13,160,7,166,5,166,5" href="Services.aspx" title="web services" alt="web services" />
        </map>
    </div>
</asp:Content>    

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div id="registration">
<h2>my PlasticChatter account information</h2>
<asp:Panel ID="HeaderPanel" runat="server">
    <table>
    <tr><td><asp:Label ID="CustomerIdLabel" Text="Customer Id:" runat="server" /></td>
        <td><asp:Label ID="CustomerId" runat="server" /></td></tr>
    <tr><td><asp:Label ID="AccountStatusLabel" Text="Account Status:" runat="server" /></td>
        <td><asp:Label ID="AccountStatus" visible="true" runat="server" />
            <asp:DropDownList ID="AccountStatusDropDownList" Visible="false" runat="server">
                <asp:ListItem Value="1">Active</asp:ListItem>
                <asp:ListItem Value="2">Inactive</asp:ListItem>
            </asp:DropDownList></td></tr>
    <tr><td><asp:Label ID="SubscriptionTypeLabel" runat="server" Text="Subscription Type:" /></td>
        <td><asp:Label ID="SubscriptionType" runat="server" Visible="true" />
            <asp:DropDownList ID="SubscriptionTypeDropDownList" runat="server" visible="false">
                <asp:ListItem Value="1">Free</asp:ListItem>
                <asp:ListItem Value="2">Monthly</asp:ListItem>
                <asp:ListItem Value="3">Yearly</asp:ListItem>
                <asp:ListItem Value="4">Unlimited</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr><td><asp:Panel ID="ExpiryPanel" runat="server">
            <asp:Label ID="ExpiryDateLabel" runat="server" Text="Account Expiry Date:" /></td>
        <td><asp:Label ID="ExpiryDate" runat="server" Visible="true" />
            <asp:Calendar ID="ExpiryDateCalendar" runat="server" Visible="false" /></td>
        </asp:Panel>
    </tr>
    <tr><td><asp:Label ID="PreferredLanguageLabel" runat="server" Text="Preferred Language:" /></td>
        <td><asp:DropDownList ID="PreferredLanguageDropDownList" runat="server">
                <asp:ListItem>English</asp:ListItem>
                <asp:ListItem>French</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr><td><asp:Label ID="TupperwareLevelLabel" runat="server" Text="Tupperware Level:" /></td>
        <td><asp:DropDownList ID="TupperwareLevelDropDownList" runat="server">
                <asp:ListItem Value="1">Consultant</asp:ListItem>
                <asp:ListItem Value="2">Manager</asp:ListItem>
                <asp:ListItem Value="3">Director</asp:ListItem>
                <asp:ListItem Value="4">Star Manager</asp:ListItem>
                <asp:ListItem Value="5">DIQ</asp:ListItem>
                <asp:ListItem Value="7">2 Star Director</asp:ListItem>
                <asp:ListItem Value="8">3 Star Director</asp:ListItem>
                <asp:ListItem Value="9">Executive Director</asp:ListItem>
                <asp:ListItem Value="10">Legacy Executive Director</asp:ListItem>
                <asp:ListItem Value="11">Presidential Director</asp:ListItem>
                <asp:ListItem Value="6">Star Director</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr><td><asp:Label ID="ReferrerLabel" runat="server" Text="Referred by:" /></td>
        <td><asp:TextBox ID="ReferrerTextBox" runat="server" /></td>
    </tr>
        </asp:Panel>
    </table>
</div>
<br /><br />
<div id="registration">
<h2>my web services customizations</h2>
<asp:Label ID="UserNameLabel" Text="my.tupperware.ca account name:" runat="server" />
<asp:TextBox ID="WebOfficeUserNameTextBox" runat="server" />
                        <br /><br /><br />
    <div id="registration_left">
        <asp:Label ID="PictureLabel" Text="Picture:" runat="server" />
        <asp:Label ID="PictureIdLabel" Visible="false" runat="server" />
                        <br />
            <asp:Image ID="PictureImage" runat="server" />           
            <asp:FileUpload ID="PictureUpload" runat="server" />                               
                        <br />
           </asp:Panel>
    </div>
    <div id="registration_right">
        <asp:Label ID="ProfileLabel" Text="about me:" runat="server" />
        <asp:TextBox ID="ProfileTextBox" TextMode="MultiLine" Height="200" Width="450" runat="server" />
    </div>
</div>
<div id="registration">
<asp:Label ID="EditorLabel" Text="Customer Feed:" Visible="False" runat="server" />
                        <br />
<ed:Editor PathPrefix="Editor_data/" id="HTMLEditor" Submit="false" visible="false" runat="server" InsideOboutWindow="false" 
            ShowQuickFormat="false" ImageBrowse="myImageBrowse.aspx" AutoFocus="False">                                        
    <Buttons>
        <ed:HorizontalSeparator/>
	    <ed:Custom OnClientClick="myImageUpload" ImageName="ed_upload_image_a.gif" ToolTip="Upload image" />
    </Buttons>
</ed:Editor>
</div>
<br /><br />
<div id="registration">
<h2>my billing information</h2>
<table id="billing">
    <tr>
        <td width="10px"><br /></td>
        <td width="100px"><asp:Label ID="FirstNameLabel" Text="First Name:" runat="server" /></td>
        <td width="20px"><br /></td>
        <td width="200px"><asp:Label ID="LastNameLabel" Text="Last Name:" runat="server" /></td>
    </tr>
    <tr>
        <td><br /></td>
        <td><asp:TextBox ID="FirstNameTextbox" runat="server" width="200" /></td>
        <td><br /></td>
        <td><asp:TextBox ID="LastNameTextBox" runat="server" width="300" /></td>
    </tr>
    <tr>
        <td><br /></td>
        <td><asp:Label ID="EmailLabel" Text="Email:" runat="server" /></td>
        <td><br /></td>
        <td><asp:Label ID="PhoneLabel" Text="Phone:" runat="server" /></td>
    </tr>
    <tr>
        <td><br /></td>
        <td><asp:TextBox ID="EmailTextbox" runat="server" width="300" /></td>
        <td><br /></td>
        <td><asp:TextBox ID="PhoneTextBox" runat="server" width="300" /></td>
    </tr>
    <tr>
        <td><br /></td>
        <td><asp:Label ID="StreetAddress1Label" Text="Street Address 1:" runat="server" width="300" /></td>
        <td><br /></td>
        <td><asp:Label ID="StreetAddress2Label" Text="Street Address 2:" runat="server" width="300" /></td>
    </tr>
    <tr>
        <td><br /></td>
        <td><asp:TextBox ID="StreetAddress1Textbox" runat="server" /></td>
        <td><br /></td>
        <td><asp:TextBox ID="StreetAddress2Textbox" runat="server" /></td>
    </tr>
    <tr>
        <td><br /></td>
        <td><asp:Label ID="CityLabel" Text="City:" runat="server" /></td>
        <td><br /></td>
        <td><asp:Label ID="ProvinceLabel" Text="Province:" runat="server" /></td>
    </tr>
    <tr>
        <td><br /></td>
        <td><asp:TextBox ID="CityTextBox" runat="server" /></td>
        <td><br /></td>
        <td><asp:TextBox ID="ProvinceTextBox" runat="server" /></td>
    </tr>
    <tr>
        <td><br /></td>
        <td><asp:Label ID="PostalCodeLabel" Text="Postal Code:" runat="server" /></td>
        <td><br /></td>
        <td><br /></td>
    </tr>    
    <tr>
        <td><br /></td>
        <td><asp:TextBox ID="PostalCodeTextBox" runat="server" /></td>
        <td><br /></td>
        <td><br /></td>
    </tr>
</table>
<br />
<br />
<asp:Button ID="SaveButton" runat="server" Text="Save My Account Information" onclick="SubmitButton_Click" width="300" CssClass="SaveMyInfoButton" />
</div>
<br />
<br />
<asp:Panel ID="PayPalPanel" runat="server" Visible="false">
<div id="registration">
<h2>my payment options</h2>
<div id="paypal_left">
<p>To have monthly payments made from your MasterCard, Visa, AMEX, Bank or PayPal account:<br /></p>
    <p><br /></p>
<!-- PayPal subscribe -->
    <input type="hidden" name="cmd" value="_s-xclick" />
    <input type="hidden" name="hosted_button_id" value="4274765" />
    <%--<a href="javascript:theForm.__VIEWSTATE.value='';theForm.encoding='application/x-www-form-urlencoded';theForm.action='https://www.paypal.com/cgi-bin/webscr';theForm.submit();"><img src="https://www.paypal.com/en_US/i/btn/btn_subscribeCC_LG.gif" border="0"></a>--%>
    <a href="javascript:theForm.encoding='application/x-www-form-urlencoded';theForm.action='https://www.paypal.com/cgi-bin/webscr';theForm.submit();"><img src="https://www.paypal.com/en_US/i/btn/btn_subscribe_LG.gif" class="center" /></a>
    <img alt="" border="0" src="https://www.paypal.com/en_US/i/scr/pixel.gif" width="1" height="1">
</div>
<div id="paypal_right">
<!-- Paypal monthly -->
<p>Just want to try us out?  Choose the 3 month service option <br />Or prepay for one year and get two months free!<br /><br /></p>
    <input type="hidden" name="cmd" value="_s-xclick" />
    <input type="hidden" name="hosted_button_id" value="4274733" />
    <input type="hidden" name="on0" value="Term" />
    <select name="os0" class="paypal">
        <option value="12 months">12 months $120.00</option>
        <option value="3 months">3 months $36.00</option>
    </select> 
    <input type="hidden" name="currency_code" value="CAD" />
    <%--<a href="javascript:theForm.__VIEWSTATE.value='';theForm.encoding='application/x-www-form-urlencoded';theForm.action='https://www.paypal.com/cgi-bin/webscr';theForm.submit();"><img src="https://www.paypal.com/en_US/i/btn/btn_paynowCC_LG.gif" border="0"></a>                    --%>
    <a href="javascript:theForm.encoding='application/x-www-form-urlencoded';theForm.action='https://www.paypal.com/cgi-bin/webscr';theForm.submit();"><img src="https://www.paypal.com/en_US/i/btn/btn_paynow_LG.gif" class="center" /></a>                    
    <img alt="" border="0" src="https://www.paypal.com/en_US/i/scr/pixel.gif" width="1" height="1">
</div>    
</div>
</asp:Panel>
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
</asp:Content>
