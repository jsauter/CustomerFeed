using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using CustomerFeed.Models;
using CustomerFeedDataAccess;

namespace CustomerFeed.Controllers
{
    public class LogController
    {
        public void SaveLog(LogModel log)
        {
            Dictionary<string, DataAccessType> logDictionary = new Dictionary<string, DataAccessType>();

            logDictionary.Add("Id", new DataAccessType(log.Id.ToString(), typeof(string)));
            logDictionary.Add("Message", new DataAccessType(log.Message, typeof(string)));
            logDictionary.Add("LogType", new DataAccessType(log.LogType.ToString(), typeof(string)));
            logDictionary.Add("ReferrerUrl", new DataAccessType(log.ReferrerUrl, typeof(string)));
            logDictionary.Add("CustomerId", new DataAccessType(log.CustomerId.ToString(), typeof(string)));

            DataAccess.Save(new LogModel().DataProvider, new LogModel().KeyField, "logs", logDictionary);
        }

        public List<LogModel> LoadLogs()
        {
            List<LogModel> logs = new List<LogModel>();

            DataSet pageDataSet = DataAccess.GetAll(new LogModel().DataProvider, "logs");

            foreach (DataRow row in pageDataSet.Tables["logs"].Rows)
            {
                logs.Add(FillLog(row));
            }

            return logs;
        }

        public LogModel LoadLog(Int32 id)
        {
            DataSet logDataSet = DataAccess.GetSingleByParameter(new LogModel().DataProvider, "logs", new KeyValuePair<string, string>("Id", id.ToString()));

            if (logDataSet.Tables["logs"].Rows[0] != null)
            {
                return FillLog(logDataSet.Tables["pages"].Rows[0]);
            }

            return null;
        }

        public LogModel FillLog(DataRow row)
        {
            LogModel log = new LogModel();

            log.Id = (Int64)row["Id"];
            log.Message = row["Message"] == DBNull.Value ? "" : (string)row["Message"];
            log.LogType = row["LogType"] == DBNull.Value ? 0 : (int)row["LogType"];
            log.ReferrerUrl = row["ReferrerUrl"] == DBNull.Value ? "" : (string)row["ReferrerUrl"];
            log.LogTimeStamp = row["LogTimeStamp"] == DBNull.Value ? new DateTime() : (DateTime) row["LogTimeStamp"];
            log.CustomerId = row["CustomerId"] == DBNull.Value ? 0 : (Int64) row["CustomerId"];

            return log;
        }

        internal void DeleteLog(LogModel log)
        {
            DataAccess.Delete(new LogModel().DataProvider,"logs", new KeyValuePair<string, string>("Id", log.Id.ToString()));
        }

        internal void DeleteAllLogs()
        {
            DataAccess.DeleteAll(new LogModel().DataProvider, "logs");
        }
    }
}
