using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmritsarClub.Models
{
    public class Product
    {
        public string item_code { get; set; }
        public string Party_Enq_Code { get; set; }
        public string Party_item_name { get; set; }
        public string dsg_form { get; set; }
        public string unit { get; set; }
        public string Size { get; set; }
        public decimal? Qtydosg_unit_perpack { get; set; }
        public decimal? No_of_pack { get; set; }
        public decimal? Total_unit { get; set; }
        public byte[] Product_Image { get; set; }
        public string Imagedata { get; set; }
        public string qtation_status { get; set; }
        public string item_status { get; set; }
        public string Enquiry_Status { get; set; }
        public DateTime date { get; set; }
    }
}