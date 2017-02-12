using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomerFeed.Controllers;
using CustomerFeed.Models;

namespace CustomerFeed
{
    public partial class Register : WebApp
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitButtonClick(object sender, EventArgs e)
        {
            recaptcha.Validate();

            if(Page.IsValid)
            {
                UserModel user = new UserModel();
                ForumUserModel forumUser = new ForumUserModel();

                user.CreateOrReset = true;

                user.UserName = UserNameTextBox.Text;
                user.Email = EmailTextBox.Text;
                user.Password = PasswordReenterTextBox.Text;

                //default, we make users not administrators but they are customers.
                user.Administrator = false;
                user.IsCustomer = true;

                forumUser.UserName = UserNameTextBox.Text;
                forumUser.Email = EmailTextBox.Text;
                forumUser.Password = "";

                UserController controller = new UserController();

                if (!controller.IsExistingUser(user))
                {
                    controller.SaveUser(user);

                    ForumUserController forumUserController = new ForumUserController();
                    forumUserController.CreateForumUser(forumUser);

                    MailUserCreationToUser(user, PasswordReenterTextBox.Text);
                    MailUserCreationToSupportMessage(user);
                    SendToThankYou(user.UserName, user.Email);
                }
                else
                {
                    UserNameNotAvailableLabel.Visible = true;
                }    
            }            
        }

        private void MailUserCreationToUser(UserModel user, string password)
        {
            string to = user.Email;
            string body = ConfigurationManager.AppSettings["newUserWelcomeBody"];
            string subject = ConfigurationManager.AppSettings["newUserWelcomeSubject"];

            Mailer.MailMessage(to, subject, string.Format(body, user.UserName, user.Email, password));
        }

        private void MailUserCreationToSupportMessage(UserModel user)
        {
            string to = ConfigurationManager.AppSettings["notificationEmail"];
            string body = ConfigurationManager.AppSettings["newUserNotificationBody"];
            string subject = "User created";

            Mailer.MailMessage(to, subject, string.Format(body, user.UserName));
        }
    }
}
