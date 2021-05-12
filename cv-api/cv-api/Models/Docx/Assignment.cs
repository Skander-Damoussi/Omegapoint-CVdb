using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cv_api.Models
{
    public class Assignment
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Role { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<String> Experiences { get; set; }

    }
}
