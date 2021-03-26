﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using cv_api.Repository;
using MongoDB.Bson.Serialization.Attributes;

namespace cv_api.Models
{
    [BsonCollection("Users")]
    public class User : Document
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }
    }
}
