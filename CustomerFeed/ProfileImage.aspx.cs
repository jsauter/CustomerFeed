using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomerFeed.Controllers;
using CustomerFeed.Models;

namespace CustomerFeed
{
    public partial class ProfileImage : System.Web.UI.Page
    {
        private Int64 ImageId = 0;
        private bool validRequest;

        protected void Page_Load(object sender, EventArgs e)
        {
            Initialize();

            if(IsValidImage())
            {
                LoadImage();
            }        
        }

        private bool IsValidImage()
        {
            return ImageId >= 0;
        }

        private void LoadImage()
        {
            PictureController controller = new PictureController();
            PictureModel model = controller.GetPicture(ImageId);

            if(model != null)
            {
                Response.ContentType = "image/GIF";
                Response.BinaryWrite(model.Data);
            }
        }

        private void Initialize()
        {
            int result;
            validRequest = Int32.TryParse(Request.QueryString["ImageId"], out result);

            if (validRequest)
            {
                ImageId = Convert.ToInt64(Request.QueryString["ImageId"]);    
            }      
        }
    }
}
