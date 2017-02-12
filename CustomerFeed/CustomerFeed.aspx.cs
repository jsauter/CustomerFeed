using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomerFeed.Controllers;
using CustomerFeed.Models;

namespace CustomerFeed
{
    /// <summary>
    /// This page does not inherit from webapp base as we want it to be readable by anyone on the 
    /// specified domains.
    /// </summary>
    public partial class CustomerFeed : System.Web.UI.Page
    {
        private string RequestPage = "";
        private string CustomerId = "";
        private string Referrer = "";
        private string FullReferrer = "";
        private bool ValidRequest;
        private CustomerModel customer;
        private PageModel page;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Request.UrlReferrer != null)
                {
                    FullReferrer = Request.UrlReferrer.ToString();
                    Referrer = Request.UrlReferrer.Host;
                }
                
                CustomerId = Request.QueryString["Id"];
                RequestPage = Request.QueryString["Page"];

                LogRequest(FullReferrer, CustomerId, RequestPage);
                
                ValidateRequest();
            }
   
            if(ValidRequest)
            {
                customer = LoadCustomer();

                if (customer != null && IsCustomerActive() && (IsValidPage() || IsStartPage()))
                {                    
                    page = LoadPage();

                    LoadFeed();
                }
                else
                {
                    DisplayInvalidRequest(null);
                }
            }
        }

        private bool IsStartPage()
        {
            return String.IsNullOrEmpty(RequestPage);
        }

        private bool IsValidPage()
        {
            bool isValid = false;

            if(!String.IsNullOrEmpty(RequestPage))
            {
                if(IsAlphaNumeric(RequestPage))
                {
                    PageController pageController = new PageController();

                    PageModel pageModel = pageController.LoadPageByName(RequestPage);

                    if (pageModel != null && pageModel.Active)
                    {
                        isValid = true;
                    }

                }                
            }

            return isValid;
        }

        private bool IsAlphaNumeric(string strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");

            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }

        private void LogRequest(string referrer, string customerId, string requestPage)
        {
            string message = "Request for page " + requestPage + " received";
            Logger.CreateLog(customerId, referrer, message, 1);
        }

        private CustomerModel LoadCustomer()
        {
            CustomerController controller = new CustomerController();

            return controller.GetCustomer(System.Convert.ToInt32(CustomerId));
        }

        private PageModel LoadPage()
        {
            PageController controller = new PageController();

            PageModel pageModel = null;

            if(IsValidPage())
            {
                pageModel = controller.LoadPageByName(RequestPage);
            }
            else if(IsStartPage())
            {
                pageModel = controller.LoadPageByStartPage();
            }

            return pageModel;
        }

        private void LoadFeed()
        {
            CustomerFeedLabel.Text = ParsePageFeed(page);
        }

        private string ParsePageFeed(PageModel pageModel)
        {
            return ParseFeed(pageModel.Content);
        }

        //This parses out the keywords and replaces them with the dynamic data
        private string ParseFeed(string content)
        {
            ReplacerController controller = new ReplacerController();

            List<ReplacerModel> replacerModels = controller.LoadReplacers();

            StringBuilder builder = new StringBuilder(content);

            State state = new State();

            foreach(ReplacerModel replacerModel in replacerModels)
            {
                try
                {                   
                    PropertyInfo property = typeof(CustomerModel).GetProperty(replacerModel.DataField);

                    builder.Replace(replacerModel.Keyword, System.Convert.ToString(property.GetValue(customer, null)));    
                }
                catch (Exception ex)
                {
                    Logger.CreateLog(CustomerId, Referrer, "Parsing of content failed. " + ex.InnerException, 1);
                    state.Cache().SetError(ex);
                    Response.Redirect("Error.aspx");
                }                               
            }
                   
            return builder.ToString();
        }

        private void ValidateRequest()
        {
            int result;
            bool isNumber = int.TryParse(CustomerId, out result);

            if(ReferrerValid())
            {
                if (String.IsNullOrEmpty(CustomerId) || !isNumber)
                {
                    ValidRequest = false;
                    DisplayInvalidRequest(null);
                }
                else
                {
                    ValidRequest = true;
                }   
            }
            else
            {
                DisplayInvalidRequest(Referrer);
            }
            
        }

        private bool IsCustomerActive()
        {
            bool valid = false;

            int subscriptionType = Convert.ToInt32(customer.SubscriptionType);
            int accountStatus = Convert.ToInt32(customer.AccountStatus);

            if(accountStatus == 1) //if the account is active
            {
                if(subscriptionType == 1 || subscriptionType == 4) //if the account is free or unlimited
                {
                    valid = true;
                }
                else if(subscriptionType == 2 || subscriptionType == 3) //if the account is not free or unlimited
                {
                    if (DateTime.Now <= DateTime.Parse(customer.ExpiryDate)) //if the account date is not expired
                    {
                        valid = true;
                    }
                }
            }

            return valid;
        }

        private bool ReferrerValid()
        {
            bool valid = false;

            ReferrerController controller = new ReferrerController();
            List<ReferrerModel> referrers = controller.GetAllReferrers();

            foreach (ReferrerModel referrer in referrers)
            {
                if(Referrer == referrer.ReferrerUrl)
                {
                    valid = true;
                }
            }

            return valid;
        }

        private void DisplayInvalidRequest(string referrerUrl)
        {
            string invalidText = "";

            if(!String.IsNullOrEmpty(referrerUrl))
            {
                invalidText = "Referral from " + referrerUrl + " is invalid. Please see your administrator.";
            }
            else
            {
                invalidText = "Invalid request.  Please see your administrator.";
            }

            CustomerFeedLabel.Text = invalidText;
        }
    }
}
