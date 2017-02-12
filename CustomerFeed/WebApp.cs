using System;
using System.Collections.Generic;
using System.Web;
using CustomerFeed.Models;

namespace CustomerFeed
{
    public class WebApp : System.Web.UI.Page
    {
        protected bool NeedsAdministrator;
       
        protected void ValidateSecurity()
        {
            State state = new State();

            if(!state.Cache().IsLoggedIn())
            {
                 SendToLogin();
            }

            if(NeedsAdministrator && !state.Cache().IsAdminstrator())
            {
                SendToLogin();
            }
        }     

        protected void SendToHomeScreen()
        {
            State state = new State();

            if(state.Cache().IsLoggedIn())
            {
                if (state.Cache().IsAdminstrator())
                {
                    Response.Redirect("Administration.aspx");
                }
                else
                {
                    Response.Redirect("Customer.aspx?ID=" + state.Cache().GetUser().CustomerId);
                }    
            }
            else
            {
                SendToLogin();
            }
            
        }

        protected void SendToLogin()
        {
            Response.Redirect("Default.aspx");
        }

        protected void SendToInvalidRequest()
        {
            Response.Redirect("InvalidRequest.aspx");
        }

        protected void SendToThankYou(string userName, string email)
        {
            Response.Redirect("ThankYou.aspx?UserName=" + userName + "&Email=" + email);
        }        
    }
}
