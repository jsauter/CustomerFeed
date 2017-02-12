using System;
using System.Collections.Generic;
using System.Web;

namespace CustomerFeed.Models
{
    public class UserModel : ModelBase, IEntity
    {
        public bool CreateOrReset;
        public Int64 Id { get; set; }
        private string userName;
        public string UserName
        {
            get
            {
                return userName.ToLower();
            }
            set
            {
                userName = value.ToLower();
            }
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Administrator { get; set; }
        public bool IsCustomer { get; set; }
        public int CustomerId { get; set; }
        public string PasswordHint { get; set; }
        public string PasswordHintAnswer { get; set; }

        public UserModel()
        {
            CreateOrReset = false;
        }

        public UserModel(string userId, string password)
        {
            UserName = userId;
            Password = password;
        }
    }
}
