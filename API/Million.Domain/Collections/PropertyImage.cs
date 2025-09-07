using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Domain.Collections
{
    public class PropertyImage
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string IdPropertyImage { get; set; }
        public string IdProperty { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }


    }
}
