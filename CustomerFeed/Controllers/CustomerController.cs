using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using CustomerFeedDataAccess;
using CustomerFeed.Models;

namespace CustomerFeed.Controllers
{
    public class CustomerController
    {
        public CustomerModel GetCustomer(int id)
        {
            DataSet dataSet = DataAccess.GetSingleByParameter(new CustomerModel().DataProvider, "Customers", new KeyValuePair<string, string>("Id", id.ToString()));

            if (dataSet.Tables["Customers"].Rows.Count != 0)
            {
                return FillCustomer(dataSet.Tables["Customers"].Rows[0]);    
            }

            return null;
        }
        
        public List<CustomerModel> GetAllCustomers()
        {
            List<CustomerModel> customers = new List<CustomerModel>();

            DataSet dataSet = DataAccess.GetAll(new CustomerModel().DataProvider, "Customers");

            foreach(DataRow row in dataSet.Tables["Customers"].Rows)
            {
                customers.Add(FillCustomer(row));
            }

            return customers;
        }

        public int SaveCustomer(CustomerModel customer)
        {
            Dictionary<string, DataAccessType> customerDictionary = new Dictionary<string, DataAccessType>();

            customerDictionary.Add("Id", new DataAccessType(customer.Id.ToString(), typeof(string)));
            customerDictionary.Add("WebOfficeUserName", new DataAccessType(customer.WebOfficeUserName, typeof(string)));
            customerDictionary.Add("FirstName", new DataAccessType(customer.FirstName, typeof(string)));
            customerDictionary.Add("LastName", new DataAccessType(customer.LastName, typeof(string)));
            customerDictionary.Add("Email", new DataAccessType(customer.Email, typeof(string)));
            customerDictionary.Add("Phone", new DataAccessType(customer.PhoneNumber,typeof(string)));
            customerDictionary.Add("StreetAddress1", new DataAccessType(customer.StreetNumber1, typeof(string)));
            customerDictionary.Add("StreetAddress2", new DataAccessType(customer.StreetNumber2,typeof(string)));
            customerDictionary.Add("City", new DataAccessType(customer.City,typeof(string)));
            customerDictionary.Add("Province", new DataAccessType(customer.Province,typeof(string)));
            customerDictionary.Add("PostalCode", new DataAccessType(customer.PostalCode,typeof(string)));
            customerDictionary.Add("PreferredLanguage", new DataAccessType(customer.PreferredLanguage,typeof(string)));
            customerDictionary.Add("TupperwareLevel", new DataAccessType(customer.TupperwareLevel,typeof(string)));
            customerDictionary.Add("Referrer", new DataAccessType(customer.Referrer,typeof(string)));
            customerDictionary.Add("AccountStatus", new DataAccessType(customer.AccountStatus,typeof(string)));
            customerDictionary.Add("ExpiryDate", new DataAccessType(customer.ExpiryDate,typeof(string)));
            customerDictionary.Add("SubscriptionType", new DataAccessType(customer.SubscriptionType,typeof(string)));
            customerDictionary.Add("HtmlContent", new DataAccessType(customer.HtmlContent,typeof(string)));
            customerDictionary.Add("Profile", new DataAccessType(customer.Profile,typeof(string)));       

            if(customer.Picture.Data != null && customer.PictureChaged)
            {
                PictureController controller = new PictureController();
                customer.PictureId = controller.SavePicture(customer.Picture);               
            }

            customerDictionary.Add("PictureId", new DataAccessType(customer.PictureId, typeof(Int64)));

            return DataAccess.Save(new CustomerModel().DataProvider, new CustomerModel().KeyField, "Customers", customerDictionary);
        }

        private CustomerModel FillCustomer(DataRow row)
        {
            CustomerModel model = new CustomerModel();

            model.Id = (Int64)row["Id"];
            model.WebOfficeUserName = row["WebOfficeUserName"] == DBNull.Value ? "" : (string)row["WebOfficeUserName"];
            model.FirstName = row["FirstName"] == DBNull.Value ? "" : (string)row["FirstName"];
            model.LastName = row["LastName"] == DBNull.Value ? "" : (string)row["LastName"];
            model.Email = row["Email"] == DBNull.Value ? "" : (string)row["Email"];
            model.PhoneNumber = row["Phone"] == DBNull.Value ? "" : (string)row["Phone"];
            model.StreetNumber1 = row["StreetAddress1"] == DBNull.Value ? "" : (string)row["StreetAddress1"];
            model.StreetNumber2 = row["StreetAddress2"] == DBNull.Value ? "" : (string)row["StreetAddress2"];
            model.City = row["City"] == DBNull.Value ? "" : (string)row["City"];
            model.Province = row["Province"] == DBNull.Value ? "" : (string)row["Province"];
            model.PostalCode = row["PostalCode"] == DBNull.Value ? "" : (string)row["PostalCode"];
            model.PreferredLanguage = row["PreferredLanguage"] == DBNull.Value ? "English" : (string)row["PreferredLanguage"];
            model.TupperwareLevel = row["TupperwareLevel"] == DBNull.Value ? "0" : ((int)row["TupperwareLevel"]).ToString();
            model.Referrer = row["Referrer"] == DBNull.Value ? "" : (string)row["Referrer"];
            model.AccountStatus = row["AccountStatus"] == DBNull.Value ? "" : ((int)row["AccountStatus"]).ToString();
            model.ExpiryDate = row["ExpiryDate"] == DBNull.Value ? "" : ((DateTime)row["ExpiryDate"]).ToShortDateString();
            model.SubscriptionType = row["SubscriptionType"] == DBNull.Value ? "" : ((int)row["SubscriptionType"]).ToString();
            model.HtmlContent = row["HtmlContent"] == DBNull.Value ? "" : (string)row["HtmlContent"];
            model.Profile = row["Profile"] == DBNull.Value ? "" : (string)row["Profile"];
            model.PictureId = row["PictureId"] == DBNull.Value ? 0 : (Int64)row["PictureId"];

            return model;
        }
    }
}
