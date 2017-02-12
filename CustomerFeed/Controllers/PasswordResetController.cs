using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using CustomerFeed.Models;
using CustomerFeedDataAccess;

namespace CustomerFeed.Controllers
{
    public class PasswordResetController
    {
        public Int64 SavePasswordReset(PasswordResetModel passwordResetModel)
        {
            Dictionary<string, DataAccessType> passwordResetDictionary = new Dictionary<string, DataAccessType>();

            passwordResetDictionary.Add("Id", new DataAccessType(passwordResetModel.Id.ToString(), typeof(string)));
            passwordResetDictionary.Add("PasswordResetKey", new DataAccessType(passwordResetModel.PasswordResetKey.ToString(), typeof(string)));
            passwordResetDictionary.Add("UserId", new DataAccessType(passwordResetModel.UserId.ToString(), typeof(string)));
            passwordResetDictionary.Add("Completed", new DataAccessType(passwordResetModel.Completed ? "1" : "0", typeof(string)));

            return DataAccess.Save(new PasswordResetModel().DataProvider, new PasswordResetModel().KeyField, "passwordresets", passwordResetDictionary);
        }

        public List<PasswordResetModel> LoadPasswordResets()
        {
            List<PasswordResetModel> passwordResetModels = new List<PasswordResetModel>();

            DataSet pageDataSet = DataAccess.GetAll(new PasswordResetModel().DataProvider, "passwordresets");

            foreach (DataRow row in pageDataSet.Tables["passwordresets"].Rows)
            {
                passwordResetModels.Add(FillPasswordReset(row));
            }

            return passwordResetModels;
        }

        public PasswordResetModel LoadPasswordReset(Int32 id)
        {
            DataSet logDataSet = DataAccess.GetSingleByParameter(new PasswordResetModel().DataProvider, "passwordresets", new KeyValuePair<string, string>("Id", id.ToString()));
    
            if (logDataSet.Tables["passwordresets"].Rows.Count != 0)
            {
                return FillPasswordReset(logDataSet.Tables["passwordresets"].Rows[0]);
            }

            return null;
        }

        public PasswordResetModel LoadPasswordResetByKey(Guid guid)
        {
            DataSet logDataSet = DataAccess.GetSingleByParameter(new PasswordResetModel().DataProvider, "passwordresets", new KeyValuePair<string, string>("PasswordResetKey", guid.ToString()));

            if (logDataSet.Tables["passwordresets"].Rows.Count != 0)
            {
                return FillPasswordReset(logDataSet.Tables["passwordresets"].Rows[0]);
            }

            return null;
        }

        public PasswordResetModel FillPasswordReset(DataRow row)
        {
            PasswordResetModel passwordResetModel = new PasswordResetModel();

            passwordResetModel.Id = (Int64)row["Id"];
            passwordResetModel.PasswordResetKey = row["PasswordResetKey"] == DBNull.Value ? System.Guid.Empty : new Guid((string)row["PasswordResetKey"]);
            passwordResetModel.UserId = row["UserId"] == DBNull.Value ? 0 : System.Convert.ToInt64(row["UserId"]);
            passwordResetModel.Completed = row["Completed"] == DBNull.Value ? false : (bool)row["Completed"];
            
            return passwordResetModel;
        }
    }
}
