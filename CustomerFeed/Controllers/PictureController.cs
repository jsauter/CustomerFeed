using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using CustomerFeed.Models;
using CustomerFeedDataAccess;

namespace CustomerFeed.Controllers
{
    public class PictureController
    {
        public PictureModel GetPicture(Int64 id)
        {
            DataSet dataSet = DataAccess.GetSingleByParameter(new PictureModel().DataProvider, "pictures", new KeyValuePair<string, string>("Id", id.ToString()));

            if (dataSet.Tables["pictures"].Rows.Count != 0)
            {
                return FillPicture(dataSet.Tables["pictures"].Rows[0]);
            }

            return null;
        }

        public List<PictureModel> GetAllPictures()
        {
            List<PictureModel> pictures = new List<PictureModel>();

            DataSet dataSet = DataAccess.GetAll(new PictureModel().DataProvider, "pictures");

            foreach (DataRow row in dataSet.Tables["pictures"].Rows)
            {
                pictures.Add(FillPicture(row));
            }

            return pictures;
        }

        public int SavePicture(PictureModel picture)
        {
            Dictionary<string, DataAccessType> pictureDictionary = new Dictionary<string, DataAccessType>();

            pictureDictionary.Add("Id", new DataAccessType(picture.Id.ToString(), typeof(string)));
            pictureDictionary.Add("Data", new DataAccessType(picture.Data, typeof(byte[])));
            pictureDictionary.Add("Size", new DataAccessType(picture.Size, typeof(int)));
            pictureDictionary.Add("Name", new DataAccessType(picture.Name, typeof(string)));

            return DataAccess.Save(new PictureModel().DataProvider, new PictureModel().KeyField, "pictures", pictureDictionary);
        }

        private PictureModel FillPicture(DataRow row)
        {
            PictureModel model = new PictureModel();

            model.Id = (Int64)row["Id"];
            model.Name = row["Name"] == DBNull.Value ? "" : (string)row["Name"];
            model.Size = row["Size"] == DBNull.Value ? 0 : (int)row["Size"];
            model.Data = row["Data"] == DBNull.Value ? null : (byte[])row["Data"];
        
            return model;
        }

        internal void DeletePicture(PictureModel pictureModel)
        {
            DataAccess.Delete(new PictureModel().DataProvider, "pictures", new KeyValuePair<string, string>("Id", pictureModel.Id.ToString()));
        }
    }
}
