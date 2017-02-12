using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Security;
using CustomerFeed.Models;
using CustomerFeedDataAccess;
using System.Security.Cryptography;

namespace CustomerFeed.Controllers
{
    public class UserController
    {
        public bool IsExistingUser(UserModel user)
        {
            State state = new State();

            bool exitingUser = false;

            DataSet data = DataAccess.GetSingleByParameter(new UserModel().DataProvider, "users", new KeyValuePair<string, string>("UserName", user.UserName));

            if(data.Tables["users"].Rows.Count != 0)
            {
                UserModel loadedUser = FillUser(data.Tables["users"].Rows[0]);

                exitingUser = true;
               
                loadedUser.Password = "";

                ForumUserController forumUserController = new ForumUserController();
                ForumUserModel forumUserModel = forumUserController.GetForumUserByName(loadedUser.UserName);

                state.Cache().SetUser(loadedUser, forumUserModel);
            }

            return exitingUser;            
        }
        
        public void SaveUser(UserModel user)
        {
            
            string password = user.Password + user.UserName;
            HashAlgorithm mhash = new SHA1CryptoServiceProvider();
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] bytHash = mhash.ComputeHash(bytValue);
            mhash.Clear();
            user.Password = Convert.ToBase64String(bytHash);

            Dictionary<string, DataAccessType> userDictionary = new Dictionary<string, DataAccessType>();

            userDictionary.Add("Id", new DataAccessType(user.Id.ToString(), typeof(string)));
            userDictionary.Add("Username", new DataAccessType(user.UserName, typeof(string)));
            userDictionary.Add("Email", new DataAccessType(user.Email, typeof(string)));

            //if this is a user creation scenario, then we add the password to save
            //otherwise we dont as not to overwrite it
            if(user.CreateOrReset)
            {
                userDictionary.Add("Password", new DataAccessType(user.Password, typeof(string)));              
            }

            userDictionary.Add("Administrator", new DataAccessType(user.Administrator ? "1" : "0", typeof(string)));
            userDictionary.Add("IsCustomer", new DataAccessType(user.IsCustomer ? "1" : "0", typeof(string)));
            userDictionary.Add("CustomerId", new DataAccessType(user.CustomerId.ToString(), typeof(string)));
            userDictionary.Add("PasswordHint", new DataAccessType(user.PasswordHint, typeof(string)));
            userDictionary.Add("PasswordHintAnswer", new DataAccessType(user.PasswordHintAnswer, typeof(string)));

            DataAccess.Save(new UserModel().DataProvider, new UserModel().KeyField, "users", userDictionary);
        }
      
        internal List<UserModel> LoadUsers()
        {
            List<UserModel> users = new List<UserModel>();

            DataSet dataSet = DataAccess.GetAll(new UserModel().DataProvider, "users");

            foreach(DataRow row in dataSet.Tables["users"].Rows)
            {
                users.Add(FillUser(row));
            }

            return users;
        }

        private UserModel FillUser(DataRow row)
        {
            UserModel user = new UserModel();

            user.Id = (Int64)row["Id"];
            user.UserName = ((string) row["UserName"]);
            user.Email = (string)row["Email"];
            user.Password = (string)row["Password"];
            user.Administrator = row["Administrator"] == DBNull.Value ? false : (bool)row["Administrator"];
            user.IsCustomer = row["IsCustomer"] == DBNull.Value ? true : (bool)row["IsCustomer"];
            user.CustomerId = row["CustomerId"] == DBNull.Value ? 0 : (int)row["CustomerId"];
            user.PasswordHint = row["PasswordHint"] != DBNull.Value ? (string) row["PasswordHint"] : "";
            user.PasswordHintAnswer = row["PasswordHintAnswer"] != DBNull.Value ? (string)row["PasswordHintAnswer"] : "";
            return user;
        }

        internal UserModel LoadUser(string id)
        {
            UserModel user = new UserModel();

            KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>("Id", id);

            DataSet dataSet = DataAccess.GetSingleByParameter(new UserModel().DataProvider, "users", keyValuePair);

            user = FillUser(dataSet.Tables["users"].Rows[0]);

            return user;
        }

        internal bool ValidateUser(UserModel userModel)
        {
            string password = userModel.Password + userModel.UserName;
            HashAlgorithm mhash = new SHA1CryptoServiceProvider();
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] bytHash = mhash.ComputeHash(bytValue);
            mhash.Clear();
            userModel.Password = Convert.ToBase64String(bytHash);

            bool validUser = false;

            List<DataParameter> parameters = new List<DataParameter>();

            parameters.Add(new DataParameter("UserName", userModel.UserName, "AND"));
            parameters.Add(new DataParameter("Password", userModel.Password, ""));

            DataSet data = DataAccess.GetSingleByMultipleParameter(new UserModel().DataProvider, "users", parameters);

            if (data.Tables["users"].Rows.Count != 0)
            {
                UserModel loadedUser = FillUser(data.Tables["users"].Rows[0]);

                validUser = true;

                ForumUserController controller = new ForumUserController();

                ForumUserModel forumUserModel = controller.GetForumUserByName(loadedUser.UserName);

                SetStateUser(loadedUser, forumUserModel);
            }

            return validUser;     
        }

        public void SetStateUser(UserModel loadedUser, ForumUserModel forumUserModel)
        {
            State state = new State();

            //null out password, because we don't want that to go through the session state
            loadedUser.Password = "";

            state.Cache().SetUser(loadedUser, forumUserModel);
        }

        internal UserModel LoadUserByNameAndEmail(UserModel user)
        {
            List<DataParameter> dataParameterList = new List<DataParameter>();

            dataParameterList.Add(new DataParameter("UserName", user.UserName, "AND"));
            dataParameterList.Add(new DataParameter("Email", user.Email, ""));

            DataSet dataSet = DataAccess.GetSingleByMultipleParameter(new UserModel().DataProvider, "users", dataParameterList);

            if (dataSet.Tables["users"].Rows.Count != 0)
            {
                return FillUser(dataSet.Tables["users"].Rows[0]);
            }

            return null;
        }
    }
}
