using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CustomerFeed
{
    public partial class ThankYou : WebApp
    {
        private string userName = "";
        private string email = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                userName = Request.QueryString["UserName"];
                email = Request.QueryString["Email"];

                Initialize();
            }            
        }

        private void Initialize()
        {
            State state = new State();

            UserNameLabel.Text = userName;
            EmailLabel.Text = email;
        }

        protected void ReturnHomeLinkButton_Click(object sender, EventArgs e)
        {
            SendToHomeScreen();
        }
    }
}
