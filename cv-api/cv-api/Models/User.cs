using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace cv_api.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        //public object _id { get; set; } 
        [Required]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string FirstName { get; set; }

        [Required]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string LastName { get; set; }

        [Required]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string UserName { get; set; }

        [Required]
        //[DataType(DataType.Password)]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Password { get; set; }

        [Required]
        // [DataType(DataType.EmailAddress)]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Role { get; set; }

        [Required]
        // [DataType(DataType.EmailAddress)]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Email { get; set; }


        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string PhoneNo { get; set; }
    }
}
