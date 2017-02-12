using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerFeedDataAccess
{
    public class DataAccessType
    {
        public object Data { get; set; }
        public Type Type { get; set; }

        public DataAccessType(object data, Type type)
        {
            Data = data;
            Type = type;
        }
    }
}
