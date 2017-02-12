using System;
using System.Collections.Generic;
using System.Web;

namespace CustomerFeed.Models
{
    public class PasswordResetModel : ModelBase, IEntity
    {
        public Int64 Id { get; set; }
        public Int64 UserId { get; set; }
        public Guid PasswordResetKey { get; set; }
        public bool Completed { get; set; }
    }
}
