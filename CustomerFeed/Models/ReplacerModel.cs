using System;
using System.Collections.Generic;
using System.Web;

namespace CustomerFeed.Models
{
    public class ReplacerModel : ModelBase, IEntity
    {
        public Int64 Id { get; set; }
        public string Keyword { get; set; }
        public string DataField { get; set; }
    }
}
