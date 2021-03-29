using cv_api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cv_api.Models
{

    [BsonCollection("Users")]
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
