using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;
using CustomerFeed.Models;

namespace CustomerFeed
{
    public class Cache : WebApp
    {
        public void SetError(Exception ex)
        {
            if(Session["Error"] == null)
            {
                Session["Error"] = ex;
            }
        }

        public Exception GetError()
        {
            if(Session["Error"] != null)
            {
                return (Exception)Session["Error"];
            }

            return null;
        }

        public void SetUser(UserModel user, ForumUserModel forumUserModel)
        {
            if(Session["User"] == null)
            {
                Session["User"] = user; 
            }

            if(forumUserModel != null)
            {
                Session["aspnetforumUserID"] = forumUserModel.Id;
                Session["aspnetforumUserName"] = forumUserModel.UserName;
            }     
        }

        public UserModel GetUser()
        {
            if (Session["User"] != null)
            {
                return (UserModel)Session["User"];
            }

            return null;
        }

        public bool IsLoggedIn()
        {
            if (Session["User"] != null)
            {
                return true;
            }

            return false;
        }

        public bool IsAdminstrator()
        {
            if (Session["User"] != null)
            {
                UserModel user = (UserModel)Session["User"];

                return user.Administrator;
            }

            return false;

        }

        internal void EmptyState()
        {
            Session.Clear();

            //HttpCookie cookieUID = new HttpCookie("aspnetforumUID", ""); //empty value
            //cookieUID.Expires = DateTime.Now.AddYears(-1); //expires = negative value
            //HttpCookie cookiePSW = new HttpCookie("aspnetforumPSW", ""); //empty value
            //cookiePSW.Expires = DateTime.Now.AddYears(-1); //expires = negative value
            //Response.Cookies.Add(cookieUID);
            //Response.Cookies.Add(cookiePSW);

        }

        internal void RemoveUser()
        {
            Session["User"] = null;
            Session["aspnetforumUserID"] = null;
            Session["aspnetforumUserName"] = null;
        }
    }
}
