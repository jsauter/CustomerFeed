using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace CustomerFeed
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["Test"] == null)
            {
                if (Request.Path.EndsWith("/Customer.aspx"))
                {
                    if (!Request.IsSecureConnection)
                    {
                        Response.Redirect(Request.Url.ToString().Replace("http://", "https://"));
                    }
                }
                else
                {
                    {
                        if (Request.IsSecureConnection)
                        {
                            Response.Redirect(Request.Url.ToString().Replace("https://", "http://"));
                        }
                    }
                }    
            }            
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}