using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;


namespace CustomerFeedDataAccess
{

    public class DataAccess
    {

        public static DataSet GetAll(DataProviderType providerType, string tableName)
        {
            IDataProvider provider = GetDataProvider(providerType);

            return provider.GetAll(tableName);
        }

        public static DataSet GetSingleByParameter(DataProviderType providerType, string tableName, KeyValuePair<string, string> key)
        {
            IDataProvider provider = GetDataProvider(providerType);

            return provider.GetSingleByParameter(tableName, key);
        }


        public static DataSet GetSingleByMultipleParameter(DataProviderType providerType, string tableName, List<DataParameter> parameters)
        {
            IDataProvider provider = GetDataProvider(providerType);

            return provider.GetSingleByMultipleParameter(tableName, parameters);
        }

        public static void Delete(DataProviderType providerType, string tableName, KeyValuePair<string, string> key)
        {
            IDataProvider provider = GetDataProvider(providerType);

            provider.Delete(tableName, key);
        }

        public static int Save(DataProviderType providerType, string keyField, string tableName, Dictionary<string, DataAccessType> parameterDictionary)
        {
            IDataProvider provider = GetDataProvider(providerType);

            return provider.Save(tableName, keyField, parameterDictionary);
        }

        public static void DeleteAll(DataProviderType providerType, string tableName)
        {
            IDataProvider provider = GetDataProvider(providerType);

            provider.DeleteAll(tableName);
        }

        private static IDataProvider GetDataProvider(DataProviderType type)
        {
            return DataProviderFactory.GetDataProvider(type);
        }
    }
}
