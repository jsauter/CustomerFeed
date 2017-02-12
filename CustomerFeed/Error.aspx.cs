using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CustomerFeed
{
    public partial class Error : WebApp
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                SetErrorLabel();
            }
        }

        private void SetErrorLabel()
        {
            State state = new State();

            if(state.Cache().GetError() != null)
            {
                ErrorLabel.Text = state.Cache().GetError().ToString();
            }
        }

        protected void ReturnHomeLinkButton_Click(object sender, EventArgs e)
        {
            SendToHomeScreen();
        }
    }
}
