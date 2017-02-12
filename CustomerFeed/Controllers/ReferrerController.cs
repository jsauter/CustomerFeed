using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using CustomerFeed.Models;
using CustomerFeedDataAccess;

namespace CustomerFeed.Controllers
{
    public class ReferrerController
    {
        public ReferrerModel GetReferrer(int id)
        {
            DataSet dataSet = DataAccess.GetSingleByParameter(new ReferrerModel().DataProvider, "Referrers", new KeyValuePair<string, string>("Id", id.ToString()));

            return FillReferrer(dataSet.Tables["Referrers"].Rows[0]);
        }

        public List<ReferrerModel> GetAllReferrers()
        {
            List<ReferrerModel> referrers = new List<ReferrerModel>();

            DataSet dataSet = DataAccess.GetAll(new ReferrerModel().DataProvider, "Referrers");

            foreach (DataRow row in dataSet.Tables["Referrers"].Rows)
            {
                referrers.Add(FillReferrer(row));
            }

            return referrers;
        }

        public void SaveReferrer(ReferrerModel referrerModel)
        {
            Dictionary<string, DataAccessType> customerDictionary = new Dictionary<string, DataAccessType>();

            customerDictionary.Add("Id", new DataAccessType(referrerModel.Id.ToString(), typeof(string)));
            customerDictionary.Add("ReferrerUrl", new DataAccessType(referrerModel.ReferrerUrl, typeof(string)));
            
            DataAccess.Save(new ReferrerModel().DataProvider, new ReferrerModel().KeyField, "Referrers", customerDictionary);
        }

        public void DeleteReferrer(ReferrerModel referrerModel)
        {
            DataAccess.Delete(new ReferrerModel().DataProvider, "Referrers", new KeyValuePair<string, string>("Id", referrerModel.Id.ToString()));
        }

        private ReferrerModel FillReferrer(DataRow row)
        {
            ReferrerModel model = new ReferrerModel();

            model.Id = (Int64)row["Id"];
            model.ReferrerUrl = (string) row["ReferrerUrl"];
            
            return model;
        }
    }
}
