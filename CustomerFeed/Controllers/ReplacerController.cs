using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using CustomerFeed.Models;
using CustomerFeedDataAccess;

namespace CustomerFeed.Controllers
{
    public class ReplacerController
    {
        public void SaveReplacer(ReplacerModel user)
        {
            Dictionary<string, DataAccessType> replacerDictionary = new Dictionary<string, DataAccessType>();

            replacerDictionary.Add("Id", new DataAccessType(user.Id.ToString(), typeof(string)));
            replacerDictionary.Add("Keyword", new DataAccessType(user.Keyword, typeof (string)));
            replacerDictionary.Add("Datafield", new DataAccessType(user.DataField, typeof (string)));

            DataAccess.Save(new ReplacerModel().DataProvider, new ReplacerModel().KeyField, "replacers", replacerDictionary);
        }

        internal List<ReplacerModel> LoadReplacers()
        {
            List<ReplacerModel> replacers = new List<ReplacerModel>();

            DataSet dataSet = DataAccess.GetAll(new ReplacerModel().DataProvider, "replacers");

            foreach (DataRow row in dataSet.Tables["replacers"].Rows)
            {
                replacers.Add(FillReplacer(row));
            }

            return replacers;
        }

        public void DeleteReplacer(ReplacerModel replacerModel)
        {
            DataAccess.Delete(new ReplacerModel().DataProvider, "Replacers", new KeyValuePair<string, string>("Id", replacerModel.Id.ToString()));
        }

        private ReplacerModel FillReplacer(DataRow row)
        {
            ReplacerModel replacer = new ReplacerModel();

            replacer.Id = (Int64)row["Id"];
            replacer.Keyword = (string)row["Keyword"];
            replacer.DataField = (string)row["DataField"];
           
            return replacer;
        }
    }
}
