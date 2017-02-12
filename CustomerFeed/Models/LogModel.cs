using System;
using System.Collections.Generic;
using System.Web;

namespace CustomerFeed.Models
{
    public class LogModel : ModelBase, IEntity
    {
        public Int64 Id { get; set; }
        public string Message { get; set; }
        public int LogType { get; set; }
        public string ReferrerUrl { get; set; }
        public DateTime LogTimeStamp { get; set; }
        public Int64 CustomerId { get; set; }
    }
}
