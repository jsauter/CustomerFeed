using System;
using System.Collections.Generic;
using System.Web;

namespace CustomerFeed.Models
{
    public class ReferrerModel : ModelBase, IEntity
    {
        public Int64 Id { get; set; }
        public string ReferrerUrl { get; set; }
    }
}
