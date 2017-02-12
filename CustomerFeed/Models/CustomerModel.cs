using System;
using System.Collections.Generic;
using System.Web;
using CustomerFeed.Controllers;

namespace CustomerFeed.Models
{
    public class CustomerModel : ModelBase, IEntity
    {
        public Int64 Id { get; set; }
        public string WebOfficeUserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetNumber1 { get; set; }
        public string StreetNumber2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string PreferredLanguage { get; set; }
        public string TupperwareLevel { get; set; }
        public string Referrer { get; set; }
        public string AccountStatus { get; set; }
        public string ExpiryDate { get; set; }
        public string SubscriptionType { get; set; }
        public string HtmlContent { get; set; }
        public string Profile { get; set; }
        public Int64 PictureId { get; set; }
        public bool PictureChaged { get; set; }

        private PictureModel picture;
        public PictureModel Picture
        {
            get
            {
                if(picture == null)
                {
                    PictureController controller = new PictureController();
                    picture = controller.GetPicture(PictureId);
                    PictureId = picture.Id;

                    return picture;
                }
                else
                {
                    //PictureId = picture.Id;
                    return picture;
                }
            }
            set
            {
                picture = value;
                PictureController controller = new PictureController();
                PictureId = controller.SavePicture(picture);
            }
        }

        public CustomerModel()
        {
            PictureChaged = false;
            picture = new PictureModel();
        }        
    }
}
