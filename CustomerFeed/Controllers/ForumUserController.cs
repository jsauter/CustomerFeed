using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using CustomerFeed.Models;
using CustomerFeedDataAccess;

namespace CustomerFeed.Controllers
{
    public class ForumUserController
    {
        public bool CreateForumUser(ForumUserModel forumUser)
        {

            Dictionary<string, DataAccessType> forumUsersDictionary = new Dictionary<string, DataAccessType>();

            forumUsersDictionary.Add("UserId", new DataAccessType(forumUser.Id.ToString(), typeof(int)));
            forumUsersDictionary.Add("UserName", new DataAccessType(forumUser.UserName, typeof(string)));
            forumUsersDictionary.Add("Password", new DataAccessType(forumUser.Password, typeof(string)));
            forumUsersDictionary.Add("Email", new DataAccessType(forumUser.Email, typeof(string)));

            DataAccess.Save(new ForumUserModel().DataProvider, new ForumUserModel().KeyField, "ForumUsers", forumUsersDictionary);

            return true;
        }

        internal ForumUserModel GetForumUserByName(string userName)
        {
            DataSet dataSet = DataAccess.GetSingleByParameter(new ForumUserModel().DataProvider, "ForumUsers", new KeyValuePair<string, string>("UserName", userName));

            if (dataSet.Tables["ForumUsers"].Rows.Count != 0)
            {
                return FillForumUser(dataSet.Tables["ForumUsers"].Rows[0]);
            }

            return null;
        }

        private ForumUserModel FillForumUser(DataRow row)
        {
            ForumUserModel model = new ForumUserModel();

            model.Id = (int) row["UserId"];
            model.UserName = (string) row["UserName"];
            model.Password = (string) row["Password"];
            model.Email = (string) row["Email"];

            return model;
        }
    }
}
