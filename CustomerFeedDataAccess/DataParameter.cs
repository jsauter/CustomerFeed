using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerFeedDataAccess
{
    public class DataParameter
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Operator { get; set; }

        public DataParameter()
        {
            Key = "";
            Value = "";
            Operator = "";
        }

        public DataParameter(string key, string value, string op)
        {
            Key = key;
            Value = value;
            Operator = op;
        }
    }
}
