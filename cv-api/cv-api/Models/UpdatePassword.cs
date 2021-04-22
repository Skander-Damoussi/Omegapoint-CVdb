using cv_api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cv_api.Models
{
    [BsonCollection("Users")]
    public class UpdatePassword
    {
        public string Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
    
}
