using System;
using System.Collections.Generic;
using System.Web;

namespace CustomerFeed.Models
{
    public class PictureModel : ModelBase, IEntity
    {
        public Int64 Id { get; set; }
        public byte[] Data { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
    }
}
