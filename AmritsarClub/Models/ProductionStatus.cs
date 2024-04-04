using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmritsarClub.Models
{
    public class ProductionStatus
    {
        public string NoOfDays { get; set; }
        public string ItemName { get; set; }
        public string BatchNo { get; set; }
        public int BatchSize { get; set; }
        public int OrderQuantity { get; set; }
        public string Unit { get; set; }
        public string ProStartDate { get; set; }
    }
}