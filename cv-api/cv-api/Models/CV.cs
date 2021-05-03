using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cv_api.Models
{
    public class otherListItem
    {
        public string id { get; set; }
        public string role { get; set; }
        public string description { get; set; }
        public bool isChecked { get; set; }
    }
    public class CV
    {
        public string color { get; set; }
        public string company_logo { get; set; }
        public string? company_name { get; set; }
        public string consult_experience_focus_description { get; set; }
        public string consult_experience_focus_role { get; set; }
        public string consult_experience_focus_title { get; set; }
        public string consult_name { get; set; }
        public string consult_role { get; set; }
        public List<string> consult_presentations { get; set; }
        public string consult_picture { get; set; }
        public string contact_email { get; set; }
        public string contact_phoneNumber { get; set; }
        public string contact_website { get; set; }
        public string sale_email { get; set; }
        public string sale_name { get; set; }
        public string sale_phone { get; set; }
        public List<otherListItem> consult_experience_other_list { get; set; }
    }

    public class CVDTO
    {
        public string userID { get; set; }
        public string color { get; set; }
        public string company_logo { get; set; }
        public string company_name { get; set; }
        public string consult_experience_focus_description { get; set; }
        public string consult_experience_focus_role { get; set; }
        public string consult_experience_focus_title { get; set; }
        public string consult_name { get; set; }
        public string consult_role { get; set; }
        public List<string> consult_presentations { get; set; }
        public string consult_picture { get; set; }
        public string contact_email { get; set; }
        public string contact_phoneNumber { get; set; }
        public string contact_website { get; set; }
        public string sale_email { get; set; }
        public string sale_name { get; set; }
        public string sale_phone { get; set; }
        public List<otherListItem> consult_experience_other_list { get; set; }
    }
}
