using System;
using System.Collections.Generic;
using System.Web;

namespace CustomerFeed.Models
{
    public class PageModel : ModelBase, IEntity
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public bool Active { get; set; }
        public bool StartPage { get; set; }
        public bool SaveStartPage { get; set; }

        public PageModel()
        {
            SaveStartPage = false; 
        }
    }
}
