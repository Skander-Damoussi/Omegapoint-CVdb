using cv_api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cv_api.Models
{
    public class Experience
    {
        public string Titel { get; set; }
        // C#, Javascript, CSS
        public List<string>? Language { get; set; }
        // MongoDB Compass, Office 365 Admin Portal
        public List<string>? Software { get; set; }
        // In-house Trainee developer 
        public List<string>? Assignments { get; set; }
        // Systemutvecklare
        public List<string>? Role { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
