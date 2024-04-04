using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmritsarClub.Models
{
    public class PackingS
    {
        public string StatusGreen { get; set; }
        public string StatusYellow { get; set; }
        public string WorkOrderNo { get; set; }
        public string ItemName { get; set; }
        public string NoOfDays { get; set; }
        public string BatchNo { get; set; }
        public int BatchSize { get; set; }
        public int OrderQuantity { get; set; }
        public string Uom { get; set; }
        public string DateOfPacking { get; set; }
        public int PackedQuantity { get; set; }

    }
}