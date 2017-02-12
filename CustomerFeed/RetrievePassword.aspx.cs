using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomerFeed.Controllers;
using CustomerFeed.Models;

namespace CustomerFeed
{
    public partial class RetrievePassword : System.Web.UI.Page
    {
        private string reset = "";
        private Guid resetId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(ValidResetRequest())
                {
                    StartRetrievePanel.Visible = false;
                    ResetPasswordPanel.Visible = true;
                }
            }
        }

        private bool ValidResetRequest()
        {
            try
            {
                string resetIdString = Request.QueryString["ResetId"];
                reset = Request.QueryString["Reset"];

                if (!string.IsNullOrEmpty(reset))
                {
                    int result;
                    bool isNumber = int.TryParse(reset, out result);

                    if (isNumber && result == 1)
                    {
                        resetId = new Guid(resetIdString);
                        ViewState["ResetId"] = resetId.ToString();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        protected void StartRetrieveSubmitButton_Click(object sender, EventArgs e)
        {
            
            UserController controller = new UserController();

            UserModel userModel = new UserModel();

            userModel.UserName = UserNameTextBox.Text;
            userModel.Email = UserEmailTextBox.Text;

            UserModel retrievedUser = controller.LoadUserByNameAndEmail(userModel);

            Logger.CreateLog("Password reset request received for user " + userModel.UserName, 1);

            if (retrievedUser == null)
            {
                ResponseInformationLabel.Text = "Invalid User Information, please re-enter";
                ResponseInformationLabel.Visible = true;
            }
            else
            {
                ProcessPasswordResetRequest(retrievedUser);

                ResponseInformationLabel.Text = "Reset password email sent to the address you specified at registration.";
                ResponseInformationLabel.Visible = true;
            }
            
        }

        protected void ResetPasswordSubmitButton_Click(object sender, EventArgs e)
        {
            ProcessPasswordReset();
        }

        private void ProcessPasswordResetRequest(UserModel user)
        {
            PasswordResetController passwordResetController = new PasswordResetController();

            PasswordResetModel passwordResetModel = new PasswordResetModel();

            passwordResetModel.PasswordResetKey = Guid.NewGuid();
            passwordResetModel.UserId = user.Id;
            passwordResetModel.Completed = false;

            passwordResetModel.Id = passwordResetController.SavePasswordReset(passwordResetModel);

            Mailer.MailUserPasswordResetRequest(passwordResetModel, user);           
        }

        private void ProcessPasswordReset()
        {
            if(ViewState["ResetId"].ToString() != "")
            {
                PasswordResetController controller = new PasswordResetController();

                PasswordResetModel passwordResetModel = controller.LoadPasswordResetByKey(new Guid(ViewState["ResetId"].ToString()));    

                if(passwordResetModel != null && passwordResetModel.Completed == false)
                {
                    UserController userController = new UserController();

                    UserModel userModel = userController.LoadUser(passwordResetModel.UserId.ToString());

                    userModel.CreateOrReset = true;

                    userModel.Password = ReEnterPasswordTextBox.Text;

                    userController.SaveUser(userModel);

                    passwordResetModel.Completed = true;

                    controller.SavePasswordReset(passwordResetModel);

                    Logger.CreateLog("Password reset done for user " + userModel.UserName, 1);

                    PasswordResetLabel.Text =
                       "Password reset.  Please click <a href='Default.aspx'>here</a> to login.";
                    ResetPasswordPanel.Visible = false;
                }
                else
                {
                    PasswordResetLabel.Text =
                        "Password reset code already used.  Please create a <a href='RetrievePassword.aspx'>new</a> reset request.";
                    ResetPasswordPanel.Visible = false;
                }
            }            
        }
    }
}
