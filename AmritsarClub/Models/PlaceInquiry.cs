using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmritsarClub.Models
{
    public class PlaceInquiry
    {
        public string Party_Enq_Code { get; set; }
        public string item_code { get; set; }
        public string Party_item_name { get; set; }
        public string Dsg_code { get; set; }
        public string Units { get; set; }
        public string Size { get; set; }
        public decimal? Qtydosg_unit_perpack { get; set; }
        public decimal? No_of_pack { get; set; }
        public decimal? Total_unit { get; set; }
        public string Imagedata { get; set; }
      
    }
}