using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomerFeed.Controllers;
using CustomerFeed.Models;

namespace CustomerFeed
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
       
        protected new void Init()
        {
            State state = new State();

            state.Cache().EmptyState();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            State state = new State();

            if (state.Cache().IsLoggedIn())
            {
                if (state.Cache().GetUser().CustomerId != 0)
                {
                    CustomerController controller = new CustomerController();
                    CustomerModel customer = controller.GetCustomer(state.Cache().GetUser().CustomerId);

                    if(customer != null)
                    {
                        WelcomeBackName.Text = customer.FirstName;    
                    }
                }
                else
                {
                    WelcomeBackName.Text = state.Cache().GetUser().UserName;
                }
                

                LoginPanel.Visible = false;
                LogoutPanel.Visible = true;

                if (state.Cache().IsAdminstrator())
                {
                    AdministrationLinkButton.Visible = true;
                }
                else
                {
                    CustomerLinkButton.Visible = true;
                }
            }
            else
            {
                LoginPanel.Visible = true;
                LogoutPanel.Visible = false;
            }
        }

        protected void Logout(object sender, EventArgs e)
        {
            State state = new State();

            if(state.Cache().IsLoggedIn())
            {
                state.Logout();

                Response.Redirect("Default.aspx");    
            }            
        }

        protected void Navigate(object sender, CommandEventArgs e)
        {
            State state = new State();

            if(e.CommandArgument.ToString() == "Administration")
            {
                Response.Redirect("Administration.aspx");
            }

            if (e.CommandArgument.ToString() == "Home")
            {
                Response.Redirect("Customer.aspx?Id=" + state.Cache().GetUser().CustomerId);
            }
        }

        protected void SubmitButtonClick(object sender, EventArgs e)
        {
            State state = new State();

            state.Cache().EmptyState();

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
            else
            {
                InvalidLabel.Visible = true;
            }
        }

        private void RedirectUser()
        {
            State state = new State();

            //If we are an administrator, go to the administration page.
            if (state.Cache().IsAdminstrator())
            {
                Response.Redirect("Administration.aspx");
            }
            else //if we are not an administrator, so a customer, go to your page.
            {
                //Response.Redirect("Customer.aspx?Id=" + state.Cache().GetUser().CustomerId);
                //Response.Redirect("Default.aspx");
                Response.Redirect(Request.Url.ToString());
            }
        }

        protected void RegisterHyperlink_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}
