using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using CustomerFeed.Models;
using CustomerFeedDataAccess;

namespace CustomerFeed.Controllers
{
    public class PageController
    {
        public PageModel LoadPageByName(string pageName)
        {
            DataSet dataSet = DataAccess.GetSingleByParameter(new PageModel().DataProvider, "pages", new KeyValuePair<string, string>("Name", pageName));

            if(dataSet.Tables["pages"].Rows.Count != 0)
            {
                return FillPage(dataSet.Tables["pages"].Rows[0]);  
            }

            return null;
        }

        public int SavePage(PageModel page)
        {
            Dictionary<string, DataAccessType> pageDictionary = new Dictionary<string, DataAccessType>();

            pageDictionary.Add("Id", new DataAccessType(page.Id.ToString(), typeof(string)));
            pageDictionary.Add("Name", new DataAccessType(page.Name, typeof(string)));
            pageDictionary.Add("Content", new DataAccessType(page.Content, typeof(string)));
            pageDictionary.Add("Active", new DataAccessType(page.Active ? "1" : "0", typeof(string)));

            //if we are saving this for a start page then we check this
            if(page.SaveStartPage)
            {
                pageDictionary.Add("StartPage", new DataAccessType(page.StartPage ? "1" : "0", typeof(byte[])));   
            }

            return DataAccess.Save(new PageModel().DataProvider, new PageModel().KeyField, "pages", pageDictionary);
        }

        public List<PageModel> LoadPages()
        {
            List<PageModel> pages = new List<PageModel>();

            DataSet pageDataSet = DataAccess.GetAll(new PageModel().DataProvider, "pages");

            foreach(DataRow row in pageDataSet.Tables["pages"].Rows)
            {
                pages.Add(FillPage(row));    
            }

            return pages;
        }

        public PageModel LoadPage(Int64 id)
        {
            DataSet pageDataSet = DataAccess.GetSingleByParameter(new PageModel().DataProvider, "pages", new KeyValuePair<string, string>("Id", id.ToString()));

            if(pageDataSet.Tables["pages"].Rows[0] != null)
            {
                return FillPage(pageDataSet.Tables["pages"].Rows[0]);
            }

            return null;
        }

        public PageModel FillPage(DataRow row)
        {
            PageModel page = new PageModel();

            page.Id = (Int64) row["ID"];
            page.Name = row["Name"] == DBNull.Value ? "" : (string)row["Name"];
            page.Content = row["Content"] == DBNull.Value ? "" : (string) row["Content"];
            page.Active = (bool) row["Active"];
            page.StartPage = (bool)row["StartPage"];

            return page;
        }

        internal void DeletePage(PageModel Page)
        {
            DataAccess.Delete(new PageModel().DataProvider, "pages", new KeyValuePair<string, string>("Id", Page.Id.ToString()));
        }

        internal PageModel LoadPageByStartPage()
        {
            DataSet pageDataSet = DataAccess.GetSingleByParameter(new PageModel().DataProvider, "pages",
                                                              new KeyValuePair<string, string>("StartPage", "1"));

            if(pageDataSet.Tables["pages"].Rows[0] != null)
            {
                return FillPage(pageDataSet.Tables["pages"].Rows[0]);
            }

            return null;
        }
    }
}
