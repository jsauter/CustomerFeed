using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web;
using System.Web.UI;
using CustomerFeed.Controllers;
using CustomerFeed.Models;
using Image=System.Web.UI.WebControls.Image;

namespace CustomerFeed
{
    public partial class Customer : WebApp
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            ValidateSecurity();

            if (!IsPostBack)
            {
                ProcessRequest();                
            }
        }

        private void ProcessRequest()
        {

            State state = new State();

            string customerId = Request.QueryString["Id"];

            int result;
            bool isNumber = int.TryParse(customerId, out result);

            if ((!String.IsNullOrEmpty(customerId) && customerId != "0") &&
                isNumber && (state.Cache().IsAdminstrator() ||
                String.Compare(state.Cache().GetUser().CustomerId.ToString(), customerId) == 0))
            {
                PayPalPanel.Visible = true;
                LoadCustomer(customerId);
                Initialize();
            }
            else if (customerId == "0" && isNumber)
            {
                HeaderPanel.Visible = false;
                PictureImage.Visible = false;
                Initialize();
            }
            else
            {
                SendToInvalidRequest();
            }
        }

        private void Initialize()
        {
            State state = new State();

            //If we are an administrator all fields are editable
            if (state.Cache().IsAdminstrator())
            {
                HeaderPanel.Visible = true;
                ExpiryPanel.Visible = true;
                AccountStatus.Visible = false;
                AccountStatusDropDownList.Visible = true;
                ExpiryDate.Visible = false;
                ExpiryDateCalendar.Visible = true;
                SubscriptionType.Visible = false;
                SubscriptionTypeDropDownList.Visible = true;
                PayPalPanel.Visible = true;
                //HTMLEditor.Visible = true;
                //EditorLabel.Visible = true;
            }
        }

        private void LoadCustomer(string customerId)
        {
            CustomerController controller = new CustomerController();

            CustomerModel model = controller.GetCustomer(System.Convert.ToInt32(customerId));

            if(model != null)
            {
                PopulateCustomerFields(model);    
            }
            else
            {
                SendToInvalidRequest();
            }
        }

        private void PopulateCustomerFields(CustomerModel model)
        {
            CustomerId.Text = model.Id.ToString();
            AccountStatusDropDownList.SelectedValue = model.AccountStatus;
            AccountStatus.Text = AccountStatusDropDownList.SelectedItem.Text;  //using dropdown to get text so easier to maintain            
            ExpiryDate.Text = model.ExpiryDate;
            ExpiryDateCalendar.SelectedDate = System.DateTime.Parse(model.ExpiryDate);
            ExpiryDateCalendar.VisibleDate = System.DateTime.Parse(model.ExpiryDate);
            SubscriptionTypeDropDownList.SelectedValue = model.SubscriptionType;
            SubscriptionType.Text = SubscriptionTypeDropDownList.SelectedItem.Text; //using dropdown to get text so easier to maintain       
            WebOfficeUserNameTextBox.Text = model.WebOfficeUserName;
            FirstNameTextbox.Text = model.FirstName;
            LastNameTextBox.Text = model.LastName;
            EmailTextbox.Text = model.Email;
            PhoneTextBox.Text = model.PhoneNumber;
            StreetAddress1Textbox.Text = model.StreetNumber1;
            StreetAddress2Textbox.Text = model.StreetNumber2;
            CityTextBox.Text = model.City;
            ProvinceTextBox.Text = model.Province;
            PostalCodeTextBox.Text = model.PostalCode;
            PreferredLanguageDropDownList.SelectedValue = model.PreferredLanguage;
            TupperwareLevelDropDownList.SelectedValue = model.TupperwareLevel;
            ReferrerTextBox.Text = model.Referrer;
            HTMLEditor.Content = model.HtmlContent;
            ProfileTextBox.Text = model.Profile.Replace("<br />", Environment.NewLine);
            PictureIdLabel.Text = model.PictureId.ToString();

            if(model.PictureId != 0)
            {
                PictureImage.ImageUrl = "~/ProfileImage.aspx?ImageId=" + model.PictureId;
            }
            else
            {
                PictureImage.Visible = false;
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            int customerId = SaveCustomer();
            SaveUser(customerId);
            Response.Redirect("Customer.aspx?Id=" + customerId);
        }


        protected int SaveCustomer()
        {
            CustomerController controller = new CustomerController();

            CustomerModel customerModel = new CustomerModel();

            customerModel.Id = CustomerId.Text == "" ? 0 : System.Convert.ToInt64(CustomerId.Text);
            customerModel.WebOfficeUserName = WebOfficeUserNameTextBox.Text;
            customerModel.FirstName = FirstNameTextbox.Text;
            customerModel.LastName = LastNameTextBox.Text;
            customerModel.Email = EmailTextbox.Text;
            customerModel.PhoneNumber = PhoneTextBox.Text;
            customerModel.StreetNumber1 = StreetAddress1Textbox.Text;
            customerModel.StreetNumber2 = StreetAddress2Textbox.Text;
            customerModel.City = CityTextBox.Text;
            customerModel.Province = ProvinceTextBox.Text;
            customerModel.PostalCode = PostalCodeTextBox.Text;
            customerModel.PreferredLanguage = PreferredLanguageDropDownList.SelectedItem.Value;
            customerModel.TupperwareLevel = TupperwareLevelDropDownList.SelectedItem.Value;
            customerModel.Referrer = ReferrerTextBox.Text;
            customerModel.HtmlContent = HTMLEditor.Content;
            customerModel.AccountStatus = AccountStatusDropDownList.SelectedItem.Value;
            customerModel.ExpiryDate = ExpiryDateCalendar.SelectedDate.ToString("yyyy-MM-dd hh:mm:ss");
            customerModel.SubscriptionType = SubscriptionTypeDropDownList.SelectedItem.Value;
            customerModel.Profile = ProfileTextBox.Text.Replace(Environment.NewLine, "<br />");
            
            if(PictureIdLabel.Text != "")
            {
                customerModel.PictureId = Convert.ToInt32(PictureIdLabel.Text);    
            }
            else
            {
                customerModel.PictureId = 0;
            }
            
            
                if (PictureUpload.PostedFile != null && !String.IsNullOrEmpty(PictureUpload.PostedFile.FileName) && PictureUpload.PostedFile.InputStream != null)
                {
                    customerModel.PictureChaged = true;

                    string extension = Path.GetExtension(PictureUpload.PostedFile.FileName).ToLower();
                    bool validMime = false;

                    switch (extension)
                    {
                        case ".gif":
                            validMime = true;
                            break;
                        case ".jpg":
                        case ".jpeg":
                        case ".jpe":
                            validMime = true;
                            break;
                        case ".png":
                            validMime = true;
                            break;
                    }

                    if (validMime)
                    {
                        byte[] imageBytes = new byte[PictureUpload.PostedFile.InputStream.Length];
                        PictureUpload.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);

                        customerModel.Picture.Name = PictureUpload.PostedFile.FileName;
                        customerModel.Picture.Size = (int)PictureUpload.PostedFile.InputStream.Length;
                        customerModel.Picture.Data = ImageUtilites.ResizeImage(imageBytes);
                    }
                }
            

            int customerId = controller.SaveCustomer(customerModel);

            return customerId;
        }

        

        protected void SaveUser(int customerId)
        {
            State state = new State();
            //Returns 0 in case of update, if it is an update, we dont do the update so
            //to prevent the admins from associating to other accounts.
            if (customerId != 0 && !state.Cache().IsAdminstrator())
            {
                //saving customer id to the user we are logged in as
                UserModel userModel = state.Cache().GetUser();
                
                UserController controller = new UserController();

                userModel.CustomerId = customerId;

                controller.SaveUser(userModel);
            }
        }
    }
}
