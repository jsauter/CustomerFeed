using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace CustomerFeedDataAccess
{
    public class MySqlDataProvider : IDataProvider
    {
        private string connectionString = ConfigurationManager.AppSettings["connectionString"];

        private MySqlConnection sqlConnection;
        
        public MySqlDataProvider()
        {
            sqlConnection = new MySqlConnection(connectionString);
        }

        public void Delete(string tableName, KeyValuePair<string, string> key)
        {
            string sql = "delete from " + tableName + " where " + key.Key + "=@" + key.Key;

            sqlConnection.Open();

            MySqlCommand command = new MySqlCommand(sql, sqlConnection);

            command.Prepare();

            command.Parameters.AddWithValue("@" + key.Key, key.Value);

            command.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public void DeleteAll(string tableName)
        {
            string sql = "delete from " + tableName;

            sqlConnection.Open();

            MySqlCommand command = new MySqlCommand(sql, sqlConnection);

            command.Prepare();

            command.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public DataSet GetAll(string tableName)
        {
            string sql = "select * from " + tableName;

            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, sqlConnection);

            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet, tableName);

            sqlConnection.Close();

            return dataSet;
        }

        public DataSet GetSingleByMultipleParameter(string tableName, List<DataParameter> parameters)
        {
            string sql = "select * from " + tableName + " where ";
            StringBuilder builder = new StringBuilder(sql);

            foreach (DataParameter parameter in parameters)
            {
                builder.Append(parameter.Key + "='" + parameter.Value + "' ");

                if (parameter.Operator != "")
                {
                    builder.Append(" " + parameter.Operator + " ");
                }
            }

            MySqlDataAdapter adapter = new MySqlDataAdapter(builder.ToString(), sqlConnection);

            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet, tableName);

            sqlConnection.Close();

            return dataSet;
        }

        public DataSet GetSingleByParameter(string tableName, KeyValuePair<string, string> key)
        {
            string sql = "select * from " + tableName + " where " + key.Key + "='" + key.Value + "'";

            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, sqlConnection);

            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet, tableName);

            sqlConnection.Close();

            return dataSet;
        }

        public int Save(string tableName, string keyField, Dictionary<string, DataAccessType> parameterDictionary)
        {

            int returnId = 0;
            string sql = "";
            StringBuilder stringBuilder = new StringBuilder(sql);
            
            sqlConnection.Open();
                
            //insert
            if ((string)parameterDictionary[keyField].Data == "" || (string)parameterDictionary[keyField].Data == "0" || (string)parameterDictionary[keyField].Data == null)
            {
                List<string> columnList = new List<string>();
                List<string> valueList = new List<string>();

                foreach (KeyValuePair<string, DataAccessType> pair in parameterDictionary)
                {
                    if (pair.Key != keyField)
                    {
                        columnList.Add(pair.Key);
                        valueList.Add("@" + pair.Key);
                    }
                }

                stringBuilder.Append("insert into " + tableName + " (");
                stringBuilder.Append(string.Join(",", columnList.ToArray()) + ") values(");
                stringBuilder.Append(string.Join(",", valueList.ToArray()) + ")");
                stringBuilder.Append("; SELECT last_insert_id()");

                MySqlCommand command = new MySqlCommand(stringBuilder.ToString(), sqlConnection);

                command.Prepare();

                foreach (KeyValuePair<string, DataAccessType> pair in parameterDictionary)
                {
                    if (pair.Key != keyField)
                    {
                        command.Parameters.AddWithValue("@" + pair.Key, pair.Value.Data);
                    }
                }

                object response = command.ExecuteScalar();

                return System.Convert.ToInt32(response);
            }
            else //update
            {
                returnId = Convert.ToInt32(parameterDictionary[keyField].Data);

                List<string> updateColumns = new List<string>();

                foreach (KeyValuePair<string, DataAccessType> pair in parameterDictionary)
                {
                    if (pair.Value.Data != "")
                    {
                        updateColumns.Add(pair.Key + "=@" + pair.Key);
                    }

                }

                string trimmedColumns = string.Join(",", updateColumns.ToArray());

                stringBuilder.Append("update " + tableName + " ");
                stringBuilder.Append("set " + trimmedColumns + " ");
                stringBuilder.Append("where " + keyField + "=" + parameterDictionary[keyField].Data);

                MySqlCommand command = new MySqlCommand(stringBuilder.ToString(), sqlConnection);

                command.Prepare();

                foreach (KeyValuePair<string, DataAccessType> pair in parameterDictionary)
                {
                    if (pair.Value.Data != "")
                    {
                        command.Parameters.AddWithValue("@" + pair.Key, pair.Value.Data);
                    }
                }

                command.ExecuteNonQuery();
            }

            sqlConnection.Close();

            return returnId;
        }
    }
}
