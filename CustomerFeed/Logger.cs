using System;
using System.Collections.Generic;
using System.Web;
using CustomerFeed.Controllers;
using CustomerFeed.Models;

namespace CustomerFeed
{
    public class Logger
    {
        public static void CreateLog(string customerId, string referrer, string message, int logType)
        {
            LogController logController = new LogController();

            LogModel logModel = new LogModel();

            logModel.LogType = 1;
            logModel.CustomerId = System.Convert.ToInt64(customerId);
            logModel.ReferrerUrl = referrer;
            logModel.Message = message;
            
            logController.SaveLog(logModel);            
        }

        public static void CreateLog(string message, int logType)
        {
            LogController logController = new LogController();

            LogModel logModel = new LogModel();

            logModel.LogType = logType;
            logModel.Message = message;

            logController.SaveLog(logModel);
        }
    }
}
