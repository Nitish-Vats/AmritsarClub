using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmritsarClub.Models
{
    public class RM
    {
        public string NegativeQty { get; set; }
        public int NoOfDays { get; set; }
        public string WorkOrderNo { get; set; }
        public string ItemName { get; set; }
        public string IngredentName { get; set; }
        public string DateOfBlocking { get; set; }
        public string StockAvailability { get; set; }
        public string OrderStatusNO { get; set; }
        public string OrderStaDate { get; set; }
    }
}