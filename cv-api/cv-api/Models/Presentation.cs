using cv_api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cv_api.Models
{
    public class Presentation
    {
        public string? Title { get; set; }
        public List<string>? Paragraph { get; set; }
        public bool Show { get; set; }
        public string id { get; set; }
    }

    public class PresentationDTO
    {
        public string? title { get; set; }
        public List<string>? Paragraph { get; set; }

        public string userID { get; set; }

        public bool newPresentation { get; set; }

        public bool show { get; set; }

        public string id { get; set; }
    }
}
