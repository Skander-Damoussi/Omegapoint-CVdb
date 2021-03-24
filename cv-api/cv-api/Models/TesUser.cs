using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cv_api.Models
{
        
        public class TestUser
        {
            //public object _id { get; set; } 
            
            public string FirstName { get; set; }

            
            public string LastName { get; set; }

            
            
            public string Email { get; set; }

            
            
            public string Role { get; set; }

            
            public string Password { get; set; }
        }
}
