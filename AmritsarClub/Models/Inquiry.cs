using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmritsarClub.Models
{
    public class Inquiry
    {
        public string Party_Enq_Code { get; set; }
        public string Party_code { get; set; }
        public string Party_Name { get; set; }
        public string EnquiryDate { get; set; }
        public string des_con { get; set; }
        public string des_air { get; set; }
        public string des_sea { get; set; }
        public string Status { get; set; }
        public string enq_code { get; set; }
    }
}