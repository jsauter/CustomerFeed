using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomerFeed.Controllers;
using CustomerFeed.Models;

namespace CustomerFeed
{
    public partial class Login : WebApp
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                State.Cache().EmptyViewState();
            }
            
        }

        protected void SubmitButtonClick(object sender, EventArgs e)
        {
            State.Cache().EmptyViewState();

            UserController controller = new UserController();

            UserModel user = new UserModel();

            user.UserName = UserNameTextBox.Text;
            user.Password = PasswordTextBox.Text;

            //Validates user, and if valid saves user to session state.
            bool isValidUser = controller.ValidateUser(user);

            if (isValidUser)
            {
                RedirectUser();
            }
        }

        private void RedirectUser()
        {
            //If we are an administrator, go to the administration page.
            if (State.Cache().IsAdminstrator())
            {
                Response.Redirect("Administration.aspx");
            }
            else //if we are not an administrator, so a customer, go to your page.
            {
                Response.Redirect("Customer.aspx?Id=" + State.Cache().GetUser().CustomerId);
            }
        }
    }
}
