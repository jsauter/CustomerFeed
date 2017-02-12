using System;
using System.Collections.Generic;
using System.Web;
using CustomerFeedDataAccess;

namespace CustomerFeed.Models
{
    public class ForumUserModel : ModelBase, IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ForumUserModel()
        {
            KeyField = "UserId";
            DataProvider = DataProviderType.MSSql;
        }
    }
}
