using System;
using System.Collections.Generic;
using System.Web;
using CustomerFeedDataAccess;

namespace CustomerFeed.Models
{
    public class ModelBase
    {
        public string KeyField { get; set; }

        public DataProviderType DataProvider { get; set; }

        public ModelBase()
        {
            KeyField = "Id";
            DataProvider = DataProviderType.MySql;
        }
    }
}
