using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CustomerFeedDataAccess
{
    public interface IDataProvider
    {
        void Delete(string tableName, KeyValuePair<string, string> key);
        void DeleteAll(string tableName);
        DataSet GetAll(string tableName);
        DataSet GetSingleByMultipleParameter(string tableName, List<DataParameter> parameters);
        DataSet GetSingleByParameter(string tableName, KeyValuePair<string, string> key);
        int Save(string tableName, string keyField, Dictionary<string, DataAccessType> parameterDictionary);
    }
}
