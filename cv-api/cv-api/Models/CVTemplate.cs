﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace cv_api.Models
{
    public class CVTemplate
    {
        [Required]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Name { get; set; }
    }
}
