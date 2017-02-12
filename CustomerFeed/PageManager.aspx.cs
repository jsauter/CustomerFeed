using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomerFeed.Controllers;
using CustomerFeed.Models;

namespace CustomerFeed
{
    public partial class PageManager : WebApp
    {
        private bool Delete = false;
        private int PageId;
        private PageModel Page;

        protected void Page_Load(object sender, EventArgs e)
        {
            NeedsAdministrator = true;

            if(!IsPostBack)
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            RunValidation();
           
            if (Delete)
            {
                LoadPage();

                DeleteConfirmationLabel.Text = "Clicking Yes will delete page " + Page.Name +
                                               ". Are you sure you want to do this?";
                DeletePanel.Visible = true;
                EditPanel.Visible = false;
            }

            if(!String.IsNullOrEmpty(Request.QueryString["Id"]))
            {
                LoadPage();
            }
            else
            {
                Page = new PageModel();
            }
        }

        private void RunValidation()
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Delete"]))
            {
                if (ValidRequest(Request.QueryString["Delete"]))
                {
                    Delete = true;
                }
                else
                {
                    SendToInvalidRequest();
                }
            }

            if (!String.IsNullOrEmpty(Request.QueryString["Id"]))
            {
                if (ValidRequest(Request.QueryString["Id"]))
                {
                    PageId = Convert.ToInt32(Request.QueryString["Id"]);
                }
                else
                {
                    SendToInvalidRequest();
                }
            }
        }

        private bool ValidRequest(string parameter)
        {
            bool validRequest = false;

            validRequest = ParseParameter(parameter);
                    
            return validRequest;
        }

        bool ParseParameter(string parameter)
        {
            bool validParse = false;

            int convertedParameter;

            if (!String.IsNullOrEmpty(parameter))
            {
                if (int.TryParse(parameter, out convertedParameter))
                {
                    validParse = true;
                }
            }

            return validParse;
        }

        private long GetPageId()
        {
            RunValidation();

            return Convert.ToInt64(Request.QueryString["Id"]);
        }

        private void LoadPage()
        {
            PageNameInUseLabel.Visible = false;

            PageController pageController = new PageController();

            Page = pageController.LoadPage(PageId);

            PageIdLabel.Text = Page.Id.ToString();
            PageNameTextBox.Text = Page.Name;
            PageActiveCheckBox.Checked = Page.Active;
            HTMLEditor.Content = Page.Content;
        }

        protected void NoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Administration.aspx?Show=PageContent");
        }

        protected void YesLinkButton_Click(object sender, EventArgs e)
        {
            PageController controller = new PageController();

            PageModel pageModel = new PageModel();
            pageModel.Id = GetPageId();
            controller.DeletePage(pageModel);

            Response.Redirect("Administration.aspx?Show=PageContent");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string id = PageIdLabel.Text;
            PageController controller = new PageController();
            PageModel pageModel = new PageModel();


            if(String.IsNullOrEmpty(id)) //new page
            {
                PageModel nameCheckPage = controller.LoadPageByName(PageNameTextBox.Text);

                if(nameCheckPage == null)
                {
                    pageModel.Active = PageActiveCheckBox.Checked;
                    pageModel.Content = HTMLEditor.Content;
                    pageModel.Name = PageNameTextBox.Text;

                    PageIdLabel.Text = controller.SavePage(pageModel).ToString();
                }
                else
                {
                    PageNameInUseLabel.Text = "Page name already in use.";
                    PageNameInUseLabel.Visible = true;
                } 
            }
            else //update page
            {
                PageModel nameCheckPage = controller.LoadPageByName(PageNameTextBox.Text);
                
                pageModel.Id = System.Convert.ToInt32(id);
                pageModel.Active = PageActiveCheckBox.Checked;
                pageModel.Content = HTMLEditor.Content;
                pageModel.Name = PageNameTextBox.Text;

                if(nameCheckPage != null)
                {
                    if (nameCheckPage.Id == pageModel.Id)
                    {
                        controller.SavePage(pageModel);
                    }
                    else
                    {
                        PageNameInUseLabel.Text = "Page name already in use.";
                        PageNameInUseLabel.Visible = true;
                    }    
                }
                else
                {
                    controller.SavePage(pageModel);
                }
                   
            }                  
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Administration.aspx?Show=PageContent");
        }
    }
}
