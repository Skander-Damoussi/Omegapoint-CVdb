using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using cv_api.Repository;
using System.IO;

namespace cv_api.Models
{
    [BsonCollection("testCv")]
    public class CVTemplate : Document
    {
        //[Required]
        //[BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string StringId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public byte[] FileByte { get; set; }
        //public string Base64String { get; set; }
        

    }
}
