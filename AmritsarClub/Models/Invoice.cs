using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmritsarClub.Models
{
    public class Invoice
    {
        public string NoOfDays { get; set; }
        public string BillingPartyName { get; set; }
        public string NotifyParty { get; set; }
        public string DispachParty { get; set; }
        public string DispachDestination { get; set; }
        public string FinalDestination { get; set; }
        public int InvoiceNo { get; set; }
        public string ItemName { get; set; }
        public string BatchNo { get; set; }
        public float ItemPrBatch { get; set; }
        public int OrderQuantity { get; set; }
        public int DispatchQuantity { get; set; }
        public float TotalAmount { get; set; }
        public string FinalYear { get; set; }
    }
}