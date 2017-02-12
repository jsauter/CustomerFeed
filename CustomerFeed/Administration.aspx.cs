using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomerFeed.Controllers;
using CustomerFeed.Models;
using CustomerFeed.Models.Comparers;
using Vladsm.Web.UI.WebControls;

namespace CustomerFeed
{
    public partial class Administration : WebApp
    {
        private PagedDataSource PagedDataSource;
        public int CurrentLogPage
        {
            get
            {
                // look for current page in ViewState
                object o = this.ViewState["_CurrentPage"];
                if (o == null)
                    return 0; // default page index of 0
                else
                    return (int)o;
            }

            set
            {
                this.ViewState["_CurrentPage"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PagedDataSource = new PagedDataSource();

            NeedsAdministrator = true;

            ValidateSecurity();

            if(!IsPostBack)
            {
                Initialize();
            }
            
        }

        private void Initialize()
        {            
            LoadCustomers();
            LoadReferrers();
            LoadUsers();
            LoadReplacers();
            LoadPages();
            LoadLogs();
                                 
            if (!String.IsNullOrEmpty(Request.QueryString["Show"]))
            {
                ProcessDisplayParameter(Request.QueryString["Show"]);             
            }
        }

        private void LoadReplacers()
        {
            ReplacerController replacerController = new ReplacerController();

            List<ReplacerModel> replaceModels = replacerController.LoadReplacers();

            ReplacerList.DataSource = replaceModels;
            ReplacerList.DataBind();

            List<string> possibleFields = new List<string>();
            
            //loading customer object to iterate through properties and display them
            CustomerModel model = new CustomerModel();

            PropertyInfo[] properties = model.GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                possibleFields.Add(prop.Name);
            }

            PossibleFields.Text = string.Join(", ", possibleFields.ToArray());
        }

        private void LoadUsers()
        {
            State state = new State();

            UserController userController = new UserController();

            List<UserModel> users = userController.LoadUsers();

            //We want to find the user we are logged in as, and remove it from the list
            //so we don't accidently remove administrator rights to it.
            foreach (UserModel user in users)
            {
                if(user.Id == state.Cache().GetUser().Id)
                {
                    users.Remove(user);
                    break;
                }
            }

            UsersList.DataSource = users;
            UsersList.DataBind();

        }

        private void LoadPages()
        {
            PageController pageController = new PageController();

            List<PageModel> pages = pageController.LoadPages();

            PageContentList.DataSource = pages;

            PageContentList.DataBind();
        }

        private void LoadLogs()
        {
            LogController logController = new LogController();

            List<LogModel> logs = logController.LoadLogs();

            LogTimeStampDescendingComparer timeStampSort = new LogTimeStampDescendingComparer();

            logs.Sort(timeStampSort);

            PagedDataSource.DataSource = logs;

            PagedDataSource.AllowPaging = true;

            PagedDataSource.PageSize = 20;

            PagedDataSource.CurrentPageIndex = CurrentLogPage;

            CurrentPageLabel.Text = "Page: " + (CurrentLogPage + 1).ToString() + " of "
                         + PagedDataSource.PageCount.ToString();

            //// Disable Prev or Next buttons if necessary
            CmdPrev.Enabled = !PagedDataSource.IsFirstPage;
            CmdNext.Enabled = !PagedDataSource.IsLastPage;

            LogList.DataSource = PagedDataSource;

            LogList.DataBind();
        }

        private void LoadReferrers()
        {
            ReferrerController referrerController = new ReferrerController();

            List<ReferrerModel> referrers = referrerController.GetAllReferrers();

            ReferrerList.DataSource = referrers;

            ReferrerList.DataBind();
        }

        private void LoadCustomers()
        {
            CustomerController customerController = new CustomerController();

            List<CustomerModel> customers = customerController.GetAllCustomers();

            CustomerList.DataSource = customers;

            CustomerList.DataBind();
        }

        protected void SwitchPane(object sender, CommandEventArgs e)
        {
            ProcessDisplayParameter(e.CommandArgument.ToString());
        }

        private void ShowListContent(Control controlToShow)
        {
            ReplacerPanel.Visible = false;
            CustomerList.Visible = false;
            ReferrerList.Visible = false;
            UsersList.Visible = false;
            PageContentList.Visible = false;
            LogPanel.Visible = false;

            controlToShow.Visible = true;
        }

        protected void NewReferrer(object sender, EventArgs e)
        {
            Response.Redirect("Referrer.aspx");
        }

        private void ProcessDisplayParameter(string displayParameter)
        {  
            switch (displayParameter)
            {
                case "Customers":
                    ShowListContent(CustomerList);
                    break;
                case "Referrers":
                    ShowListContent(ReferrerList);
                    break;
                case "Users":
                    ShowListContent(UsersList);
                    break;
                case "Replacers":
                    ShowListContent(ReplacerPanel);
                    break;
                case "PageContent" :
                    ShowListContent(PageContentList);
                    break;
                case "Logs" :
                    ShowListContent(LogPanel);
                    break;
            }    
        }

       
        protected void UsersList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "Save")
            {
                UserController controller = new UserController();

                UserModel user = controller.LoadUser(e.CommandArgument.ToString());

                CheckBox checkBox = (CheckBox)e.Item.FindControl("AdministratorCheckbox");

                user.Administrator = checkBox.Checked;
               
                controller.SaveUser(user);
            }
        }

        protected void ReplacerList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "Edit")
            {
                ReplacerItemShowEdit(e);
            }
            if (e.CommandName == "Save")
            {
                ReplacerItemSave(e);
                ReplacerItemShowNonEdit(e);
                Initialize();
            }
            if (e.CommandName == "Delete")
            {
                ReplacerItemDelete(e);
                Initialize();
            }
            if (e.CommandName == "Add")
            {
                ReplacerItemAdd((Repeater)source, e);
                Initialize();
            }

           
        }

        private void ReplacerItemShowEdit(RepeaterCommandEventArgs e)
        {
            e.Item.FindControl("KeywordLabel").Visible = false;
            e.Item.FindControl("KeywordTextBox").Visible = true;

            e.Item.FindControl("DataFieldLabel").Visible = false;
            e.Item.FindControl("DataFieldTextBox").Visible = true;

            e.Item.FindControl("EditLinkButton").Visible = false;
            e.Item.FindControl("SaveLinkButton").Visible = true;
        }

        private void ReplacerItemShowNonEdit(RepeaterCommandEventArgs e)
        {
            e.Item.FindControl("KeywordLabel").Visible = true;
            e.Item.FindControl("KeywordTextBox").Visible = false;

            e.Item.FindControl("DataFieldLabel").Visible = true;
            e.Item.FindControl("DataFieldTextBox").Visible = false;

            e.Item.FindControl("EditLinkButton").Visible = true;
            e.Item.FindControl("SaveLinkButton").Visible = false;
        }

        private void ReplacerItemSave(RepeaterCommandEventArgs e)
        {
            ReplacerController controller = new ReplacerController();

            Label id = (Label)e.Item.FindControl("IdLabel");
            TextBox keyword = (TextBox)e.Item.FindControl("KeywordTextBox");
            TextBox dataField = (TextBox)e.Item.FindControl("DataFieldTextBox");

            ReplacerModel replacer = new ReplacerModel();

            if(id.Text != "")
            {
                replacer.Id = System.Convert.ToInt32(id.Text);                
            }

            replacer.Keyword = keyword.Text;
            replacer.DataField = dataField.Text;

            controller.SaveReplacer(replacer);
        }

        private void ReplacerItemDelete(RepeaterCommandEventArgs e)
        {
            ReplacerController controller = new ReplacerController();

            Label id = (Label)e.Item.FindControl("IdLabel");

            ReplacerModel replacer = new ReplacerModel();

            replacer.Id = System.Convert.ToInt32(id.Text);
                 
            controller.DeleteReplacer(replacer);
        }

        private void ReplacerItemAdd(Repeater source, RepeaterCommandEventArgs e)
        {
            ReplacerController controller = new ReplacerController();

            TextBox addKeywordTextbox = (TextBox)e.Item.FindControl("AddKeywordTextbox");
            TextBox addDataFieldTextBox = (TextBox)e.Item.FindControl("AddDataFieldTextBox");

            ReplacerModel replacer = new ReplacerModel();

            replacer.Keyword = addKeywordTextbox.Text;
            replacer.DataField = addDataFieldTextBox.Text;

            controller.SaveReplacer(replacer);
        }

        protected void ReferrerList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                ReferrerItemShowEdit(e);
            }
            if (e.CommandName == "Save")
            {
                ReferrerItemSave(e);
                ReferrerItemShowNonEdit(e);
                Initialize();
            }
            if (e.CommandName == "Delete")
            {
                ReferrerItemDelete(e);
                Initialize();
            }
            if (e.CommandName == "Add")
            {
                ReferrerItemAdd((Repeater)source, e);
                Initialize();
            }
        }

        private void ReferrerItemAdd(Repeater repeater, RepeaterCommandEventArgs e)
        {
            ReferrerController controller = new ReferrerController();

            TextBox referrerUrlTextBox = (TextBox)e.Item.FindControl("AddReferrerUrlTextBox");

            ReferrerModel referrer = new ReferrerModel();

            referrer.ReferrerUrl = referrerUrlTextBox.Text;

            controller.SaveReferrer(referrer);
        }

        private void ReferrerItemDelete(RepeaterCommandEventArgs e)
        {
            ReferrerController controller = new ReferrerController();

            Label referrerIdLabel = (Label)e.Item.FindControl("ReferrerIdLabel");

            ReferrerModel referrer = new ReferrerModel();

            referrer.Id = System.Convert.ToInt64(referrerIdLabel.Text);

            controller.DeleteReferrer(referrer);
        }

        private void ReferrerItemShowNonEdit(RepeaterCommandEventArgs e)
        {
            e.Item.FindControl("ReferrerUrlLabel").Visible = true;
            e.Item.FindControl("ReferrerUrlTextBox").Visible = false;

            e.Item.FindControl("EditLinkButton").Visible = false;
            e.Item.FindControl("SaveLinkButton").Visible = true;
        }

        private void ReferrerItemSave(RepeaterCommandEventArgs e)
        {
            ReferrerController controller = new ReferrerController();

            Label id = (Label)e.Item.FindControl("ReferrerIdLabel");
            TextBox referrerUrlTextBox = (TextBox)e.Item.FindControl("ReferrerUrlTextBox");           

            ReferrerModel referrer = new ReferrerModel();

            if (id.Text != "")
            {
                referrer.Id = System.Convert.ToInt64(id.Text);
            }

            referrer.ReferrerUrl = referrerUrlTextBox.Text;

            controller.SaveReferrer(referrer);
        }

        private void ReferrerItemShowEdit(RepeaterCommandEventArgs e)
        {
            e.Item.FindControl("ReferrerUrlLabel").Visible = false;
            e.Item.FindControl("ReferrerUrlTextBox").Visible = true;

            e.Item.FindControl("EditLinkButton").Visible = false;
            e.Item.FindControl("SaveLinkButton").Visible = true;
        }

        protected void PageContentList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Repeater repeater = (Repeater) source;

            if(e.CommandName == "Save")
            {
                SavePages(repeater);
            }
            if(e.CommandName == "Edit")
            {
                EditPage(e);
            }
            if(e.CommandName == "Delete")
            {
                DeletePage(e);
            }
            if(e.CommandName == "Add")
            {
                AddPage();
            }
        }

        private void SavePages(Repeater e)
        {
            PageController pageController = new PageController();

            foreach(RepeaterItem item in e.Items)
            {
                Label redirectId = (Label)item.FindControl("IdLabel");

                PageModel pageModel = pageController.LoadPage(System.Convert.ToInt64(redirectId.Text));

                pageModel.SaveStartPage = true;

                GroupRadioButton startPageCheckBox = (GroupRadioButton)item.FindControl("StartPageRadioButton");

                pageModel.StartPage = startPageCheckBox.Checked;

                pageController.SavePage(pageModel);
            }
        }

        private void AddPage()
        {
            Response.Redirect("PageManager.aspx");
        }

        private void EditPage(RepeaterCommandEventArgs e)
        {
            Label redirectId = (Label)e.Item.FindControl("IdLabel");
            Response.Redirect("PageManager.aspx?Id=" + redirectId.Text);
        }

        private void DeletePage(RepeaterCommandEventArgs e)
        {
            Label redirectId = (Label)e.Item.FindControl("IdLabel");
            Response.Redirect("PageManager.aspx?Id=" + redirectId.Text + "&Delete=1");
        }

        protected void LogList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "Delete")
            {
                LogItemDelete(e);
            }
        }

        private void LogItemDelete(RepeaterCommandEventArgs e)
        {
            LogController controller = new LogController();

            Label idLabel = (Label)e.Item.FindControl("IdLabel");

            LogModel log = new LogModel();

            log.Id = System.Convert.ToInt64(idLabel.Text);

            controller.DeleteLog(log);

            LoadLogs();
        }

        protected void LogListNavigation(object sender, CommandEventArgs e)
        {
            if(e.CommandArgument.ToString() == "Next")
            {
                // Set viewstate variable to the previous page
                CurrentLogPage += 1;
            }

            if(e.CommandArgument.ToString() == "Previous")
            {
                // Set viewstate variable to the previous page
                CurrentLogPage -= 1;
            }
         
            // Reload control
            LoadLogs();
        }

        protected void LogRefresh(object sender, CommandEventArgs e)
        {
            if(e.CommandArgument.ToString() == "Refresh")
            {
                LoadLogs();
            }

        }

        protected void LogDelete(object sender, CommandEventArgs e)
        {
            if(e.CommandArgument.ToString() == "DeleteAll")
            {
                LogController logController = new LogController();

                logController.DeleteAllLogs();

                LoadLogs();
            }
        }
        
    }
}
