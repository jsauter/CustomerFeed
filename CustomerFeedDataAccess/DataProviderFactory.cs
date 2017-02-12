using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerFeedDataAccess
{
    public enum DataProviderType
    {
        MySql,
        MSSql
    }

    public class DataProviderFactory
    {
        public static IDataProvider GetDataProvider(DataProviderType dataProviderType)
        {
            if(dataProviderType == DataProviderType.MSSql)
            {
                return new MSSqlDataProvider();
            }
            if(dataProviderType == DataProviderType.MySql)
            {
                return new MySqlDataProvider();
            }

            return null;
        }
    }
}
